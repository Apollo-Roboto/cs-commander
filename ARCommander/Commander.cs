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

					AssignValue(options, name, value);

					i++;
					continue;

					// find field that has attribute with this name and set the value
				}

				if(arg.StartsWith("-"))
				{
					string name = arg.Substring(1);
					string value = args[i+1];
					Console.WriteLine($"Name is {name}, Value is {value}");

					AssignValue(options, name, value);

					i++;
					continue;
					// find field that has attribute with this name and set the value
				}

				Console.WriteLine($"Value is {arg}");
				AssignValue(options, i, arg);

			}

			// FieldInfo field = typeof(T).GetField("");

			
			// return (T)Activator.CreateInstance(typeof(T), new object[]{});
			return options;
		}

		private void AssignValue(T options, string name, string value)
		{
			FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);
			foreach(FieldInfo field in fields)
			{
				ParameterAttribute attr = field.GetCustomAttribute<ParameterAttribute>();

				// if attribute name does not match the input, skip
				if(attr.Name.ToLower() != name.ToLower()) continue;

				// would curently only work if field is a string
				field.SetValue(options, value);
			}
		}

		private void AssignValue(T options, int position, string value)
		{

		}

	}
}