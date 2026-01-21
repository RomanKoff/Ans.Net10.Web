using Ans.Net10.Common;
using Ans.Net10.Common.Resources;
using Microsoft.AspNetCore.Html;
using System.Resources;

namespace Ans.Net10.Web.Forms
{

	public class FormResources(
		params ResourceManager[] resources)
		: ResourcesHelper(resources)
	{

		/* readonly properties */


		public string TitlePluralize
			=> field ??= GetCalcFaceHelper("_TitlePluralize").Title;


		public HtmlString TitlePluralize_Html
			=> field ??= TitlePluralize.ToHtml(true);


		public string TitleWhoWhat
			=> field ??= GetCalcFaceHelper("_TitleWhoWhat").Title;


		public HtmlString TitleWhoWhat_Html
			=> field ??= TitleWhoWhat.ToHtml(true);


		public string ListPageTitle
			=> string.Format(Form.Template_PageTitle_List, TitlePluralize);


		public string AddPageTitle
			=> string.Format(Form.Template_PageTitle_Add, TitleWhoWhat);


		public string EditPageTitle
			=> string.Format(Form.Template_PageTitle_Edit, TitleWhoWhat);


		public string DetailPageTitle
			=> string.Format(Form.Template_PageTitle_Details, TitleWhoWhat);


		public string DeletePageTitle
			=> string.Format(Form.Template_PageTitle_Delete, TitleWhoWhat);


		public HtmlString Text_EmptyItems_Html
			=> field ??= Form.Text_EmptyItems.ToHtml(true);


		public HtmlString Text_Cancel_Html
			=> field ??= Form.Text_Cancel.ToHtml(true);


		public HtmlString Text_Add_Html
			=> field ??= Form.Text_Add.ToHtml(true);


		public HtmlString Text_Save_Html
			=> field ??= Form.Text_Save.ToHtml(true);


		public HtmlString Text_SubmitAdd_Html
			=> field ??= Form.Text_SubmitAdd.ToHtml(true);


		public HtmlString Text_SubmitSave_Html
			=> field ??= Form.Text_SubmitSave.ToHtml(true);


		public HtmlString Text_SubmitDelete_Html
			=> field ??= Form.Text_SubmitDelete.ToHtml(true);


		public HtmlString Title_Edit_Html
			=> field ??= Form.Title_Edit.ToHtml(true);


		public HtmlString Title_Detail_Html
			=> field ??= Form.Title_Detail.ToHtml(true);


		public HtmlString Title_Delete_Html
			=> field ??= Form.Title_Delete.ToHtml(true);

	}

}
