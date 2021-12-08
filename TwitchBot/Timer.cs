using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TwitchBot
{
    static class Timer
    {
        private static string dateTimePath = @"E:\Desktop\Temp\dateTime.txt";

        private static void OverwriteText()
        {
            if(File.Exists(dateTimePath))
            {
                File.Delete(dateTimePath);
            }
            using (StreamWriter writer = File.AppendText(dateTimePath))
            {
                writer.WriteLine((DateTime.Now.ToString("MM/dd/yyyy HH:mm tt").ToUpper()));
            }
        }

        public static void Timer_Tick()
        {
            OverwriteText();
        }

        public static void Start()
        {
            System.Threading.Timer timer = new System.Threading.Timer(_ => Timer.Timer_Tick(), null, 0, 1000);
        }
    }
}
