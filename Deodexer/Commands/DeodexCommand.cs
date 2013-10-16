using System;
using System.Collections.Generic;
using System.IO;

namespace Deodexer.Commands
{
	class DeodexCommand : FileProcessingComand
	{
		private Deodexer deodexer;
		protected override string fileTypes {
			get { return "Android Java Packages|*.apk;*.jar|All Files|*.*"; }
		}

		protected override string title{
			get { return "Select files to be deodexed"; }
		}

		public DeodexCommand(Deodexer deodexer){
			description = "Deodexes a apk/jar file and aligns it";
			detailedDescription = "usage: deodex file1 file2 file3 file4 etc" + Environment.NewLine;
			this.deodexer = deodexer;
		}

		public override void exec(List<string> fileNames) {
			foreach (string fn in fileNames)
				processFileOrDir(fn);
		}

		protected override bool filter(string filename) {
			var ext = Path.GetExtension(filename);
			return (ext == ".jar" || ext == ".apk");
		}
		
		protected override void processFile(string filename) {
			deodexer.deodex(filename);
		}
	}

}
