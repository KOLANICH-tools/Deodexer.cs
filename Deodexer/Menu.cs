using System;
using System.Collections.Generic;
using System.Linq;
using Deodexer.Commands;

namespace Deodexer
{
	
	class Menu
	{
		public Dictionary<string, Command> commands=new Dictionary<string, Command>();
		public void exec(string command, IEnumerable<string> args=null){
			var args1 = args != null ? args.ToList() : new List<string>();
			if (commands.ContainsKey(command)) {
				commands[command].preprocessArguments(args1);
				commands[command].exec(args1);
			} else {
				Console.WriteLine("Command not found");
			}
		}

		public void addDefaultCommands(){
			commands.Add("help", new HelpCommand(this));
			commands.Add("echo", new EchoCommand());
			commands.Add("credits", new CreditsCommand());
			commands.Add("exit", new ExitAppCommand());
		}
	}

}
