using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Data;

namespace DataAccsessLayer
{
    public class SqlDataProvider
    {
        public SqlConnection Connection { get; set; }
        public SqlCommand Command { get; set; }

        public SqlDataProvider(string connStr)
        {
            this.Connection = new SqlConnection(connStr);
            this.Command = this.Connection.CreateCommand();

        }
        public DataTable GetDataTable()
        {

        }

        public object GetSingleData()
        {

        }
    }
}
