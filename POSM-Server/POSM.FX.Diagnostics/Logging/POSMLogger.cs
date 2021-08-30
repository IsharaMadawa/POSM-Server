using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using POSM.FX.Diagnostics.Logging.LogCategories;

namespace POSM.FX.Diagnostics.Logging
{
	public class POSMLogger<T> : IPOSMLogger<T>
    {
        private readonly ILogger<T> logger;

        public POSMLogger(ILogger<T> logger)
        {
            this.logger = logger;
        }

        public void Log(string message, LogLevel LogLevel = LogLevel.Information, string user = "", string data = "", int categoryId = (int) LogCategoryEnum.LogCategory_Information)
        {
            Dictionary<string, object> additionalProperties = new Dictionary<string, object>
            {
                ["User"] = user,
                ["Data"] = data,
                ["CategoryId"] = categoryId
            };

            if (string.IsNullOrWhiteSpace(user))
            {
                additionalProperties.Remove("User");
            }

            if (string.IsNullOrWhiteSpace(data))
            {
                additionalProperties.Remove("Data");
            }

            using (logger.BeginScope(additionalProperties))
            {
                WriteLog(message, LogLevel);
            }
        }

        public void Log(string message, LogLevel LogLevel = LogLevel.Information, Dictionary<string, object> additionalProperties = null)
        {
            if (additionalProperties != null && additionalProperties.Any())
            {
                using (logger.BeginScope(additionalProperties))
                {
                    WriteLog(message, LogLevel);
                }
            }
            else
            {
                WriteLog(message, LogLevel);
            }
        }

        private void WriteLog(string message, LogLevel LogLevel)
        {
            switch (LogLevel)
            {
                case LogLevel.Critical:
                    logger.LogCritical(message);
                    break;
                case LogLevel.Debug:
                    logger.LogDebug(message);
                    break;
                case LogLevel.Error:
                    logger.LogError(message);
                    break;
                case LogLevel.Information:
                    logger.LogInformation(message);
                    break;
                case LogLevel.Trace:
                    logger.LogTrace(message);
                    break;
                case LogLevel.Warning:
                    logger.LogWarning(message);
                    break;
                default:
                    logger.Log(LogLevel.None, message);
                    break;
            }
        }
    }
}
