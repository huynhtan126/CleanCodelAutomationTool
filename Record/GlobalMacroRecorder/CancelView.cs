using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GlobalMacroRecorder
{
    public partial class CancelView : Form
    {
        public CancelView()
        {
            InitializeComponent();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            MacroForm.isBreak= true;
            this.Close();
        }
    }
}
