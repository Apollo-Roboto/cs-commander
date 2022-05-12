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
		private Type OptionsType;

		public Commander()
		{
			
		}

		public T Parse(string[] args)
		{
			T options = new T();

			// https://stackoverflow.com/questions/31342882/assigning-field-values-to-generic-type


			for(int i = 0; i < args.Length; i++)
			{
				string arg = args[i];

				if(arg.StartsWith("--"))
				{
					string name = arg.Substring(2);
					string value = args[i+1];
					Console.WriteLine($"Name is {name}, Value is {value}");
					i ++;
					continue;

					// find field that has attribute with this name and set the value
				}

				if(arg.StartsWith("-"))
				{
					string name = arg.Substring(1);
					string value = args[i+1];
					Console.WriteLine($"Name is {name}, Value is {value}");

					i ++;
					continue;
					// find field that has attribute with this name and set the value
				}

				Console.WriteLine($"Value is {arg}");

			}

			// FieldInfo field = typeof(T).GetField("");

			
			// return (T)Activator.CreateInstance(typeof(T), new object[]{});
			return (T)Activator.CreateInstance(typeof(T));
		}
	}
}