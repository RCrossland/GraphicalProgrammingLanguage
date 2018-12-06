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

		public bool ValidateCommand(int lineNumber, string command)
		{
			// Split the command based on a space
			string[] splitCommand = command.ToLower().Split(' ');
			string commandString = splitCommand[0];
			string[] parameters = splitCommand.Skip(1).ToArray();

			// Check whether the command is valid
			if (!acceptedCommands.ContainsKey(commandString))
			{
				return false;
			}

			// Get the parameters for the given command
			string[] expectedParameters = acceptedCommands[commandString];

			// Check whether the right number of parameters have been passed
			if(!expectedParameters.Length.Equals(parameters.Length))
			{
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
						return false;
					}
				}
			}

			return true;
		}
	}
}
