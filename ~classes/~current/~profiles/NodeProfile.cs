using Ans.Net10.Common;

namespace Ans.Net10.Web
{

	public class NodeProfile(
		CurrentContext current)
		: _CurrentProfile_Proto(current)
	{

		/* properties */


		public MapNodesItem NodeItem { get; set; }


		public override string ContainerClasses
		{
			get => field ?? _Current.Site.ContainerClasses;
			set => field = value;
		}


		public override string Title
		{
			get => base.Title ?? NodeItem?.Title;
			set => base.Title = value;
		}


		public override string ShortTitle
		{
			get => IsShortTitleUnique
				? base.ShortTitle
				: NodeItem?.ShortTitle;
			set => base.ShortTitle = value;
		}


		public string ResPath
		{
			get => field ??= _Current.Request.NodeName;
			set
			{
				field = value;
				_resUrl = null;
			}
		}


		private string _resUrl;
		public override string ResUrl
		{
			get => _resUrl ??= $"{_Current.Site.ResUrl}{ResPath.Make("/{0}")}";
			set => _resUrl = value;
		}


		/* readonly properties */


		public override string Url
			=> $"{_Current.Site.Url}{NodeItem?.Target?.Make("/{0}")}";


		public MapPages MapPages
		{
			get => field ?? _Current.Maps.GetMapPages(
				NodeItem?.Target); //?? _Current.Request.NodeName);
			set => field = value;
		}


		public bool HasPages
			=> MapPages?.HasItems ?? false;


		public bool HasSlaves
			=> NodeItem?.HasSlaves ?? false;


		public bool HasParentNode
			=> ParentNode != null;


		public MapNodesItem ParentNode
			=> field ??= _getParentNode();


		/* methods */


		public void SetPages(
			string path,
			params MapPagesItem[] pages)
		{
			MapPages = new(pages, _Current.Host.VirtualPath, path);
		}


		/* privates */


		private MapNodesItem _getParentNode()
		{
			if (!NodeItem.HasMasters)
				return null;
			foreach (var item1 in NodeItem.Masters.Reverse())
				if (item1.Type != MapItemTypeEnum.Group)
					return (MapNodesItem)item1;
			return null;
		}

	}

}
