using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Ans.Net10.Web.TagHelpers
{

	/*
	 * <site-container>...</site-container>
	 * <node-container>...</node-container>
	 * <page-container>...</page-container>	 
	 * <admin-container free="false">...</admin-container>
	 */



	[HtmlTargetElement("site-container")]
	public class SiteContainerTagHelper(
		CurrentContext current)
		: _AnsTagHelper_Base(current)
	{
		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			UseAutoContent = true;
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = "div";
			ClassBefore = Current.Site.ContainerClasses;
			base.Process(context, output);
		}
	}



	[HtmlTargetElement("node-container")]
	public class NodeContainerTagHelper(
		CurrentContext current)
		: _AnsTagHelper_Base(current)
	{
		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			UseAutoContent = true;
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = "div";
			ClassBefore = Current.Node.ContainerClasses;
			base.Process(context, output);
		}
	}



	[HtmlTargetElement("page-container")]
	public class PageContainerTagHelper(
		CurrentContext current)
		: _AnsTagHelper_Base(current)
	{
		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			UseAutoContent = true;
			output.TagMode = TagMode.StartTagAndEndTag;
			output.TagName = "div";
			ClassBefore = Current.Page.ContainerClasses;
			base.Process(context, output);
		}
	}



	[HtmlTargetElement("admin-container")]
	public class AdminContainerTagHelper(
		CurrentContext current)
		: _AnsTagHelper_Base(current)
	{

		private const string Free_AttributeName = "free";


		/* properties */


		[HtmlAttributeName(Free_AttributeName)]
		public bool IsFree { get; set; } = false;


		/* methods */


		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			UseAutoContent = true;
			output.TagName = null;
			if (!Current.Network.IsAdmin)
			{
				output.Content.Clear();
				return;
			}
			if (!IsFree)
			{
				output.TagMode = TagMode.StartTagAndEndTag;
				output.TagName = "div";
				Class = "bg-warning-subtle";
			}
			base.Process(context, output);
		}
	}

}
