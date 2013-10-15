using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Deodexer
{
	
	class Program
	{
		//thanks to http://stackoverflow.com/a/749653/450946
		[DllImport("shell32.dll", SetLastError = true)]
		static extern IntPtr CommandLineToArgvW(
			[MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine, out int pNumArgs);

		public static string[] CommandLineToArgs(string commandLine) {
			int argc;
			var argv = CommandLineToArgvW(commandLine, out argc);
			if (argv == IntPtr.Zero)
				throw new System.ComponentModel.Win32Exception();
			try {
				var args = new string[argc];
				for (var i = 0; i < args.Length; i++) {
					var p = Marshal.ReadIntPtr(argv, i * IntPtr.Size);
					args[i] = Marshal.PtrToStringUni(p);
				}

				return args;
			}
			finally {
				Marshal.FreeHGlobal(argv);
			}
		}

		[STAThread]
		static void Main(string[] args){
			var menu = new Menu();
			menu.addDefaultCommands();
			var deodexer = new Deodexer();
			menu.commands.Add("deodex",new DeodexCommand(deodexer));
			menu.commands.Add("framework", new SetFrameworkCommand(deodexer));



			if (args.Length > 0)
			{
				menu.exec(args[0], (args.Length >= 1 ? args.Skip(1) : null));
				return;
			}

			menu.exec("help");

			while (true){
				Console.ForegroundColor=ConsoleColor.Green;
				Console.Write("deodexer> ");
				Console.ResetColor();
				var line = Console.ReadLine();
				if(line==null)continue;

				var commandTextRep=CommandLineToArgs(line);
				if (commandTextRep.Length>0) {
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.BackgroundColor = ConsoleColor.White;
					menu.exec(commandTextRep[0], (commandTextRep.Length >= 2 ? commandTextRep.Skip(1) : null));
					Console.ResetColor();
				}
			}
		}
	}
}
