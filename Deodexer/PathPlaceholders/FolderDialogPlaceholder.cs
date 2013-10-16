using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Deodexer.PathPlaceholders
{
	class FolderDialogPlaceholder : IPathPlaceholder
	{
		protected FolderBrowserDialog folderDialog = new FolderBrowserDialog();

		public FolderDialogPlaceholder(string title = null) {
			folderDialog.Description = title;
			folderDialog.ShowNewFolderButton = true;
		}

		public List<string> expand(string name) {
			switch (name) {
				case "??":
				case "%ASK_FOLDER%":
					return exec();
				default:
					return null;
			}
		}
		public void Dispose() {
			folderDialog.Dispose();
		}


		public List<string> exec() {
			if (folderDialog.ShowDialog() == DialogResult.OK)
				return new List<string> { folderDialog.SelectedPath };
			Console.WriteLine("File selection cancelled by user");
			return new List<string>();
		}
	}
}