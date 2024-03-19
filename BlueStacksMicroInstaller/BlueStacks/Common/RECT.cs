using System;

namespace BlueStacks.Common
{
	// Token: 0x02000083 RID: 131
	[Serializable]
	public struct RECT
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000B828 File Offset: 0x00009A28
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000B830 File Offset: 0x00009A30
		public int Left { readonly get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000B839 File Offset: 0x00009A39
		// (set) Token: 0x06000216 RID: 534 RVA: 0x0000B841 File Offset: 0x00009A41
		public int Top { readonly get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000B84A File Offset: 0x00009A4A
		// (set) Token: 0x06000218 RID: 536 RVA: 0x0000B852 File Offset: 0x00009A52
		public int Right { readonly get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000B85B File Offset: 0x00009A5B
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000B863 File Offset: 0x00009A63
		public int Bottom { readonly get; set; }

		// Token: 0x0600021B RID: 539 RVA: 0x0000B86C File Offset: 0x00009A6C
		public RECT(int left, int top, int right, int bottom)
		{
			this.Left = left;
			this.Top = top;
			this.Right = right;
			this.Bottom = bottom;
		}
	}
}
