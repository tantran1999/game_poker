using System;
using System.Net.Sockets;


namespace LoadBalancer
{
    /// <summary>
    /// Description of SocketModel.
    /// </summary>
    [Serializable]
    public class SocketModel
    {
        private Socket socket;
        public byte[] array_to_receive_data;
        public int LastReceived;
        public string RemoteEndPoint;
        
        public SocketModel(Socket s)
        {
            socket = s;
            socket.NoDelay = true;
            array_to_receive_data = new byte[100];
            RemoteEndPoint = socket.RemoteEndPoint.ToString();
        }

        public byte[] ReceiveBytes()
        {
            try
            {
                LastReceived = socket.Receive(array_to_receive_data);
                return array_to_receive_data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                LastReceived = 0;
                return null;
            }
        }

        /// <summary>
        /// Send data to client
        /// </summary>
        /// <param name="str">Data for send</param>
        public void SendBytes(byte[] data, int length)
        {
            try
            {
                if (socket.Connected)
                {
                    socket.Send(data,length,SocketFlags.None);
                    
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
