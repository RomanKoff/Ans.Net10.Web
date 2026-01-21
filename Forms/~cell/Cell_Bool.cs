using Ans.Net10.Common;

namespace Ans.Net10.Web.Forms
{

	public class Cell_Bool
		: _Cell_Text_Base,
		IFormCellControl
	{

		public Cell_Bool(
			bool value)
			: base(
				  value.Make(
					  Resources.Common.Html_CellTrue,
					  Resources.Common.Html_CellFalse),
				  true)
		{
		}

	}

}
