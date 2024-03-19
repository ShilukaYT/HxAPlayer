using System;
using System.Runtime.InteropServices;

namespace BlueStacks.Common
{
	// Token: 0x02000084 RID: 132
	public struct COPYDATASTRUCT
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000B890 File Offset: 0x00009A90
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000B898 File Offset: 0x00009A98
		public IntPtr dwData { readonly get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000B8A1 File Offset: 0x00009AA1
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000B8A9 File Offset: 0x00009AA9
		public int cbData { readonly get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000B8B2 File Offset: 0x00009AB2
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000B8BA File Offset: 0x00009ABA
		public IntPtr lpData { readonly get; set; }

		// Token: 0x06000222 RID: 546 RVA: 0x0000B8C4 File Offset: 0x00009AC4
		public static COPYDATASTRUCT CreateForString(int dwData, string value, bool _ = false)
		{
			return new COPYDATASTRUCT
			{
				dwData = (IntPtr)dwData,
				cbData = ((value != null) ? value.Length : 1) * 2,
				lpData = Marshal.StringToHGlobalUni(value)
			};
		}
	}
}
