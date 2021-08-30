namespace POSM.Fx.Utilities.Interfaces
{
	public interface IEnvironmentManager
	{
        string GetCurrentEnvironment();
        bool IsEnvironment(string environmentName);
        bool IsLocalhost();
        bool IsDevelopment();
        bool IsTest();
        bool IsProduction();
        string GetPath(string path);
    }
}
