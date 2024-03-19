using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BlueStacks.Common.Decoding
{
	// Token: 0x02000251 RID: 593
	internal class GifFrame : GifBlock
	{
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x0000EFD7 File Offset: 0x0000D1D7
		// (set) Token: 0x06001227 RID: 4647 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public GifImageDescriptor Descriptor { get; private set; }

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x0000EFE8 File Offset: 0x0000D1E8
		// (set) Token: 0x06001229 RID: 4649 RVA: 0x0000EFF0 File Offset: 0x0000D1F0
		public GifColor[] LocalColorTable { get; private set; }

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x0000EFF9 File Offset: 0x0000D1F9
		// (set) Token: 0x0600122B RID: 4651 RVA: 0x0000F001 File Offset: 0x0000D201
		public IList<GifExtension> Extensions { get; private set; }

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x0000F00A File Offset: 0x0000D20A
		// (set) Token: 0x0600122D RID: 4653 RVA: 0x0000F012 File Offset: 0x0000D212
		public GifImageData ImageData { get; private set; }

		// Token: 0x0600122E RID: 4654 RVA: 0x0000EF6B File Offset: 0x0000D16B
		private GifFrame()
		{
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x0000346F File Offset: 0x0000166F
		internal override GifBlockKind Kind
		{
			get
			{
				return GifBlockKind.GraphicRendering;
			}
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0000F01B File Offset: 0x0000D21B
		internal static GifFrame ReadFrame(Stream stream, IEnumerable<GifExtension> controlExtensions, bool metadataOnly)
		{
			GifFrame gifFrame = new GifFrame();
			gifFrame.Read(stream, controlExtensions, metadataOnly);
			return gifFrame;
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x000441B4 File Offset: 0x000423B4
		private void Read(Stream stream, IEnumerable<GifExtension> controlExtensions, bool metadataOnly)
		{
			this.Descriptor = GifImageDescriptor.ReadImageDescriptor(stream);
			if (this.Descriptor.HasLocalColorTable)
			{
				this.LocalColorTable = GifHelpers.ReadColorTable(stream, this.Descriptor.LocalColorTableSize);
			}
			this.ImageData = GifImageData.ReadImageData(stream, metadataOnly);
			this.Extensions = controlExtensions.ToList<GifExtension>().AsReadOnly();
		}

		// Token: 0x04000E56 RID: 3670
		internal const int ImageSeparator = 44;
	}
}
