using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.run();
        }
        public void run()
        {
            TcpClient client = new TcpClient();
            client.Connect("127.0.0.1", 7881);
            Stream strm = client.GetStream();
            ASCIIEncoding asen = new ASCIIEncoding();
            byte[] buff = asen.GetBytes("testing");
            strm.Write(buff, 0, buff.Length);
            strm.Flush();
            strm.Close();
        }
    }
}
