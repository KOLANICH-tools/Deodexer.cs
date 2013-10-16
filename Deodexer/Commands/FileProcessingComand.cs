using System;
using System.IO;
using Deodexer.PathPlaceholders;

namespace Deodexer.Commands
{
	abstract class FileProcessingComand : FolderProcessingCommand
	{
		protected abstract string fileTypes {get;}

		protected FileProcessingComand() {
			description = "Used for file operations";
			detailedDescription = "fileN - path of a file or a folder or one of the constants:" + Environment.NewLine +
								  "\t%ASK% - shows a dialog for picking file" + Environment.NewLine +
								  "\t? - alias for ask" + Environment.NewLine;
			defaultPlaceholder = new FileDialogPlaceholder(fileTypes, title);
			placeholders.Add(defaultPlaceholder);
		}

		protected void processFileOrDir(string fn) {
			if (Directory.Exists(fn)) {
				var dirInfo = new DirectoryInfo(fn);
				var dirs = dirInfo.GetDirectories();
				var files = dirInfo.GetFiles();
				foreach (var file in files) {
					if(filter(file.FullName))
						processFileOrDir(file.FullName);	
				}
				foreach (var dir in dirs)
					processFileOrDir(dir.FullName);
			} else if (File.Exists(fn)) {
				processFile(Path.GetFullPath(fn));
			} else {
				var prevCol = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("File " + fn + " doesn't exist!");
				Console.ForegroundColor = prevCol;
			}
		}

		protected virtual bool filter(string filename) {
			return true;
		}

		protected virtual void processFile(string filename){
		}
	}
}
