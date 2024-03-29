using System;
using System.Collections.Generic;
using System.Reflection;

namespace ApolloRoboto.Commander
{
	public class Helper
	{
		private static string Header()
		{
			return "";
		}

		private static string Footer()
		{
			return "";
		}

		public static void PrintHelp<T>()
		{

			List<ParameterAttribute> parameterAttributes = new List<ParameterAttribute>();
			List<PositionalAttribute> positionalAttributes = new List<PositionalAttribute>();

			// get the fields
			FieldInfo[] fieldInfos = typeof(T).GetFields(BindingFlags.Instance | BindingFlags.Public);
			
			// for each field, add corresponding option|positional
			foreach(FieldInfo fieldInfo in fieldInfos)
			{
				ParameterAttribute optional = fieldInfo.GetCustomAttribute<ParameterAttribute>();
				if(optional != null)
				{
					parameterAttributes.Add(optional);
					continue;
				}

				PositionalAttribute positional = fieldInfo.GetCustomAttribute<PositionalAttribute>();
				if(positional != null)
				{
					positionalAttributes.Add(positional);
					continue;
				}
			}

			Console.WriteLine("USAGE: App <command> [subcommand] [argument] [flags]");

			Console.WriteLine("\nPositional:");
			foreach(PositionalAttribute attr in positionalAttributes)
			{
				Console.WriteLine($"  {attr.Position,2} {attr.Name,-15}                                          {attr.Help}");
			}

			Console.WriteLine("\nParameter:");
			foreach(ParameterAttribute attr in parameterAttributes)
			{
				Console.WriteLine($"  -{attr.ShortName} --{attr.Name,-15} {(attr.Required ? "Optional" : "Required")}                               {attr.Help}");
			}

			Console.WriteLine("");
		}
	}
}