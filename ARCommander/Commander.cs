using System;
using System.Collections.Generic;
using System.Linq;

namespace ARCommander
{
	public class Commander
	{
		public Dictionary<string, Commander> SubCommands = new Dictionary<string, Commander>();
		public Dictionary<string, OptionalArgument> OptionalArguments = new Dictionary<string, OptionalArgument>();
		public List<object> PositionalArguments;
		public Action<ParsedArguments> Action {get;set;}

		public void AddArgument(OptionalArgument arg)
		{
			// argument validation here

			OptionalArguments.Add(arg.Name.ToLower(), arg);
		}
		
		public void AddArgument(PositionalArgument arg)
		{
			// argument validation here

			PositionalArguments.Add(arg);
		}

		public Commander AddSubCommand(string name)
		{
			// if there is a global optional argument, add it here now
			Commander subcmd = new Commander();

			SubCommands.Add(name, subcmd);

			return subcmd;
		}

		public void SetAction(Action<ParsedArguments> action)
		{
			this.Action = action;
		}

		public ParsedArguments Parse(string[] args)
		{

			ParsedArguments parsedArguments = new ParsedArguments();

			for(int i = 0; i < args.Length; i++)
			{
				string arg = args[i];

				if(i == 0 && SubCommands.ContainsKey(arg))
				{
					Commander subParser = SubCommands.GetValueOrDefault(arg);
					return subParser.Parse(args.Skip(1).ToArray());
				}

				if(arg.StartsWith("--"))
				{
					string name = arg.Substring(2, arg.Length-2).ToLower();
					
					string value = args[i+1];

					OptionalArgument mathingArgument = OptionalArguments.GetValueOrDefault(name, null);
					if(mathingArgument == null)
					{
						throw new ParsingException($"Unknown flag '{name}'.");
					}

					parsedArguments.OptionalArguments.Add(name, value);

					i++;
					continue;
				}
			}

			return parsedArguments;
		}

		public void Execute(string[] args)
		{
			ParsedArguments pargs = Parse(args);

			// Execute the subcommand's action if any
			for(int i = 0; i < args.Length; i++)
			{
				string arg = args[i];

				if(i == 0 && SubCommands.ContainsKey(arg))
				{
					Commander subParser = SubCommands.GetValueOrDefault(arg);
					subParser.Execute(args.Skip(1).ToArray());
					return;
				}
			}

			// if no subcommand, invoke this commander's action
			this.Action.Invoke(pargs);
		}
	}
}