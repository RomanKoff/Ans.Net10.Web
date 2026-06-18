using Microsoft.AspNetCore.Routing;
using System.Text;

namespace Ans.Net10.Web
{

	public class RequestData
	{

		private readonly CurrentContext _current;


		/* ctor */


		public RequestData(
			CurrentContext current)
		{
			_current = current;
			RelativeUrl = _current.HttpContext.Request.Path.ToString().ToLower().TrimEnd('/');
			AbsoluteUrl = $"{_current.Host.ApplicationUrl}{RelativeUrl}";
			Params = _current.HttpContext.Request.QueryString.Value;
			AreaName = current.HttpContext.GetRouteValue("Area")?.ToString();
			RazorPage = current.HttpContext.GetRouteValue("Page")?.ToString();
			ControllerName = current.HttpContext.GetRouteValue("Controller")?.ToString();
			ActionName = current.HttpContext.GetRouteValue("Action")?.ToString();
			ActionUniqueID = _getActionUID();
			IsController = !string.IsNullOrEmpty(ControllerName);
			IsRazorPage = !string.IsNullOrEmpty(RazorPage);
		}


		/* readonly properties */


		public string RelativeUrl { get; }
		public string AbsoluteUrl { get; }
		public string Params { get; }
		public string AreaName { get; }
		public string ControllerName { get; }
		public string ActionName { get; }
		public string RazorPage { get; }
		public string ActionUniqueID { get; }
		public bool IsController { get; }
		public bool IsRazorPage { get; }


		///// <summary>
		///// Это стартовая страница сайта
		///// </summary>
		//public bool IsStartSite { get; private set; }


		///// <summary>
		///// Это стартовая страница узла
		///// </summary>
		//public bool IsStartNode { get; private set; }


		///// <summary>
		///// Это стартовая страница
		///// </summary>
		//public bool IsStartPage { get; private set; }


		///// <summary>
		///// Имя узла
		///// </summary>
		//public string NodeName { get; private set; }


		///// <summary>
		///// Имя страницы
		///// </summary>
		//public string PageName { get; private set; }


		///// <summary>
		///// Ссылочный путь страницы
		///// </summary>
		//public string PagePath { get; private set; }


		///// <summary>
		///// Ресурсный путь страницы
		///// </summary>
		//public string PageResources { get; private set; }


		///// <summary>
		///// Полный путь запроса
		///// </summary>
		//public string QueryPath { get; private set; }


		///// <summary>
		///// Путь представления
		///// </summary>
		//public string ViewPath { get; private set; }


		/* privates */


		private string _getActionUID()
		{
			var sb1 = new StringBuilder();
			if (AreaName != null)
				sb1.Append($"/{AreaName}");
			if (RazorPage != null)
				sb1.Append($"{RazorPage}");
			if (ControllerName != null)
				sb1.Append($"/{ControllerName}");
			if (ActionName != null)
				sb1.Append($"/{ActionName}");
			return sb1.ToString().ToLower();
		}

	}

}
