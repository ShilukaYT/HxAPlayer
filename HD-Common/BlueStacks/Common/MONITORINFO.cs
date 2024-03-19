using System;

namespace BlueStacks.Common
{
	// Token: 0x0200017E RID: 382
	public struct MONITORINFO
	{
		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x0000D0A8 File Offset: 0x0000B2A8
		// (set) Token: 0x06000E7E RID: 3710 RVA: 0x0000D0B0 File Offset: 0x0000B2B0
		public int cbSize { readonly get; set; }

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x0000D0B9 File Offset: 0x0000B2B9
		// (set) Token: 0x06000E80 RID: 3712 RVA: 0x0000D0C1 File Offset: 0x0000B2C1
		public RECT rcMonitor { readonly get; set; }

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x0000D0CA File Offset: 0x0000B2CA
		// (set) Token: 0x06000E82 RID: 3714 RVA: 0x0000D0D2 File Offset: 0x0000B2D2
		public RECT rcWork { readonly get; set; }

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x0000D0DB File Offset: 0x0000B2DB
		// (set) Token: 0x06000E84 RID: 3716 RVA: 0x0000D0E3 File Offset: 0x0000B2E3
		public uint dwFlags { readonly get; set; }
	}
}
