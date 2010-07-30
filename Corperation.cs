using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlackCoreJabber
{
    class Corperation
    {
        public int id;
        public int allianceid;
        public string name;
        public string ticker;
        public static List<Corperation> corperationCache;

        public Corperation(int id, string name, int allianceid, string ticker)
        {
            this.id = id;
            this.allianceid = allianceid;
            this.name = name;
            this.ticker = ticker;
        }
       

        public static List<string[]> getCorpDetailList()
        {
            List<string[]> corpList = Program.userDatabase.getMoreResult("select * from BlackCore.corp");
            return corpList;
        }

        public static bool loadCorps()
        {
            corperationCache = new List<Corperation>();
            List<string[]> listing = Corperation.getCorpDetailList();
            if (listing.Count == 0) return false;
            foreach (string[] s in listing)
            {
                Corperation temp = new Corperation(int.Parse(s[0]), s[1], int.Parse(s[2]), s[3]);
                corperationCache.Add(temp);
            }
            return true;

        }

        public static List<Corperation> getCorpsByAllianceID(int id)
        {
            List<Corperation> corpList = new List<Corperation>();
            foreach (Corperation c in corperationCache)
            {
                if (c.allianceid == id)
                {
                    corpList.Add(c);
                }
            }
            return corpList;
        }

        public static int getCorpIdByName(string name)
        {
            int id = -1;

            foreach (Corperation a in corperationCache)
            {
                if (a.name.Equals(name))
                {
                    id = a.id;
                }
            }

            return id;
        }

        public static string getCorpNameByID(int id)
        {
            string tempstring = "";

            foreach (Corperation a in corperationCache)
            {
                if (a.id == id)
                {
                    tempstring = a.name;
                }
            }

            return tempstring;
        }

        public static void updateTableByID(DataGridView table, int allianceid)
        {
            table.Rows.Clear();
            List<Corperation> templist = Corperation.getCorpsByAllianceID(allianceid);
            foreach (Corperation a in templist)
            {
                string[] tempstring = new string[4];
                tempstring[0] = a.id.ToString();
                tempstring[1] = a.name;
                tempstring[2] = a.ticker;
                tempstring[3] = Alliance.getAllianceNameByID(a.allianceid);

                table.Rows.Add(tempstring);
            }
        }
    }
}
