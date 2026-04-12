using Ans.Net10.Common;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace Ans.Net10.Web
{

	public class TagBuilderExt
		: TagBuilder
	{

		/* ctor */


		public TagBuilderExt(
			string tagName,
			TagRenderMode mode)
			: base(tagName)
		{
			TagRenderMode = mode;
		}


		/* methods */


		public void Apply(
			TagClassesBuilder classes,
			TagStylesBuilder styles,
			TagAttributesBuilder attributes)
		{
			if (classes.Items.Count > 0)
				this.ExpandClassAttribute(classes.ToString());
			if (styles.Items.Count > 0)
				this.ExpandStyleAttribute(classes.ToString());
			foreach (var item1 in attributes.Items)
				MergeAttribute(item1.Key, item1.Value);
		}


		public void Apply(
			string classes,
			string styles,
			string attributes)
		{
			var classes1 = new TagClassesBuilder(classes);
			var styles1 = new TagStylesBuilder(styles);
			var attributes1 = new TagAttributesBuilder(attributes);
			Apply(classes1, styles1, attributes1);
		}


		/* functions */


		public override string ToString()
		{
			var sw1 = new StringWriter();
			WriteTo(sw1, HtmlEncoder.Default);
			return sw1.ToString();
		}


		public HtmlString ToHtml()
		{
			return new HtmlString(ToString());
		}

	}

}
