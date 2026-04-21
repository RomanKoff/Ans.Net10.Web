using Microsoft.AspNetCore.Mvc;

namespace Ans.Net10.Web.Controllers
{

	public class _NodeController_Base(
		CurrentContext current)
		: Controller
	{

		/* actions */


		public virtual IActionResult Page(
			string path)
		{
			var path1 = current.Request.NodesParsePath(path);
			return path1 switch
			{
				null => NotFound(),
				"" => View($"/Views/Nodes{current.Request.ViewPath}.cshtml"),
				_ => Redirect(path1)
			};
		}

	}

}
