using System;

namespace BlueStacks.Common
{
	// Token: 0x02000163 RID: 355
	public struct SP_DEVINFO_DATA
	{
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x0000C898 File Offset: 0x0000AA98
		// (set) Token: 0x06000D34 RID: 3380 RVA: 0x0000C8A0 File Offset: 0x0000AAA0
		public int cbSize { readonly get; set; }

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x0000C8A9 File Offset: 0x0000AAA9
		// (set) Token: 0x06000D36 RID: 3382 RVA: 0x0000C8B1 File Offset: 0x0000AAB1
		public Guid ClassGuid { readonly get; set; }

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x0000C8BA File Offset: 0x0000AABA
		// (set) Token: 0x06000D38 RID: 3384 RVA: 0x0000C8C2 File Offset: 0x0000AAC2
		public int DevInst { readonly get; set; }

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x0000C8CB File Offset: 0x0000AACB
		// (set) Token: 0x06000D3A RID: 3386 RVA: 0x0000C8D3 File Offset: 0x0000AAD3
		public int Reserved { readonly get; set; }
	}
}
