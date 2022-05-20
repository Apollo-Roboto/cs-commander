using System;

namespace ARCommander
{
	public class ParameterAttribute : BaseAttribute
	{

		/// <summary>
		/// The name of the Argument. Used as a parameter shortcut.
		/// </summary>
		public char ShortName;

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