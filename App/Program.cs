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

		[Positional("path", 0, Required=true)]
		public string Path;
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
			// args = new string[]{"./out.json", "-o", "json"};
			
			var cmd = new Commander<RootCommand>();
			
			RootCommand options = cmd.Parse(args);
			
			switch(options.Output)
			{
				case SupportedOutput.JSON:
					string content = "{\"msg\":\"hello\"}";
					File.WriteAllText(options.Path, content);
					break;
				case SupportedOutput.CSV:
					File.WriteAllText(options.Path, "msg,hello");
					break;
				case SupportedOutput.XML:
					File.WriteAllText(options.Path, "<msg>hello</msg>");
					break;
				case SupportedOutput.YAML:
					File.WriteAllText(options.Path, "msg: hello");
					break;
			}
		}
	}
}
