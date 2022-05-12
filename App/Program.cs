using System;
using System.Reflection;
using System.IO;
using ARCommander;

namespace App
{
	class RootCommand
	{
		[Parameter(
			Name = "output",
			Default = SupportedOutput.JSON,
			Short = 'o'
		)]
		public SupportedOutput Output;
		
		[Positional("file", 0)]
		public string File;
	}

	enum SupportedOutput
	{
		JSON, CSV, XML
	}

	class Program
	{
		static void Main(string[] args)
		{

			var cmd = new Commander<RootCommand>();
			
			Utils.Info<RootCommand>();

			RootCommand options = cmd.Parse(args);

		}
	}
}
