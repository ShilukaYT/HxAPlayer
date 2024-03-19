using System;

namespace BlueStacks.Common
{
	// Token: 0x02000241 RID: 577
	public class Tuple<T1, T2, T3, T4> : Tuple<T1, T2, T3>
	{
		// Token: 0x060011CB RID: 4555 RVA: 0x0000EDCB File Offset: 0x0000CFCB
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4) : base(item1, item2, item3)
		{
			this.Item4 = item4;
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x0000EDDE File Offset: 0x0000CFDE
		// (set) Token: 0x060011CD RID: 4557 RVA: 0x0000EDE6 File Offset: 0x0000CFE6
		public T4 Item4 { get; set; }
	}
}
