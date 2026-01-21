using Ans.Net10.Common;

namespace Ans.Net10.Web.Forms
{

	public class Cell_EnumString
		: _Cell_Text_Base,
		IFormCellControl
	{

		public Cell_EnumString(
			string value,
			RegistryList registry)
			: base(registry.GetValueOrKey(value), true)
		{
		}

	}

}
