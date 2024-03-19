using System;

namespace BlueStacks.Common
{
	// Token: 0x0200017D RID: 381
	[Serializable]
	public struct WINDOWPLACEMENT
	{
		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0000D042 File Offset: 0x0000B242
		// (set) Token: 0x06000E72 RID: 3698 RVA: 0x0000D04A File Offset: 0x0000B24A
		public int length { readonly get; set; }

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x0000D053 File Offset: 0x0000B253
		// (set) Token: 0x06000E74 RID: 3700 RVA: 0x0000D05B File Offset: 0x0000B25B
		public int flags { readonly get; set; }

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x0000D064 File Offset: 0x0000B264
		// (set) Token: 0x06000E76 RID: 3702 RVA: 0x0000D06C File Offset: 0x0000B26C
		public int showCmd { readonly get; set; }

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x0000D075 File Offset: 0x0000B275
		// (set) Token: 0x06000E78 RID: 3704 RVA: 0x0000D07D File Offset: 0x0000B27D
		public POINT minPosition { readonly get; set; }

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x0000D086 File Offset: 0x0000B286
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x0000D08E File Offset: 0x0000B28E
		public POINT maxPosition { readonly get; set; }

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x0000D097 File Offset: 0x0000B297
		// (set) Token: 0x06000E7C RID: 3708 RVA: 0x0000D09F File Offset: 0x0000B29F
		public RECT normalPosition { readonly get; set; }
	}
}
