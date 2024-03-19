using System;

namespace BlueStacks.Common
{
	// Token: 0x0200008B RID: 139
	public class Tuple<T1, T2, T3> : Tuple<T1, T2>
	{
		// Token: 0x0600027B RID: 635 RVA: 0x0000DFAC File Offset: 0x0000C1AC
		public Tuple(T1 item1, T2 item2, T3 item3) : base(item1, item2)
		{
			this.Item3 = item3;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000DFC0 File Offset: 0x0000C1C0
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000DFC8 File Offset: 0x0000C1C8
		public T3 Item3 { get; set; }
	}
}
