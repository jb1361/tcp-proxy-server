using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpProxyServer
{
    class Game2Proxy
    {
        IPAddress host;
        public int port;
        public Socket Game;       
        Boolean connected = false;
        public Game2Proxy(string host, int port)
        {
            this.host = IPAddress.Parse(host);           
            this.port = port;
           // Game = new TcpClient();
            Console.WriteLine("Proxy("+ port + "): Waiting for a connection... ");           
            StartListener();
        }
     
        public void StartListener()
        {                  
            IPEndPoint localEndPoint = new IPEndPoint(this.host, this.port);
            Game = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            Game.Bind(localEndPoint);
            Game.Listen(1);
            Game.Accept();
            Console.WriteLine("Proxy(" + port + "): Connected");
            Byte[] bytes = new Byte[4096];
            while (true)
            {
                Game.Receive(bytes);               
                Console.WriteLine("Game: " + bytes);
                //SendData2Game(bytes);
            }
        }
        public void ReadData()
        {
            String data = null;
            Byte[] bytes = new Byte[4096];
            // Stream stream = Game.GetStream();
            data = null;
          //  data = stream.Read(bytes, 0, bytes.Length).ToString();
            Console.WriteLine("Game: " + data);
        }
        public void SendData2Proxy(Byte[] bytes)
        {
            //listener.AcceptSocket();
            //Proxy.SendData2Server(bytes);
        }
        public void SendData2Game(Byte[] bytes)
        {
            //Stream strm = Game.GetStream();
            //strm.Write(bytes, 0, bytes.Length);
            //strm.Flush();
            //strm.Close();
        }
    }
}
