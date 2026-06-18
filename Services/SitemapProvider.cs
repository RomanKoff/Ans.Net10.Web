using Ans.Net10.Common;
using System.Diagnostics;

namespace Ans.Net10.Web.Services
{

	public class SitemapProvider
	{

		/* ctor */

		public SitemapProvider()
		{
			Debug.WriteLine("[Ans.Net10.Web] ctor SitemapProvider()");
		}


		/* readonly properties */


		public MapNodesItem TreeNodes { get; } = new();


		/* functions */


		public MapNodesItem GetNode(
			string name)
		{
			return _getNode(TreeNodes.Childs, name);
		}


		public (MapNodesItem, MapPagesItem) GetItem(
			string path)
		{
			// Path					Node		Page
			// -------------------------------------------------------------
			//						_main		start
			// path					path		start
			// 						_main		path
			// 						_main		path/start
			// path1/path2				path1		path2
			//						path1		path2/start
			//						_main		path1/path2
			//						_main		path1/path2/start

			MapNodesItem node1 = null;
			MapPagesItem page1 = null;
			if (string.IsNullOrEmpty(path))
				return (node1, page1);

			return (node1, page1);
		}


		/* privates */


		private static MapNodesItem _getNode(
			IEnumerable<ITreeItem> nodes,
			string name)
		{
			if (!(nodes?.Count() > 0))
				return null;
			foreach (var node1 in (IEnumerable<MapNodesItem>)nodes)
			{
				if (node1.Name.Equals(name))
					return node1;
				var found1 = _getNode(node1.Childs, name);
				if (found1 != null)
					return found1;
			}
			return null;
		}

	}

}
