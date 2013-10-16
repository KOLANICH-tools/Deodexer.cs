using System;
using System.Collections.Generic;
using System.IO;

namespace Deodexer.Commands
{
	class FrameworkCommand : FolderProcessingCommand
	{
		private Deodexer deodexer;
		public FrameworkCommand(Deodexer deodexer) {
			description = "Used to set framework directory.";
			this.deodexer = deodexer;
		}

		public override void exec(List<string> dirs) {
			string dir = dirs.Count == 0 ? Deodexer.defaultFrameworkDir : dirs[0];
			if (Directory.Exists(dir))
				deodexer.frameworkDir = dir;
			else {
				var prevCol = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Directory doesn't exist");
				Console.ForegroundColor = prevCol;
			}
		}

		protected override string title {
			get { return "Select the folder you want to set as framework folder"; }
		}
	}
}
