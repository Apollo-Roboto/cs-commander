using System;
using Xunit;
using ARCommander;

namespace ARCommander.Tests
{
	public class UnitTest1
	{
		[Fact]
		public void Test1()
		{
			string[] args = "--name Alex".Split(" ");
			
			Commander cmd = new Commander();
			cmd.AddArgument(new OptionalArgument{
				Name = "name",
				Type = typeof(string),
			});
			cmd.SetAction(pargs => {
				Assert.Equal("Alex", pargs.Get("name"));
			});

			cmd.Execute(args);
		}

		[Fact]
		public void ActionGetsCalledOnExecute()
		{
			bool wasCalled = false;

			Commander cmd = new Commander();
			cmd.SetAction(pargs => {
				wasCalled = true;
			});

			cmd.Execute(new string[]{});

			Assert.True(wasCalled);
		}
	}
}
