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
			T options = new T();

			// https://stackoverflow.com/questions/31342882/assigning-field-values-to-generic-type

			for(int i = 0; i < args.Length; i++)
			{
				string arg = args[i];

				// find parameter by Name
				if(arg.StartsWith("--"))
				{
					if(i+1 >= args.Length) throw new ParsingException("Inconsistant use of parameters and values.");

					string name = arg.Substring(2);
					string value = args[i+1];

					AssignValue(options, name, value);

					i++;
					continue;
				}

				// find parameter by ShortName
				if(arg.StartsWith("-"))
				{
					char shortName = arg.ToCharArray()[1];
					string value = args[i+1];
					if(i+1 >= args.Length) throw new ParsingException("Inconsistant use of parameters and values.");

					AssignValue(options, shortName, value);

					i++;
					continue;
				}

				// no parameter means positional
				AssignValue(options, i, arg);
			}

			return options;
		}

		private void AssignValue(T options, string name, string value)
		{
			FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);
			foreach(FieldInfo field in fields)
			{
				ParameterAttribute attr = field.GetCustomAttribute<ParameterAttribute>();
				if(attr == null) continue;

				// if attribute name does not match the input, skip
				if(attr.Name.ToLower() != name.ToLower()) continue;

				object convertedValue = StringToTypedValue(value, field.FieldType);

				field.SetValue(options, convertedValue);
				return;
			}
			throw new ArgumentException($"Unknown argument --{name}");
		}
		
		private void AssignValue(T options, char shortName, string value)
		{
			FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);
			foreach(FieldInfo field in fields)
			{
				ParameterAttribute attr = field.GetCustomAttribute<ParameterAttribute>();
				if(attr == null) continue;

				// if attribute name does not match the input, skip
				if(attr.ShortName != shortName) continue;

				object convertedValue = StringToTypedValue(value, field.FieldType);

				field.SetValue(options, convertedValue);
				return;
			}
			throw new ArgumentException($"Unknown argument -{shortName}");
		}

		private void AssignValue(T options, int position, string value)
		{
			FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance);
			foreach(FieldInfo field in fields)
			{
				PositionalAttribute attr = field.GetCustomAttribute<PositionalAttribute>();
				if(attr == null) continue;

				// if attribute name does not match the input, skip
				if(attr.Position != position) continue;

				object convertedValue = StringToTypedValue(value, field.FieldType);

				field.SetValue(options, convertedValue);
				return;
			}

			throw new ArgumentException($"No argument at position {position}");
		}

		private object StringToTypedValue(string value, Type type)
		{
			// System.FormatException will be thrown on inparsable string
			// System.OverflowException will be thrown on large values

			switch(Type.GetTypeCode(type))
			{
				case TypeCode.String:
					return value;

				case TypeCode.Boolean:
					if(value.Equals("1"))
						return true;

					if(value.Equals("0"))
						return false;
						
					return bool.Parse(value);

				case TypeCode.Char:
					return Char.Parse(value);

				case TypeCode.Decimal:
					return Decimal.Parse(value);

				case TypeCode.Double:
					return double.Parse(value);

				case TypeCode.Single: // includes float
					return Single.Parse(value);

				case TypeCode.Byte:
					return byte.Parse(value);

				case TypeCode.DateTime:
					return DateTime.Parse(value);

				case TypeCode.Int16: // includes short
					return Int16.Parse(value);

				case TypeCode.Int32: // includes int and enum
				
					if(type.IsEnum)
						return Enum.Parse(type, value, true);

					return Int32.Parse(value);

				case TypeCode.Int64: // include long
					return Int64.Parse(value);

				case TypeCode.UInt16: // include ushort
					return UInt16.Parse(value);
					
				case TypeCode.UInt32: // include uint
					return UInt32.Parse(value);

				case TypeCode.UInt64: // include ulong
					return UInt64.Parse(value);

				default:
					// custom type interface here whenever I figure that out
					// IParsable thing = (IParsable)field.FieldType;
					// return (T)Activator.CreateInstance(typeof(T), new object[]{});

					throw new NotSupportedException($"Cannot convert string '{value}' to type '{type.FullName}'.\nImplement interface blah for custom types.");
			}
		}
	}
}