using Ans.Net10.Common;

namespace Ans.Net10.Web
{

	public class NodeProfile(
		CurrentContext current)
		: _CurrentProfile_Proto(current)
	{

		/* overrides */


		public override string ContainerClasses
		{
			get => field ?? _Current.Site.ContainerClasses;
			set;
		}


		public override string Url
			=> "! TODO !"; // $"{_Current.Site.Url}{NodemapItem?.Path?.Make("/{0}")}";


		private string _resUrl;
		public override string ResUrl
		{
			get => _resUrl ??= $"{_Current.Site.ResUrl}{ResPath.Make("/{0}")}";
			set => _resUrl = value;
		}


		/* properties */


		public string ResPath
		{
			get => field ??= "! TODO !"; // _Current.Request.NodeName;
			set
			{
				field = value;
				_resUrl = null;
			}
		}

	}

}
