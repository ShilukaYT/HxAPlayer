using System;
using System.Globalization;

namespace BlueStacks.Common
{
	// Token: 0x02000228 RID: 552
	public class Digest
	{
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x0000E8BC File Offset: 0x0000CABC
		// (set) Token: 0x06001134 RID: 4404 RVA: 0x0000E8C4 File Offset: 0x0000CAC4
		public uint A { get; set; }

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x0000E8CD File Offset: 0x0000CACD
		// (set) Token: 0x06001136 RID: 4406 RVA: 0x0000E8D5 File Offset: 0x0000CAD5
		public uint B { get; set; }

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001137 RID: 4407 RVA: 0x0000E8DE File Offset: 0x0000CADE
		// (set) Token: 0x06001138 RID: 4408 RVA: 0x0000E8E6 File Offset: 0x0000CAE6
		public uint C { get; set; }

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06001139 RID: 4409 RVA: 0x0000E8EF File Offset: 0x0000CAEF
		// (set) Token: 0x0600113A RID: 4410 RVA: 0x0000E8F7 File Offset: 0x0000CAF7
		public uint D { get; set; }

		// Token: 0x0600113B RID: 4411 RVA: 0x0000E900 File Offset: 0x0000CB00
		public Digest()
		{
			this.A = 1732584193U;
			this.B = 4023233417U;
			this.C = 2562383102U;
			this.D = 271733878U;
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00040DCC File Offset: 0x0003EFCC
		public string GetString()
		{
			return Digest.ReverseByte(this.A).ToString("X8", CultureInfo.InvariantCulture) + Digest.ReverseByte(this.B).ToString("X8", CultureInfo.InvariantCulture) + Digest.ReverseByte(this.C).ToString("X8", CultureInfo.InvariantCulture) + Digest.ReverseByte(this.D).ToString("X8", CultureInfo.InvariantCulture);
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0000E934 File Offset: 0x0000CB34
		private static uint ReverseByte(uint uiNumber)
		{
			return (uiNumber & 255U) << 24 | uiNumber >> 24 | (uiNumber & 16711680U) >> 8 | (uiNumber & 65280U) << 8;
		}
	}
}
