using System;
using System.ComponentModel.DataAnnotations;

namespace Konami
{
	public class KonamiCode
	{
		public int KonamiCodeId { get; set; }

		[Required]
		public string Code { get; set; }

		public int MaxUsages { get; set; }

		public int TotalUsages { get; set; }

		public DateTime? ExpirationDateTime { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public string RequirementsTest { get; set; }

		[Required]
		public string Action { get; set; }
	}
}