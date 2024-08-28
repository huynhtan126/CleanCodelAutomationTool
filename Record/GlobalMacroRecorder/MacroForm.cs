
using gma.System.Windows;
using MouseKeyboardLibrary;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace GlobalMacroRecorder
{
    public partial class MacroForm : MetroFramework.Forms.MetroForm
    {
        public string duongdanfolder = "C:\\Record";
        private string pathCurrentFile = "";
        private string pathTest = "";
        private string pathTestApp = "";
        private string AppTestName = "GlobalHookDemo.exe";
        private string AppRecordName = "RecordVideo.exe";
        private string _pathRecordVideo = "";
        private string _prefixContent = "Content=";
        private decimal _speed = 1;
        private bool _isDefaultSpeed = true;
        List<MacroEvent> events = new List<MacroEvent>();
        int lastTimeRecorded = 0;
        private List<ResItem> liststring = new List<ResItem>();
        MouseHook mouseHook = new MouseHook();
        KeyboardHook keyboardHook = new KeyboardHook();
        //KeyboardHook keyboardHook_cancel = new KeyboardHook();
        public static bool isBreak = false;
        public MacroForm()
        {

            InitializeComponent();
            mouseHook.MouseWheel += new MouseEventHandler(mouseHook_MouseWheel);
            mouseHook.MouseMove += new MouseEventHandler(mouseHook_MouseMove);
            mouseHook.MouseDown += new MouseEventHandler(mouseHook_MouseDown);
            mouseHook.MouseUp += new MouseEventHandler(mouseHook_MouseUp);

            keyboardHook.KeyDown += new KeyEventHandler(keyboardHook_KeyDown);
            keyboardHook.KeyUp += new KeyEventHandler(keyboardHook_KeyUp);
            //keyboardHook_cancel.KeyDown += new KeyEventHandler(keyboardHookCancel_KeyDown);
            //keyboardHook_cancel.KeyUp += new KeyEventHandler(keyboardHookCancel_KeyUp);
            var pathfolder = System.Reflection.Assembly.GetExecutingAssembly().Location;

            var fileor = new FileInfo(pathfolder);
            pathfolder = fileor.Directory.ToString();

            _pathRecordVideo = pathfolder + "\\RecordVideo\\" + AppRecordName;
            pathTestApp = pathfolder + "\\RecordVideo\\" + AppTestName;
            pathTest = pathfolder + "\\RecordVideo\\Test.txt";
            //m_AsyncWorker.WorkerReportsProgress = true;
            //m_AsyncWorker.WorkerSupportsCancellation = true;
            //m_AsyncWorker.ProgressChanged += new ProgressChangedEventHandler(bwAsync_ProgressChanged);
            //m_AsyncWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwAsync_RunWorkerCompleted);
            //m_AsyncWorker.DoWork += new DoWorkEventHandler(bwAsync_DoWork);
        }
        void mouseHook_MouseWheel(object sender, MouseEventArgs e)
        {

            events.Add(
                new MacroEvent(
                    MacroEventType.MouseWheel,
                    e.Delta.ToString(),
                    Environment.TickCount - lastTimeRecorded
                )); ;

            lastTimeRecorded = Environment.TickCount;

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
            //if (e.Button == MouseButtons.Middle) return;
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
            //if (e.Button == MouseButtons.Middle) return;
            events.Add(
                new MacroEvent(
                    MacroEventType.MouseUp,
                    e.Button.ToString(),
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }
        void keyboardHookCancel_KeyDown(object sender, KeyEventArgs e)
        {
            //if (/*e.Alt == true &&*/ e.KeyCode == Keys.F12)
            //{
            //    isBreak = true;
            //    keyboardHook_cancel.Stop();
            //    return;
            //}

        }
        void keyboardHook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F11)
            {
                events.Add(
                new MacroEvent(
                    MacroEventType.KeyDown,
                     e.KeyCode.ToString(),
                    Environment.TickCount - lastTimeRecorded
                ));

                lastTimeRecorded = Environment.TickCount;
                //MessageBox.Show("Đã chụp");

                lbl_Status.Text = "Đã chụp lúc " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ": " + DateTime.Now.Second;
                return;
            }
            if (e.KeyCode == Keys.F12)
            {
                //if(_process!=null)
                //    _process.Kill();
                keyboardHook.Stop();
                mouseHook.Stop();
                ExportJson();
                recordButton.Text = "Record";

                return;
            }
            events.Add(
                new MacroEvent(
                    MacroEventType.KeyDown,
                     e.KeyCode.ToString(),
                    Environment.TickCount - lastTimeRecorded
                ));

            lastTimeRecorded = Environment.TickCount;

        }
        void keyboardHookCancel_KeyUp(object sender, KeyEventArgs e)
        {
            //if (/*e.Alt == true &&*/ e.KeyCode == Keys.C)
            //{
            //    isBreak = true;
            //    keyboardHook_cancel.Stop();
            //    return;
            //}
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
        private Process _process;
        private void recordStartButton_Click(object sender, EventArgs e)
        {

            if (recordButton.Text == "Record")
            {
                MessageBox.Show("Tool works better in 1920x1080 and scale 100%");
                MessageBox.Show("To Break record : F12");
                //this.WindowState = FormWindowState.Minimized;
                //if (Directory.Exists(duongdanfolder))
                //{
                //    var startInfo = new ProcessStartInfo();
                //    startInfo.FileName = "\"" + pathTestApp + "\"";
                //    startInfo.Arguments = "\"" + pathTest + "\"";
                //    startInfo.Verb = "runas";
                //    startInfo.UseShellExecute = false;
                //    startInfo.CreateNoWindow = true;
                //    _process = Process.Start(startInfo);

                events.Clear();
                lastTimeRecorded = Environment.TickCount;

                keyboardHook.Start();
                mouseHook.Start();
                recordButton.Text = "F12 to Stop";
                //}
            }
            else
            {

                keyboardHook.Stop();
                mouseHook.Stop();
                ExportJson();
                recordButton.Text = "Record";

            }

        }

        public void ExportJson()
        {
            if (!Directory.Exists(duongdanfolder))
            {
                Directory.CreateDirectory(duongdanfolder);
            }
            var vanbanKho = JsonConvert.SerializeObject(events);
            var datenow = DateTime.Now.ToString("yyyyMMddHHmm");
            pathCurrentFile = duongdanfolder + "\\" + datenow + ".mcr";
            File.WriteAllText(pathCurrentFile, vanbanKho);
            Process.Start(duongdanfolder);
        }

        private void playBackMacroButton_Click(object sender, EventArgs e)
        {
            InitialSetup();

            var startInfo = new ProcessStartInfo();
            startInfo.FileName = "\"" + pathTestApp + "\"";
            startInfo.Arguments = "\"" + pathTest + "\"";
            startInfo.Verb = "runas";
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            var process = Process.Start(startInfo);

            RunMacroandSaveVideo();
            if (File.Exists(pathTest))
            {
                File.Delete(pathTest);

                WindowState = FormWindowState.Normal;
                lbl_Status.Text = "Macro is cancel.";
                process.Kill();
                return;
            }
            process.Kill();
            WindowState = FormWindowState.Normal;
            lbl_Status.Text = "Macro is done.";

        }

        private void RunMacroandSaveVideo(bool check = true)
        {
            if (dataGridView2.SelectedRows.Count > 0 && check)
            {
                for (int k = dataGridView2.SelectedRows.Count - 1; k >= 0; k--)
                {
                    pathCurrentFile = duongdanfolder + "\\" + dataGridView2.SelectedRows[k].Cells[0].Value.ToString();
                    if (!File.Exists(pathCurrentFile))
                    {
                        if (check)
                        {
                            lbl_Status.Text = "File not exist";

                        }
                        return;
                    }

                    //newWindowThread.SetApartmentState(ApartmentState.STA);
                    //newWindowThread.IsBackground = true;
                    //newWindowThread.Start();

                    var vanban = File.ReadAllText(pathCurrentFile);
                    var docMacro = JsonConvert.DeserializeObject<List<MacroEvent>>(vanban);
                    this.events = docMacro;
                    var fileName = Path.GetFileNameWithoutExtension(pathCurrentFile);
                    Directory.CreateDirectory(duongdanfolder + "\\TestResult");
                    Process process = null;
                    if (cb_exportAV.Checked)
                    {
                        var startInfo = new ProcessStartInfo();
                        startInfo.FileName = "\"" + _pathRecordVideo + "\"";
                        startInfo.Arguments = "\"" + duongdanfolder + "\\TestResult\\" + fileName + ".avi" + "\"";
                        startInfo.Verb = "runas";
                        startInfo.UseShellExecute = false;
                        startInfo.CreateNoWindow = true;
                        process = Process.Start(startInfo);
                    }

                    List<MacroEvent> doneEvents = new List<MacroEvent>();
                    int eventIndex = -1;
                    foreach (MacroEvent macroEvent in events)
                    {
                        //isBreak =bool.Parse(File.ReadAllText(pathTest));
                        //if (isBreak == true)
                        //{
                        //    process.Kill();
                        //    return;
                        //}
                        eventIndex++;
                        if (File.Exists(pathTest))
                        {
                            if (!cb_Manual_Split.Checked)
                            {
                                File.Delete(pathTest);
                                lbl_Status.Text = "Macro is cancel.";
                            }
                            else
                            {
                                //split current file/
                                ExportJsonSelectedEvents(doneEvents, fileName + "_Done");

                                List<MacroEvent> remainEvents = new List<MacroEvent>();
                                for (int i = eventIndex; i < events.Count; i++)
                                {
                                    remainEvents.Add(events[i]);
                                }
                                ExportJsonSelectedEvents(remainEvents, fileName + "_Remain");
                            }
                            if (cb_exportAV.Checked)
                            {
                                process.Kill();
                            }
                            return;
                        }
                        if (_isDefaultSpeed)
                        {
                            _speed = numericUpDown1.Value;

                        }
                        var sleepTime = Math.Round(macroEvent.TimeSinceLastEvent / _speed);
                        Thread.Sleep(int.Parse(sleepTime.ToString()));

                        switch (macroEvent.MacroEventType)
                        {
                            case MacroEventType.MouseWheel:
                                {
                                    var wheel = int.Parse(macroEvent.EventArgs);
                                    MouseSimulator.MouseWheel(wheel);
                                    break;
                                }

                            case MacroEventType.MouseMove:
                                {

                                    var click = Regex.Split(macroEvent.EventArgs, "&&");

                                    MouseSimulator.X = int.Parse(click[0]);
                                    MouseSimulator.Y = int.Parse(click[1]);

                                }
                                break;
                            case MacroEventType.MouseDown:
                                {
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
                                    Keys keys = (Keys)Enum.Parse(typeof(Keys), macroEvent.EventArgs);
                                    if (keys == Keys.F11)
                                    {

                                        _bitmaps.Add(ScreenShotUtils.CaptureActiveWindow());
                                    }
                                    KeyboardSimulator.KeyDown(keys);

                                }
                                break;
                            case MacroEventType.KeyUp:
                                {
                                    Keys keys = (Keys)Enum.Parse(typeof(Keys), macroEvent.EventArgs);
                                    KeyboardSimulator.KeyUp(keys);

                                }
                                break;
                            default:
                                break;
                        }
                        doneEvents.Add(macroEvent);
                    }
                    if (cb_exportAV.Checked)
                    {
                        process.Kill();
                    }
                }
            }
            else
            {
                if (!File.Exists(pathCurrentFile))
                {
                    if (check)
                    {
                        lbl_Status.Text = "File not exist";

                    }
                    return;
                }

                //newWindowThread.SetApartmentState(ApartmentState.STA);
                //newWindowThread.IsBackground = true;
                //newWindowThread.Start();

                var vanban = File.ReadAllText(pathCurrentFile);
                var docMacro = JsonConvert.DeserializeObject<List<MacroEvent>>(vanban);
                this.events = docMacro;
                var fileName = Path.GetFileNameWithoutExtension(pathCurrentFile);
                Directory.CreateDirectory(duongdanfolder + "\\TestResult");
                var startInfo = new ProcessStartInfo();
                startInfo.FileName = "\"" + _pathRecordVideo + "\"";
                startInfo.Arguments = "\"" + duongdanfolder + "\\TestResult\\" + fileName + ".avi" + "\"";
                startInfo.Verb = "runas";
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                var process = Process.Start(startInfo);
                List<MacroEvent> doneEvents = new List<MacroEvent>();
                int eventIndex = -1;
                foreach (MacroEvent macroEvent in events)
                {
                    //isBreak =bool.Parse(File.ReadAllText(pathTest));
                    //if (isBreak == true)
                    //{
                    //    process.Kill();
                    //    return;
                    //}
                    eventIndex++;
                    if (File.Exists(pathTest))
                    {

                        if (!cb_Manual_Split.Checked)
                        {
                            File.Delete(pathTest);
                            lbl_Status.Text = "Macro is cancel.";
                        }
                        else
                        {
                            //split current file/
                            ExportJsonSelectedEvents(doneEvents, fileName + "_Done");

                            List<MacroEvent> remainEvents = new List<MacroEvent>();
                            for (int i = eventIndex; i < events.Count; i++)
                            {
                                remainEvents.Add(events[i]);
                            }
                            ExportJsonSelectedEvents(remainEvents, fileName + "_Remain");
                        }
                        process.Kill();
                        return;
                    }
                    _speed = numericUpDown1.Value;
                    var sleepTime = Math.Round(macroEvent.TimeSinceLastEvent / _speed);
                    Thread.Sleep(int.Parse(sleepTime.ToString()));

                    switch (macroEvent.MacroEventType)
                    {
                        case MacroEventType.MouseWheel:
                            {
                                var wheel = int.Parse(macroEvent.EventArgs);
                                MouseSimulator.MouseWheel(wheel);
                                break;
                            }

                        case MacroEventType.MouseMove:
                            {

                                var click = Regex.Split(macroEvent.EventArgs, "&&");

                                MouseSimulator.X = int.Parse(click[0]);
                                MouseSimulator.Y = int.Parse(click[1]);

                            }
                            break;
                        case MacroEventType.MouseDown:
                            {
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
                                Keys keys = (Keys)Enum.Parse(typeof(Keys), macroEvent.EventArgs);
                                if (keys == Keys.F11)
                                {

                                    _bitmaps.Add(ScreenShotUtils.CaptureActiveWindow());
                                }
                                KeyboardSimulator.KeyDown(keys);

                            }
                            break;
                        case MacroEventType.KeyUp:
                            {
                                Keys keys = (Keys)Enum.Parse(typeof(Keys), macroEvent.EventArgs);
                                KeyboardSimulator.KeyUp(keys);

                            }
                            break;
                        default:
                            break;
                    }
                    doneEvents.Add(macroEvent);
                }
                process.Kill();
            }

        }
        public Bitmap GetScreenSnapshot()
        {
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);

            return bitmap;
        }
        private UserActivityHook actHook;
        private void MacroForm_Load(object sender, EventArgs e)
        {
            formula.SelectedIndex = 0;
            UpdateDataFileMRC();
            //actHook = new UserActivityHook(); // crate an instance with global hooks
            //                                  // hang on events
            ////actHook.OnMouseActivity += new MouseEventHandler(MouseMoved);
            //actHook.KeyDown += new KeyEventHandler(MyKeyDown);
            //actHook.KeyPress += new KeyPressEventHandler(MyKeyPress);
            //actHook.KeyUp += new KeyEventHandler(MyKeyUp);

        }

        public void MyKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                isBreak = true;

                //keyboardHook_cancel.Stop();
                return;
            }
            //LogWrite("KeyDown 	- " + e.KeyData.ToString());
        }

        public void MyKeyPress(object sender, KeyPressEventArgs e)
        {
            //LogWrite("KeyPress 	- " + e.KeyChar);
        }

        public void MyKeyUp(object sender, KeyEventArgs e)
        {
            //LogWrite("KeyUp 		- " + e.KeyData.ToString());
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
                PathfileorFolder.Text = duongdanfolder;
            }

        }
        private List<Bitmap> _bitmaps = new List<Bitmap>();
        private void InitialSetup()
        {
            #region Initial setup
            //var listProcess = Process.GetProcessesByName(_pathRecordVideo);
            var listProcess = Process.GetProcessesByName(AppTestName);
            foreach (var item in listProcess)
            {
                item.Kill();
            }

            var listProcessTest = Process.GetProcessesByName(AppRecordName);
            foreach (var item in listProcessTest)
            {
                item.Kill();
            }
            if (File.Exists(pathTest))
            {
                File.Delete(pathTest);
            }
            //MessageBox.Show("Trở về trạng thái ban đầu. OK?");
            MessageBox.Show("Tool works better in 1920x1080 and scale 100%");
            MessageBox.Show("To Break automation : F12");

            this.WindowState = FormWindowState.Minimized;
            #endregion
        }
        private void Run_click(object sender, EventArgs e)
        {
            InitialSetup();

            if (Directory.Exists(duongdanfolder))
            {
                var startInfo = new ProcessStartInfo();
                startInfo.FileName = "\"" + pathTestApp + "\"";
                startInfo.Arguments = "\"" + pathTest + "\"";
                startInfo.Verb = "runas";
                startInfo.UseShellExecute = false;
                startInfo.CreateNoWindow = true;
                var process = Process.Start(startInfo);

                #region get all file

                DirectoryInfo d = new DirectoryInfo(duongdanfolder);

                //FileInfo[] Files = d.GetFiles("*.mcr"); 
                //var filess = Files.ToList();
                #endregion

                FileInfo[] FilesExcel = d.GetFiles("*.xlsx");
                var filex = FilesExcel.ToList();
                var path = filex[0].FullName;
                FileInfo fileInfo = new FileInfo(path);

                ExcelPackage package = new ExcelPackage(fileInfo);
                var listErrorCase = new List<string>();
                var index = 0;
                var listValid = new List<int>();
                var validSheet = "";
                if (CheckFromTo.Checked)
                {
                    validSheet = txSheetName.Text;
                    var from = int.Parse(FromEx.Value.ToString());
                    var to = int.Parse(ToEx.Value.ToString());
                    for (int i = from; i < to + 1; i++)
                    {
                        listValid.Add(i);
                    }
                }
                foreach (var sheet in package.Workbook.Worksheets)
                {
                    index++;
                    if (validSheet != "" && sheet.Name != validSheet) continue;
                    if (index == 1) continue;
                    ExcelWorksheet worksheet = sheet;

                    int rows = worksheet.Dimension.Rows;
                    int columns = worksheet.Dimension.Columns;
                    //keyboardHook_cancel.Start();
                    var pics = worksheet.Drawings;
                    var listPic = new List<ExcelPicture>();
                    foreach (ExcelPicture item in pics)
                    {
                        listPic.Add(item);
                    }

                    for (int i = 12; i <= rows; i++)
                    {
                        try
                        {
                            if (listValid.Count != 0)
                            {
                                if (!listValid.Contains(i))
                                {
                                    continue;
                                }
                            }
                            var fileName = worksheet.Cells[i, 1].Text.Trim();
                            if (fileName == "") continue;
                            _isDefaultSpeed = true;
                            if (fileName.StartsWith("$"))
                            {
                                _isDefaultSpeed = false;
                                decimal.TryParse(fileName.Substring(1, 1), out _speed);
                                fileName = fileName.Substring(2);
                            }
                            pathCurrentFile = duongdanfolder + "\\" + sheet.Name + "\\" + fileName + ".mcr";
                            lbl_Status.Text = "Excute test : " + sheet.Name + " \\ " + fileName;
                            RunMacroandSaveVideo(false);

                            if (File.Exists(pathTest))
                            {
                                File.Delete(pathTest);
                                this.WindowState = FormWindowState.Normal;
                                lbl_Status.Text = "Macro is cancel.";
                                process.Kill();
                                return;
                            }

                            //var screeenshot = GetScreenSnapshot();   
                            var screeenshot = _bitmaps.LastOrDefault();
                            if (worksheet.Cells[i, 10].Text.ToString().Trim() == string.Empty && File.Exists(pathCurrentFile))
                            {
                                worksheet.Cells[i, 10].Formula = "=HYPERLINK(" + @"""" + fileName + ".avi" + @"""" + "," + @"""Evidence :" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + @"""" + ")";
                                if (screeenshot != null)
                                {
                                    var cellH = int.Parse(Math.Round(worksheet.Row(i).Height * 1.2).ToString());
                                    var cellW = int.Parse(Math.Round(worksheet.Column(9).Width * 7).ToString());
                                    //var picture = worksheet.Drawings.AddPicture(Guid.NewGuid().ToString(), screeenshot);

                                    var pic = listPic.Where(x => x.From.Row == i - 1 && x.From.Column == 7).ToList();
                                    if (pic.Count == 1)
                                    {
                                        var kq = ImageUtils.CompareMemCmp(pic[0].Image as Bitmap, screeenshot);
                                        if (kq)
                                        {
                                            worksheet.Cells[i, 11].Value = "OK";
                                        }
                                        else
                                        {
                                            worksheet.Cells[i, 11].Value = "NG";

                                        }
                                    }
                                    var picture = worksheet.Drawings.AddPicture(Guid.NewGuid().ToString(), screeenshot);
                                    //var picture = worksheet.Drawings.AddPicture("Teo1", screeenshot);
                                    picture.SetSize(cellW, cellH);
                                    //picture.SetSize(400, 400);
                                    picture.SetPosition(i - 1, 0, 8, 0);
                                }
                                //else if (worksheet.Cells[i, 12].Text.ToString().Trim() == string.Empty)
                                //{
                                //    worksheet.Cells[i, 12].Formula = "=HYPERLINK(" + @"""" + fileName + ".avi" + @"""" + "," + @"""Evidence :" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + @"""" + ")";
                                //    if (screeenshot != null)
                                //    {
                                //        var cellH = int.Parse(Math.Round(worksheet.Row(i).Height * 1.2).ToString());
                                //        var cellW = int.Parse(Math.Round(worksheet.Column(9).Width * 7).ToString());
                                //        var picture = worksheet.Drawings.AddPicture(Guid.NewGuid().ToString(), screeenshot);
                                //        picture.SetSize(cellW, cellH);
                                //        picture.SetPosition(i - 1, 0, 8, 0);

                                //        var pic = listPic.Where(x => x.From.Row == i - 1 && x.From.Column == 7).ToList();
                                //        if (pic.Count == 1)
                                //        {
                                //            var kq = ImageUtils.CompareMemCmp(pic[0].Image as Bitmap, screeenshot);
                                //            if (kq)
                                //            {
                                //                worksheet.Cells[i, 11].Value = "OK";
                                //            }
                                //            else
                                //            {
                                //                worksheet.Cells[i, 11].Value = "NG";

                                //            }
                                //        }
                                //    }
                                //}
                                //else if (worksheet.Cells[i, 14].Text.ToString().Trim() == string.Empty)
                                //{
                                //    worksheet.Cells[i, 14].Formula = "=HYPERLINK(" + @"""" + fileName + ".avi" + @"""" + "," + @"""Evidence :" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + @"""" + ")";
                                //    if (screeenshot != null)
                                //    {
                                //        var picture = worksheet.Drawings.AddPicture(Guid.NewGuid().ToString(), screeenshot);
                                //        picture.SetSize(400, 250);
                                //        picture.SetPosition((i - 1), 100, 13, 0);

                                //        var pic = listPic.Where(x => x.From.Row == i - 1 && x.From.Column == 6).ToList();
                                //        if (pic.Count == 1)
                                //        {
                                //            var kq = ImageUtils.CompareMemCmp(pic[0].Image as Bitmap, screeenshot);
                                //            if (kq)
                                //            {
                                //                worksheet.Cells[i, 15].Value = "OK";
                                //            }
                                //            else
                                //            {
                                //                worksheet.Cells[i, 15].Value = "NG";
                                //            }
                                //        }
                                //    }
                                //}
                            }
                        }
                        catch (Exception ex)
                        {
                            listErrorCase.Add("Sheet name: " + worksheet.Name + " Error at row :" + i + " " + ex.Message);
                            continue;
                        }
                    }
                }
                var content = "";
                foreach (var item in listErrorCase)
                {
                    content = content + item;
                }
                if (content != "")
                {
                    MessageBox.Show(content);
                }

                var name = DateTime.Now.ToString("yyyyMMddHHmm");
                Directory.CreateDirectory(duongdanfolder + "\\TestResult");
                package.SaveAs(new FileInfo(duongdanfolder + "\\TestResult\\" + name + ".xlsx"));
                Process.Start(duongdanfolder + "\\TestResult\\" + name + ".xlsx");
                //keyboardHook_cancel.Stop();

                //actHook.Stop();
                process.Kill();
            }
            else
            {
                MessageBox.Show("Folder not exist");
            }
            this.WindowState = FormWindowState.Normal;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            duongdanfolder = PathfileorFolder.Text;
        }
        private void GetDataGridtoListView()
        {
            liststring = new List<ResItem>();
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if (item.Cells[0].Value == null || item.Cells[1].Value == null || item.Cells[2].Value == null)
                {
                    continue;
                }
                var res = new ResItem()
                {
                    StringContent = item.Cells[0].Value.ToString(),
                    NameinResx = item.Cells[1].Value.ToString(),
                    NameInSource = item.Cells[2].Value.ToString(),
                };
                liststring.Add(res);

            }
        }
        private void ShowDataGrid()
        {
            dataGridView1.Rows.Clear();
            foreach (var item in liststring)
            {
                var row = new DataGridViewRow();
                var row1 = dataGridView1.Rows.Add(row);
                dataGridView1.Rows[row1].Cells[0].Value = item.StringContent;
                dataGridView1.Rows[row1].Cells[1].Value = item.NameinResx;
                dataGridView1.Rows[row1].Cells[2].Value = item.NameInSource;
            }
        }
        private void ApplySource_Click(object sender, EventArgs e)
        {

            if (ResourceName.Text == string.Empty)
            {
                MessageBox.Show("Please fill Name Command");
                return;
            }
            if (PathfileorFolder.Text == string.Empty)
            {
                MessageBox.Show("Please fill path");
                return;
            }
            GetDataGridtoListView();
            var filess = GetFiles(TypeFile.all);
            foreach (FileInfo file in filess)
            {

                string vanban = "";

                foreach (var item in liststring)
                {
                    try
                    {
                        using (StreamReader reader = new StreamReader(file.FullName))
                        {
                            vanban = reader.ReadToEnd();

                            vanban = vanban.Replace(item.StringContent, item.NameInSource);
                        }
                        using (StreamWriter writer = new StreamWriter(file.FullName))
                        {
                            writer.Write(vanban);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show(ex.ToString());
                    }
                }

            }
            MessageBox.Show(string.Format("Apply {0} files succesful", filess.Count()));
        }

        private void Clipboard_Click(object sender, EventArgs e)
        {

            try
            {

                GetDataGridtoListView();
                var content = "";
                foreach (var item in liststring)
                {
                    var value = item.StringContent.Replace("\"", "");
                    value = value.Replace(_prefixContent, "");

                    content = content + item.NameinResx + "\t" + value + "\r\n";
                }
                Clipboard.SetText(content);
                MessageBox.Show("Set to clipboard successful.");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }

        private void removeComment_Click(object sender, EventArgs e)
        {
            if (ResourceName.Text == string.Empty)
            {
                MessageBox.Show("Please fill path");
                return;
            }
            var typefile = TypeFile.all;
            if (cs_cpp.Checked)
            {
                typefile = TypeFile.cs_cpp_h;
            }
            if (xaml.Checked)
            {
                typefile = TypeFile.xaml;
            }

            var filess = GetFiles(typefile);
            foreach (FileInfo file in filess)
            {
                try
                {
                    string vanban;
                    using (StreamReader reader = new StreamReader(file.FullName))
                    {
                        vanban = reader.ReadToEnd();
                        var blockComments = @"/\*(.*?)\*/";
                        var lineComments = @"//(.*?)\r?\n";
                        var strings = @"""((\\[^\n]|[^""\n])*)""";
                        var verbatimStrings = @"@(""[^""]*"")+";
                        vanban = Regex.Replace(vanban,
                        blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings,
                        me =>
                        {
                            if (todo.Checked && me.Value.StartsWith("//TODO"))
                            {
                            }
                            else if (note.Checked && me.Value.StartsWith("//Note"))
                            {
                            }
                            else if (sum.Checked && me.Value.StartsWith("///"))
                            {
                            }
                            else
                            {

                                if (me.Value.StartsWith("/*") || me.Value.StartsWith("//"))
                                    return me.Value.StartsWith("//") ? Environment.NewLine : "";
                            }

                            return me.Value;
                        },
                        RegexOptions.Singleline);
                    }
                    using (StreamWriter writer = new StreamWriter(file.FullName))
                    {
                        writer.Write(vanban);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString());
                }
            }
            MessageBox.Show(string.Format("Clean code {0} files succesful", filess.Count()));
        }

        public enum TypeFile
        {
            cs_cpp_h,
            xaml,
            all,
        }
        public FileInfo[] GetFiles(TypeFile typeFile)
        {
            try
            {
                if (File.Exists(PathfileorFolder.Text))
                {
                    FileInfo[] listFile = new FileInfo[] { new FileInfo(PathfileorFolder.Text) };
                    return listFile;
                }
                else
                {
                    var pathfolder = PathfileorFolder.Text;
                    DirectoryInfo d = new DirectoryInfo(pathfolder);
                    var allfile = d.GetFiles("*.*", SearchOption.AllDirectories).ToList();
                    switch (typeFile)
                    {
                        case TypeFile.cs_cpp_h:
                            return allfile.Where(s => s.Name.EndsWith(".cs", StringComparison.OrdinalIgnoreCase)
                || s.Name.EndsWith(".cpp", StringComparison.OrdinalIgnoreCase)
                || s.Name.EndsWith(".h", StringComparison.OrdinalIgnoreCase)
                ).ToArray();

                        case TypeFile.xaml:
                            return allfile.Where(s => s.Name.EndsWith(".xaml", StringComparison.OrdinalIgnoreCase)

            ).ToArray();

                        case TypeFile.all:
                            return allfile.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            FileInfo[] listFile1 = new FileInfo[] { };
            return listFile1;
        }

        private void getContentUI_Click(object sender, EventArgs e)
        {

            if (ResourceName.Text == string.Empty)
            {
                MessageBox.Show("Please fill Name Command");
                return;
            }
            if (PathfileorFolder.Text == string.Empty)
            {
                MessageBox.Show("Please fill path");
                return;
            }

            liststring = new List<ResItem>();
            var filess = GetFiles(TypeFile.xaml);
            foreach (FileInfo file in filess)
            {
                var filePath = file.FullName;
                string fileContent = File.ReadAllText(filePath);
                var pattern = _prefixContent + @"""(.*?)""";
                MatchCollection matches = Regex.Matches(fileContent, pattern);
                string modifiedContent = fileContent;
                foreach (Match match in matches)
                {
                    try
                    {
                        var replacementString = match.Groups[0].Value.Replace(_prefixContent + @"""", string.Empty);
                        replacementString = replacementString.Replace(@" ", string.Empty);
                        replacementString = Regex.Replace(replacementString, "[^a-zA-Z0-9]", "");
                        var rex = new ResItem { NameInSource = _prefixContent + "\"{" + string.Format(formula.Text, ResourceName.Text + replacementString) + "}\"", StringContent = match.Groups[0].Value, NameinResx = replacementString };
                        if (!liststring.Any(x => x.StringContent == rex.StringContent))
                        {
                            liststring.Add(rex);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            MessageBox.Show(string.Format("Get string {0} files succesful", filess.Count()));
            ShowDataGrid();
        }

        private void Gettextfrom_Click(object sender, EventArgs e)
        {
            if (ResourceName.Text == string.Empty)
            {
                MessageBox.Show("Please fill Name Command");
                return;
            }
            if (PathfileorFolder.Text == string.Empty)
            {
                MessageBox.Show("Please fill path");
                return;
            }

            liststring = new List<ResItem>();
            var filess = GetFiles(TypeFile.all);
            foreach (FileInfo file in filess)
            {
                var filePath = file.FullName;
                string fileContent = File.ReadAllText(filePath);
                var pattern = @"""(.*?)""";
                MatchCollection matches = Regex.Matches(fileContent, pattern);
                string modifiedContent = fileContent;
                foreach (Match match in matches)
                {
                    try
                    {
                        var replacementString = match.Groups[0].Value.Replace(@"""", string.Empty);
                        replacementString = replacementString.Replace(@" ", string.Empty);
                        replacementString = Regex.Replace(replacementString, "[^a-zA-Z0-9]", "");
                        replacementString = "_" + replacementString;
                        var rex = new ResItem { NameInSource = string.Format(formula.Text, ResourceName.Text + replacementString), StringContent = match.Groups[0].Value, NameinResx = replacementString };
                        if (!liststring.Any(x => x.StringContent == rex.StringContent))
                        {
                            liststring.Add(rex);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            MessageBox.Show(string.Format("Get string {0} files succesful", filess.Count()));
            ShowDataGrid();
        }

        private void RemoveDouble_Click(object sender, EventArgs e)
        {
            if (ResourceName.Text == string.Empty)
            {
                MessageBox.Show("Please fill path");
                return;
            }
            var typefile = TypeFile.all;
            if (cs_cpp.Checked)
            {
                typefile = TypeFile.cs_cpp_h;
            }
            if (xaml.Checked)
            {
                typefile = TypeFile.xaml;
            }
            var filess = GetFiles(typefile);
            foreach (FileInfo file in filess)
            {
                try
                {
                    string vanban;
                    using (StreamReader reader = new StreamReader(file.FullName))
                    {
                        vanban = reader.ReadToEnd();

                        var emptyline = @"^(?([^\r\n])\s)*\r?\n(?([^\r\n])\s)*\r?\n";
                        vanban = Regex.Replace(vanban, emptyline, string.Empty, RegexOptions.Multiline);
                    }
                    using (StreamWriter writer = new StreamWriter(file.FullName))
                    {
                        writer.Write(vanban);
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString());
                }
            }
            MessageBox.Show(string.Format("Clean code {0} files succesful", filess.Count()));
        }

        private void MacroForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            keyboardHook.Stop();
            //keyboardHook_cancel.Stop();
            mouseHook.Stop();
        }

        private void metroButton1_Click_1(object sender, EventArgs e)
        {
            //Rectangle bounds = this.Bounds;
            //using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            //{
            //    using (Graphics g = Graphics.FromImage(bitmap))
            //    {
            //        g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            //    }
            var bitmap = ScreenShotUtils.CaptureActiveWindow();
            bitmap.Save(@"C:\Users\huynh\Downloads\Active.jpg", ImageFormat.Jpeg);
            MessageBox.Show("save ok");
            //}

        }
        //compare
        private void metroButton4_Click(object sender, EventArgs e)
        {
            var bitmap = Bitmap.FromFile(@"C:\Users\huynh\Downloads\Active.jpg") as Bitmap;
            var bitmap1 = Bitmap.FromFile(@"C:\Users\huynh\Downloads\Active2.jpg") as Bitmap;
            var kq = ImageUtils.CompareMemCmp(bitmap, bitmap1);
            MessageBox.Show(kq.ToString());
            //using (Comparer comparer = new Comparer("filepath/soureImage.jpg"))
            //{
            //    CompareOptions options = new CompareOptions();
            //    options.GenerateSummaryPage = false; // To get the difference summary, set it 'true'

            //    comparer.Add("filepath/targetImage.jpg");
            //    comparer.Compare("filepath/comparisonResultImage.jpg", options);
            //}
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UpdateDataFileMRC()
        {
            #region get all file
            if (!Directory.Exists(duongdanfolder))
            {
                Directory.CreateDirectory(duongdanfolder);
            }
            DirectoryInfo d = new DirectoryInfo(duongdanfolder);//Assuming Test is your Folder

            FileInfo[] Files = d.GetFiles("*.mcr"); //Getting Text files
            var filess = Files.ToList();
            #endregion
            dataGridView2.Rows.Clear();
            foreach (var item in filess)
            {
                var row = new DataGridViewRow();
                var row1 = dataGridView2.Rows.Add(row);
                dataGridView2.Rows[row1].Cells[0].Value = item.Name;
            }

        }

        private void metroButton1_Click_2(object sender, EventArgs e)
        {
            UpdateDataFileMRC();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void CheckFromTo_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckFromTo.Checked)
            {
                GBexcel.Enabled = true;
            }
            else
            {
                GBexcel.Enabled = false;
            }

        }

        private void txSheetName_TextChanged(object sender, EventArgs e)
        {

        }
        public void ExportJsonSelectedEvents(List<MacroEvent> listEvent, string nameSuffix)
        {
            if (!Directory.Exists(duongdanfolder))
            {
                Directory.CreateDirectory(duongdanfolder);
            }
            var vanbanKho = JsonConvert.SerializeObject(listEvent);
            var datenow = DateTime.Now.ToString("yyyyMMddHHmm");
            pathCurrentFile = duongdanfolder + "\\" + datenow + "_" + nameSuffix + ".mcr";
            File.WriteAllText(pathCurrentFile, vanbanKho);
            Process.Start(duongdanfolder);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void JoinButton_Click(object sender, EventArgs e)
        {
            string retContent = string.Empty;
            for (int i = dataGridView2.SelectedRows.Count - 1; i >= 0; i--)
            {
                string mcrFile = duongdanfolder + "\\" + dataGridView2.SelectedRows[i].Cells[0].Value.ToString();
                if (File.Exists(mcrFile))
                {
                    string contentFile = File.ReadAllText(mcrFile);
                    if (retContent.Equals(string.Empty))
                    {
                        retContent = contentFile;
                    }
                    else
                    {
                        string firstContent = retContent.Substring(0, retContent.Length - 1);
                        string secondContent = contentFile.Substring(1, contentFile.Length - 1);

                        retContent = firstContent + "," + secondContent;
                    }
                }
            }

            var datenow = DateTime.Now.ToString("yyyyMMddHHmmss");
            pathCurrentFile = duongdanfolder + "\\" + datenow + "_Joined.mcr";
            File.WriteAllText(pathCurrentFile, retContent);
            Process.Start(duongdanfolder);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        //Check mapping 
        private void metroButton4_Click_1(object sender, EventArgs e)
        {
            #region Xuat hien hop thoai mo file

            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                //InitialDirectory = @"E:\",
                Title = "Browse Files",
                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "xlsx",
                Filter = "Excel files (*.xlsx)|*.xlsx",
                FilterIndex = 2,
                RestoreDirectory = true,

                //ReadOnlyChecked = true
                //ShowReadOnly = true
            };
            #endregion

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var thongtinfile = openFileDialog1.FileName;
                ExcelPackage packageMappping = new ExcelPackage(new FileInfo(thongtinfile));

                #region Xuat hien hop thoai mo file

                OpenFileDialog openFileDialog2 = new OpenFileDialog
                {
                    //InitialDirectory = @"E:\",
                    Title = "Browse Files",
                    CheckFileExists = true,
                    CheckPathExists = true,

                    DefaultExt = "xlsx",
                    Filter = "Excel files (*.xlsx)|*.xlsx",
                    FilterIndex = 2,
                    RestoreDirectory = true,

                    //ReadOnlyChecked = true
                    //ShowReadOnly = true
                };
                #endregion
                if (openFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    var thongtinfile1 = new FileInfo(openFileDialog2.FileName);
                    ExcelPackage packageReal4 = new ExcelPackage(thongtinfile1);
                    // loop through the worksheet rows and columns
                    var path = thongtinfile1.Directory + "\\Result";
                    Directory.CreateDirectory(path);
                    var sheetReal4 = packageReal4.Workbook.Worksheets[1];
                    var worksheetMapping = packageMappping.Workbook.Worksheets[1];

                    foreach (var sheetMapping in packageMappping.Workbook.Worksheets)
                    {
                        if (sheetMapping.Name == sheetReal4.Name)
                        {
                            worksheetMapping = sheetMapping;
                        }
                    }

                    //foreach (var sheetReal4 in sheetReal4Col)
                    {
                        //if (sheetReal4.Name.StartsWith("Tsugite") || sheetReal4.Name.StartsWith(("Be-su")))
                        {
                            int rowsReal4 = sheetReal4.Dimension.Rows; // 20
                            int columnsReal4 = sheetReal4.Dimension.Columns; // 7
                            //foreach (var worksheetMapping in worksheets)
                            {              // get number of rows and columns in the sheet
                                int rows = worksheetMapping.Dimension.Rows; // 20
                                int columns = worksheetMapping.Dimension.Columns; // 7
                                //if (worksheetMapping.Name.StartsWith("Tsugite") || worksheetMapping.Name.StartsWith(("Be-su")))

                                for (int l = 2; l <= rowsReal4; l++)
                                {
                                    worksheetMapping.Name = worksheetMapping.Name.Split('_')[0];
                                    var nameConnection = sheetReal4.Cells[l, 1].Text;
                                    var keyOpt = "0";
                                    for (int i = 2; i <= rows; i++)
                                    {
                                        var key = worksheetMapping.Cells[i, 1].Text.Trim();
                                        if (key == "") continue;
                                        var valueMapping = worksheetMapping.Cells[i, 2].Text.Replace(" ", string.Empty);
                                        var checkPlus = false;
                                        for (int i4 = 2; i4 <= columnsReal4; i4++)
                                        {
                                            var variable = "[" + sheetReal4.Cells[1, i4].Text.Replace(" ", string.Empty) + "]";
                                            var variableConvertNum = sheetReal4.Cells[1, i4].Text.Replace(" ", string.Empty) + "$c";
                                            var variableSplit1 = "[" + sheetReal4.Cells[1, i4].Text.Replace(" ", string.Empty) + "$x1]";
                                            var variableSplit1Plus = "[" + sheetReal4.Cells[1, i4].Text.Replace(" ", string.Empty) + "$x1d]";
                                            var variableSplit3 = "[" + sheetReal4.Cells[1, i4].Text.Replace(" ", string.Empty) + "$x3]";
                                            var variableSplit4 = "[" + sheetReal4.Cells[1, i4].Text.Replace(" ", string.Empty) + "$x4]";
                                            var variableSplitDash1 = "[" + sheetReal4.Cells[1, i4].Text.Replace(" ", string.Empty) + "$d1]";
                                            var variableNaShi = "[" + sheetReal4.Cells[1, i4].Text.Replace(" ", string.Empty) + "$if]";
                                            var variableOptType = "[" + sheetReal4.Cells[1, i4].Text.Replace(" ", string.Empty) + "$d1$o1]";
                                            var variableByType = "[" + sheetReal4.Cells[1, i4].Text.Replace(" ", string.Empty) + $"$b{keyOpt}]";
                                            var valueReal4 = sheetReal4.Cells[l, i4].Text.Replace(" ", string.Empty);
                                            if (valueReal4 != "")
                                                switch (valueMapping)
                                                {
                                                    case string n when n.Contains(variableConvertNum):
                                                        {
                                                            if (double.TryParse(valueReal4, out _))
                                                            {
                                                                valueMapping = valueReal4;
                                                            }
                                                            else
                                                            {
                                                                valueMapping = valueMapping.Replace("]", string.Empty).Split("$c".ToCharArray()).Last();
                                                            }
                                                        }
                                                        break;
                                                    case string n when n.Contains(variableByType):
                                                        {
                                                            valueMapping = valueReal4;
                                                        }
                                                        break;
                                                    case string n when n.Contains(variableOptType):
                                                        {
                                                            valueReal4 = valueReal4.Split('-')[0];
                                                            keyOpt = valueReal4;
                                                            valueMapping = valueMapping.Replace(variableOptType, valueReal4);
                                                        }
                                                        break;
                                                    case string n when n.Contains(variableSplit1):
                                                        {
                                                            valueReal4 = valueReal4.Split('x')[0];
                                                            valueMapping = valueMapping.Replace(variableSplit1, valueReal4);
                                                        }
                                                        break;
                                                    case string n when n.Contains(variableSplit1Plus):
                                                        {
                                                            checkPlus = true;
                                                            valueReal4 = valueReal4.Split('x')[0];
                                                            valueMapping = valueMapping.Replace(variableSplit1Plus, valueReal4);
                                                        }
                                                        break;
                                                    case string n when n.Contains(variableSplit3):
                                                        {
                                                            valueReal4 = valueReal4.Split('x')[2];
                                                            valueMapping = valueMapping.Replace(variableSplit3, valueReal4);
                                                        }
                                                        break;
                                                    case string n when n.Contains(variableSplit4):
                                                        {
                                                            valueReal4 = valueReal4.Split('x')[3];
                                                            valueMapping = valueMapping.Replace(variableSplit4, valueReal4);
                                                        }
                                                        break;
                                                    case string n when n.Contains(variableSplitDash1):
                                                        {
                                                            valueReal4 = valueReal4.Split('-')[0];
                                                            valueMapping = valueMapping.Replace(variableSplitDash1, valueReal4);
                                                        }
                                                        break;
                                                    case string n when n.Contains(variableNaShi):
                                                        {

                                                            if (!valueReal4.Contains("なし"))
                                                            {
                                                                valueReal4 = "true";
                                                            }
                                                            else
                                                            {
                                                                valueReal4 = "false";
                                                            }
                                                            valueMapping = valueMapping.Replace(variableNaShi, valueReal4);
                                                        }
                                                        break;
                                                    default:
                                                        if (valueReal4 != "" && valueMapping.Contains(variable))
                                                        {
                                                            valueMapping = valueMapping.Replace(variable, valueReal4);
                                                        }
                                                        break;

                                                }
                                            valueMapping = valueMapping.Replace(" ", string.Empty).Normalize();
                                            worksheetMapping.Cells[i, 2].Value = valueMapping;
                                            worksheetMapping.Cells[i, 4].Value = UtilCalculate.ComputeEquation(valueMapping, checkPlus);
                                        }
                                    }

                                    packageMappping.SaveAs(new FileInfo(path + "\\" + nameConnection + ".xlsx"));
                                    packageMappping = new ExcelPackage(new FileInfo(thongtinfile));
                                    worksheetMapping = packageMappping.Workbook.Worksheets[1];
                                }
                            }

                        }

                        Process.Start(path);
                    }
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void autoReport_Click(object sender, EventArgs e)
        {
            liststring = new List<ResItem>();
            var filess = GetFiles(TypeFile.cs_cpp_h);
            //foreach (FileInfo file in filess)
            {
                var file = new FileInfo("C:\\TGL\\NewCAD\\SOURCE_\\newcad\\NewCadCpp\\OdDbBrace.cpp");
                var filePath = file.FullName;
                string fileContent = File.ReadAllText(filePath);
                // var pattern = @"^OdResult OdDbBrace::dwgInFields.*void$";
                string pattern = @"^OdResult OdDbBrace::dwgInFields.*}$";
                //^OdResult OdDbBrace::dwgInFields.* p$
                //var pattern = @"""(.*?)""";
                //MatchCollection matches = Regex.Matches(fileContent, pattern);
                MatchCollection matches = Regex.Matches(fileContent, pattern, RegexOptions.Multiline);
                var matches1 = Regex.Match(fileContent, pattern, RegexOptions.Multiline);

                string modifiedContent = fileContent;
                foreach (Match match in matches)
                {
                    try
                    {
                        var replacementString = match.Groups[0].Value.Replace(_prefixContent + @"""", string.Empty);
                        replacementString = replacementString.Replace(@" ", string.Empty);
                        replacementString = Regex.Replace(replacementString, "[^a-zA-Z0-9]", "");
                        var rex = new ResItem { NameInSource = _prefixContent + "\"{" + string.Format(formula.Text, ResourceName.Text + replacementString) + "}\"", StringContent = match.Groups[0].Value, NameinResx = replacementString };
                        if (!liststring.Any(x => x.StringContent == rex.StringContent))
                        {
                            liststring.Add(rex);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            MessageBox.Show(string.Format("Get string {0} files succesful", filess.Count()));
            ShowDataGrid();

        }
        private void testing(object sender, EventArgs e)
        {
            //Regex regex = new Regex(@"\bdwgInFields\s*\([^)]*\)\s*\{(?:[^{}]|\{(?:[^{}]|\{[^{}]*\})*\})*\}");
            Regex regex = new Regex(@"\bdwgInFields\s*\([^)]*\)\s*\{(?:[^{}]*|\{(?:[^{}]*|\{(?:[^{}]*|\{[^{}]*\})*\})*\})*\}");

            string hardcodePath = @"C:\Users\MSI_i7\Desktop\OdDbTopColumn.cpp";
            string hardcodeTest = @"C:\Users\MSI_i7\Desktop\TestOdDbTopColumn.cpp";
            string allText = File.ReadAllText(hardcodePath);

            MatchCollection mc = regex.Matches(allText);
            List<string> matchResults = new List<string>();
            MessageBox.Show(mc.Count.ToString());
            foreach (Match match in mc)
            {
                matchResults.Add(match.Value);
                MessageBox.Show(match.Value);
            }
            File.WriteAllText(hardcodeTest, matchResults[0]);
        }
        private void getFunction(string filePath, DateTime dt, string folder)
        {
            int startIndex = filePath.LastIndexOf('\\');
            int endIndex = filePath.LastIndexOf(".cpp");
            string fileName = filePath.Substring(startIndex, endIndex - startIndex);

            string allText = File.ReadAllText(filePath);
            //Regex regex = new Regex(@"\bdwgInFields\s*\([^)]*\)\s*\{(?:[^{}]|\{(?:[^{}]|\{[^{}]*\})*\})*\}");
            Regex regex = new Regex(@"\bdwgInFields\s*\([^)]*\)\s*\{(?:[^{}]*|\{(?:[^{}]*|\{(?:[^{}]*|\{[^{}]*\})*\})*\})*\}");
            MatchCollection mc = regex.Matches(allText);
            if (mc.Count == 1)
            {
                string savePath = folder + "\\" + dt.ToString("MMddyy_HHmm") + "\\SavedFunctions\\";
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }
                File.WriteAllText(savePath + fileName, mc[0].Value);
            }
            else
            {
                string errorPath = folder+"\\"+dt.ToString("MMddyy_HHmm") + "\\ErrorFiles\\" ;
                if (!Directory.Exists(errorPath))
                {
                    Directory.CreateDirectory(errorPath);
                }
                File.WriteAllText(errorPath + fileName, string.Empty);
            }
        }
        private void extractFunction(object sender, EventArgs e)
        {
            //using (var fbd = new FolderBrowserDialog())
            //{
            //    DialogResult result = fbd.ShowDialog();
            DateTime dt = DateTime.Now;
            //    if (result == DialogResult.OK && !string.IsNullOrEmpty(fbd.SelectedPath))
            //    {
            string[] files = Directory.GetFiles(TargetToReplaceFolder.Text);
            foreach (string file in files)
            {
                if (file.Contains("OdDb") && file.Contains(".cpp"))
                {
                    getFunction(file, dt, SavedFunctionFolder.Text);
                }
            }
            Process.Start(SavedFunctionFolder.Text+"\\"+ dt.ToString("MMddyy_HHmm"));
            //    }
            //}
        }
        private void pasteFunction(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"\b\:\:dwgInFields\s*\([^)]*\)\s*\{(?:[^{}]*|\{(?:[^{}]*|\{(?:[^{}]*|\{[^{}]*\})*\})*\})*\}");

            string[] files = Directory.GetFiles(SavedFunctionFolder.Text);
            foreach (string filePath in files)
            {
                int startIndex = filePath.LastIndexOf('\\');
                string fileName = filePath.Substring(startIndex + 1);
                string content = File.ReadAllText(filePath);

                string fileToReplacePath = TargetToReplaceFolder.Text + "\\" + fileName + ".cpp";
                if (File.Exists(fileToReplacePath))
                {
                    string replaceContent = File.ReadAllText(fileToReplacePath);
                    var listMatch = regex.Matches(replaceContent);
                    if (listMatch.Count > 0)
                    {
                        string replaceText = listMatch[0].Value.Substring(2);
                        replaceContent = replaceContent.Replace(replaceText, content);

                        File.WriteAllText(fileToReplacePath, replaceContent);
                    }
                }
            }
        }
        private void metroButton5_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrEmpty(fbd.SelectedPath))
                {
                    SavedFunctionFolder.Text = fbd.SelectedPath;
                }
            }
        }
        private void metroButton6_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrEmpty(fbd.SelectedPath))
                {
                    TargetToReplaceFolder.Text = fbd.SelectedPath;
                }
            }
        }
    }
}
