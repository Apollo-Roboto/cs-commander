using System;
using System.Collections.Generic;
using System.Reflection;
using ARCommander;

namespace App
{
	public static class Utils
	{
		public static void Info<T>()
		{

			List<ParameterAttribute> optionalAttributes = new List<ParameterAttribute>();
			List<PositionalAttribute> positionalAttributes = new List<PositionalAttribute>();

			// get the fields
			FieldInfo[] fieldInfos = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public);

			
			
			// for each field, add corresponding option|positional
			foreach(FieldInfo fieldInfo in fieldInfos)
			{
				ParameterAttribute optional = fieldInfo.GetCustomAttribute<ParameterAttribute>();
				if(optional != null)
				{
					optionalAttributes.Add(optional);
					continue;
				}

				PositionalAttribute positional = fieldInfo.GetCustomAttribute<PositionalAttribute>();
				if(positional != null)
				{
					positionalAttributes.Add(positional);
					continue;
				}
			}

			Console.WriteLine($"{fieldInfos.Length} field{(fieldInfos.Length > 1 ? "s" : "")}");

			Console.WriteLine("USAGE: App <command> [subcommand] [argument] [flags]");

			Console.WriteLine("\nPositional:");
			foreach(PositionalAttribute attr in positionalAttributes)
			{
				Console.WriteLine($"  {attr.Position,2} {attr.Name,-15}");
			}

			Console.WriteLine("\nParameter:");
			foreach(ParameterAttribute attr in optionalAttributes)
			{
				Console.WriteLine($"  -{attr.Short} --{attr.Name,-15} {(attr.Optional ? "Optional" : "Required")}       Default: {attr.Default}");
			}

			Console.WriteLine("");
		}
	}
}