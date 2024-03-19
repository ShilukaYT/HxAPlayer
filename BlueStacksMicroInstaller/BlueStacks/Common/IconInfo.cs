using System;

namespace BlueStacks.Common
{
	// Token: 0x02000087 RID: 135
	public struct IconInfo
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000B946 File Offset: 0x00009B46
		// (set) Token: 0x0600022A RID: 554 RVA: 0x0000B94E File Offset: 0x00009B4E
		public bool fIcon { readonly get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000B957 File Offset: 0x00009B57
		// (set) Token: 0x0600022C RID: 556 RVA: 0x0000B95F File Offset: 0x00009B5F
		public int xHotspot { readonly get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600022D RID: 557 RVA: 0x0000B968 File Offset: 0x00009B68
		// (set) Token: 0x0600022E RID: 558 RVA: 0x0000B970 File Offset: 0x00009B70
		public int yHotspot { readonly get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000B979 File Offset: 0x00009B79
		// (set) Token: 0x06000230 RID: 560 RVA: 0x0000B981 File Offset: 0x00009B81
		public IntPtr hbmMask { readonly get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000231 RID: 561 RVA: 0x0000B98A File Offset: 0x00009B8A
		// (set) Token: 0x06000232 RID: 562 RVA: 0x0000B992 File Offset: 0x00009B92
		public IntPtr hbmColor { readonly get; set; }
	}
}
