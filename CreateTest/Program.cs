using gma.System.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;

namespace CreateTest
{
    internal class Program
    {
        private static UserActivityHook actHook;
        private static string path = @"C:\TGL\CleanCode\CleanCodelAutomationTool\Record\GlobalMacroRecorder\bin\Debug\RecordVideo\Test.txt";
        static void Main(string[] args)
        {
            //path = args[0];
            actHook = new UserActivityHook(); // crate an instance with global hooks
                                              // hang on events
                                              //actHook.OnMouseActivity += new MouseEventHandler(MouseMoved);
            actHook.KeyDown += new KeyEventHandler(MyKeyDown);
            actHook.Start();
            Console.WriteLine("Press any key to Stop...");
            Console.ReadKey();
            //actHook.KeyPress += new KeyPressEventHandler(MyKeyPress);
            //actHook.KeyUp += new KeyEventHandler(MyKeyUp);
            actHook.Stop();
        }
        public static void MyKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                //isBreak = true;
                File.WriteAllText(path, "true");
                //keyboardHook_cancel.Stop();
                return;
            }
            //LogWrite("KeyDown 	- " + e.KeyData.ToString());
        }
    }
}
