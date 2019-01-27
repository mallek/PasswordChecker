using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace TravisHaley.PasswordChecker
{
	public class PasswordChecker : IPasswordChecker
	{
		private readonly string _apiAddress;
		private readonly int _numberOfLeaksForPwn = 1;

		public PasswordChecker() : this("https://api.pwnedpasswords.com/range", 1){}

		public PasswordChecker(string apiAddress, int numberOfLeaksForPwn)
		{
			_apiAddress = apiAddress;
			_numberOfLeaksForPwn = numberOfLeaksForPwn;
		}

		public bool IsPasswordPwned(string plainTextPassword)
		{
			return this.GetNumberOfTimesPasswordPwned(plainTextPassword) >= this._numberOfLeaksForPwn;
		}

		public int GetNumberOfTimesPasswordPwned(string plainTextPassword)
		{
			string sha1Hash = Utils.GetSha1Hash(plainTextPassword);
			string firstFiveOfHash = sha1Hash.Substring(0, 5);
			string remainingSubstring = sha1Hash.Substring(5);
			using (WebClient webClient = new WebClient())
			{
				using (Stream stream = webClient.OpenRead($"{this._apiAddress}/{firstFiveOfHash}"))
				{
					if (stream == null)
						return 0;
					using (StreamReader streamReader = new StreamReader(stream))
					{
						while (!streamReader.EndOfStream)
						{
							string str3 = streamReader.ReadLine();
							if (str3 != null)
							{
								string[] strArray = str3.Split(':');
								string str4 = strArray[0];
								int num = int.Parse(strArray[1]);
								if (str4 == remainingSubstring)
									return num;
							}
						}
					}
				}
			}
			return 0;
		}

		public List<string> GetPwned(params string[] passwords)
		{
			return ((IEnumerable<string>)passwords).Where<string>(new Func<string, bool>(this.IsPasswordPwned)).ToList<string>();
		}

		public List<string> GetPwned(IEnumerable<string> passwords)
		{
			return this.GetPwned(passwords.ToArray<string>());
		}
	}
}
