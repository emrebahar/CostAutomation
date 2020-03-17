using System;

namespace DataAccsessLayer
{
    public class SqlConnection
    {
        private string connStr;

        public SqlConnection(string connStr)
        {
            this.connStr = connStr;
        }

        internal SqlCommand CreateCommand()
        {
            throw new NotImplementedException();
        }
    }
}