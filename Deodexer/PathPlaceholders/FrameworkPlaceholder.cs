using System.Collections.Generic;

namespace Deodexer.PathPlaceholders
{
	class FrameworkPlaceholder: IPathPlaceholder
	{
		public Deodexer deodexer;
		public List<string> expand(string name) {
			switch (name) {
				case "%FRAMEWORK%":
					return exec();
				default:
					return null;
			}
		}
		public void Dispose() {}

		public List<string> exec() {
			return new List<string>{deodexer.frameworkDir};
		}
	}
}
