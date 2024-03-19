using System;
using System.Drawing;

namespace BlueStacks.Common
{
	// Token: 0x02000082 RID: 130
	public struct COMPOSITIONFORM
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000B7F5 File Offset: 0x000099F5
		// (set) Token: 0x0600020E RID: 526 RVA: 0x0000B7FD File Offset: 0x000099FD
		public int dwStyle { readonly get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000B806 File Offset: 0x00009A06
		// (set) Token: 0x06000210 RID: 528 RVA: 0x0000B80E File Offset: 0x00009A0E
		public Point ptCurrentPos { readonly get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000B817 File Offset: 0x00009A17
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000B81F File Offset: 0x00009A1F
		public RECT rcArea { readonly get; set; }
	}
}
