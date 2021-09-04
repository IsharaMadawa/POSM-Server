namespace POSM.Fx.Cryptography.Interfaces
{
	public interface IPOSMHasher
	{
		string PasswordHash(string password);

		byte[] GenerateSaltNewInstance(int size);

		bool ValidatePasswordHash(string password, string dbPassword);
	}
}
