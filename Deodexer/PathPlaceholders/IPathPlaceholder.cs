using System;
using System.Collections.Generic;

namespace Deodexer.PathPlaceholders
{
	interface IPathPlaceholder:IDisposable{
		List<string> expand(string name);
		List<string> exec();
	}
}