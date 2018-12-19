using System;

namespace TcpProxyServer
{
    class Proxy
    {
        public Proxy(string from_host, string to_host, int port) {
            Console.WriteLine("Proxy(" + port + "): Creating Proxy");
            Game2Proxy g2p = new Game2Proxy(from_host, port);
            //Proxy2Server p2s = new Proxy2Server(to_host, port);         
            //g2p.Proxy = p2s;
            //p2s.Game = g2p;

            g2p.StartListener();
           // p2s.StartListener();
        }
    }
}
