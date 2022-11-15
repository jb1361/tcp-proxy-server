using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpProxyServerNetSix
{
    public class Proxy
    {
        public Proxy() { }

        public async Task StartServer()
        {
            const string fromHost = "0.0.0.0";
            const string toHost = "127.0.0.1";
            const int port = 1340;
            
            ClientToProxy clientToProxy = new ClientToProxy();
            ProxyToServer proxyToServer = new ProxyToServer();
            clientToProxy.host = IPAddress.Parse(fromHost);
            clientToProxy.port = port;
            proxyToServer.host = IPAddress.Parse(toHost);
            proxyToServer.port = port;
            clientToProxy.Proxy = proxyToServer;
            proxyToServer.Proxy = clientToProxy;
            await clientToProxy.ClientToProxyStart();
            Thread clientReceiveThread = new Thread(clientToProxy.ReceiveClientData);
            Thread serverReceiveThread = new Thread(proxyToServer.ReceiveServerData);
            clientReceiveThread.Start();
            serverReceiveThread.Start();
        }
    }

    public class ClientToProxy
    {
        public IPAddress host;
        public int port;
        public Socket Client;
        public ProxyToServer Proxy;
        public async Task ClientToProxyStart()
        {
            var localEndPoint = new IPEndPoint(host, port);
            var sock = new Socket(AddressFamily.InterNetwork,
            SocketType.Stream, ProtocolType.Tcp);
            sock.Bind(localEndPoint);
            sock.Listen(1);
            Console.WriteLine($"Waiting for client on port: {port}");   
            Client = await sock.AcceptAsync();
            await Task.Run(() => Console.WriteLine("Client Connected..."));
            await Proxy.Connect();
        }

        public void ReceiveClientData()
        {
            var bytes = new Byte[1024];
            while (true)
            {
                Client.ReceiveAsync(bytes, SocketFlags.None);
                if (bytes.Any(b => b > 0))
                {
                    var logBytes = bytes.Where(b => b > 0).ToArray();
                    Console.WriteLine("Client: " + Encoding.ASCII.GetString(logBytes, 0, logBytes.Length).Trim());
                    Proxy.SendDataToServer(bytes);
                    bytes = new Byte[1024];
                }
            }
        }

        public void SendDataToClient(Byte[] bytes)
        {
            Client.Send(bytes, SocketFlags.None);
        }
        
    }

    public class ProxyToServer
    {
        public IPAddress host;
        public int port;
        public ClientToProxy Proxy;
        public Socket Server;

        public void ReceiveServerData()
        {
            var bytes = new Byte[1024];
            while (true)
            {
                Server.ReceiveAsync(bytes, SocketFlags.None);
                if (bytes.Any(b => b > 0))
                {
                    var logBytes = bytes.Where(b => b > 0).ToArray();
                    Console.WriteLine("Server: " + Encoding.ASCII.GetString(logBytes, 0, logBytes.Length).Trim());
                    Proxy.SendDataToClient(bytes);
                    bytes = new Byte[1024];
                }
            }
        }

        public void SendDataToServer(Byte[] bytes)
        {
            Server.Send(bytes, SocketFlags.None);
        }
        
        public async Task Connect()
        {
            await Task.Run(() => Console.WriteLine($"Connecting Proxy to Server."));
            Server = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            await Server.ConnectAsync(new IPEndPoint(host, port));
            await Task.Run(() => Console.WriteLine($"Proxy connected to server on port: {port}"));
        }
    }
}
