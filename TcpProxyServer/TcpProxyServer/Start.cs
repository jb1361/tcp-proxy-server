using System;
using System.Collections.Generic;
using System.Text;

namespace TcpProxyServer
{
    class Start
    {
        static void Main(string[] args)
        {
            Proxy prox = new Proxy("0.0.0.0", "64.237.39.158", 7880);
        }
    }
}
