using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  // Cho ASCIIEncoding
using System.Threading.Tasks;
using System.IO;    // Cho Stream
using System.Net.Sockets;   // Cho TcpClient

namespace TienLen_Client
{
    public class TCPClient
    {
        private TcpClient tcpclnt;
        private Stream stm;
        private byte[] byteSend;
        private byte[] byteReceive;
        private string IPofServer;
        private int port;
        
        public TCPClient(string ip, int p)
        {
            IPofServer = ip;
            port = p;
            tcpclnt = new TcpClient();
            byteReceive = new byte[100];
        }

        public void ConnectToServer()
        {
            try
            {
                tcpclnt.Connect(IPofServer, port);
                stm = tcpclnt.GetStream();
                Console.WriteLine("Connecting to server successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error ... " + e.StackTrace);
            }

        }

        public void SendData(string str)
        {
            try
            {
                UTF8Encoding asen = new UTF8Encoding();
                byteSend = asen.GetBytes(str);
                stm.Write(byteSend, 0, byteSend.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error ... " + e.StackTrace);
            }
        }

        public string ReadData()
        {
            string str = "";
            try
            {
                int k = stm.Read(byteReceive, 0, 100);
                Console.WriteLine("Length of data received " + k.ToString());
                Console.WriteLine("From server: ");
                char[] c = new char[k];
                for (int i = 0; i < k; i++)
                {
                    c[i] = Convert.ToChar(byteReceive[i]);
                }
                str = new string(c);
                Console.WriteLine(str);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error ... " + e.StackTrace);
            }
            return str;
        }

        public void CloseConnection()
        {
            tcpclnt.Close();
        }
    }
}
