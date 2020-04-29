using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ChatAppServer
{
    class Server
    {
        static void Main(string[] args)
        {
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.5"), 8080);

            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(endpoint);

            serverSocket.Listen(10);

            Console.WriteLine("Server started successfully!");
            Console.WriteLine("---Expecting a client---");

            Socket cliSocket = serverSocket.Accept();

            IPEndPoint cliEndpoint = (IPEndPoint)cliSocket.RemoteEndPoint;

            Console.WriteLine("A client with IP >>{0}<< connected successfully.", cliEndpoint.Address);

            string serverGreeting = "Successfully connected to the server. Say H-E-L-L-O!";

            byte[] clientMessage = Encoding.UTF8.GetBytes(serverGreeting);

            cliSocket.Send(clientMessage, clientMessage.Length, SocketFlags.None);

            while (true)
            {
                int clientReceiver = cliSocket.Receive(clientMessage);

                   if (clientReceiver == 0)

                        break;
              
                Console.WriteLine("Client: " + Encoding.UTF8.GetString(clientMessage, 0, clientReceiver));

                string serverMessage = Console.ReadLine();

                Console.SetCursorPosition(0, Console.CursorTop - 1);

                Console.WriteLine("Server(You): " + serverMessage);

                cliSocket.Send(Encoding.UTF8.GetBytes(serverMessage));
            } 

            Console.WriteLine("Client {0} has disconnected.", cliEndpoint.Address);

            cliSocket.Close();

            serverSocket.Close();
        }
    }
}
