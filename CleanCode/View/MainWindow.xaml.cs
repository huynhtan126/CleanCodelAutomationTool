using CleanCode;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
namespace CleanCode
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new CleanCodeViewModel();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Path.SelectAll();
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            //Path.Text = Clipboard.GetText();
        }

    }
}
