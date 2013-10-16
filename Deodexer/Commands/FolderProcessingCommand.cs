using System;
using Deodexer.PathPlaceholders;

namespace Deodexer.Commands
{
	abstract class FolderProcessingCommand : Command, IDisposable
	{
		protected abstract string title { get; }

		protected FolderProcessingCommand() {
			description = "Used for folder operations";
			detailedDescription = "";
			placeholders.Add(new FolderDialogPlaceholder(title));
		}
		public void Dispose() {
			foreach (var placeholder in placeholders) {
				placeholder.Dispose();
			}
		}
	}
}