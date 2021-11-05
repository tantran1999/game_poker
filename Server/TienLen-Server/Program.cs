using System;
using System.Threading;

namespace TienLen_Server
{
    class Program
    {

        public static Server server;

        static void KhoiDongServer()
        {
            server.KhoiDongServer();
        }

        /// <summary>
        /// Catch admin's control while server is running. For shutdown and debug
        /// </summary>
        static void CatchControlSignals()
        {
            string signal = "";
            while (signal != "shutdown")
            {
                signal = Console.ReadLine();
                switch (signal)
                {
                    case "shutdown":
                        server.Shutdown();
                        break;
                    //case "prtusr":
                    //    server.PrintAllUsers();
                    //    Console.WriteLine();
                    //    break;
                    //case "onlusr":
                    //    server.PrintOnlineUser();
                    //    Console.WriteLine();
                    //    break;
                    case "dsphong":
                        server.DsPhong();
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine("Command not found");
                        Console.WriteLine();
                        break;
                    //write new command here 

                }
            }
        }
        
        // main function here
        static int Main(string[] args)
        {

            server = new Server();
            Console.Write("Start Server(Y/N)? ");
            //if yes then startup server, else exit
            if ("Yy".Contains(Console.ReadKey().KeyChar.ToString()))
            {
                Console.WriteLine("\nStarting Server...");
                //khoi dong server
                Thread StartServer = new Thread(KhoiDongServer);
                StartServer.Start();
                Thread.CurrentThread.Priority = ThreadPriority.Lowest;

                //wait for control signal from console
                while (!server.Started) ;
                CatchControlSignals();

                //thoat server
                Console.Write("Server is shutting down...\nPress any key to close console and full exit");
                Console.ReadKey();
                Environment.Exit(Environment.ExitCode);
                return 0;
            }
            else return 0;
        }



        /*
        //
        //use main below for debug
        //
        static void Main(string[] args)
        {
            Timer timer = new Timer(5);
            timer.Start();
            Console.ReadKey();
        }
        */
        
    }
}
