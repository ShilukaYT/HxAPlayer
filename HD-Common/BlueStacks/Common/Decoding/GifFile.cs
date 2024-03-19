using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BlueStacks.Common.Decoding
{
	// Token: 0x02000250 RID: 592
	internal class GifFile
	{
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x0000EF73 File Offset: 0x0000D173
		// (set) Token: 0x06001219 RID: 4633 RVA: 0x0000EF7B File Offset: 0x0000D17B
		public GifHeader Header { get; private set; }

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x0000EF84 File Offset: 0x0000D184
		// (set) Token: 0x0600121B RID: 4635 RVA: 0x0000EF8C File Offset: 0x0000D18C
		public GifColor[] GlobalColorTable { get; set; }

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x0000EF95 File Offset: 0x0000D195
		// (set) Token: 0x0600121D RID: 4637 RVA: 0x0000EF9D File Offset: 0x0000D19D
		public IList<GifFrame> Frames { get; set; }

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x0000EFA6 File Offset: 0x0000D1A6
		// (set) Token: 0x0600121F RID: 4639 RVA: 0x0000EFAE File Offset: 0x0000D1AE
		public IList<GifExtension> Extensions { get; set; }

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x0000EFB7 File Offset: 0x0000D1B7
		// (set) Token: 0x06001221 RID: 4641 RVA: 0x0000EFBF File Offset: 0x0000D1BF
		public ushort RepeatCount { get; set; }

		// Token: 0x06001222 RID: 4642 RVA: 0x0000225B File Offset: 0x0000045B
		private GifFile()
		{
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x0000EFC8 File Offset: 0x0000D1C8
		internal static GifFile ReadGifFile(Stream stream, bool metadataOnly)
		{
			GifFile gifFile = new GifFile();
			gifFile.Read(stream, metadataOnly);
			return gifFile;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x0004408C File Offset: 0x0004228C
		private void Read(Stream stream, bool metadataOnly)
		{
			this.Header = GifHeader.ReadHeader(stream);
			if (this.Header.LogicalScreenDescriptor.HasGlobalColorTable)
			{
				this.GlobalColorTable = GifHelpers.ReadColorTable(stream, this.Header.LogicalScreenDescriptor.GlobalColorTableSize);
			}
			this.ReadFrames(stream, metadataOnly);
			GifApplicationExtension gifApplicationExtension = this.Extensions.OfType<GifApplicationExtension>().FirstOrDefault(new Func<GifApplicationExtension, bool>(GifHelpers.IsNetscapeExtension));
			if (gifApplicationExtension != null)
			{
				this.RepeatCount = GifHelpers.GetRepeatCount(gifApplicationExtension);
				return;
			}
			this.RepeatCount = 1;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00044110 File Offset: 0x00042310
		private void ReadFrames(Stream stream, bool metadataOnly)
		{
			List<GifFrame> list = new List<GifFrame>();
			List<GifExtension> list2 = new List<GifExtension>();
			List<GifExtension> list3 = new List<GifExtension>();
			for (;;)
			{
				GifBlock gifBlock = GifBlock.ReadBlock(stream, list2, metadataOnly);
				if (gifBlock.Kind == GifBlockKind.GraphicRendering)
				{
					list2 = new List<GifExtension>();
				}
				if (gifBlock is GifFrame)
				{
					list.Add((GifFrame)gifBlock);
				}
				else
				{
					GifExtension gifExtension = gifBlock as GifExtension;
					if (gifExtension != null)
					{
						GifBlockKind kind = gifExtension.Kind;
						if (kind != GifBlockKind.Control)
						{
							if (kind == GifBlockKind.SpecialPurpose)
							{
								list3.Add(gifExtension);
							}
						}
						else
						{
							list2.Add(gifExtension);
						}
					}
					else if (gifBlock is GifTrailer)
					{
						break;
					}
				}
			}
			this.Frames = list.AsReadOnly();
			this.Extensions = list3.AsReadOnly();
		}
	}
}
