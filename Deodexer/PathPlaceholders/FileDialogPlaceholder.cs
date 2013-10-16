using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Deodexer.PathPlaceholders
{
	class FileDialogPlaceholder : IPathPlaceholder
	{
		protected OpenFileDialog fileDialog = new OpenFileDialog();

		public FileDialogPlaceholder(string fileTypes = null, string title = null){
			fileDialog.AddExtension = true;
			fileDialog.Filter = fileTypes;
			fileDialog.Title = title;
			fileDialog.Multiselect = true;
		}

		public List<string> expand(string name){
			switch (name) {
				case "?":
				case "%ASK%":
					return exec();
				default:
					return null;
			}
		}
		public void Dispose() {
			fileDialog.Dispose();
		}


		public List<string> exec() {
			if (fileDialog.ShowDialog() == DialogResult.OK)
				return fileDialog.FileNames.ToList();
			Console.WriteLine("File selection cancelled by user");
			return new List<string>();
		}
	}
}
