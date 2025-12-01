// phongconnection.cs
using System;
using System.Data;
using System.Data.SqlClient;

namespace QlKhachSan
{
    
    public class Connection
    {
        // Chuỗi kết nối SQL Server (có thể thay đổi nếu cần)
        private static string stringConnection =
            @"Data Source=localhost,1433;Initial Catalog=qlkhachsan;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        // Phương thức trả về đối tượng SqlConnection
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(stringConnection);
        }

        // Constructor (không bắt buộc, để lại cho rõ ràng)
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