using System;
using System.Collections.Generic;
using System.IO;

namespace BlueStacks.Common.Decoding
{
	// Token: 0x0200024A RID: 586
	internal abstract class GifBlock
	{
		// Token: 0x06001207 RID: 4615 RVA: 0x00043F58 File Offset: 0x00042158
		internal static GifBlock ReadBlock(Stream stream, IEnumerable<GifExtension> controlExtensions, bool metadataOnly)
		{
			int num = stream.ReadByte();
			if (num < 0)
			{
				throw GifHelpers.UnexpectedEndOfStreamException();
			}
			if (num == 33)
			{
				return GifExtension.ReadExtension(stream, controlExtensions, metadataOnly);
			}
			if (num == 44)
			{
				return GifFrame.ReadFrame(stream, controlExtensions, metadataOnly);
			}
			if (num != 59)
			{
				throw GifHelpers.UnknownBlockTypeException(num);
			}
			return GifTrailer.ReadTrailer();
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001208 RID: 4616
		internal abstract GifBlockKind Kind { get; }
	}
}
