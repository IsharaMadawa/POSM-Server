using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using POSM_Server.GraphQL;
using POSM_Server.GraphQL.InvoiceQuery;
using POSM_Server.GraphQL.ItemQuery;
using POSM_Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSM_Server
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddPooledDbContextFactory<POSMContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services
				.AddGraphQLServer()
				.AddQueryType<Query>()
				.AddType<ItemType>()
				.AddType<InvoiceType>()
				.AddMutationType<Mutation>()
				.AddProjections()
				.AddSorting()
				.AddFiltering();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
