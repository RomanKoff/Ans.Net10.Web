using Ans.Net10.Common;
using Microsoft.Extensions.Caching.Memory;

namespace Ans.Net10.Web
{

	public class WebGridService(
		CurrentContext current)
	{

		private readonly CurrentContext _current = current;


		/* functions */


		public WebGridCachedHelper<T> GetHelper<T>(
			Func<string, T> parser,
			string resUrl,
			string cacheKey,
			MemoryCacheEntryOptions cacheOptions)
		{
			var url1 = _current.GetResUrl(resUrl);
			return new(
				_current.HttpClientFactory.CreateClient(),
				_current.MemoryCache,
				parser,
				url1,
				cacheKey ?? url1,
				cacheOptions);
		}


		public IEnumerable<T> Get<T>(
			Func<string, T> parser,
			string resUrl,
			int slidingExpirationSeconds = 0,
			int absoluteExpirationRelativeToNowSeconds = 10)
		{
			var helper1 = GetHelper<T>(
				parser,
				resUrl,
				null,
				SuppCache.GetOptions(
					slidingExpirationSeconds,
					absoluteExpirationRelativeToNowSeconds));
			return helper1.SendQuery().Grid;
		}


		public IEnumerable<T> GetFromEndpoint<T>(
			Func<string, T> parser,
			string endpointName,
			int slidingExpirationSeconds = 0,
			int absoluteExpirationRelativeToNowSeconds = 10)
		{
			var url1 = _current.GetEndpoint(endpointName);
			return Get(parser, url1, slidingExpirationSeconds, absoluteExpirationRelativeToNowSeconds);
		}

	}

}
