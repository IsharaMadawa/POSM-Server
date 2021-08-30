using POSM.APIs.GraphQLServer.StartupConfig.AppConfig;
using POSM.APIs.GraphQLServer.StartupConfig.ServiceConfig;

namespace POSM.APIs.GraphQLServer
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// IsharaK[29/08/2021] : This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			LoggingServiceConfig.ConfigServices(services);
			DataConfig.ConfigServices(services, Configuration);
			GraphQLConfig.ConfigServices(services);
			ConfigurationConfig.ConfigServices(services);
			EnvironmentConfig.ConfigServices(services);
			SecurityConfig.ConfigServices(services, Configuration);
			BusinessConfig.ConfigServices(services);
			SystemConfig.ConfigServices(services);
		}

		// IsharaK[29/08/2021] : This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			ExceptionConfig.ConfigApp(app, env);
			RoutingConfig.ConfigApp(app);
			EndpointConfig.ConfigApp(app);
		}
	}
}
