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

		private string[] commands = { "run", "penup", "pendown", "drawto", "moveto", "repeat",
			"circle", "rectangle", "square", "triangle", "polygon" };

		private bool penUp = false;

		Dictionary<string, string> variables = new Dictionary<string, string>();

		public string[] SplitUserInput(string userInput)
		{
			// Split the command based on a space to get the command and the parameters
			return userInput.Trim().Split(' ');
		}

		public string[] SplitParameters(string[] userInput)
		{
			// Remove the command from the array to only leave parameters
			string[] parameters = userInput.Skip(1).ToArray();
			// Put the parameters array back to a string based on spaces
			string parametersString = string.Join(" ", parameters).Trim();
			// Split the parameters based on a comma
			string[] parameterSplit = parameters.Length > 0 ? parametersString.Split(',').Select(parameter => parameter.Trim()).ToArray() : new string[0];
			return parameterSplit;
		}

		public bool ValidateCommand(int lineNumber, string commandString, string[] commandParameters, out string errorMessage)
		{
			// Check whether the command is valid
			if(!Array.Exists(commands, command => command == commandString.ToLower()))
			{
				if (commandString.Contains("="))
				{
					string[] variableSplit = commandString.Trim().Split('=').ToArray();
					string variableName = variableSplit[0];

					if(commandParameters.Length > 1)
					{
						errorMessage = "You've defined more than one value to be stored as a variable.";
						return false;
					}
					else if(commandParameters.Length < 1 && String.Equals(variableSplit[1], ""))
					{
						errorMessage = "You haven't defined a value to store with the variable.";
						return false;
					}
					else if (variables.ContainsKey(variableName))
					{
						variables[variableName] = variableSplit[1].Trim();

						errorMessage = "";
						return true;
					}
					else
					{
						variables.Add(variableName, variableSplit[1].Trim());

						errorMessage = "";
						return true;
					}
				}
				else if (commandParameters[0].Contains("="))
				{
					if (String.IsNullOrWhiteSpace(commandString))
					{
						errorMessage = "You must define a variable name.";
						return false;
					}

					string[] splitVariable = commandParameters[0].Split('=');

					if (String.IsNullOrWhiteSpace(splitVariable[1]))
					{
						errorMessage = "You must define a value for the variable.";
						return false;
					}
					else if(variables.ContainsKey(commandString))
					{
						variables[commandString] = splitVariable[1].Trim();

						errorMessage = "";
						return true;
					}
					else
					{
						variables.Add(commandString, splitVariable[1].Trim());

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
				if (commandParameters.Length < 5)
				{
					errorMessage = "Repeat command must be in the format. 'Repeat <no of repeats>, <shape>, <colour>, <less/greater>, <incrementer>'";
					return false;
				}
				else if(!ValidateInteger(commandParameters[0], out errorMessage))
				{
					// Validate the second parameter is a integer
					return false;
				}
				else if(!Array.Exists(commands, command => command == commandParameters[1]))
				{
					// Check the specified command is valid
					errorMessage = commandParameters[1] + " is an invalid command. Please see 'help' for a list of commands.";
					return false;
				}
				else if(!ValidateColour(commandParameters[2], out errorMessage))
				{
					// Validate the third parameter is a valid colour
					return false;
				}
				else if(!Regex.IsMatch(commandParameters[3], "^[+-]$"))
				{
					// The repeat by conditional is not a + or -
					errorMessage = "The third parameter must instruct to repeat by + or - times.";
					return false;
				}
				else if(!ValidateInteger(commandParameters[4], out errorMessage) && !String.Equals(commandParameters[1].ToLower(), "polygon")
					&& !String.Equals(commandParameters[1].ToLower(), "triangle")){
					// Validate the parameter to check for an integer
					return false;
				}
				else
				{
					// If the user has specified a shape that requires more parameters
					if(String.Equals(commandParameters[1].ToLower(), "rectangle"))
					{
						if(commandParameters.Length != 6)
						{
							errorMessage = "Repeat command must be in the format. 'Repeat <no of repeats>, <shape>, <colour>, <less/greater>, <incrementer_X>, <incrementer_Y>'";
							return false;
						}
						else if(!ValidateInteger(commandParameters[5], out errorMessage))
						{
							return false;
						}
					}
					else if(String.Equals(commandParameters[1].ToLower(), "triangle"))
					{
						if(commandParameters.Length != 7)
						{
							errorMessage = "Repeat command must be in the format. 'Repeat <no of repeats>, <shape>, <colour>, <less/greater>, <point>, <point>, <point>'";
							return false;
						}
						else if(!ValidatePoint(commandParameters[4], out errorMessage) || !ValidatePoint(commandParameters[5], out errorMessage) ||
							!ValidatePoint(commandParameters[6], out errorMessage)){
							return false;
						}
					}
					else if(String.Equals(commandParameters[1].ToLower(), "polygon"))
					{
						if(commandParameters.Length < 7)
						{
							errorMessage = "Repeat command must be in the format. 'Repeat <no of repeats>, <shape>, <colour>, <less/greater>, <point>, <point>, <point>'";
							return false;
						}
						else
						{
							string[] polygonPoints = commandParameters.Skip(4).ToArray();

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
			else if(commandString.ToLower() == "run")
			{
				if(commandParameters.Length != 1)
				{
					errorMessage = "The run command must have a file path specified";
					return false;
				}
				else if (!ValidateFile(commandParameters[0], out errorMessage))
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
				if(commandParameters.Length != 2)
				{
					// Check the correct number of parameters are passed
					errorMessage = "MoveTo expects 2 parameters to be passed.";
					return false;
				}
				else if (!ValidateInteger(commandParameters[0], out errorMessage))
				{
					// Check the second parameter passed is a number
					errorMessage = "The first parameter " + commandParameters[0] + " must be an integer.";
					return false;
				}
				else if (!ValidateInteger(commandParameters[1], out errorMessage))
				{
					// Check the second parameter passed is a number
					errorMessage = "The second parameter " + commandParameters[1] + " must be an integer.";
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
				if(commandParameters.Length != 3)
				{
					errorMessage = "When the pen is down, MoveTo expects 3 parameters to be passed.";
					return false;
				}
				else if(!ValidateColour(commandParameters[0], out errorMessage))
				{
					return false;
				}
				else if(!ValidateInteger(commandParameters[1], out errorMessage))
				{
					return false;
				}
				else if(!ValidateInteger(commandParameters[2], out errorMessage))
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
				if (commandParameters.Length != 2)
				{
					// Check the correct number of parameters are passed
					errorMessage = "Circle expects 2 parameters to be passed.";
					return false;
				}
				else if (!ValidateColour(commandParameters[0], out errorMessage))
				{
					// Check that the first parameter passed is a valid colour.
					return false;
				}
				else if (!ValidateInteger(commandParameters[1], out errorMessage))
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
				if(commandParameters.Length != 3)
				{
					// Check the correct number of parameters are passed
					errorMessage = "Rectangle expects 3 parameters to be passed.";
					return false;
				}
				else if(!ValidateColour(commandParameters[0], out errorMessage))
				{
					// Check that the first parameter passed is a valid colour.
					return false;
				}
				else if(!ValidateInteger(commandParameters[1], out errorMessage))
				{
					// Check the second parameter passed is a number
					return false;
				}
				else if(!ValidateInteger(commandParameters[2], out errorMessage))
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
				if (commandParameters.Length != 2)
				{
					// Check the correct number of parameters are passed
					errorMessage = "Square expects 2 parameters to be passed.";
					return false;
				}
				else if (!ValidateColour(commandParameters[0], out errorMessage))
				{
					// Check that the first parameter passed is a valid colour.
					return false;
				}
				else if (!ValidateInteger(commandParameters[1], out errorMessage))
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
				if (commandParameters.Length != 4)
				{
					// Check the correct number of parameters are passed
					errorMessage = "Triangle expects 4 parameters to be passed.";
					return false;
				}
				else if (!ValidateColour(commandParameters[0], out errorMessage))
				{
					// Check that the first parameter passed is a valid colour.
					return false;
				}
				else
				{
					for (int i = 1; i < 4; i++)
					{
						if(!ValidatePoint(commandParameters[i], out errorMessage))
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
				if(commandParameters.Length < 4)
				{
					errorMessage = "The minimum number of points in a polygon is 3.";
					return false;
				}
				else if (!ValidateColour(commandParameters[0], out errorMessage))
				{
					return false;
				}
				else
				{
					for(int i = 1; i < (commandParameters.Length - 1); i++)
					{
						if (!ValidatePoint(commandParameters[i], out errorMessage))
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

		public bool ValidateInteger(string numberParamter, out string errorMessage)
		{
			if(Regex.IsMatch(numberParamter, "^[0-9]+$"))
			{
				errorMessage = "'";
				return true;
			}
			else
			{
				if (variables.ContainsKey(numberParamter))
				{
					ValidateInteger(variables[numberParamter], out errorMessage);

					errorMessage = "";
					return true;
				}
				else
				{
				errorMessage = "'" + numberParamter + "' must be an integer.";
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
			if (variables.ContainsKey(pointParameter))
			{
				ValidateInteger(variables[pointParameter], out errorMessage);

				errorMessage = "";
				return true;
			}

			string[] points = pointParameter.Split(' ');

			if (points.Length != 2)
			{
				errorMessage = "Points " + pointParameter + " must have two points separated by a space.";
				return false;
			}
			else if (!Regex.IsMatch(points[0].Trim(), "^[0-9]+$"))
			{
				errorMessage = "Points '" + points[0] + "' must be an integer at '" + pointParameter + "'.";
				return false;
			}
			else if (!Regex.IsMatch(points[1].Trim(), "^[0-9]+$"))
			{
				errorMessage = "Points '" + points[1] + "' must be an integer at '" + pointParameter + "'.";
				return false;
			}
			else
			{
					errorMessage = "";
					return true;
			}
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

		public bool ExecuteCommand(ArrayList shapeCommands, string commandString, string[] commandParameters)
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
				string shapeCommandString = commandParameters[1];
				string commandOperator = commandParameters[3];
				for(int i = 0; i < Int32.Parse(commandParameters[0]); i++){
					if (String.Equals(shapeCommandString.ToUpper(), "SQUARE") || String.Equals(shapeCommandString.ToUpper(), "CIRCLE"))
					{
						int parameterValue;
						if (String.Equals(commandOperator, "+"))
						{
							parameterValue = Int32.Parse(commandParameters[4]) * (i + 1);
						}
						else
						{
							parameterValue = Int32.Parse(commandParameters[4]) / (i + 1);
						}

						List<string> shapeCommandParameters = new List<String>() { commandParameters[2], parameterValue.ToString() };

						ExecuteCommand(shapeCommands, shapeCommandString, shapeCommandParameters.ToArray());
					}
					else if(String.Equals(shapeCommandString.ToUpper(), "RECTANGLE"))
					{
						int parameterValueX, parameterValueY;

						if(String.Equals(commandOperator, "+"))
						{
							parameterValueX = Int32.Parse(commandParameters[4]) * (i + 1);
							parameterValueY = Int32.Parse(commandParameters[5]) * (i + 1);
						}
						else
						{
							parameterValueX = Int32.Parse(commandParameters[4]) / (i + 1);
							parameterValueY = Int32.Parse(commandParameters[5]) / (i + 1);
						}

						List<string> shapeCommandParameters = new List<String>() { commandParameters[2], parameterValueX.ToString(), parameterValueY.ToString() };

						ExecuteCommand(shapeCommands, shapeCommandString, shapeCommandParameters.ToArray());
					}
					else if(String.Equals(shapeCommandString.ToUpper(), "TRIANGLE"))
					{
						int parameterPoint1X, parameterPoint1Y, parameterPoint2X, parameterPoint2Y, parameterPoint3X, parameterPoint3Y;

						string[] parameterPoints1 = commandParameters[4].Split(' ').ToArray();
						string[] parameterPoints2 = commandParameters[5].Split(' ').ToArray();
						string[] parameterPoints3 = commandParameters[6].Split(' ').ToArray();

						if (String.Equals(commandOperator, "+"))
						{
							parameterPoint1X = Int32.Parse(parameterPoints1[0]);
							parameterPoint1Y = Int32.Parse(parameterPoints1[1]);
							parameterPoint2X = Int32.Parse(parameterPoints2[0]) * (i + 1);
							parameterPoint2Y = Int32.Parse(parameterPoints2[1]) * (i + 1);
							parameterPoint3X = Int32.Parse(parameterPoints3[0]) * (i + 1);
							parameterPoint3Y = Int32.Parse(parameterPoints3[1]) * (i + 1);
						}
						else
						{
							parameterPoint1X = Int32.Parse(parameterPoints1[0]);
							parameterPoint1Y = Int32.Parse(parameterPoints1[1]);
							parameterPoint2X = Int32.Parse(parameterPoints2[0]) / (i + 1);
							parameterPoint2Y = Int32.Parse(parameterPoints2[1]) / (i + 1);
							parameterPoint3X = Int32.Parse(parameterPoints3[0]) / (i + 1);
							parameterPoint3Y = Int32.Parse(parameterPoints3[1]) / (i + 1);
						}

						List<string> shapeCommandParameters = new List<String>() { commandParameters[2], (parameterPoint1X + " " + parameterPoint1Y),
						(parameterPoint2X + " " + parameterPoint2Y), (parameterPoint3X + " " + parameterPoint3Y)};

						ExecuteCommand(shapeCommands, shapeCommandString, shapeCommandParameters.ToArray());
					}
					else if(String.Equals(shapeCommandString.ToUpper(), "POLYGON"))
					{
						string[] parameterPoints = commandParameters.Skip(4).ToArray();
						List<string> shapeCommandParameters = new List<String>() { commandParameters[2] };

						for(int d = 0; d < parameterPoints.Length; d++)
						{
							string[] splitPoints = parameterPoints[d].Split(' ').ToArray();
							int splitPointX, splitPointY;

							if(String.Equals(commandParameters[3], "+"))
							{
								splitPointX = Int32.Parse(splitPoints[0]) * (i + 1);
								splitPointY = Int32.Parse(splitPoints[1]) * (i + 1);
							}
							else
							{
								splitPointX = Int32.Parse(splitPoints[0]) / (i + 1);
								splitPointY = Int32.Parse(splitPoints[1]) / (i + 1);
							}

							shapeCommandParameters.Add(splitPointX.ToString() + " " + splitPointY.ToString());
						}

						ExecuteCommand(shapeCommands, shapeCommandString, shapeCommandParameters.ToArray());
					}
				}

				return true;
			}
			else if(commandString == "RECTANGLE")
			{
				int pointX = 0;
				int pointY = 0;

				if (Regex.IsMatch(commandParameters[1], "^[0-9]+$") && Regex.IsMatch(commandParameters[2], "^[0-9]+$"))
				{
					pointX = Int32.Parse(commandParameters[1]);
					pointY = Int32.Parse(commandParameters[2]);
				}
				else if (variables.ContainsKey(commandParameters[1]) && variables.ContainsKey(commandParameters[2]))
				{
					if (Regex.IsMatch(variables[commandParameters[1]], "^[0-9]+$") && 
						Regex.IsMatch(variables[commandParameters[2]], "^[0-9]+$"))
					{
						pointX = Int32.Parse(variables[commandParameters[1]]);
						pointY = Int32.Parse(variables[commandParameters[2]]);
					}
					else
					{
						return false;
					}
				}

				// Get the shape and set the values
				Shape shape = shapeFactory.GetShape(commandString);
				shape.Set(Color.FromName(commandParameters[0]), currentX, currentY, pointX, pointY);

				// Add the shape to the ArrayList
				shapeCommands.Add(shape);

				return true;
			}
			else if(commandString == "SQUARE")
			{
				int point = 0;

				if(Regex.IsMatch(commandParameters[1], "^[0-9]+$"))
				{
					point = Int32.Parse(commandParameters[1]);
				}
				else if (variables.ContainsKey(commandParameters[1]))
				{
					if (Regex.IsMatch(variables[commandParameters[1]], "^[0-9]+$"))
					{
						point = Int32.Parse(variables[commandParameters[1]]);
					}
					else
					{
						return false;
					}
				}

				// Get the shape and set the values
				Shape shape = shapeFactory.GetShape(commandString);
				shape.Set(Color.FromName(commandParameters[0]), currentX, currentY, point, point);

				// Add the shape to the ArrayList
				shapeCommands.Add(shape);

				return true;
			}
			else if(commandString == "CIRCLE")
			{
				int point = 0;

				if (Regex.IsMatch(commandParameters[1], "^[0-9]+$"))
				{
					point = Int32.Parse(commandParameters[1]);
				}
				else if (variables.ContainsKey(commandParameters[1]))
				{
					if (Regex.IsMatch(variables[commandParameters[1]], "^[0-9]+$"))
					{
						point = Int32.Parse(variables[commandParameters[1]]);
					}
					else
					{
						return false;
					}
				}

				// Get the shape and set the values
				Shape shape = shapeFactory.GetShape(commandString);
				shape.Set(Color.FromName(commandParameters[0]), currentX, currentY, point);

				// Add the shape to the ArrayList
				shapeCommands.Add(shape);

				return true;
			}
			else if(commandString == "MOVETO" && penUp)
			{
				int pointX = 0;
				int pointY = 0;

				if (Regex.IsMatch(commandParameters[0], "^[0-9]+$") && Regex.IsMatch(commandParameters[1], "^[0-9]+$"))
				{
					pointX = Int32.Parse(commandParameters[0]);
					pointY = Int32.Parse(commandParameters[1]);
				}
				else if (variables.ContainsKey(commandParameters[0]) && variables.ContainsKey(commandParameters[1]))
				{
					if (Regex.IsMatch(variables[commandParameters[0]], "^[0-9]+$") &&
						Regex.IsMatch(variables[commandParameters[1]], "^[0-9]+$"))
					{
						pointX = Int32.Parse(variables[commandParameters[0]]);
						pointY = Int32.Parse(variables[commandParameters[1]]);
					}
					else
					{
						return false;
					}
				}

				currentX = pointX;
				currentY = pointY;

				return true;
			}
			else if(commandString == "MOVETO" && !penUp)
			{
				int pointX = 0;
				int pointY = 0;

				if (Regex.IsMatch(commandParameters[1], "^[0-9]+$") && Regex.IsMatch(commandParameters[2], "^[0-9]+$"))
				{
					pointX = Int32.Parse(commandParameters[1]);
					pointY = Int32.Parse(commandParameters[2]);
				}
				else if (variables.ContainsKey(commandParameters[1]) && variables.ContainsKey(commandParameters[2]))
				{
					if (Regex.IsMatch(variables[commandParameters[1]], "^[0-9]+$") &&
						Regex.IsMatch(variables[commandParameters[2]], "^[0-9]+$"))
					{
						pointX = Int32.Parse(variables[commandParameters[1]]);
						pointY = Int32.Parse(variables[commandParameters[2]]);
					}
					else
					{
						return false;
					}
				}

				// Get the shape and set the values
				Shape shape = shapeFactory.GetShape(commandString);
				shape.Set(Color.FromName(commandParameters[0]), currentX, currentY, pointX, pointY);

				shapeCommands.Add(shape);

				currentX = pointX;
				currentY = pointY;

				return true;
			}
			else if(commandString == "TRIANGLE")
			{
				int point1X, point1Y, point2X, point2Y, point3X, point3Y = 0;

				if (variables.ContainsKey(commandParameters[1]))
				{
					string[] points1 = variables[commandParameters[1]].Split(' ');

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
					string[] point1 = commandParameters[1].Split(' ');
					point1X = Int32.Parse(point1[0]);
					point1Y = Int32.Parse(point1[1]);
				}


				if (variables.ContainsKey(commandParameters[2]))
				{
					string[] points2 = variables[commandParameters[2]].Split(' ');

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
					string[] points2 = commandParameters[2].Split(' ');

					point2X = Int32.Parse(points2[0]);
					point2Y = Int32.Parse(points2[1]);
				}

				if (variables.ContainsKey(commandParameters[3]))
				{
					string[] points3 = variables[commandParameters[3]].Split(' ');

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
					string[] points3 = commandParameters[3].Split(' ');

					point3X = Int32.Parse(points3[0]);
					point3Y = Int32.Parse(points3[1]);
				}

				// Get the shape and set the values
				Shape shape = shapeFactory.GetShape(commandString);
				shape.Set(Color.FromName(commandParameters[0]), currentX, currentY, point1X, point1Y, point2X, point2Y, point3X, point3Y);

				// Add the shape to the ArrayList
				shapeCommands.Add(shape);

				return true;
			}
			else if(commandString == "POLYGON")
			{
				List<int> integerPoints = new List<int>();
				integerPoints.Add(currentX);
				integerPoints.Add(currentY);

				// Create a new array without the colour
				string[] pointsList = commandParameters.Skip(1).ToArray();
				// Cycle through the new array setting each colour to a new nteger array
				for(int i = 0; i < pointsList.Length; i++)
				{
					int pointX = 0;
					int pointY = 0;

					if (variables.ContainsKey(pointsList[i]))
					{
						string[] pointsSplit = variables[pointsList[i]].Split(' ').ToArray();

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
				shape.Set(Color.FromName(commandParameters[0]), integerPoints.ToArray());

				shapeCommands.Add(shape);

				return true;
			}
			return false;
		}

	}
}
