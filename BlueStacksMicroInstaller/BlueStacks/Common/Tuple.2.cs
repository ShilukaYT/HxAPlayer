using System;

namespace BlueStacks.Common
{
	// Token: 0x0200008A RID: 138
	public class Tuple<T1, T2> : Tuple<T1>
	{
		// Token: 0x06000278 RID: 632 RVA: 0x0000DF88 File Offset: 0x0000C188
		public Tuple(T1 item1, T2 item2) : base(item1)
		{
			this.Item2 = item2;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000DF9B File Offset: 0x0000C19B
		// (set) Token: 0x0600027A RID: 634 RVA: 0x0000DFA3 File Offset: 0x0000C1A3
		public T2 Item2 { get; set; }
	}
}
