namespace Ans.Net10.Web
{

	public class MvcAccessHelper
	{

		/* ctor */


		/// <param name="data">
		/// Catalog1;Catalog2;Catalog3
		/// </param>
		public MvcAccessHelper(
			string data)
		{
			if (string.IsNullOrEmpty(data))
				return;
			Catalogs = data.Split(';', StringSplitOptions.RemoveEmptyEntries)
				.Select(x => new MvcAccessCatalog(x.Trim()));
		}


		/* readonly properties */


		public IEnumerable<MvcAccessCatalog> Catalogs { get; }


		/* functions */


		public string[] GetResultClaimsValue()
		{
			var values1 = new List<string>();
			foreach (var catalor1 in Catalogs)
				if (catalor1.Controllers?.Count() > 0)
					foreach (var controller1 in catalor1.Controllers)
					{
						var s1 = $"{catalor1.Name}.{controller1.Name}";
						if (controller1.Actions?.Count() > 0)
							foreach (var action1 in controller1.Actions)
								values1.Add($"{s1}.{action1}");
						else
							values1.Add(s1);
					}
				else
					values1.Add(catalor1.Name);
			return [.. values1];
		}

	}



	public class MvcAccessCatalog
	{
		/// <param name="data">
		/// Name>Controller1,Controller2,Controller3
		/// </param>
		public MvcAccessCatalog(
			string data)
		{
			var a1 = data.Split('>', StringSplitOptions.RemoveEmptyEntries);
			Name = a1[0].Trim();
			if (a1.Length > 1)
				Controllers = a1[1].Split(',')
					.Select(x => new MvcAccessController(x.Trim()));
		}

		public string Name { get; }
		public IEnumerable<MvcAccessController> Controllers { get; }
	}



	public class MvcAccessController
	{
		/// <param name="data">
		/// Name=Action1+Action2+Action3
		/// </param>
		public MvcAccessController(
			string data)
		{
			var a1 = data.Split('=', StringSplitOptions.RemoveEmptyEntries);
			Name = a1[0].Trim();
			if (a1.Length > 1)
				Actions = a1[1].Split('+')
					.Select(x => x.Trim());
		}

		public string Name { get; }
		public IEnumerable<string> Actions { get; }
	}

}
