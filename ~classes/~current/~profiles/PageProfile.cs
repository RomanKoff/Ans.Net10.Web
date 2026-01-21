using Ans.Net10.Common;

namespace Ans.Net10.Web
{

	public class PageProfile(
		CurrentContext current)
		: _CurrentProfile_Proto(current)
	{

		/* properties */


		public MapPagesItem PageItem { get; set; }


		public override string ContainerClasses
		{
			get => field ?? _Current.Node.ContainerClasses;
			set => field = value;
		}


		public override string Title
		{
			get => base.Title ?? PageItem?.Title;
			set => base.Title = value;
		}


		public override string ShortTitle
		{
			get => IsShortTitleUnique
				? base.ShortTitle
				: base.Title ?? PageItem?.ShortTitle;
			set => base.ShortTitle = value;
		}


		public string CustomBrowserTitle { get; set; }


		public string ResPath
		{
			get => field ??= _Current.Request.PagePath;
			set
			{
				field = value;
				_resUrl = null;
			}
		}


		private string _resUrl;
		public override string ResUrl
		{
			get => _resUrl ??= $"{_Current.Node.ResUrl}{ResPath.Make("/{0}")}";
			set => _resUrl = value;
		}


		/* readonly properties */


		public override string Url
			=> $"{_Current.Node.Url}{_Current.Request.PagePath.Make("/{0}")}";


		public string ParentsTitles
			=> field ??= _getParentsTitles();


		public bool HasSlaves
			=> PageItem?.HasSlaves ?? false;


		/* privates */


		private string _getParentsTitles()
		{
			return ParentsLinks?.MakeFromCollection(
				x => x.InnerHtml, null, null, ". ")
				.GetTypografMin();
		}

	}

}
