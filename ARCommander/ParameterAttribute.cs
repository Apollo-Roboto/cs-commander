using System;

namespace ARCommander
{
	[System.AttributeUsage(System.AttributeTargets.Field)]
	public class ParameterAttribute : System.Attribute
	{
		/// <summary>
		/// The name of the Argument. Used as parameter name and documentation.
		/// </summary>
		public string Name;

		/// <summary>
		/// The name of the Argument. Used as a parameter shortcut.
		/// </summary>
		public char ShortName;

		/// <summary>
		/// Help text describing this argument.
		/// </summary>
		public string Help;

		/// <summary>
		/// If true, the command will fail when this argument is missing. <br/>
		/// If false, the value will be the default when missing.
		/// </summary>
		public bool Required = false;

		public ParameterAttribute() { }

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