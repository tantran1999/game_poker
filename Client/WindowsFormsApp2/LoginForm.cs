using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TienLen_Client
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        private TCPClient tcp;
        private void LoginForm_Load(object sender, EventArgs e)
        {
            ConnectToServer();
        }
        private void ConnectToServer()
        {
            tcp = new TCPClient("192.168.43.123", 8080);
            tcp.ConnectToServer();
            if (tcp.ReadData() == "0")
            {
                bt_SignIn.Enabled = true;
                bt_Signup.Enabled = true;
            }
            else
            {
                MessageBox.Show("Fail to connect to server!!!");
                Environment.Exit(Environment.ExitCode);
            }
        }
        private void bt_SignIn_Click(object sender, EventArgs e)
        {
            string data = "1|" + tb_Account.Text + "|" + tb_Password.Text;
            tcp.SendData(data);
            data = tcp.ReadData();
            string[] res = data.Split('|');
            if (res[0] == "0")
            {
                LobbyForm lf = new LobbyForm(tcp, res[1], int.Parse(res[2]));
                this.Hide();
                lf.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("Lỗi đăng nhập!!!");
        }

        private void bt_Signup_Click(object sender, EventArgs e)
        {
            string data = "0|" + tb_Account.Text;
            tcp.SendData(data);
            data = tcp.ReadData();
            string[] res = data.Split('|');
            if (res[0] == "0")
            {
                LobbyForm lf = new LobbyForm(tcp, res[1], int.Parse(res[2]));
                this.Hide();
                lf.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("Lỗi đăng kí!!!");
        }

        private void bt_Exit_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        private void tb_Account_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bt_SignIn_Click(this, new EventArgs());
            }
        }
    }
}
