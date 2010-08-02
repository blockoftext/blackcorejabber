using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;
using System.IO;

namespace BlackCoreJabber
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

       /* public static string server_response = "<stream:stream " +
            "from='192.168.1.100' " +
            "id='asdf' " +
            "xmlns='jabber:client' " +
            "xmlns:stream='http://etherx.jabber.org/streams' " +
            "version='1.0'>";*/

       /* public static string server_features = "<stream:features>" +
            "<bind xmlns='urn:ietf:params:xml:ns:xmpp-bind'/>" +
            "<session xmlns='urn:ietf:params:xml:ns:xmpp-session'/>" +
            "</stream:features>";*/
            
       /* public static string server_starttls = "<stream:features>" +*/
          /*  "<starttls xmlns='urn:ietf:params:xml:ns:xmpp-tls'>" +
            "<required/>" +
            "</starttls>" +*/
          /*  "<mechanisms xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>" +           
            "<mechanism>PLAIN</mechanism>" +
            "<mechanism>MD5-DIGEST</mechanism>" +*/
            //"</required>"+
          /*  "</mechanisms>" +
            "</stream:features>";*/
        public static string server_authok = "<success xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>";

       /* public static string create_response(string stream_id)
        {
           return "<stream:stream " +
            "from='192.168.1.100' " +
            "id='" + stream_id + "' " +
            "xmlns='jabber:client' " +
            "xmlns:stream='http://etherx.jabber.org/streams' " +
            "version='1.0'>";
        }*/

        public static void handleIncomingMessage(Resource activeResource, byte[] message)
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
                            Program.mainWindow.log("stream", null, 1);
                            handleStream(activeResource);
                            break;
                        }
                        //authentication request
                        else if (reader.Name.Equals("auth") && reader.NodeType == XmlNodeType.Element)
                        {
                            Program.mainWindow.log("auth", null, 1);
                            handleAuthMessage(activeResource, reader);     
                            break;
                        }
                        //incoming IQ
                        else if (reader.Name.Equals("iq") && reader.NodeType == XmlNodeType.Element)
                        {
                            Program.mainWindow.log("IQ", activeResource.parentUser.username, 1);
                            handleIQ(activeResource, reader);
                            break;
                        }
                        //presence
                        else if (reader.Name.Equals("presence") && reader.NodeType == XmlNodeType.Element)
                        {
                            Program.mainWindow.log("presence", activeResource.parentUser.username, 1);
                            break;
                        }
                        //message
                        else if (reader.Name.Equals("message") && reader.NodeType == XmlNodeType.Element)
                        {
                            Program.mainWindow.log("message", activeResource.parentUser.username, 1);
                            handleIncomingChatMessage(activeResource, reader);
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
        public static void handleIncomingChatMessage(Resource activeResource, XmlReader reader)
        {
            Dictionary<string, string> messageDict = new Dictionary<string, string>();
            string lastNodeName = "";
            try
            {
                do
                {
                   // Program.mainWindow.log(reader.NodeType + ":" + reader.Name + ":" + reader.Value, null, 1);
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
                       // Program.mainWindow.log("NodeName" + ":" + lastNodeName + ":" + reader.Value, null, 1);
                        //both the attribute and node id exist
                        if(lastNodeName.Equals("id")){
                            lastNodeName = "threadID";
                        }
                        messageDict.Add(lastNodeName, reader.Value);
                    }
                    
                    while (reader.MoveToNextAttribute())
                    {
                       // Program.mainWindow.log(reader.NodeType + ":" + reader.Name + ":" + reader.Value, null, 1);
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

            activeResource.parentUser.recieveMessage(messageDict);

        }
        public static void handleIQ(Resource activeResource, XmlReader reader)
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
                Program.mainWindow.log("IQ type: " + reader.Value, activeResource.parentUser.username, 1);
               
                if (type.Equals("get"))
                {
                    reader.Read();
                    Program.mainWindow.log("IQ subtype: " + reader.Name, activeResource.parentUser.username, 1);
                    
                    if (reader.Name.Equals("query"))
                    {
                        reader.MoveToAttribute("xmlns");
                        if(reader.Value.Equals("jabber:iq:roster")){
                            Program.mainWindow.log("Roster Request", activeResource.parentUser.username, 1);
                            
                           /* string response = "<iq id='" + stream_id + "' to='" + activeResource.parentUser.username + "@" + Program.hostName + "/" + activeResource.name + "' type='result'>" +
                                "<query xmlns='jabber:iq:roster'>";
                            List<User> userlist = activeResource.parentUser.getRoster();
                            foreach (User u in userlist)
                            {
                                response = response + "<item jid='" + u.username + "@" + Program.hostName + "'/>";
                            }
                            response = response + "</query></iq>";  */ 
                         
                            activeResource.sendMessage(XMPPStanza.getRoster(stream_id, activeResource.getFullJID()));
                        }
                    }
                }
                else if (type.Equals("set"))
                {                    
                    reader.Read();
                    Program.mainWindow.log("IQ subtype: " + reader.Name, activeResource.parentUser.username, 1);
                    if(reader.Name.Equals("bind")){
                        reader.Read();
                        Program.mainWindow.log("IQ subtype: " + reader.Name, activeResource.parentUser.username, 1);
                        if (reader.Name.Equals("resource"))
                        {
                            reader.Read();
                            Program.mainWindow.log("binding resource: " + reader.Value, activeResource.parentUser.username, 1);                                                     
                            activeResource.name = reader.Value;
                          /*  string response = "<iq id='" + stream_id + "' type='result'>" +
                                "<bind xmlns='urn:ietf:params:xml:ns:xmpp-bind'>" +
                                "<jid>" + activeResource.parentUser.username + "@" + Program.hostName + "/" + activeResource.name + "</jid>" +
                                "</bind>" +
                                "</iq>";*/
                            activeResource.sendMessage(XMPPStanza.getBindResponse(stream_id, activeResource.getFullJID()));
                        }
                    }
                    else if (reader.Name.Equals("session"))
                    {
                        string response = "<iq id='" + stream_id + "' type='result'/>";
                        activeResource.sendMessage(response);

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
                    Program.mainWindow.addText("Error, unsupported or nil IQ Type: " + reader.Value);
                }
            }

        }
        public static void handleAuthMessage(Resource activeResource, XmlReader reader)
        {
            //read with mechanism the client picked
            if (activeResource.isAuthed)
            {
                Program.mainWindow.addText("Already Authed");
                return;
            }
            reader.MoveToAttribute("mechanism");
            if (reader.ReadAttributeValue())
            {
                Program.mainWindow.addText("Auth type: " + reader.Value);
                if (reader.Value.Equals("PLAIN"))
                {
                    activeResource.auth_type = (int)auth_type.PLAIN;
                }
                else if (reader.Value.Equals("MD5-DIGEST"))
                {
                    activeResource.auth_type = (int)auth_type.MD5_DIGEST;
                }
                else
                {
                    Program.mainWindow.addText("Error, unsupported or nil Auth Type: " + reader.Value);
                }
            }

            //go on to the next value
            reader.Read();
        
            byte[] decbuff = Convert.FromBase64String(reader.Value);
            string result = System.Text.Encoding.UTF8.GetString(decbuff);
            string[] stringArray = result.Split('\0');

            User activeUser = User.getUserByUsername(stringArray[1]);
            
            //activeResource.username = stringArray[1];
          //  activeUser.password = stringArray[2];
           // activeResource.getPasswordFromDatabase();

            string hashstring = Program.CalculateMD5Hash(stringArray[2]);
            if (hashstring.Equals(activeUser.password))
            {
                Program.mainWindow.log("Hash is equal", activeUser.username, 3);
            }
            else
            {
                Program.mainWindow.log("Hash is not equal, " + hashstring + "|" + activeUser.password, activeUser.username, 3);
            }


            activeResource.streamid = "asdf"; 

            Program.mainWindow.addText("Sending Success");
            activeResource.sendMessage("<success xmlns='urn:ietf:params:xml:ns:xmpp-sasl'/>");
            
            activeResource.isAuthed = true;

            activeUser.isConnected = true;
            activeUser.addActiveResource(activeResource);
            activeResource.parentUser = activeUser;
            Program.activeResources.Add(activeResource);
            Program.activeUsers.Add(activeUser);

            Program.mainWindow.updateUserList(Program.getConnectedUserNames());
        }

        public static void handleStream(Resource activeResource)
        {
            if (activeResource.isAuthed)
            {
                activeResource.sendMessage(XMPPStanza.getServerResponse("qwer") + XMPPStanza.getServerFeatures());
               
            }
            else
            {
                activeResource.sendMessage(server_xml);
                activeResource.sendMessage(XMPPStanza.getServerResponse("asdf"));
                activeResource.sendMessage(XMPPStanza.getServerStartTLS(new string[] {"PLAIN", "MD5HASH"}, false));            
            }
        }
    }
}
