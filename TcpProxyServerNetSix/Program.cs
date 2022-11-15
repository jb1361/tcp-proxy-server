
namespace TcpProxyServerNetSix
{
    static class Start
    {
        public static async Task Main(string[] args)
        {
            Proxy proxyServer = new Proxy();
            await proxyServer.StartServer();
        }
    }
}