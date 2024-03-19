using System;

namespace BlueStacks.Common
{
	// Token: 0x02000018 RID: 24
	public struct GamePad
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003F17 File Offset: 0x00002117
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00003F1F File Offset: 0x0000211F
		public int X { readonly get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003F28 File Offset: 0x00002128
		// (set) Token: 0x0600005B RID: 91 RVA: 0x00003F30 File Offset: 0x00002130
		public int Y { readonly get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003F39 File Offset: 0x00002139
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00003F41 File Offset: 0x00002141
		public int Z { readonly get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003F4A File Offset: 0x0000214A
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00003F52 File Offset: 0x00002152
		public int Rx { readonly get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003F5B File Offset: 0x0000215B
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00003F63 File Offset: 0x00002163
		public int Ry { readonly get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003F6C File Offset: 0x0000216C
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00003F74 File Offset: 0x00002174
		public int Rz { readonly get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003F7D File Offset: 0x0000217D
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00003F85 File Offset: 0x00002185
		public int Hat { readonly get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003F8E File Offset: 0x0000218E
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00003F96 File Offset: 0x00002196
		public uint Mask { readonly get; set; }
	}
}
