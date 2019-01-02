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
			// Test split user input passing in a command with awkward spaces
			Command command = new Command();

			string userCommand = " command parameters";

			string[] actual = command.SplitUserInput(userCommand);
			string[] expected = { "command", "parameters" };

			CollectionAssert.AreEqual(actual, expected, "Failed to split the command.");
		}
		
		[TestMethod]
		public void Test_SplitParameters()
		{
			// Tets SplitParameters() passing an expected return value from SplitUserInput() and a comma split delimiter
			Command command = new Command();

			string userInput = " parameter1, parameter2 ";
			string splitDelimiter = ",";

			string[] actual = command.SplitParameters(userInput, splitDelimiter);
			string[] expected = { "parameter1", "parameter2" };

			CollectionAssert.AreEqual(actual, expected);
		}
		
		[TestMethod]
		public void Test_Invalid_Command_Call()
		{
			// Check whether an incorrect command error message is returned from ValidateCommand()
			Command command = new Command();

			string commandString = "IncorrectCommand";
			string parameterString = "parameter1, paramteter2";

			string errorMessage;
			bool actual = command.ValidateCommand(1, commandString, parameterString, out errorMessage);
			bool expected = false;

			Assert.AreEqual("IncorrectCommand is an invalid command. Please see 'help' for a list of commands.", errorMessage, "The wrong error message was returned.");
			Assert.AreEqual(expected, actual, "Incorrect command wasn't spotted.");
		}
		
		[TestMethod]
		public void Test_Invalid_Number_Of_Parameters()
		{
			// Pass an invalid number of parameters for the Square method and ensure the correct error message is returned
			Command command = new Command();

			string commandString = "Square";
			string parameterString = "5, 10, 25";

			string errorMessage;
			bool actual = command.ValidateCommand(1, commandString, parameterString, out errorMessage);
			bool expected = false;

			Assert.AreEqual("Square expects 2 parameters to be passed.", errorMessage, "The wrong error message was returned.");
			Assert.AreEqual(expected, actual, "Incorrect number of parameters passed.");
		}
		
		[TestMethod]
		public void Test_Invalid_Parmeter_Validate_Integer()
		{
			// Pass a string to the ValidateInteger method and ensure the correct error message is returned
			Command command = new Command();

			string parameterString = "parameter1";

			string errorMessage;
			bool actual = command.ValidateInteger(parameterString, out errorMessage);
			bool expected = false;

			Assert.AreEqual("'parameter1' must be a positive integer.", errorMessage, "The wrong error message was returned.");
			Assert.AreEqual(expected, actual, "Incorrect parameter type not spotted.");
		}

		[TestMethod]
		public void Test_Valid_Parmeter_Validate_Integer()
		{
			// Pass a valid integer to the ValidateInteger method and ensure the correct error message is returned
			Command command = new Command();

			string parameterString = "1";

			string errorMessage;
			bool actual = command.ValidateInteger(parameterString, out errorMessage);
			bool expected = true;
			
			Assert.AreEqual("", errorMessage, "The wrong error message was returned.");
			Assert.AreEqual(expected, actual, "Incorrect parameter type not spotted.");
		}

		[TestMethod]
		public void Test_Invalid_Variable_Parmeter_Validate_Integer()
		{
			// Pass an invalid variable variable to the ValidateInteger method and ensure the correct error message is returned
			Command command = new Command();

			command.ExecuteCommand(null, "counter = string", "");

			string parameterString = "counter";

			string errorMessage;
			bool actual = command.ValidateInteger(parameterString, out errorMessage);
			bool expected = true;

			Assert.AreEqual("", errorMessage, "The wrong error message was returned.");
			Assert.AreEqual(expected, actual, "Incorrect parameter type not spotted.");
		}

		[TestMethod]
		public void Test_Valid_Variable_Parmeter_Validate_Integer()
		{
			// Pass a valid variable to the ValidateInteger method and ensure the correct error message is returned
			Command command = new Command();

			command.ExecuteCommand(null, "counter = 10", "");

			string parameterString = "counter";

			string errorMessage;
			bool actual = command.ValidateInteger(parameterString, out errorMessage);
			bool expected = true;

			Assert.AreEqual("", errorMessage, "The wrong error message was returned.");
			Assert.AreEqual(expected, actual, "Incorrect parameter type not spotted.");
		}

		[TestMethod]
		public void Test_Invalid_Colour_Validate_Colour()
		{
			// Pass an invalid colour to the ValidateColour method to ensure the correct errorMessage is returned
			Command command = new Command();

			string colourString = "Invalid";

			string errorMessage;
			bool actual = command.ValidateColour(colourString, out errorMessage);
			bool expected = false;

			Assert.AreEqual("'Invalid' is not a known colour.", errorMessage, "The wrong error message was returned.");
			Assert.AreEqual(expected, actual, "Incorrect parameter type not spotted.");
		}

		[TestMethod]
		public void Test_Valid_Colour_Validate_Colour()
		{
			// Pass an valid colour to the ValidateColour method to ensure the correct errorMessage is returned
			Command command = new Command();

			string colourString = "red";


			string errorMessage;
			bool actual = command.ValidateColour(colourString, out errorMessage);
			bool expected = true;

			Assert.AreEqual("", errorMessage, "The wrong error message was returned.");
			Assert.AreEqual(expected, actual, "Incorrect parameter type not spotted.");
		}

		[TestMethod]
		public void Test_Invalid_String_Point_Validate_Point()
		{
			// Pass an invalid point to the ValidatePoint method
			Command command = new Command();

			string pointParameter = "string 100";

			string errorMessage;
			bool actual = command.ValidatePoint(pointParameter, out errorMessage);
			bool expected = false;

			Assert.AreEqual("Points 'string' must be an integer at 'string 100'.", errorMessage, "The wrong error message was returned.");
			Assert.AreEqual(expected, actual, "Incorrect parameter type not spotted.");
		}

		[TestMethod]
		public void Test_Invalid_Variable_Point_Validate_Point()
		{
			// Pass an invalid variable point to the ValidatePoint method
			Command command = new Command();

			command.ExecuteCommand(null, "point1=string", "");

			string pointParameter = "point1 100";

			string errorMessage;
			bool actual = command.ValidatePoint(pointParameter, out errorMessage);
			bool expected = false;

			Assert.AreEqual("Points 'STRING' must be an integer at 'point1 100'.", errorMessage, "The wrong error message was returned.");
			Assert.AreEqual(expected, actual, "Incorrect parameter type not spotted.");
		}

		[TestMethod]
		public void Test_Valid_Variable_Point_Validate_Point()
		{
			// Pass an invalid variable point to the ValidatePoint method
			Command command = new Command();

			command.ExecuteCommand(null, "point1=100", "");

			string pointParameter = "point1 100";

			string errorMessage;
			bool actual = command.ValidatePoint(pointParameter, out errorMessage);
			bool expected = true;

			Assert.AreEqual("", errorMessage, "The wrong error message was returned.");
			Assert.AreEqual(expected, actual, "Incorrect parameter type not spotted.");
		}

		[TestMethod]
		public void Test_Invalid_Structure_Point_Validate_Point()
		{
			// Pass an invalid variable point to the ValidatePoint method
			Command command = new Command();

			string pointParameter = "100,100";

			string errorMessage;
			bool actual = command.ValidatePoint(pointParameter, out errorMessage);
			bool expected = false;

			Assert.AreEqual("Points 100,100 must have two points separated by a space.", errorMessage, "The wrong error message was returned.");
			Assert.AreEqual(expected, actual, "Incorrect parameter type not spotted.");
		}

		[TestMethod]
		public void Test_Valid_Integer_Point_Validate_Point()
		{
			// Pass an invalid variable point to the ValidatePoint method
			Command command = new Command();

			string pointParameter = "100 100";

			string errorMessage;
			bool actual = command.ValidatePoint(pointParameter, out errorMessage);
			bool expected = true;

			Assert.AreEqual("", errorMessage, "The wrong error message was returned.");
			Assert.AreEqual(expected, actual, "Incorrect parameter type not spotted.");
		}
	}
}
