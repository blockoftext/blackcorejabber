using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCoreJabber
{
    class XMPPStanza
    {
        private static string rosterCache = "";
        
        public static string getServerFeatures()
        {
            XMLWriter features = new XMLWriter();

            features.WriteStartElement("stream:features");
            
            features.WriteStartElement("bind");
            features.WriteProperty("xmlns", "urn:ietf:params:xml:ns:xmpp-bind");
            features.closeTag(true);

            features.WriteStartElement("session");
            features.WriteProperty("xmlns", "urn:ietf:params:xml:ns:xmpp-session");
            features.closeTag(true);

            features.WriteEndElement("stream:features");

           // Console.WriteLine(features);
            return features.ToString();
        }

        public static string getServerResponse(string stream_id)
        {
            XMLWriter response = new XMLWriter();

            response.WriteStartElement("stream:stream");
            response.WriteProperty("from", Program.hostName);
            response.WriteProperty("id", stream_id);
            response.WriteProperty("xmlns", "jabber:client");
            response.WriteProperty("xmlns:stream", "http://etherx.jabber.org/streams");
            response.WriteProperty("version", "1.0");
            response.closeTag(false);


         //   Console.WriteLine(response);
            return response.ToString();
        }

        public static string getBindResponse(string stream_id, string fullJID)
        {
            XMLWriter bind = new XMLWriter();

            bind.WriteStartElement("iq");
            bind.WriteProperty("id", stream_id);
            bind.WriteProperty("type", "result");
            bind.WriteStartElement("bind");
            bind.WriteProperty("xmlns", "urn:ietf:params:xml:ns:xmpp-bind");
            bind.WriteStartElement("jid");
            bind.WriteString(fullJID);
            bind.WriteEndElement("jid");
            bind.WriteEndElement("bind");
            bind.WriteEndElement("iq");

        //    Console.WriteLine(bind);
            return bind.ToString();
        }

        public static string getServerStartTLS(string[] supportedMechanisms, bool hasTLS)
        {
            XMLWriter tls = new XMLWriter();
            tls.WriteStartElement("stream:features");
            if (hasTLS)
            {
                tls.WriteStartElement("starttls");
                tls.WriteProperty("xmlns", "urn:ietf:params:xml:ns:xmpp-tls");
                tls.WriteStartElement("required");
                tls.closeTag(true);
                tls.WriteEndElement("starttls");
            }

            tls.WriteStartElement("mechanisms");
            tls.WriteProperty("xmlns", "urn:ietf:params:xml:ns:xmpp-sasl");

            foreach (String s in supportedMechanisms)
            {
                tls.WriteStartElement("mechanism");
                tls.WriteString(s);
                tls.WriteEndElement("mechanism");
            }

            tls.WriteEndElement("mechanisms");
            tls.WriteEndElement("stream:features");

          //  Console.WriteLine(tls);
            return tls.ToString();
        }

        public static string getRoster(string stream_id, string destination)
        {
          /*  if (!rosterCache.Equals(""))
            {
                return rosterCache;
            }*/

            XMLWriter roster = new XMLWriter();

            roster.WriteStartElement("iq");
            roster.WriteProperty("to", destination);
            roster.WriteProperty("id", stream_id);
            roster.WriteProperty("type", "result");
            roster.WriteStartElement("query");
            roster.WriteProperty("xmlns", "jabber:iq:roster");

            foreach (User u in User.userCache)
            {
                roster.WriteStartElement("item");
                roster.WriteProperty("jid", u.getJID());
                roster.WriteProperty("name", u.username);
                roster.WriteProperty("subscription", "both");
                roster.WriteStartElement("group");
                roster.WriteString(Corperation.getCorpNameByID(u.corpid));
                roster.WriteEndElement("group");
                roster.WriteEndElement("item");
            }
            roster.WriteEndElement("query");
            roster.WriteEndElement("iq");

         //   Console.WriteLine(roster);
            rosterCache = roster.ToString();
            return roster.ToString();
        }

        public static string getMessage(string to, string from, string body) 
        {
            XMLWriter message = new XMLWriter();

            message.WriteStartElement("message");
            message.WriteProperty("from", from);
            message.WriteProperty("to", to);
            message.WriteProperty("type", "chat");
            message.WriteStartElement("body");
            message.WriteString(body);
            message.WriteEndElement("body");
            message.WriteStartElement("active");
            message.closeTag(true);
            message.WriteEndElement("message");
            Console.WriteLine(message);
            return message.ToString();
        }
        public static string getMessageNoBody(string to, string from, string state) 
        {
            XMLWriter message = new XMLWriter();

            message.WriteStartElement("message");
            message.WriteProperty("from", from);
            message.WriteProperty("to", to);
            message.WriteProperty("type", "chat");
            if (!state.Equals(""))
            {
                message.WriteStartElement(state);
                message.closeTag(true);
            }
            message.WriteEndElement("message");
            Console.WriteLine(message);
            return message.ToString();
        }
    }
}
