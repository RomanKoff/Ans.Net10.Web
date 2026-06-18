using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Ans.Net10.Web.Areas.Ans.Pages.Errors
{

	public class HttpErrorsModel(
		ILogger<HttpErrorsModel> logger,
		CurrentContext current)
		: AnsPageErrorModel(current)
	{

		public void OnGet(
			int code)
		{
			Init();
			HttpCode = code;
			logger.LogError(
				"http-{HttpCode} | {OriginalPath} | {RefererUri} | {RequestId} | {ExceptionMessage}",
				HttpCode, OriginalPath, RefererUri, RequestId, ExceptionMessage);
			Debug.WriteLine($"[Ans.Net10.Web] HttpErrors : {HttpCode}");
		}

	}

}
