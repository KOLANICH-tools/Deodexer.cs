using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Deodexer
{
	
	class Program
	{
		[STAThread]
		static void Main(string[] args){
			var menu = new Menu();
			menu.addDefaultCommands();
			var deodexer = new Deodexer();
			menu.commands.Add("deodex",new DeodexCommand(deodexer));
			menu.commands.Add("framework", new SetFrameworkCommand(deodexer));



			if (args.Length > 0)
			{
				var str = "";
				if(args.Length>1)
					for (var i = 1; i < args.Length; i++)
						str += args[i];
				menu.exec(args[0],str);
				return;
			}

			menu.exec("help");


			var rx = new Regex(@"^([^\s]+)(?:\s+(.+))?$");
			while (true)
			{
				Console.ForegroundColor=ConsoleColor.Green;
				Console.Write("deodexer> ");
				Console.ResetColor();
				var line = Console.ReadLine();
				if(line==null)continue;
				var match = rx.Match(line);
				if (match.Groups[1].Success){
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.BackgroundColor = ConsoleColor.White;
					menu.exec(match.Groups[1].Value, (match.Groups[2].Success ? match.Groups[2].Value : ""));
					Console.ResetColor();
				}
			}
		}
	}
}
