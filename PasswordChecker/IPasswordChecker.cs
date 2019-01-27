using System.Collections.Generic;

namespace TravisHaley.PasswordChecker
{
	public interface IPasswordChecker
	{
		bool IsPasswordPwned(string plainTextPassword);
		int GetNumberOfTimesPasswordPwned(string plainTextPassword);
		List<string> GetPwned(params string[] passwords);
		List<string> GetPwned(IEnumerable<string> passwords);
	}
}