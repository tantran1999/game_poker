using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienLen_Client
{
    public class Player
    {
        public TCPClient tcp;
        public string username;
        private int status;
        public int moneyAvailable;
        public Player(TCPClient tcp, string username, int money)
        {
            this.tcp = tcp;
            this.username = username;
            this.moneyAvailable = money;
        }
        public Player(string username)
        {
            this.username = username;
        }
        public Player()
        {
            this.username = "";
            this.moneyAvailable = 0;
        }
    }
}
