using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LoadBalancer
{
    class Balancer
    {
        private List<Client> Clients = new List<Client>();
        private List<string> RoomsSV1 = new List<string>();
        private List<string> RoomsSV2 = new List<string>();
        private static IPAddress ipserver = IPAddress.Parse("192.168.43.123");
        //private static IPAddress ipserver = IPAddress.Parse("127.0.0.1");
        private TcpListener TcpListener = new TcpListener(ipserver, 8080);
        private TCPClient control1, control2;
        public void Start()
        {
            control1 = new TCPClient("127.0.0.1", 10000);
            control2 = new TCPClient("127.0.0.1", 11000);
            TcpListener.Start();
            Listen();
        }

        private void Listen()
        {
            while (true)
            {
                SocketModel socket = new SocketModel(TcpListener.AcceptSocket());
                socket.SendBytes(Encoding.ASCII.GetBytes("0"), 1);
                Thread comm1 = new Thread(communicate1);
                Thread comm2 = new Thread(communicate2);
                Thread phucvu = new Thread(PhucVuSocket);
                comm1.Start();
                comm2.Start();
                phucvu.Start(socket);
            }
        }

        private void communicate1()
        {
            while (control1.isStillConnect())
            {
                string[] data = Encoding.ASCII.GetString(control1.ReceiveBytes()).Split('|');
                if (data[0] == "0")
                {
                    RoomsSV1.Remove(data[1]);
                }
            }
        }

        private void communicate2()
        {
            while (control2.isStillConnect())
            {
                string[] data = Encoding.ASCII.GetString(control2.ReceiveBytes()).Split('|');
                if (data[0] == "0")
                {
                    RoomsSV2.Remove(data[1]);
                }
            }
        }

        private int TimPhong(Client client, byte[] data)
        {
            string[] request = Encoding.ASCII.GetString(data).Split('|');
            string TenPhong = request[1];
            if (RoomsSV1.Contains(TenPhong))
            {
                return 1; //server 1
            }
            else if (RoomsSV2.Contains(TenPhong))
                return 2; //server 2
            else
            {
                if (RoomsSV1.Count > RoomsSV2.Count)
                {
                    RoomsSV2.Add(TenPhong);
                    return 2;
                }
                else
                {
                    RoomsSV1.Add(TenPhong);
                    return 1;
                }
            }
        }

        private bool Authorize(Client client)
        {
            try
            {
                byte[] cdata = client.ReceiveBytesFromClient(); //byte[] phục vụ giao tiếp với client
                client.ForwardToServer();
                byte[] sdata = client.ReceiveBytesFromServer(); //byte[] phục vụ gửi nhận lên server
                if (sdata[0] == (byte)'0')
                {
                    cdata[0] = (byte)'1';
                    client.ForwardToServer(false, cdata);
                    client.BoQuaDuLieuTrung(); //connected server 2
                    client.BoQuaDuLieuTrung(); //login server 2
                    client.BeginForwardToClient();
                    return true;
                }
                client.SendResultToClient(sdata);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void PhucVuSocket(object sk)
        {
            try
            {
                //setup
                SocketModel socket = (SocketModel)sk;
                TCPClient client1 = new TCPClient("127.0.0.1", 10000);
                TCPClient client2 = new TCPClient("127.0.0.1", 11000);

                //create new client
                Client client;
                client = new Client(socket, client1, client2);

                //đăng ký, đăng nhập
                while (!Authorize(client) && client.isStillConnect) ;

                while (client.isStillConnect)
                {
                    byte[] recv = client.ReceiveBytesFromClient();
                    if (recv[0] == (byte)'v')
                    {
                        int phong = TimPhong(client, recv);
                        Console.WriteLine(phong);
                        client.ChooseServer(phong);
                    }
                    client.ForwardToServer();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void indsphong()
        {
            Console.Write("DS phong server 1: ");
            foreach (string ten in RoomsSV1)
            {
                Console.Write(ten + " ");
            }
            Console.WriteLine();

            Console.Write("DS phong server 2: ");
            foreach (string ten in RoomsSV2)
            {
                Console.Write(ten + " ");
            }
            Console.WriteLine();
        }
    }
}
