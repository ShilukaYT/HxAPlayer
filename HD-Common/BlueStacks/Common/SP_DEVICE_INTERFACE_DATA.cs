using System;

namespace BlueStacks.Common
{
	// Token: 0x02000166 RID: 358
	public struct SP_DEVICE_INTERFACE_DATA
	{
		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x0000C8DC File Offset: 0x0000AADC
		// (set) Token: 0x06000D3C RID: 3388 RVA: 0x0000C8E4 File Offset: 0x0000AAE4
		public int CbSize { readonly get; set; }

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x0000C8ED File Offset: 0x0000AAED
		// (set) Token: 0x06000D3E RID: 3390 RVA: 0x0000C8F5 File Offset: 0x0000AAF5
		public Guid InterfaceClassGuid { readonly get; set; }

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x0000C8FE File Offset: 0x0000AAFE
		// (set) Token: 0x06000D40 RID: 3392 RVA: 0x0000C906 File Offset: 0x0000AB06
		public int Flags { readonly get; set; }

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x0000C90F File Offset: 0x0000AB0F
		// (set) Token: 0x06000D42 RID: 3394 RVA: 0x0000C917 File Offset: 0x0000AB17
		public int Reserved { readonly get; set; }
	}
}
