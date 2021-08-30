using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace POSM.APIs.GraphQLServer.StartupConfig.AppConfig
{
	public static class ExceptionConfig
	{
        public static void ConfigApp(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsEnvironment("Localhost"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }


            //IsharaK[30.08.2021] : overall system exception handling before GraphQL servers are accessed; after this middleware, a HTTP request will access a GraphQL server, and any exception later in the GraphQL server will be handled by GraphQLErrorFilter.
            app.UseStatusCodePages(async context =>
            {
                context.HttpContext.Response.ContentType = "application/json";

                string errorMessage;

                switch (context.HttpContext.Response.StatusCode)
                {
                    case (int) HttpStatusCode.NotFound:
                        errorMessage = "Resource was not found.";
                        break;
                    case (int) HttpStatusCode.InternalServerError:
                        errorMessage = "Internal server error occurred.";
                        break;
                    default:
                        errorMessage = "System error occurred.";
                        break;
                }

                await context.HttpContext.Response.WriteAsync("System error occurred before GraphQL servers launched. Error code: " + context.HttpContext.Response.StatusCode + ", error message: " + errorMessage);
            });
        }
    }
}
