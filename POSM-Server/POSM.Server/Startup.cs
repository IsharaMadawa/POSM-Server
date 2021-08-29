using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using POSM.APIs.GraphQLServer.StartupConfig.ServiceConfig;
using POSM.Core.Data.Db.Models;

namespace POSM.APIs.GraphQLServer
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<POSMDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			GraphQLConfig.ConfigServices(services);
			BusinessConfig.ConfigServices(services);
		}

		// IsharaK[29/08/2021] : This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/", async context =>
				{
					await context.Response.WriteAsync("POSM Server working!");
				});
				endpoints.MapGraphQL();
			});

			app.UseGraphQLVoyager(new VoyagerOptions()
			{
				GraphQLEndPoint = "/graphql"
			}, "/graphql-voyager");
		}
	}
}
