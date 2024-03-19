using System;
using System.IO;

namespace BlueStacks.Common
{
	// Token: 0x020000C5 RID: 197
	public static class ForceDedicatedGPU
	{
		// Token: 0x060004D4 RID: 1236 RVA: 0x00018A58 File Offset: 0x00016C58
		public static bool ToggleDedicatedGPU(bool enable, string binPath = null)
		{
			try
			{
				if (binPath == null)
				{
					binPath = Path.Combine(RegistryStrings.InstallDir, "HD-ForceGPU.exe");
				}
				string args = enable ? "1" : "0";
				return RunCommand.RunCmd(binPath, args, true, true, false, 0).ExitCode == 0;
			}
			catch (Exception ex)
			{
				Logger.Error("An error occured while running {0}, Ex: {1}", new object[]
				{
					binPath,
					ex
				});
			}
			return false;
		}

		// Token: 0x04000228 RID: 552
		private const string ENABLE_ARG = "1";

		// Token: 0x04000229 RID: 553
		private const string DISABLE_ARG = "0";
	}
}
