using System;

namespace ARCommander
{
	public class OptionalArgument
	{
		public string Name {get;set;}
		public char? Short {get;set;} = null;
		public string Help {get;set;} = "";
		public ParsedArguments[] Examples {get;set;}
		public Type Type {get;set;} = typeof(string);
		public object Default {get;set;} = null;
		public bool Required {get;set;} = false;
		public bool Global {get;set;} = false;
	}
}
