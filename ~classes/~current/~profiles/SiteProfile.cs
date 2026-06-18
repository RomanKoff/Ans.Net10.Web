namespace Ans.Net10.Web
{

	public class SiteProfile(
		CurrentContext current)
		: _CurrentProfile_Proto(current)
	{

		/* overrides */


		public override string ContainerClasses
		{
			get => field ??= _Current.Options.DefaultContainerClasses ?? "container";
			set;
		}


		public override string Url
			=> _Current.Host.ApplicationUrl;


		public override string ResUrl
		{
			get => field ??= $"{Url}/content";
			set;
		}

	}

}
