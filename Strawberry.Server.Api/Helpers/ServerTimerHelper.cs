using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Strawberry.Server.Api.Helpers
{
    public class ServerTimerHelper
    {
        public static string Url { get; internal set; }

        private static Timer timer;

        private static Timer Timer
        {
            get
            {
                if (timer == null)
                {
                    timer = new Timer
                    {
                        AutoReset = false,
                        Enabled = false,
                    };
                    timer.Elapsed += Timer_Elapsed;
                }
                return timer;
            }
        }

        private static async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
#if DEBUG
                Console.WriteLine("==================================================");
                Console.WriteLine("[ServerTimer Start]");
                Console.WriteLine($"Time : {DateTime.Now}");
                Console.WriteLine("=================================================="); 
#endif

                var now = DateTime.Now;
                var nextTime = new DateTime(now.Year, now.Month, now.Day, 0, 10, 0).AddDays(1);
                var interval = (nextTime - now).TotalMilliseconds;

                Timer.Interval = interval;
                Timer.Enabled = true;

                using (var http = new System.Net.Http.HttpClient())
                {
                    await http.GetAsync($"http://localhost:50000/System/CheckFreeCount");
                }

#if DEBUG
                Console.WriteLine("==================================================");
                Console.WriteLine("[ServerTimer End]");
                Console.WriteLine($"Time : {DateTime.Now}");
                Console.WriteLine("=================================================="); 
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine("==================================================");
                Console.WriteLine("[ServerTimer Error]");
                Console.WriteLine(ex);
                Console.WriteLine("==================================================");
            }
        }

        public static void Start()
        {
            var now = DateTime.Now;
            var nextTime = new DateTime(now.Year, now.Month, now.Day, 0, 10, 0).AddDays(1);
            var interval = (nextTime - now).TotalMilliseconds;

            Timer.Interval = interval;
            Timer.Enabled = true;
        }

        public static void Stop()
        {
            Timer.Enabled = false;
        }
    }
}
