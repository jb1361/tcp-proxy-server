using System;
using System.Collections.Generic;
using System.Text;

namespace TcpProxyServer
{
    static class Start
    {
        static void Main(string[] args)
        {
            Proxy prox = new Proxy("127.0.0.2", "127.0.0.1", 1340);
        }
    }
}
