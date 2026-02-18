using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ans.Net10.Web.Attributes
{

	public class ActionAccessAttribute
		: TypeFilterAttribute
	{
		public ActionAccessAttribute(
			string path)
			: base(typeof(ActionAccessFilter))
		{
			Arguments = [path];
		}
	}



	public class ActionAccessFilter(
		string path)
		: IAuthorizationFilter
	{

		/* methods */


		public void OnAuthorization(
			AuthorizationFilterContext context)
		{
			var http1 = context.HttpContext;
			var user1 = http1.User;
			if (http1.IsClaimsAdmin()
				|| user1.AllowAccessAction(path))
				return;
			context.Result = new ForbidResult();
		}

	}



	public class ClaimRequirementAttribute
		: TypeFilterAttribute
	{
		public ClaimRequirementAttribute(
			string type,
			Func<string, bool> funcValue)
			: base(typeof(ClaimRequirementFilter))
		{
			Arguments = [type, funcValue];
		}
	}



	public class ClaimRequirementFilter(
		string type,
		Func<string, bool> funcValue)
		: IAuthorizationFilter
	{
		public void OnAuthorization(
			AuthorizationFilterContext context)
		{
			var f1 = context.HttpContext.User.Claims.Any(
				x => x.Type == type && funcValue(x.Value));
			if (!f1)
				context.Result = new ForbidResult();
		}
	}

}