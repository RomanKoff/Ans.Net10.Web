using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Ans.Net10.Web
{

	public static partial class _e_ISession
	{

		/* methods */


		public static void Set<T>(
			this ISession session,
			string key,
			T value)
		{
			session.SetString(key, JsonSerializer.Serialize(value));
		}


		/* functions */


		public static T Get<T>(
			this ISession session,
			string key)
		{
			var value = session.GetString(key);
			return value == null
				? default
				: JsonSerializer.Deserialize<T>(value);
		}

	}

}
