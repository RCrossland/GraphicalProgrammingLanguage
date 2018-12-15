using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicalProgrammingLanguage
{
	public partial class GraphicalProgrammingLanguageForm : Form
	{
		ArrayList shapes = new ArrayList();
		Command command = new Command();

		public GraphicalProgrammingLanguageForm()
		{
			InitializeComponent();
		}

		// Single Line Input
		private void SingleLineInputEnter(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				// Save the object of the textbox
				TextBox singleLineInputTextBox = (sender as TextBox);
				string[] splitUserInput = command.SplitUserInput(singleLineInputTextBox.Text);

				string commandString = splitUserInput[0];
				string[] commandParameters = command.SplitParameters(splitUserInput);

				// Validate the command and hold the error message returned in a variable
				string errorMessage;
				bool validCommand = command.ValidateCommand(1, commandString, commandParameters, out errorMessage);

				if (!validCommand)
				{
					// If the user has not entered a valid command
					this.SingleLineOutput.SelectionColor = Color.Red;
					this.SingleLineOutput.AppendText(errorMessage + "\n");
					this.SingleLineOutput.ScrollToCaret();
				}
				else
				{
					// Else if the user has entered a valid command
					// Add the command to the textbox
					this.SingleLineOutput.SelectionColor = Color.Green;
					this.SingleLineOutput.AppendText(SingleLineInputTextbox.Text + "\n");
					this.SingleLineOutput.ScrollToCaret();

					// Set the TextBox value to be an empty string
					singleLineInputTextBox.Text = "";

					if(command.ExecuteCommand(shapes, commandString, commandParameters))
					{
						this.GraphicsPictureBox.Refresh();
					}
				}
			}
		}

		// Multi Line Input
		private void MultiLineInputLoadBtn_Click(object sender, EventArgs e)
		{
			if (MultiLineInputLoadFileDialog.ShowDialog() == DialogResult.OK)
			{
				System.IO.StreamReader sr = new System.IO.StreamReader(MultiLineInputLoadFileDialog.FileName);
				MultiLineInputTextBox.Text = sr.ReadToEnd();
				sr.Close();
			}
		}

		private void MultiLineInputSaveBtn_Click(object sender, EventArgs e)
		{
			if (MultiLineInputSaveFileDialog.ShowDialog() == DialogResult.OK)
			{
				MultiLineInputTextBox.SaveFile(MultiLineInputSaveFileDialog.FileName, RichTextBoxStreamType.PlainText);
			}
		}

		private void GraphicsPictureBoxPaint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			for(int i = 0; i < shapes.Count; i++)
			{
				Shape s = (Shape) shapes[i];
				s.Draw(g);
			}
		}
	}
}
