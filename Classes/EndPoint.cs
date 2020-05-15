using System;
using System.Runtime.CompilerServices;

namespace ProcessAutoStart.Classes
{
	internal class EndPoint
	{
		public static string ConfigFile
		{
			get;
			set;
		}

		static EndPoint()
		{
			EndPoint.ConfigFile = "config.json";
		}

		public EndPoint()
		{
		}
	}
}