using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using Excel;
using ConfigSql_Core;

namespace WindowsFormsApplication2
{
    class ConnectToServer
    {
        private static ConnectToServer instance = null;
        private static SqlConnection conn;

        private static ConfigSql_Core.Class1 config = new Class1();

        /// <summary>
        /// Get instance of class
        /// </summary>
        public static ConnectToServer Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new ConnectToServer();
                }
                return instance;
            }
        }

        /// <summary>
        /// Constructer
        /// </summary>
        private ConnectToServer()
        {
            conn = new SqlConnection();
            conn.ConnectionString = config.GetConnectionString();
        }

        /// <summary>
        /// Return tabel from SQl command
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetTable(string sql)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// Excute SQL do not return
        /// </summary>
        /// <param name="sql"></param>
        public void ExecuteQuerryNotReturn(string sql)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            cmd.Dispose();

        }

        /// <summary>
        /// Excute command sql
        /// </summary>
        /// <param name="storename"></param>
        /// <returns></returns>
        public DataTable ThucHiencommand(string storename)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlCommand cmd =new SqlCommand(storename, conn);
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;

        }

        /// <summary>
        /// Close connection
        /// </summary>
        public void CloseConnecttion()
        {
            conn.Close();
        }
      
    }
}
