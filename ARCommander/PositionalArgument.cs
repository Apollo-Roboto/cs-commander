using System;

namespace ARCommander
{
	public class PositionalArgument
	{
		public string Name {get;set;}
		public string Help {get;set;}
		public string[] Examples {get;set;}
		public Type Type {get;set;}
	}
}