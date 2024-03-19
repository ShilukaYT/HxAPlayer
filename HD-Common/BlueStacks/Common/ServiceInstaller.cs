using System;
using System.Diagnostics;
using System.IO;

namespace BlueStacks.Common
{
	// Token: 0x020000E4 RID: 228
	public static class ServiceInstaller
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x0001C234 File Offset: 0x0001A434
		public static int ReinstallService()
		{
			string args = "-reinstall -oem bgp";
			Process process = ProcessUtils.StartExe(ServiceInstaller.BinaryName, args, true);
			process.WaitForExit();
			return process.ExitCode;
		}

		// Token: 0x040002E2 RID: 738
		private static string BinaryName = Path.Combine(RegistryStrings.InstallDir, "HD-ServiceInstaller.exe");
	}
}
