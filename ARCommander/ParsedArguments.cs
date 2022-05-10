using System;
using System.Collections.Generic;

namespace ARCommander
{
	public class ParsedArguments
	{
		public Dictionary<string, object> OptionalArguments = new Dictionary<string, object>();
		public List<object> PositionalArguments = new List<object>();

		public object Get(int i)
		{
			return PositionalArguments[i];
		}

		public object Get(string name)
		{
			return OptionalArguments[name];
		}
	}
}