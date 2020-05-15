using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace ProcessAutoStart.Classes
{
	internal class Config
	{
		public Config()
		{
		}

		public class Info
		{
			public List<string> FilesToOpen
			{
				get;
				set;
			}

			public string ProcessForScan
			{
				get;
				set;
			}

			public Info()
			{
			}
		}
	}
}