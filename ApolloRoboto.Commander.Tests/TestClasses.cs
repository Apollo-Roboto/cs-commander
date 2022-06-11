using System;

namespace ApolloRoboto.Commander.Tests
{
	static class TestClasses
	{
		public enum SupportedOutput
		{
			JSON, CSV, XML, YAML
		}

		public class AllTypes
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

		public class SimplePositional
		{
			[Positional("file", 0, Help= "A file to use.")]
			public string File;
		}

		public class TwoPositional
		{
			[Positional("source", 0, Help= "A file source.")]
			public string Source;
			[Positional("target", 1, Help= "A file target.")]
			public string Target;
		}

		public class SimpleParameter
		{
			[Parameter("file", 'f', Required=false, Help= "A file to use.")]
			public string File;
		}

		public class TwoParameter
		{
			[Parameter("source", 's', Required=false, Help= "A file source.")]
			public string Source;
			[Parameter("target", 't', Required=false, Help= "A file target.")]
			public string Target;
		}

		public class SimpleBool
		{
			[Positional("x", 0)]
			public bool X;
		}

		public class SimpleEnum
		{
			[Positional("x", 0)]
			public SupportedOutput X;
		}

		public class SimpleDefault
		{
			[Parameter("x")]
			public string X = "A Default Value";
		}

		public class SimpleRequiredParameter
		{
			[Parameter("x", Required=true)]
			public string X;
		}
	}
}