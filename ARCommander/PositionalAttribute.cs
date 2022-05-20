using System;

namespace ARCommander
{
	public class PositionalAttribute : BaseAttribute
	{
		/// <summary>
		/// The position of the argument. Starts at zero.
		/// </summary>
		public int Position;

		public PositionalAttribute() { }

		public PositionalAttribute(string name, int position)
		{
			this.Name = name;
			this.Position = position;
		}
	}
}