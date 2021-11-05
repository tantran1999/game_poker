using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TienLen_Client
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
        }
        private Player player;
        private List<Card> CurrentCards = new List<Card>();
        private List<Card> lcard = new List<Card>();
        private List<Card> beforeCard = new List<Card>();
        private Player[] players = new Player[3];
        private List<Bunifu.Framework.UI.BunifuImageButton> DownImage = new List<Bunifu.Framework.UI.BunifuImageButton>();
        private int MyTurn = 0;
        private int numbefCard = 0;
        private int timer = 15;
        private string RoomID;
        private bool isClickedSkip = false;
        private bool isClickedDanh = false;
        private string ThongTinCho;

        public Game(Player player, string ThongTinCho, string RoomID)
        {
            InitializeComponent();
            this.player = player;
            this.ThongTinCho = ThongTinCho;
            this.RoomID = RoomID;
        }

        private void Game_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            HideImageThread();          
            Thread t = new Thread(Playing);
            t.IsBackground = true;
            t.Start();
            MyLabel.Text = player.username;
            lb_RoomID.Text = "Phòng " + RoomID;
            if (RoomID.StartsWith("bot"))
            {
                bt_Start.Enabled = true;
                bt_Start.Show();
            }
        }

        private void Card_Click(object sender, EventArgs e)
        {
            Bunifu.Framework.UI.BunifuImageButton pb = (Bunifu.Framework.UI.BunifuImageButton)sender;
            DownImage.Add(pb);
            string nameImage = pb.AccessibleDescription;
            Console.WriteLine(nameImage);
            int flag = 0;
            Card tmp = new Card(nameImage);
            if (lcard.Count == 0)
            {
                lcard.Add(tmp);
                pb.Location = new Point(pb.Location.X, pb.Location.Y - 25);
            }
            else
            {
                for (int i = 0; i < lcard.Count; i++)
                {
                    if (lcard[i] == tmp)
                    {
                        pb.Location = new Point(pb.Location.X, pb.Location.Y + 25);
                        lcard.Remove(lcard[i]);
                        flag = 1;
                        DownImage.Remove(pb);
                        DownImage.Remove(pb);
                        break;
                    }
                }
                if (flag == 0)
                {
                    lcard.Add(tmp);
                    pb.Location = new Point(pb.Location.X, pb.Location.Y - 25);
                }
            }
            CardType ct = new CardType(lcard);
            if (ct.isHopLy(new CardType(beforeCard)) && (MyTurn == 1) && (lcard.Count() != 0))
                Danh.Enabled = true;
            else
                Danh.Enabled = false;
        }

        private void Danh_Click(object sender, EventArgs e)
        {
            isClickedDanh = true;
            Danh.Enabled = false;
            Danh.Hide();
            Skip.Hide();
            SendData();
            int numCardDelete = lcard.Count();
            int tmp = CurrentCards.Count();
            RemoveCard();
            lcard.Clear();
            Thread t = new Thread(LoadImage);
            t.Start();
            DownImage.Clear();
            DeleteImage(numCardDelete, tmp);
            MyTurn = 0;
        }

        private void Skip_Click(object sender, EventArgs e)
        {
            isClickedSkip = true;
            SkipClickEven();
        }

        private void bt_XepBai_Click(object sender, EventArgs e)
        {
            CurrentCards.Sort((x, y) => x.CompareTo(y));
            Thread t = new Thread(LoadImage);
            t.Start();
        }

        private void ShowChatBox_OnValueChange(object sender, EventArgs e)
        {
            if (PanelChatBox.Width == 35)
            {
                PanelChatBox.Visible = false;
                PanelChatBox.Width = 284;
                PanelAnimation.ShowSync(PanelChatBox);
                Message.Show();
                ChatBox.Show();
                Send.Enabled = true;
                Send.Show();
                ShowChatBox.Location = new Point(249, 383);
            }
            else
            {
                ShowChatBox.Location = new Point(0, 383);
                PanelChatBox.Visible = false;
                PanelChatBox.Width = 35;
                PanelAnimation.ShowSync(PanelChatBox);
                Message.Hide();
                ChatBox.Hide();
                Send.Enabled = false;
                Send.Hide();
            }
        }

        private void Send_Click(object sender, EventArgs e)
        {
            player.tcp.SendData("5|" + Message.Text);
            Message.Clear();
        }

        private void Message_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Send_Click(this, new EventArgs());
            }
        }

        private void ChatBox_TextChanged(object sender, EventArgs e)
        {
            ChatBox.SelectionStart = ChatBox.Text.Length;
            ChatBox.ScrollToCaret();
        }

        private void bt_Start_Click(object sender, EventArgs e)
        {
            player.tcp.SendData("4|");
        }

        //Sự kiện khi bỏ lượt
        private void SkipClickEven()
        {
            string data = "3|boluot";
            player.tcp.SendData(data);
            Thread t = new Thread(DownCardImage);
            t.Start();
            lcard.Clear();
            Danh.Hide();
            Skip.Hide();
        }

        //Hạ bài xuống khi đã nhấn vào card mà bỏ lượt
        private void DownCardImage() 
        {
            foreach (Bunifu.Framework.UI.BunifuImageButton ib in DownImage)
            {
                ib.Location = new Point(ib.Location.X, ib.Location.Y + 25);
            }
            DownImage.Clear();
        }

        //Gửi thông tin bài khi đánh
        private void SendData()
        {
            string data = "3";
            foreach (Card card in lcard)
                data = data + "|" + card.Information;
            player.tcp.SendData(data);
        }

        //Load ảnh sau khi chia bài
        private void ShowMyImage(Bunifu.Framework.UI.BunifuImageButton a, string name, int x, int y)
        {
            // Stretches the image to fit the pictureBox.
            a.Location = new Point(x, y);
            a.SizeMode = PictureBoxSizeMode.StretchImage;
            object image = Properties.Resources.ResourceManager.GetObject("_" + name);
            a.Image = (Image)image;
            a.AccessibleDescription = name;
        }
        private void LoadImage()
        {
            int x = 439, y = 501;
            for (int i = 1; i <= CurrentCards.Count(); i++)
            {
                Bunifu.Framework.UI.BunifuImageButton pb = (Bunifu.Framework.UI.BunifuImageButton)this.Controls.Find("bunifuImageButton" + i.ToString(), true).FirstOrDefault();
                ShowMyImage(pb, CurrentCards[i-1].Information, x, y);
                CardAnimation.ShowSync(pb);
                pb.Enabled = true;
                pb.BringToFront();
                x += 25;
            }
        }

        // Xóa các bài đã đánh
        private void RemoveCard() 
        {
            foreach (Card card in lcard)
            {
                for (int i = 0; i < CurrentCards.Count(); i++)
                {
                    if (CurrentCards[i] == card)
                    {
                        CurrentCards.Remove(CurrentCards[i]);
                        break;
                    }
                }
            }
        }

        //Xóa bài đã đánh trên giao diện người dùng
        private void DeleteImage(int numCardDelete, int numCardCur) 
        {
            for (int i = numCardCur - numCardDelete + 1; i <= numCardCur; i++)
            {
                Bunifu.Framework.UI.BunifuImageButton pb = (Bunifu.Framework.UI.BunifuImageButton)this.Controls.Find("bunifuImageButton" + i.ToString(), true).FirstOrDefault();
                pb.Image = null;
                pb.Enabled = false;
                pb.Hide();
            }
        }

        // Hiển thị bài người khác hoặc mình đánh lên bàn
        private void ShowImageBefore(Bunifu.Framework.UI.BunifuImageButton pb, string name)
        {
            object image = Properties.Resources.ResourceManager.GetObject(name);
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
            pb.Image = (Image)image;
        }
        //Hiển thi bài người khác hoặc mình đánh lên bàn
        private void LoadbeforeCardImage()
        {
            int tmp = (14 - beforeCard.Count()) / 2 + 14;
            DeleteImageBefore((14 - numbefCard) / 2 + 14, (14 - numbefCard) / 2 + 14 + numbefCard);
            for (int i = 0; i < beforeCard.Count(); i++)
            {
                Bunifu.Framework.UI.BunifuImageButton pb = (Bunifu.Framework.UI.BunifuImageButton)this.Controls.Find("bunifuImageButton" + tmp.ToString(), true).FirstOrDefault();
                ShowImageBefore(pb, "_" + beforeCard[i].Information);
                tmp += 1;
                pb.Show();
            }
            numbefCard = beforeCard.Count();
        }

        //Xóa bài người khác đánh trên bàn khi có người khác đánh bài mới
        private void DeleteImageBefore(int Start, int Finish)
        {
            for (int i = Start; i <= Finish; i++)
            {
                Bunifu.Framework.UI.BunifuImageButton pb = (Bunifu.Framework.UI.BunifuImageButton)this.Controls.Find("bunifuImageButton" + i.ToString(), true).FirstOrDefault();
                pb.Image = null;
                pb.Hide();
            }
        }

        //Xử lí thông tin chỗ ngồi --- Được sắp xếp theo thứ tự đánh
        private void XuLiThongTinCho()
        {
            string[] data = ThongTinCho.Split('|');
            int MyIndex = 0;
            for (int i = 0; i < 3; i++)
                players[i] = new Player();
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == player.username)
                {
                    MyIndex = i;
                    break;
                }
            }
            int tmp = (MyIndex + 1) % 4;
            int index = 0;
            while (tmp != MyIndex)
            {
                players[index] = new Player(data[tmp]);
                tmp = (tmp + 1) % 4;
                index++;
            }
            
        }

        //Hiển thị chỗ ngồi của các người chơi khác
        private void DisplayOtherPlayers()
        {
            int count = 1;
            if (players[0].username != "")
            {
                bunifuImageButton27.Show();
                bunifuPictureBox1.Show();
                bunifuImageButton27.Show();
                bunifuImageButton27.BringToFront();
                count++;
            }
            if (players[1].username != "")
            {
                bunifuImageButton28.Show();
                bunifuPictureBox2.Show();
                bunifuImageButton28.Show();
                bunifuImageButton28.BringToFront();
                count++;
            }
            if (players[2].username != "")
            {
                bunifuImageButton29.Show();
                bunifuPictureBox3.Show();
                bunifuImageButton29.Show();
                bunifuImageButton29.BringToFront();
                count++;
            }
            if (count >= 2)
            {
                bt_Start.Enabled = true;
                bt_Start.Show();
            }
        }

        //Đếm ngược thời gian đánh của người chơi - Nêu hết time xem như Skip
        private void CountDown(Bunifu.Framework.UI.BunifuCircleProgressbar cp)
        {
            while (true)
            {
                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        cp.Value = timer;
                    });
                    Thread.Sleep(1000);
                    if (timer == 0 || isClickedSkip == true)
                    {
                        if (isClickedSkip != true)
                            SkipClickEven();
                        timer = 15;
                        isClickedSkip = false;
                        cp.Hide();
                        break;
                    }
                    else if (isClickedDanh == true)
                    {
                        timer = 15;
                        isClickedDanh = false;
                        cp.Hide();
                        break;
                    }
                    timer--;
                }
                catch (Exception ex)
                {

                }
            }
        }

        //Hiển thị đếm ngược khi người chơi khác đang đánh -- Nhận biết người chơi nào đang đánh
        private void DisplayCurrentPlayer(string username)
        {
            for (int i = 0; i < players.Count(); i++)
            {
                if (players[i] != null && players[i].username == username)
                {
                    Bunifu.Framework.UI.BunifuCircleProgressbar cp = (Bunifu.Framework.UI.BunifuCircleProgressbar)this.Controls.Find("bunifuCircleProgressbar" + (i + 1).ToString(), true).FirstOrDefault();
                    cp.Show();
                    Thread t = new Thread(() => CountDownOtherPlayer(cp, 15));
                    t.Start();
                    break;
                }
            }
        }
        private void CountDownOtherPlayer(Bunifu.Framework.UI.BunifuCircleProgressbar cp, int Ptimer)
        {
            while (true)
            {
                try
                {
                    Invoke((MethodInvoker)delegate
                    {
                        cp.Value = Ptimer;
                    });
                    Thread.Sleep(1000);
                    if (Ptimer == 0)
                    {
                        cp.Hide();
                        break;
                    }
                    Ptimer--;
                }
                catch (Exception ex)
                {

                }
            }
        }

        //Kiểm tra tới trắng
        int DemSoDoi(List<Card> lcard, List<Card> arrDoi)
        {
            int n = 0;
            arrDoi.Clear();
            lcard.Sort((x, y) => x.CompareTo(y)); //Sort bài
            for (int i = 0; i < lcard.Count() - 1;)
            {
                if (lcard[i].Rank == lcard[i + 1].Rank)
                {
                    arrDoi.Add(lcard[i]);
                    n++;
                    i += 2;
                }
                else i++;
            }
            return n;
        }
        bool ktToiTrang(List<Card> lcard)
        {
            List<Card> Doi = new List<Card>();
            int sodoi = DemSoDoi(lcard, Doi);

            CardType tmpType = new CardType(Doi);
            if (Doi.Count > 1)
                if (Doi[Doi.Count - 2].Rank == 15)
                    return true;

            if (sodoi == 6 || (sodoi == 0 || (sodoi == 1) && lcard[lcard.Count - 1].Rank != 15))
                return true;


            if (tmpType.count == 5 && tmpType.type == 2)
                return true;
            return false;
        }

        //Xu li MyTurn
        private void ExeMyTurn(string data)
        {
            if (player.username == data) // lượt chơi là của mình thì thực hiện
            {
                Danh.Show();
                Skip.Show();
                Skip.Enabled = true;
                MyTurn = 1;
                MyCircleProgess.Show();
                Thread t = new Thread(() => CountDown(MyCircleProgess)); //thực hiện đếm ngược thời gian để đánh bài
                t.Start();
            }
            else // lượt chơi của người khác thì thực hiện
            {
                DisplayCurrentPlayer(data);
            }
        }

        //ReadData từ Server để xử lí tín hiệu
        private void Playing()
        {
            while (true)
            {
                string rawdata = player.tcp.ReadData();
                if (rawdata != "")
                {
                    string[] data = rawdata.Split('|');
                    if (data[0] == "vanmoi")  //bắt đầu ván mới
                    {
                        Exit.Enabled = false;
                        bt_Start.Hide();
                        bt_Start.Enabled = false;
                        for (int i = 1; i < data.Length; i++)
                            CurrentCards.Add(new Card(data[i]));  //Add bài của Client vào 
                        /*if (ktToiTrang(CurrentCards) == true)
                            player.tcp.SendData("6");*/
                        Thread l = new Thread(LoadImage);
                        l.Start();
                        bt_XepBai.Show();
                    }
                    else if (data[0] == "ttc") //Nhận tín hiệu để xử lí thông tin chỗ
                    {
                        ThongTinCho = rawdata.Remove(0, 4);
                        XuLiThongTinCho();
                        DisplayOtherPlayers();
                    }
                    else if (data[0] == "bai") //nhận bài để hiển thị trên bàn hoặc bỏ lượt từ người chơi
                    {
                        if (data[1] != "boluot")
                        {
                            beforeCard.Clear();
                            for (int i = 1; i < data.Length; i++)
                                beforeCard.Add(new Card(data[i]));
                            Thread t = new Thread(LoadbeforeCardImage);
                            t.Start();
                        }
                    }
                    else if (data[0] == "newturn") //nhận lượt đánh mới
                    {
                        beforeCard.Clear();
                        Thread t = new Thread(() => DeleteImageBefore(14, 26)); //Xóa tất cả các bài trên bàn
                        t.Start();
                        numbefCard = 0;
                        if (data[1] != "") // Xu li loi~ newturn|turn|...
                            ExeMyTurn(data[2]);
                    }
                    else if (data[0] == "turn") // nhận lượt chơi
                    {
                        ExeMyTurn(data[1]);
                    }
                    else if (data[0] == "chat")
                    {
                        string senddata = "";
                        if (data[1] == player.username)
                            senddata += "Bạn: ";
                        else
                            senddata += data[1] + ": ";
                        ChatBox.AppendText(senddata + data[2] + Environment.NewLine);
                    }
                    else if (data[0] == "winner" || data[0] == "tt") //Nhận thông tin người chiến thắng và cũng xem như đã kết thúc ván
                    {
                        Exit.Enabled = true;
                        bt_XepBai.Hide();
                        if (data[1] == player.username)
                        {
                            Skip.Enabled = false;
                            bt_Start.Enabled = true;
                            bt_Start.Show();
                        }
                        beforeCard.Clear(); //Clear các bài đã đánh
                        Thread t = new Thread(() => DeleteImageBefore(14,26)); //Xóa bài trên bàn
                        t.Start();                     
                        CurrentCards.Clear(); //Xóa bài của mình
                        Thread t1 = new Thread(() => DeleteImageBefore(1, 13)); //Xóa bài mình đang cầm hiển thị trên giao diện người dùng
                        t1.Start();
                    }
                }
            }
        }

        private void HideImageThread()
        {
            Thread t1 = new Thread(HideImage);
            t1.Start();
            Thread t2 = new Thread(HideImage1);
            t2.Start();
            Thread t3 = new Thread(HideImage2);
            t3.Start();
            Thread t4 = new Thread(HideImage3);
            t4.Start();
            Thread t5 = new Thread(HideImage4);
            t5.Start();
            Thread t6 = new Thread(HideImage5);
            t6.Start();
        }
        private void HideImage()
        {
            bunifuImageButton1.Hide();
            bunifuImageButton2.Hide();
            bunifuImageButton3.Hide();
            bunifuImageButton4.Hide();
            bunifuImageButton5.Hide();
            bt_XepBai.Hide();
        }
        private void HideImage1()
        {
            bunifuImageButton7.Hide();
            bunifuImageButton8.Hide();
            bunifuImageButton9.Hide();
            bunifuImageButton10.Hide();
            bunifuImageButton11.Hide();
            bunifuImageButton29.Hide();
            bunifuCircleProgressbar1.Hide();
        }
        private void HideImage2()
        {
            bunifuImageButton6.Hide();
            bunifuImageButton13.Hide();
            bunifuImageButton12.Hide();
            Danh.Hide();
            Skip.Hide();
            bunifuImageButton28.Hide();
            MyCircleProgess.Hide();
            bunifuCircleProgressbar2.Hide();
        }
        private void HideImage3()
        {
            bunifuImageButton14.Hide();
            bunifuImageButton15.Hide();
            bunifuImageButton16.Hide();
            bunifuImageButton17.Hide();
            bunifuImageButton18.Hide();
            bunifuCircleProgressbar3.Hide();
        }
        private void HideImage4()
        {
            bunifuImageButton19.Hide();
            bunifuImageButton20.Hide();
            bunifuImageButton21.Hide();
            bunifuImageButton22.Hide();
            bunifuPictureBox3.Hide();
            bunifuImageButton27.Hide();
            //bt_Start.Hide();
        }
        private void HideImage5()
        {
            bunifuImageButton23.Hide();
            bunifuImageButton24.Hide();
            bunifuImageButton25.Hide();
            bunifuImageButton26.Hide();
            bunifuPictureBox1.Hide();
            bunifuPictureBox2.Hide();
            //bt_Start.Enabled = false;
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            player.tcp.SendData("t|");
            this.Close();
            
        }

    }
}
