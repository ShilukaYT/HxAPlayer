using System;

namespace BlueStacks.Common
{
	// Token: 0x02000089 RID: 137
	public class Tuple<T1>
	{
		// Token: 0x06000275 RID: 629 RVA: 0x0000DF65 File Offset: 0x0000C165
		public Tuple(T1 item1)
		{
			this.Item1 = item1;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000DF77 File Offset: 0x0000C177
		// (set) Token: 0x06000277 RID: 631 RVA: 0x0000DF7F File Offset: 0x0000C17F
		public T1 Item1 { get; set; }
	}
}
