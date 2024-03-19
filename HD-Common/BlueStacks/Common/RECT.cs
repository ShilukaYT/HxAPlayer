using System;

namespace BlueStacks.Common
{
	// Token: 0x02000238 RID: 568
	[Serializable]
	public struct RECT
	{
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x0000EB03 File Offset: 0x0000CD03
		// (set) Token: 0x06001161 RID: 4449 RVA: 0x0000EB0B File Offset: 0x0000CD0B
		public int Left { readonly get; set; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001162 RID: 4450 RVA: 0x0000EB14 File Offset: 0x0000CD14
		// (set) Token: 0x06001163 RID: 4451 RVA: 0x0000EB1C File Offset: 0x0000CD1C
		public int Top { readonly get; set; }

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001164 RID: 4452 RVA: 0x0000EB25 File Offset: 0x0000CD25
		// (set) Token: 0x06001165 RID: 4453 RVA: 0x0000EB2D File Offset: 0x0000CD2D
		public int Right { readonly get; set; }

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001166 RID: 4454 RVA: 0x0000EB36 File Offset: 0x0000CD36
		// (set) Token: 0x06001167 RID: 4455 RVA: 0x0000EB3E File Offset: 0x0000CD3E
		public int Bottom { readonly get; set; }

		// Token: 0x06001168 RID: 4456 RVA: 0x0000EB47 File Offset: 0x0000CD47
		public RECT(int left, int top, int right, int bottom)
		{
			this.Left = left;
			this.Top = top;
			this.Right = right;
			this.Bottom = bottom;
		}
	}
}
