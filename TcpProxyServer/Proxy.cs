using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TcpProxyServer
{
    class Proxy
    {
        public Proxy(string from_host, string to_host, int port) {
            Console.WriteLine("Proxy(" + port + "): Creating Proxy");           
            Game2Proxy g2p = new Game2Proxy();
            Proxy2Server p2s = new Proxy2Server();
            g2p.host = IPAddress.Parse(from_host);
            g2p.port = port;
            p2s.host = IPAddress.Parse(to_host);
            p2s.port = port;
            g2p.Server = p2s;
            p2s.Game = g2p;
            Thread gameThread = new Thread(new ThreadStart(g2p.Game2ProxyStart));
            Thread proxyThread = new Thread(new ThreadStart(p2s.Proxy2ServerStart));
            gameThread.Start();
            proxyThread.Start();
        }
    }

    class Game2Proxy
    {
        public IPAddress host;
        public int port;
        public Socket Game;
        public Proxy2Server Server;
        public void Game2ProxyStart()
        {   
            Console.WriteLine("Proxy(" + port + "): Waiting for game connection... ");
            IPEndPoint localEndPoint = new IPEndPoint(this.host, this.port);
            Socket sock = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            sock.Bind(localEndPoint);
            sock.Listen(1);
            Game = sock.Accept();
            Console.WriteLine("Proxy(" + port + ") Game: Connected");
            Byte[] bytes = new Byte[4096];            
            while (true)
            {
                Game.Receive(bytes);
                Console.WriteLine("Game: " + Encoding.ASCII.GetString(bytes, 0, bytes.Length));
                Server.Server.Send(bytes);
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
    }

    class Proxy2Server
    {
        public IPAddress host;
        public int port;
        public Game2Proxy Game;
        public Socket Server;
        public void Proxy2ServerStart()
        {          
            Console.WriteLine("Proxy(" + port + "): Starting server connection... ");
            IPEndPoint remoteEndPoint = new IPEndPoint(this.host, this.port);
            Server = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            Server.Connect(remoteEndPoint);
            Console.WriteLine("Proxy(" + port + ") Server: Connected");            
            Byte[] bytes = new Byte[4096];
            while (true)
            {
                Server.Receive(bytes);
                Console.WriteLine("Server: " + Encoding.ASCII.GetString(bytes, 0, bytes.Length));
                Game.Game.Send(bytes);
            }
        }
    }
}
