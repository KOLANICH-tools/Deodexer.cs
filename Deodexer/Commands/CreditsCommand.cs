using System;
using System.Collections.Generic;

namespace Deodexer.Commands
{
	class CreditsCommand : Command
	{
		public CreditsCommand() {
			description = "Shows credits";
		}

		override public void exec(List<string> args = null) {
			Console.WriteLine();
			Console.WriteLine("Deodexer");
			Console.WriteLine("Copyleft by KOLANICH, 2013");
			Console.WriteLine(@"The license is Apache License. More info in https://github.com/KOLANICH/Deodexer/blob/master/LICENSE and http://tldrlegal.com/license/apache-license-2.0-%28apache-2.0%29");
			Console.WriteLine(@"More info on Github: https://github.com/KOLANICH/Deodexer/");
		}
	}
}