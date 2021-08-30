using HotChocolate;
using Microsoft.AspNetCore.Http;
using POSM.Fx.Utilities.Interfaces;
using POSM.FX.Diagnostics.ExceptionHandling.Exceptions;
using POSM.FX.Diagnostics.Logging;
using POSM.FX.Diagnostics.Logging.LogCategories;

namespace POSM.FX.Diagnostics.ExceptionHandling
{
	public class GraphQLErrorFilter : IErrorFilter
    {
        private readonly IPOSMLogger<LogCategory_Error> logger;
        private readonly IEnvironmentManager environmentManager;

        public GraphQLErrorFilter(IHttpContextAccessor httpContext)
        {
            this.logger = (IPOSMLogger<LogCategory_Error>) httpContext.HttpContext.RequestServices.GetService(typeof(IPOSMLogger<LogCategory_Error>));
            this.environmentManager = (IEnvironmentManager) httpContext.HttpContext.RequestServices.GetService(typeof(IEnvironmentManager));
        }

        public IError OnError(IError error)
        {
            string errorCode;
            string errorMessage;

            POSMException busException = error.Exception as POSMException;

            if (busException != null)
            {
                errorCode = busException.Code;
                errorMessage = busException.Message;
            }
            else
            {
                errorCode = error.Code;
                errorMessage = error.Exception == null ? error.Message : (error.Exception.InnerException == null ? error.Exception.Message : error.Exception.InnerException.Message);
            }


            logger.Log(errorMessage, Microsoft.Extensions.Logging.LogLevel.Error, errorCode, errorMessage, (int) LogCategoryEnum.LogCategory_Error);


            error = error.WithMessage(errorMessage)
                         .WithCode(errorCode);

            if (!environmentManager.IsLocalhost())
            {
                error = error.RemoveLocations()
                             .RemovePath()
                             .RemoveExtensions();
            }

            return error;
        }
    }
}
