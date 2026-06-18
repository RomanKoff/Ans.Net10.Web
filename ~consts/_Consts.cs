using Microsoft.AspNetCore.Mvc;

namespace Ans.Net10.Web
{

	public static partial class _Consts
	{

		public const string OPTION_TABS = "&nbsp;&nbsp;&nbsp;";


		public static readonly Dictionary<string, CacheProfile> CACHE_PROFILES = new()
		{
			{
				"NONE",
				new() {
					Duration = 1,
					Location = ResponseCacheLocation.None,
					NoStore = true,
					VaryByQueryKeys = ["*"]
				}
			},
			{
				"D10",
				new() {
					Duration = 10,
					Location = ResponseCacheLocation.Any,
					NoStore = false,
					VaryByQueryKeys = ["*"]
				}
			},
			{
				"D30",
				new() {
					Duration = 30,
					Location = ResponseCacheLocation.Any,
					NoStore = false,
					VaryByQueryKeys = ["*"]
				}
			},
			{
				"D60",
				new() {
					Duration = 60,
					Location = ResponseCacheLocation.Any,
					NoStore = false,
					VaryByQueryKeys = ["*"]
				}
			}
		};


		public const string CORS_ALLOW_ALL = "ALLOW ALL";

	}

}
