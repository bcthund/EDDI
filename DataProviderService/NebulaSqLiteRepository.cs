using EddiDataDefinitions;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using Utilities;

namespace EddiDataProviderService
{
    public class NebulaSqLiteRepository : SqLiteBaseRepository, INebulaRepository
    {
        // Creates a simple 

        public static bool unitTesting;

        private const string CREATE_TABLE_SQL = @" 
                    CREATE TABLE IF NOT EXISTS nebula
                    (
                        nebulaId INT PRIMARY KEY UNIQUE NOT NULL,
                        visited TINYINT(1) DEFAULT 0
                     ) WITHOUT ROWID;";

        private const string INSERT_SQL = @" 
                    INSERT INTO nebula
                    (
                        nebulaId,
                        visited
                    )";

        private const string UPDATE_SQL = @" 
                    UPDATE nebula
                        SET 
                            visited = @visited
                    ";

        private const string DELETE_SQL = @"DELETE FROM nebula ";
        private const string SELECT_SQL = @"SELECT * FROM nebula ";

        private const string VALUES_SQL = @" 
                    VALUES
                    (
                        @nebulaId, 
                        @visited
                    )";

        private const string WHERE_NEBULAID = @"WHERE nebulaId = @nebulaId; PRAGMA optimize;";

        private static NebulaSqLiteRepository instance;

        private NebulaSqLiteRepository()
        { }

        private static readonly object instanceLock = new object();
        public static NebulaSqLiteRepository Instance
        {
            get
            {
                if ( instance == null )
                {
                    lock ( instanceLock )
                    {
                        if ( instance == null )
                        {
                            Logging.Debug( "No NebulaSqLiteRepository instance: creating one" );
                            instance = new NebulaSqLiteRepository();
                            CreateOrUpdateDatabase();
                        }
                    }
                }
                return instance;
            }
        }

        public void ToggleNebulaVisited(int? nebulaId) {
            bool current = GetNebulaVisited(nebulaId);
            SaveNebulaVisited(nebulaId, !current);
        }

        public bool GetNebulaVisited(int? nebulaId)
        {
            if (nebulaId==null) { return false; }

            if (!File.Exists(DbFile)) { return false; }
            DatabaseNebula result = Instance.ReadNebula(nebulaId);

            if (result == null) { return false; }            
            return result.visited;
        }

        [NotNull, ItemNotNull]
        private DatabaseNebula ReadNebula(int? nebulaId)
        {
            DatabaseNebula result = null;
            using (var con = SimpleDbConnection())
            {
                con.Open();
                using (var cmd = new SQLiteCommand(con))
                {
                    using (var transaction = con.BeginTransaction())
                    {
                        try
                        {
                            cmd.Prepare();
                            cmd.Parameters.AddWithValue("@nebulaId", nebulaId);
                            cmd.CommandText = SELECT_SQL + WHERE_NEBULAID;
                            result = ReadNebulaEntry( cmd );
                        }
                        catch (SQLiteException)
                        {
                            Logging.Warn("Problem reading data for Nebula ID '" + nebulaId + "' from database, returning null.");
                            //RecoverNebulaDB();
                            //Instance.GetNebulaVisited( nebulaId );
                        }
                        transaction.Commit();
                    }
                }
            }
            return result;
        }

        public void GetNebulaeVisited ( ref List<Nebula> nebulae )
        {
            if ( !File.Exists( DbFile ) )
            { return; }
            if ( !nebulae.Any() )
            { return; }

            // Read the list of Nebulae, automatically update the visited property.
            Instance.ReadNebulae( ref nebulae );
        }

        private bool ReadNebulae ( ref List<Nebula> nebulae )
        {
            if ( !nebulae.Any() )
            { return false; }

            using ( var con = SimpleDbConnection() )
            {
                con.Open();
                using ( var cmd = new SQLiteCommand( con ) )
                {
                    using ( var transaction = con.BeginTransaction() )
                    {
                        foreach ( Nebula nebula in nebulae )
                        {
                            try
                            {
                                cmd.Prepare();
                                cmd.Parameters.AddWithValue( "@nebulaId", nebula.id );
                                cmd.CommandText = SELECT_SQL + WHERE_NEBULAID;

                                DatabaseNebula result = ReadNebulaEntry( cmd ) ?? new DatabaseNebula( nebula.id, false );
                                nebula.visited = result.visited;

                            }
                            catch ( SQLiteException )
                            {
                                Logging.Warn( "Problem reading data for Nebula ID '" + nebula.id + "' from database." );
                                //RecoverNebulaDB();
                                //Instance.GetNebulaVisited( nebula.id );
                            }
                        }
                        transaction.Commit();
                    }
                }
            }
            return true;
        }

