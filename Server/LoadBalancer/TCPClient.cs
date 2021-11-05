using System;
using System.IO;    // Cho Stream
using System.Net.Sockets;   // Cho TcpClient

namespace LoadBalancer
{
    public class TCPClient
    {
        private TcpClient tcpclnt;
        private Stream stm;
        public int LastReceived = 0;
        public byte[] byteReceive;
        private string IPofServer;
        private int port;

        public TCPClient(string ip, int p)
        {
            IPofServer = ip;
            port = p;
            tcpclnt = new TcpClient();
            byteReceive = new byte[100];
            ConnectToServer();
        }

        private void ConnectToServer()
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

        public void SendBytes(byte[] data, int n)
        {
            try
            {
                stm.Write(data, 0, n);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error ... " + e.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="useful">false if no use of received data</param>
        /// <returns></returns>
        public byte[] ReceiveBytes(bool useful = true)
        {
            try
            {
                if (useful)
                {
                    LastReceived = stm.Read(byteReceive, 0, 100);
                    return byteReceive;
                }
                else stm.Read(new byte[100], 0, 100);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error ... " + e.StackTrace);
            }
            return null;
        }

        public void CloseConnection()
        {
            tcpclnt.Close();
        }

        public bool isStillConnect()
        {
            return tcpclnt.Connected;
        }
    }
}
