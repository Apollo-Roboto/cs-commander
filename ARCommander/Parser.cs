using System;
using System.Collections.Generic;
using System.Reflection;

namespace ARCommander
{
	public class Parser<T> where T : new()
	{
		private int NumberOfRequiredPositional()
		{
			int required = 0;

			FieldInfo[] fields = typeof(T).GetFields();
			foreach(FieldInfo field in fields)
			{
				PositionalAttribute attr = field.GetCustomAttribute<PositionalAttribute>();
				if(attr == null) continue;

				if(attr.Required == true)
					required++;
			}
			return required;
		}


		public T Parse(string[] args)
		{
			List<string> tmpargs = new List<string>(args);
			T options = new T();

			int position = 0;
			while(tmpargs.Count > 0)
			{
				string arg = tmpargs[0];

				if(arg.StartsWith("--"))
				{
					string name = arg.Substring(2);
					// if match regex --.*=.* use what is after the first equal as value

					if(tmpargs.Count <= 1)
						throw new ParsingException($"No value for '--{name}'.");

					string value = tmpargs[1];

					AssignValue(options, name, value);
					tmpargs.RemoveAt(0);
					tmpargs.RemoveAt(0);
				}
				else if(arg.StartsWith("-"))
				{
					char shortName = arg.ToCharArray()[1];

					if(tmpargs.Count <= 1)
						throw new ParsingException($"No value for '-{shortName}'.");

					string value = tmpargs[1];

					AssignValue(options, shortName, value);
					tmpargs.RemoveAt(0);
					tmpargs.RemoveAt(0);
				}
				else
				{
					AssignValue(options, position, arg);
					position++;
					tmpargs.RemoveAt(0);
				}
			}

			if(position < NumberOfRequiredPositional())
				throw new MissingArgumentException("Missing Required Positional");

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
						// throw InvalidEnumException with suggestion if not valid
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