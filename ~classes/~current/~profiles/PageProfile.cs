using Ans.Net10.Common;

namespace Ans.Net10.Web
{

	public class PageProfile(
		CurrentContext current)
		: _CurrentProfile_Proto(current)
	{

		/* overrides */


		public override string ContainerClasses
		{
			get => field ?? _Current.Node.ContainerClasses;
			set;
		}


		public override string Url
			=> "! TODO !"; // $"{_Current.Node.Url}{PagemapItem?.Path?.Make("/{0}")}";


		private string _resUrl;
		public override string ResUrl
		{
			get => _resUrl ??= $"{_Current.Node.ResUrl}{ResPath.Make("/{0}")}";
			set => _resUrl = value;
		}


		/* properties */


		public string ResPath
		{
			get => field ??= "! TODO !"; // _Current.Request.PagePath;
			set
			{
				field = value;
				_resUrl = null;
			}
		}


		public string CustomBrowserTitle { get; set; }

	}

}
