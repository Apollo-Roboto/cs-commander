using System;
using System.Reflection;
using System.IO;
using ARCommander;

namespace App
{
	#pragma warning disable 0649
	class RootCommand
	{
		[Parameter("output", 'o')]
		public SupportedOutput Output = SupportedOutput.JSON;

		[Positional("file", 0)]
		public string File;
	}
	#pragma warning restore 0649

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
