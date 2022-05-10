using System;
using System.IO;
using ARCommander;

namespace App
{
	class Program
	{
		static void Main(string[] args)
		{
			Commander cmd = new Commander();

			cmd.AddArgument(new OptionalArgument{
				Name="verbose",
				Short='v',
				Help="Write every logs.",
				Type=typeof(bool),
				Default=false,
				Required=false,
				Global=true,
			});
			
			cmd.SetAction(pargs => {
				Console.WriteLine("Root Action");
			});


			Commander createCmd = cmd.AddSubCommand("create");
			createCmd.SetAction(pargs => {
				Console.WriteLine("Create Action");
			});

			Commander createFileCmd = createCmd.AddSubCommand("file");
			createFileCmd.SetAction(pargs => {
				Console.WriteLine($"Create File Action: file name is {pargs.Get(0)}");
			});

			Console.WriteLine("----------------------------");
			Console.WriteLine("The commands are:\n");
			Utils.PrintAllCommands(cmd);
			Console.WriteLine("\n----------------------------");

			Console.WriteLine("Execute output:\n");
			cmd.Execute(args);
		}
	}
}
