using System;

namespace BlueStacks.Common
{
	// Token: 0x02000240 RID: 576
	public class Tuple<T1, T2, T3> : Tuple<T1, T2>
	{
		// Token: 0x060011C8 RID: 4552 RVA: 0x0000EDA9 File Offset: 0x0000CFA9
		public Tuple(T1 item1, T2 item2, T3 item3) : base(item1, item2)
		{
			this.Item3 = item3;
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x0000EDBA File Offset: 0x0000CFBA
		// (set) Token: 0x060011CA RID: 4554 RVA: 0x0000EDC2 File Offset: 0x0000CFC2
		public T3 Item3 { get; set; }
	}
}
