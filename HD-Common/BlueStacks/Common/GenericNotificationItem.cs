using System;

namespace BlueStacks.Common
{
	// Token: 0x02000077 RID: 119
	public class GenericNotificationItem
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000379F File Offset: 0x0000199F
		// (set) Token: 0x060002CD RID: 717 RVA: 0x000037A7 File Offset: 0x000019A7
		public string Id { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002CE RID: 718 RVA: 0x000037B0 File Offset: 0x000019B0
		// (set) Token: 0x060002CF RID: 719 RVA: 0x000037B8 File Offset: 0x000019B8
		public string Title { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x000037C1 File Offset: 0x000019C1
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x000037C9 File Offset: 0x000019C9
		public string Message { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x000037D2 File Offset: 0x000019D2
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x000037DA File Offset: 0x000019DA
		public NotificationPriority Priority { get; set; } = NotificationPriority.Normal;

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x000037E3 File Offset: 0x000019E3
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x000037EB File Offset: 0x000019EB
		public bool ShowRibbon { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x000037F4 File Offset: 0x000019F4
		// (set) Token: 0x060002D7 RID: 727 RVA: 0x000037FC File Offset: 0x000019FC
		public DateTime CreationTime { get; set; } = DateTime.Now;

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00003805 File Offset: 0x00001A05
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x0000380D File Offset: 0x00001A0D
		public string NotificationMenuImageUrl { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00003816 File Offset: 0x00001A16
		// (set) Token: 0x060002DB RID: 731 RVA: 0x0000381E File Offset: 0x00001A1E
		public string NotificationMenuImageName { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002DC RID: 732 RVA: 0x00003827 File Offset: 0x00001A27
		// (set) Token: 0x060002DD RID: 733 RVA: 0x0000382F File Offset: 0x00001A2F
		public bool IsRead { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00003838 File Offset: 0x00001A38
		// (set) Token: 0x060002DF RID: 735 RVA: 0x00003840 File Offset: 0x00001A40
		public bool IsShown { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x00003849 File Offset: 0x00001A49
		// (set) Token: 0x060002E1 RID: 737 RVA: 0x00003851 File Offset: 0x00001A51
		public NotificationPayloadType PayloadType { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000385A File Offset: 0x00001A5A
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x00003862 File Offset: 0x00001A62
		public bool IsDeleted { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000386B File Offset: 0x00001A6B
		// (set) Token: 0x060002E5 RID: 741 RVA: 0x00003873 File Offset: 0x00001A73
		public bool IsDeferred { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000387C File Offset: 0x00001A7C
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x00003884 File Offset: 0x00001A84
		public string DeferredApp { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000388D File Offset: 0x00001A8D
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x00003895 File Offset: 0x00001A95
		public long DeferredAppUsage { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000389E File Offset: 0x00001A9E
		// (set) Token: 0x060002EB RID: 747 RVA: 0x000038A6 File Offset: 0x00001AA6
		public GenericNotificationDesignItem NotificationDesignItem { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002EC RID: 748 RVA: 0x000038AF File Offset: 0x00001AAF
		// (set) Token: 0x060002ED RID: 749 RVA: 0x000038B7 File Offset: 0x00001AB7
		public SerializableDictionary<string, string> ExtraPayload { get; set; } = new SerializableDictionary<string, string>();

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002EE RID: 750 RVA: 0x000038C0 File Offset: 0x00001AC0
		// (set) Token: 0x060002EF RID: 751 RVA: 0x000038C8 File Offset: 0x00001AC8
		public bool IsAndroidNotification { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x000038D1 File Offset: 0x00001AD1
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x000038D9 File Offset: 0x00001AD9
		public bool IsReceivedStatSent { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x000038E2 File Offset: 0x00001AE2
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x000038EA File Offset: 0x00001AEA
		public string VmName { get; set; } = "Android";

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x000038F3 File Offset: 0x00001AF3
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x000038FB File Offset: 0x00001AFB
		public string Package { get; set; }
	}
}
