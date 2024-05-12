namespace EddiNavigationService
{
    public class Query
    {
        public QueryType QueryType { get; }

        public string StringArg0 { get; }

        public string StringArg1 { get; }

        public double? NumericArg { get; }

        public bool? BooleanArg { get; }

        public bool FromUI { get; }

        public Query ( 
            QueryType queryType, 
            string stringArg0 = null, 
            string stringArg1 = null,
            double? numericArg = null, 
            bool? booleanArg = null, 
            bool fromUI = false 
            )
        {
            this.QueryType = queryType;
            this.StringArg0 = stringArg0;
            this.StringArg1 = stringArg1;
            this.NumericArg = numericArg;
            this.BooleanArg = booleanArg;
            this.FromUI = fromUI;
        }
    }
}
