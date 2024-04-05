﻿using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Platform_Final
{
    public class DBConnection
    {
        MySqlConnection connect = new MySqlConnection("server=localhost; port=3307; user id=root; password=12345; database=Freelance");

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