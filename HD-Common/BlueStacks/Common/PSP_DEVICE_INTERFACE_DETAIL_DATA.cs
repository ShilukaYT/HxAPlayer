using System;
using System.Runtime.InteropServices;

namespace BlueStacks.Common
{
	// Token: 0x02000167 RID: 359
	public struct PSP_DEVICE_INTERFACE_DETAIL_DATA
	{
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x0000C920 File Offset: 0x0000AB20
		// (set) Token: 0x06000D44 RID: 3396 RVA: 0x0000C928 File Offset: 0x0000AB28
		public int cbSize { readonly get; set; }

		// Token: 0x04000650 RID: 1616
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string DevicePath;
	}
}
