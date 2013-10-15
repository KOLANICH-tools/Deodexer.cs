using System;
using System.Collections.Generic;
using System.Linq;

namespace Deodexer
{
	abstract class Command
	{
		public string description = "Description is not available";
		public string detailedDescription = null;
		abstract public void exec(string[] args=null);
	}

	class ExitAppCommand : Command
	{
		public ExitAppCommand(){
			description = "exits application";
			detailedDescription = "exits this application";
		}
		override public void exec(string[] args=null) {
			Environment.Exit(0);
		}
	}

	class HelpCommand : Command
	{
		private Menu menu;

		public HelpCommand(Menu menu) {
			this.menu = menu;
			description = "Shows this help";
		}

		override public void exec(string[] cmds=null) {
			if (cmds==null) {
				foreach (var cmd in menu.commands) {
					Console.WriteLine(cmd.Key + " :" + Environment.NewLine + "\t" + cmd.Value.description.Replace(Environment.NewLine, Environment.NewLine + "\t"));
				}
			} else {
				foreach (var cmd in cmds)
					if (menu.commands.ContainsKey(cmd)) {
						Console.ForegroundColor = ConsoleColor.Blue;
						Console.WriteLine(cmd +
										  Environment.NewLine + menu.commands[cmd].description +
										  (
											  menu.commands[cmd].detailedDescription != null
												  ? (Environment.NewLine + menu.commands[cmd].detailedDescription)
												  : ""
											  )
							);
					}
					else {
						Console.ForegroundColor = ConsoleColor.DarkRed;
						Console.WriteLine("Command "+cmd +"is not found!!!");
					}
			}
		}
	}
	class CreditsCommand : Command
	{
		public CreditsCommand(){
			description = "Shows credits";
		}

		override public void exec(string[] args=null) {
			Console.WriteLine();
			Console.WriteLine("Deodexer");
			Console.WriteLine("Copyleft by KOLANICH, 2013");
			Console.WriteLine(@"The license is Apache License. More info in https://github.com/KOLANICH/Deodexer/blob/master/LICENSE and http://tldrlegal.com/license/apache-license-2.0-%28apache-2.0%29");
			Console.WriteLine(@"More info on Github: https://github.com/KOLANICH/Deodexer/");
		}
	}

	class Menu
	{
		public Dictionary<string, Command> commands=new Dictionary<string, Command>();
		public void exec(string command, IEnumerable<string> args=null){
			string[] args1 = args != null ? args.ToArray() : null;
			/*for (var i = 0; i < args1.Length; i++) {
				//TODO: make templates global (also use command pattern for it)
			}*/
			if (commands.ContainsKey(command)) {
				commands[command].exec(args1);
			} else {
				Console.WriteLine("Command not found");
			}
		}

		public void addDefaultCommands(){
			commands.Add("help", new HelpCommand(this));
			commands.Add("credits", new CreditsCommand());
			commands.Add("exit", new ExitAppCommand());
		}
	}

}
