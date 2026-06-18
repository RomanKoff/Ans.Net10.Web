using Microsoft.AspNetCore.Mvc;

namespace Ans.Net10.Web
{

	public class AnsPageSystemModel(
		CurrentContext current)
		: _AnsPageModel_Base(current)
	{

		public virtual IActionResult OnGet()
		{
			if (string.IsNullOrEmpty(Options.SystemToken))
				throw Options.GetExceptionParamRequired(nameof(Options.SystemToken));
			var token1 = Current.QueryString.GetString("token");
			if (token1 == Options.SystemToken)
				return null;
			return NotFound();
		}

	}

}
