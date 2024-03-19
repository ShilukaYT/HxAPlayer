using System;
using System.Globalization;

namespace BlueStacks.Common
{
	// Token: 0x02000073 RID: 115
	public class Digest
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x0000A94A File Offset: 0x00008B4A
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x0000A952 File Offset: 0x00008B52
		public uint A { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x0000A95B File Offset: 0x00008B5B
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x0000A963 File Offset: 0x00008B63
		public uint B { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001EA RID: 490 RVA: 0x0000A96C File Offset: 0x00008B6C
		// (set) Token: 0x060001EB RID: 491 RVA: 0x0000A974 File Offset: 0x00008B74
		public uint C { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001EC RID: 492 RVA: 0x0000A97D File Offset: 0x00008B7D
		// (set) Token: 0x060001ED RID: 493 RVA: 0x0000A985 File Offset: 0x00008B85
		public uint D { get; set; }

		// Token: 0x060001EE RID: 494 RVA: 0x0000A98E File Offset: 0x00008B8E
		public Digest()
		{
			this.A = 1732584193U;
			this.B = 4023233417U;
			this.C = 2562383102U;
			this.D = 271733878U;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000A9C8 File Offset: 0x00008BC8
		public string GetString()
		{
			return Digest.ReverseByte(this.A).ToString("X8", CultureInfo.InvariantCulture) + Digest.ReverseByte(this.B).ToString("X8", CultureInfo.InvariantCulture) + Digest.ReverseByte(this.C).ToString("X8", CultureInfo.InvariantCulture) + Digest.ReverseByte(this.D).ToString("X8", CultureInfo.InvariantCulture);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000AA58 File Offset: 0x00008C58
		private static uint ReverseByte(uint uiNumber)
		{
			return (uiNumber & 255U) << 24 | uiNumber >> 24 | (uiNumber & 16711680U) >> 8 | (uiNumber & 65280U) << 8;
		}
	}
}
