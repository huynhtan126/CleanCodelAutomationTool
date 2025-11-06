using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadReport
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        //[STAThread]
        //static void Main()
        //{
        //    ScheduleDailyTask(new TimeSpan(17,59, 0)); // Chạy lúc 7:00 sáng mỗi ngày

        //    Console.WriteLine("Chương trình đang chạy, nhấn Enter để thoát...");
        //    Console.ReadLine();
        //}

        //static void ScheduleDailyTask(TimeSpan timeOfDay)
        //{
        //    DateTime now = DateTime.Now;
        //    DateTime nextRun = now.Date + timeOfDay;

        //    // Nếu thời gian đã qua hôm nay, chạy vào ngày mai
        //    if (nextRun < now)
        //        nextRun = nextRun.AddDays(1);

        //    TimeSpan initialDelay = nextRun - now;

        //    var timer = new System.Threading.Timer(RunTask, null, initialDelay, TimeSpan.FromDays(1));
        //    Console.WriteLine($"Tác vụ được lên lịch lần đầu vào: {nextRun}");
        //}

        //static void RunTask(object state)
        //{
        //    Console.WriteLine($"Chạy tác vụ vào: {DateTime.Now}");
        //    Form1.Report_(1,1,true,false,false,"");
        //}

    }
}
