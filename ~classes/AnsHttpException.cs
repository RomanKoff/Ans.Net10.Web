using System.Net;

namespace Ans.Net10.Web
{

	public class AnsHttpException(
		HttpStatusCode statusCode)
		: Exception
	{
		public HttpStatusCode StatusCode { get; set; } = statusCode;
	}

}
