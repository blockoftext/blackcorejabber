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
        public static string[] chatTypes = { "normal", "chat", "groupchat", "headline", "error" };
        public static string[] chatStates = { "starting", "active", "composing", "paused", "inactive", "gone" };
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
            //message will be padded with nulls, so split the string to get the content
            string messageString = Encoding.ASCII.GetString(message);
            string[] stringArray =  messageString.Split('\0');
            string splitMessage = stringArray[0];
            splitMessage = splitMessage + " ";

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

                    //while the reader has content left
                    while (reader.Read())
                    {  
                        //new stream request 
                        if (reader.Name.Equals("stream:stream"))
                        {
                            Program.form1.log("stream", activeUser.username, 1);
                            handleStream(activeUser);
                            break;
                        }
                        //authentication request
                        else if (reader.Name.Equals("auth") && reader.NodeType == XmlNodeType.Element)
                        {
                            Program.form1.log("auth", activeUser.username, 1);
                            handleAuthMessage(activeUser, reader);     
                            break;
                        }
                        //incoming IQ
                        else if (reader.Name.Equals("iq") && reader.NodeType == XmlNodeType.Element)
                        {
                            Program.form1.log("IQ", activeUser.username, 1);
                            handleIQ(activeUser, reader);
                            break;
                        }
                        //presence
                        else if (reader.Name.Equals("presence") && reader.NodeType == XmlNodeType.Element)
                        {
                            Program.form1.log("presence", activeUser.username, 1);
                            break;
                        }
                        //message
                        else if (reader.Name.Equals("message") && reader.NodeType == XmlNodeType.Element)
                        {
                            Program.form1.log("message", activeUser.username, 1);
                            handleIncomingMessage(activeUser, reader);
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
        public static void handleIncomingMessage(User activeUser, XmlReader reader)
        {
            Dictionary<string, string> messageDict = new Dictionary<string, string>();
            string lastNodeName = "";
            try
            {
                do
                {
                   // Program.form1.log(reader.NodeType + ":" + reader.Name + ":" + reader.Value, null, 1);
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        foreach (string s in chatStates)
                        {
                            if (s.Equals(reader.Name))
                            {
                                messageDict.Add("state", reader.Name);
                                break;
                            }
                        }
                        lastNodeName = reader.Name;
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        foreach (string s in chatStates)
                        {
                            if (s.Equals(reader.Name))
                            {
                                messageDict.Add("state", reader.Name);
                                break;
                            }
                        }
                        continue;
                    }
                    else if (reader.NodeType == XmlNodeType.Text)
                    {
                       // Program.form1.log("NodeName" + ":" + lastNodeName + ":" + reader.Value, null, 1);
                        //both the attribute and node id exist
                        if(lastNodeName.Equals("id")){
                            lastNodeName = "threadID";
                        }
                        messageDict.Add(lastNodeName, reader.Value);
                    }
                    
                    while (reader.MoveToNextAttribute())
                    {
                       // Program.form1.log(reader.NodeType + ":" + reader.Name + ":" + reader.Value, null, 1);
                        if (reader.Name.Equals("xmlns"))
                        {
                            continue;
                        }
                        messageDict.Add(reader.Name, reader.Value);
                    }
                } while (reader.Read());

            }
            catch (XmlException e)
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(reader.Name + " " + reader.Value);
                Console.WriteLine(e);
            }

            activeUser.recieveMessage(messageDict);

        }
        public static void handleIQ(User activeUser, XmlReader reader)
        {
           
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
                Program.form1.log("IQ type: " + reader.Value, activeUser.username, 1);
               
                if (type.Equals("get"))
                {
                    reader.Read();
                    Program.form1.log("IQ subtype: " + reader.Name, activeUser.username, 1);
                    
                    if (reader.Name.Equals("query"))
                    {
                        reader.MoveToAttribute("xmlns");
                        if(reader.Value.Equals("jabber:iq:roster")){
                            Program.form1.log("Roster Request", activeUser.username, 1);
                            string response = "<iq id='" + stream_id + "' to='" +activeUser.username + "@" + Program.hostName + "/" + activeUser.resource + "' type='result'>" +
                                "<query xmlns='jabber:iq:roster'>";
                            List<User> userlist = activeUser.getRoster();
                            foreach (User u in userlist)
                            {
                                response = response + "<item jid='" + u.username + "@" + Program.hostName + "'/>";
                            }
                            response = response + "</query></iq>";                            
                            activeUser.sendMessage(response);
                        }
                    }
                }
                else if (type.Equals("set"))
                {                    
                    reader.Read();
                    Program.form1.log("IQ subtype: " + reader.Name, activeUser.username, 1);
                    if(reader.Name.Equals("bind")){
                        reader.Read();
                        Program.form1.log("IQ subtype: " + reader.Name, activeUser.username, 1);
                        if (reader.Name.Equals("resource"))
                        {
                            reader.Read();
                            Program.form1.log("binding resource: " + reader.Value, activeUser.username, 1);                                                     
                            activeUser.resource = reader.Value;
                            string response = "<iq id='" + stream_id + "' type='result'>" +
                                "<bind xmlns='urn:ietf:params:xml:ns:xmpp-bind'>" +
                                "<jid>" + activeUser.username + "@" + Program.hostName + "/" + activeUser.resource + "</jid>" +
                                "</bind>" +
                                "</iq>";
                            activeUser.sendMessage(response);
                        }
                    }
                    else if (reader.Name.Equals("session"))
                    {
                        string response = "<iq id='" + stream_id + "' type='result'/>";
                        activeUser.sendMessage(response);

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

            activeUser.id = "asdf"; 

            Program.form1.addText("Sending Success");
            activeUser.sendMessage("<success xmlns='urn:ietf:params:xml:ns:xmpp-sasl'/>");
            
            activeUser.isAuthed = true;
            Program.userList.Add(activeUser);
            Program.form1.updateUserList(Program.getConnectedUserNames());
        }

        public static void handleStream(User activeUser)
        {
            if (activeUser.isAuthed)
            {
                activeUser.sendMessage(create_response("wqerqwerwer") + server_features);
               
            }
            else
            {
                activeUser.sendMessage(server_xml);
                activeUser.sendMessage(server_response);
                activeUser.sendMessage(server_starttls);            
            }
        }
    }
}
