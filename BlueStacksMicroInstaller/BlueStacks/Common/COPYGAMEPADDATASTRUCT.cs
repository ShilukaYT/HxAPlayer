using System;

namespace BlueStacks.Common
{
	// Token: 0x02000085 RID: 133
	public struct COPYGAMEPADDATASTRUCT
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000B913 File Offset: 0x00009B13
		// (set) Token: 0x06000224 RID: 548 RVA: 0x0000B91B File Offset: 0x00009B1B
		public IntPtr dwData { readonly get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000B924 File Offset: 0x00009B24
		// (set) Token: 0x06000226 RID: 550 RVA: 0x0000B92C File Offset: 0x00009B2C
		public int size { readonly get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000B935 File Offset: 0x00009B35
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0000B93D File Offset: 0x00009B3D
		public IntPtr lpData { readonly get; set; }
	}
}
