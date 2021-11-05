using System;
using System.Threading;
namespace TienLen_Server
{
    class Room
    {
        public string SoPhong { get; set; }
        private Player[] Players;
        private int ServingPlayer; //nguoi duoc phep danh/nguoi ve nhat
        public int SoLuongNguoiChoi;
        private Deck deck;
        private Timer timer;
        public bool Playing;
        private bool[] Turn;
        public int MaxPlayer;
        public bool isBotRoom;
        public Room(string SoPhong)
        {
            MaxPlayer = 4;
            this.SoPhong = SoPhong;
            Players = new Player[MaxPlayer];
            ServingPlayer = -1;
            deck = new Deck();
            SoLuongNguoiChoi = 0;
            timer = new Timer(15);
            Playing = false;
            Turn = new bool[MaxPlayer];
            isBotRoom = false;
        }

        public Room()
        {
            MaxPlayer = 2;
            SoLuongNguoiChoi = 1;
            isBotRoom = true;
        }

        /// <summary>
        /// setup các giá trị trước khi bắt đầu ván mới
        /// </summary>
        public void Setup()
        {
            for (int i = 0; i < MaxPlayer; i++)
                if (Players[i] != null)
                    Players[i].XaBai();
        }

        public int TimGheTrong()
        {
            for (int ghe = 0; ghe < MaxPlayer; ghe++)
                if (Players[ghe] == null) return ghe;
            return -1;
        }
        /// <summary>
        /// Thêm người chơi khi vào phòng
        /// </summary>
        /// <param name="player"></param>
        public void ThemNguoiChoi(Player player)
        {
            if (SoLuongNguoiChoi < MaxPlayer)
            {
                Players[TimGheTrong()] = player;
                SoLuongNguoiChoi++;
                GuiThongTinCho();
                Console.WriteLine("Phong {0} da them user {1}. So luong nguoi choi: {2}", SoPhong, player.username, SoLuongNguoiChoi);
                if (SoLuongNguoiChoi > 1)
                {
                    if (!timer.Counting)
                    {
                        Thread wait = new Thread(WaitForReady);
                        wait.Start();
                    }
                    else timer.Reset();
                }
            }
        }
        public void WaitForReady()
        {
            timer.StartFromBegin();
            while (timer.Counting && Playing == false) ;//wait
            if (SoLuongNguoiChoi >= 2)
            {
                StartGame();
            }
        }

        /// <summary>
        /// Tìm ghế player đang ngồi trong bàn
        /// </summary>
        /// <param name="player">Player cần tìm</param>
        /// <returns>-1 nếu không tìm thấy</returns>
        private int TimGheDangNgoi(Player player)
        {
            for (int ghe = 0; ghe < MaxPlayer; ghe++)
                if (Players[ghe] == player) return ghe;
            return -1;
        }
        /// <summary>
        /// Xoá người chơi khi khỏi phòng
        /// </summary>
        /// <param name="player"></param>
        public void XoaNguoiChoi(Player player)
        {
            int vitri = TimGheDangNgoi(player);
            if (vitri != -1)
            {
                if (player.isInGame)
                {
                    player.money -= 300;//trừ tiền nếu chưa kết thúc game
                    //nếu đang là lượt của nó
                    if (ServingPlayer == vitri) player.DanhBai("3|boluot");
                    Players[vitri] = null;
                }
                if (ServingPlayer == vitri) ServingPlayer = -1; //người thoát là người về nhất thì reset lượt đánh
                Players[vitri] = null;
                GuiThongTinCho();
                SoLuongNguoiChoi--;
                Console.WriteLine("User {1} da thoat khoi phong {0}. So luong nguoi choi {2}", SoPhong, player.username, SoLuongNguoiChoi);
                if (SoLuongNguoiChoi == 1 && Playing)
                {
                    int i = 0;
                    for (i = 0; Players[i] == null; i++) ;
                    Console.WriteLine("last user: " + Players[i].username);
                    EndGame(Players[i]);
                }
            }
        }

