using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpProxyServer
{
    /// <summary>
    /// Opens a tcplistener on all ip address ranges and port 4000. It listens for any tcp data being sent through then writes it to the console.
    /// </summary>
    class Proxy2Server
    {
        Game2Proxy game = null;
        public Proxy2Server(IPAddress ip, int port)
        {  
            TcpListener listener = new TcpListener(ip, port);
            listener.Start();
            Byte[] bytes = new Byte[4096];
            String data = null;
            // Enter the listening loop.
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                data = null;
                int i;
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);
                }
                Console.WriteLine("Server: " + data);               
            }
        }

        public void ForwardData()
        {

        }
    }
}
