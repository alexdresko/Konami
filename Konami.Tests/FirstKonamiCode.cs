using System.Collections.Generic;

namespace Konami.Tests
{
	public class FirstKonamiCode : KonamiCodeBase
	{
		public Person Person { get; set; }

		public override List<string> GetNamespaces()
		{
			return null;
		}
	}
}