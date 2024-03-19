using System;
using System.Drawing;

namespace BlueStacks.Common
{
	// Token: 0x02000237 RID: 567
	public struct COMPOSITIONFORM
	{
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x0000EAD0 File Offset: 0x0000CCD0
		// (set) Token: 0x0600115B RID: 4443 RVA: 0x0000EAD8 File Offset: 0x0000CCD8
		public int dwStyle { readonly get; set; }

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600115C RID: 4444 RVA: 0x0000EAE1 File Offset: 0x0000CCE1
		// (set) Token: 0x0600115D RID: 4445 RVA: 0x0000EAE9 File Offset: 0x0000CCE9
		public Point ptCurrentPos { readonly get; set; }

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x0000EAF2 File Offset: 0x0000CCF2
		// (set) Token: 0x0600115F RID: 4447 RVA: 0x0000EAFA File Offset: 0x0000CCFA
		public RECT rcArea { readonly get; set; }
	}
}
