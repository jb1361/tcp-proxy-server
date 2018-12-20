using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpListenerServer
{
    /// <summary>
    /// Opens a tcplistener on all ip address ranges and port 4000. It listens for any tcp data being sent through then writes it to the console.
    /// </summary>
    class Server
    {
        static void Main(string[] args)
        {
            IPAddress localAddr = IPAddress.Parse("0.0.0.0");
            TcpListener listener = new TcpListener(localAddr, 64748);
            listener.Start();
            Byte[] bytes = new Byte[256];
            String data = null;
            // Enter the listening loop.
            while (true)
            {
                Console.Write("Waiting for a connection... ");

                // Perform a blocking call to accept requests.
                // You could also user server.AcceptSocket() here.
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Connected!");

                data = null;

                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();

                int i;

                // Loop to receive all the data sent by the client.
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);                                 
                }
                stream.Close();
            }
        }
    }
}
