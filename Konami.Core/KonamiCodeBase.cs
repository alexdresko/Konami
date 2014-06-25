using System;
using System.Collections.Generic;

namespace Konami
{
	public abstract class KonamiCodeBase
	{
		public List<string> ErrorMessages = new List<string>();
		public Func<bool> IsValid;
		public KonamiCode KonamiCode;
		public Action Process;
		public abstract List<string> GetNamespaces();
	}
}