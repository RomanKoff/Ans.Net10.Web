using Ans.Net10.Common;

namespace Ans.Net10.Web.Forms
{

	public class Edit_Reference
		: _Edit_Registry_Base,
		IFormEditControl
	{

		public Edit_Reference(
			string name,
			int? value,
			RegistryList registry = null,
			RegistryModeEnum registryMode = RegistryModeEnum.Auto,
			string cssClasses = null)
			: base(
				  name,
				  value?.ToString(),
				  registry,
				  registryMode,
				  cssClasses,
				  false)
		{
		}


		//public Edit_Reference(
		//	string name,
		//	int? value,
		//	RegistryList registry,
		//	string cssClasses = null)
		//	: this(
		//		  name,
		//		  value,
		//		  registry,
		//		  RegistryModeEnum.Auto,
		//		  cssClasses)
		//{
		//}

	}

}
