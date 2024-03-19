using System;
using System.Diagnostics;
using System.IO;

namespace BlueStacks.Common
{
	// Token: 0x020000CA RID: 202
	public static class ComRegistration
	{
		// Token: 0x060004EF RID: 1263 RVA: 0x00004DCC File Offset: 0x00002FCC
		public static int Register()
		{
			Logger.Info("Registering COM components");
			return ComRegistration.RunBinary("-reg");
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00004DE2 File Offset: 0x00002FE2
		public static int Unregister()
		{
			Logger.Info("Unregistering COM components");
			return ComRegistration.RunBinary("-unreg");
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001939C File Offset: 0x0001759C
		private static int RunBinary(string args)
		{
			Process process = new Process();
			process.StartInfo.FileName = ComRegistration.BIN_PATH;
			process.StartInfo.Arguments = args;
			process.StartInfo.UseShellExecute = true;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.Verb = "runas";
			process.Start();
			process.WaitForExit();
			return process.ExitCode;
		}

		// Token: 0x04000234 RID: 564
		private static string BIN_PATH = Path.Combine(RegistryStrings.InstallDir, "HD-ComRegistrar.exe");
	}
}
