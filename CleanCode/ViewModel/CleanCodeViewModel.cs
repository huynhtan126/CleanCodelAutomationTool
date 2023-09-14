using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Markup;
using Utilities;
namespace CleanCode
{
    public class CleanCodeViewModel : ViewModelBase
    {
        #region Properties
        public MainWindow MainWindowView
        {
            get
            {
                if (MainWindowView == null)
                {
                    MainWindowView = new MainWindow() { DataContext = this };
                }
                return MainWindowView;
            }
            set
            {
                MainWindowView = value;
                OnPropertyChanged(nameof(MainWindowView));
            }
        }
        private ObservableCollection<ResItem> _liststring;
        public ObservableCollection<ResItem> liststring
        {
            get { return _liststring; }
            set
            {
                if (_liststring != value)
                {
                    _liststring = value;
                    OnPropertyChanged(nameof(_liststring));
                }
            }
        }
        private string _path;
        public string Path
        {
            get { return _path; }
            set
            {
                if (_path != value)
                {
                    _path = value;
                    OnPropertyChanged(nameof(_path));
                }
            }
        }
        private string _NameCommand;
        public string NameCommand
        {
            get { return _NameCommand; }
            set
            {
                if (_NameCommand != value)
                {
                    _NameCommand = value;
                    OnPropertyChanged(nameof(_NameCommand));
                }
            }
        }
        #endregion
        #region Initialize
        public CleanCodeViewModel()
        {
            Path = string.Empty;
            NameCommand = string.Empty;
            ButtonRemoveComment = new RelayCommand<object>(p => true, p => Remove_comment());
            ButtonRemoveEmptyLine = new RelayCommand<object>(p => true, p => Remove_empty_Line());
            ButtonGetStringFromSource = new RelayCommand<object>(p => true, p => GetStringFromSource());
            ButtonResourceToClipBoard = new RelayCommand<object>(p => true, p => ResourceToClipBoard());
            ButtonApplyStringtoSource = new RelayCommand<object>(p => true, p => ApplyStringtoSource());
        }
        #endregion
        public RelayCommand<object> ButtonRemoveComment { get; set; }
        public RelayCommand<object> ButtonRemoveEmptyLine { get; set; }
        public RelayCommand<object> ButtonGetStringFromSource { get; set; }
        public RelayCommand<object> ButtonApplyStringtoSource { get; set; }
        public RelayCommand<object> ButtonResourceToClipBoard { get; set; }
        public void ApplyStringtoSource()
        {
            if (NameCommand == string.Empty)
            {
                MessageBox.Show("Please fill Name Command");
                return;
            }
            if (Path == string.Empty)
            {
                MessageBox.Show("Please fill path");
                return;
            }

            var filess = GetFiles();
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
        public void ResourceToClipBoard()
        {

            try
            {
                var content = "";
                foreach (var item in liststring)
                {
                    var value = item.StringContent.Replace("\"", "");
                    content = content + "<data name=\"" + item.NameinResx + "\" xml:space=\"preserve\"><value>" + value + "</value></data>\r\n";
                }
                Clipboard.SetText(content);
                MessageBox.Show("Set to clipboard successful.");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }
        public void GetStringFromSource()
        {
            if (NameCommand == string.Empty)
            {
                MessageBox.Show("Please fill Name Command");
                return;
            }
            if (Path == string.Empty)
            {
                MessageBox.Show("Please fill path");
                return;
            }
            liststring = new ObservableCollection<ResItem>();
            var filess = GetFiles();
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
                        var rex = new ResItem { NameInSource = NameCommand + "." + replacementString, StringContent = match.Groups[0].Value, NameinResx = replacementString };
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
            OnPropertyChanged(nameof(liststring));
            MessageBox.Show(string.Format("Get string {0} files succesful", filess.Count()));
        }
        public void Remove_comment()
        {
            if (Path == string.Empty)
            {
                MessageBox.Show("Please fill path");
                return;
            }
            var filess = GetFiles();
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
                            if (me.Value.StartsWith("//TODO"))
                            {
                            }
                            else
                            {
                                if (me.Value.StartsWith("///"))
                                {
                                }
                                else
                                {
                                    if (me.Value.StartsWith("/*") || me.Value.StartsWith("//"))
                                        return me.Value.StartsWith("//") ? Environment.NewLine : "";
                                }
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
        public void Remove_empty_Line()
        {
            if (Path == string.Empty)
            {
                MessageBox.Show("Please fill path");
                return;
            }
            var filess = GetFiles();
            foreach (FileInfo file in filess)
            {
                try
                {
                    string vanban;
                    using (StreamReader reader = new StreamReader(file.FullName))
                    {
                        vanban = reader.ReadToEnd();
                        var emptyline = @"^\s*$\n";
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
        public FileInfo[] GetFiles()
        {
            try
            {


                if (File.Exists(Path))
                {
                    FileInfo[] listFile = new FileInfo[] { new FileInfo(Path) };
                    return listFile;
                }
                else
                {
                    var pathfolder = Path;
                    DirectoryInfo d = new DirectoryInfo(pathfolder);
                    return d.GetFiles("*.cs", SearchOption.AllDirectories);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                FileInfo[] listFile = new FileInfo[] { };
                return listFile;
            }
        }
    }
}
