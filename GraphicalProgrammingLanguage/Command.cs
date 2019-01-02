using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
	public class Command
	{
		int currentX, currentY;

		public Command() {
			currentX = 0;
			currentY = 0;
		}

		private string[] commands = { "loop", "run", "penup", "pendown", "drawto", "moveto", "repeat",
			"circle", "rectangle", "square", "triangle", "polygon" };

		private bool penUp = false;

		Dictionary<string, string> variables = new Dictionary<string, string>();

		public string[] SplitUserInput(string userInput)
		{
			// Split the command based on a space to get the command and the parameters
			return userInput.Trim().Split(' ');
		}

		public string[] SplitParameters(string userInput, string splitBy)
		{
			// Split the parameters based on a comma
			string[] parameterSplit = userInput.Split(splitBy[0]).Select(parameter => parameter.Trim()).ToArray();
			return parameterSplit;
		}

		public bool ValidateCommand(int lineNumber, string commandString, string commandParameters, out string errorMessage)
		{
			// Check whether the command is valid
			if(!Array.Exists(commands, command => command == commandString.ToLower()))
			{
				string[] commandParametersSplit = SplitParameters(commandParameters, ",");

				if (commandString.Contains("="))
				{
					string[] variableSplit = commandString.Trim().Split('=').ToArray();
					string variableName = variableSplit[0];

					if(commandParametersSplit.Length > 1)
					{
						errorMessage = "You've defined more than one value to be stored as a variable.";
						return false;
					}
					else if(commandParametersSplit.Length < 1 && String.Equals(variableSplit[1], ""))
					{
						errorMessage = "You haven't defined a value to store with the variable.";
						return false;
					}
					else
					{
						errorMessage = "";
						return true;
					}
				}
				else if (commandParametersSplit[0].Contains("="))
				{
					if (String.IsNullOrWhiteSpace(commandString))
					{
						errorMessage = "You must define a variable name.";
						return false;
					}

					string[] splitVariable = commandParametersSplit[0].Split('=');

					if (String.IsNullOrWhiteSpace(splitVariable[1]))
					{
						errorMessage = "You must define a value for the variable.";
						return false;
					}
					else
					{
						errorMessage = "";
						return true;
					}
				}
				else
				{
					// The command is not recognised. This could mean the user is trying to perform another programming action and needs to be checked.
					errorMessage = commandString + " is an invalid command. Please see 'help' for a list of commands.";
					return false;
				}
			}

			if(commandString.ToLower() == "repeat")
			{
				string[] commandParametersSplit = SplitParameters(commandParameters, ",");
				if (commandParametersSplit.Length < 5)
				{
					errorMessage = "Repeat command must be in the format. 'Repeat <no of repeats>, <shape>, <colour>, <less/greater>, <incrementer>'";
					return false;
				}
				else if(!ValidateInteger(commandParametersSplit[0], out errorMessage))
				{
					// Validate the second parameter is a integer
					return false;
				}
				else if(!Array.Exists(commands, command => command == commandParametersSplit[1].ToLower()))
				{
					// Check the specified command is valid
					errorMessage = commandParametersSplit[1] + " is an invalid command. Please see 'help' for a list of commands.";
					return false;
				}
				else if(!ValidateColour(commandParametersSplit[2], out errorMessage))
				{
					// Validate the third parameter is a valid colour
					return false;
				}
				else if(!Regex.IsMatch(commandParametersSplit[3], "^[+-]$"))
				{
					// The repeat by conditional is not a + or -
					errorMessage = "The third parameter must instruct to repeat by + or - times.";
					return false;
				}
				else if(!ValidateInteger(commandParametersSplit[4], out errorMessage) && !String.Equals(commandParametersSplit[1].ToLower(), "polygon")
					&& !String.Equals(commandParametersSplit[1].ToLower(), "triangle")){
					// Validate the parameter to check for an integer
					return false;
				}
				else
				{
					// If the user has specified a shape that requires more parameters
					if(String.Equals(commandParametersSplit[1].ToLower(), "rectangle"))
					{
						if(commandParametersSplit.Length != 6)
						{
							errorMessage = "Repeat command must be in the format. 'Repeat <no of repeats>, <shape>, <colour>, <less/greater>, <incrementer_X>, <incrementer_Y>'";
							return false;
						}
						else if(!ValidateInteger(commandParametersSplit[5], out errorMessage))
						{
							return false;
						}
					}
					else if(String.Equals(commandParametersSplit[1].ToLower(), "triangle"))
					{
						if(commandParametersSplit.Length != 7)
						{
							errorMessage = "Repeat command must be in the format. 'Repeat <no of repeats>, <shape>, <colour>, <less/greater>, <point>, <point>, <point>'";
							return false;
						}
						else if(!ValidatePoint(commandParametersSplit[4], out errorMessage) || !ValidatePoint(commandParametersSplit[5], out errorMessage) ||
							!ValidatePoint(commandParametersSplit[6], out errorMessage)){
							return false;
						}
					}
					else if(String.Equals(commandParametersSplit[1].ToLower(), "polygon"))
					{
						if(commandParametersSplit.Length < 7)
						{
							errorMessage = "Repeat command must be in the format. 'Repeat <no of repeats>, <shape>, <colour>, <less/greater>, <point>, <point>, <point>'";
							return false;
						}
						else
						{
							string[] polygonPoints = commandParametersSplit.Skip(4).ToArray();

							for(int i = 0; i < polygonPoints.Length; i++)
							{
								if(!ValidatePoint(polygonPoints[i], out errorMessage))
								{
									return false;
								}
							}
						}
					}

					errorMessage = "";
					return true;
				}
			}
			else if(commandString.ToLower() == "loop")
			{
				string[] commandParametersSplit = SplitParameters(commandParameters, ";");
				// loop 3; rectangle, red, 10 20; moveto 10, 10;

				if (commandParametersSplit.Length < 2){
					errorMessage = "Incorrect loop format. E.g. 'Loop <number of loops>; <command>; <command>;...'";
					return false;
				}

				if (!ValidateInteger(commandParametersSplit[0], out errorMessage))
				{
					return false;
				}

				// Loop over the remaining commands and validate each command
				string[] loopActions = commandParametersSplit.Skip(1).ToArray();
				for (int i = 0; i < loopActions.Length; i++)
				{
					string[] loopActionsSplit = SplitUserInput(loopActions[i]);

					string loopActionsCommand = loopActionsSplit[0];
					string loopActionsParameter = string.Join(" ", loopActionsSplit.Skip(1).ToArray());

					if(loopActionsCommand.ToLower() == "penup")
					{
						penUp = true;
					} else if(loopActionsCommand.ToLower() == "pendown")
					{
						penUp = false;
					}

					if(!ValidateCommand(0, loopActionsCommand, loopActionsParameter, out errorMessage))
					{
						return false;
					}
				}

				errorMessage = "";
				return true;
			}
			else if(commandString.ToLower() == "run")
			{
				string[] commandParametersSplit = SplitParameters(commandParameters, ",");

				if (commandParametersSplit.Length != 1)
				{
					errorMessage = "The run command must have a file path specified";
					return false;
				}
				else if (!ValidateFile(commandParametersSplit[0], out errorMessage))
				{
					return false;
				}
				else
				{
					errorMessage = "";
					return true;
				}
			}
			else if(commandString.ToLower() == "moveto" && penUp)
			{
				string[] commandParametersSplit = SplitParameters(commandParameters, ",");

				if (commandParametersSplit.Length != 2)
				{
					// Check the correct number of parameters are passed
					errorMessage = "MoveTo expects 2 parameters to be passed.";
					return false;
				}
				else if (!ValidateInteger(commandParametersSplit[0], out errorMessage))
				{
					// Check the second parameter passed is a number
					errorMessage = "The first parameter " + commandParametersSplit[0] + " must be an integer.";
					return false;
				}
				else if (!ValidateInteger(commandParametersSplit[1], out errorMessage))
				{
					// Check the second parameter passed is a number
					errorMessage = "The second parameter " + commandParametersSplit[1] + " must be an integer.";
					return false;
				}
				else
				{
					// The moveto has been entered correctly
					errorMessage = "";
					return true;
				}
			}
			else if(commandString.ToLower() == "moveto" && !penUp)
			{
				string[] commandParametersSplit = SplitParameters(commandParameters, ",");

				if (commandParametersSplit.Length != 3)
				{
					errorMessage = "When the pen is down, MoveTo expects 3 parameters to be passed.";
					return false;
				}
				else if(!ValidateColour(commandParametersSplit[0], out errorMessage))
				{
					return false;
				}
				else if(!ValidateInteger(commandParametersSplit[1], out errorMessage))
				{
					return false;
				}
				else if(!ValidateInteger(commandParametersSplit[2], out errorMessage))
				{
					return false;
				}
				else
				{
					errorMessage = "";
					return true;
				}
			}
			else if(commandString.ToLower() == "circle")
			{
				string[] commandParametersSplit = SplitParameters(commandParameters, ",");

				if (commandParametersSplit.Length != 2)
				{
					// Check the correct number of parameters are passed
					errorMessage = "Circle expects 2 parameters to be passed.";
					return false;
				}
				else if (!ValidateColour(commandParametersSplit[0], out errorMessage))
				{
					// Check that the first parameter passed is a valid colour.
					return false;
				}
				else if (!ValidateInteger(commandParametersSplit[1], out errorMessage))
				{
					// Check the second parameter passed is a number
					return false;
				}
				else
				{
					// The circle has been entered correctly
					errorMessage = "";
					return true;
				}
			}
			else if(commandString.ToLower() == "rectangle")
			{
				string[] commandParametersSplit = SplitParameters(commandParameters, ",");

				if (commandParametersSplit.Length != 3)
				{
					// Check the correct number of parameters are passed
					errorMessage = "Rectangle expects 3 parameters to be passed.";
					return false;
				}
				else if(!ValidateColour(commandParametersSplit[0], out errorMessage))
				{
					// Check that the first parameter passed is a valid colour.
					return false;
				}
				else if(!ValidateInteger(commandParametersSplit[1], out errorMessage))
				{
					// Check the second parameter passed is a number
					return false;
				}
				else if(!ValidateInteger(commandParametersSplit[2], out errorMessage))
				{
					// Check the third parameter passed is a number
					return false;
				}
				else
				{
					// The rectangle has been entered correctly
					errorMessage = "";
					return true;
				}
			}
			else if(commandString.ToLower() == "square")
			{
				string[] commandParametersSplit = SplitParameters(commandParameters, ",");

				if (commandParametersSplit.Length != 2)
				{
					// Check the correct number of parameters are passed
					errorMessage = "Square expects 2 parameters to be passed.";
					return false;
				}
				else if (!ValidateColour(commandParametersSplit[0], out errorMessage))
				{
					// Check that the first parameter passed is a valid colour.
					return false;
				}
				else if (!ValidateInteger(commandParametersSplit[1], out errorMessage))
				{
					// Check the second parameter passed is a number
					return false;
				}
				else
				{
					// The square has been entered correctly
					errorMessage = "";
					return true;
				}
			}
			else if(commandString.ToLower() == "triangle")
			{
				string[] commandParametersSplit = SplitParameters(commandParameters, ",");

				if (commandParametersSplit.Length != 4)
				{
					// Check the correct number of parameters are passed
					errorMessage = "Triangle expects 4 parameters to be passed.";
					return false;
				}
				else if (!ValidateColour(commandParametersSplit[0], out errorMessage))
				{
					// Check that the first parameter passed is a valid colour.
					return false;
				}
				else
				{
					for (int i = 1; i < 4; i++)
					{
						if(!ValidatePoint(commandParametersSplit[i], out errorMessage))
						{
							return false;
						}
					}

					errorMessage = "";
					return true;
				}
			} 
			else if(commandString.ToLower() == "polygon")
			{
				string[] commandParametersSplit = SplitParameters(commandParameters, ",");

				if (commandParametersSplit.Length < 4)
				{
					errorMessage = "The minimum number of points in a polygon is 3.";
					return false;
				}
				else if (!ValidateColour(commandParametersSplit[0], out errorMessage))
				{
					return false;
				}
				else
				{
					for(int i = 1; i < (commandParametersSplit.Length - 1); i++)
					{
						if (!ValidatePoint(commandParametersSplit[i], out errorMessage))
						{
							return false;
						}
					}

					errorMessage = "";
					return true;
				}
			}
			else
			{
				errorMessage = "";
				return true;
			}
		}

		public bool ValidateInteger(string numberParameter, out string errorMessage)
		{
			if(Regex.IsMatch(numberParameter, "^[0-9]+$"))
			{
				errorMessage = "";
				return true;
			}
			else
			{
				if (variables.ContainsKey(numberParameter.Trim().ToUpper()))
				{
					ValidateInteger(variables[numberParameter.Trim().ToUpper()], out errorMessage);

					errorMessage = "";
					return true;
				}
				else
				{
				errorMessage = "'" + numberParameter + "' must be a positive integer.";
				return false;
				}
			}
		}

		public bool ValidateColour(string colourParameter, out string errorMessage)
		{
			if (Color.FromName(colourParameter).IsKnownColor)
			{
				errorMessage = "";
				return true;
			}
			else
			{
				errorMessage = "'" + colourParameter + "' is not a known colour.";
				return false;
			}
		}

		public bool ValidatePoint(string pointParameter, out string errorMessage)
		{
			if (variables.ContainsKey(pointParameter.Trim().ToUpper()))
			{
				ValidateInteger(variables[pointParameter.Trim().ToUpper()], out errorMessage);

				errorMessage = "";
				return true;
			}

			string[] points = pointParameter.Split(' ');

			if (points.Length != 2)
			{
				errorMessage = "Points " + pointParameter + " must have two points separated by a space.";
				return false;
			}

			for(int i = 0; i < points.Length; i++)
			{
				if (variables.ContainsKey(points[i].Trim().ToUpper()))
				{
					if (!Regex.IsMatch(variables[points[i].Trim().ToUpper()], "^[0-9]+$"))
					{
						errorMessage = "Points '" + variables[points[i].Trim().ToUpper()] + "' must be an integer at '" + pointParameter + "'.";
						return false;
					}
				}
				else if (!Regex.IsMatch(points[i].Trim(), "^[0-9]+$"))
				{
					errorMessage = "Points '" + points[i] + "' must be an integer at '" + pointParameter + "'.";
					return false;
				}
				else
				{
					errorMessage = "";
					return true;
				}
			}

			errorMessage = "";
			return true;
		}

		public bool ValidateFile(string filePath, out string errorMessage)
		{
			if (Path.GetExtension(filePath) != ".txt")
			{
				errorMessage = "The file extension must be .txt";
				return false;
			}
			else if (!File.Exists(filePath))
			{
				errorMessage = "The file " + filePath + " doesn't exist.";
				return false;
			}
			else
			{
				errorMessage = "";
				return true;
			}
		}

		public bool ExecuteCommand(ArrayList shapeCommands, string commandString, string commandParameters)
		{
			ShapeFactory shapeFactory = new ShapeFactory();

			commandString = commandString.ToUpper().Trim();
			if(commandString == "PENUP")
			{
				penUp = true;

				return true;
			}
			else if(commandString == "PENDOWN")
			{
				penUp = false;

				return true;
			}
			else if(commandString == "REPEAT")
			{
				string[] commandParametersSplit = SplitParameters(commandParameters, ",");

				// 'Repeat <no of iterations>, <shape>, <colour>, <command operator (+/=)>, <points>....'
				int numberOfIterations = Int32.Parse(commandParametersSplit[0]);
				string shapeCommandString = commandParametersSplit[1];
				string commandColour = commandParametersSplit[2];
				string commandOperator = commandParametersSplit[3];
				for(int i = 0; i < numberOfIterations; i++){
					if (String.Equals(shapeCommandString.ToUpper(), "SQUARE") || String.Equals(shapeCommandString.ToUpper(), "CIRCLE"))
					{
						// Get the point defined by the user
						var inputPoint = commandParametersSplit[4];
						int point = 0;
						int calculatedPoint;

						if (variables.ContainsKey(inputPoint.Trim().ToUpper()))
						{
							if(Regex.IsMatch(variables[inputPoint.Trim().ToUpper()], "^[0-9]+$"))
							{
								point = Int32.Parse(variables[inputPoint.Trim().ToUpper()]);
							}
							else
							{
								return false;
							}
						}
						else
						{
							point = Int32.Parse(inputPoint);
						}

						if (String.Equals(commandOperator, "+"))
						{
							calculatedPoint = point * (i + 1);
						}
						else
						{
							calculatedPoint = point / (i + 1);
						}

						string shapeCommandParameters = commandColour + "," + calculatedPoint.ToString();

						ExecuteCommand(shapeCommands, shapeCommandString, shapeCommandParameters);
					}
					else if(String.Equals(shapeCommandString.ToUpper(), "RECTANGLE"))
					{
						var inputPointX = commandParametersSplit[4];
						var inputPointY = commandParametersSplit[5];
						int pointX = 0;
						int pointY = 0;
						int calculatedPointX, calculatedPointY;

						if (variables.ContainsKey(inputPointX.Trim().ToUpper()))
						{
							if(Regex.IsMatch(variables[inputPointX.Trim().ToUpper()], "^[0-9]+$"))
							{
								pointX = Int32.Parse(variables[inputPointX.Trim().ToUpper()]);
							} 
							else
							{
								return false;
							}
						}
						else
						{
							pointX = Int32.Parse(inputPointX);
						}

						if (variables.ContainsKey(inputPointY.Trim().ToUpper()))
						{
							if (Regex.IsMatch(variables[inputPointY.Trim().ToUpper()], "^[0-9]+$"))
							{
								pointY = Int32.Parse(variables[inputPointY.Trim().ToUpper()]);
							}
							else
							{
								return false;
							}
						}
						else
						{
							pointY = Int32.Parse(inputPointY);
						}

						if (String.Equals(commandOperator, "+"))
						{
							calculatedPointX = pointX * (i + 1);
							calculatedPointY = pointY * (i + 1);
						}
						else
						{
							calculatedPointX = pointX / (i + 1);
							calculatedPointY = pointY / (i + 1);
						}

						string shapeCommandParameters = commandColour + "," + calculatedPointX.ToString() + "," + calculatedPointY.ToString();

						ExecuteCommand(shapeCommands, shapeCommandString, shapeCommandParameters);
					}
					else if(String.Equals(shapeCommandString.ToUpper(), "TRIANGLE"))
					{
						var inputPoint1 = commandParametersSplit[4];
						var inputPoint2 = commandParametersSplit[5];
						var inputPoint3 = commandParametersSplit[6];
						string points1, points2, points3;
						int calculatedPoint1X, calculatedPoint1Y, calculatedPoint2X, calculatedPoint2Y, calculatedPoint3X, calculatedPoint3Y;

						if (variables.ContainsKey(inputPoint1.Trim().ToUpper()))
						{
							points1 = variables[inputPoint1.Trim().ToUpper()];
						}
						else
						{
							points1 = inputPoint1;
						}

						if (variables.ContainsKey(inputPoint2.Trim().ToUpper()))
						{
							points2 = variables[inputPoint2.Trim().ToUpper()];
						}
						else
						{
							points2 = inputPoint2;
						}

						if (variables.ContainsKey(inputPoint3.Trim().ToUpper()))
						{
							points3 = variables[inputPoint3.Trim().ToUpper()];
						}
						else
						{
							points3 = inputPoint3;
						}

						string[] points1Split = points1.Split(' ').ToArray();
						string[] points2Split = points2.Split(' ').ToArray();
						string[] points3Split = points3.Split(' ').ToArray();

						if (String.Equals(commandOperator, "+"))
						{
							calculatedPoint1X = Int32.Parse(points1Split[0]);
							calculatedPoint1Y = Int32.Parse(points1Split[1]);
							calculatedPoint2X = Int32.Parse(points2Split[0]) * (i + 1);
							calculatedPoint2Y = Int32.Parse(points2Split[1]) * (i + 1);
							calculatedPoint3X = Int32.Parse(points3Split[0]) * (i + 1);
							calculatedPoint3Y = Int32.Parse(points3Split[1]) * (i + 1);
						}
						else
						{
							calculatedPoint1X = Int32.Parse(points1Split[0]);
							calculatedPoint1Y = Int32.Parse(points1Split[1]);
							calculatedPoint2X = Int32.Parse(points2Split[0]) / (i + 1);
							calculatedPoint2Y = Int32.Parse(points2Split[1]) / (i + 1);
							calculatedPoint3X = Int32.Parse(points3Split[0]) / (i + 1);
							calculatedPoint3Y = Int32.Parse(points3Split[1]) / (i + 1);
						}

						string shapeCommandParameters = commandColour + "," + (calculatedPoint1X + " " + calculatedPoint1Y) + "," +
						(calculatedPoint2X + " " + calculatedPoint2Y) + "," + (calculatedPoint3X + " " + calculatedPoint3Y);

						ExecuteCommand(shapeCommands, shapeCommandString, shapeCommandParameters);
					}
					else if(String.Equals(shapeCommandString.ToUpper(), "POLYGON"))
					{
						string[] parameterPoints = commandParametersSplit.Skip(4).ToArray();
						string shapeCommandParameters = commandColour; ;

						for(int d = 0; d < parameterPoints.Length; d++)
						{
							string points;

							if (variables.ContainsKey(parameterPoints[d].Trim().ToUpper()))
							{
								points = variables[parameterPoints[d].Trim().ToUpper()];
							}
							else
							{
								points = parameterPoints[d];
							}

							string[] splitPoints = points.Split(' ').ToArray();
							int splitPointX, splitPointY;

							if(String.Equals(commandParametersSplit[3], "+"))
							{
								splitPointX = Int32.Parse(splitPoints[0]) * (i + 1);
								splitPointY = Int32.Parse(splitPoints[1]) * (i + 1);
							}
							else
							{
								splitPointX = Int32.Parse(splitPoints[0]) / (i + 1);
								splitPointY = Int32.Parse(splitPoints[1]) / (i + 1);
							}

							shapeCommandParameters = shapeCommandParameters + "," + splitPointX.ToString() + " " + splitPointY.ToString();
						}

						ExecuteCommand(shapeCommands, shapeCommandString, shapeCommandParameters);
					}
				}

				return true;
			}
			else if(commandString == "LOOP")
			{
				string[] commandParametersSplit = SplitParameters(commandParameters, ";");

				string[] loopActions = commandParametersSplit.Skip(1).ToArray();
				for (int x = 0; x < Int32.Parse(commandParametersSplit[0]); x++)
				{
					for (int i = 0; i < loopActions.Length; i++)
					{
						string[] loopActionsSplit = SplitUserInput(loopActions[i]);

						string loopActionsCommand = loopActionsSplit[0];
						string loopActionsParameter = string.Join(" ", loopActionsSplit.Skip(1).ToArray());

						ExecuteCommand(shapeCommands, loopActionsCommand, loopActionsParameter);
					}
				}

				return true;
			}
			else if(commandString == "RECTANGLE")
			{
				string[] commandParametersSplit = SplitParameters(commandParameters, ",");

				string commandColour = commandParametersSplit[0];
				var inputPointX = commandParametersSplit[1];
				var inputPointY = commandParametersSplit[2];

				int pointX = 0;
				int pointY = 0;

				if(Regex.IsMatch(inputPointX, "^[0-9]+$"))
				{
					pointX = Int32.Parse(inputPointX);
				} 
				else if (variables.ContainsKey(inputPointX.Trim().ToUpper()))
				{
					pointX = Int32.Parse(variables[inputPointX.Trim().ToUpper()]);
				}
				else
				{
					return false;
				}

				if(Regex.IsMatch(inputPointY, "^[0-9]+$"))
				{
					pointY = Int32.Parse(inputPointY);
				}
				else if (variables.ContainsKey(inputPointY.Trim().ToUpper()))
				{
					pointY = Int32.Parse(variables[inputPointY.Trim().ToUpper()]);
				}
				else
				{
					return false;
				}

				// Get the shape and set the values
				Shape shape = shapeFactory.GetShape(commandString);
				shape.Set(Color.FromName(commandColour), currentX, currentY, pointX, pointY);

				// Add the shape to the ArrayList
				shapeCommands.Add(shape);

				return true;
			}
			else if(commandString == "SQUARE")
			{
				string[] commandParametersSplit = SplitParameters(commandParameters, ",");

				string commandColour = commandParametersSplit[0];
				var inputPoint = commandParametersSplit[1];
				int point = 0;

				if(Regex.IsMatch(inputPoint, "^[0-9]+$"))
				{
					point = Int32.Parse(inputPoint);
				}
				else if (variables.ContainsKey(inputPoint.Trim().ToUpper()))
				{
					if (Regex.IsMatch(variables[inputPoint.Trim().ToUpper()], "^[0-9]+$"))
					{
						point = Int32.Parse(variables[inputPoint.Trim().ToUpper()]);
					}
					else
					{
						return false;
					}
				}

				// Get the shape and set the values
				Shape shape = shapeFactory.GetShape(commandString);
				shape.Set(Color.FromName(commandColour), currentX, currentY, point, point);

				// Add the shape to the ArrayList
				shapeCommands.Add(shape);

				return true;
			}
			else if(commandString == "CIRCLE")
			{
				string[] commandParametersSplit = SplitParameters(commandParameters, ",");

				string commandColour = commandParametersSplit[0];
				var inputPoint = commandParametersSplit[1];
				int point = 0;

				if (Regex.IsMatch(inputPoint, "^[0-9]+$"))
				{
					point = Int32.Parse(inputPoint);
				}
				else if (variables.ContainsKey(inputPoint.Trim().ToUpper()))
				{
					if (Regex.IsMatch(variables[inputPoint.Trim().ToUpper()], "^[0-9]+$"))
					{
						point = Int32.Parse(variables[inputPoint.Trim().ToUpper()]);
					}
					else
					{
						return false;
					}
				}

				// Get the shape and set the values
				Shape shape = shapeFactory.GetShape(commandString);
				shape.Set(Color.FromName(commandColour), currentX, currentY, point);

				// Add the shape to the ArrayList
				shapeCommands.Add(shape);

				return true;
			}
			else if(commandString == "MOVETO" && penUp)
			{
				string[] commandParametersSplit = SplitParameters(commandParameters, ",");

				var inputPointX = commandParametersSplit[0];
				var inputPointY = commandParametersSplit[1];
				int pointX = 0;
				int pointY = 0;

				if(Regex.IsMatch(inputPointX, "^[0-9]+$"))
				{
					pointX = Int32.Parse(inputPointX);
				}
				else if (variables.ContainsKey(inputPointX.Trim().ToUpper()))
				{
					pointX = Int32.Parse(variables[inputPointX.Trim().ToUpper()]);
				}
				else
				{
					return false;
				}

				if(Regex.IsMatch(inputPointY, "^[0-9]+$"))
				{
					pointY = Int32.Parse(inputPointY);
				}
				else if (variables.ContainsKey(inputPointY.Trim().ToUpper()))
				{
					pointY = Int32.Parse(variables[inputPointY.Trim().ToUpper()]);
				}
				else
				{
					return false;
				}

				currentX = pointX;
				currentY = pointY;

				return true;
			}
			else if(commandString == "MOVETO" && !penUp)
			{
				string[] commandParametersSplit = SplitParameters(commandParameters, ",");

				string commandColour = commandParametersSplit[0];
				var inputPointX = commandParametersSplit[1];
				var inputPointY = commandParametersSplit[2];
				int pointX = 0;
				int pointY = 0;

				if (Regex.IsMatch(inputPointX, "^[0-9]+$"))
				{
					pointX = Int32.Parse(inputPointX);
				}
				else if (variables.ContainsKey(inputPointX.Trim().ToUpper()))
				{
					pointX = Int32.Parse(variables[inputPointX.Trim().ToUpper()]);
				}
				else
				{
					return false;
				}

				if (Regex.IsMatch(inputPointY, "^[0-9]+$"))
				{
					pointY = Int32.Parse(inputPointY);
				}
				else if (variables.ContainsKey(inputPointY.Trim().ToUpper()))
				{
					pointY = Int32.Parse(variables[inputPointY.Trim().ToUpper()]);
				}
				else
				{
					return false;
				}

				// Get the shape and set the values
				Shape shape = shapeFactory.GetShape(commandString);
				shape.Set(Color.FromName(commandColour), currentX, currentY, pointX, pointY);

				shapeCommands.Add(shape);

				currentX = pointX;
				currentY = pointY;

				return true;
			}
			else if(commandString == "TRIANGLE")
			{
				string[] commandParametersSplit = SplitParameters(commandParameters, ",");

				string commandColour = commandParametersSplit[0];
				var inputPoint1 = commandParametersSplit[1];
				var inputPoint2 = commandParametersSplit[2];
				var inputPoint3 = commandParametersSplit[3];

				int point1X, point1Y, point2X, point2Y, point3X, point3Y = 0;

				if (variables.ContainsKey(inputPoint1.Trim().ToUpper()))
				{
					string[] points1 = variables[inputPoint1.Trim().ToUpper()].Split(' ');

					if(!Regex.IsMatch(points1[0], "^[0-9]+$") || !Regex.IsMatch(points1[1], "^[0-9]+$")){
						return false;
					}
					else
					{
						point1X = Int32.Parse(points1[0]);
						point1Y = Int32.Parse(points1[1]);
					}
				}
				else
				{
					string[] point1 = inputPoint1.Split(' ');
					point1X = Int32.Parse(point1[0]);
					point1Y = Int32.Parse(point1[1]);
				}


				if (variables.ContainsKey(inputPoint2.Trim().ToUpper()))
				{
					string[] points2 = variables[inputPoint2.Trim().ToUpper()].Split(' ');

					if (!Regex.IsMatch(points2[0], "^[0-9]+$") || !Regex.IsMatch(points2[1], "^[0-9]+$"))
					{
						return false;
					}
					else
					{
						point2X = Int32.Parse(points2[0]);
						point2Y = Int32.Parse(points2[1]);
					}
				}
				else
				{
					string[] points2 = inputPoint2.Split(' ');

					point2X = Int32.Parse(points2[0]);
					point2Y = Int32.Parse(points2[1]);
				}

				if (variables.ContainsKey(inputPoint3.Trim().ToUpper()))
				{
					string[] points3 = variables[inputPoint3.Trim().ToUpper()].Split(' ');

					if (!Regex.IsMatch(points3[0], "^[0-9]+$") || !Regex.IsMatch(points3[1], "^[0-9]+$"))
					{
						return false;
					}
					else
					{
						point3X = Int32.Parse(points3[0]);
						point3Y = Int32.Parse(points3[1]);
					}
				}
				else
				{
					string[] points3 = inputPoint3.Split(' ');

					point3X = Int32.Parse(points3[0]);
					point3Y = Int32.Parse(points3[1]);
				}

				// Get the shape and set the values
				Shape shape = shapeFactory.GetShape(commandString);
				shape.Set(Color.FromName(commandColour), currentX, currentY, point1X, point1Y, point2X, point2Y, point3X, point3Y);

				// Add the shape to the ArrayList
				shapeCommands.Add(shape);

				return true;
			}
			else if(commandString == "POLYGON")
			{
				string[] commandParametersSplit = SplitParameters(commandParameters, ",");

				string commandColour = commandParametersSplit[0];

				List<int> integerPoints = new List<int>();
				integerPoints.Add(currentX);
				integerPoints.Add(currentY);

				// Create a new array without the colour
				string[] pointsList = commandParametersSplit.Skip(1).ToArray();
				// Cycle through the new array setting each colour to a new nteger array
				for(int i = 0; i < pointsList.Length; i++)
				{
					int pointX = 0;
					int pointY = 0;

					if (variables.ContainsKey(pointsList[i].Trim().ToUpper()))
					{
						string[] pointsSplit = variables[pointsList[i].Trim().ToUpper()].Split(' ').ToArray();

						if(Regex.IsMatch(pointsSplit[0], "^[0-9]+$") && Regex.IsMatch(pointsSplit[1], "^[0-9]+$"))
						{
							pointX = Int32.Parse(pointsSplit[0]);
							pointY = Int32.Parse(pointsSplit[1]);
						} 
						else
						{
							return false;
						}
					}
					else
					{
						string[] pointsSplit = pointsList[i].Split(' ').ToArray();

						pointX = Int32.Parse(pointsSplit[0]);
						pointY = Int32.Parse(pointsSplit[1]);
					}

					integerPoints.Add(pointX);
					integerPoints.Add(pointY);
				}

				Shape shape = shapeFactory.GetShape(commandString);
				shape.Set(Color.FromName(commandColour), integerPoints.ToArray());

				shapeCommands.Add(shape);

				return true;
			}
			else
			{
				// Ensure the command isn't a variable reference
				string[] commandParametersSplit = SplitParameters(commandParameters, ",");
				if (commandString.Contains("="))
				{
					string[] variableSplit = commandString.Trim().Split('=').ToArray();
					string variableName = variableSplit[0];

					if (variables.ContainsKey(variableName.Trim().ToUpper()))
					{
						variables[variableName.Trim().ToUpper()] = variableSplit[1].Trim();
						return true;
					}
					else
					{
						variables.Add(variableName.Trim().ToUpper(), variableSplit[1].Trim());
						return true;
					}
				}
				else if (commandParametersSplit[0].Contains("="))
				{
					string[] splitVariable = commandParametersSplit[0].Split('=');
					int variableValue = 0;

					if(splitVariable[1].Contains("+"))
					{
						string[] splitVariableOnOperator = splitVariable[1].Split('+');

						for(int d = 0; d < splitVariableOnOperator.Length; d++)
						{
							if (variables.ContainsKey(splitVariableOnOperator[d].Trim().ToUpper()))
							{
								variableValue = variableValue + Int32.Parse(variables[splitVariableOnOperator[d].Trim().ToUpper()]);
							}
							else
							{
								variableValue = variableValue + Int32.Parse(splitVariableOnOperator[d]);
							}
						}
					}
					else if(splitVariable[1].Contains("-"))
					{
						string[] splitVariableOnOperator = splitVariable[1].Split('-');

						for (int d = 0; d < splitVariableOnOperator.Length; d++)
						{
							if (variables.ContainsKey(splitVariableOnOperator[d].Trim().ToUpper()))
							{
								variableValue = variableValue - Int32.Parse(variables[splitVariableOnOperator[d].Trim().ToUpper()]);
							}
							else
							{
								variableValue = variableValue - Int32.Parse(splitVariableOnOperator[d]);
							}
						}
					}
					else
					{
						variableValue = Int32.Parse(splitVariable[1]);
					}

					if (variables.ContainsKey(commandString.Trim().ToUpper()))
					{
						variables[commandString.Trim().ToUpper()] = variableValue.ToString();
						return true;
					}
					else
					{
						variables.Add(commandString.Trim().ToUpper(), variableValue.ToString());
						return true;
					}
				}
				else
				{
					return false;
				}
			}
		}

	}
}
