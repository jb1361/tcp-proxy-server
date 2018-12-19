using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpProxyServer
{
    class Game2Proxy
    {
        public string host;
        public int port;
        public Proxy2Server Proxy;
        private TcpClient client;
        private TcpListener listener;
        public Game2Proxy(string host, int port)
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
                SendData2Proxy(bytes);
            }
        }
        public void SendData2Proxy(Byte[] bytes)
        {
            Proxy.SendData2Server(bytes);
        }
        public void SendData2Game(Byte[] bytes)
        {
            client.GetStream().Write(bytes, 0, bytes.Length);
        }
    }
}
