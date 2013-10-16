using System;
using System.Collections.Generic;

namespace Deodexer.Commands
{
	class ExitAppCommand : Command
	{
		public ExitAppCommand() {
			description = "exits application";
			detailedDescription = "exits this application";
		}
		override public void exec(List<string> args = null) {
			Environment.Exit(0);
		}
	}
}
