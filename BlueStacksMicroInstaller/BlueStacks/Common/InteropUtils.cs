using System;
using System.Runtime.InteropServices;

namespace BlueStacks.Common
{
	// Token: 0x02000056 RID: 86
	public static class InteropUtils
	{
		// Token: 0x060000C2 RID: 194
		[DllImport("kernel32.dll")]
		public static extern long GetTickCount64();
	}
}
