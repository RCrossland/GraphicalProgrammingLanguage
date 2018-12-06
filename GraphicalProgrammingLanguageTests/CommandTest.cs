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
			bool commandType = command.ValidateCommand(1, "IncorrectCommand");

			Assert.AreEqual(false, commandType, "Incorrect command wasn't spotted.");
		}

		[TestMethod]
		public void Pass_Invalid_Number_Of_Parameters()
		{
			Command command = new Command();
			bool commandType = command.ValidateCommand(1, "drawto 0 0 0");

			Assert.AreEqual(false, commandType, "Incorrect number of parameters passed.");
		}

		[TestMethod]
		public void Pass_Invalid_Parameter_Type()
		{
			Command command = new Command();
			bool commandType = command.ValidateCommand(1, "drawto 0 string");

			Assert.AreEqual(false, commandType, "Incorrect parameter type not spotted");
		}
	}
}
