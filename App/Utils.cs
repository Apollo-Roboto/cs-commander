using System;
using ARCommander;
using System.Collections.Generic;

namespace App
{
	public static class Utils
	{
		public static void PrintAllCommands(Commander commander, int depth = 0)
		{
			Dictionary<string, Commander> commands = commander.SubCommands;

			foreach(string key in commands.Keys)
			{

				Console.WriteLine($"{new string('\t', depth)}{key}");

				Commander cmd = commands.GetValueOrDefault(key);
				if(cmd.SubCommands.Count > 0)
				{
					PrintAllCommands(cmd, depth+1);
				}
			}
		}
	}
}