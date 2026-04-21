namespace Ans.Net10.Web
{

	public class AnsPageModel(
		CurrentContext current)
		: _AnsPageModel_Base(current)
	{

		public virtual void OnGet()
		{
			var path1 = Current.Request.RazorPage;
			_ = Current.Request.NodesParsePath(path1);
			//Debug.WriteLine($"{path1}");
		}

	}

}
