using System;
using System.Collections.Generic;
using System.Threading;
namespace TienLen_Server
{
    [Serializable]
    class Player
    {
        public string username;
        public int money;
        public bool isInGame => PhongDangChoi != null ? PhongDangChoi.Playing : false;
        public SocketModel socket;
        private List<Card> BaiDangGiu;
        public Room PhongDangChoi;
        public int BaiConLai => BaiDangGiu == null ? 0 : BaiDangGiu.Count;

        /// <summary>
        /// create a new user
        /// </summary>
        /// <param name="uname">Username</param>
        /// <param name="money">Money</param>
        public Player(string uname, int money = 0)
        {
            username = uname;
            this.money = money;
            BaiDangGiu = new List<Card>();
            PhongDangChoi = null;
        }

        public Player()
        {
            username = "Mr.Robot";
            BaiDangGiu = new List<Card>();
            PhongDangChoi = null;
        }

        /// <summary>
        /// Gán socket cho Player mới chơi vào sử dụng
        /// </summary>
        /// <param name="use">Socket sử dụng để giao tiếp</param>
        public void UseSocket(SocketModel use)
        {
            socket = use;
        }

        /// <summary>
        /// Player thoát game
        /// </summary>
        public void ExitGame()
        {
            //thoat khi dang choi
            if (isInGame)
                money -= 300; //tru tien

            BaiDangGiu.Clear();
            ThoatPhong();
            socket = null;

        }

        public void EnjoyGame()
        {
            try
            {
                while (socket.isStillConnect())
                {
                    string received = socket.ReceiveData();
                    string[] data = received.Split('|');
                    switch (data[0]) //data[0]: 1:chon phong, 2: thoat
                    {
                        case "v":
                            VaoPhong(data[1]);
                            break;
                        case "t":
                            ThoatPhong();
                            break;
                        case "3":
                            DanhBai(received.Remove(0, 2)); //danh bai / bo luot
                            break;
                        case "4": //chu phong bat dau
                            PhongDangChoi.Playing = true;
                            break;
                        case "5":
                            PhongDangChoi.BroadCast("chat|" + username + "|" + data[1]); //chat
                            break;
                        case "6":
                            PhongDangChoi.BroadCast("tt|" + username); //toi trang
                            break;
                    }
                }
                ExitGame();
            }
            catch (Exception ect)
            {
                Console.WriteLine(ect.Message);
            }
            Console.WriteLine("User " + username + " exited");
            socket.CloseSocket();
            socket = null;
        }

        public void DanhBai(string data)
        {
            PhongDangChoi.BroadCast("bai|" + data);
            if (data != "boluot")
            {
                string[] cardinfo = data.Split('|');
                foreach (string ci in cardinfo)
                    for (int i = 0; i < BaiDangGiu.Count; i++)
                        if (BaiDangGiu[i].Information == ci)
                        {
                            BaiDangGiu.RemoveAt(i);
                            break;
                        }
                if (BaiDangGiu.Count != 0)
                    PhongDangChoi.ChonLuotTiepTheo();
                else PhongDangChoi.EndGame(this);
            }
            else PhongDangChoi.ChonLuotTiepTheo(true);
        }

        public void VaoPhong(string SoPhong)
        {
            Server.ThemNguoiChoiVaoPhong(this, SoPhong);
            if (PhongDangChoi == null)
            {
                Console.WriteLine("Phong da full hoac da bat dau");
                socket.SendData("-1");
            }
        }

        public void ThoatPhong()
        {
            if (PhongDangChoi != null)
                Server.XoaNguoiChoiKhoiPhong(this);
        }

        public void NhanBai(Card card)
        {
            if (BaiDangGiu == null) BaiDangGiu = new List<Card>();
            BaiDangGiu.Add(card);
        }

        public void Disconnect()
        {
            //thoát khỏi phòng
            if (socket != null)
            {
                socket.CloseSocket();
            }
        }

        /// <summary>
        /// Lấy ra quân bài có giá trị nhỏ nhất
        /// </summary>
        /// <returns></returns>
        public Card GetLowestCard()
        {
            if (BaiDangGiu.Capacity > 0)
            {
                Card lowest = BaiDangGiu[0];
                foreach (Card card in BaiDangGiu)
                    if (card < lowest) lowest = card;
                return lowest;
            }
            else return null;
        }

        public string Sanh()
        {
            string sanh = "";
            if (BaiDangGiu != null)
            {
                foreach (Card card in BaiDangGiu)
                {
                    sanh += card.Information + "|";
                }
                return sanh.Remove(sanh.Length - 1, 1);
            }
            return sanh;
        }

        /// <summary>
        /// xả bài. clear bài đang giữ, bắt đầu ván mới
        /// </summary>
        public void XaBai()
        {
            if (BaiDangGiu != null)
                BaiDangGiu.Clear();
        }

        public void SendData(string data)
        {
            socket.SendData(data);
            Thread.Sleep(250);
        }
        //
        //code_s below is for debug
        //
        public void PrintInfor()
        {
            Console.WriteLine("username: " + username + " || money: " + money);
        }
        public void PrintBaiDangGiu()
        {
            foreach (Card card in BaiDangGiu)
            {
                card.Debug_Print();
            }
            Console.WriteLine();
        }
    }
}
