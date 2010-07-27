using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace BlackCoreJabber
{
    class database
    {
        private MySqlConnection dbconnection;
        private string constr = "SERVER=localhost;" +
                "DATABASE=blackcore;" +
                "UID=blackcore;" +
                "PASSWORD=blackcore;";
        public bool connect()
        {
            try
            {
                dbconnection = new MySqlConnection(constr);
                
               
                return true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception f)
            {
                Console.WriteLine("Error opening database connection, " + f);
            }
            return false;
        }

        public bool disconnect()
        {

            try
            {
                
                return true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception f)
            {
                Console.WriteLine("Error opening database connection, " + f);
            }
            return false;
        }

        //hack for now, will implement database layer properly later
        public string getResult(string querystring)
        {
            string result = "";
            dbconnection.Open();
            MySqlCommand command = dbconnection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = querystring;
           
            Reader = command.ExecuteReader();
            while (Reader.Read())
            {

                for (int i = 0; i < Reader.FieldCount; i++)
                    result += Reader.GetValue(i).ToString();
                
            }
            dbconnection.Close();
            return result;
        }

        public List<string[]> getMoreResult(string querystring)
        {
        
            List<string[]> resultList = new List<string[]>();
            dbconnection.Open();
            MySqlCommand command = dbconnection.CreateCommand();
            MySqlDataReader Reader;
            command.CommandText = querystring;

            Reader = command.ExecuteReader();
            while (Reader.Read())
            {
                string[] resultcontents = new string[Reader.FieldCount];
                for (int i = 0; i < Reader.FieldCount; i++)
                    resultcontents[i] = Reader.GetValue(i).ToString();
                resultList.Add(resultcontents);

            }
            dbconnection.Close();
            return resultList;
        }

    }
}
