﻿using System;
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
