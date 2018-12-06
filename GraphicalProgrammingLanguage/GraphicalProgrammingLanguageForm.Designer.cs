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
			this.GraphicsPanel = new System.Windows.Forms.Panel();
			this.MultiLineInputTab = new System.Windows.Forms.TabControl();
			this.SingleLineInputTab = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.GraphicsPanel.SuspendLayout();
			this.MultiLineInputTab.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// GraphicsPanel
			// 
			this.GraphicsPanel.Controls.Add(this.menuStrip1);
			this.GraphicsPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.GraphicsPanel.Location = new System.Drawing.Point(0, 0);
			this.GraphicsPanel.Name = "GraphicsPanel";
			this.GraphicsPanel.Size = new System.Drawing.Size(731, 640);
			this.GraphicsPanel.TabIndex = 0;
			// 
			// MultiLineInputTab
			// 
			this.MultiLineInputTab.Controls.Add(this.SingleLineInputTab);
			this.MultiLineInputTab.Controls.Add(this.tabPage2);
			this.MultiLineInputTab.Dock = System.Windows.Forms.DockStyle.Right;
			this.MultiLineInputTab.Location = new System.Drawing.Point(737, 0);
			this.MultiLineInputTab.Name = "MultiLineInputTab";
			this.MultiLineInputTab.SelectedIndex = 0;
			this.MultiLineInputTab.Size = new System.Drawing.Size(653, 640);
			this.MultiLineInputTab.TabIndex = 1;
			// 
			// SingleLineInputTab
			// 
			this.SingleLineInputTab.Location = new System.Drawing.Point(4, 25);
			this.SingleLineInputTab.Name = "SingleLineInputTab";
			this.SingleLineInputTab.Padding = new System.Windows.Forms.Padding(3);
			this.SingleLineInputTab.Size = new System.Drawing.Size(645, 611);
			this.SingleLineInputTab.TabIndex = 0;
			this.SingleLineInputTab.Text = "Single Line Input";
			this.SingleLineInputTab.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.button3);
			this.tabPage2.Controls.Add(this.button2);
			this.tabPage2.Controls.Add(this.button1);
			this.tabPage2.Controls.Add(this.richTextBox1);
			this.tabPage2.Location = new System.Drawing.Point(4, 25);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(645, 611);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Multi Line Input";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(16, 57);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(610, 529);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(16, 16);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(91, 35);
			this.button1.TabIndex = 1;
			this.button1.Text = "Load File";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(113, 16);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(110, 35);
			this.button2.TabIndex = 2;
			this.button2.Text = "Save to File";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(535, 16);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(91, 35);
			this.button3.TabIndex = 3;
			this.button3.Text = "Execute";
			this.button3.UseVisualStyleBackColor = true;
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.menuToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(731, 28);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// menuToolStripMenuItem
			// 
			this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
			this.menuToolStripMenuItem.Size = new System.Drawing.Size(58, 24);
			this.menuToolStripMenuItem.Text = "Menu";
			// 
			// GraphicalProgrammingLanguageForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1390, 640);
			this.Controls.Add(this.MultiLineInputTab);
			this.Controls.Add(this.GraphicsPanel);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "GraphicalProgrammingLanguageForm";
			this.Text = "GraphicalProgrammingLanguageInterface";
			this.GraphicsPanel.ResumeLayout(false);
			this.GraphicsPanel.PerformLayout();
			this.MultiLineInputTab.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel GraphicsPanel;
		private System.Windows.Forms.TabControl MultiLineInputTab;
		private System.Windows.Forms.TabPage SingleLineInputTab;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
	}
}