using Ans.Net10.Common;

namespace Ans.Net10.Web.Forms
{

	public class Edit__Select
		: _Edit_Registry_Base,
		IFormEditControl
	{

		public Edit__Select(
			string name,
			int? value,
			RegistryList registry,
			string cssClasses = null)
			: base(
				  name,
				  value?.ToString(),
				  registry,
				  RegistryModeEnum.Select,
				  cssClasses,
				  false)
		{
		}

	}

}
