using System;

namespace BlueStacks.Common
{
	// Token: 0x0200023E RID: 574
	public class Tuple<T1>
	{
		// Token: 0x060011C2 RID: 4546 RVA: 0x0000ED68 File Offset: 0x0000CF68
		public Tuple(T1 item1)
		{
			this.Item1 = item1;
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x0000ED77 File Offset: 0x0000CF77
		// (set) Token: 0x060011C4 RID: 4548 RVA: 0x0000ED7F File Offset: 0x0000CF7F
		public T1 Item1 { get; set; }
	}
}
