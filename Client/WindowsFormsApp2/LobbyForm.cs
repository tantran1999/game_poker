using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TienLen_Client
{
    public partial class LobbyForm : Form
    {
        public LobbyForm()
        {
            InitializeComponent();
        }
        Player player;
        string[] TableInfo;
        public LobbyForm(TCPClient tcp, string username, int money)
        {
            InitializeComponent();
            this.player = new Player(tcp, username, money);
        }
        private void LobbyForm_Load(object sender, EventArgs e)
        {
            lb_Username.ButtonText = "Username: " + this.player.username;
            lb_Money.ButtonText = "Money: " + this.player.moneyAvailable.ToString();
        }
        private void bt_Join_Click(object sender, EventArgs e)
        {
            try
            {
                player.tcp.SendData("v|" + tb_RoomID.Text);
                string data = player.tcp.ReadData();
                TableInfo = data.Split('|');
                if (TableInfo[0] == "ttc")
                {
                    Game g = new Game(player, data.Remove(0, 4), tb_RoomID.Text);
                    this.Hide();
                    g.ShowDialog();
                    tb_RoomID.Clear();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Fail to join room!!!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void tb_RoomID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bt_Join_Click(this, new EventArgs());
            }
        }

        private void bt_JoinBot_Click(object sender, EventArgs e)
        {
            try
            {
                player.tcp.SendData("v|bot" + tb_RoomID.Text);
                //string data = player.tcp.ReadData();
                //TableInfo = data.Split('|');
                //if (TableInfo[0] == "ttc")
                //{
                    Game g = new Game(player, "ngu", "bot" + tb_RoomID.Text);
                    this.Hide();
                    g.ShowDialog();
                    this.Show();
                /*}
                else
                {
                    MessageBox.Show("Fail to join room!!!");
                }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
