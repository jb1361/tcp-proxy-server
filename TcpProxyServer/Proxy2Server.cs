using System;
using System.Collections.Generic;
using System.IO;
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
        public TcpClient client;
        private TcpListener listener;
        public Proxy2Server(string host, int port)
        {
            this.host = host;
            this.port = port;
            client = new TcpClient();
            client.Connect(this.host, this.port);
        }
        public void StartListener()
        {
            IPAddress ip = IPAddress.Parse(this.host);
            listener = new TcpListener(ip, this.port);
            listener.Start();
            Byte[] bytes = new Byte[4096];
            String data = null;
            // Enter the listening loop.            
            while (true)
            {
                client = listener.AcceptTcpClient();
                Stream stream = client.GetStream();
                data = null;
                data = stream.Read(bytes, 0, bytes.Length).ToString();
                Console.WriteLine("Game: " + data);
                SendData2Game(bytes);
            }
        }
        public void SendData2Server(Byte[] bytes)
        {
            NetworkStream stream = client.GetStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
            stream.Close();
        }
        public void SendData2Game(Byte[] bytes)
        {
            //Game.SendData2Game(bytes);
        }
    }
}

