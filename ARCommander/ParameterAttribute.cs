using System;

namespace ARCommander
{
	[System.AttributeUsage(System.AttributeTargets.Field)]
	public class ParameterAttribute : System.Attribute
	{
		public string Name;
		public char ShortName;
		public bool Required = false;
		public bool Global;
		public string Help;
		
		public ParameterAttribute(){}

		public ParameterAttribute(string n)
		{
			this.Name = n;
		}

		public ParameterAttribute(string n, char s)
		{
			this.Name = n;
			this.ShortName = s;
		}

	}
}