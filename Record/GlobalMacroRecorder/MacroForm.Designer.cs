namespace GlobalMacroRecorder
{
    partial class MacroForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.recordButton = new MetroFramework.Controls.MetroButton();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.metroButton3 = new MetroFramework.Controls.MetroButton();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.Brower = new MetroFramework.Controls.MetroButton();
            this.PathfileorFolder = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ApplySource = new MetroFramework.Controls.MetroButton();
            this.Clipboard = new MetroFramework.Controls.MetroButton();
            this.removeComment = new MetroFramework.Controls.MetroButton();
            this.cb_source = new System.Windows.Forms.CheckBox();
            this.cb_UI = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.RemoveDouble = new MetroFramework.Controls.MetroButton();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.ResourceName = new System.Windows.Forms.TextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.formula = new System.Windows.Forms.ComboBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.getContentUI = new MetroFramework.Controls.MetroButton();
            this.Gettextfrom = new MetroFramework.Controls.MetroButton();
            this.programBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.metroTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.programBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Controls.Add(this.metroTabPage3);
            this.metroTabControl1.Location = new System.Drawing.Point(23, 116);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 1;
            this.metroTabControl1.Size = new System.Drawing.Size(500, 448);
            this.metroTabControl1.TabIndex = 3;
            this.metroTabControl1.UseSelectable = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.groupBox2);
            this.metroTabPage1.Controls.Add(this.groupBox1);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(492, 406);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Automation test";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.recordButton);
            this.groupBox2.Controls.Add(this.metroButton2);
            this.groupBox2.Location = new System.Drawing.Point(3, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(486, 100);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Create test case";
            // 
            // recordButton
            // 
            this.recordButton.Location = new System.Drawing.Point(23, 34);
            this.recordButton.Name = "recordButton";
            this.recordButton.Size = new System.Drawing.Size(88, 46);
            this.recordButton.TabIndex = 3;
            this.recordButton.Text = "Record";
            this.recordButton.UseSelectable = true;
            this.recordButton.Click += new System.EventHandler(this.recordStartButton_Click);
            // 
            // metroButton2
            // 
            this.metroButton2.Location = new System.Drawing.Point(131, 34);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(88, 46);
            this.metroButton2.TabIndex = 4;
            this.metroButton2.Text = "Play back";
            this.metroButton2.UseSelectable = true;
            this.metroButton2.Click += new System.EventHandler(this.playBackMacroButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.metroButton3);
            this.groupBox1.Location = new System.Drawing.Point(3, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(486, 82);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Run automation test case";
            // 
            // metroButton3
            // 
            this.metroButton3.Location = new System.Drawing.Point(12, 19);
            this.metroButton3.Name = "metroButton3";
            this.metroButton3.Size = new System.Drawing.Size(207, 46);
            this.metroButton3.TabIndex = 8;
            this.metroButton3.Text = "Run and Export Report";
            this.metroButton3.UseSelectable = true;
            this.metroButton3.Click += new System.EventHandler(this.Run_click);
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.Gettextfrom);
            this.metroTabPage2.Controls.Add(this.getContentUI);
            this.metroTabPage2.Controls.Add(this.metroLabel2);
            this.metroTabPage2.Controls.Add(this.formula);
            this.metroTabPage2.Controls.Add(this.metroLabel1);
            this.metroTabPage2.Controls.Add(this.ResourceName);
            this.metroTabPage2.Controls.Add(this.Clipboard);
            this.metroTabPage2.Controls.Add(this.ApplySource);
            this.metroTabPage2.Controls.Add(this.dataGridView1);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 10;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(492, 406);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Auto resouces";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.Controls.Add(this.checkBox3);
            this.metroTabPage3.Controls.Add(this.RemoveDouble);
            this.metroTabPage3.Controls.Add(this.checkBox2);
            this.metroTabPage3.Controls.Add(this.checkBox1);
            this.metroTabPage3.Controls.Add(this.cb_UI);
            this.metroTabPage3.Controls.Add(this.cb_source);
            this.metroTabPage3.Controls.Add(this.removeComment);
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.HorizontalScrollbarSize = 10;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(492, 406);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "Auto format code";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            this.metroTabPage3.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.VerticalScrollbarSize = 10;
            // 
            // Brower
            // 
            this.Brower.Location = new System.Drawing.Point(468, 88);
            this.Brower.Name = "Brower";
            this.Brower.Size = new System.Drawing.Size(42, 22);
            this.Brower.TabIndex = 6;
            this.Brower.Text = "...";
            this.Brower.UseSelectable = true;
            this.Brower.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // PathfileorFolder
            // 
            this.PathfileorFolder.Location = new System.Drawing.Point(30, 88);
            this.PathfileorFolder.Name = "PathfileorFolder";
            this.PathfileorFolder.Size = new System.Drawing.Size(432, 20);
            this.PathfileorFolder.TabIndex = 7;
            this.PathfileorFolder.Text = "C:\\Step2";
            this.PathfileorFolder.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.Location = new System.Drawing.Point(3, 48);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(486, 303);
            this.dataGridView1.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Text on source";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Name in Resx";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Name in Source";
            this.Column3.Name = "Column3";
            // 
            // ApplySource
            // 
            this.ApplySource.Location = new System.Drawing.Point(258, 357);
            this.ApplySource.Name = "ApplySource";
            this.ApplySource.Size = new System.Drawing.Size(107, 46);
            this.ApplySource.TabIndex = 4;
            this.ApplySource.Text = "Apply in Source";
            this.ApplySource.UseSelectable = true;
            this.ApplySource.Click += new System.EventHandler(this.ApplySource_Click);
            // 
            // Clipboard
            // 
            this.Clipboard.Location = new System.Drawing.Point(380, 357);
            this.Clipboard.Name = "Clipboard";
            this.Clipboard.Size = new System.Drawing.Size(109, 46);
            this.Clipboard.TabIndex = 5;
            this.Clipboard.Text = "Resx to Clipboard";
            this.Clipboard.UseSelectable = true;
            this.Clipboard.Click += new System.EventHandler(this.Clipboard_Click);
            // 
            // removeComment
            // 
            this.removeComment.Location = new System.Drawing.Point(199, 69);
            this.removeComment.Name = "removeComment";
            this.removeComment.Size = new System.Drawing.Size(107, 46);
            this.removeComment.TabIndex = 5;
            this.removeComment.Text = "Remove Comment";
            this.removeComment.UseSelectable = true;
            this.removeComment.Click += new System.EventHandler(this.removeComment_Click);
            // 
            // cb_source
            // 
            this.cb_source.AutoSize = true;
            this.cb_source.Location = new System.Drawing.Point(3, 27);
            this.cb_source.Name = "cb_source";
            this.cb_source.Size = new System.Drawing.Size(91, 17);
            this.cb_source.TabIndex = 6;
            this.cb_source.Text = "*.cs *.cpp *.h ";
            this.cb_source.UseVisualStyleBackColor = true;
            // 
            // cb_UI
            // 
            this.cb_UI.AutoSize = true;
            this.cb_UI.Location = new System.Drawing.Point(122, 27);
            this.cb_UI.Name = "cb_UI";
            this.cb_UI.Size = new System.Drawing.Size(54, 17);
            this.cb_UI.TabIndex = 7;
            this.cb_UI.Text = "*.xaml";
            this.cb_UI.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(199, 27);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(98, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "Keep //TODO ";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(303, 27);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(87, 17);
            this.checkBox2.TabIndex = 9;
            this.checkBox2.Text = "Keep //Note";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // RemoveDouble
            // 
            this.RemoveDouble.Location = new System.Drawing.Point(327, 69);
            this.RemoveDouble.Name = "RemoveDouble";
            this.RemoveDouble.Size = new System.Drawing.Size(165, 46);
            this.RemoveDouble.TabIndex = 10;
            this.RemoveDouble.Text = "Remove double emptyline";
            this.RemoveDouble.UseSelectable = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(391, 27);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(69, 17);
            this.checkBox3.TabIndex = 11;
            this.checkBox3.Text = "Keep ///";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // ResourceName
            // 
            this.ResourceName.Location = new System.Drawing.Point(3, 22);
            this.ResourceName.Name = "ResourceName";
            this.ResourceName.Size = new System.Drawing.Size(102, 20);
            this.ResourceName.TabIndex = 8;
            this.ResourceName.Text = "_";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(3, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(102, 19);
            this.metroLabel1.TabIndex = 9;
            this.metroLabel1.Text = "Resource Name";
            // 
            // formula
            // 
            this.formula.FormattingEnabled = true;
            this.formula.Items.AddRange(new object[] {
            "x:Static lang:{0}",
            "Binding {0}",
            "{0}"});
            this.formula.Location = new System.Drawing.Point(111, 21);
            this.formula.Name = "formula";
            this.formula.Size = new System.Drawing.Size(167, 21);
            this.formula.TabIndex = 10;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(111, -1);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(108, 19);
            this.metroLabel2.TabIndex = 11;
            this.metroLabel2.Text = "Formula Replace";
            // 
            // getContentUI
            // 
            this.getContentUI.Location = new System.Drawing.Point(284, 3);
            this.getContentUI.Name = "getContentUI";
            this.getContentUI.Size = new System.Drawing.Size(101, 39);
            this.getContentUI.TabIndex = 12;
            this.getContentUI.Text = "Get Content UI";
            this.getContentUI.UseSelectable = true;
            this.getContentUI.Click += new System.EventHandler(this.getContentUI_Click);
            // 
            // Gettextfrom
            // 
            this.Gettextfrom.Location = new System.Drawing.Point(388, 3);
            this.Gettextfrom.Name = "Gettextfrom";
            this.Gettextfrom.Size = new System.Drawing.Size(104, 39);
            this.Gettextfrom.TabIndex = 13;
            this.Gettextfrom.Text = "Get text from";
            this.Gettextfrom.UseSelectable = true;
            this.Gettextfrom.Click += new System.EventHandler(this.Gettextfrom_Click);
            // 
            // programBindingSource
            // 
            this.programBindingSource.DataSource = typeof(GlobalMacroRecorder.Program);
            // 
            // MacroForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 575);
            this.Controls.Add(this.PathfileorFolder);
            this.Controls.Add(this.Brower);
            this.Controls.Add(this.metroTabControl1);
            this.Name = "MacroForm";
            this.Text = "Automation Manage Code";
            this.Load += new System.EventHandler(this.MacroForm_Load);
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.metroTabPage2.ResumeLayout(false);
            this.metroTabPage2.PerformLayout();
            this.metroTabPage3.ResumeLayout(false);
            this.metroTabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.programBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroButton recordButton;
        private MetroFramework.Controls.MetroButton Brower;
        private System.Windows.Forms.TextBox PathfileorFolder;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroButton metroButton3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private MetroFramework.Controls.MetroButton Clipboard;
        private MetroFramework.Controls.MetroButton ApplySource;
        private System.Windows.Forms.CheckBox cb_source;
        private MetroFramework.Controls.MetroButton removeComment;
        private System.Windows.Forms.CheckBox cb_UI;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private MetroFramework.Controls.MetroButton RemoveDouble;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.ComboBox formula;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.TextBox ResourceName;
        private System.Windows.Forms.CheckBox checkBox3;
        private MetroFramework.Controls.MetroButton Gettextfrom;
        private MetroFramework.Controls.MetroButton getContentUI;
        private System.Windows.Forms.BindingSource programBindingSource;
    }
}

