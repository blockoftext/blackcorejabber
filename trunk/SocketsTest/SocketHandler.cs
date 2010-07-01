using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
namespace WindowsFormsApplication1
{
    class SocketHandler
    {
        Socket listeningSocket;
        //Socket acceptedSocket;
       // Thread t;
        int port = 5222;
        public bool connect()
        {
            if (listeningSocket != null)
            {
                return false;
            }

            listeningSocket = null;
            IPHostEntry hostEntry = null;

            // Get host related information.
            hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress address in hostEntry.AddressList)
            {
               
                IPEndPoint ipe = new IPEndPoint(address, port);             
                if (ipe.AddressFamily.Equals(AddressFamily.InterNetworkV6)) //ipv6 can diaf
                {
                    continue;
                }
                Socket tempSocket = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    tempSocket.Bind(ipe);
                    tempSocket.Listen(100);
                    tempSocket.BeginAccept(new AsyncCallback(AcceptCallback), tempSocket);
                }
                catch (SocketException e)
                {
                    Program.form1.addText("Error Binding Socket: " + e.ToString());
                    Console.WriteLine("Error Binding Socket: " + e.ToString());
                }
                catch (ObjectDisposedException)
                {
                    Program.form1.addText("Error Binding Socket: Socket Disposed");
                }
      

                if (tempSocket.IsBound)
                {
                    listeningSocket = tempSocket;
                    return true;
                }
                else
                {
                    continue;
                }
            }
   
            return false;
         
        }

        public bool disconnect()
        {
            try
            {
                foreach(User user in Program.userList){
                    if (user.workSocket != null && user.workSocket.Connected)
                    {
                        user.workSocket.Close();
                        user.workSocket = null;
                    }
                }
                if (listeningSocket != null)
                {
                    if(listeningSocket.Connected)
                        listeningSocket.Shutdown(SocketShutdown.Both);                  
                    listeningSocket.Close();
                    listeningSocket = null;
                }
                return true;
            }
            catch (ObjectDisposedException)
            {
                Program.form1.addText("Error disconnecting: socket already disposed of");
                return false;
            }
            catch (SocketException e)
            {
                Program.form1.addText("Error disconnecting: " + e.ToString());
                Console.WriteLine("Error disconnecting: " + e.ToString());
                return false;
            }

        }

        public void AcceptCallback(IAsyncResult ar)
        {
            if (listeningSocket == null) return;
            try
            {
                Program.form1.addText("Connection incoming");
                Socket acceptedSocket = listeningSocket.EndAccept(ar);
                User state = new User();
                state.workSocket = acceptedSocket;
                acceptedSocket.BeginReceive(state.buffer, 0, User.BufferSize, 0, new AsyncCallback(handleMessages), state);
                listeningSocket.BeginAccept(new AsyncCallback(AcceptCallback), listeningSocket);
            }
            catch (SocketException e)
            {
                Program.form1.addText("Socket Exception on Accept");
                Console.WriteLine("Socket Exception on Accept: " + e.ToString());
            }
            catch (ObjectDisposedException)
            {
                Program.form1.addText("Error, Socket Closed");
            }

        }
        public void sendMessage(Socket reciever, string message)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(message);
            try
            {
               reciever.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), reciever);
            }
            catch (SocketException e)
            {
                if (e.SocketErrorCode.ToString().Equals("ConnectionReset"))
                {
                    Program.form1.addText("Connection Reset by Host");   
                }
                else
                {
                    Program.form1.addText("Socket Exception on Send: " + e.ToString());
                    Program.form1.addText("Error Code " + e.SocketErrorCode);
                    Console.WriteLine("Socket Exception on Send: " + e.ToString());
                }
            }
            catch (ObjectDisposedException)
            {
                Program.form1.addText("Error, Socket Closed");
            }

        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                Program.form1.addText("Bytes sent to client: " + bytesSent);  

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void handleMessages(IAsyncResult ar)
        {
            
            String content = String.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            User state = (User)ar.AsyncState;
            Socket handler = state.workSocket;

            try
            {
                // Read data from the client socket. 
                int bytesRead = handler.EndReceive(ar);
                //Program.form1.addText("Read " + bytesRead + " bytes");
                if (bytesRead > 0)
                {
                   // Program.form1.addText("Read From Socket: " + Encoding.ASCII.GetString(state.buffer, 0, bytesRead) + "\r\n");
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    string strContent;
                    strContent = state.sb.ToString();
                  //  Program.form1.addText("Read " + strContent.Length + " bytes from socket. \r\n Data: " + strContent);
                    XMPP.handleIncomingMessage(state, state.buffer);
                    
                    handler.BeginReceive(state.buffer, 0, User.BufferSize, 0,
                                             new AsyncCallback(handleMessages), state);
                }
                else
                {
                    if (state.sb.Length > 1)
                    {
                        //All of the data has been read, so displays it to the console
                        string strContent;
                        strContent = state.sb.ToString();
                     //   Program.form1.addText("Read " + strContent.Length + " bytes from socket. \r\nData: " + strContent);
                        state.sb = new StringBuilder();
                    }
                    if (handler.Connected)
                    {
                        handler.BeginReceive(state.buffer, 0, User.BufferSize, 0,
                            new AsyncCallback(handleMessages), state);
                    }
   
                }
            }
            catch (SocketException e)
            {
                if (e.SocketErrorCode.ToString().Equals("ConnectionReset"))
                {
                    Program.form1.addText("Connection Reset by Host");

                    if (state.sb.Length > 1)
                    {
                        //connection has been reset, but there may be data in the buffer
                        string strContent;
                        strContent = state.sb.ToString();
                        Program.form1.addText("Read " + strContent.Length + " bytes from socket. \r\nData: " + strContent);

                        //remove user from user list now that the connection is done with
                        Program.userList.Remove(state); 
                    }
                }
                else
                {
                    Program.form1.addText("Socket Exception on Receive: " + e.ToString());
                    Program.form1.addText("Error Code " + e.SocketErrorCode);
                    Console.WriteLine("Socket Exception on Receive: " + e.ToString());
                }

            }
            catch (ObjectDisposedException)
            {
                Program.form1.addText("Error, Socket Closed");
            }


        }
    }
}
