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
			// Split the command based on a space
			string[] splitCommand = command.Split(' ');
			string commandString = splitCommand[0].ToLower();
			string[] parameters = splitCommand.Skip(1).ToArray();

			// Check whether the command is valid
			if (!acceptedCommands.ContainsKey(commandString))
			{
				errorMessage = splitCommand[0] + " is an invalid command. Please see 'help' for a list of commands.";
				return false;
			}

			// Get the parameters for the given command
			string[] expectedParameters = acceptedCommands[commandString];

			// Check whether the right number of parameters have been passed
			if(!expectedParameters.Length.Equals(parameters.Length))
			{
				errorMessage = "Wrong number of parameters passed.";
				return false;
			}

			// Loop through the parameters checking that the user has inputted the correct object type
			for(int i = 0; i < parameters.Length; i++)
			{
				var userInputtedParameter = parameters[i];
				var expectedParameter = expectedParameters[i];

				if(expectedParameter == "int")
				{
					int throwAwayVariable;
					if (!int.TryParse(userInputtedParameter, out throwAwayVariable))
					{
						errorMessage = "You've entered the wrong parameter type.";
						return false;
					}
				}
			}

			errorMessage = "";
			return true;
		}
	}
}
