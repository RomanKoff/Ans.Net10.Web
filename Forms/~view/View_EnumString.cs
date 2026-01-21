using Ans.Net10.Common;

namespace Ans.Net10.Web.Forms
{

	public class View_EnumString
		: _View_Reference_Base,
		IFormViewControl
	{

		public View_EnumString(
			string name,
			string value,
			RegistryList registry)
			: base(
				  name,
				  value,
				  registry)
		{
		}

	}

}
