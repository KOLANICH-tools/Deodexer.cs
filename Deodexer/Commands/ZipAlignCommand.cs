using System;
using System.Collections.Generic;

namespace Deodexer.Commands
{
	class ZipAlignCommand : FileProcessingComand
	{
		private Deodexer deodexer;
		public ZipAlignCommand(Deodexer deodexer) {
			description = "Used to set bulk aligning of zips." + Environment.NewLine;
			this.deodexer = deodexer;
		}

		public override void exec(List<string> fileNames) {
			foreach (string fn in fileNames)
				processFileOrDir(fn);
		}
		protected override void processFile(string filename) {
			deodexer.zipalign(filename);
		}

		protected override string fileTypes {
			get { return "Zip files|*.apk;*.jar;*.zip|All Files|*.*"; }
		}

		protected override string title {
			get { return "Select Zip files to align"; }
		}
	}
}
