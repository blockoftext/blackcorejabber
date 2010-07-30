using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace BlackCoreJabber

{
    class Alliance
    {
        public int ID;
        public string name;
        public string ticker;

        public static List<Alliance> allianceCache;
        public Alliance(int id, string name, string ticker)
        {
            this.ID = id;
            this.name = name;
            this.ticker = ticker;
        }
        public static List<string[]> getAllianceDetailList()
        {
            List<string[]> allianceList = Program.userDatabase.getMoreResult("select * from BlackCore.alliance");
            return allianceList;
        }

        public static bool loadAlliances(){
            allianceCache = new List<Alliance>();
            List<string[]> listing = Alliance.getAllianceDetailList();
            if (listing.Count == 0) return false;
            foreach(string[] s in listing){
                Alliance temp = new Alliance(int.Parse(s[0]), s[1], s[2]);
                allianceCache.Add(temp);
            }
        
            return true;

        }

        public static string getAllianceNameByID(int id)
        {
            string tempstring = "";

            foreach(Alliance a in allianceCache)
            {
                if (a.ID == id)
                {
                    tempstring = a.name;
                }
            }

            return tempstring;
        }

        public static int getAllianceIdByName(string name)
        {
            int id = -1;

            foreach (Alliance a in allianceCache)
            {
                if (a.name.Equals(name))
                {
                    id = a.ID;
                }
            }

            return id;
        }
        public static void updateTable(DataGridView table){
            foreach (Alliance a in allianceCache)
            {
                string[] tempstring = new string[3];
                tempstring[0] = a.ID.ToString();
                tempstring[1] = a.name;
                tempstring[2] = a.ticker;
                
                table.Rows.Add(tempstring);
            }
        }
    }
}
