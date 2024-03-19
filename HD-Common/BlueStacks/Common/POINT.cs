using System;

namespace BlueStacks.Common
{
	// Token: 0x0200017C RID: 380
	[Serializable]
	public struct POINT
	{
		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0000D010 File Offset: 0x0000B210
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x0000D018 File Offset: 0x0000B218
		public int X { readonly get; set; }

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x0000D021 File Offset: 0x0000B221
		// (set) Token: 0x06000E6F RID: 3695 RVA: 0x0000D029 File Offset: 0x0000B229
		public int Y { readonly get; set; }

		// Token: 0x06000E70 RID: 3696 RVA: 0x0000D032 File Offset: 0x0000B232
		public POINT(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}
	}
}
