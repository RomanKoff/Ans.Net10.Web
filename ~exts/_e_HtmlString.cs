using Ans.Net10.Common;
using Microsoft.AspNetCore.Html;

namespace Ans.Net10.Web
{

	public static partial class _e_HtmlString
	{

		/* functions */


		public static string GetMasked(
			this HtmlString html)
		{
			return SuppTypograph.GetHtml2Text(html.ToString());
		}

	}

}
