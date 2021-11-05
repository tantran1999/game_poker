using System;
using System.Net.Sockets;
using System.Text;


namespace TienLen_Server
{
    /// <summary>
    /// Description of SocketModel.
    /// </summary>
    [Serializable]
    public class SocketModel
    {
        private Socket socket;
        private byte[] array_to_receive_data;
        public string RemoteEndPoint;

        public SocketModel(Socket s)
        {
            socket = s;
            socket.NoDelay = true;
            array_to_receive_data = new byte[100];
            RemoteEndPoint = socket.RemoteEndPoint.ToString();
        }

        /// <summary>
        /// Receive data from client
        /// </summary>
        /// <returns>Data received</returns>
        public string ReceiveData()
        {
            //server just can receive data AFTER a connection is set up between server and client
            string str = "";
            try
            {
                //count the length of data received (maximum is 100 bytes)
                int k = socket.Receive(array_to_receive_data);
                if (k == 0 || !socket.Connected) socket.Close(); //k==0 when disconnected
                else
                {
                    Console.Write("From client " + RemoteEndPoint + ": ");
                    //convert the byte recevied into string
                    char[] c = new char[k];
                    for (int i = 0; i < k; i++)
                    {
                        c[i] = Convert.ToChar(array_to_receive_data[i]);
                    }
                    str = new string(c);
                    Console.WriteLine(str);
                }
            }
            catch (Exception e)
            {
                if (socket != null) socket.Close();
                Console.WriteLine(e.Message);
            }
            return str;
        }

        /// <summary>
        /// Send data to client
        /// </summary>
        /// <param name="str">Data for send</param>
        public void SendData(string str)
        {
            try
            {
                if (socket.Connected)
                {
                    ASCIIEncoding asen = new ASCIIEncoding();
                    socket.Send(asen.GetBytes(str));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Close Socket
        /// </summary>
        public void CloseSocket()
        {
            if (socket.Connected)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }

        public bool isStillConnect()
        {
            return socket.Connected;
        }
    }
}
