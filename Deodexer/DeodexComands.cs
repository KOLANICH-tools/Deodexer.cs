using System;
using System.IO;
using System.Windows.Forms;

namespace Deodexer
{
	class DeodexCommand : Command,IDisposable
	{
		private Deodexer deodexer;
		private OpenFileDialog dialog = new OpenFileDialog();

		public DeodexCommand(Deodexer deodexer){
			description = "Deodexes a apk/jar file and aligns it";
			detailedDescription = "usage: deodex file1 file2 file3 file4 etc" +Environment.NewLine+
								  "fileN - path of a file or a folder or one of the constants:" +Environment.NewLine+
								  "\t%ASK% - shows a dialog for picking file" +Environment.NewLine+
								  "\t? - alias for ask" +Environment.NewLine+
								  "\t%FRAMEWORK% - framework folder";
			this.deodexer = deodexer;
			dialog.AddExtension = true;
			dialog.Filter = "Android Java Packages|*.apk;*.jar|All Files|*.*";
			dialog.Title = "Select files to be deodexed";
		}

		public override void exec(string[] fileNames=null) {
			if (fileNames == null)
			{
				dialog.Multiselect = true;
				dialog.ShowDialog();
				fileNames = dialog.FileNames;
			}

			dialog.Multiselect = false;
			for(var i=0;i<fileNames.Length;i++)
			{
				switch (fileNames[i]) {
					case "?":
					case "%ASK%":
						if (dialog.ShowDialog() == DialogResult.OK)
							fileNames[i] = dialog.FileName;
						else{
							Console.WriteLine("File selection cancelled by user");
							fileNames[i] = "";
						}
					break;
					case "%FRAMEWORK%":
						fileNames[i] = deodexer.frameworkDir;
					break;
				}
			}
			foreach (string fn in fileNames)
				deodexFileOrDir(fn);
		}

		private void deodexFileOrDir(string fn){
			if (Directory.Exists(fn)){
				var dirInfo = new DirectoryInfo(fn);
				var dirs = dirInfo.GetDirectories();
				var files = dirInfo.GetFiles();
				foreach (var file in files){
					var ext = Path.GetExtension(file.FullName);
					if (ext==".jar"||ext==".apk")
						deodexFileOrDir(file.FullName);
				}
				foreach (var dir in dirs)
						deodexFileOrDir(dir.FullName);
			} else if (File.Exists(fn)) {
				deodexer.deodex(Path.GetFullPath(fn));
			}else {
				var prevCol = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("File " + fn + " doesn't exist!");
				Console.ForegroundColor = prevCol;
			}
		}

		public void Dispose() {
			dialog.Dispose();
		}
	}
	class SetFrameworkCommand : Command
	{
		private Deodexer deodexer;
		public SetFrameworkCommand(Deodexer deodexer) {
			description = "Used to set framework directory.";
			this.deodexer = deodexer;
		}

		public override void exec(string[] dirs = null){
			string dir = dirs == null ? Deodexer.defaultFrameworkDir : dirs[0];
			if (Directory.Exists(dir))
				deodexer.frameworkDir = dir;
			else {
				var prevCol = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Directory doesn't exist");
				Console.ForegroundColor = prevCol;
			}
		}
	}
}
