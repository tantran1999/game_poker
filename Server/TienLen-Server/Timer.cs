using System.Threading;
using System;
namespace TienLen_Server
{
    class Timer
    {
        private int Time { get; set; }
        public bool Counting { get; set; }
        private int TimeLeft;

        /// <summary>
        /// Khởi tạo timer
        /// </summary>
        /// <param name="CountingTime">Thời gian muốn đếm</param>
        public Timer(int CountingTime = 0)
        {
            Set(CountingTime);
            Counting = false;
        }
        /// <summary>
        /// Đặt thời gian mới.
        /// </summary>
        /// <param name="Time"></param>
        public void Set(int Time)
        {
            this.Time = Time;
            TimeLeft = Time;
        }
        /// <summary>
        /// Bắt đầu đếm ngược
        /// </summary>
        public void Start()
        {
            if (Counting==false)
            {
                Counting = true;
                Reset();
                Thread count = new Thread(CountDown);
                count.Start();
            }
        }
        public void StartFromBegin()
        {
            Reset();
            if (Counting==false) Start();
        }
        private void CountDown()
        {
            Console.WriteLine("Timer started");

            while (TimeLeft > 0 && Counting)
            {
                TimeLeft--;
                Thread.Sleep(1000);
            }
            this.Stop();
            this.Reset();
        }
        /// <summary>
        /// Reset timer về giá trị ban đầu và tiếp tục nếu đang đếm
        /// </summary>
        public void Reset()
        {
            TimeLeft = Time;
        }

        /// <summary>
        /// Dừng timer. Timer vẫn giữ nguyên giá trị thời gian còn lại
        /// </summary>
        public void Stop()
        {
            Counting = false;
        }
    }
}
