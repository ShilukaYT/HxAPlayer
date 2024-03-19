using System;

namespace BlueStacks.Common
{
	// Token: 0x020000DE RID: 222
	[Serializable]
	public class NotificationItem
	{
		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00005219 File Offset: 0x00003419
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x00005221 File Offset: 0x00003421
		public string ID { get; set; } = string.Empty;

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0000522A File Offset: 0x0000342A
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x00005232 File Offset: 0x00003432
		public MuteState MuteState { get; set; } = MuteState.AutoHide;

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0000523B File Offset: 0x0000343B
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x00005243 File Offset: 0x00003443
		public DateTime MuteTime { get; set; } = DateTime.MinValue;

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x0000524C File Offset: 0x0000344C
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x00005254 File Offset: 0x00003454
		public bool ShowDesktopNotifications { get; set; }

		// Token: 0x0600057E RID: 1406 RVA: 0x0001B57C File Offset: 0x0001977C
		public NotificationItem(string key, MuteState state, DateTime now, bool isShowDesktopNotifications = false)
		{
			this.ID = key;
			this.MuteState = state;
			this.MuteTime = now;
			this.ShowDesktopNotifications = isShowDesktopNotifications;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0000525D File Offset: 0x0000345D
		public NotificationItem()
		{
		}
	}
}
