using Ans.Net10.Common;
using Microsoft.Extensions.Caching.Memory;

namespace Ans.Net10.Web
{

	public class CacheService(
		CurrentContext current)
	{

		private readonly CurrentContext _current = current;
		private readonly MemoryCacheHelper _helper = new(current.MemoryCache);


		/* functions */


		public T Get<T>(
			string cacheKey,
			Func<T> getObject,
			MemoryCacheEntryOptions options = null)
		{
			return _helper.Get<T>(cacheKey, getObject, options);
		}


		/* methods */


		public void Remove(
			string cacheKey)
		{
			_helper.Remove(cacheKey);
		}

	}

}
