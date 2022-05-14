using System;
using System.Reflection;
using System.IO;
using ARCommander;

namespace App
{
	class RootCommand
	{
		[Parameter("output", 'o')]
		public SupportedOutput Output;

		[Positional("file", 0)]
		public string File;
	}

	enum SupportedOutput
	{
		JSON, CSV, XML, YAML
	}

	class Program
	{
		static void Main(string[] args)
		{
			// args = new string[]{"--output", "CSV"};

			var cmd = new Commander<RootCommand>();
			
			Utils.Info<RootCommand>();

			RootCommand options = cmd.Parse(args);

			Console.WriteLine($"Final value for Output is {options.Output}");
			Console.WriteLine($"Final value for File is {options.File}");

		}
	}
}
