using System;
using System.Runtime.Serialization;

namespace BlueStacks.Common.Decoding
{
	// Token: 0x0200024E RID: 590
	[Serializable]
	public class GifDecoderException : Exception
	{
		// Token: 0x06001212 RID: 4626 RVA: 0x00002418 File Offset: 0x00000618
		internal GifDecoderException()
		{
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00002420 File Offset: 0x00000620
		internal GifDecoderException(string message) : base(message)
		{
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00002429 File Offset: 0x00000629
		internal GifDecoderException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00002433 File Offset: 0x00000633
		protected GifDecoderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
