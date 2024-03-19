using System;

namespace BlueStacks.Common
{
	// Token: 0x0200023C RID: 572
	public struct IconInfo
	{
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x0000EBCC File Offset: 0x0000CDCC
		// (set) Token: 0x06001177 RID: 4471 RVA: 0x0000EBD4 File Offset: 0x0000CDD4
		public bool fIcon { readonly get; set; }

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x0000EBDD File Offset: 0x0000CDDD
		// (set) Token: 0x06001179 RID: 4473 RVA: 0x0000EBE5 File Offset: 0x0000CDE5
		public int xHotspot { readonly get; set; }

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x0000EBEE File Offset: 0x0000CDEE
		// (set) Token: 0x0600117B RID: 4475 RVA: 0x0000EBF6 File Offset: 0x0000CDF6
		public int yHotspot { readonly get; set; }

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x0000EBFF File Offset: 0x0000CDFF
		// (set) Token: 0x0600117D RID: 4477 RVA: 0x0000EC07 File Offset: 0x0000CE07
		public IntPtr hbmMask { readonly get; set; }

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x0000EC10 File Offset: 0x0000CE10
		// (set) Token: 0x0600117F RID: 4479 RVA: 0x0000EC18 File Offset: 0x0000CE18
		public IntPtr hbmColor { readonly get; set; }
	}
}
