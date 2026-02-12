using System.Security.Claims;
using System.Text;

namespace Ans.Net10.Web
{

	public static partial class _e_ClaimsPrincipal
	{

		public static bool HasActionClaim(
			this ClaimsPrincipal principal,
			string value)
		{
			return principal.HasClaim(_Consts.CLAIM_ACTIONS_TYPE, value);
		}


		public static bool HasActionClaim(
			this ClaimsPrincipal principal,
			Func<string, bool> funcValue)
		{
			return principal.HasClaim(
				x => x.Type == _Consts.CLAIM_ACTIONS_TYPE
					&& funcValue(x.Value));
		}


		public static bool AllowAccessAction(
			this ClaimsPrincipal principal,
			string path)
		{
			if (principal.HasActionClaim(path)
				|| principal.HasActionClaim(x => x.StartsWith($"{path}.")))
				return true;
			var sb1 = new StringBuilder();
			foreach (var item1 in path.Split('.'))
			{
				sb1.Append(item1);
				if (principal.HasActionClaim(sb1.ToString()))
					return true;
				sb1.Append('.');
			}
			return false;
		}

	}

}
