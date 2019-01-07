namespace GraphicalProgrammingLanguage
{
	partial class GraphicalProgrammingLanguageForm
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
			this.TopMenuStrip = new System.Windows.Forms.MenuStrip();
			this.homeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CodeInputTabControl = new System.Windows.Forms.TabControl();
			this.SingleLineInputTab = new System.Windows.Forms.TabPage();
			this.SingleLineOutput = new System.Windows.Forms.RichTextBox();
			this.SingleLineInputTextbox = new System.Windows.Forms.TextBox();
			this.MultiLineInputTab = new System.Windows.Forms.TabPage();
			this.MultiLineInputSaveBtn = new System.Windows.Forms.Button();
			this.MultiLineInputLoadBtn = new System.Windows.Forms.Button();
			this.MultiLineInputTextBox = new System.Windows.Forms.RichTextBox();
			this.MultiLineInputLoadFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.MultiLineInputSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.GraphicsPictureBox = new System.Windows.Forms.PictureBox();
			this.DrawingPanel = new System.Windows.Forms.Panel();
			this.HelpPanel = new System.Windows.Forms.Panel();
			this.HelpGridView = new System.Windows.Forms.DataGridView();
			this.CommandColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ParametersColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DescriptionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.TopMenuStrip.SuspendLayout();
			this.CodeInputTabControl.SuspendLayout();
			this.SingleLineInputTab.SuspendLayout();
			this.MultiLineInputTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GraphicsPictureBox)).BeginInit();
			this.DrawingPanel.SuspendLayout();
			this.HelpPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.HelpGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// TopMenuStrip
			// 
			this.TopMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.TopMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homeToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.TopMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.TopMenuStrip.Name = "TopMenuStrip";
			this.TopMenuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
			this.TopMenuStrip.Size = new System.Drawing.Size(1389, 28);
			this.TopMenuStrip.TabIndex = 0;
			this.TopMenuStrip.Text = "menuStrip1";
			// 
			// homeToolStripMenuItem
			// 
			this.homeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveImageToolStripMenuItem});
			this.homeToolStripMenuItem.Name = "homeToolStripMenuItem";
			this.homeToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
			this.homeToolStripMenuItem.Text = "Draw";
			this.homeToolStripMenuItem.Click += new System.EventHandler(this.HomeToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
			this.helpToolStripMenuItem.Text = "Help";
			this.helpToolStripMenuItem.Click += new System.EventHandler(this.HelpToolStripMenuItem_Click);
			// 
			// CodeInputTabControl
			// 
			this.CodeInputTabControl.Controls.Add(this.SingleLineInputTab);
			this.CodeInputTabControl.Controls.Add(this.MultiLineInputTab);
			this.CodeInputTabControl.Dock = System.Windows.Forms.DockStyle.Right;
			this.CodeInputTabControl.Location = new System.Drawing.Point(710, 0);
			this.CodeInputTabControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.CodeInputTabControl.Name = "CodeInputTabControl";
			this.CodeInputTabControl.SelectedIndex = 0;
			this.CodeInputTabControl.Size = new System.Drawing.Size(679, 601);
			this.CodeInputTabControl.TabIndex = 2;
			// 
			// SingleLineInputTab
			// 
			this.SingleLineInputTab.Controls.Add(this.SingleLineOutput);
			this.SingleLineInputTab.Controls.Add(this.SingleLineInputTextbox);
			this.SingleLineInputTab.Location = new System.Drawing.Point(4, 25);
			this.SingleLineInputTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.SingleLineInputTab.Name = "SingleLineInputTab";
			this.SingleLineInputTab.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.SingleLineInputTab.Size = new System.Drawing.Size(671, 572);
			this.SingleLineInputTab.TabIndex = 0;
			this.SingleLineInputTab.Text = "Single Line Input";
			this.SingleLineInputTab.UseVisualStyleBackColor = true;
			// 
			// SingleLineOutput
			// 
			this.SingleLineOutput.BackColor = System.Drawing.Color.White;
			this.SingleLineOutput.Enabled = false;
			this.SingleLineOutput.Location = new System.Drawing.Point(19, 97);
			this.SingleLineOutput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.SingleLineOutput.Name = "SingleLineOutput";
			this.SingleLineOutput.Size = new System.Drawing.Size(623, 450);
			this.SingleLineOutput.TabIndex = 1;
			this.SingleLineOutput.Text = "";
			// 
			// SingleLineInputTextbox
			// 
			this.SingleLineInputTextbox.Location = new System.Drawing.Point(19, 18);
			this.SingleLineInputTextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.SingleLineInputTextbox.Multiline = true;
			this.SingleLineInputTextbox.Name = "SingleLineInputTextbox";
			this.SingleLineInputTextbox.Size = new System.Drawing.Size(623, 67);
			this.SingleLineInputTextbox.TabIndex = 0;
			this.SingleLineInputTextbox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SingleLineInputEnter);
			// 
			// MultiLineInputTab
			// 
			this.MultiLineInputTab.Controls.Add(this.MultiLineInputSaveBtn);
			this.MultiLineInputTab.Controls.Add(this.MultiLineInputLoadBtn);
			this.MultiLineInputTab.Controls.Add(this.MultiLineInputTextBox);
			this.MultiLineInputTab.Location = new System.Drawing.Point(4, 25);
			this.MultiLineInputTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.MultiLineInputTab.Name = "MultiLineInputTab";
			this.MultiLineInputTab.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.MultiLineInputTab.Size = new System.Drawing.Size(671, 572);
			this.MultiLineInputTab.TabIndex = 1;
			this.MultiLineInputTab.Text = "Multi line Input";
			this.MultiLineInputTab.UseVisualStyleBackColor = true;
			// 
			// MultiLineInputSaveBtn
			// 
			this.MultiLineInputSaveBtn.Location = new System.Drawing.Point(128, 14);
			this.MultiLineInputSaveBtn.Margin = new System.Windows.Forms.Padding(4);
			this.MultiLineInputSaveBtn.Name = "MultiLineInputSaveBtn";
			this.MultiLineInputSaveBtn.Size = new System.Drawing.Size(100, 28);
			this.MultiLineInputSaveBtn.TabIndex = 3;
			this.MultiLineInputSaveBtn.Text = "Save";
			this.MultiLineInputSaveBtn.UseVisualStyleBackColor = true;
			this.MultiLineInputSaveBtn.Click += new System.EventHandler(this.MultiLineInputSaveBtn_Click);
			// 
			// MultiLineInputLoadBtn
			// 
			this.MultiLineInputLoadBtn.Location = new System.Drawing.Point(20, 14);
			this.MultiLineInputLoadBtn.Margin = new System.Windows.Forms.Padding(4);
			this.MultiLineInputLoadBtn.Name = "MultiLineInputLoadBtn";
			this.MultiLineInputLoadBtn.Size = new System.Drawing.Size(100, 28);
			this.MultiLineInputLoadBtn.TabIndex = 2;
			this.MultiLineInputLoadBtn.Text = "Load";
			this.MultiLineInputLoadBtn.UseVisualStyleBackColor = true;
			this.MultiLineInputLoadBtn.Click += new System.EventHandler(this.MultiLineInputLoadBtn_Click);
			// 
			// MultiLineInputTextBox
			// 
			this.MultiLineInputTextBox.Location = new System.Drawing.Point(20, 49);
			this.MultiLineInputTextBox.Margin = new System.Windows.Forms.Padding(4);
			this.MultiLineInputTextBox.Name = "MultiLineInputTextBox";
			this.MultiLineInputTextBox.Size = new System.Drawing.Size(636, 472);
			this.MultiLineInputTextBox.TabIndex = 0;
			this.MultiLineInputTextBox.Text = "";
			// 
			// MultiLineInputLoadFileDialog
			// 
			this.MultiLineInputLoadFileDialog.FileName = "openFileDialog1";
			// 
			// GraphicsPictureBox
			// 
			this.GraphicsPictureBox.Dock = System.Windows.Forms.DockStyle.Left;
			this.GraphicsPictureBox.Location = new System.Drawing.Point(0, 0);
			this.GraphicsPictureBox.Name = "GraphicsPictureBox";
			this.GraphicsPictureBox.Size = new System.Drawing.Size(708, 601);
			this.GraphicsPictureBox.TabIndex = 3;
			this.GraphicsPictureBox.TabStop = false;
			this.GraphicsPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.GraphicsPictureBoxPaint);
			// 
			// DrawingPanel
			// 
			this.DrawingPanel.Controls.Add(this.GraphicsPictureBox);
			this.DrawingPanel.Controls.Add(this.CodeInputTabControl);
			this.DrawingPanel.Location = new System.Drawing.Point(0, 43);
			this.DrawingPanel.Name = "DrawingPanel";
			this.DrawingPanel.Size = new System.Drawing.Size(1389, 601);
			this.DrawingPanel.TabIndex = 4;
			// 
			// HelpPanel
			// 
			this.HelpPanel.Controls.Add(this.HelpGridView);
			this.HelpPanel.Location = new System.Drawing.Point(0, 43);
			this.HelpPanel.Name = "HelpPanel";
			this.HelpPanel.Size = new System.Drawing.Size(1389, 554);
			this.HelpPanel.TabIndex = 4;
			this.HelpPanel.Visible = false;
			// 
			// HelpGridView
			// 
			this.HelpGridView.AllowUserToAddRows = false;
			this.HelpGridView.AllowUserToDeleteRows = false;
			this.HelpGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.HelpGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.HelpGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.HelpGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CommandColumn,
            this.ParametersColumn,
            this.DescriptionColumn});
			this.HelpGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.HelpGridView.Location = new System.Drawing.Point(0, 0);
			this.HelpGridView.Name = "HelpGridView";
			this.HelpGridView.ReadOnly = true;
			this.HelpGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
			this.HelpGridView.RowTemplate.Height = 24;
			this.HelpGridView.Size = new System.Drawing.Size(1389, 554);
			this.HelpGridView.TabIndex = 0;
			// 
			// CommandColumn
			// 
			this.CommandColumn.HeaderText = "Command";
			this.CommandColumn.Name = "CommandColumn";
			this.CommandColumn.ReadOnly = true;
			// 
			// ParametersColumn
			// 
			this.ParametersColumn.HeaderText = "Parameters";
			this.ParametersColumn.Name = "ParametersColumn";
			this.ParametersColumn.ReadOnly = true;
			// 
			// DescriptionColumn
			// 
			this.DescriptionColumn.HeaderText = "Description";
			this.DescriptionColumn.Name = "DescriptionColumn";
			this.DescriptionColumn.ReadOnly = true;
			// 
			// saveImageToolStripMenuItem
			// 
			this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
			this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
			this.saveImageToolStripMenuItem.Text = "Save Image";
			this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
			// 
			// GraphicalProgrammingLanguageForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(1389, 640);
			this.Controls.Add(this.HelpPanel);
			this.Controls.Add(this.TopMenuStrip);
			this.Controls.Add(this.DrawingPanel);
			this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.Name = "GraphicalProgrammingLanguageForm";
			this.Text = "GraphicalProgrammingLanguageInterface";
			this.TopMenuStrip.ResumeLayout(false);
			this.TopMenuStrip.PerformLayout();
			this.CodeInputTabControl.ResumeLayout(false);
			this.SingleLineInputTab.ResumeLayout(false);
			this.SingleLineInputTab.PerformLayout();
			this.MultiLineInputTab.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.GraphicsPictureBox)).EndInit();
			this.DrawingPanel.ResumeLayout(false);
			this.HelpPanel.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.HelpGridView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip TopMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem homeToolStripMenuItem;
		private System.Windows.Forms.TabControl CodeInputTabControl;
		private System.Windows.Forms.TabPage SingleLineInputTab;
		private System.Windows.Forms.RichTextBox SingleLineOutput;
		private System.Windows.Forms.TextBox SingleLineInputTextbox;
		private System.Windows.Forms.TabPage MultiLineInputTab;
		private System.Windows.Forms.RichTextBox MultiLineInputTextBox;
		private System.Windows.Forms.Button MultiLineInputSaveBtn;
		private System.Windows.Forms.Button MultiLineInputLoadBtn;
		private System.Windows.Forms.OpenFileDialog MultiLineInputLoadFileDialog;
		private System.Windows.Forms.SaveFileDialog MultiLineInputSaveFileDialog;
		private System.Windows.Forms.PictureBox GraphicsPictureBox;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.Panel DrawingPanel;
		private System.Windows.Forms.Panel HelpPanel;
		private System.Windows.Forms.DataGridView HelpGridView;
		private System.Windows.Forms.DataGridViewTextBoxColumn CommandColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn ParametersColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn DescriptionColumn;
		private System.Windows.Forms.ToolStripMenuItem saveImageToolStripMenuItem;
	}
}