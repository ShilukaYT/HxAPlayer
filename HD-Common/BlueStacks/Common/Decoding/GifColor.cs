using System;
using System.Globalization;

namespace BlueStacks.Common.Decoding
{
	// Token: 0x0200024C RID: 588
	internal struct GifColor
	{
		// Token: 0x0600120A RID: 4618 RVA: 0x0000EF35 File Offset: 0x0000D135
		internal GifColor(byte r, byte g, byte b)
		{
			this.R = r;
			this.G = g;
			this.B = b;
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00043FA8 File Offset: 0x000421A8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "#{0:x2}{1:x2}{2:x2}", new object[]
			{
				this.R,
				this.G,
				this.B
			});
		}

		// Token: 0x04000E4B RID: 3659
		private readonly byte R;

		// Token: 0x04000E4C RID: 3660
		private readonly byte G;

		// Token: 0x04000E4D RID: 3661
		private readonly byte B;
	}
}
