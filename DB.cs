using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace word_picker
{
    public class DB
    {
        const string SERVER = "remotemysql.com";
        const string PORT = "3306";
        const string USERNAME = "Vg2wUPpA4t";
        const string PASSWORD = "1J8x3ko5xk";
        const string DATABASE = "Vg2wUPpA4t";

        MySqlConnection connection = new MySqlConnection($"server={SERVER};port={PORT};username={USERNAME};password={PASSWORD};database={DATABASE}");
        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public MySqlConnection GetConnection()
        {
            return connection;
        }
    }
}