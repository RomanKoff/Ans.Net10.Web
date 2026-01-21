namespace Ans.Net10.Web.Forms
{

	public class Edit_Double
		: _Edit_Text_Base,
		IFormEditControl
	{

		public Edit_Double(
			string name,
			double? value,
			string cssClasses = null)
			: base(name, value.ToString(), cssClasses, _Consts.MW_Double)
		{
		}

	}

}
