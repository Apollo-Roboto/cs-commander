using System;

namespace ARCommander
{
	public class MissingArgumentException : ParsingException
	{
		public MissingArgumentException() : base() { }
		public MissingArgumentException(string message) : base(message) { }
		public MissingArgumentException(string message, Exception inner) : base(message, inner) { }
		public MissingArgumentException(System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
