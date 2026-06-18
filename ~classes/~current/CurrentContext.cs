using Ans.Net10.Common;
using Ans.Net10.Common.Services;
using Ans.Net10.Web.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
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
			IWebHostEnvironment env,
			IConfiguration configuration,
			IHttpContextAccessor httpContextAccessor,
			IMemoryCache memoryCache,
			IViewRenderService viewRender,
			IHttpClientFactory httpClientFactory,
			SitemapProvider sitemap,
			IMailerService mailer,
			LinkGenerator linkGenerator)
		{
			Env = env;
			Configuration = configuration;
			HttpContext = httpContextAccessor.HttpContext;
			MemoryCache = memoryCache;
			ViewRender = viewRender;
			HttpClientFactory = httpClientFactory;
			Sitemap = sitemap;
			Mailer = mailer;
			LinkGenerator = linkGenerator;

			Options = Configuration.GetLibWebOptions();
			Culture = CultureInfo.CurrentCulture;
			DateTimeHelper = new();

			Host = new(this);
			Request = new(this);
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


		public IWebHostEnvironment Env { get; }
		public IConfiguration Configuration { get; }
		public HttpContext HttpContext { get; }
		public IMemoryCache MemoryCache { get; }
		public IViewRenderService ViewRender { get; }
		public IHttpClientFactory HttpClientFactory { get; }
		public SitemapProvider Sitemap { get; }
		public IMailerService Mailer { get; }
		public LinkGenerator LinkGenerator { get; }

		public LibWebOptions Options { get; }
		public CultureInfo Culture { get; }
		public DateTimeHelper DateTimeHelper { get; }

		public HostData Host { get; }
		public RequestData Request { get; }
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

		public bool IsDevelopment
			=> Env.IsDevelopment();

		public string[] BrowserTitleItems
			=> field ??= _getBrowserTitleItems();

		public bool HasBrowserTitleItems
			=> BrowserTitleItems?.Length > 0;


		/* properties */


		public string DefaultLayout
		{
			get => field ??= Options.DefaultLayout;
			set;
		}


		public string SystemLayout
		{
			get => field ??= (Options.SystemLayout ?? DefaultLayout);
			set;
		}


		public string ErrorsLayout
		{
			get => field ??= (Options.Errors.Layout ?? SystemLayout);
			set;
		}


		/* methods */


		[System.Diagnostics.CodeAnalysis.SuppressMessage(
			"Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
		public void ThrowNotFound()
		{
			throw new AnsHttpException(HttpStatusCode.NotFound);
		}


		[System.Diagnostics.CodeAnalysis.SuppressMessage(
			"Performance", "CA1822:Mark members as static", Justification = "<Pending>")]
		public void ThrowForbidden()
		{
			throw new AnsHttpException(HttpStatusCode.Forbidden);
		}


		/* functions */


		/// <summary>
		/// Возвращает ссылку на страницу:
		/// "{абсолютный url до внешнего ресурса}",
		/// "/{путь внутри сайта}",
		/// "site:{путь внутри сайта}",
		/// "node:{путь от текущего узла}",
		/// "page:{путь от текущей страницы}"
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
		/// "{абсолютный url до внешнего ресурса}",
		/// "/{путь от каталога /content/}",
		/// "site:{путь от каталога /content/}",
		/// "node:{путь от каталога /content/{текущий_узел}/}",
		/// "page:{путь от каталога /content/{текущий_узел}/{путь_текущей_страницы}/}"
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


		/* privates */


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
