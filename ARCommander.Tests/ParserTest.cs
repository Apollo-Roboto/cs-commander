using System;
using Xunit;
using ARCommander;

namespace ARCommander.Tests
{
	public class ParserTest
	{
		[Fact]
		public void TestSimpleParameterName()
		{
			Parser<TestClasses.SimpleParameter> parser = new Parser<TestClasses.SimpleParameter>();
			TestClasses.SimpleParameter options = parser.Parse(new string[]{"--file", "./asd"});
			Assert.Equal("./asd", options.File);
		}

		[Fact]
		public void TestSimpleParameterShortName()
		{
			Parser<TestClasses.SimpleParameter> parser = new Parser<TestClasses.SimpleParameter>();
			TestClasses.SimpleParameter options = parser.Parse(new string[]{"-f", "./asd"});
			Assert.Equal("./asd", options.File);
		}

		[Fact]
		public void TestSimplePositional()
		{
			Parser<TestClasses.SimplePositional> parser = new Parser<TestClasses.SimplePositional>();
			TestClasses.SimplePositional options = parser.Parse(new string[]{"./asd"});
			Assert.Equal("./asd", options.File);
		}

		[Fact]
		public void TestTwoPositional()
		{
			Parser<TestClasses.TwoPositional> parser = new Parser<TestClasses.TwoPositional>();
			TestClasses.TwoPositional options = parser.Parse(new string[]{"./source", "./target"});
			Assert.Equal("./source", options.Source);
			Assert.Equal("./target", options.Target);
		}

		[Fact]
		public void TestBoolCaseIgnored()
		{
			Parser<TestClasses.SimpleBool> parser = new Parser<TestClasses.SimpleBool>();
			TestClasses.SimpleBool options = parser.Parse(new string[]{"tRuE"});
			Assert.Equal(true, options.X);
		}

		[Fact]
		public void TestEnumCaseIgnored()
		{
			Parser<TestClasses.SimpleEnum> parser = new Parser<TestClasses.SimpleEnum>();
			TestClasses.SimpleEnum options = parser.Parse(new string[]{"XmL"});
			Assert.Equal(TestClasses.SupportedOutput.XML, options.X);
		}

		[Fact]
		public void TestBoolTakes1or0()
		{
			Parser<TestClasses.SimpleBool> parser = new Parser<TestClasses.SimpleBool>();
			TestClasses.SimpleBool options0 = parser.Parse(new string[]{"0"});
			Assert.Equal(false, options0.X);

			TestClasses.SimpleBool options1 = parser.Parse(new string[]{"1"});
			Assert.Equal(true, options1.X);
		}

		[Fact]
		public void TestUsesDefaultValue()
		{
			Parser<TestClasses.SimpleDefault> parser = new Parser<TestClasses.SimpleDefault>();
			TestClasses.SimpleDefault options = parser.Parse(new string[]{});
			Assert.Equal("A Default Value", options.X);
		}

		[Fact]
		public void TestInvalidParameter()
		{
			Parser<TestClasses.SimpleRequiredParameter> parser = new Parser<TestClasses.SimpleRequiredParameter>();

			Assert.Throws<ParsingException>(() => {
				TestClasses.SimpleRequiredParameter options = parser.Parse(new string[]{"--asd", "asd"});
			});
		}

		[Fact]
		public void TestMissingValue()
		{
			Parser<TestClasses.SimpleParameter> parser = new Parser<TestClasses.SimpleParameter>();

			Assert.Throws<ParsingException>(() => {
				TestClasses.SimpleParameter options = parser.Parse(new string[]{"--file"});
			});
		}

		// [Fact]
		// public void TestSubCommand()
		// {
		// 	Commander<object> commander = new Commander<object>();

		// 	object options = commander.Parse(new string[]{"create", "note", "-n", "today"});

		// }
	}
}
