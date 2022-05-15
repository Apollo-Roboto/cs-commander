using System;
using Xunit;
using ARCommander;

namespace ARCommander.Tests
{
	public class UnitTest1
	{
		enum SupportedOutput
		{
			JSON, CSV, XML, YAML
		}
		class AllTypes
		{
			[Parameter("string")]
			public string String;

			[Parameter("bool", 'b')]
			public bool Bool;

			[Parameter("char", 'c')]
			public char Char;

			[Parameter("decimal")]
			public decimal Decimal;

			[Parameter("double")]
			public double Double;

			[Parameter("single")]
			public Single Single;

			[Parameter("byte")]
			public byte Byte;

			[Parameter("datetime")]
			public DateTime datetime;

			[Parameter("enum", 'e')]
			public SupportedOutput Enum;

			[Parameter("Int16")]
			public Int16 Int16;

			[Parameter("Int32")]
			public Int32 Int32;

			[Parameter("Int64")]
			public Int64 Int64;

			[Parameter("UInt16")]
			public UInt16 UInt16;

			[Parameter("UInt32")]
			public UInt32 UInt32;
			
			[Parameter("UInt64")]
			public UInt64 UInt64;
		}
		class SimplePositional
		{
			[Positional("file", 0, Help= "A file to use.")]
			public string File;
		}
		class TwoPositional
		{
			[Positional("source", 0, Help= "A file source.")]
			public string Source;
			[Positional("target", 1, Help= "A file target.")]
			public string Target;
		}
		class SimpleParameter
		{
			[Parameter("file", 'f', Required=false, Help= "A file to use.")]
			public string File;
		}
		class SimpleBool
		{
			[Positional("x", 0)]
			public bool X;
		}
		class SimpleEnum
		{
			[Positional("x", 0)]
			public SupportedOutput X;
		}
		class SimpleDefault
		{
			[Parameter("x")]
			public string X = "A Default Value";
		}
		class SimpleRequiredParameter
		{
			[Parameter("x", Required=true)]
			public string X;
		}

		[Fact]
		public void TestSimpleParameterName()
		{
			Commander<SimpleParameter> commander = new Commander<SimpleParameter>();
			SimpleParameter options = commander.Parse(new string[]{"--file", "./asd"});
			Assert.Equal("./asd", options.File);
		}

		[Fact]
		public void TestSimpleParameterShortName()
		{
			Commander<SimpleParameter> commander = new Commander<SimpleParameter>();
			SimpleParameter options = commander.Parse(new string[]{"-f", "./asd"});
			Assert.Equal("./asd", options.File);
		}

		[Fact]
		public void TestSimplePositional()
		{
			Commander<SimplePositional> commander = new Commander<SimplePositional>();
			SimplePositional options = commander.Parse(new string[]{"./asd"});
			Assert.Equal("./asd", options.File);
		}

		[Fact]
		public void TestTwoPositional()
		{
			Commander<TwoPositional> commander = new Commander<TwoPositional>();
			TwoPositional options = commander.Parse(new string[]{"./source", "./target"});
			Assert.Equal("./source", options.Source);
			Assert.Equal("./target", options.Target);
		}

		[Fact]
		public void TestBoolCaseIgnored()
		{
			Commander<SimpleBool> commander = new Commander<SimpleBool>();
			SimpleBool options = commander.Parse(new string[]{"tRuE"});
			Assert.Equal(true, options.X);
		}

		[Fact]
		public void TestEnumCaseIgnored()
		{
			Commander<SimpleEnum> commander = new Commander<SimpleEnum>();
			SimpleEnum options = commander.Parse(new string[]{"XmL"});
			Assert.Equal(SupportedOutput.XML, options.X);
		}

		[Fact]
		public void TestBoolTakes1or0()
		{
			Commander<SimpleBool> commander = new Commander<SimpleBool>();
			SimpleBool options0 = commander.Parse(new string[]{"0"});
			Assert.Equal(false, options0.X);

			SimpleBool options1 = commander.Parse(new string[]{"1"});
			Assert.Equal(true, options1.X);
		}

		[Fact]
		public void TestParseExceptionThrown()
		{
			Commander<SimpleBool> commander = new Commander<SimpleBool>();

			Assert.Throws<ParsingException>(() => {
				SimpleBool options0 = commander.Parse(new string[]{"unparsable"});
			});
		}

		[Fact]
		public void TestUsesDefaultValue()
		{
			Commander<SimpleDefault> commander = new Commander<SimpleDefault>();
			SimpleDefault options = commander.Parse(new string[]{});
			Assert.Equal("A Default Value", options.X);
		}

		[Fact(Skip="No exception defined yet")]
		public void TestMissingRequiredParameter()
		{
			Commander<SimpleRequiredParameter> commander = new Commander<SimpleRequiredParameter>();

			Assert.Throws<Exception>(() => {
				SimpleRequiredParameter options = commander.Parse(new string[]{});
			});
		}

		[Fact]
		public void TestInvalidParameter()
		{
			Commander<SimpleRequiredParameter> commander = new Commander<SimpleRequiredParameter>();

			Assert.Throws<ArgumentException>(() => {
				SimpleRequiredParameter options = commander.Parse(new string[]{"--asd", "asd"});
			});
		}

		[Fact]
		public void TestMissingValue()
		{
			Commander<SimpleParameter> commander = new Commander<SimpleParameter>();

			Assert.Throws<ParsingException>(() => {
				SimpleParameter options = commander.Parse(new string[]{"--file"});
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
