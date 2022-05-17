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

		public T Parse(string[] args)
		{
			// this should probably show the help when an exception occurs
			try
			{
				return new Parser<T>().Parse(args);
			}
			catch (NoArgumentException e)
			{
				throw e;
			}
			catch (FormatException e)
			{
				throw new ParsingException(e.Message, e);
			}
			catch(OverflowException e)
			{
				throw new ParsingException(e.Message, e);
			}
			catch(ArgumentException e)
			{
				throw e;
			}
		}

	}
}