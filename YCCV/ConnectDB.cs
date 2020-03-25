using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YCCV
{
    class ConnectDB
    {
        public static SqlConnection DataConnection()
        {
            string connectionString = null;
            connectionString = @"Data Source=.;Intergrated Security = true";
            SqlConnection cnn = new SqlConnection(connectionString);
            return cnn;
        }
    }
}
