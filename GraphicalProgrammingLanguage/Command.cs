using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
	public class Command
	{
		public Command() { }

		private IDictionary<string, string[]> acceptedCommands = new Dictionary<string, string[]>()
		{
			{"drawto", new string[]{"int", "int"} },
			{"moveto", new string[]{"int", "int"} },
			{"circle", new string[]{"int"} },
			{"rectangle", new string[]{"int", "int"} },
			{"triangle", new string[]{"int", "int", "int"} },
			{"polygon", new string[]{"int"} }
		};

		public bool ValidateCommand(int lineNumber, string command, out string errorMessage)
		{
			// Split the command based on a space to get the command and the parameters
			string[] splitCommand = command.Trim().Split(' ');
			string commandString = splitCommand[0];

			// Remove the command from the array
			string[] parameters = splitCommand.Skip(1).ToArray();
			// Put the parameters array back to a string based on spaces
			string parametersString = string.Join(" ", parameters).Trim();
			// Split the parameters based on a comma
			string[] parameterSplit = parameters.Length > 0 ? parametersString.Split(',') : new string[0];

			// Check whether the command is valid
			if (!acceptedCommands.ContainsKey(commandString.ToLower()))
			{
				errorMessage = splitCommand[0] + " is an invalid command. Please see 'help' for a list of commands.";
				return false;
			}

			// Get the parameters for the given command
			string[] expectedParameters = acceptedCommands[commandString.ToLower()];

			// Check whether the right number of parameters have been passed
			if(!expectedParameters.Length.Equals(parameterSplit.Length))
			{
				errorMessage = commandString + " expects " + expectedParameters.Length + " parameters to be passed.";
				return false;
			}

			// Loop through the parameters checking that the user has inputted the correct object type
			for(int i = 0; i < parameterSplit.Length; i++)
			{
				var userInputtedParameter = parameterSplit[i].Trim();
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
			}

			errorMessage = "";
			return true;
		}
	}
}
