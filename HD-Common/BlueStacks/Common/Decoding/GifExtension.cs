using System;
using System.Collections.Generic;
using System.IO;

namespace BlueStacks.Common.Decoding
{
	// Token: 0x0200024F RID: 591
	internal abstract class GifExtension : GifBlock
	{
		// Token: 0x06001216 RID: 4630 RVA: 0x00044020 File Offset: 0x00042220
		internal static GifExtension ReadExtension(Stream stream, IEnumerable<GifExtension> controlExtensions, bool metadataOnly)
		{
			int num = stream.ReadByte();
			if (num < 0)
			{
				throw GifHelpers.UnexpectedEndOfStreamException();
			}
			if (num <= 249)
			{
				if (num == 1)
				{
					return GifPlainTextExtension.ReadPlainText(stream, controlExtensions, metadataOnly);
				}
				if (num == 249)
				{
					return GifGraphicControlExtension.ReadGraphicsControl(stream);
				}
			}
			else
			{
				if (num == 254)
				{
					return GifCommentExtension.ReadComment(stream);
				}
				if (num == 255)
				{
					return GifApplicationExtension.ReadApplication(stream);
				}
			}
			throw GifHelpers.UnknownExtensionTypeException(num);
		}

		// Token: 0x04000E50 RID: 3664
		internal const int ExtensionIntroducer = 33;
	}
}
