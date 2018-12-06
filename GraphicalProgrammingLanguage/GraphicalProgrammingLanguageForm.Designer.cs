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
			this.GraphicalPanel = new System.Windows.Forms.Panel();
			this.CodeInputTabControl = new System.Windows.Forms.TabControl();
			this.SingleLineInputTab = new System.Windows.Forms.TabPage();
			this.MultiLineInputTab = new System.Windows.Forms.TabPage();
			this.SingleLineInputTextbox = new System.Windows.Forms.TextBox();
			this.SingleLineOutput = new System.Windows.Forms.RichTextBox();
			this.TopMenuStrip.SuspendLayout();
			this.CodeInputTabControl.SuspendLayout();
			this.SingleLineInputTab.SuspendLayout();
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
			this.TopMenuStrip.Size = new System.Drawing.Size(1390, 28);
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
			// GraphicalPanel
			// 
			this.GraphicalPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.GraphicalPanel.Location = new System.Drawing.Point(0, 28);
			this.GraphicalPanel.Name = "GraphicalPanel";
			this.GraphicalPanel.Size = new System.Drawing.Size(705, 612);
			this.GraphicalPanel.TabIndex = 1;
			// 
			// CodeInputTabControl
			// 
			this.CodeInputTabControl.Controls.Add(this.SingleLineInputTab);
			this.CodeInputTabControl.Controls.Add(this.MultiLineInputTab);
			this.CodeInputTabControl.Dock = System.Windows.Forms.DockStyle.Right;
			this.CodeInputTabControl.Location = new System.Drawing.Point(711, 28);
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
			this.SingleLineInputTab.Name = "SingleLineInputTab";
			this.SingleLineInputTab.Padding = new System.Windows.Forms.Padding(3);
			this.SingleLineInputTab.Size = new System.Drawing.Size(671, 583);
			this.SingleLineInputTab.TabIndex = 0;
			this.SingleLineInputTab.Text = "Single Line Input";
			this.SingleLineInputTab.UseVisualStyleBackColor = true;
			// 
			// MultiLineInputTab
			// 
			this.MultiLineInputTab.Location = new System.Drawing.Point(4, 25);
			this.MultiLineInputTab.Name = "MultiLineInputTab";
			this.MultiLineInputTab.Padding = new System.Windows.Forms.Padding(3);
			this.MultiLineInputTab.Size = new System.Drawing.Size(671, 583);
			this.MultiLineInputTab.TabIndex = 1;
			this.MultiLineInputTab.Text = "Multi line Input";
			this.MultiLineInputTab.UseVisualStyleBackColor = true;
			// 
			// SingleLineInputTextbox
			// 
			this.SingleLineInputTextbox.Location = new System.Drawing.Point(19, 19);
			this.SingleLineInputTextbox.Name = "SingleLineInputTextbox";
			this.SingleLineInputTextbox.Size = new System.Drawing.Size(622, 22);
			this.SingleLineInputTextbox.TabIndex = 0;
			// 
			// SingleLineOutput
			// 
			this.SingleLineOutput.Enabled = false;
			this.SingleLineOutput.Location = new System.Drawing.Point(19, 57);
			this.SingleLineOutput.Name = "SingleLineOutput";
			this.SingleLineOutput.Size = new System.Drawing.Size(622, 490);
			this.SingleLineOutput.TabIndex = 1;
			this.SingleLineOutput.Text = "";
			// 
			// GraphicalProgrammingLanguageForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1390, 640);
			this.Controls.Add(this.CodeInputTabControl);
			this.Controls.Add(this.GraphicalPanel);
			this.Controls.Add(this.TopMenuStrip);
			this.Name = "GraphicalProgrammingLanguageForm";
			this.Text = "GraphicalProgrammingLanguageInterface";
			this.TopMenuStrip.ResumeLayout(false);
			this.TopMenuStrip.PerformLayout();
			this.CodeInputTabControl.ResumeLayout(false);
			this.SingleLineInputTab.ResumeLayout(false);
			this.SingleLineInputTab.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip TopMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem codeToolStripMenuItem;
		private System.Windows.Forms.Panel GraphicalPanel;
		private System.Windows.Forms.TabControl CodeInputTabControl;
		private System.Windows.Forms.TabPage SingleLineInputTab;
		private System.Windows.Forms.RichTextBox SingleLineOutput;
		private System.Windows.Forms.TextBox SingleLineInputTextbox;
		private System.Windows.Forms.TabPage MultiLineInputTab;
	}
}