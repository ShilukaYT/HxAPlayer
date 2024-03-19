using System;

namespace BlueStacks.Common
{
	// Token: 0x0200023F RID: 575
	public class Tuple<T1, T2> : Tuple<T1>
	{
		// Token: 0x060011C5 RID: 4549 RVA: 0x0000ED88 File Offset: 0x0000CF88
		public Tuple(T1 item1, T2 item2) : base(item1)
		{
			this.Item2 = item2;
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x0000ED98 File Offset: 0x0000CF98
		// (set) Token: 0x060011C7 RID: 4551 RVA: 0x0000EDA0 File Offset: 0x0000CFA0
		public T2 Item2 { get; set; }
	}
}
