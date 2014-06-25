using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Konami.Tests
{
	[TestClass]
	public class KonamiCodeTests
	{
		[TestMethod]
		public void TestMethod1()
		{
			var couponCode = new KonamiCode
			{
				RequirementsTest =
					"{ var isValid = coupon.Person.Name == \"Alex\" && DateTime.Now > DateTime.Parse(\"1/1/2014\"); if (!isValid) { coupon.ErrorMessages.Add(\"Sorry, buddy\"); } return isValid; }",
				Action = "{ coupon.Person.Name = coupon.Person.Name.ToUpper(); }"
			};


			var repo = Substitute.For<IKonamiRepository>();
			repo.GetCode(Arg.Any<string>(), Arg.Any<string>()).Returns(couponCode);


			var person = new Person { Name = "Alex" };

			var coupons = new KonamiCodeProcessor<FirstKonamiCode>("", "", repo, coupon => { coupon.Person = person; });

			var result = coupons.Process();

			Assert.AreEqual(0, result.ErrorMessages.Count);
			Assert.AreEqual("ALEX", person.Name);
		}
	}
}