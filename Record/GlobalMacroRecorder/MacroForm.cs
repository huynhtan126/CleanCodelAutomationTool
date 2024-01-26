
using MouseKeyboardLibrary;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using Timer = System.Windows.Forms.Timer;

namespace GlobalMacroRecorder
{
    public partial class MacroForm : MetroFramework.Forms.MetroForm
    {
        public string duongdanfolder = @"C:\Step2";
        private string pathCurrentFile = "";
        private string _pathRecordVideo = "";
        private string _prefixContent = "Content=";
        List<MacroEvent> events = new List<MacroEvent>();
        int lastTimeRecorded = 0;
        private List<ResItem> liststring = new List<ResItem>();
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
            MessageBox.Show("Tool works better in 1920x1080 and scale 100%");
            RunMacroandSaveVideo();
        }

        private void RunMacroandSaveVideo()
        {
            if(!File.Exists(pathCurrentFile))
            {
                MessageBox.Show("File not exist");
                return;
            }
            var vanban = File.ReadAllText(pathCurrentFile);
            var docMacro = JsonConvert.DeserializeObject<List<MacroEvent>>(vanban);
            this.events = docMacro;
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

            }
            
            process.Kill();
           
        }
        public Bitmap GetScreenSnapshot()
        {
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size);

            return bitmap;
        }
        private void MacroForm_Load(object sender, EventArgs e)
        {
            formula.SelectedIndex = 0;
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

        private void Run_click(object sender, EventArgs e)
        {
            MessageBox.Show("Tool works better in 1920x1080 and scale 100%");
            if (Directory.Exists(duongdanfolder))
            {
                #region get all file

                DirectoryInfo d = new DirectoryInfo(duongdanfolder);

                FileInfo[] Files = d.GetFiles("*.mcr"); 
                var filess = Files.ToList();
                #endregion
                
                FileInfo[] FilesExcel = d.GetFiles("*.xlsx"); 
                var filex = FilesExcel.ToList();
                var path = filex[0].FullName;
                FileInfo fileInfo = new FileInfo(path);

                ExcelPackage package = new ExcelPackage(fileInfo);
                ExcelWorksheet worksheet = package.Workbook.Worksheets[2];
                int rows = worksheet.Dimension.Rows; 
                int columns = worksheet.Dimension.Columns; 
                var listErrorCase = new List<string>();
                for (int i = 12; i <= rows; i++)
                {
                    try
                    {
                        var fileName = worksheet.Cells[i, 5].Value.ToString();
                        pathCurrentFile = duongdanfolder + "\\" + fileName + ".mcr";

                        RunMacroandSaveVideo();
                        var screeenshot = GetScreenSnapshot();
                        if (worksheet.Cells[i, 9].Text.ToString().Trim() == string.Empty)
                        {
                            worksheet.Cells[i, 9].Formula = "=HYPERLINK(" + @"""" + duongdanfolder + "\\" + fileName + ".avi" + @"""" + "," + @"""Evidence :" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + @"""" + ")";

                            var picture = worksheet.Drawings.AddPicture(fileName, screeenshot);
                            picture.SetSize(400, 250);
                            picture.SetPosition((i-1),100, 9,0);
                        }
                        else if (worksheet.Cells[i, 11].Text.ToString().Trim() == string.Empty)
                        {
                            worksheet.Cells[i, 11].Formula = "=HYPERLINK(" + @"""" + duongdanfolder + "\\" + fileName + ".avi" + @"""" + "," + @"""Evidence :" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + @"""" + ")";

                            var picture = worksheet.Drawings.AddPicture(fileName, screeenshot);
                            picture.SetSize(400, 250);
                            picture.SetPosition((i - 1), 100, 11, 0);
                        }
                        else if (worksheet.Cells[i, 13].Text.ToString().Trim() == string.Empty)
                        {
                            worksheet.Cells[i, 13].Formula = "=HYPERLINK(" + @"""" + duongdanfolder + "\\" + fileName + ".avi" + @"""" + "," + @"""Evidence :" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + @"""" + ")";

                            var picture = worksheet.Drawings.AddPicture(fileName, screeenshot);
                            picture.SetSize(400, 250);
                            picture.SetPosition((i - 1), 100, 13, 0);
                        }
                    }
                    catch (Exception ex)
                    {
                        listErrorCase.Add("Error at row :" + i + " " + ex.Message);
                        continue;
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
            }
            else
            {
                MessageBox.Show("Folder not exist");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

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
                        replacementString= "_" + replacementString;
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
    }
}
