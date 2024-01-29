using System;
using System.IO;
using System.Windows.Forms;
using gma.System.Windows;

namespace GlobalHookDemo
{
    class MainForm : System.Windows.Forms.Form
    {

        public MainForm()
        {
            InitializeComponent();

        }

        // THIS METHOD IS MAINTAINED BY THE FORM DESIGNER
        // DO NOT EDIT IT MANUALLY! YOUR CHANGES ARE LIKELY TO BE LOST
        void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(10, 10);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }
        //private static string path = "C:\\TGL\\CleanCode\\CleanCodelAutomationTool\\Record\\GlobalMacroRecorder\\bin\\Debug\\RecordVideo\\Test.txt";
        private static string path = "";
        [STAThread]
        public static void Main(string[] args)
        {
            path = args[0];
            Application.Run(new MainForm());
        }

        void ButtonStartClick(object sender, System.EventArgs e)
        {
            actHook.Start();
        }

        void ButtonStopClick(object sender, System.EventArgs e)
        {
            actHook.Stop();
        }

        UserActivityHook actHook;

        public void MyKeyDown(object sender, KeyEventArgs e)
        {
           if(e.KeyCode== Keys.F12)
            {
                File.WriteAllText(path, "true");
                //Application.Exit();
            }    
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Hide();
            actHook = new UserActivityHook(); // crate an instance with global hooks
                                              // hang on events
                                              //actHook.OnMouseActivity+=new MouseEventHandler(MouseMoved);
            actHook.KeyDown += new KeyEventHandler(MyKeyDown);
        }
    }
}
