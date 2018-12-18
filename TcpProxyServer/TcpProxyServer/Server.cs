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
    class Server
    {
        public void Proxy2Server(IPAddress ip, int port)
        {           
            TcpListener listener = new TcpListener(ip, port);
            listener.Start();
            Byte[] bytes = new Byte[4096];
            String data = null;
            // Enter the listening loop.
            while (true)
            {                           
                Socket packet = listener.AcceptSocket();
                data = packet.Available.ToString();
                Console.WriteLine(data);
            }
        }
    }
}