        /// <summary>
        /// gửi thông tin về các chỗ ngồi trong phòng cho các player. phục vụ cập nhật giao diện khi có người chơi vào/thoát phòng
        /// </summary>
        private void GuiThongTinCho()
        {
            string thongtincho = "ttc";
            //lấy thông tin các chỗ ngồi
            for (int ghe = 0; ghe < MaxPlayer; ghe++)
                thongtincho += Players[ghe] == null ? "|" : "|" + Players[ghe].username;
            //gửi thông tin cho các player trong phòng
            BroadCast(thongtincho);
        }
        /// <summary>
        /// Xác định người đầu tiên được đánh
        /// </summary>
        public void FindFirstPlayer()
        {
            if (ServingPlayer == -1) //nếu bắt đầu ván mới hoặc người về nhất đã thoát thì mới cần xác định lại
            {
                ServingPlayer = 0;
                for (int ghe = 0; ghe < MaxPlayer; ghe++)
                    if (Players[ghe] != null)
                        if (Players[ghe].GetLowestCard() < Players[ServingPlayer].GetLowestCard())
                            ServingPlayer = ghe;
            }
            //ngược lại giữ nguyên

            //thông báo ai đang giữ lượt cho các user
            BroadCast("turn|" + Players[ServingPlayer].username);
        }

        /// <summary>
        /// Chia bài khi vào ván mới
        /// </summary>
        public void ChiaBai()
        {
            deck.Shuffle();
            int nguoicanchia = (ServingPlayer != -1 ? ServingPlayer : 0);
            int baicanchia = 0;
            for (int round = 0; round < 13; round++)
            {
                for (int seq = 0; seq < SoLuongNguoiChoi; seq++)
                {
                    Players[nguoicanchia].NhanBai(deck.Cards[baicanchia]);

                    nguoicanchia = (nguoicanchia + 1) % SoLuongNguoiChoi;
                    baicanchia++;
                }
            }
            for (int seq = 0; seq < SoLuongNguoiChoi; seq++)
            {
                Players[nguoicanchia].SendData("vanmoi|" + Players[nguoicanchia].Sanh());
                nguoicanchia = (nguoicanchia + 1) % SoLuongNguoiChoi;
            }
        }


        /// <summary>
        /// BroadCast information to all player in room
        /// </summary>
        /// <param name="Message"></param>
        public void BroadCast(string Message)
        {
            if (!isBotRoom)
                foreach (Player player in Players)
                {
                    if (player != null) player.SendData(Message);
                }
            else ((BotRoom)this).BroadCast(Message);
        }

        public void StartGame()
        {
            Playing = true;
            Setup();
            Console.WriteLine("Phong {0} started new game", SoPhong);
            ChiaBai();
            Thread.Sleep(5000);
            NewTurn();
            FindFirstPlayer();
            Console.WriteLine(Players[ServingPlayer].username + " danh truoc");
        }

        public void ChonLuotTiepTheo(bool boluot = false)
        {
            if (!isBotRoom)
            {
                int NextPlayer = (ServingPlayer + 1) % MaxPlayer;
                while (Turn[NextPlayer] == false) NextPlayer = (NextPlayer + 1) % MaxPlayer;
                if (boluot)
                {
                    Turn[ServingPlayer] = false;
                    while (Turn[ServingPlayer] == false)
                    {
                        ServingPlayer = (ServingPlayer + 3) % MaxPlayer;
                    }
                }
                if (NextPlayer == ServingPlayer)
                {
                    NewTurn();
                    //Thread.Sleep(500);
                }
                ServingPlayer = NextPlayer;
                BroadCast("turn|" + Players[ServingPlayer].username);
            }
        }

        private void NewTurn()
        {
            for (int i = 0; i < MaxPlayer; i++)
                if (Players[i] != null)
                {
                    Turn[i] = true;
                    Players[i].SendData("newturn|");
                }
        }

        public void EndGame(Player player)
        {
            if (!isBotRoom)
            {
                BroadCast("winner|" + player.username);
                Playing = false;
                //bắt đầu ván mới
                Thread waitforready = new Thread(WaitForReady);
                waitforready.Start();
            }
            else ((BotRoom)this).EndGame(player);
        }
        public void PrintPlayer()
        {
            Console.WriteLine("DS User phong {0}:", SoPhong);
            foreach (Player player in Players)
            {
                player.PrintInfor();
            }
        }
    }
}
