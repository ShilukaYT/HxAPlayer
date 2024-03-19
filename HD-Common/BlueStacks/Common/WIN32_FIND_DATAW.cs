using System;
using System.Runtime.InteropServices;

namespace BlueStacks.Common
{
	// Token: 0x0200013D RID: 317
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct WIN32_FIND_DATAW
	{
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0000C0F5 File Offset: 0x0000A2F5
		// (set) Token: 0x06000C30 RID: 3120 RVA: 0x0000C0FD File Offset: 0x0000A2FD
		public uint dwFileAttributes { readonly get; set; }

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0000C106 File Offset: 0x0000A306
		// (set) Token: 0x06000C32 RID: 3122 RVA: 0x0000C10E File Offset: 0x0000A30E
		public long ftCreationTime { readonly get; set; }

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000C33 RID: 3123 RVA: 0x0000C117 File Offset: 0x0000A317
		// (set) Token: 0x06000C34 RID: 3124 RVA: 0x0000C11F File Offset: 0x0000A31F
		public long ftLastAccessTime { readonly get; set; }

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x0000C128 File Offset: 0x0000A328
		// (set) Token: 0x06000C36 RID: 3126 RVA: 0x0000C130 File Offset: 0x0000A330
		public long ftLastWriteTime { readonly get; set; }

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0000C139 File Offset: 0x0000A339
		// (set) Token: 0x06000C38 RID: 3128 RVA: 0x0000C141 File Offset: 0x0000A341
		public uint nFileSizeHigh { readonly get; set; }

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0000C14A File Offset: 0x0000A34A
		// (set) Token: 0x06000C3A RID: 3130 RVA: 0x0000C152 File Offset: 0x0000A352
		public uint nFileSizeLow { readonly get; set; }

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0000C15B File Offset: 0x0000A35B
		// (set) Token: 0x06000C3C RID: 3132 RVA: 0x0000C163 File Offset: 0x0000A363
		public uint dwReserved0 { readonly get; set; }

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x0000C16C File Offset: 0x0000A36C
		// (set) Token: 0x06000C3E RID: 3134 RVA: 0x0000C174 File Offset: 0x0000A374
		public uint dwReserved1 { readonly get; set; }

		// Token: 0x0400058A RID: 1418
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string cFileName;

		// Token: 0x0400058B RID: 1419
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
		public string cAlternateFileName;
	}
}
