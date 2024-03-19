using System;

namespace BlueStacks.Common
{
	// Token: 0x020001A9 RID: 425
	public struct GamePad
	{
		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x0000D837 File Offset: 0x0000BA37
		// (set) Token: 0x06000F6E RID: 3950 RVA: 0x0000D83F File Offset: 0x0000BA3F
		public int X { readonly get; set; }

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x0000D848 File Offset: 0x0000BA48
		// (set) Token: 0x06000F70 RID: 3952 RVA: 0x0000D850 File Offset: 0x0000BA50
		public int Y { readonly get; set; }

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x0000D859 File Offset: 0x0000BA59
		// (set) Token: 0x06000F72 RID: 3954 RVA: 0x0000D861 File Offset: 0x0000BA61
		public int Z { readonly get; set; }

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x0000D86A File Offset: 0x0000BA6A
		// (set) Token: 0x06000F74 RID: 3956 RVA: 0x0000D872 File Offset: 0x0000BA72
		public int Rx { readonly get; set; }

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000F75 RID: 3957 RVA: 0x0000D87B File Offset: 0x0000BA7B
		// (set) Token: 0x06000F76 RID: 3958 RVA: 0x0000D883 File Offset: 0x0000BA83
		public int Ry { readonly get; set; }

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x0000D88C File Offset: 0x0000BA8C
		// (set) Token: 0x06000F78 RID: 3960 RVA: 0x0000D894 File Offset: 0x0000BA94
		public int Rz { readonly get; set; }

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x0000D89D File Offset: 0x0000BA9D
		// (set) Token: 0x06000F7A RID: 3962 RVA: 0x0000D8A5 File Offset: 0x0000BAA5
		public int Hat { readonly get; set; }

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0000D8AE File Offset: 0x0000BAAE
		// (set) Token: 0x06000F7C RID: 3964 RVA: 0x0000D8B6 File Offset: 0x0000BAB6
		public uint Mask { readonly get; set; }
	}
}
