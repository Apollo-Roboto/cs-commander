using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace ARCommander
{
	public class Commander<T> where T : new()
	{
		// private List<OptionalAttribute> OptionalAttributes;
		// private List<PositionalAttribute> PositionalAttributes;

		public Commander(){}

		public void ShowHelp()
		{
			Helper.PrintHelp<T>();
			Environment.Exit(0);
		}

		public void ShowHelp(Exception e)
		{
			Console.WriteLine($"\n  ==> {e.Message} <==  \n");
			Helper.PrintHelp<T>();
			Environment.Exit(1);
		}

		public T Parse(string[] args)
		{
			// this should probably show the help when an exception occurs

			if(args.Length > 0 && (args[0] == "--help" || args[0] == "-h"))
			{
				ShowHelp();
			}

			try
			{
				return new Parser<T>().Parse(args);
			}
			catch(ParsingException e)
			{
				ShowHelp(e);
			}
			catch(FormatException e)
			{
				ShowHelp(e);
			}
			catch(OverflowException e)
			{
				ShowHelp(e);
			}
			catch(ArgumentException e)
			{
				ShowHelp(e);
			}

			return new T();
		}
	}
}