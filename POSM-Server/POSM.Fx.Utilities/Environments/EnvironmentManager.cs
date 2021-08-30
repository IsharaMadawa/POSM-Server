using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using POSM.Fx.Utilities.Interfaces;

namespace POSM.Fx.Utilities.Environments
{
    public class EnvironmentManager : IEnvironmentManager
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public EnvironmentManager(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public string GetCurrentEnvironment()
        {
            return webHostEnvironment.EnvironmentName;
        }

        public bool IsEnvironment(string environmentName)
        {
            return webHostEnvironment.IsEnvironment(environmentName);
        }

        public bool IsLocalhost()
        {
            return IsEnvironment("Localhost");
        }

        public bool IsDevelopment()
        {
            return webHostEnvironment.IsDevelopment();
        }

        public bool IsTest()
        {
            return IsEnvironment("Test");
        }

        public bool IsProduction()
        {
            return IsEnvironment("Production");
        }

        public string GetPath(string path)
        {
            return webHostEnvironment.ContentRootPath + "/" + path;
        }
    }
}
