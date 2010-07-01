using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;
using System.IO;

namespace WindowsFormsApplication1
{
    class XMPP
    {

        enum auth_type
        {
            PLAIN,
            MD5_DIGEST
        };

        public static string server_xml = "<?xml version='1.0'?>";

        public static string server_response = "<stream:stream " +
            "from='192.168.1.100' " +
            "id='asdf' " +
            "xmlns='jabber:client' " +
            "xmlns:stream='http://etherx.jabber.org/streams' " +
            "version='1.0'>";

        public static string server_features = "<stream:features>" +
            "<bind xmlns='urn:ietf:params:xml:ns:xmpp-bind'/>" +
            "<session xmlns='urn:ietf:params:xml:ns:xmpp-session'/>" +
            "</stream:features>";
            
        public static string server_starttls = "<stream:features>" +
          /*  "<starttls xmlns='urn:ietf:params:xml:ns:xmpp-tls'>" +
            "<required/>" +
            "</starttls>" +*/
            "<mechanisms xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>" +           
            "<mechanism>PLAIN</mechanism>" +
            "<mechanism>MD5-DIGEST</mechanism>" +
            //"</required>"+
            "</mechanisms>" +
            "</stream:features>";
        public static string server_authok = "<success xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>";

        public static string create_response(string stream_id)
        {
           return "<stream:stream " +
            "from='192.168.1.100' " +
            "id='" + stream_id + "' " +
            "xmlns='jabber:client' " +
            "xmlns:stream='http://etherx.jabber.org/streams' " +
            "version='1.0'>";
        }

