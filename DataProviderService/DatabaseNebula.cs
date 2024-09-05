using System;

namespace EddiDataProviderService
{
    public class DatabaseNebula
    {
        // Data as read from columns in our database
        public int nebulaId { get; private set; }
        public bool visited { get; private set; }

        public DatabaseNebula(int nebulaId, bool visited)
        {
            this.nebulaId = nebulaId;
            this.visited = visited;
        }
    }
}
