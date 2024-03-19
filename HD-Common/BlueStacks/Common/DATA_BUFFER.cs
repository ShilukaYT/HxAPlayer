using System;
using System.Runtime.InteropServices;

namespace BlueStacks.Common
{
	// Token: 0x02000164 RID: 356
	public struct DATA_BUFFER
	{
		// Token: 0x04000644 RID: 1604
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1024)]
		public string Buffer;
	}
}
