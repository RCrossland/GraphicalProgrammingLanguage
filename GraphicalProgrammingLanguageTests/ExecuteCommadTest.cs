using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphicalProgrammingLanguage;
using System.Collections;
using System.Drawing;

namespace GraphicalProgrammingLanguageTests
{
	/// <summary>
	/// Summary description for ExecuteCommadTest
	/// </summary>
	[TestClass]
	public class ExecuteCommadTest
	{
		/// <summary>
		/// Test whether the correct exception is thrown with an invalid variable
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(FormatException), "The expected exception was not thrown.")]
		public void Test_Execute_Loop_Format_Exception()
		{
			Command command = new Command();

			ArrayList shapes = new ArrayList();

			command.ExecuteCommand(null, "variable1=five", "");

			command.ExecuteCommand(shapes, "LOOP", "5; rectangle red, variable1, 20");
		}

		/// <summary>
		/// Test whether the correct exception is thrown with an invalid number of parameters
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException), "The expected exception was not thrown.")]
		public void Test_Execute_Loop_Index_Exception()
		{
			Command command = new Command();

			ArrayList shapes = new ArrayList();

			command.ExecuteCommand(null, "variable1=five", "");

			command.ExecuteCommand(shapes, "LOOP", "5");
		}

		/// <summary>
		/// Test whether the correct exception is thrown with an invalid number of parameters.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(IndexOutOfRangeException), "The expected exception was not thrown.")]
		public void Test_Execute_Rectangle_Index_Exception()
		{
			Command command = new Command();

			ArrayList shapes = new ArrayList();

			command.ExecuteCommand(shapes, "RECTANGLE", "red, 20");
		}

		/// <summary>
		/// Test whether the correct exception is thrown when a variable is not an integer.
		/// </summary>
		[TestMethod]
		[ExpectedException(typeof(FormatException), "The expected exception was not thrown.")]
		public void Test_Execute_Rectangle_Format_Exception()
		{
			Command command = new Command();

			ArrayList shapes = new ArrayList();

			command.ExecuteCommand(null, "variable1=five", "");

			command.ExecuteCommand(shapes, "RECTANGLE", "red, variable1, 20");
		}

		/// <summary>
		/// Test execute rectangle with valid parameters
		/// </summary>
		[TestMethod]
		public void Test_Execute_Rectangle_Valid()
		{
			Command command = new Command();
			bool actual = command.ExecuteCommand(null, "RECTANGLE", "red, 10, 20,");
			bool expected = true;

			Assert.AreEqual(expected, actual, "The wrong boolean value was returned");
		}
	}
}
