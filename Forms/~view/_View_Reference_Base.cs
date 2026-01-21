using Ans.Net10.Common;

namespace Ans.Net10.Web.Forms
{

	public class _View_Reference_Base
		: _View_Text_Base,
		IFormViewControl
	{

		public _View_Reference_Base(
			string name,
			string value,
			RegistryList registry)
			: base(
				  name,
				  registry.GetValueOrKey(value),
				  value,
				  registry.GetMaxWidth(),
				  true)
		{
		}

	}

}
