using System;

namespace BlueStacks.Common
{
	// Token: 0x0200008C RID: 140
	public class Tuple<T1, T2, T3, T4> : Tuple<T1, T2, T3>
	{
		// Token: 0x0600027E RID: 638 RVA: 0x0000DFD1 File Offset: 0x0000C1D1
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4) : base(item1, item2, item3)
		{
			this.Item4 = item4;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000DFE7 File Offset: 0x0000C1E7
		// (set) Token: 0x06000280 RID: 640 RVA: 0x0000DFEF File Offset: 0x0000C1EF
		public T4 Item4 { get; set; }
	}
}
