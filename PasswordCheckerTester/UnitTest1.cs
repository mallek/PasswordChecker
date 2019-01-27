using NUnit.Framework;
using TravisHaley.PasswordChecker;

namespace PasswordCheckerTester
{
	public class Tests
	{
		private IPasswordChecker _passwordChecker;

		[SetUp]
		public void Setup()
		{
			_passwordChecker = new PasswordChecker();
		}

		[Test]
		public void How_Many_Times_Is_Password_Pwned()
		{
			int sut = _passwordChecker.GetNumberOfTimesPasswordPwned("P@ssw0rd");
			Assert.IsTrue(sut > 0);
		}

		[Test]
		public void Is_Password_Pwned()
		{
			bool sut = _passwordChecker.IsPasswordPwned("P@ssw0rd");
			Assert.IsTrue(sut);
		}
	}
}