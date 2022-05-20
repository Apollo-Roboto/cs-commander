using System;

namespace ARCommander
{
	public class InvalidEnumException : ParsingException
	{
		public string Suggestion;
		public InvalidEnumException() : base() { }
		public InvalidEnumException(string message) : base(message) { }
		public InvalidEnumException(string message, string suggestion) : base(message)
		{
			Suggestion = suggestion;
		}
		public InvalidEnumException(string message, Exception inner) : base(message, inner) { }
		public InvalidEnumException(string message, Exception inner, string suggestion) : base(message, inner)
		{
			Suggestion = suggestion;
		}
		public InvalidEnumException(System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
