using Ans.Net10.Common;
using System.Diagnostics;

namespace Ans.Net10.Web.Services
{

	public class MapNodesItem
		: _TreeItem_Base
	{

		/* ctors */


		public MapNodesItem()
		{
		}


		public MapNodesItem(
			string name,
			string title,
			string shortTitle)
			: this()
		{
			Name = name;
			Title = title;
			ShortTitle = shortTitle;

			Debug.WriteLine($"[Ans.Net10.Web] ctor NodeItem(\"{name}\", \"{title}\", \"{shortTitle}\")");
		}


		public MapNodesItem(
			string name,
			string title,
			string shortTitle,
			params MapPagesItem[] nodes)
			: this(name, title, shortTitle)
		{
			AppendChilds(nodes);
		}


		/* properties */


		public string Name { get; set; }
		public string Title { get; set; }
		public string ShortTitle { get; set; }


		/* readonly properties */


		public MapPagesItem TreePages { get; } = new();

	}

}
