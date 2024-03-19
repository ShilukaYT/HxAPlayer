using System;
using BlueStacks.Common;

namespace BlueStacks.MicroInstaller
{
	// Token: 0x0200009E RID: 158
	public class MicroInstallerProperties
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00017CD9 File Offset: 0x00015ED9
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x00017CE0 File Offset: 0x00015EE0
		public static DownloadBuildType BuildPlatformToDownload { get; set; } = DownloadBuildType.Default;
	}
}