        public static void handleIncomingMessage(User activeUser, byte[] message)
        {
  
            string messageString = Encoding.ASCII.GetString(message);
            string[] stringArray =  messageString.Split('\0');
            string splitMessage = stringArray[0];
            splitMessage = splitMessage + " ";
           // Program.form1.addText("Message length: " + splitMessage.Length + " Message: " + splitMessage);
      
           
            if (splitMessage.Length > 0)
            {
                try
                {
   
                    // Create the XmlNamespaceManager.
                    NameTable nt = new NameTable();
                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(nt);
                    nsmgr.AddNamespace("stream", "http://etherx.jabber.org/streams");

                    // Create the XmlParserContext.
                    XmlParserContext context = new XmlParserContext(null, nsmgr, null, XmlSpace.None);

                    // Create the reader. 
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.ConformanceLevel = ConformanceLevel.Fragment;
                    settings.IgnoreWhitespace = true;
                    XmlReader reader = XmlReader.Create(new StringReader(splitMessage), settings, context);

                    while (reader.Read())
                    {
                       /* if (reader.Name.Equals("xml"))
                        {
                            continue;
                        }

                        if (reader.HasValue)
                        {

                            Program.form1.addText("" + reader.NodeType + " [" + reader.Name + "] = " + reader.Value);
                        }

                        else
                        {
                            Program.form1.addText("" + reader.NodeType + " [" + reader.Name + "]");
                        }*/

                        if (reader.Name.Equals("stream:stream"))
                        {
                            handleStream(activeUser);
                            break;
                        }
                        else if (reader.Name.Equals("auth") && reader.NodeType == XmlNodeType.Element)
                        {
                            handleAuthMessage(activeUser, reader);     
                            break;
                        }
                        else if (reader.Name.Equals("iq") && reader.NodeType == XmlNodeType.Element)
                        {
                            handleIQ(activeUser, reader);
                            break;
                        }
                        else if (reader.Name.Equals("presence") && reader.NodeType == XmlNodeType.Element)
                        {
                            Program.form1.addText("omg presence");
                            break;
                        }

                    }

                }
                catch (XmlException e)
                {
                    Console.WriteLine("XML Exception: " + e);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error parsing message: " + e);
                }

            }

           
        }
        public static void handleIQ(User activeUser, XmlReader reader)
        {
            Program.form1.addText("Handling IQ from: " + activeUser.username);
            string type;
            string stream_id = "nil";
            reader.MoveToAttribute("id");
            if (reader.ReadAttributeValue())
            {
                stream_id = reader.Value;
            }
            reader.MoveToAttribute("type");
            if (reader.ReadAttributeValue())
            {
                type = reader.Value;
                Program.form1.addText("IQ type: " + reader.Value);
                if (type.Equals("get"))
                {
                    reader.Read();
                    Program.form1.addText("IQ subtype: " + reader.Name);
                    if (reader.Name.Equals("query"))
                    {
                        reader.MoveToAttribute("xmlns");
                        if(reader.Value.Equals("jabber:iq:roster")){
                            Program.form1.addText("getting roster");
                            string response = "<iq id='" + stream_id + "' to='" +activeUser.username + "@" + Program.hostName + "/" + activeUser.resource + "' type='result'>" +
                                "<query xmlns='jabber:iq:roster'>";
                            List<User> userlist = activeUser.getRoster();
                            foreach (User u in userlist)
                            {
                                response = response + "<item jid='" + u.username + "@" + Program.hostName + "'/>";
                            }
                            response = response + "</query></iq>";
                            //Program.form1.addText("response: " + response);
                            Program.ha.sendMessage(activeUser.workSocket, response);
                        }
                    }
                }
                else if (type.Equals("set"))
                {                    
                    reader.Read();
                    Program.form1.addText("IQ type: " + reader.Name);
                    if(reader.Name.Equals("bind")){
                        reader.Read();
                        Program.form1.addText("IQ type: " + reader.Name);
                        if (reader.Name.Equals("resource"))
                        {
                            reader.Read();
                            Program.form1.addText("binding resource: " + reader.Value);                            
                            activeUser.resource = reader.Value;
                            string response = "<iq id='" + stream_id + "' type='result'>" +
                                "<bind xmlns='urn:ietf:params:xml:ns:xmpp-bind'>" +
                                "<jid>" + activeUser.username + "@" + Program.hostName + "/" + activeUser.resource + "</jid>" +
                                "</bind>" +
                                "</iq>";
                            Program.ha.sendMessage(activeUser.workSocket, response);
                        }
                    }
                    else if (reader.Name.Equals("session"))
                    {
                        string response = "<iq id='" + stream_id + "' type='result'/>";
                        Program.ha.sendMessage(activeUser.workSocket, response);

                    }
                }
                else if (type.Equals("result"))
                {

                }
                else if (type.Equals("error"))
                {

                }
                else
                {
                    Program.form1.addText("Error, unsupported or nil IQ Type: " + reader.Value);
                }
            }

        }
        public static void handleAuthMessage(User activeUser, XmlReader reader)
        {
            //read with mechanism the client picked
            if (activeUser.isAuthed)
            {
                Program.form1.addText("Already Authed");
                return;
            }
            reader.MoveToAttribute("mechanism");
            if (reader.ReadAttributeValue())
            {
                Program.form1.addText("Auth type: " + reader.Value);
                if (reader.Value.Equals("PLAIN"))
                {
                    activeUser.auth_type = (int)auth_type.PLAIN;
                }
                else if (reader.Value.Equals("MD5-DIGEST"))
                {
                    activeUser.auth_type = (int)auth_type.MD5_DIGEST;
                }
                else
                {
                    Program.form1.addText("Error, unsupported or nil Auth Type: " + reader.Value);
                }
            }

            //go on to the next value
            reader.Read();

            //String auth = Encoding.ASCII.GetString(Convert.FromBase64String(reader.Value));
            byte[] decbuff = Convert.FromBase64String(reader.Value);
            string result = System.Text.Encoding.UTF8.GetString(decbuff);
            string[] stringArray = result.Split('\0');
            Program.form1.addText("Auth String: " + reader.Value);
            for (int i = 0; i < stringArray.Length; i++)
            {
                Program.form1.addText(i + ": " + stringArray[i]);
            }
            
           
            activeUser.username = stringArray[1];
            activeUser.password = stringArray[2];
            activeUser.id = "asdf"; //debug stream ID

            Program.form1.addText("Sending Success");
            Program.ha.sendMessage(activeUser.workSocket, "<success xmlns='urn:ietf:params:xml:ns:xmpp-sasl'/>");
            activeUser.isAuthed = true;
           // Program.ha.sendMessage(activeUser.workSocket, server_authok);
            //activeUser.workSocket.Close();
            //Program.ha.sendMessage(activeUser.workSocket, create_response("123") + server_features);
        }

        public static void handleStream(User activeUser)
        {
            if (activeUser.isAuthed)
            {
                Program.ha.sendMessage(activeUser.workSocket, create_response("wqerqwerwer") + server_features);
            }
            else
            {
                Program.ha.sendMessage(activeUser.workSocket, server_xml);
                Program.ha.sendMessage(activeUser.workSocket, server_response);
                Program.ha.sendMessage(activeUser.workSocket, server_starttls);
            }
        }
    }
}
