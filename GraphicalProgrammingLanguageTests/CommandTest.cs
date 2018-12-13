using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphicalProgrammingLanguage;

namespace GraphicalProgrammingLanguageTests
{
	[TestClass]
	public class CommandTest
	{
		[TestMethod]
		public void Pass_Invalid_Command_Call()
		{
			Command command = new Command();
			string errorMessage;
			bool commandType = command.ValidateCommand(1, "IncorrectCommand 1", out errorMessage);

			Assert.AreEqual("IncorrectCommand is an invalid command. Please see 'help' for a list of commands.", errorMessage, "The wrong error message was returned.");
			Assert.AreEqual(false, commandType, "Incorrect command wasn't spotted.");
		}

		[TestMethod]
		public void Pass_Invalid_Number_Of_Parameters()
		{
			Command command = new Command();
			string errorMessage;
			bool commandType = command.ValidateCommand(1, "drawto 0 0 0", out errorMessage);

			Assert.AreEqual("Wrong number of parameters passed.", errorMessage, "The wrong error message was returned.");
			Assert.AreEqual(false, commandType, "Incorrect number of parameters passed.");
		}

		[TestMethod]
		public void Pass_Invalid_Parameter_Type()
		{
			Command command = new Command();
			string errorMessage;
			bool commandType = command.ValidateCommand(1, "drawto 0 string", out errorMessage);

			Assert.AreEqual("You've entered the wrong parameter type.", errorMessage, "The wrong error message was returned.");
			Assert.AreEqual(false, commandType, "Incorrect parameter type not spotted.");
		}
	}
}