        private DatabaseNebula ReadNebulaEntry(SQLiteCommand cmd)
        {
            int nebulaId = 0;
            bool visited = false;

            using (SQLiteDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.Read())
                {
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        if (rdr.GetName(i) == "nebulaId")
                        {
                            nebulaId = rdr.IsDBNull(i) ? 0 : rdr.GetInt32(i);
                        }

                        if (rdr.GetName(i) == "visited")
                        {
                            visited = !rdr.IsDBNull(i) && rdr.GetBoolean(i);
                        }
                    }
                }
            }
            return new DatabaseNebula(nebulaId, visited);
        }

        public void SaveNebulaVisited(int? nebulaId, bool visited)
        {
            if (nebulaId == null || unitTesting) { return; }

            DatabaseNebula dbNebula = Instance.ReadNebula(nebulaId);

            if ( dbNebula is null )
            {
                Instance.insertNebula( nebulaId, visited );
            }
            else
            {
                Instance.updateNebula( nebulaId, visited );
            }
        }

        private void insertNebula(int? nebulaId, bool visited)
        {
            if ( nebulaId==null ) { return; }

            lock ( nameof( SimpleDbConnection ) ) // Lock before writing to the database
            {
                using ( var con = SimpleDbConnection() )
                {
                    try
                    {
                        con.Open();
                        using ( var cmd = new SQLiteCommand( con ) )
                        {
                            using ( var transaction = con.BeginTransaction() )
                            {
                                Logging.Debug( "Inserting new Nebula ID " + nebulaId );
                                cmd.CommandText = INSERT_SQL + VALUES_SQL;
                                cmd.Prepare();
                                cmd.Parameters.AddWithValue( "@nebulaId", nebulaId );
                                cmd.Parameters.AddWithValue( "@visited", visited );
                                cmd.ExecuteNonQuery();

                                transaction.Commit();
                            }
                        }
                    }
                    catch ( SQLiteException ex )
                    {
                        handleSqlLiteException( con, ex );
                    }
                }
            }
        }

        private void updateNebula(int? nebulaId, bool visited)
        {
            if ( nebulaId==null ) { return; }

            lock ( nameof( SimpleDbConnection ) ) // Lock before writing to the database
            {
                using ( var con = SimpleDbConnection() )
                {
                    try
                    {
                        con.Open();
                        using ( var cmd = new SQLiteCommand( con ) )
                        {
                            using ( var transaction = con.BeginTransaction() )
                            {

                                cmd.CommandText = UPDATE_SQL + WHERE_NEBULAID;
                                cmd.Prepare();
                                cmd.Parameters.AddWithValue( "@nebulaId", nebulaId );
                                cmd.Parameters.AddWithValue( "@visited", visited );
                                cmd.ExecuteNonQuery();

                                transaction.Commit();
                            }
                        }
                    }
                    catch ( SQLiteException ex )
                    {
                        handleSqlLiteException( con, ex );
                    }
                }
            }
        }

        private void deleteNebula(int? nebulaId)
        {
            lock ( nameof( SimpleDbConnection ) ) // Lock before writing to the database
            {
                using ( var con = SimpleDbConnection() )
                {
                    try
                    {
                        con.Open();
                        using ( var cmd = new SQLiteCommand( con ) )
                        {
                            using ( var transaction = con.BeginTransaction() )
                            {
                                
                                cmd.CommandText = DELETE_SQL + WHERE_NEBULAID;
                                cmd.Prepare();
                                cmd.Parameters.AddWithValue( "@nebulaId", nebulaId );
                                cmd.ExecuteNonQuery();

                                transaction.Commit();
                            }
                        }
                    }
                    catch ( SQLiteException ex )
                    {
                        handleSqlLiteException( con, ex );
                    }
                }
            }
        }

        private static void CreateOrUpdateDatabase()
        {
            lock ( nameof( SimpleDbConnection ) ) // Lock before writing to the database
            {
                using ( var con = SimpleDbConnection() )
                {
                    try
                    {
                        con.Open();

                        using ( var cmd = new SQLiteCommand( CREATE_TABLE_SQL, con ) )
                        {
                            Logging.Debug( "Preparing Nebula repository" );
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch ( SQLiteException ex )
                    {
                        handleSqlLiteException( con, ex );
                    }
                }
            }
            Logging.Debug( "Nebula repository ready." );
        }

        public void RecoverNebulaDB ()
        {
            lock ( nameof( SimpleDbConnection ) ) // Lock before writing to the database
            {
                using ( var con = SimpleDbConnection() )
                {
                    try
                    {
                        con.Close();
                        SQLiteConnection.ClearAllPools();
                        File.Delete( Constants.DATA_DIR + @"\EDDI.sqlite" );
                    }
                    catch ( SQLiteException ex )
                    {
                        handleSqlLiteException( con, ex );
                    }
                }
            }

            CreateOrUpdateDatabase();
        }

        private static void handleSqlLiteException(SQLiteConnection con, SQLiteException ex)
        {
            Logging.Warn( "SQLite error: ", ex.ToString() );

            try
            {
                con.BeginTransaction()?.Rollback();
            }
            catch ( SQLiteException ex2 )
            {
                Logging.Warn( "SQLite transaction rollback failed." );
                Logging.Warn( "SQLite error: ", ex2.ToString() );
            }
            finally
            {
                con.Dispose();
            }
        }
    }
}
