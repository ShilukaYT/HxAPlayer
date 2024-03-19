using System;

namespace BlueStacks.Common.Decoding
{
	// Token: 0x02000259 RID: 601
	internal class GifTrailer : GifBlock
	{
		// Token: 0x0600129F RID: 4767 RVA: 0x0000EF6B File Offset: 0x0000D16B
		private GifTrailer()
		{
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x0000F0D2 File Offset: 0x0000D2D2
		internal override GifBlockKind Kind
		{
			get
			{
				return GifBlockKind.Other;
			}
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0000F418 File Offset: 0x0000D618
		internal static GifTrailer ReadTrailer()
		{
			return new GifTrailer();
		}

		// Token: 0x04000E83 RID: 3715
		internal const int TrailerByte = 59;
	}
}
