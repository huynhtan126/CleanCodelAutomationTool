using GlobalMacroRecorder;
using MouseKeyboardLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;

using System.Windows.Input;
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
        private string _NameCommandTemplate;
        public string NameCommand_Suffix
        {
            get { return _NameCommandTemplate; }
            set
            {
                if (_NameCommandTemplate != value)
                {
                    _NameCommandTemplate = value;
                    OnPropertyChanged(nameof(_NameCommandTemplate));
                }
            }
        }
        private List<string> _listItem;
        public List<string> listItem
        {
            get { return _listItem; }
            set
            {
                if (_listItem != value)
                {
                    _listItem = value;
                    OnPropertyChanged(nameof(_listItem));
                }
            }
        }

        #endregion
        #region Initialize
        public CleanCodeViewModel()
        {
            listItem = new List<string> { "{0}", "lang:Localization {0}", "TranslationSource.Instance[nameof({0})]" };
            Path = string.Empty;
            NameCommand_Suffix = "TranslationSource.Instance[nameof({0})]";
            NameCommand = "_";
            ButtonRemoveComment = new RelayCommand<object>(p => true, p => Remove_comment());
            ButtonRemoveEmptyLine = new RelayCommand<object>(p => true, p => Remove_empty_Line());
            ButtonGetStringFromSource = new RelayCommand<object>(p => true, p => GetStringFromSource());
            ButtonResourceToClipBoard = new RelayCommand<object>(p => true, p => ResourceToClipBoard());
            ButtonApplyStringtoSource = new RelayCommand<object>(p => true, p => ApplyStringtoSource());
            //ButtonRecordMacro = new RelayCommand<object>(p => true, p => RecordMacro());
            //ButtonPlayBack = new RelayCommand<object>(p => true, p => Playback());
            ButtonGetContentFromSource = new RelayCommand<object>(p => true, p => GetContentFromSource());

        }
        #endregion
        private string _prefixContent = "Content=";
        //private string _prefixContent = "Title=";
        public RelayCommand<object> ButtonGetContentFromSource { get; set; }
        public RelayCommand<object> ButtonRemoveComment { get; set; }
        public RelayCommand<object> ButtonRemoveEmptyLine { get; set; }
        public RelayCommand<object> ButtonGetStringFromSource { get; set; }
        public RelayCommand<object> ButtonApplyStringtoSource { get; set; }
        public RelayCommand<object> ButtonResourceToClipBoard { get; set; }
        public RelayCommand<object> ButtonRecordMacro { get; set; }
        public RelayCommand<object> ButtonPlayBack { get; set; }
        public string ButtonRecordMacroContent { get; set; }
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
                    value = value.Replace(_prefixContent, "");
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
            //TranslationSource.Instance[nameof(InsertLevelViewRes.MainGrid)]
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
                        var rex = new ResItem { NameInSource = string.Format(NameCommand_Suffix, NameCommand + "." + replacementString), StringContent = match.Groups[0].Value, NameinResx = replacementString };
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
        public void GetContentFromSource()
        {
            //TranslationSource.Instance[nameof(InsertLevelViewRes.MainGrid)]
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
                        var rex = new ResItem { NameInSource = _prefixContent + "\"{" + string.Format(NameCommand_Suffix, NameCommand + replacementString) + "}\"", StringContent = match.Groups[0].Value, NameinResx = replacementString };
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
                        //var emptyline = @"^\s*$\n";
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
