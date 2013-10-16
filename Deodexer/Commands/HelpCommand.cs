using System;
using System.Collections.Generic;

namespace Deodexer.Commands
{
	class HelpCommand : Command
	{
		private Menu menu;

		public HelpCommand(Menu menu) {
			this.menu = menu;
			description = "Shows this help";
		}

		override public void exec(List<string> cmds = null) {
			if (cmds.Count==0) {
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
					} else {
						Console.ForegroundColor = ConsoleColor.DarkRed;
						Console.WriteLine("Command " + cmd + "is not found!!!");
					}
			}
		}
	}
}