using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;


namespace BlackCoreJabber
{
    class EVEAPI
    {
        public static string webFetch(string uri, string apiID, string apiKey)
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

        public static byte[] getPost(string userID, string apiKey)
        {
            Dictionary<string, string> variables = new Dictionary<string, string>();
            variables.Add("userID", userID);
            variables.Add("apiKey", apiKey);
            ASCIIEncoding encoding=new ASCIIEncoding();
            return encoding.GetBytes( dictionaryToPostString(variables));
        }


        public static string dictionaryToPostString(Dictionary<string, string> postVariables)
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
