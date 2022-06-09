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
        const string USERNAME = "R52QYsdmFl";
        const string PASSWORD = "cmFhRfTJoF";
        const string DATABASE = "R52QYsdmFl";

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