
using MouseKeyboardLibrary;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace GlobalMacroRecorder
{
    public partial class MacroForm : MetroFramework.Forms.MetroForm
    {

        List<MacroEvent> events = new List<MacroEvent>();
        int lastTimeRecorded = 0;

        MouseHook mouseHook = new MouseHook();
        KeyboardHook keyboardHook = new KeyboardHook();

        public MacroForm()
        {

            InitializeComponent();

            mouseHook.MouseMove += new MouseEventHandler(mouseHook_MouseMove);
            mouseHook.MouseDown += new MouseEventHandler(mouseHook_MouseDown);
            mouseHook.MouseUp += new MouseEventHandler(mouseHook_MouseUp);

            keyboardHook.KeyDown += new KeyEventHandler(keyboardHook_KeyDown);
            keyboardHook.KeyUp += new KeyEventHandler(keyboardHook_KeyUp);
            var pathfolder = System.Reflection.Assembly.GetExecutingAssembly().Location;

            var fileor = new FileInfo(pathfolder);
            pathfolder = fileor.Directory.ToString();
            _pathRecordVideo = pathfolder + "\\RecordVideo\\RecordVideo.exe"; 
        }

        void mouseHook_MouseMove(object sender, MouseEventArgs e)
        {

            events.Add(
                new MacroEvent(
                    MacroEventType.MouseMove,
                    e.X + "&&" + e.Y,
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }

        void mouseHook_MouseDown(object sender, MouseEventArgs e)
        {

            events.Add(
                new MacroEvent(
                    MacroEventType.MouseDown,
                    e.Button.ToString(),
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }

        void mouseHook_MouseUp(object sender, MouseEventArgs e)
        {

            events.Add(
                new MacroEvent(
                    MacroEventType.MouseUp,
                    e.Button.ToString(),
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }

        void keyboardHook_KeyDown(object sender, KeyEventArgs e)
        {

            events.Add(
                new MacroEvent(
                    MacroEventType.KeyDown,
                     e.KeyCode.ToString(),
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }

        void keyboardHook_KeyUp(object sender, KeyEventArgs e)
        {

            events.Add(
                new MacroEvent(
                    MacroEventType.KeyUp,
                    e.KeyCode.ToString(),
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }

        private void recordStartButton_Click(object sender, EventArgs e)
        {

            if (recordButton.Text == "Record")
            {
                events.Clear();
                lastTimeRecorded = Environment.TickCount;

                keyboardHook.Start();
                mouseHook.Start();
                recordButton.Text = "Stop";
                //timer1.Start();
             
            }
            else
            {

                keyboardHook.Stop();
                mouseHook.Stop();
                ExportJson();
                recordButton.Text = "Record";
                //timer1.Stop();
            }

        }

        public string duongdanfolder = @"C:\Step2";
        public void ExportJson()
        {
            if(!Directory.Exists(duongdanfolder))
            {
                Directory.CreateDirectory(duongdanfolder);
            }
            var vanbanKho = JsonConvert.SerializeObject(events);
            var datenow = DateTime.Now.ToString("yyyyMMddHHmm");
            pathCurrentFile = duongdanfolder + "\\" + datenow + ".mcr";
            File.WriteAllText(pathCurrentFile, vanbanKho);
        }

        private void playBackMacroButton_Click(object sender, EventArgs e)
        {
            //var vanban = File.ReadAllText(pathCurrentFile);
            //var docMacro = JsonConvert.DeserializeObject<List<MacroEvent>>(vanban);
            //this.events = docMacro;
            RunMacroandSaveVideo();
        }
        private string pathCurrentFile = "";
        private string _pathRecordVideo = "";
        private void RunMacroandSaveVideo()
        {
            //var datenow = DateTime.Now.ToString("ddMMyyyyHHmm");
            //var rec = new Recorder(new RecorderParams(duongdanfolder + "\\" + datenow + ".avi", 30, SharpAvi.KnownFourCCs.Codecs.MicrosoftMpeg4V3, 70));
            var fileName = Path.GetFileNameWithoutExtension(pathCurrentFile);
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = "\"" + _pathRecordVideo + "\"";
            startInfo.Arguments = "\"" + duongdanfolder + "\\" + fileName + ".avi" + "\"";
            startInfo.Verb = "runas";
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            var process = Process.Start(startInfo);

            foreach (MacroEvent macroEvent in events)
            {

                Thread.Sleep(macroEvent.TimeSinceLastEvent);

                switch (macroEvent.MacroEventType)
                {
                    case MacroEventType.MouseMove:
                        {

                            //MouseEventArgs mouseArgs = (MouseEventArgs)macroEvent.EventArgs;

                            var click = Regex.Split(macroEvent.EventArgs, "&&");

                            MouseSimulator.X = int.Parse(click[0]);
                            MouseSimulator.Y = int.Parse(click[1]);

                        }
                        break;
                    case MacroEventType.MouseDown:
                        {

                            //MouseEventArgs mouseArgs = macroEvent.EventArgs  as MouseEventArgs;
                            MouseButton button = (MouseButton)Enum.Parse(typeof(MouseButton), macroEvent.EventArgs);
                            MouseSimulator.MouseDown(button);

                        }
                        break;
                    case MacroEventType.MouseUp:
                        {

                            MouseButton button = (MouseButton)Enum.Parse(typeof(MouseButton), macroEvent.EventArgs);

                            MouseSimulator.MouseUp(button);

                        }
                        break;
                    case MacroEventType.KeyDown:
                        {

                            //KeyEventArgs keyArgs = (KeyEventArgs)macroEvent.EventArgs;
                            Keys keys = (Keys)Enum.Parse(typeof(Keys), macroEvent.EventArgs);
                            KeyboardSimulator.KeyDown(keys);

                        }
                        break;
                    case MacroEventType.KeyUp:
                        {

                            //KeyEventArgs keyArgs = (KeyEventArgs)macroEvent.EventArgs;
                            Keys keys = (Keys)Enum.Parse(typeof(Keys), macroEvent.EventArgs);
                            KeyboardSimulator.KeyUp(keys);

                        }
                        break;
                    default:
                        break;
                }

            }

            //vf.Save(duongdanfolder + "\\" + datenow + ".mp4");
            //rec.Dispose();
            process.Kill();
        }
        private void MacroForm_Load(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            var openFolder = new FolderBrowserDialog
            {
                ShowNewFolderButton = true,

            };
            if (openFolder.ShowDialog() == DialogResult.OK)
            {
                duongdanfolder = openFolder.SelectedPath;
                textBox1.Text = duongdanfolder;
            }

        }

        private void Run_click(object sender, EventArgs e)
        {
            if (Directory.Exists(duongdanfolder))
            {
                #region get all file

                DirectoryInfo d = new DirectoryInfo(duongdanfolder);//Assuming Test is your Folder

                FileInfo[] Files = d.GetFiles("*.mcr"); //Getting Text files
                var filess = Files.ToList();
                #endregion
                //OPEN FILE EXCEL
                FileInfo[] FilesExcel = d.GetFiles("*.xlsx"); //Getting Text files
                var filex = FilesExcel.ToList();
                var path = filex[0].FullName;
                FileInfo fileInfo = new FileInfo(path);

                ExcelPackage package = new ExcelPackage(fileInfo);
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                // get number of rows and columns in the sheet
                int rows = worksheet.Dimension.Rows; // 20
                int columns = worksheet.Dimension.Columns; // 7

                for (int i = 10; i <= rows; i++)
                {
                    try
                    {
                        var asembly = int.Parse(worksheet.Cells[i, 22].Value.ToString());

                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                foreach (var item in filess)
                {
                    var vanban = File.ReadAllText(item.FullName);
                    var docMacro = JsonConvert.DeserializeObject<List<MacroEvent>>(vanban);
                    this.events = docMacro;
                    RunMacroandSaveVideo();
                }

            }
            else
            {
                MessageBox.Show("Folder not exist");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
