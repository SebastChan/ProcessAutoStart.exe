using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProcessAutoStart.Classes
{
	internal static class Settings
	{
		public static Config.Info GetConfig()
		{
			Config.Info info;
			if (!File.Exists(EndPoint.ConfigFile))
			{
				Config.Info info1 = new Config.Info()
				{
					FilesToOpen = new List<string>()
				};
				File.WriteAllText(EndPoint.ConfigFile, JsonConvert.SerializeObject(info1, 1));
				return info1;
			}
			try
			{
				info = JsonConvert.DeserializeObject<Config.Info>(File.ReadAllText(EndPoint.ConfigFile));
			}
			catch (Exception exception)
			{
				Console.WriteLine(string.Concat("Failed while reading config!\n\n", exception.Message));
				return null;
			}
			return info;
		}
	}
}