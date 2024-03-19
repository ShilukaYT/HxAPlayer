using System;
using System.Runtime.InteropServices;

namespace BlueStacks.Common
{
	// Token: 0x020001E8 RID: 488
	public static class InteropUtils
	{
		// Token: 0x06000FDA RID: 4058
		[DllImport("kernel32.dll")]
		public static extern long GetTickCount64();
	}
}
