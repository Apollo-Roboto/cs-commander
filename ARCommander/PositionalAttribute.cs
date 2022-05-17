using System;

namespace ARCommander
{
	[System.AttributeUsage(System.AttributeTargets.Field)]
	public class PositionalAttribute : System.Attribute
	{
		public string Name;
		public int Position;
		public string Help;
		public bool Required;
		
		public PositionalAttribute() { }
		
		public PositionalAttribute(string name, int position)
		{
			this.Name = name;
			this.Position = position;
		}
	}
}