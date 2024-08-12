using MySqlConnector;

namespace Freelance_Platform_Final
{
    public class DBConnection
    {
        readonly MySqlConnection connect = new("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");

        public MySqlConnection GetConnection()
        {
            return connect;
        }

        public void OpenConnection()
        {
            if (connect.State == System.Data.ConnectionState.Closed)
            {
                connect.Open();
            }
        }

        public void CloseConnection()
        {
            if (connect.State == System.Data.ConnectionState.Open)
            {
                connect.Close();
            }
        }
    }
}
