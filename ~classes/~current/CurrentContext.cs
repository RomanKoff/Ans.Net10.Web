using Ans.Net10.Common;
using Ans.Net10.Common.Services;
using Ans.Net10.Web.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Net;

namespace Ans.Net10.Web
{

	/*
	 *	LibStartup.Add_AnsNet10Web()
	 *		builder.Services.AddScoped<CurrentContext>();
	 */



	public partial class CurrentContext
	{

		/* ctor */


		public CurrentContext(
			IConfiguration configuration,
			IHttpContextAccessor httpContextAccessor,
			IHttpClientFactory httpClientFactory,
			IMemoryCache memoryCache,
			LinkGenerator linkGenerator,
			IMailerService mailer,
			IViewRenderService viewRender,
			IMapNodesProvider mapNodesProvider,
			IMapPagesProvider mapPagesProvider)
		{
			Configuration = configuration;
			HttpContext = httpContextAccessor.HttpContext;
			HttpClientFactory = httpClientFactory;
			MemoryCache = memoryCache;
			LinkGenerator = linkGenerator;
			Mailer = mailer;
			ViewRender = viewRender;

			Options = Configuration.GetLibWebOptions();
			Culture = CultureInfo.CurrentCulture;
			DateTimeHelper = new();

			App = new(this);
			Host = new(this);
			Request = new(this);
			Maps = new(this, mapNodesProvider, mapPagesProvider);
			Meta = new(this);

			Site = new(this);
			Node = new(this);
			Page = new(this);

			Cache = new(this);
			Cookies = new(this);
			Network = new(this);
			QueryString = new(this);
			Send = new(this);
			WebApi = new(this);
			WebGrid = new(this);
		}


		/* readonly properties */


		public IConfiguration Configuration { get; }
		public HttpContext HttpContext { get; }
		public IHttpClientFactory HttpClientFactory { get; }
		public IMemoryCache MemoryCache { get; }
		public LinkGenerator LinkGenerator { get; }
		public IMailerService Mailer { get; }
		public IViewRenderService ViewRender { get; }

		public LibWebOptions Options { get; }
		public CultureInfo Culture { get; }
		public DateTimeHelper DateTimeHelper { get; }

		public AppData App { get; }
		public HostData Host { get; }
		public RequestData Request { get; }
		public MapsData Maps { get; }
		public MetaData Meta { get; }

		public SiteProfile Site { get; }
		public NodeProfile Node { get; }
		public PageProfile Page { get; }

		public CacheService Cache { get; }
		public CookiesService Cookies { get; }
		public NetworkService Network { get; }
		public QueryStringService QueryString { get; }
		public SendService Send { get; }
		public WebApiService WebApi { get; }
		public WebGridService WebGrid { get; }


		public IEnumerable<LinkBuilder> Breadcrumbs
			=> field ??= _getBreadcrumbs();


		public string[] BrowserTitleItems
			=> field ??= _getBrowserTitleItems();


		public bool HasBreadcrumbs
			=> Breadcrumbs?.Count() > 0;


		public bool HasBrowserTitleItems
			=> BrowserTitleItems?.Length > 0;


		/* properties */


		public string DefaultLayout
		{
			get => field ??= Options.DefaultLayout;
			set { field = value; }
		}


		public string SystemLayout
		{
			get => field ??= (Options.SystemLayout ?? DefaultLayout);
			set { field = value; }
		}


		public string ErrorsLayout
		{
			get => field ??= (Options.Errors.Layout ?? SystemLayout);
			set { field = value; }
		}


		/* methods */


		[System.Diagnostics.CodeAnalysis.SuppressMessage(
			"Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
		public void ThrowNotFound()
			=> throw new AnsHttpException(HttpStatusCode.NotFound);


		[System.Diagnostics.CodeAnalysis.SuppressMessage(
			"Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
		public void ThrowForbidden()
			=> throw new AnsHttpException(HttpStatusCode.Forbidden);



		/* functions */


		/// <summary>
		/// Возвращает заголовок браузера
		/// </summary>
		public HtmlString BrowserTitle()
		{
			if (Page.CustomBrowserTitle != null)
				return Page.CustomBrowserTitle.ToHtml();
			return BrowserTitleItems
				.MakeFromCollection(null, null, " – ")
				.ToHtml();
		}


		/// <summary>
		/// Возвращает ссылку на страницу:
		/// "{absolute url}",
		/// "/{path from site}",
		/// "site:{path from site}",
		/// "node:{path from current node}",
		/// "page:{path from current page}"
		/// </summary>
		public string GetUrl(
			string target)
		{
			if (string.IsNullOrEmpty(target))
				return null;
			if (target[0] == '/')
				return $"{Site.Url}{target}";
			if (target.StartsWith("site:"))
				return $"{Site.Url}{target[5..].Make("/{0}")}";
			if (target.StartsWith("node:"))
				return $"{Node.Url}{target[5..].Make("/{0}")}";
			if (target.StartsWith("page:"))
				return $"{Page.Url}{target[5..].Make("/{0}")}";
			return target;
		}


		/// <summary>
		/// Возвращает ссылку на ресурс:
		/// "{absolute url}",
		/// "/{path from site content}",
		/// "site:{path from site content}",
		/// "node:{path from current node content}",
		/// "page:{path from current page content}"
		/// </summary>
		public string GetResUrl(
			string target)
		{
			if (string.IsNullOrEmpty(target))
				return null;
			if (target[0] == '/')
				return $"{Site.ResUrl}{target}";
			if (target.StartsWith("site:"))
				return $"{Site.ResUrl}{target[5..].Make("/{0}")}";
			if (target.StartsWith("node:"))
				return $"{Node.ResUrl}{target[5..].Make("/{0}")}";
			if (target.StartsWith("page:"))
				return $"{Page.ResUrl}{target[5..].Make("/{0}")}";
			return target;
		}


		/* privates */


		private IEnumerable<LinkBuilder> _getBreadcrumbs()
		{
			var items1 = new List<LinkBuilder>();
			items1.AddRange(Site.ParentsLinks);
			var url1 = Request.IsStartSite
				? null : SuppValues.Default(Site.Url, "/");
			items1.Add(new LinkBuilder(url1, Site.ShortTitle));
			items1.AddRange(Node.ParentsLinks);
			if (Node.NodeItem != null)
				items1.Add(Site.GetNodeLink(Node.NodeItem));
			items1.AddRange(Page.ParentsLinks);
			if (Page.PageItem != null)
				items1.Add(Page.PageItem.Link);
			return items1.AsEnumerable();
		}


		private string[] _getBrowserTitleItems()
		{
			var items1 = new List<string>();
			items1.AddRange(Site.ParentsLinks.Select(x => x.InnerHtml));
			items1.Add(Site.ShortTitle);
			items1.Add(Node.ShortTitle);
			items1.Add($"{Page.ParentsTitles.Make("{0}. ")}{Page.ShortTitle}");
			items1.Reverse();
			return [.. items1];
		}

	}

}
