using System;
using System.Threading;
namespace TienLen_Server
{
    class BotRoom : Room
    {
        private Player player;
        private Bot bot;
        Deck deck = new Deck();
        public BotRoom(Player player, string tenphong)
        {
            SoPhong = tenphong;
            this.player = player;
            bot = new Bot();
            Console.WriteLine("da tao phong bot");
        }
        public new void WaitForReady()
        {
            Console.WriteLine("waiting for ready from client");
            while (Playing == false) ;//wait
            StartGame();
        }

        public new void BroadCast(string Message) //message = "bai|"+ bai / bo luot
        {
            //if (!Message.StartsWith("winner"))
            {
                //ham broadcast se do player goi, broadcast bot se do bot goi sau khi xu ly xong
                player.SendData(Message);
                if (Message == "bai|boluot")
                    NewTurn(bot.username); //bot nhan luot moi
                else
                {
                    bot.ReceiveData(Message);
                    if (Message.StartsWith("bai") || Message == "turn|Mr.Robot")
                    {
                        string xulybot = bot.ReceiveData("turn|" + bot.username);
                        Console.WriteLine("xu ly bot: " + xulybot);

                        Thread.Sleep(250);
                        if (xulybot != "")
                            BotBroadCast(xulybot);
                    }
                }
            }
            //else player.SendData(Message);
        }

        public void BotBroadCast(string Message)
        {
            player.SendData(Message);
            if (Message == "bai|boluot") NewTurn(player.username);
            if (Message.StartsWith("bai"))
            {
                Thread.Sleep(250);
                player.SendData("turn|" + player.username);
            }
        }
        public new void Setup()
        {
            player.XaBai();
            bot.XaBai();
        }

        //bo luot thi gui newturn
        private void NewTurn(string username) //username nguoi giu luot moi
        {
            player.SendData("newturn|");
            bot.ReceiveData("newturn|");
            BroadCast("turn|" + username);
        }

        public new void FindFirstPlayer()
        {
            BroadCast("turn|" + player.username);
        }

        public new void EndGame(Player player = null)
        {
            BroadCast("winner|" + player == null ? bot.username : player.username);
            Playing = false;
            //bắt đầu ván mới
            Thread waitforready = new Thread(WaitForReady);
            waitforready.Start();
        }

        public new void ChonLuotTiepTheo(string username, bool boluot = false)
        {

        }
        public new void StartGame()
        {
            Playing = true;
            Setup();
            ChiaBai();
            Thread.Sleep(5000);
            NewTurn(player.username);
        }
        public new void ChiaBai()
        {
            string baibot = "";
            deck.Shuffle();
            int baicanchia = 0;
            for (int round = 0; round < 13; round++)
            {
                player.NhanBai(deck.Cards[baicanchia]);
                baicanchia++;
                baibot += "|" + deck.Cards[baicanchia].Information;
                baicanchia++;
            }
            player.SendData("vanmoi|" + player.Sanh());
            bot.ReceiveData("vanmoi" + baibot);
        }

        public new void ThemNguoiChoi(Player player)
        {
            if (SoLuongNguoiChoi < MaxPlayer)
            {
                this.player = player;
                SoLuongNguoiChoi++;
                Console.WriteLine("Phong {0} da them user {1}. So luong nguoi choi: {2}", SoPhong, player.username, SoLuongNguoiChoi);

                Thread wait = new Thread(WaitForReady);
                wait.Start();

            }
        }

        public new void XoaNguoiChoi(Player player)
        {
            SoLuongNguoiChoi = 0;
        }
    }
}
