using System;
using System.Text;
using System.Threading;

namespace LoadBalancer
{
    class Client
    {
        private SocketModel incoming;
        private TCPClient outcoming, outcoming1, outcoming2;
        private byte[] buf;
        public bool isStillConnect => incoming.isStillConnect();

        public Client(SocketModel Incoming, TCPClient Outcoming1, TCPClient Outcoming2)
        {
            this.incoming = Incoming;
            this.outcoming1 = Outcoming1;
            this.outcoming = Outcoming1;
            this.outcoming2 = Outcoming2;
        }

        public void BeginForwardToClient()
        {
            Thread forward1 = new Thread(ForwardFromSev1ToClient);
            forward1.Start();
            Thread forward2 = new Thread(ForwardFromSev2ToClient);
            forward2.Start();
        }

        public void ChooseServer(int server)
        {
            outcoming = server == 1 ? outcoming1 : outcoming2;
        }

        public void ForwardToServer(bool usedefaulfserver = true, byte[] buf = null)
        {
            if (usedefaulfserver)
                outcoming.SendBytes(this.buf, incoming.LastReceived);
            else
            {
                if (outcoming != outcoming1) outcoming1.SendBytes(buf, incoming.LastReceived);
                else outcoming2.SendBytes(buf, incoming.LastReceived);
            }
        }

        private void ForwardFromSev1ToClient()
        {
            while (incoming.isStillConnect())
            {
                try
                {
                    incoming.SendBytes(outcoming1.ReceiveBytes(), outcoming1.LastReceived);
                }
                catch (Exception ex)
                {
                    string str = "ERROR: " + ex.Message;
                    byte[] data = Encoding.ASCII.GetBytes(str);
                    Console.WriteLine(str);
                    incoming.SendBytes(data, data.Length);
                }
            }
        }

        private void ForwardFromSev2ToClient()
        {
            while (incoming.isStillConnect())
            {
                try
                {
                    incoming.SendBytes(outcoming2.ReceiveBytes(), outcoming2.LastReceived);
                }
                catch (Exception ex)
                {
                    string str = "ERROR: " + ex.Message;
                    byte[] data = Encoding.ASCII.GetBytes(str);
                    Console.WriteLine(str);
                    incoming.SendBytes(data, data.Length);
                }
            }
        }
        public void SendResultToClient(byte[] data = null)
        {
            if (data == null) incoming.SendBytes(ReceiveBytesFromServer(), outcoming.LastReceived);
            else incoming.SendBytes(data, outcoming.LastReceived);
        }

        public byte[] ReceiveBytesFromClient()
        {
            buf = incoming.ReceiveBytes();
            return buf;
        }

        public byte[] ReceiveBytesFromServer()
        {
            buf = outcoming.ReceiveBytes();
            return buf;
        }

        /// <summary>
        /// gửi request đến 2 server để đồng bộ sẽ nhận về 2 response. chỉ quan tâm đến 1 kết quả trả về
        /// </summary>
        public void BoQuaDuLieuTrung()
        {
            outcoming2.ReceiveBytes(false);
        }
    }
}
