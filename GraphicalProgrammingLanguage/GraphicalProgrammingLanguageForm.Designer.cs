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
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.codeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CodeInputTabControl = new System.Windows.Forms.TabControl();
			this.SingleLineInputTab = new System.Windows.Forms.TabPage();
			this.SingleLineOutput = new System.Windows.Forms.RichTextBox();
			this.SingleLineInputTextbox = new System.Windows.Forms.TextBox();
			this.MultiLineInputTab = new System.Windows.Forms.TabPage();
			this.MultiLineInputSaveBtn = new System.Windows.Forms.Button();
			this.MultiLineInputLoadBtn = new System.Windows.Forms.Button();
			this.MultiLineInputRunBtn = new System.Windows.Forms.Button();
			this.MultiLineInputTextBox = new System.Windows.Forms.RichTextBox();
			this.MultiLineInputLoadFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.MultiLineInputSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.GraphicsPictureBox = new System.Windows.Forms.PictureBox();
			this.TopMenuStrip.SuspendLayout();
			this.CodeInputTabControl.SuspendLayout();
			this.SingleLineInputTab.SuspendLayout();
			this.MultiLineInputTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.GraphicsPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// TopMenuStrip
			// 
			this.TopMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.TopMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.codeToolStripMenuItem});
			this.TopMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.TopMenuStrip.Name = "TopMenuStrip";
			this.TopMenuStrip.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
			this.TopMenuStrip.Size = new System.Drawing.Size(1389, 28);
			this.TopMenuStrip.TabIndex = 0;
			this.TopMenuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// codeToolStripMenuItem
			// 
			this.codeToolStripMenuItem.Name = "codeToolStripMenuItem";
			this.codeToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
			this.codeToolStripMenuItem.Text = "Code";
			// 
			// CodeInputTabControl
			// 
			this.CodeInputTabControl.Controls.Add(this.SingleLineInputTab);
			this.CodeInputTabControl.Controls.Add(this.MultiLineInputTab);
			this.CodeInputTabControl.Dock = System.Windows.Forms.DockStyle.Right;
			this.CodeInputTabControl.Location = new System.Drawing.Point(710, 28);
			this.CodeInputTabControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.CodeInputTabControl.Name = "CodeInputTabControl";
			this.CodeInputTabControl.SelectedIndex = 0;
			this.CodeInputTabControl.Size = new System.Drawing.Size(679, 612);
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
			this.SingleLineInputTab.Size = new System.Drawing.Size(671, 583);
			this.SingleLineInputTab.TabIndex = 0;
			this.SingleLineInputTab.Text = "Single Line Input";
			this.SingleLineInputTab.UseVisualStyleBackColor = true;
			// 
			// SingleLineOutput
			// 
			this.SingleLineOutput.BackColor = System.Drawing.Color.White;
			this.SingleLineOutput.Enabled = false;
			this.SingleLineOutput.Location = new System.Drawing.Point(19, 57);
			this.SingleLineOutput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.SingleLineOutput.Name = "SingleLineOutput";
			this.SingleLineOutput.Size = new System.Drawing.Size(623, 490);
			this.SingleLineOutput.TabIndex = 1;
			this.SingleLineOutput.Text = "";
			// 
			// SingleLineInputTextbox
			// 
			this.SingleLineInputTextbox.Location = new System.Drawing.Point(19, 18);
			this.SingleLineInputTextbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.SingleLineInputTextbox.Name = "SingleLineInputTextbox";
			this.SingleLineInputTextbox.Size = new System.Drawing.Size(623, 22);
			this.SingleLineInputTextbox.TabIndex = 0;
			this.SingleLineInputTextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SingleLineInputEnter);
			// 
			// MultiLineInputTab
			// 
			this.MultiLineInputTab.Controls.Add(this.MultiLineInputSaveBtn);
			this.MultiLineInputTab.Controls.Add(this.MultiLineInputLoadBtn);
			this.MultiLineInputTab.Controls.Add(this.MultiLineInputRunBtn);
			this.MultiLineInputTab.Controls.Add(this.MultiLineInputTextBox);
			this.MultiLineInputTab.Location = new System.Drawing.Point(4, 25);
			this.MultiLineInputTab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.MultiLineInputTab.Name = "MultiLineInputTab";
			this.MultiLineInputTab.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.MultiLineInputTab.Size = new System.Drawing.Size(671, 581);
			this.MultiLineInputTab.TabIndex = 1;
			this.MultiLineInputTab.Text = "Multi line Input";
			this.MultiLineInputTab.UseVisualStyleBackColor = true;
			// 
			// MultiLineInputSaveBtn
			// 
			this.MultiLineInputSaveBtn.Location = new System.Drawing.Point(128, 14);
			this.MultiLineInputSaveBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
			this.MultiLineInputLoadBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MultiLineInputLoadBtn.Name = "MultiLineInputLoadBtn";
			this.MultiLineInputLoadBtn.Size = new System.Drawing.Size(100, 28);
			this.MultiLineInputLoadBtn.TabIndex = 2;
			this.MultiLineInputLoadBtn.Text = "Load";
			this.MultiLineInputLoadBtn.UseVisualStyleBackColor = true;
			this.MultiLineInputLoadBtn.Click += new System.EventHandler(this.MultiLineInputLoadBtn_Click);
			// 
			// MultiLineInputRunBtn
			// 
			this.MultiLineInputRunBtn.Location = new System.Drawing.Point(557, 14);
			this.MultiLineInputRunBtn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MultiLineInputRunBtn.Name = "MultiLineInputRunBtn";
			this.MultiLineInputRunBtn.Size = new System.Drawing.Size(100, 28);
			this.MultiLineInputRunBtn.TabIndex = 1;
			this.MultiLineInputRunBtn.Text = "Run";
			this.MultiLineInputRunBtn.UseVisualStyleBackColor = true;
			// 
			// MultiLineInputTextBox
			// 
			this.MultiLineInputTextBox.Location = new System.Drawing.Point(20, 49);
			this.MultiLineInputTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
			this.GraphicsPictureBox.Location = new System.Drawing.Point(0, 28);
			this.GraphicsPictureBox.Name = "GraphicsPictureBox";
			this.GraphicsPictureBox.Size = new System.Drawing.Size(708, 612);
			this.GraphicsPictureBox.TabIndex = 3;
			this.GraphicsPictureBox.TabStop = false;
			this.GraphicsPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.GraphicsPictureBoxPaint);
			// 
			// GraphicalProgrammingLanguageForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1389, 640);
			this.Controls.Add(this.GraphicsPictureBox);
			this.Controls.Add(this.CodeInputTabControl);
			this.Controls.Add(this.TopMenuStrip);
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
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip TopMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem codeToolStripMenuItem;
		private System.Windows.Forms.TabControl CodeInputTabControl;
		private System.Windows.Forms.TabPage SingleLineInputTab;
		private System.Windows.Forms.RichTextBox SingleLineOutput;
		private System.Windows.Forms.TextBox SingleLineInputTextbox;
		private System.Windows.Forms.TabPage MultiLineInputTab;
		private System.Windows.Forms.RichTextBox MultiLineInputTextBox;
		private System.Windows.Forms.Button MultiLineInputRunBtn;
		private System.Windows.Forms.Button MultiLineInputSaveBtn;
		private System.Windows.Forms.Button MultiLineInputLoadBtn;
		private System.Windows.Forms.OpenFileDialog MultiLineInputLoadFileDialog;
		private System.Windows.Forms.SaveFileDialog MultiLineInputSaveFileDialog;
		private System.Windows.Forms.PictureBox GraphicsPictureBox;
	}
}