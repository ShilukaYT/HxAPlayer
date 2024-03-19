using System;
using System.IO;
using Microsoft.Win32;

namespace BlueStacks.Common
{
	// Token: 0x0200012F RID: 303
	public static class MonitorLocator
	{
		// Token: 0x060009E1 RID: 2529 RVA: 0x0002A56C File Offset: 0x0002876C
		public static void Publish(string vmName, uint vmId)
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(MonitorLocator.REG_PATH, true);
			foreach (string name in registryKey.GetValueNames())
			{
				if (registryKey.GetValueKind(name) != RegistryValueKind.DWord)
				{
					registryKey.DeleteValue(name);
				}
				else
				{
					uint num = (uint)((int)registryKey.GetValue(name, 0));
					if (vmId == num)
					{
						registryKey.DeleteValue(name);
					}
				}
			}
			registryKey.SetValue(vmName, vmId, RegistryValueKind.DWord);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00008D0E File Offset: 0x00006F0E
		public static string[] Fetch()
		{
			return Registry.LocalMachine.OpenSubKey(MonitorLocator.REG_PATH, true).GetValueNames();
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00008D25 File Offset: 0x00006F25
		public static uint Lookup(string vmName)
		{
			return (uint)((int)Registry.LocalMachine.OpenSubKey(MonitorLocator.REG_PATH).GetValue(vmName, 0));
		}

		// Token: 0x04000503 RID: 1283
		private static readonly string REG_PATH = Path.Combine(RegistryManager.Instance.BaseKeyPath, "Monitors");
	}
}
