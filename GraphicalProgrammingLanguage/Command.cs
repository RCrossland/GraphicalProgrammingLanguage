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

		private IDictionary<string, string[]> acceptedCommands = new Dictionary<string, string[]>()
		{
			{"drawto", new string[]{"int", "int"} },
			{"moveto", new string[]{"int", "int"} },
			{"circle", new string[]{"colour", "int"} },
			{"rectangle", new string[]{"colour", "int", "int"} },
			{"square", new string[]{"colour", "int"} },
			{"triangle", new string[]{"colour", "point", "point", "point"} },
			{"polygon", new string[]{"colour", "point"} },
			{"colour", new string[]{"string"} }
		};

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
			if (!acceptedCommands.ContainsKey(commandString.ToLower()))
			{
				errorMessage = commandString + " is an invalid command. Please see 'help' for a list of commands.";
				return false;
			}

			// Get the parameters for the given command
			string[] expectedParameters = acceptedCommands[commandString.ToLower()];

			// Check whether the right number of parameters have been passed
			if(!expectedParameters.Length.Equals(commandParameters.Length) && commandString.ToLower() != "polygon")
			{
				errorMessage = commandString + " expects " + expectedParameters.Length + " parameters to be passed.";
				return false;
			}

			// Loop through the parameters checking that the user has inputted the correct object type
			for(int i = 0; i < commandParameters.Length; i++)
			{
				var userInputtedParameter = commandParameters[i].Trim();
				var expectedParameter = expectedParameters[i];

				if(expectedParameter == "int")
				{
					int throwAwayVariable;
					if (!int.TryParse(userInputtedParameter, out throwAwayVariable))
					{
						errorMessage = userInputtedParameter + " must be an integer.";
						return false;
					}
				}
				else if(expectedParameter == "string")
				{
					if(!Regex.IsMatch(userInputtedParameter, "^[a-zA-Z]+$"))
					{
						errorMessage = userInputtedParameter + " must be a string.";
						return false;
					}
				}
				else if(expectedParameter == "colour")
				{
					if (!Color.FromName(userInputtedParameter).IsKnownColor)
					{
						errorMessage = userInputtedParameter + " is not a known colour.";
						return false;
					}
				}
				else if(expectedParameter == "point")
				{
					string[] points = userInputtedParameter.Split(' ');
					int throwAwayVariable;

					if (points.Length != 2)
					{
						errorMessage = "You must enter two points sepearted by a space. Each point combination is separated by a command. " +
							userInputtedParameter + " is invalid.";
						return false;
					}
					else if (!int.TryParse(points[0], out throwAwayVariable)){
						errorMessage = points[0] + " point must be an integer.";
						return false;
					}
					else if (!int.TryParse(points[1], out throwAwayVariable))
					{
						errorMessage = points[1] + " must be an integer in points '" + userInputtedParameter + "'";
						return false;
					}
				}
			}

			errorMessage = "";
			return true;
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
			else if (commandString == "MOVETO")
			{
				currentX = Int32.Parse(commandParameters[0]);
				currentY = Int32.Parse(commandParameters[1]);

				return true;
			}
			else if (commandString == "TRIANGLE")
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

			return false;
		}
	}
}
