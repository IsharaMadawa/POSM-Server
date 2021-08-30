using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using POSM.FX.Diagnostics.Logging.LogCategories;

namespace POSM.FX.Diagnostics.Logging
{
	public interface IPOSMLogger<T>
	{
		void Log(string message, LogLevel LogLevel = LogLevel.Information, string user = "", string data = "", int categoryId = (int) LogCategoryEnum.LogCategory_Information);
		void Log(string message, LogLevel LogLevel = LogLevel.Information, Dictionary<string, object> additionalProperties = null);
	}
}
