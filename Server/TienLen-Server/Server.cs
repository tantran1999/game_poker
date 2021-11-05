using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace TienLen_Server
{
    class Server
    {

        private static List<Room> Rooms;   //quan ly cac phong choi
        private TcpListener tcpListener;
        public bool Started = false;
        private static SocketModel control = null; //socket to communicate with load balancer
        private DBServer Database = new DBServer("Data Source=DESKTOP-NAR8O1L;Initial Catalog=TienLen;Integrated Security=True");
        /// <summary>
        /// Start Server
        /// </summary>
        public void KhoiDongServer()
        {
            try
            {
                //setup server
                Database.ConnectToServer();
                Rooms = new List<Room>();

                //setup tcp listener
                //Console.Write("Nhap IP: ");
                //IPAddress IP = IPAddress.Parse(Console.ReadLine());
                IPAddress IP = IPAddress.Parse("127.0.0.1");
                Console.Write("Nhap Port: ");
                int Port;
                int.TryParse(Console.ReadLine(), out Port);
                tcpListener = new TcpListener(IP, Port);
                //tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8080);
                tcpListener.Start();
                Started = true;

                //connect balancer
                Console.WriteLine("Waiting for Balancer");
                control = new SocketModel(tcpListener.AcceptSocket());
                Console.WriteLine("Balancer connected");

                //server started successfully
                Console.WriteLine("Server Started. Listening at " + tcpListener.LocalEndpoint.ToString());

                //working/listening
                Listen();

            }
            catch (Exception ect)
            {
                Console.WriteLine(ect.Message);
            }
        }

        /// <summary>
        /// Listen for new connection and then serve for CLIENT at connected socket
        /// </summary>
        private void Listen()
        {
            try
            {
                SocketModel tempSocket;
                while (true)
                {
                    tempSocket = new SocketModel(tcpListener.AcceptSocket());
                    tempSocket.SendData("0");
                    Console.WriteLine("New client connected at " + tempSocket.RemoteEndPoint);
                    Thread phucvusocket = new Thread(PhucVuSocket);
                    phucvusocket.Start(tempSocket);
                }
            }
            catch (Exception ect)
            {
                Console.WriteLine(ect.Message);
            }
        }

        /// <summary>
        /// Phục vụ các yêu cầu đăng ký, đăng nhập trên socket mới kết nối
        /// </summary>
        /// <param name="socket">Socket vừa mới kết nối</param>
        private void PhucVuSocket(object socket)
        {
            SocketModel ServingSocket = (SocketModel)socket; //socket gui nhan du lieu
            Player PlayingUser = null; //user đang phục vụ

            //lặp đến khi đăng nhập hoặc đăng ký thành công
            try
            {
                while (ServingSocket.isStillConnect() && PlayingUser == null)
                {

                    //nhan du lieu yeu cau dang ky/dang nhap
                    string data = ServingSocket.ReceiveData();
                    if (data != "")
                    {
                        string[] received = data.Split('|');
                        //dang ky/dang nhap
                        if (received[0] == "0")
                        {
                            Console.Write(ServingSocket.RemoteEndPoint + ": Dang ky: ");
                            PlayingUser = DangKy(received[1], received[2]);
                        }
                        else if (received[0] == "1")
                        {
                            Console.Write(ServingSocket.RemoteEndPoint + ": Dang nhap: ");
                            PlayingUser = DangNhap(received[1], received[2]);
                        }
                        //dangky/dangnhap thanh cong
                        if (PlayingUser != null)
                        {
                            Console.WriteLine(received[1] + ": OK!");
                            ServingSocket.SendData("0|" + PlayingUser.username + "|" + PlayingUser.money.ToString()); //send ok
                            PlayingUser.UseSocket(ServingSocket); //gan socket cho user su dung
                            PlayingUser.EnjoyGame(); //user sẵn sàng chơi game
                        }
                        else
                        {
                            Console.WriteLine(received[1] + ": Failed!");
                            ServingSocket.SendData("1");
                            //lặp lại (đăng ký, đăng nhập lại nếu không thành công)
                        }
                    }
                }

            }
            catch (Exception ect)
            {
                Console.WriteLine(ect.Message);
            }
            if (PlayingUser == null)
            {
                Console.WriteLine(ServingSocket.RemoteEndPoint + " closed");
                ServingSocket = null;
            }

        }

        /// <summary>
        /// Đăng ký user mới
        /// </summary>
        /// <param name="uname">Username</param>
        /// <returns>Player mới được đăng ký nếu thành công. Lưu trong Players</returns>
        public Player DangKy(string uname, string psw)
        {
            return Database.CreateNewUser(uname, psw);
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="uname">Username</param>
        /// <returns>Player có sẵn trong Players nếu đăng nhập thành công</returns>
        public Player DangNhap(string uname, string psw)
        {
            return Database.FindUser(uname, psw);
        }

        /// <summary>
        /// Shutdown server
        /// </summary>
        public void Shutdown()
        {
            ////đóng tất cả kết nối
            //tcpListener.Stop();
            //foreach (Player player in Players)
            //    player.Disconnect();

            ////save user
            //FileStream fileStream = new FileStream("Users_Information.txt", FileMode.OpenOrCreate);
            //BinaryFormatter binaryFormatter = new BinaryFormatter();
            //binaryFormatter.Serialize(fileStream, Players);
            //fileStream.Close();
            //Console.WriteLine("All Users's Information Saved to \"" + fileStream.Name + "\". Please keep it safe");

            //send request to balancer. 
        }

        /// <summary>
        /// Thêm người chơi vào phòng được chọn
        /// </summary>
        /// <param name="player">Người chơi</param>
        /// <param name="SoPhong">Số Phòng</param>
        public static void ThemNguoiChoiVaoPhong(Player player, string SoPhong)
        {
            Room foundedroom = Rooms.Find(r => r.SoPhong == SoPhong);
            if (foundedroom == null)
            {
                if (SoPhong.StartsWith("bot"))
                    foundedroom = new BotRoom(player, SoPhong);
                else
                    foundedroom = new Room(SoPhong);
                Rooms.Add(foundedroom);
            }
            if (foundedroom.SoLuongNguoiChoi == foundedroom.MaxPlayer || foundedroom.Playing == true) foundedroom = null;
            else
            {
                player.PhongDangChoi = foundedroom;
                if (!foundedroom.isBotRoom)
                    foundedroom.ThemNguoiChoi(player);
                else ((BotRoom)foundedroom).ThemNguoiChoi(player);
            }
        }

        /// <summary>
        /// Xoá người chơi ra khỏi phòng
        /// </summary>
        /// <param name="player"></param>
        /// <param name="room"></param>
        public static void XoaNguoiChoiKhoiPhong(Player player)
        {
            Room room = player.PhongDangChoi;
            if (room != null)
            {
                if (!room.isBotRoom)
                    room.XoaNguoiChoi(player);
                else
                    ((BotRoom)room).XoaNguoiChoi(player);
                player.PhongDangChoi = null;
                if (room.SoLuongNguoiChoi == 0)
                {
                    control.SendData("0|" + room.SoPhong);
                    Rooms.Remove(room);
                }
            }
        }

        ///// <summary>
        ///// Print all users's information
        ///// </summary>
        //public void PrintAllUsers()
        //{
        //    Console.WriteLine("All Player:");
        //    foreach (Player player in Players)
        //    {
        //        player.PrintInfor();
        //    }
        //    Console.WriteLine("Player Count: {0}", Players.Count);
        //}

        ///// <summary>
        ///// Get list of online user
        ///// </summary>
        //public void PrintOnlineUser()
        //{
        //    int count = 0;
        //    Console.WriteLine("Online User: ");
        //    foreach (Player player in Players)
        //    {
        //        if (player.socket != null)
        //        {
        //            if (player.socket.isStillConnect())
        //            {
        //                player.PrintInfor();
        //                count++;
        //            }
        //        }
        //    }
        //    Console.WriteLine("Online User Count: {0}", count);
        //}
        public void DsPhong()
        {
            foreach (Room room in Rooms)
            {
                Console.WriteLine("Phong {0}: {1}", room.SoPhong, room.SoLuongNguoiChoi);
            }
        }
    }
}
