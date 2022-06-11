using System;

namespace ApolloRoboto.Commander
{
	public class NoArgumentException : ParsingException
	{
		public NoArgumentException() : base() { }
		public NoArgumentException(string message) : base(message) { }
		public NoArgumentException(string message, Exception inner) : base(message, inner) { }
		public NoArgumentException(System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
