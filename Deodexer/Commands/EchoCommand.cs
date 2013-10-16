using System;
using System.Collections.Generic;
using Deodexer.PathPlaceholders;

namespace Deodexer.Commands
{
	class EchoCommand:Command
	{

		public EchoCommand(){
			description = "Used to test different placeholders";
			placeholders.Add(new FileDialogPlaceholder());
			placeholders.Add(new FolderDialogPlaceholder());
		}

		override public void exec(List<string> cmds){
			foreach (var cmd in cmds) {
				Console.Write(cmd);
				Console.Write('\t');
			}
			Console.WriteLine();
		}
	}
}
