using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000078 RID: 120
	public class BrowserEventArgs : EventArgs
	{
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00003934 File Offset: 0x00001B34
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000393C File Offset: 0x00001B3C
		public BrowserControlTags ClientTag { get; private set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00003945 File Offset: 0x00001B45
		// (set) Token: 0x060002FA RID: 762 RVA: 0x0000394D File Offset: 0x00001B4D
		public string mVmName { get; private set; } = string.Empty;

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00003956 File Offset: 0x00001B56
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0000395E File Offset: 0x00001B5E
		public JObject ExtraData { get; private set; }

		// Token: 0x060002FD RID: 765 RVA: 0x00003967 File Offset: 0x00001B67
		public BrowserEventArgs(BrowserControlTags tag, string vmName, JObject extraData)
		{
			this.ClientTag = tag;
			this.mVmName = vmName;
			this.ExtraData = extraData;
		}
	}
}
