using System;
using System.Collections.Generic;
using System.Text;

namespace TcpProxyServer
{
    class Start
    {
        static void Main(string[] args)
        {
            Proxy prox = new Proxy("0.0.0.0", "0.0.0.1", 4000);
        }
    }
}
