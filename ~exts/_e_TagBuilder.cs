using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ans.Net10.Web
{

	public static partial class _e_TagBuilder
	{

		/* methods */


		public static void ExpandAttribute(
			this TagBuilder tag,
			string name,
			string value,
			string separator)
		{
			if (tag.Attributes.TryGetValue(name, out var current1))
				tag.Attributes[name] = current1 + separator + value;
			else
				tag.Attributes[name] = value;
		}


		public static void ExpandClassAttribute(
			this TagBuilder tag,
			string value)
		{
			tag.ExpandAttribute("class", value, " ");
		}


		public static void ExpandStyleAttribute(
			this TagBuilder tag,
			string value)
		{
			tag.ExpandAttribute("style", value, ";");
		}

	}

}
