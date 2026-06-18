using Ans.Net10.Common;
using System.Diagnostics;

namespace Ans.Net10.Web.Services
{

	public class MapPagesItem
		: _TreeItem_Base
	{

		/* ctors */


		public MapPagesItem()
		{
		}


		public MapPagesItem(
			string name,
			string title,
			string shortTitle)
			: this()
		{
			Name = name;
			Title = title;
			ShortTitle = shortTitle;

			Debug.WriteLine($"[Ans.Net10.Web] ctor PageItem(\"{name}\", \"{title}\", \"{shortTitle}\")");
		}


		public MapPagesItem(
			string name,
			string title,
			string shortTitle,
			params MapPagesItem[] pages)
			: this(name, title, shortTitle)
		{
			AppendChilds(pages);
		}


		/* properties */


		public string Name { get; set; }
		public string Title { get; set; }
		public string ShortTitle { get; set; }


		/* readonly properties */


		public string Path
			=> field ??= $"{Parent?.Path.Make("{0}/")}{Name}";


		public new MapPagesItem Parent
			=> (MapPagesItem)base.Parent;

	}

}
