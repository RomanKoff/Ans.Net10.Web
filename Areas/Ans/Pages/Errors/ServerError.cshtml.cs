using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Ans.Net10.Web.Areas.Ans.Pages.Errors
{

	public class ServerErrorModel(
		ILogger<ServerErrorModel> logger,
		CurrentContext current)
		: AnsPageErrorModel(current)
	{

		public void OnGet()
		{
			Init();
			logger.LogError(
				"server500 | {OriginalPath} | {RefererUri} | {RequestId} | {ExceptionMessage}",
				 OriginalPath, RefererUri, RequestId, ExceptionMessage);
			Debug.WriteLine($"[Ans.Net10.Web] ServerError : \"{ExceptionMessage}\"");
		}

	}

}
