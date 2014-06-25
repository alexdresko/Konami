using System;
using System.Collections.Generic;
using System.Linq;
using CSScriptLibrary;

namespace Konami
{
	public class KonamiCodeProcessor<T> where T : KonamiCodeBase, new()
	{
		private readonly string _couponCode;
		private readonly string _couponName;
		private readonly Action<T> _couponProcessSetup;
		private readonly Action<T> _couponTestSetup;
		private readonly IKonamiRepository _iKonamiRepository;

		public KonamiCodeProcessor(string couponName, string couponCode, IKonamiRepository iKonamiRepository,
			Action<T> couponTestSetup, Action<T> couponProcessSetup = null)
		{
			_couponName = couponName;
			_couponCode = couponCode;
			_iKonamiRepository = iKonamiRepository;
			_couponTestSetup = couponTestSetup;
			_couponProcessSetup = couponProcessSetup;
			Namespaces = new List<string>();
		}

		public List<string> Namespaces { get; set; }

		public T Process()
		{
			var couponCode = _iKonamiRepository.GetCode(_couponName, _couponCode);
			var coupon = new T {KonamiCode = couponCode};
			var namespaces = coupon.GetNamespaces();
			if (namespaces != null)
			{
				Namespaces.AddRange(namespaces);
			}

			_couponTestSetup(coupon);

			var scriptCode =
				String.Format(
					"using System;{2} namespace Konami.Core {{ public static class Executor {{ public static void Initialize({3} coupon) {{ coupon.IsValid = () => {0}; coupon.Process = () => {1};  }} }} }}",
					coupon.KonamiCode.RequirementsTest, coupon.KonamiCode.Action,
					String.Join(" ", Namespaces.Select(p => "using " + p + ";")), coupon.GetType().ToString());

			var helper = new AsmHelper(CSScript.LoadCode(scriptCode, null, false));
			helper.Invoke("Konami.Core.Executor.Initialize", coupon);
			if (coupon.IsValid())
			{
				if (_couponProcessSetup != null)
				{
					_couponProcessSetup(coupon);
				}

				coupon.Process();
			}

			return coupon;
		}
	}
}