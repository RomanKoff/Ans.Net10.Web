using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Ans.Net10.Web.TagHelpers
{

	/*
	 * <img src-site-res="images/logo1.svg"/>
	 * <img src-node-res="persons/head.png"/>
	 * <img src-page-res="img1.jpg"/>
	 */



	[HtmlTargetElement("img", Attributes = Src_Site_Res_AttributeName)]
	[HtmlTargetElement("img", Attributes = Src_Node_Res_AttributeName)]
	[HtmlTargetElement("img", Attributes = Src_Page_Res_AttributeName)]
	public partial class Exts_ImgTagHelper(
		CurrentContext current)
		: _AnsTagHelper_Base(current)
	{

		private const string Src_Site_Res_AttributeName = "src-site-res";
		private const string Src_Node_Res_AttributeName = "src-node-res";
		private const string Src_Page_Res_AttributeName = "src-page-res";

		private readonly CurrentContext _current = current;


		/* properties */


		[HtmlAttributeName(Src_Site_Res_AttributeName)]
		public string SrcSiteResData { get; set; }


		[HtmlAttributeName(Src_Node_Res_AttributeName)]
		public string SrcNodeResData { get; set; }


		[HtmlAttributeName(Src_Page_Res_AttributeName)]
		public string SrcPageResData { get; set; }


		/* methods */


		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			base.Process(context, output);
			output.TagName = "img";
			output.TagMode = TagMode.SelfClosing;

			if (!string.IsNullOrEmpty(SrcSiteResData))
				_makeSrc(output, _current.GetResUrl($"site:{SrcSiteResData}"));
			else if (!string.IsNullOrEmpty(SrcNodeResData))
				_makeSrc(output, _current.GetResUrl($"node:{SrcNodeResData}"));
			else if (!string.IsNullOrEmpty(SrcPageResData))
				_makeSrc(output, _current.GetResUrl($"page:{SrcPageResData}"));
		}


		/* privates */


		private static void _makeSrc(
			TagHelperOutput output,
			string url1)
		{
			var src1 = new HtmlString(url1);
			output.Attributes.SetAttribute("src", src1);
		}

	}

}
