using Ans.Net10.Common;
using Microsoft.AspNetCore.Html;

namespace Ans.Net10.Web
{

	public static partial class __e_to
	{

		/* functions */


		public static HtmlString ToHtml(
			this string value)
		{
			return (string.IsNullOrEmpty(value))
				? HtmlString.Empty
				: new HtmlString(value);
		}


		public static HtmlString ToHtml(
			this string value,
			bool useTypograf)
		{
			return (string.IsNullOrEmpty(value))
				? HtmlString.Empty
				: new HtmlString(useTypograf
					? SuppTypograph.GetTypografMin(value)
					: value);
		}


		public static HtmlString ToHtml(
			this string value,
			string template,
			bool useTypograf = false)
		{
			if (string.IsNullOrWhiteSpace(value))
				return null;
			var s1 = (useTypograf)
				? SuppTypograph.GetTypografMin(value)
				: value;
			return s1.Make(template)
				.ToHtml();
		}

	}

}
