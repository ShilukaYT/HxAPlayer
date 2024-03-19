using System;

namespace BlueStacks.Common
{
	// Token: 0x0200008D RID: 141
	public static class Tuple
	{
		// Token: 0x06000281 RID: 641 RVA: 0x0000DFF8 File Offset: 0x0000C1F8
		public static Tuple<T1> Create<T1>(T1 item1)
		{
			return new Tuple<T1>(item1);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000E010 File Offset: 0x0000C210
		public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new Tuple<T1, T2>(item1, item2);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000E02C File Offset: 0x0000C22C
		public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
		{
			return new Tuple<T1, T2, T3>(item1, item2, item3);
		}
	}
}
