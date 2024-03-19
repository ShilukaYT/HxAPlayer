using System;

namespace BlueStacks.Common
{
	// Token: 0x02000111 RID: 273
	public class ChangeOrientationEventArgs : EventArgs
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x0000656A File Offset: 0x0000476A
		// (set) Token: 0x06000763 RID: 1891 RVA: 0x00006572 File Offset: 0x00004772
		public bool IsPotrait { get; set; }

		// Token: 0x06000764 RID: 1892 RVA: 0x0000657B File Offset: 0x0000477B
		public ChangeOrientationEventArgs(bool isPotrait)
		{
			this.IsPotrait = isPotrait;
		}
	}
}
