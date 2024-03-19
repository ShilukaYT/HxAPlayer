using System;

namespace BlueStacks.Common
{
	// Token: 0x02000110 RID: 272
	public class FrontendOrientationEventArgs : EventArgs
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00006527 File Offset: 0x00004727
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x0000652F File Offset: 0x0000472F
		public bool IsPotrait { get; set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00006538 File Offset: 0x00004738
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x00006540 File Offset: 0x00004740
		public string PackageName { get; set; } = string.Empty;

		// Token: 0x06000761 RID: 1889 RVA: 0x00006549 File Offset: 0x00004749
		public FrontendOrientationEventArgs(bool isPotrait, string packageName)
		{
			this.IsPotrait = isPotrait;
			this.PackageName = packageName;
		}
	}
}
