using System;

namespace BlueStacks.Common
{
	// Token: 0x02000112 RID: 274
	public class StringEventArgs : EventArgs
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0000658A File Offset: 0x0000478A
		// (set) Token: 0x06000766 RID: 1894 RVA: 0x00006592 File Offset: 0x00004792
		public string Str { get; set; } = string.Empty;

		// Token: 0x06000767 RID: 1895 RVA: 0x0000659B File Offset: 0x0000479B
		public StringEventArgs(string str)
		{
			this.Str = str;
		}
	}
}
