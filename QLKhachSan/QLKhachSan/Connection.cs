
using System;
using System.Data;
using System.Data.SqlClient;

namespace QlKhachSan
{
    
    public class Connection
    {
      
        private static string stringConnection =
            @"Data Source=HUAL;Initial Catalog=qlkhachsan;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

      
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(stringConnection);
        }

       
        public Connection()
        {
        }

        private SqlDataAdapter dataAdapter;
        private SqlCommand sqlCommand;

        public DataTable Table(string query)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                sqlConnection.Open();
                dataAdapter = new SqlDataAdapter(query, sqlConnection);
                dataAdapter.Fill(dataTable);
                sqlConnection.Close();
            }
            return dataTable;
        }
        public void Command(string query)
        {
            using (SqlConnection sqlConnection = GetSqlConnection())
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
    }
}