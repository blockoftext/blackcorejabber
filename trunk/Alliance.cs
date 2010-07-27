using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCoreJabber

{
    class Alliance
    {

        public static List<string[]> getAllianceDetailList()
        {
            List<string[]> allianceList = Program.userDatabase.getMoreResult("select * from BlackCore.alliance");
            return allianceList;
        }
    }
}
