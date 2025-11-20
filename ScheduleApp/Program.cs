using Microsoft.Win32.TaskScheduler;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace ScheduleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Scheduler đang chạy, sẽ thực thi file .exe mỗi ngày lúc 20:00...");

            while (true)
            {
                DateTime now = DateTime.Now;
                DateTime nextRun = DateTime.Today.AddHours(10); // 20:00 (8 PM)

                if (now > nextRun)
                {
                    nextRun = nextRun.AddDays(1); // Nếu đã qua 20:00 thì chờ đến ngày hôm sau
                }

                TimeSpan waitTime = nextRun - now;
                Console.WriteLine($"Chờ tới: {nextRun} (còn {waitTime})");

                await Task.Delay(waitTime);

                RunFixedExeAsync();
            }
        }

        static async Task RunFixedExeAsync()
        {
            string exePath = @"C:\TGL\CleanCode\CleanCodelAutomationTool\ExportIssue\bin\Debug\net7.0\ExportIssue.exe"; // Đường dẫn cố định tới file EXE

            try
            {
                Console.WriteLine($"Đang chạy: {exePath}");
                Process.Start(new ProcessStartInfo
                {
                    FileName = exePath,
                    Arguments = "1",
                    UseShellExecute = true // Quan trọng để mở file .exe (GUI hoặc Console)
                }); ; ;
                Console.WriteLine("Chạy thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi chạy file: " + ex.Message);
            }

        }


    }
}
