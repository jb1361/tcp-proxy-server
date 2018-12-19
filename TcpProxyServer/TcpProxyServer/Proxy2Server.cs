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
        public string host;
        public int port;
        public Game2Proxy Game;
        private TcpClient client;
        private TcpListener listener;
        public Proxy2Server(string host, int port)
        {
            this.host = host;
            this.port = port;
            client = new TcpClient();            
        }
        public void StartListener()
        {
            IPAddress ip = IPAddress.Parse(host);
            listener = new TcpListener(ip, port);
            listener.Start();
            Byte[] bytes = new Byte[4096];
            String data = null;
            // Enter the listening loop.
            while (true)
            {
                client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                data = null;
                data = stream.Read(bytes, 0, bytes.Length).ToString();
                Console.WriteLine("Game: " + data);
                SendData2Server(bytes);
            }
        }
        public void SendData2Server(Byte[] bytes)
        {
            client.GetStream().Write(bytes, 0, bytes.Length);
        }
        public void SendData2Game(Byte[] bytes)
        {
            Game.SendData2Game(bytes);
        }
    }
}

