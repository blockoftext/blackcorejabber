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

        public static string[] chatStatus = { "chat", "away", "xa", "dnd" };
        public static string[] chatTypes = { "normal", "chat", "groupchat", "headline", "error" };
        public static string[] chatStates = { "starting", "active", "composing", "paused", "inactive", "gone" };
        public static string server_xml = "<?xml version='1.0'?>";        
        public static string server_authok = "<success xmlns='urn:ietf:params:xml:ns:xmpp-sasl'>";
        
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
         
                            handleStream(activeResource);
                            break;
                        }
                        //authentication request
                        else if (reader.Name.Equals("auth") && reader.NodeType == XmlNodeType.Element)
                        {
                     
                            handleAuthMessage(activeResource, reader);     
                            break;
                        }
                        //incoming IQ
                        else if (reader.Name.Equals("iq") && reader.NodeType == XmlNodeType.Element)
                        {
          
                            handleIQ(activeResource, reader);
                            break;
                        }
                        //presence
                        else if (reader.Name.Equals("presence") && reader.NodeType == XmlNodeType.Element)
                        {
                            Program.mainWindow.log("presence", activeResource.parentUser.username, 1);
                            handlePresence(activeResource, reader);
                            break;
                        }
                        //message
                        else if (reader.Name.Equals("message") && reader.NodeType == XmlNodeType.Element)
                        {
    
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
                //parse all message atributes into a dictionary 
                do
                {
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
                        if(lastNodeName.Equals("id"))
                        {
                            lastNodeName = "threadID";
                        }
                        messageDict.Add(lastNodeName, reader.Value);
                    }
                    
                    while (reader.MoveToNextAttribute())
                    {
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
            User activeUser = activeResource.parentUser;


            string to = "";
            string from = "";
            string type;
            string stream_id = "nil";

            reader.MoveToAttribute("id");
            if (reader.ReadAttributeValue())
            {
                stream_id = reader.Value;
            }

            reader.MoveToAttribute("to");
            if (reader.ReadAttributeValue())
            {
                to = reader.Value;
            }

            reader.MoveToAttribute("from");
            if (reader.ReadAttributeValue())
            {
                from = reader.Value;
            }

            //not authenticated so it has to be a registration event
            if (activeUser == null)
            {

                reader.MoveToAttribute("type");
                if (reader.ReadAttributeValue())
                {                  
                    if (reader.Value.Equals("get"))
                    {
                        reader.Read(); 
                        if (reader.Name.Equals("query"))
                        {
                            reader.MoveToAttribute("xmlns");
                            if (reader.Value.Equals("jabber:iq:register"))
                            {
                                Program.mainWindow.log("Registration Request", null, 1);
                                activeResource.sendMessage(XMPPStanza.getRegistryResponse(stream_id));
                            }
                        }
                    }else if(reader.Value.Equals("set"))
                    {
                        reader.Read();
                        if (reader.Name.Equals("query"))
                        {
                            reader.MoveToAttribute("xmlns");
                            if (reader.Value.Equals("jabber:iq:register"))
                            {
                                Program.mainWindow.log("Registration Submission", null, 1);
                                Dictionary<string, string> reg = new Dictionary<string, string>();
                                string key = "", value = "";
                                while (reader.Read())
                                { 
                                    if(reader.Name.Equals("field")){
                                        reader.MoveToAttribute("var");
                                        key = reader.Value;
                                        
                                    }else if( reader.Name.Equals("value")){
                                        reader.Read(); //text
                                        value = reader.Value;                                     
                                        reg.Add(key, value);
                                        reader.Read(); //value
                                        reader.Read(); //field
                                    }
                                }
                                if (User.registerUser(reg))
                                {
                                    activeResource.sendMessage("<iq type='result' id='" + stream_id + "'/>");
                                }
                            }
                        }

                    }
                }               
                return;
            }


            reader.MoveToAttribute("type");
            if (reader.ReadAttributeValue())
            {
                type = reader.Value;
            
               // GET
                if (type.Equals("get"))
                {
                    reader.Read();
                    Program.mainWindow.log("IQ subtype: " + reader.Name, activeUser.username, 1);
                    
                    if (reader.Name.Equals("query"))
                    {
                        reader.MoveToAttribute("xmlns");

                        if(reader.Value.Equals("jabber:iq:roster")){
                            Program.mainWindow.log("Roster Request", activeUser.username, 1);
                            activeResource.sendMessage(XMPPStanza.getRoster(stream_id, activeResource.getFullJID()));
                        }
                        else if (reader.Value.Equals("jabber:iq:register"))
                        {
                           //registry event should never happen with an authenticated user
                            return;
                        }
                    }
                    else if (reader.Name.Equals("vCard"))
                    {
                       
                        //getting own vcard
                        if (to.Equals(""))
                        {
                            Program.mainWindow.log("Getting Own Vcard", activeUser.username, 1);
                            string response = XMPPStanza.getNovCard(stream_id, from);
                            activeResource.sendMessage(response);
                          
                        }
                        //getting others vcard
                        else
                        {
                            Program.mainWindow.log("Getting Others Vcard: " + to, activeUser.username, 1);
                            string response = XMPPStanza.getNoOthersvCard(stream_id, activeResource.getFullJID());
                            activeResource.sendMessage(response);
                        }
                    }
                }

                // SET
                else if (type.Equals("set"))
                {                    
                    reader.Read();
                    Program.mainWindow.log("IQ subtype: " + reader.Name, activeUser.username, 1);
                    if(reader.Name.Equals("bind")){
                        reader.Read();
                        Program.mainWindow.log("IQ subtype: " + reader.Name, activeUser.username, 1);
                        if (reader.Name.Equals("resource"))
                        {
                            reader.Read();
                            Program.mainWindow.log("binding resource: " + reader.Value, activeUser.username, 1);                                                     
                            activeResource.name = reader.Value;
     
                            activeResource.sendMessage(XMPPStanza.getBindResponse(stream_id, activeResource.getFullJID()));
                        }
                    }
                    else if (reader.Name.Equals("session"))
                    {
                        string response = "<iq id='" + stream_id + "' type='result'/>";
                        activeResource.sendMessage(response);

                    }
                }

                // RESULT
                else if (type.Equals("result"))
                {

                }

                // ERROR
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
                    activeResource.sendMessage("<failure xmlns='urn:ietf:params:xml:ns:xmpp-sasl'><invalid-mechanism/></failure>");
                    activeResource.sendMessage("</stream:stream>");
                    activeResource.close();
                    Program.mainWindow.addText("Error, unsupported or nil Auth Type: " + reader.Value);
                }
            }

            //go on to the next value
            reader.Read();
        

            byte[] decbuff = Convert.FromBase64String(reader.Value);
            string result = System.Text.Encoding.UTF8.GetString(decbuff);
            string[] stringArray = result.Split('\0');       

            User activeUser = User.getUserByUsername(stringArray[1]);

            if (activeUser == null)
            {
                Program.mainWindow.log("User not found :" + stringArray[1], null, 0);
                activeResource.sendMessage("<failure xmlns='urn:ietf:params:xml:ns:xmpp-sasl'><not-authorized/></failure>");
                activeResource.sendMessage("</stream:stream>");   
                activeResource.close();
                return;
            }

            string hashstring = Program.CalculateMD5Hash(stringArray[2]);
            if (!hashstring.Equals(activeUser.password))
            {
                Program.mainWindow.log("Hash is not equal, " + hashstring + "|" + activeUser.password, activeUser.username, 3);
                activeResource.sendMessage("<failure xmlns='urn:ietf:params:xml:ns:xmpp-sasl'><not-authorized/></failure>");
                activeResource.sendMessage("</stream:stream>");   
                activeResource.close();
                return;
            }

            //send success message
            activeResource.streamid = "asdf"; 
            activeResource.sendMessage("<success xmlns='urn:ietf:params:xml:ns:xmpp-sasl'/>");            
            activeResource.isAuthed = true;

            //move user to active users
            activeUser.isConnected = true;
            activeUser.addActiveResource(activeResource);
            activeResource.parentUser = activeUser;
            Program.activeResources.Add(activeResource);
            Program.activeUsers.Add(activeUser);

            Program.mainWindow.updateUserList(Program.getConnectedUserNames());
        }

        public static void handleStream(Resource activeResource)
        {
            //if user is authorized advertise server features else send authorization response
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

        public static void handlePresence(Resource activeResource, XmlReader reader)
        {
            string element = "";
            string show = "";
            string status = "";
            int priority = -1;

            bool isUpdate = false;

            do{
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        element = reader.Name;
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
    
                    }
                    else if (reader.NodeType == XmlNodeType.Text)
                    {
                        if (element.Equals("show"))
                        {
                            isUpdate = true;
                            show = reader.Value;
                        }
                        else if (element.Equals("status"))
                        {
                            status = reader.Value;
                        }else if(element.Equals("priority"))
                        {
                            priority = int.Parse(reader.Value);
                        }
                    }                    
              
                } while (reader.Read());



            //if not a presence update, return presence for all currently connected user
            if (!isUpdate)
            {
                foreach (User u in Program.activeUsers)
                {
                    if (u.getActiveResource() != activeResource)
                    {
                        //TODO: differentiate between the first bare <presence/> and later

                        //send activeUser all presences
                        string presence = XMPPStanza.getPresenceString(u.getFullJID(), activeResource.getFullJID());
                        activeResource.sendMessage(presence);

                        //send all other users activeUser's presence
                        presence = XMPPStanza.getPresenceString(activeResource.getFullJID(), u.getFullJID());
                        u.getActiveResource().sendMessage(presence);
                    }
                }
            }
            else
            {
                // currentStatus can only be one of the 4 default values
                foreach (string s in chatStatus)
                {
                    if (s.Equals("show"))
                    {
                        activeResource.parentUser.currentStatus = show;
                        break;
                    }
                    else
                    {
                        activeResource.parentUser.currentStatus = "";
                    }
                }

                //update custom status message
                if (!status.Equals(""))
                {
                    activeResource.parentUser.currentPresence = status;
                    activeResource.parentUser.presenceTime = DateTime.Now;

                    //send each user a detailed status update
                    foreach (User u in Program.activeUsers)
                    {
                        if (u.getActiveResource() != activeResource)
                        {
                            string presence = XMPPStanza.getPresenceString(activeResource.getFullJID(), u.getFullJID(), show, status);
                            u.getActiveResource().sendMessage(presence);

                        }
                    }
                }
                else
                {
                    //send each user an update without status
                    foreach (User u in Program.activeUsers)
                    {
                        if (u.getActiveResource() != activeResource)
                        {
                            string presence = XMPPStanza.getPresenceString(activeResource.getFullJID(), u.getFullJID(), show);
                            u.getActiveResource().sendMessage(presence);

                        }
                    }
                }

            }


        }
    }
}
