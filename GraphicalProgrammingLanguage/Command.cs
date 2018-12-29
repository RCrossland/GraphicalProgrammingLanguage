using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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

		private string[] commands = { "drawto", "moveto", "circle", "rectangle", "square", "triangle", "polygon" };

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
				// The command is not recognised. This could mean the user is trying to perform another programming action and needs to be checked.
				errorMessage = commandString + " is an invalid command. Please see 'help' for a list of commands.";
				return false;
			}

			// If the command is rectangle
			if(commandString.ToLower() == "moveto")
			{
				if(commandParameters.Length != 2)
				{
					// Check the correct number of parameters are passed
					errorMessage = "MoveTo expects 2 parameters to be passed.";
					return false;
				}
				else if (!Regex.IsMatch(commandParameters[0], "^[0-9]+$"))
				{
					// Check the second parameter passed is a number
					errorMessage = "The first parameter " + commandParameters[0] + " must be an integer.";
					return false;
				}
				else if (!Regex.IsMatch(commandParameters[1], "^[0-9]+$"))
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
				errorMessage = "'" + numberParamter + "' must be an integer.";
				return false;
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
			string[] points = pointParameter.Split(' ');

			if (points.Length != 2)
			{
				errorMessage = "Points " + points[1] + " must have two points separated by a space.";
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

		public bool ExecuteCommand(ArrayList shapeCommands, string commandString, string[] commandParameters)
		{
			ShapeFactory shapeFactory = new ShapeFactory();

			commandString = commandString.ToUpper().Trim();
			if(commandString == "RECTANGLE")
			{
				// Get the shape and set the values
				Shape shape = shapeFactory.GetShape(commandString);
				shape.Set(Color.FromName(commandParameters[0]), currentX, currentY, 
					Int32.Parse(commandParameters[1]), Int32.Parse(commandParameters[2]));

				// Add the shape to the ArrayList
				shapeCommands.Add(shape);

				return true;
			}
			else if(commandString == "SQUARE")
			{

				// Get the shape and set the values
				Shape shape = shapeFactory.GetShape(commandString);
				shape.Set(Color.FromName(commandParameters[0]), currentX, currentY,
					Int32.Parse(commandParameters[1]), Int32.Parse(commandParameters[1]));

				// Add the shape to the ArrayList
				shapeCommands.Add(shape);

				return true;
			}
			else if(commandString == "CIRCLE")
			{
				// Get the shape and set the values
				Shape shape = shapeFactory.GetShape(commandString);
				shape.Set(Color.FromName(commandParameters[0]), currentX, currentY,
					Int32.Parse(commandParameters[1]));

				// Add the shape to the ArrayList
				shapeCommands.Add(shape);

				return true;
			}
			else if(commandString == "MOVETO")
			{
				currentX = Int32.Parse(commandParameters[0]);
				currentY = Int32.Parse(commandParameters[1]);

				return true;
			}
			else if(commandString == "TRIANGLE")
			{
				string[] point1 = commandParameters[1].Split(' ');
				string[] point2 = commandParameters[2].Split(' ');
				string[] point3 = commandParameters[3].Split(' ');

				// Get the shape and set the values
				Shape shape = shapeFactory.GetShape(commandString);
				shape.Set(Color.FromName(commandParameters[0]), currentX, currentY,
					Int32.Parse(point1[0]), Int32.Parse(point1[1]), Int32.Parse(point2[0]), Int32.Parse(point2[1]),
					Int32.Parse(point3[0]), Int32.Parse(point3[1]));

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
					// Split the pointsList based on spaces
					string[] pointsSplit = pointsList[i].Split(' ');

					integerPoints.Add(Int32.Parse(pointsSplit[0]));
					integerPoints.Add(Int32.Parse(pointsSplit[1]));
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
