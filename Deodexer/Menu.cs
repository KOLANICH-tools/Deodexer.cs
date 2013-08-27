using System;
using System.Collections.Generic;
using System.Text;

namespace Deodexer
{
	abstract class Command
	{
		public string description = "Description is not available";
		public string detailedDescription = null;
		abstract public void exec(string restOfString = "");
	}

	class ExitAppCommand : Command
	{
		public ExitAppCommand(){
			description = "exits application";
			detailedDescription = "exits this application";
		}
		override public void exec(string restOfString = "") {
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

		override public void exec(string restOfString = "") {
			if (string.IsNullOrEmpty(restOfString)) {
				foreach (var cmd in menu.commands) {
					Console.WriteLine(cmd.Key + " :" + Environment.NewLine + "\t" + cmd.Value.description.Replace(Environment.NewLine, Environment.NewLine + "\t"));
				}
			} else {
				if (menu.commands.ContainsKey(restOfString)) {
					Console.WriteLine(restOfString +
						Environment.NewLine+ menu.commands[restOfString].description +
						(
							menu.commands[restOfString].detailedDescription!=null
							?
							(Environment.NewLine + menu.commands[restOfString].detailedDescription)
							:
							""
						)
					);
				}
			}
		}
	}
	class CreditsCommand : Command
	{
		public CreditsCommand(){
			description = "Shows credits";
		}

		override public void exec(string restOfString = ""){
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
		public void exec(string command, string restOfString="") {
			if (commands.ContainsKey(command)) {
				commands[command].exec(restOfString);
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
