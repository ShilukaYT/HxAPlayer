using System;

namespace BlueStacks.Common
{
	// Token: 0x02000107 RID: 263
	public class CloudNotificationItem
	{
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x00005FFF File Offset: 0x000041FF
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x00006007 File Offset: 0x00004207
		public string Title { get; set; } = string.Empty;

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x00006010 File Offset: 0x00004210
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x00006018 File Offset: 0x00004218
		public string Url { get; set; } = string.Empty;

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00006021 File Offset: 0x00004221
		// (set) Token: 0x060006BC RID: 1724 RVA: 0x00006029 File Offset: 0x00004229
		public string Message { get; set; } = string.Empty;

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x00006032 File Offset: 0x00004232
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x0000603A File Offset: 0x0000423A
		public string ImagePath { get; set; } = string.Empty;

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00006043 File Offset: 0x00004243
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x0000604B File Offset: 0x0000424B
		public bool IsRead { get; set; }

		// Token: 0x060006C1 RID: 1729 RVA: 0x00006054 File Offset: 0x00004254
		public CloudNotificationItem()
		{
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x000213EC File Offset: 0x0001F5EC
		public CloudNotificationItem(string title, string content, string imagePath, string url)
		{
			this.Title = title;
			this.Message = content;
			this.ImagePath = imagePath;
			this.Url = url;
		}
	}
}
