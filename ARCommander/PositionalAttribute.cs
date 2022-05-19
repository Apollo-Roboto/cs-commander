using System;

namespace ARCommander
{
	[System.AttributeUsage(System.AttributeTargets.Field)]
	public class PositionalAttribute : System.Attribute
	{
		/// <summary>
		/// The name of the Argument. Used for documentation.
		/// </summary>
		public string Name;

		/// <summary>
		/// The position of the argument. Starts at zero.
		/// </summary>
		public int Position;

		/// <summary>
		/// Help text describing this argument.
		/// </summary>
		public string Help;

		/// <summary>
		/// If true, the command will fail when this argument is missing. <br/>
		/// If false, the value will be the default when missing.
		/// </summary>
		public bool Required;

		public PositionalAttribute() { }

		public PositionalAttribute(string name, int position)
		{
			this.Name = name;
			this.Position = position;
		}
	}
}