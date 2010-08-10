using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackCoreJabber
{
    class XMPPStanza
    {
        private static string rosterCache = "";
        
        //generates response string for server feature list
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
  
            return features.ToString();
        }

        //generates response string for initial server response to client connection
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
            return response.ToString();
        }

        //generates response string for resource bind request
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

            return bind.ToString();
        }

        //generates response string for advertising TLS/Auth mechanisms
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


            return tls.ToString();
        }

        //generates string for sending roster
        public static string getRoster(string stream_id, string destination)
        {
            //TODO: Cache list, but not header
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
                roster.WriteProperty("name", u.characterid);
                roster.WriteProperty("subscription", "both");
                roster.WriteStartElement("group");
                roster.WriteString(Corperation.getCorpNameByID(u.corpid));
                roster.WriteEndElement("group");
                roster.WriteEndElement("item");
            }
            roster.WriteEndElement("query");
            roster.WriteEndElement("iq");


            rosterCache = roster.ToString();
            return roster.ToString();
        }

        //generates chat message response
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
            message.WriteStartElement("acparetive");
            message.closeTag(true);
            message.WriteEndElement("message");

            return message.ToString();
        }

        //generates chat state message
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
   
            return message.ToString();
        }

        //generates presence message
        public static string getPresenceString(string from, string to)
        {
            XMLWriter presence = new XMLWriter();

            presence.WriteStartElement("presence");
            presence.WriteProperty("xmlns", "jabber:client");
            presence.WriteProperty("from", from);
            presence.WriteProperty("to", to);
            presence.closeTag(true);
            return presence.ToString();
        }

        public static string getPresenceString(string from, string to, string show)
        {
            XMLWriter presence = new XMLWriter();

            presence.WriteStartElement("presence");
            presence.WriteProperty("xmlns", "jabber:client");
            presence.WriteProperty("from", from);
            presence.WriteProperty("to", to);

            presence.WriteStartElement("show");
            presence.WriteString(show);
            presence.WriteEndElement("show");

            presence.WriteEndElement("presence");
            return presence.ToString();
        }

        public static string getPresenceString(string from, string to, string show, string status)
        {
            XMLWriter presence = new XMLWriter();

            presence.WriteStartElement("presence");
            presence.WriteProperty("xmlns", "jabber:client");
            presence.WriteProperty("from", from);
            presence.WriteProperty("to", to);

            presence.WriteStartElement("show");
            presence.WriteString(show);
            presence.WriteEndElement("show");

            presence.WriteStartElement("status");
            presence.WriteString(status);
            presence.WriteEndElement("status");

            presence.WriteEndElement("presence");
            return presence.ToString();
        }

        public static string getNovCard(string streamid, string to)
        {
            XMLWriter vCard = new XMLWriter();

            vCard.WriteStartElement("iq");
            vCard.WriteProperty("id", streamid);
            vCard.WriteProperty("to", to);
            vCard.WriteProperty("type", "result");

            vCard.WriteStartElement("vCard");
            vCard.WriteProperty("xmlns", "vcard-temp");
            vCard.closeTag(true);

            vCard.WriteEndElement("iq");

            return vCard.ToString();
        }

        public static string getNoOthersvCard(string streamid, string to)
        {
            XMLWriter vCard = new XMLWriter();

            vCard.WriteStartElement("iq");
            vCard.WriteProperty("id", streamid);
            vCard.WriteProperty("to", to);        
            vCard.WriteProperty("type", "error");

            vCard.WriteStartElement("vCard");
            vCard.WriteProperty("xmlns", "vcard-temp");
            vCard.closeTag(true);

            vCard.WriteStartElement("error");
            vCard.WriteProperty("type", "cancel");
            vCard.WriteStartElement("service-unavailable");
            vCard.WriteProperty("xmlns", "urn:ietf:params:xml:ns:xmpp-stanzas");
            vCard.closeTag(true);

            vCard.WriteEndElement("error");
            vCard.WriteEndElement("iq");

            return vCard.ToString();
        }

        public static string registryResponse = "";
        public static string getRegistryResponse(string streamid)
        {
            if (registryResponse.Equals(""))
            {
                XMLWriter response = new XMLWriter();

                response.WriteStartElement("iq");
                response.WriteProperty("type", "result");
                response.WriteProperty("id", streamid);

                response.WriteStartElement("query");
                response.WriteProperty("xmlns", "jabber:iq:register");
                                
                response.WriteStartElement("x");
                response.WriteProperty("xmlns", "jabber:x:data");
                response.WriteProperty("type", "form");

                response.WriteStartElement("title");
                response.WriteString("Black Core Jabber registration");
                response.WriteEndElement("title");

                response.WriteStartElement("instructions");
                response.WriteString("Provide Username, Password, apiID and apiKey");
                response.WriteEndElement("instructions");

                response.WriteStartElement("field");
                response.WriteProperty("type", "text-single");
                response.WriteProperty("label", "username");
                response.WriteProperty("var", "username");
                response.WriteStartElement("required");
                response.closeTag(true);
                response.WriteEndElement("field");

                response.WriteStartElement("field");
                response.WriteProperty("type", "text-single");
                response.WriteProperty("label", "password");
                response.WriteProperty("var", "password");
                response.WriteStartElement("required");
                response.closeTag(true);
                response.WriteEndElement("field");

                response.WriteStartElement("field");
                response.WriteProperty("type", "text-single");
                response.WriteProperty("label", "apiID");
                response.WriteProperty("var", "apiID");
                response.WriteStartElement("required");
                response.closeTag(true);
                response.WriteEndElement("field");

                response.WriteStartElement("field");
                response.WriteProperty("type", "text-single");
                response.WriteProperty("label", "apiKey");
                response.WriteProperty("var", "apiKey");
                response.WriteStartElement("required");
                response.closeTag(true);
                response.WriteEndElement("field");

                response.WriteEndElement("x");
                response.WriteEndElement("query");
                response.WriteEndElement("iq");

                return response.ToString();
            }
            else
            {
                return registryResponse;
            }
        }

        public static string getRegistrationFail(string streamid, string code)
        {
            XMLWriter regfail = new XMLWriter();

            regfail.WriteStartElement("iq");
            regfail.WriteProperty("id", streamid);
           // regfail.WriteProperty("to", to);
            regfail.WriteProperty("type", "error");

            regfail.WriteStartElement("query");
            regfail.WriteProperty("xmlns", "jabber:iq:register");
            regfail.closeTag(true);

            regfail.WriteStartElement("error");
           // regfail.WriteProperty("code", "400");
            regfail.WriteProperty("type", "cancel");
            regfail.WriteStartElement(code);
            regfail.WriteProperty("xmlns", "urn:ietf:params:xml:ns:xmpp-stanzas");
            regfail.closeTag(true);
            regfail.WriteEndElement("error");
            regfail.WriteEndElement("iq");

            return regfail.ToString();
        }
    }


}
