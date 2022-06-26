using System;
using System.Reflection;
using System.IO;
using ApolloRoboto.Commander;

namespace App
{
	class RootCommand
	{
		[Parameter("output", 'o', Help="Output format")]
		public SupportedOutput Output = SupportedOutput.JSON;

		[Positional("path", 0, Required=true, Help="Path to the file")]
		public string Path;

		[SubCommand("validate")]
		public Validate ValidateCommand;
	}

	class ValidateCommand
	{
		[Positional("path", 0, Required=true, Help="Path to the file to validate")]
		public string Path;
	}

	enum SupportedOutput
	{
		JSON, CSV, XML, YAML
	}

	class Program
	{
		static void Main(string[] args)
		{
			// args = new string[]{"./out.json", "-o", "json"};
			
			var cmd = new Commander<RootCommand>();
			
			RootCommand options = cmd.Parse(args);
			
			if(options.Validate != null)
			{
				Console.WriteLine($"SubCommand Validate called with {options.Validate.Path}.")
			}
		}
	}
}
