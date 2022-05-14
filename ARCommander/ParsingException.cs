using System;

namespace ARCommander
{
	public class ParsingException : Exception
	{
		public ParsingException() : base() { }
		public ParsingException(string message) : base(message) { }
		public ParsingException(string message, Exception inner) : base(message, inner) { }
		public ParsingException(System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
