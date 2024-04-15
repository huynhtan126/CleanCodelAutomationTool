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
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.recordButton = new MetroFramework.Controls.MetroButton();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.metroButton3 = new MetroFramework.Controls.MetroButton();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.Gettextfrom = new MetroFramework.Controls.MetroButton();
            this.getContentUI = new MetroFramework.Controls.MetroButton();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.formula = new System.Windows.Forms.ComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.ResourceName = new System.Windows.Forms.TextBox();
            this.clip = new MetroFramework.Controls.MetroButton();
            this.ApplySource = new MetroFramework.Controls.MetroButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.metroRadioButton1 = new MetroFramework.Controls.MetroRadioButton();
            this.xaml = new MetroFramework.Controls.MetroRadioButton();
            this.cs_cpp = new MetroFramework.Controls.MetroRadioButton();
            this.sum = new System.Windows.Forms.CheckBox();
            this.RemoveDouble = new MetroFramework.Controls.MetroButton();
            this.note = new System.Windows.Forms.CheckBox();
            this.todo = new System.Windows.Forms.CheckBox();
            this.removeComment = new MetroFramework.Controls.MetroButton();
            this.Brower = new MetroFramework.Controls.MetroButton();
            this.PathfileorFolder = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.programBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.metroTabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(500, 448);
            this.metroTabControl1.TabIndex = 3;
            this.metroTabControl1.UseSelectable = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.label1);
            this.metroTabPage1.Controls.Add(this.numericUpDown1);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(314, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Speed Play Back x";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(417, 7);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(66, 20);
            this.numericUpDown1.TabIndex = 9;
            this.numericUpDown1.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.metroButton1);
            this.groupBox2.Controls.Add(this.dataGridView2);
            this.groupBox2.Controls.Add(this.recordButton);
            this.groupBox2.Controls.Add(this.metroButton2);
            this.groupBox2.Location = new System.Drawing.Point(6, 127);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(486, 271);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Create test case";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(66, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(197, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "\"Trở về trạng thái ban đầu. OK?\"";
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(269, 190);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(208, 28);
            this.metroButton1.TabIndex = 6;
            this.metroButton1.Text = "Refesh";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click_2);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4});
            this.dataGridView2.Location = new System.Drawing.Point(269, 34);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(208, 150);
            this.dataGridView2.TabIndex = 5;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "File mcr";
            this.Column4.Name = "Column4";
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
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.metroButton3);
            this.groupBox1.Location = new System.Drawing.Point(6, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(486, 82);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Run automation test case";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(266, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "\"Trở về trạng thái ban đầu. OK?\"";
            this.label2.Click += new System.EventHandler(this.label2_Click);
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
            this.metroTabPage2.Controls.Add(this.clip);
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
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(111, -1);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(108, 19);
            this.metroLabel2.TabIndex = 11;
            this.metroLabel2.Text = "Formula Replace";
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
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(3, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(102, 19);
            this.metroLabel1.TabIndex = 9;
            this.metroLabel1.Text = "Resource Name";
            // 
            // ResourceName
            // 
            this.ResourceName.Location = new System.Drawing.Point(3, 22);
            this.ResourceName.Name = "ResourceName";
            this.ResourceName.Size = new System.Drawing.Size(102, 20);
            this.ResourceName.TabIndex = 8;
            this.ResourceName.Text = "Resx.";
            // 
            // clip
            // 
            this.clip.Location = new System.Drawing.Point(380, 357);
            this.clip.Name = "clip";
            this.clip.Size = new System.Drawing.Size(109, 46);
            this.clip.TabIndex = 5;
            this.clip.Text = "Resx to Clipboard";
            this.clip.UseSelectable = true;
            this.clip.Click += new System.EventHandler(this.Clipboard_Click);
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
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
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
            // metroTabPage3
            // 
            this.metroTabPage3.Controls.Add(this.metroRadioButton1);
            this.metroTabPage3.Controls.Add(this.xaml);
            this.metroTabPage3.Controls.Add(this.cs_cpp);
            this.metroTabPage3.Controls.Add(this.sum);
            this.metroTabPage3.Controls.Add(this.RemoveDouble);
            this.metroTabPage3.Controls.Add(this.note);
            this.metroTabPage3.Controls.Add(this.todo);
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
            // metroRadioButton1
            // 
            this.metroRadioButton1.AutoSize = true;
            this.metroRadioButton1.Checked = true;
            this.metroRadioButton1.Location = new System.Drawing.Point(3, 91);
            this.metroRadioButton1.Name = "metroRadioButton1";
            this.metroRadioButton1.Size = new System.Drawing.Size(36, 15);
            this.metroRadioButton1.TabIndex = 14;
            this.metroRadioButton1.TabStop = true;
            this.metroRadioButton1.Text = "*.*";
            this.metroRadioButton1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.metroRadioButton1.UseSelectable = true;
            // 
            // xaml
            // 
            this.xaml.AutoSize = true;
            this.xaml.Location = new System.Drawing.Point(3, 60);
            this.xaml.Name = "xaml";
            this.xaml.Size = new System.Drawing.Size(57, 15);
            this.xaml.TabIndex = 13;
            this.xaml.Text = "*.xaml";
            this.xaml.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.xaml.UseSelectable = true;
            // 
            // cs_cpp
            // 
            this.cs_cpp.AutoSize = true;
            this.cs_cpp.Location = new System.Drawing.Point(3, 29);
            this.cs_cpp.Name = "cs_cpp";
            this.cs_cpp.Size = new System.Drawing.Size(94, 15);
            this.cs_cpp.TabIndex = 12;
            this.cs_cpp.Text = "*.cs *.cpp *.h ";
            this.cs_cpp.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.cs_cpp.UseSelectable = true;
            // 
            // sum
            // 
            this.sum.AutoSize = true;
            this.sum.Checked = true;
            this.sum.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sum.Location = new System.Drawing.Point(391, 27);
            this.sum.Name = "sum";
            this.sum.Size = new System.Drawing.Size(69, 17);
            this.sum.TabIndex = 11;
            this.sum.Text = "Keep ///";
            this.sum.UseVisualStyleBackColor = true;
            // 
            // RemoveDouble
            // 
            this.RemoveDouble.Location = new System.Drawing.Point(327, 69);
            this.RemoveDouble.Name = "RemoveDouble";
            this.RemoveDouble.Size = new System.Drawing.Size(165, 46);
            this.RemoveDouble.TabIndex = 10;
            this.RemoveDouble.Text = "Remove double emptyline";
            this.RemoveDouble.UseSelectable = true;
            this.RemoveDouble.Click += new System.EventHandler(this.RemoveDouble_Click);
            // 
            // note
            // 
            this.note.AutoSize = true;
            this.note.Checked = true;
            this.note.CheckState = System.Windows.Forms.CheckState.Checked;
            this.note.Location = new System.Drawing.Point(303, 27);
            this.note.Name = "note";
            this.note.Size = new System.Drawing.Size(87, 17);
            this.note.TabIndex = 9;
            this.note.Text = "Keep //Note";
            this.note.UseVisualStyleBackColor = true;
            // 
            // todo
            // 
            this.todo.AutoSize = true;
            this.todo.Checked = true;
            this.todo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.todo.Location = new System.Drawing.Point(199, 27);
            this.todo.Name = "todo";
            this.todo.Size = new System.Drawing.Size(98, 17);
            this.todo.TabIndex = 8;
            this.todo.Text = "Keep //TODO ";
            this.todo.UseVisualStyleBackColor = true;
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
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::GlobalMacroRecorder.Properties.Resources.images__9_;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(38, 108);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(202, 135);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // programBindingSource
            // 
            this.programBindingSource.DataSource = typeof(GlobalMacroRecorder.Program);
            // 
            // MacroForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(546, 575);
            this.Controls.Add(this.PathfileorFolder);
            this.Controls.Add(this.Brower);
            this.Controls.Add(this.metroTabControl1);
            this.MaximizeBox = false;
            this.Name = "MacroForm";
            this.Text = "Automation Manage Code";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MacroForm_FormClosed);
            this.Load += new System.EventHandler(this.MacroForm_Load);
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.metroTabPage2.ResumeLayout(false);
            this.metroTabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.metroTabPage3.ResumeLayout(false);
            this.metroTabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private MetroFramework.Controls.MetroButton clip;
        private MetroFramework.Controls.MetroButton ApplySource;
        private MetroFramework.Controls.MetroButton removeComment;
        private System.Windows.Forms.CheckBox todo;
        private System.Windows.Forms.CheckBox note;
        private MetroFramework.Controls.MetroButton RemoveDouble;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.ComboBox formula;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.TextBox ResourceName;
        private System.Windows.Forms.CheckBox sum;
        private MetroFramework.Controls.MetroButton Gettextfrom;
        private MetroFramework.Controls.MetroButton getContentUI;
        private System.Windows.Forms.BindingSource programBindingSource;
        private MetroFramework.Controls.MetroRadioButton metroRadioButton1;
        private MetroFramework.Controls.MetroRadioButton xaml;
        private MetroFramework.Controls.MetroRadioButton cs_cpp;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private MetroFramework.Controls.MetroButton metroButton1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

