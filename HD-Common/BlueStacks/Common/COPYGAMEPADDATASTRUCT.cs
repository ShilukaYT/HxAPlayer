using System;

namespace BlueStacks.Common
{
	// Token: 0x0200023A RID: 570
	public struct COPYGAMEPADDATASTRUCT
	{
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x0000EB99 File Offset: 0x0000CD99
		// (set) Token: 0x06001171 RID: 4465 RVA: 0x0000EBA1 File Offset: 0x0000CDA1
		public IntPtr dwData { readonly get; set; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x0000EBAA File Offset: 0x0000CDAA
		// (set) Token: 0x06001173 RID: 4467 RVA: 0x0000EBB2 File Offset: 0x0000CDB2
		public int size { readonly get; set; }

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x0000EBBB File Offset: 0x0000CDBB
		// (set) Token: 0x06001175 RID: 4469 RVA: 0x0000EBC3 File Offset: 0x0000CDC3
		public IntPtr lpData { readonly get; set; }
	}
}
