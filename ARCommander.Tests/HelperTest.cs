using System;
using Xunit;

namespace ARCommander.Tests
{
	public class HelperTest
	{

		[Fact]
		public void TestContainsParameterName()
		{
			ConsoleCapture Capture = new ConsoleCapture();
			Console.SetOut(Capture);
			
			Helper.PrintHelp<TestClasses.TwoParameter>();

			string result = Capture.Output.ToLower();

			Assert.Contains("source", result);
			Assert.Contains("target", result);
		}

		[Fact]
		public void TestContainsParameterHelp()
		{
			ConsoleCapture Capture = new ConsoleCapture();
			Console.SetOut(Capture);
			
			Helper.PrintHelp<TestClasses.TwoParameter>();

			string result = Capture.Output.ToLower();

			Assert.Contains("A file source.".ToLower(), result);
			Assert.Contains("A file target.".ToLower(), result);
		}
	}
}