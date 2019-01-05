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

				if (!String.IsNullOrWhiteSpace(singleLineInputTextBox.Text))
				{
					bool validCommand = false;
					string errorMessage = "";

					string[] splitUserInput = command.SplitUserInput(singleLineInputTextBox.Text.Trim());

					string commandString = splitUserInput[0];
					string commandParameters = string.Join(" ", splitUserInput.Skip(1).ToArray());

					validCommand = command.ValidateCommand(1, commandString, commandParameters, out errorMessage);

					if (!validCommand)
					{
						// If the user has not entered a valid command
						this.SingleLineOutput.SelectionColor = Color.Red;
						this.SingleLineOutput.AppendText(errorMessage + "\n");
						this.SingleLineOutput.ScrollToCaret();
					}
					else if(validCommand && String.Equals(errorMessage, "multilineLoop"))
					{
						// Do nothing
					}
					else
					{
						if (commandString.ToLower() == "run")
						{
							RunFileCommand(commandParameters);
						}
						else
						{
							// Else if the user has entered a valid command
							// Add the command to the textbox
							this.SingleLineOutput.SelectionColor = Color.Green;
							this.SingleLineOutput.AppendText(SingleLineInputTextbox.Text.Trim() + "\n");
							this.SingleLineOutput.ScrollToCaret();

							// Set the TextBox value to be an empty string
							singleLineInputTextBox.Text = "";

							if (command.ExecuteCommand(shapes, commandString, commandParameters))
							{
								this.GraphicsPictureBox.Refresh();
							}
						}
					}
				}
			}
		}

		public void RunFileCommand(string filePath)
		{
			// Output to the user that file is attempting to be loaded.
			this.SingleLineOutput.SelectionColor = Color.Green;
			this.SingleLineOutput.AppendText("Attempting to load file (" + filePath + ") \n");
			this.SingleLineOutput.ScrollToCaret();

			SingleLineInputTextbox.Text = "";

			string errorMessage;
			if (!command.ValidateFile(filePath, out errorMessage))
			{
				this.SingleLineOutput.SelectionColor = Color.Red;
				this.SingleLineOutput.AppendText(errorMessage);
				this.SingleLineOutput.ScrollToCaret();
			}
			else
			{
				// Attempt to load the file
				System.IO.StreamReader file = new System.IO.StreamReader(filePath);

				string line;
				int lineNumber = 0;
				while ((line = file.ReadLine()) != null)
				{
					// Iterate the file line by line
					// Increment the line number
					lineNumber++;

					string[] splitUserInput = line.Split(' ');

					// Split the line of the file into command and parameters
					string commandString = splitUserInput[0];
					string commandParameters = string.Join(" ", splitUserInput.Skip(1).ToArray());

					// Pass each line to the validator along with the line number
					bool validCommand = command.ValidateCommand(lineNumber, commandString, commandParameters, out errorMessage);

					if (!validCommand)
					{
						// If the user has not entered a valid command
						this.SingleLineOutput.SelectionColor = Color.Red;
						this.SingleLineOutput.AppendText("Line: " + lineNumber + " - " + errorMessage + "\n");
						this.SingleLineOutput.ScrollToCaret();
					}
					else
					{
						// Else if the user has entered a valid command
						// Add the command to the textbox
						this.SingleLineOutput.SelectionColor = Color.Green;
						this.SingleLineOutput.AppendText("Line: " + lineNumber + " - " + line + "\n");
						this.SingleLineOutput.ScrollToCaret();


						if (command.ExecuteCommand(shapes, commandString, commandParameters))
						{
							this.GraphicsPictureBox.Refresh();
						}
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

		private void HomeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.HelpPanel.Visible = false;
			this.DrawingPanel.Visible = true;
		}

		private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.HelpGridView.Rows.Clear();

			string[][] rows = new string[][] {
				new string[] { "Clear", "", "Clears the current shapes on the canvas." },
				new string[] { "Loop", "<Integer: Number of times to loop>; <Shape Command: The shape to draw>; ...", "Single line loop which based on the first parameter will iterate the commands." },
				new string[] { "Loop", "<Integer: NUmber of times to loop> NEW LINE <Shape Command: The shape to draw> NEW LINE EndLoop", "Draws a square in the specified colour." },
				new string[] { "Run", "<String: File path to a file to run>", "Run commands from a specified file." },
				new string[] { "MoveTo", "<Colour: Shape colour>, <Integer: X value>, <Integer: Y value>", "If the pen is down MoveTo will draw a line to the specified X and Y." },
				new string[] { "MoveTo", "<Integer: X value>, <Integer: Y value>", "If the pen is up MoveTo will move the reference to the specified x and y." },
				new string[] { "Square", "<Colour: Shape colour>, <Integer: Width>", "Draws a square in the specified colour." },
				new string[] { "Circle", "<Colour: Shape colour>, <Integer: Diameter>", "Draws a circle in the specified colour." },
				new string[] { "Rectangle", "<Colour: Shape colour>, <Integer: Width>, <Integer: Height>", "Draws a rectangle in the specified colour." },
				new string[] { "Triangle", "<Colour: Shape colour>, <Integer: Point 1x> <Integer: Point1y>, <Integer: Point2a> <Integer: Point2b>, <Integer: Point3a> <Integer: Point3b>", "Draws a triangle in the specified colour." },
				new string[] { "Polygon", "<Colour: Shape colour>, <Integer: Point 1x> <Integer: Point1y>, <Integer: Point2a> <Integer: Point2b>, <Integer: Point3a> <Integer: Point3b>", "Draws a polygon in the specified colour." },
				new string[] { "Repeat", "<Integer: Number of repeats>, <Shape Command: The shape to draw>, <Color: shape colour>, <Optional: Either + or ->, <Integer: Starting point>", "Incrementally repeats a shape command and either + or - the starting point each repeat." }
			};

			for(int i = 0; i < rows.Length; i++)
			{
				this.HelpGridView.Rows.Add(rows[i]);
			}

			this.HelpPanel.Visible = true;
			this.DrawingPanel.Visible = false;
		}
	}
}
