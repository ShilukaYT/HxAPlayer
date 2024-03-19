using System;

namespace BlueStacks.Common
{
	// Token: 0x02000242 RID: 578
	public static class Tuple
	{
		// Token: 0x060011CE RID: 4558 RVA: 0x0000EDEF File Offset: 0x0000CFEF
		public static Tuple<T1> Create<T1>(T1 item1)
		{
			return new Tuple<T1>(item1);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0000EDF7 File Offset: 0x0000CFF7
		public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new Tuple<T1, T2>(item1, item2);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0000EE00 File Offset: 0x0000D000
		public static Tuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
		{
			return new Tuple<T1, T2, T3>(item1, item2, item3);
		}
	}
}
