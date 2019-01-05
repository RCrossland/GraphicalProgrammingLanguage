using System;
using System.IO;
using System.Collections;
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

		// Validation Functions 
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

		[TestMethod]
		public void Test_Command_Repeat_Valid__Greater_Than_Parameters()
		{
			// Test Repeat with valid parameters
			Command command = new Command();

			string testCommand = "repeat";
			string testParameters = "5, Square, red, +, 10";

			string actualErrorMessage;
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "";
			bool expected = true;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Repeat_Valid_Less_Than_Parameters()
		{
			// Test Repeat with valid parameters
			Command command = new Command();

			string testCommand = "repeat";
			string testParameters = "5, Square, red, -, 20";

			string actualErrorMessage;
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "";
			bool expected = true;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Repeat_Invalid_Length_Parameters()
		{
			// Test Repeat with valid parameters
			Command command = new Command();

			string testCommand = "repeat";
			string testParameters = "5, Square, +";

			string actualErrorMessage;
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "Repeat command must be in the format. 'Repeat <no of repeats>, <shape>, <colour>, <less/greater>, <incrementer>'";
			bool expected = false;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Repeat_Invalid_Command_Parameters()
		{
			// Test Repeat with valid parameters
			Command command = new Command();

			string testCommand = "repeat";
			string testParameters = "5, Invalid, red, +, 10";

			string actualErrorMessage;
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "Invalid is an invalid command. Please see 'help' for a list of commands.";
			bool expected = false;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Repeat_Invalid_Operator_Parameters()
		{
			// Test Repeat with valid parameters
			Command command = new Command();

			string testCommand = "repeat";
			string testParameters = "5, Square, red, /, 10";

			string actualErrorMessage;
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "The third parameter must instruct to repeat by + or - times.";
			bool expected = false;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Repeat_Invalid_Integer_Iterator_Parameters()
		{
			// Test Repeat with valid parameters
			Command command = new Command();

			string testCommand = "repeat";
			string testParameters = "string, Square, red, +, 10";

			string actualErrorMessage;
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "'string' must be a positive integer.";
			bool expected = false;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Repeat_Invalid_Integer_Parameters()
		{
			// Test Repeat with valid parameters
			Command command = new Command();

			string testCommand = "repeat";
			string testParameters = "5, Square, red, +, string";

			string actualErrorMessage;
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "'string' must be a positive integer.";
			bool expected = false;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Single_Loop_Valid_Parameters ()
		{
			// Test Repeat with valid parameters
			Command command = new Command();

			string testCommand = "loop";
			string testParameters = "5; rectangle red, 10, 20";

			string actualErrorMessage;
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "";
			bool expected = true;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Single_Loop_Invalid_Incrementer_Parameters()
		{
			// Test Repeat with valid parameters
			Command command = new Command();

			string testCommand = "loop";
			string testParameters = "string; rectangle red, 10, 20";

			string actualErrorMessage;
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "'string' must be a positive integer.";
			bool expected = false;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Single_Loop_Invalid_Command_Parameter()
		{
			// Test Repeat with valid parameters
			Command command = new Command();

			string testCommand = "loop";
			string testParameters = "5; invalid red, 10, 20";

			string actualErrorMessage;
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "invalid is an invalid command. Please see 'help' for a list of commands.";
			bool expected = false;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Multi_Line_Loop_Valid_Parameter()
		{
			// Test Repeat with valid parameters
			Command command = new Command();

			string testCommand = "loop";
			string testParameters = "5";

			string actualErrorMessage;
			command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			testCommand = "loop";
			testParameters = "5 \n\rrectangle red, 10, 20";

			command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			testCommand = "loop";
			testParameters = "5 \n\rrectangle red, 10, 20\n\rendloop";

			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "";
			bool expected = true;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Multi_Line_Loop_Invalid_Incrementer_Parameter()
		{
			// Test Repeat with valid parameters
			Command command = new Command();

			string testCommand = "loop";
			string testParameters = "string";

			string actualErrorMessage;
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "'string' must be a positive integer.";
			bool expected = false;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Multi_Line_Loop_Invalid_Command_Parameter()
		{
			// Test Repeat with valid parameters
			Command command = new Command();

			string testCommand = "loop";
			string testParameters = "5";

			string actualErrorMessage;
			command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			testCommand = "loop";
			testParameters = "5 \n\rinvali" +
				"d red, 10, 20";

			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "invalid is an invalid command. Please see 'help' for a list of commands.";
			bool expected = false;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Multi_Line_Loop_Empty_Parameter()
		{
			// Test Repeat with valid parameters
			Command command = new Command();

			string testCommand = "loop";
			string testParameters = "5";

			string actualErrorMessage;
			command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			testCommand = "loop";
			testParameters = "5 \n\rrectangle red, 10, 20\n\rendloop";

			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "The loop was empty";
			bool expected = false;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Run_Valid_Parameter()
		{
			Command command = new Command();

			string testFile = System.AppDomain.CurrentDomain.BaseDirectory;

			string testCommand = "run";
			string testParameters = testFile + "\\..\\..\\TextFile.txt";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "";
			bool expected = true;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Run_Invalid_File_Parameter()
		{
			Command command = new Command();

			string testCommand = "run";
			string testParameters = "InvalidFile.txt";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "The file InvalidFile.txt doesn't exist."; ;
			bool expected = false;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Run_Invalid_File_Path_Parameter()
		{
			Command command = new Command();

			string testCommand = "run";
			string testParameters = "InvalidFile.js";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "The file extension must be .txt";
			bool expected = false;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Move_To_Pen_Up_Valid_Parameter()
		{
			Command command = new Command();

			string testCommand = "moveto";
			string testParameters = "100, 100";

			string actualErrorMessage = "";
			command.ExecuteCommand(null, "penup", "");
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "";
			bool expected = true;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Move_To_Pen_Down_Valid_Parameter()
		{
			Command command = new Command();

			string testCommand = "moveto";
			string testParameters = "red, 100, 100";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "";
			bool expected = true;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Move_To_Pen_Up_Invalid_Parameter()
		{
			Command command = new Command();

			string testCommand = "moveto";
			string testParameters = "string, 100";

			string actualErrorMessage = "";
			command.ExecuteCommand(null, "penup", "");
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "The first parameter string must be an integer.";
			bool expected = false;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Move_To_Pen_Down_In_Valid_Parameter()
		{
			Command command = new Command();

			string testCommand = "moveto";
			string testParameters = "red, string, 100";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "'string' must be a positive integer.";
			bool expected = false;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Circle_Valid_Parameters()
		{
			Command command = new Command();

			string testCommand = "circle";
			string testParameters = "red, 100";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "";
			bool expected = true;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Circle_Invalid_Integer_Parameters()
		{
			Command command = new Command();

			string testCommand = "circle";
			string testParameters = "red, string";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "'string' must be a positive integer.";
			bool expected = false;

			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Circle_Invalid_Length_Parameters()
		{
			Command command = new Command();

			string testCommand = "circle";
			string testParameters = "red";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "Circle expects 2 parameters to be passed.";
			bool expected = false;


			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Rectangle_Valid_Parameters()
		{
			Command command = new Command();

			string testCommand = "rectangle";
			string testParameters = "red, 10, 20";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "";
			bool expected = true;


			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Rectangle_Invalid_Length_Parameters()
		{
			Command command = new Command();

			string testCommand = "rectangle";
			string testParameters = "red, 10";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "Rectangle expects 3 parameters to be passed.";
			bool expected = false;


			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Rectangle_Invalid_Integer_Parameters()
		{
			Command command = new Command();

			string testCommand = "rectangle";
			string testParameters = "red, string, 10";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "'string' must be a positive integer.";
			bool expected = false;


			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Rectangle_Invalid_Color_Parameters()
		{
			Command command = new Command();

			string testCommand = "rectangle";
			string testParameters = "invalid, 20, 10";
		 
			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "'invalid' is not a known colour.";
			bool expected = false;


			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Square_Valid_Parameters()
		{
			Command command = new Command();

			string testCommand = "square";
			string testParameters = "red, 10";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "";
			bool expected = true;


			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Square_Invalid_Integer_Parameters()
		{
			Command command = new Command();

			string testCommand = "square";
			string testParameters = "red, string";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "'string' must be a positive integer.";
			bool expected = false;


			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Square_Invalid_Color_Parameters()
		{
			Command command = new Command();

			string testCommand = "square";
			string testParameters = "invalid, 10";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "'invalid' is not a known colour.";
			bool expected = false;


			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Square_Invalid_Length_Parameters()
		{
			Command command = new Command();

			string testCommand = "square";
			string testParameters = "invalid";
		   
			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "Square expects 2 parameters to be passed.";
			bool expected = false;


			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Triangle_Valid_Parameters()
		{
			Command command = new Command();

			string testCommand = "triangle";
			string testParameters = "red, 10 10, 20 20, 30 30";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "";
			bool expected = true;


			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Triangle_Invalid_Length_Parameters()
		{
			Command command = new Command();

			string testCommand = "triangle";
			string testParameters = "red, 10 10, 20 20";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "Triangle expects 4 parameters to be passed.";
			bool expected = false;


			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Triangle_Invalid_Color_Parameters()
		{
			Command command = new Command();

			string testCommand = "triangle";
			string testParameters = "invalid, 10 10, 20 20, 30 30";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "" +
				"'invalid' is not a known colour.";
			bool expected = false;


			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Triangle_Invalid_Point_Parameters()
		{
			Command command = new Command();

			string testCommand = "triangle";
			string testParameters = "red, 10, 20 20, 30 30";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "Points 10 must have two points separated by a space.";
			bool expected = false;


			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Polygon_Valid_Parameters()
		{
			Command command = new Command();

			string testCommand = "polygon";
			string testParameters = "red, 10 10, 20 20, 30 30, 40 40";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "";
			bool expected = true;


			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Polygon_Invalid_Length_Parameters()
		{
			Command command = new Command();

			string testCommand = "polygon";
			string testParameters = "red, 10 10, 20 20";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "The minimum number of points in a polygon is 3.";
			bool expected = false;


			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Polygon_Invalid_Color_Parameters()
		{
			Command command = new Command();

			string testCommand = "polygon";
			string testParameters = "invalid, 10 10, 20 20, 30 30";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "'invalid' is not a known colour.";
			bool expected = false;


			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Polygon_Invalid_Point_Parameters()
		{
			Command command = new Command();

			string testCommand = "polygon";
			string testParameters = "red, 10 10, 20 20, 30";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "Points 30 must have two points separated by a space.";
			bool expected = false;


			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Clear_Valid_Parameters()
		{
			Command command = new Command();

			string testCommand = "clear";
			string testParameters = "";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "";
			bool expected = true;


			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}

		[TestMethod]
		public void Test_Command_Clear_Invalid_Length_Parameters()
		{
			Command command = new Command();

			string testCommand = "clear";
			string testParameters = "10";

			string actualErrorMessage = "";
			bool actual = command.ValidateCommand(1, testCommand, testParameters, out actualErrorMessage);

			string expectedErrorMessage = "Clear doesn't have any extra parameters.";
			bool expected = false;


			Assert.AreEqual(expectedErrorMessage, actualErrorMessage, "The error messages were different.");
			Assert.AreEqual(expected, actual, "The wrong boolean was returned.");
		}
	}
}
