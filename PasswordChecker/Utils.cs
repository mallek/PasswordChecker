using System.Security.Cryptography;
using System.Text;

namespace TravisHaley.PasswordChecker
{
	public class Utils
	{
		public static string GetSha1Hash(string input)
		{
			using (SHA1Managed shA1Managed = new SHA1Managed())
			{
				byte[] hash = shA1Managed.ComputeHash(Encoding.UTF8.GetBytes(input));
				StringBuilder stringBuilder = new StringBuilder(hash.Length * 2);
				foreach (byte num in hash)
					stringBuilder.Append(num.ToString("X2"));
				return stringBuilder.ToString();
			}
		}
	}
}