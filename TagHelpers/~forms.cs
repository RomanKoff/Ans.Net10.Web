using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Ans.Net10.Web.TagHelpers
{

	/*
	 * <input-hidden for="@Model.Field" />
	 */



	public class InputHiddenTagHelper
		: _TagHelper_Base
	{

		private const string For_AttributeName = "for";


		/* properties */


		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; }


		[HtmlAttributeName(For_AttributeName)]
		public ModelExpression For { get; set; }


		/* methods */


		public override void Process(
			TagHelperContext context,
			TagHelperOutput output)
		{
			output.TagMode = TagMode.SelfClosing;
			output.TagName = "input";
						
			if (For.Metadata == null)
				throw new InvalidOperationException(
					$"[Ans.Net10.Web] <ans-input-hidden /> 'for' attribute require");

			output.Attributes.Add("type", "hidden");

			var name1 = For.Name;
			output.Attributes.Add("id", name1);
			output.Attributes.Add("name", name1);

			var value1 = For.GetModelValueString();
			output.AddAttributeIfPresent("value", value1);

			base.Process(context, output);
		}

	}

}
