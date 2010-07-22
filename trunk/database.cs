using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace WindowsFormsApplication1
{
    class database
    {
        private MySqlConnection dbconnection;
        private string constr = "SERVER=localhost;" +
                "DATABASE=blackcore;" +
                "UID=blackcore;" +
                "PASSWORD=blackcore;";
        public void connect()
        {
            try
            {
                dbconnection = new MySqlConnection(constr);
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e);
            }
            
        }
    }
}
