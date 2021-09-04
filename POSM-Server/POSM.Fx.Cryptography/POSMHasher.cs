using System.Security.Cryptography;
using POSM.Fx.Cryptography.Interfaces;

namespace POSM.Fx.Cryptography
{
	public class POSMHasher : IPOSMHasher
	{
		public string PasswordHash(string password)
		{
			byte[] salt;
			salt = GenerateSaltNewInstance(16);

			Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
			// IsharaK[20/08/2021] : GetByte(20) - 20 value should be same as iteration count in below "ValidatePasswordHash" function
			byte[] hash = pbkdf2.GetBytes(20);

			byte[] hashBytes = new byte[36];
			Array.Copy(salt, 0, hashBytes, 0, 16);
			Array.Copy(hash, 0, hashBytes, 16, 20);

			return Convert.ToBase64String(hashBytes);
		}

		public byte[] GenerateSaltNewInstance(int size)
		{
			using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
			{
				byte[] salt = new byte[size];
				generator.GetBytes(salt);
				return salt;
			}
		}

		public bool ValidatePasswordHash(string password, string dbPassword)
		{
			byte[] hashBytes = Convert.FromBase64String(dbPassword);

			byte[] salt = new byte[16];
			Array.Copy(hashBytes, 0, salt, 0, 16);

			Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
			byte[] hash = pbkdf2.GetBytes(20);

			// IsharaK[20/08/2021] : Iteration count - 20 value should be same as GetByte value in above "PasswordHash" function
			for (int i = 0; i < 20; i++)
			{
				if (hashBytes[i + 16] != hash[i])
				{
					return false;
				}
			}

			return true;
		}
	}
}
