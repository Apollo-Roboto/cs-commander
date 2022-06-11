using System;

namespace ApolloRoboto.Commander
{
	[System.AttributeUsage(System.AttributeTargets.Field)]
	public abstract class BaseAttribute : System.Attribute
	{
		/// <summary>
		/// The name of the Argument. Used as parameter name and documentation.
		/// </summary>
		public string Name;

		/// <summary>
		/// Help text describing this argument.
		/// </summary>
		public string Help;

		/// <summary>
		/// If true, the command will fail when this argument is missing. <br/>
		/// If false, the value will be the default when missing.
		/// </summary>
		public bool Required = false;
	}
}