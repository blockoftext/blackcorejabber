using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Xml;

namespace BlackCoreJabber
{
    class EVEAPI
    {
        private static string Characters = "http://api.eveonline.com/account/Characters.xml.aspx";

        public static Dictionary<string, string> getCharacterCorpsFromAccount(string apiId, string apiKey)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            string response = EVEAPI.webFetch(Characters, apiId, apiKey);
           // Program.mainWindow.log(response, null, 1);

            if (response.Length > 0)
            {
                try
                {
                    NameTable nt = new NameTable();
                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(nt);
                    //  nsmgr.AddNamespace("stream", "http://etherx.jabber.org/streams");

                    // Create the XmlParserContext.
                    XmlParserContext context = new XmlParserContext(null, nsmgr, null, XmlSpace.None);

                    // Create the reader. 
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.ConformanceLevel = ConformanceLevel.Fragment;
                    settings.IgnoreWhitespace = true;
                    XmlReader reader = XmlReader.Create(new StringReader(response), settings, context);

                    while (reader.Read())
                    {
                        if (reader.Name.Equals("error"))
                        {
                            Program.mainWindow.log("Incorrect apiId or Key: " + apiId + " ; " + apiKey, null, 1);
                            return null;
                        }
                        else if (reader.Name.Equals("row"))
                        {
                            string name, corporationName;
                            reader.MoveToAttribute("name");
                            name = reader.Value;
                            reader.MoveToAttribute("corporationName");
                            corporationName = reader.Value;
                            result.Add(name, corporationName);
                        }


                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("error registering user " + e);
                }

            }
            return result;
        }

        /*  <?xml version='1.0' encoding='UTF-8'?>
            <eveapi version="2">
            <currentTime>2010-08-09 20:42:47</currentTime>
            <result>
            <rowset name="characters" key="characterID" columns="name,characterID,corporationName,corporationID">
            <row name="Isyn" characterID="174997134" corporationName="Science and Trade Institute" corporationID="1000045" />
            <row name="Quarius Pick" characterID="1849830333" corporationName="University of Caille" corporationID="1000115" />
            <row name="arith danta" characterID="1932128501" corporationName="The Scurvy Pirates" corporationID="826740850" />
            </rowset>
            </result>
            <cachedUntil>2010-08-09 21:36:56</cachedUntil>
            </eveapi>*/

        /*  <?xml version='1.0' encoding='UTF-8'?>
            <eveapi version="2">
            <currentTime>2010-08-09 20:44:20</currentTime>
            <error code="106">Must provide userID parameter for authentication.</error>
            <cachedUntil>2010-08-09 20:49:20</cachedUntil>
            </eveapi>*/

        private static string webFetch(string uri, string apiID, string apiKey)
        {
            // used to build entire input
            StringBuilder sb = new StringBuilder();

            // used on each read operation
            byte[] buf = new byte[8192];
            byte[] postString = getPost(apiID, apiKey);
          
            // prepare the web page we will be asking for
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postString.Length;

            Stream newStream = request.GetRequestStream();
            newStream.Write(postString, 0, postString.Length);
            newStream.Close();

            // execute the request
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            // we will read data via the response stream
            Stream resStream = response.GetResponseStream();

            string tempString = null;
            int count = 0;

            do
            {
                // fill the buffer with data
                count = resStream.Read(buf, 0, buf.Length);

                // make sure we read some data
                if (count != 0)
                {
                    // translate from bytes to ASCII text
                    tempString = Encoding.ASCII.GetString(buf, 0, count);

                    // continue building the string
                    sb.Append(tempString);
                }
            }
            while (count > 0); // any more data to read?

            // print out page source
           return sb.ToString();
        }

        private static byte[] getPost(string userID, string apiKey)
        {
            Dictionary<string, string> variables = new Dictionary<string, string>();
            variables.Add("userID", userID);
            variables.Add("apiKey", apiKey);
            ASCIIEncoding encoding=new ASCIIEncoding();
            return encoding.GetBytes( dictionaryToPostString(variables));
        }


        private static string dictionaryToPostString(Dictionary<string, string> postVariables)
        {
            string postString = "";
            foreach (KeyValuePair<string, string> pair in postVariables)
            {
             
                postString += HttpUtility.UrlEncode(pair.Key) + "=" +
                    HttpUtility.UrlEncode(pair.Value) + "&";
            }
           
            return postString;
        }
    }
}
