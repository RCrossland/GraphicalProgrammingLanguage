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
		public void Test_SplitUserInput()
		{
			Command command = new Command();

			string userCommand = " command parameters";

			string[] actual = command.SplitUserInput(userCommand);
			string[] expected = { "command", "parameters" };

			CollectionAssert.AreEqual(actual, expected, "Failed to split the command.");
		}

		[TestMethod]
		public void Test_SplitParameters()
		{
			Command command = new Command();

			string[] userInput = { "command", "parameter1,", "parameter2 " };

			string[] actual = command.SplitParameters(userInput);
			string[] expected = { "parameter1", "parameter2" };

			CollectionAssert.AreEqual(actual, expected);
		}
		
		[TestMethod]
		public void Pass_Invalid_Command_Call()
		{
			Command command = new Command();

			string commandString = "IncorrectCommand";
			string[] parameters = { "parameter1", "parameter2" };

			string errorMessage;
			bool actual = command.ValidateCommand(1, commandString, parameters, out errorMessage);
			bool expected = false;

			Assert.AreEqual("IncorrectCommand is an invalid command. Please see 'help' for a list of commands.", errorMessage, "The wrong error message was returned.");
			Assert.AreEqual(expected, actual, "Incorrect command wasn't spotted.");
		}

		[TestMethod]
		public void Pass_Invalid_Number_Of_Parameters()
		{
			Command command = new Command();

			string commandString = "DrawTo";
			string[] parameters = { "5", "10", "15" };

			string errorMessage;
			bool actual = command.ValidateCommand(1, commandString, parameters, out errorMessage);
			bool expected = false;

			Assert.AreEqual("DrawTo expects 2 parameters to be passed.", errorMessage, "The wrong error message was returned.");
			Assert.AreEqual(expected, actual, "Incorrect number of parameters passed.");
		}

		[TestMethod]
		public void Pass_Invalid_Parameter_Type_Integer()
		{
			Command command = new Command();

			string commandString = "Circle";
			string[] parameters = { "parameter1" };

			string errorMessage;
			bool actual = command.ValidateCommand(1, commandString, parameters, out errorMessage);
			bool expected = false;

			Assert.AreEqual("parameter1 must be an integer.", errorMessage, "The wrong error message was returned.");
			Assert.AreEqual(expected, actual, "Incorrect parameter type not spotted.");
		}

		[TestMethod]
		public void Pass_Invalid__Parameter_Type_String()
		{
			Command command = new Command();

			string commandString = "Colour";
			string[] parameters = { "10" };

			string errorMessage;
			bool actual = command.ValidateCommand(1, commandString, parameters, out errorMessage);
			bool expected = false;

			Assert.AreEqual("10 must be a string.", errorMessage, "The wrong error message was returned.");
			Assert.AreEqual(expected, actual, "Incorrect parameter type not spotted.");
		}
	}
}
