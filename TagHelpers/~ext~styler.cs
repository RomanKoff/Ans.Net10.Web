using Ans.Net10.Common;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Ans.Net10.Web.TagHelpers
{

	/*
	 * <div ans-styler="">...</div>
	 * <img ans-styler="" />
	 */



	[HtmlTargetElement("div", Attributes = AnsStyler_AttributeName)]
	[HtmlTargetElement("img", Attributes = AnsStyler_AttributeName)]
	public partial class Exts_AnyTagHelper
		: TagHelper
	{

		private const string AnsStyler_AttributeName = "ans-styler";


		/* properties */


		[HtmlAttributeName(AnsStyler_AttributeName)]
		public TagStyler StylerData { get; set; }

		public string Class { get; set; }
		public string Style { get; set; }


		/* methods */


		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			base.Process(context, output);
			StylerData ??= new();
			StylerData.Classes.ApplyOriginal(Class);
			if (StylerData.Classes.Items.Count > 0)
				output.Attributes.SetAttribute("class", StylerData.Classes.ToString());
			StylerData.Styles.ApplyOriginal(Style);
			if (StylerData.Styles.Items.Count > 0)
				output.Attributes.SetAttribute("style", StylerData.Styles.ToString());
			var content1 = output.GetChildContent();
			output.Content.AppendHtml(content1);
		}

	}

}
