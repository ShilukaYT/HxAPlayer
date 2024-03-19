using System;
using System.Runtime.InteropServices;

namespace BlueStacks.Common
{
	// Token: 0x02000239 RID: 569
	public struct COPYDATASTRUCT
	{
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x0000EB66 File Offset: 0x0000CD66
		// (set) Token: 0x0600116A RID: 4458 RVA: 0x0000EB6E File Offset: 0x0000CD6E
		public IntPtr dwData { readonly get; set; }

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x0000EB77 File Offset: 0x0000CD77
		// (set) Token: 0x0600116C RID: 4460 RVA: 0x0000EB7F File Offset: 0x0000CD7F
		public int cbData { readonly get; set; }

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x0000EB88 File Offset: 0x0000CD88
		// (set) Token: 0x0600116E RID: 4462 RVA: 0x0000EB90 File Offset: 0x0000CD90
		public IntPtr lpData { readonly get; set; }

		// Token: 0x0600116F RID: 4463 RVA: 0x000418F0 File Offset: 0x0003FAF0
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
