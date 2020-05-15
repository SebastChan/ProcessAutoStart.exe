using ProcessAutoStart.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ProcessAutoStart
{
	internal class Program
	{
		private int WaitDelay = 2500;

		public Program()
		{
		}

		private void DoStuff()
		{
			Config.Info config = Settings.GetConfig();
			if (config == null)
			{
				Console.WriteLine("Config is null!");
				return;
			}
			if (!string.IsNullOrEmpty(config.ProcessForScan))
			{
				bool flag = false;
				Console.WriteLine(string.Concat("Scanning for Process \"", config.ProcessForScan, "\""));
				while (true)
				{
					if (!flag)
					{
						if (Process.GetProcessesByName(config.ProcessForScan).Length != 0)
						{
							flag = true;
						}
						Thread.Sleep(this.WaitDelay);
					}
					else
					{
						Console.WriteLine(string.Concat("Found Process \"", config.ProcessForScan, "\""));
						foreach (string filesToOpen in config.FilesToOpen)
						{
							string[] strArrays = filesToOpen.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries);
							string str = strArrays[(int)strArrays.Length - 1];
							string str1 = filesToOpen.Replace(str, string.Empty);
							try
							{
								(new Process()
								{
									StartInfo = new ProcessStartInfo()
									{
										FileName = "cmd.exe",
										WorkingDirectory = str1,
										Arguments = string.Concat("/c ", str),
										CreateNoWindow = true,
										UseShellExecute = false
									}
								}).Start();
								Thread.Sleep(1000);
							}
							catch (Exception exception)
							{
								Console.WriteLine(exception.Message);
							}
						}
						Console.WriteLine(string.Format("Tried to start all {0} File{1}", config.FilesToOpen.Count, (config.FilesToOpen.Count != 1 ? "s" : "")));
						while (flag)
						{
							if ((int)Process.GetProcessesByName(config.ProcessForScan).Length < 1)
							{
								flag = false;
							}
							Thread.Sleep(this.WaitDelay);
						}
						Console.WriteLine(string.Concat("Process \"", config.ProcessForScan, "\" closed..."));
					}
				}
			}
			Console.WriteLine("Please set ur config.json");
		}

		private static void Main()
		{
			Program program = new Program();
			Thread thread = new Thread(() => program.DoStuff());
			thread.Start();
			string str = Console.ReadLine();
			while (str != "stop")
			{
				str = Console.ReadLine();
			}
			thread.Abort();
		}
	}
}