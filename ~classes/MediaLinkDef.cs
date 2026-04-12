using Ans.Net10.Common;

namespace Ans.Net10.Web
{

	public enum MediaLinkDefTypeEnum
	{
		Icon = 0,
		Image = 1,
		Label = 2,
	}



	public class MediaLink
	{
		public string UrlPart { get; set; }
		public MediaLinkDefTypeEnum Type { get; set; }
		public string Inner { get; set; }

		public string GetVariant(int variant = 0)
		{
			var a1 = Inner.Split('|');
			if (variant == 0 || a1.Length <= variant)
				return a1[0];
			var s1 = a1[0].GetCropLeft('/');
			return $"{s1}/{a1[variant]}";
		}
	}



	public class MediaLinkDef
	{
		/// <param name="def">
		/// "icon-class",
		/// "img=img-url|variant1|variant2...",
		/// "label=text"
		/// </param>
		public MediaLinkDef(
			string def)
		{
			if (def.StartsWith("img="))
			{
				Type = MediaLinkDefTypeEnum.Image;
				Inner = def[4..];
			}
			else if (def.StartsWith("label="))
			{
				Type = MediaLinkDefTypeEnum.Label;
				Inner = def[6..];
			}
			else
			{
				Type = MediaLinkDefTypeEnum.Icon;
				Inner = def;
			}
		}

		public MediaLinkDefTypeEnum Type { get; }
		public string Inner { get; }
	}

}
