using System.Collections.Generic;
using Deodexer.PathPlaceholders;

namespace Deodexer.Commands
{
	abstract class Command
	{
		public string description = "Description is not available";
		public string detailedDescription = null;
		abstract public void exec(List<string> args = null);
		public List<IPathPlaceholder> placeholders=new List<IPathPlaceholder>();
		public IPathPlaceholder defaultPlaceholder;

		public void preprocessArguments(List<string> args){
			List<string> nm;
			if(args.Count==0 && defaultPlaceholder!=null)
				args.AddRange(defaultPlaceholder.exec());
			foreach (var placeholder in placeholders) {
				for (int i = 0; i < args.Count; i++) {
					nm = placeholder.expand(args[i]);
					if (nm != null) {
						args.RemoveAt(i);
						args.InsertRange(i, nm);
					}
				}
			}
		}
	}
}
