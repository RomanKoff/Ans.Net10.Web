using Ans.Net10.Common;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Routing;

namespace Ans.Net10.Web
{

	public abstract class _CurrentProfile_Proto(
		CurrentContext current)
	{

		internal readonly CurrentContext _Current = current;


		/* abstracts */


		public abstract string ContainerClasses { get; set; }
		public abstract string Url { get; }
		public abstract string ResUrl { get; set; }


		/* properties */


		private string _title;
		public string Title
		{
			get => _title;
			set
			{
				_title = value;
				_titleHtml = null;
				_shortTitle = null;
				_shortTitleHtml = null;
			}
		}


		private string _shortTitle;
		public string ShortTitle
		{
			get => _shortTitle ?? Title;
			set
			{
				_shortTitle = value;
				_shortTitleHtml = null;
			}
		}


		/* readonly properties */


		private HtmlString _titleHtml;
		public HtmlString TitleHtml
			=> _titleHtml ??= Title.ToHtml(true);


		private HtmlString _shortTitleHtml;
		public HtmlString ShortTitleHtml
			=> _shortTitleHtml ??= ShortTitle.ToHtml(true);


		public bool IsShortTitleUnique
			=> _shortTitle != null && _shortTitle != _title;


		public List<LinkBuilder> ParentsLinks { get; } = [];


		public string ParentsTitles
			=> field ??= _getParentsTitles();


		public bool HasParents
			=> ParentsLinks.Count > 0;
		

		/* methods */


		public void AddParent(
			LinkBuilder link)
		{
			ParentsLinks.Add(link);
		}


		public void AddParent(
			string url,
			string title)
		{
			var link1 = _getLink(url, title);
			AddParent(link1);
		}


		public void AddParentFromAction(
			string action,
			string controller,
			object routes,
			string title)
		{
			var link1 = _getLink(action, controller, routes, title);
			AddParent(link1);
		}


		public void InsertParent(
			LinkBuilder link,
			int index = 0)
		{
			ParentsLinks.Insert(index, link);
		}


		public void InsertParent(
			string url,
			string title,
			int index = 0)
		{
			var link1 = _getLink(url, title);
			InsertParent(link1, index);
		}


		public void InsertParentFromAction(
			string action,
			string controller,
			object routes,
			string title,
			int index = 0)
		{
			var link1 = _getLink(action, controller, routes, title);
			InsertParent(link1, index);
		}


		public void RemoveParent(
			int index)
		{
			if (ParentsLinks?.Count > index)
				ParentsLinks.RemoveAt(index);
		}


		/* privates */


		private LinkBuilder _getLink(
			string url,
			string title)
		{
			var url1 = _Current.GetUrl(url);
			var link1 = new LinkBuilder(url1, title);
			return link1;
		}


		public LinkBuilder _getLink(
			string action,
			string controller,
			object routes,
			string title)
		{
			var url1 = _Current.LinkGenerator.GetPathByAction(action, controller, routes);
			var link1 = new LinkBuilder(url1, title);
			return link1;
		}


		private string _getParentsTitles()
		{
			return ParentsLinks?.MakeFromCollection(
				x => x.InnerHtml, null, null, ". ")
				.GetTypografMin();
		}

	}

}
