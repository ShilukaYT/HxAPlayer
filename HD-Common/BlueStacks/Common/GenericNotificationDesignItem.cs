using System;
using System.Collections.Generic;

namespace BlueStacks.Common
{
	// Token: 0x02000076 RID: 118
	public class GenericNotificationDesignItem
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x000036C9 File Offset: 0x000018C9
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x000036D1 File Offset: 0x000018D1
		public double AutoHideTime { get; set; } = 3500.0;

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x000036DA File Offset: 0x000018DA
		// (set) Token: 0x060002BA RID: 698 RVA: 0x000036E2 File Offset: 0x000018E2
		public string LeftGifName { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002BB RID: 699 RVA: 0x000036EB File Offset: 0x000018EB
		// (set) Token: 0x060002BC RID: 700 RVA: 0x000036F3 File Offset: 0x000018F3
		public string LeftGifUrl { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002BD RID: 701 RVA: 0x000036FC File Offset: 0x000018FC
		// (set) Token: 0x060002BE RID: 702 RVA: 0x00003704 File Offset: 0x00001904
		public string TitleForeGroundColor { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000370D File Offset: 0x0000190D
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x00003715 File Offset: 0x00001915
		public string MessageForeGroundColor { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000371E File Offset: 0x0000191E
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x00003726 File Offset: 0x00001926
		public string BorderColor { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000372F File Offset: 0x0000192F
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x00003737 File Offset: 0x00001937
		public string Ribboncolor { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00003740 File Offset: 0x00001940
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x00003748 File Offset: 0x00001948
		public string HoverBorderColor { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x00003751 File Offset: 0x00001951
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x00003759 File Offset: 0x00001959
		public string HoverRibboncolor { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x00003762 File Offset: 0x00001962
		public List<SerializableKeyValuePair<string, double>> BackgroundGradient { get; } = new List<SerializableKeyValuePair<string, double>>();

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000376A File Offset: 0x0000196A
		public List<SerializableKeyValuePair<string, double>> HoverBackGroundGradient { get; } = new List<SerializableKeyValuePair<string, double>>();
	}
}
