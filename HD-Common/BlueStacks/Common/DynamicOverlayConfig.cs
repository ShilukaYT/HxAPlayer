using System;

namespace BlueStacks.Common
{
	// Token: 0x0200006E RID: 110
	[Serializable]
	public class DynamicOverlayConfig
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00003472 File Offset: 0x00001672
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000347A File Offset: 0x0000167A
		public string Type { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00003483 File Offset: 0x00001683
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000348B File Offset: 0x0000168B
		public string Enabled { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00003494 File Offset: 0x00001694
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0000349C File Offset: 0x0000169C
		public double X { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000274 RID: 628 RVA: 0x000034A5 File Offset: 0x000016A5
		// (set) Token: 0x06000275 RID: 629 RVA: 0x000034AD File Offset: 0x000016AD
		public double Y { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000276 RID: 630 RVA: 0x000034B6 File Offset: 0x000016B6
		// (set) Token: 0x06000277 RID: 631 RVA: 0x000034BE File Offset: 0x000016BE
		public double LookAroundX { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000278 RID: 632 RVA: 0x000034C7 File Offset: 0x000016C7
		// (set) Token: 0x06000279 RID: 633 RVA: 0x000034CF File Offset: 0x000016CF
		public double LookAroundY { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600027A RID: 634 RVA: 0x000034D8 File Offset: 0x000016D8
		// (set) Token: 0x0600027B RID: 635 RVA: 0x000034E0 File Offset: 0x000016E0
		public double LButtonX { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600027C RID: 636 RVA: 0x000034E9 File Offset: 0x000016E9
		// (set) Token: 0x0600027D RID: 637 RVA: 0x000034F1 File Offset: 0x000016F1
		public double LButtonY { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600027E RID: 638 RVA: 0x000034FA File Offset: 0x000016FA
		// (set) Token: 0x0600027F RID: 639 RVA: 0x00003502 File Offset: 0x00001702
		public double CancelX { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000350B File Offset: 0x0000170B
		// (set) Token: 0x06000281 RID: 641 RVA: 0x00003513 File Offset: 0x00001713
		public double CancelY { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000351C File Offset: 0x0000171C
		// (set) Token: 0x06000283 RID: 643 RVA: 0x00003524 File Offset: 0x00001724
		public bool LButtonShowOnOverlay { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000352D File Offset: 0x0000172D
		// (set) Token: 0x06000285 RID: 645 RVA: 0x00003535 File Offset: 0x00001735
		public bool LookAroundShowOnOverlay { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000353E File Offset: 0x0000173E
		// (set) Token: 0x06000287 RID: 647 RVA: 0x00003546 File Offset: 0x00001746
		public bool CancelShowOnOverlay { get; set; }
	}
}
