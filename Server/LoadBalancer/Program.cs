using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace LoadBalancer
{
    class Program
    {
        static Balancer balancer;
        static void Main(string[] args)
        {
            Thread startbalancer = new Thread(StartBalancer);
            startbalancer.Start();
            while (true)
            {
                string str = Console.ReadLine();
                if (str == "dsphong") balancer.indsphong();
            }
        }

        static void StartBalancer()
        {
            balancer = new Balancer();
            balancer.Start();
        }
    }
}
