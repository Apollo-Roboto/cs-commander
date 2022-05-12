using System;

namespace ARCommander
{
	[System.AttributeUsage(System.AttributeTargets.Field)]
	public class ParameterAttribute : System.Attribute
	{
		public string Name;
		public object Default;
		public char Short;
		public bool Optional;
		public bool Global;
		public string Help;
		
		public ParameterAttribute(){}
		
		public ParameterAttribute(string name)
		{
			this.Name = name;
		}
	}
}