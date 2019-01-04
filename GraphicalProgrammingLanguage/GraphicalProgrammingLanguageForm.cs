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

		// Whether the user is currently in a multi-line action
		bool inMultiLineCommand = false;
		List<string> multiLineCommands = new List<String>();

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

					// If whether the user has entered a loop
					if(String.Equals(commandString.ToLower(), "loop"))
					{
						// Split the commandParameters based on a space
						string[] loopSplit = commandParameters.Split(' ').ToArray();
						string[] loopParameters = commandParameters.Split('\n', '\r').ToArray();

						if (inMultiLineCommand)
						{
							// Already in a multi line loop
							// Split the loopParameters based on new lines and check whether the last entered command was ENDLOOP
							if(String.Equals(loopParameters[loopParameters.Length - 1].ToLower(), "endloop"))
							{
								if (multiLineCommands.Count < 2)
								{
									// If the user has not entered a valid command
									this.SingleLineOutput.SelectionColor = Color.Red;
									this.SingleLineOutput.AppendText("The loop was emty." + "\n");
									this.SingleLineOutput.ScrollToCaret();

									// Set the TextBox value to be an empty string
									singleLineInputTextBox.Text = "";
								}
								else
								{
									// The user wants to quit the loop
									for (int i = 1; i < Int32.Parse(multiLineCommands[0]); i++)
									{
										for (int d = 1; d < (multiLineCommands.Count); d++)
										{

											// Split the user input
											string[] loopInputSplit = command.SplitUserInput(multiLineCommands[d].Trim());

											string loopInputCommand = loopInputSplit[0];
											string loopInputParameters = string.Join(" ", loopInputSplit.Skip(1).ToArray());

											command.ExecuteCommand(shapes, loopInputCommand, loopInputParameters);

											// Else if the user has entered a valid command
											// Add the command to the textbox
											this.SingleLineOutput.SelectionColor = Color.Green;
											this.SingleLineOutput.AppendText(multiLineCommands[d].Trim() + "\n");
											this.SingleLineOutput.ScrollToCaret();

											// Set the TextBox value to be an empty string
											singleLineInputTextBox.Text = "";
										}
									}
									this.GraphicsPictureBox.Refresh();

									// Reset the loop variables
									inMultiLineCommand = false;
									multiLineCommands.Clear();
								}
							}
							else
							{
								// Split the last command of the loop
								string lastLoopInput= loopParameters[loopParameters.Length - 1];

								string[] lastLoopInputSplit = command.SplitUserInput(lastLoopInput.Trim());

								string lastLoopCommand = lastLoopInputSplit[0];
								string lastLoopParameter = string.Join(" ", lastLoopInputSplit.Skip(1).ToArray());

								// Validate the command 
								if(!command.ValidateCommand(1, lastLoopCommand, lastLoopParameter, out errorMessage))
								{
									validCommand = false;
								}
								else
								{
									multiLineCommands.Add(lastLoopInput);
									validCommand = true;
									return;
								}
							}
						}
						else if(loopSplit.Length == 1 && !String.IsNullOrWhiteSpace(loopSplit[0]))
						{
							// The user is trying to start a multi line loop
							// If the inMultiLineCommand is not equal to true, ensure the second parameter is an integer incrementer
							if (!command.ValidateInteger(loopParameters[0], out errorMessage))
							{
								// Second parameter was not an integer
								validCommand = false;
							}
							else
							{
								// This is the first line of the multi line loop
								inMultiLineCommand = true;

								multiLineCommands.Add(loopParameters[0]);
							}
						}
						else if(loopSplit.Length < 2)
						{
							// The user has entered an invalid loop command
							errorMessage = "The loop structure is either - 'loop <iterator>; <command>; <command>; OR  \n'loop <iterator>; \n" +
								"<command>\n" + "endLoop'";
							validCommand = false;
						}
						else
						{
							validCommand = command.ValidateCommand(1, commandString, commandParameters, out errorMessage);
						}
					}
					else
					{
						validCommand = command.ValidateCommand(1, commandString, commandParameters, out errorMessage);
					}

					if (!validCommand)
					{
						// If the user has not entered a valid command
						this.SingleLineOutput.SelectionColor = Color.Red;
						this.SingleLineOutput.AppendText(errorMessage + "\n");
						this.SingleLineOutput.ScrollToCaret();
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
	}
}
