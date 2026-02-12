using System.Security.Claims;

namespace Ans.Net10.Web
{

	public class ActionClaimsHelper(
		ClaimsPrincipal principal)
	{

		private readonly ClaimsPrincipal _principal = principal;


		public bool AllowCatalog(
			string catalog)
		{
			return _principal.HasActionClaim(catalog);
		}


		public bool AllowController(
			string catalog,
			string controller)
		{
			var s1 = $"{catalog}.{controller}";
			return _principal.HasActionClaim(s1);
		}


		public bool AllowAction(
			string catalog,
			string controller,
			string action)
		{
			var s1 = $"{catalog}.{controller}.{action}";
			return _principal.HasActionClaim(s1);
		}

	}

}
