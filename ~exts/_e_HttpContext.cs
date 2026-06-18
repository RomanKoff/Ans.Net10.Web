using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Text.Encodings.Web;

namespace Ans.Net10.Web
{

	public static partial class _e_HttpContext
	{

		/* functions */


		private static string _baseUrl;
		public static string GetBaseUrl(
			this HttpContext context)
		{
			return _baseUrl ??= $"{context.Request.Scheme}://{context.Request.Host}";
		}


		private static string _virtualPath;
		public static string GetVirtualPath(
			this HttpContext context)
		{
			return _virtualPath ??= context.Request.PathBase;
		}


		private static string _applicationUrl;
		public static string GetApplicationUrl(
			this HttpContext context)
		{
			return _applicationUrl ??= context.GetBaseUrl() + context.GetVirtualPath();
		}


		public static string GetStringFromRazor(
			this HttpContext context,
			Func<dynamic, IHtmlContent> html)
		{
			var sb1 = new StringBuilder();
			using TextWriter writer1 = new StringWriter(sb1);
			var encoder1 = (HtmlEncoder)context.RequestServices
				.GetService(typeof(HtmlEncoder)) ?? HtmlEncoder.Default;
			html("").WriteTo(writer1, encoder1);
			return sb1.ToString();
		}

	}

}
