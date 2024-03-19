using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BlueStacks.Common.Decoding
{
	// Token: 0x02000258 RID: 600
	internal class GifPlainTextExtension : GifExtension
	{
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x0000F34D File Offset: 0x0000D54D
		// (set) Token: 0x06001286 RID: 4742 RVA: 0x0000F355 File Offset: 0x0000D555
		public int BlockSize { get; private set; }

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x0000F35E File Offset: 0x0000D55E
		// (set) Token: 0x06001288 RID: 4744 RVA: 0x0000F366 File Offset: 0x0000D566
		public int Left { get; private set; }

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x0000F36F File Offset: 0x0000D56F
		// (set) Token: 0x0600128A RID: 4746 RVA: 0x0000F377 File Offset: 0x0000D577
		public int Top { get; private set; }

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x0000F380 File Offset: 0x0000D580
		// (set) Token: 0x0600128C RID: 4748 RVA: 0x0000F388 File Offset: 0x0000D588
		public int Width { get; private set; }

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x0000F391 File Offset: 0x0000D591
		// (set) Token: 0x0600128E RID: 4750 RVA: 0x0000F399 File Offset: 0x0000D599
		public int Height { get; private set; }

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600128F RID: 4751 RVA: 0x0000F3A2 File Offset: 0x0000D5A2
		// (set) Token: 0x06001290 RID: 4752 RVA: 0x0000F3AA File Offset: 0x0000D5AA
		public int CellWidth { get; private set; }

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x0000F3B3 File Offset: 0x0000D5B3
		// (set) Token: 0x06001292 RID: 4754 RVA: 0x0000F3BB File Offset: 0x0000D5BB
		public int CellHeight { get; private set; }

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x0000F3C4 File Offset: 0x0000D5C4
		// (set) Token: 0x06001294 RID: 4756 RVA: 0x0000F3CC File Offset: 0x0000D5CC
		public int ForegroundColorIndex { get; private set; }

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x0000F3D5 File Offset: 0x0000D5D5
		// (set) Token: 0x06001296 RID: 4758 RVA: 0x0000F3DD File Offset: 0x0000D5DD
		public int BackgroundColorIndex { get; private set; }

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x0000F3E6 File Offset: 0x0000D5E6
		// (set) Token: 0x06001298 RID: 4760 RVA: 0x0000F3EE File Offset: 0x0000D5EE
		public string Text { get; private set; }

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x0000F3F7 File Offset: 0x0000D5F7
		// (set) Token: 0x0600129A RID: 4762 RVA: 0x0000F3FF File Offset: 0x0000D5FF
		public IList<GifExtension> Extensions { get; private set; }

		// Token: 0x0600129B RID: 4763 RVA: 0x0000EF1C File Offset: 0x0000D11C
		private GifPlainTextExtension()
		{
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x0000346F File Offset: 0x0000166F
		internal override GifBlockKind Kind
		{
			get
			{
				return GifBlockKind.GraphicRendering;
			}
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0000F408 File Offset: 0x0000D608
		internal static GifPlainTextExtension ReadPlainText(Stream stream, IEnumerable<GifExtension> controlExtensions, bool metadataOnly)
		{
			GifPlainTextExtension gifPlainTextExtension = new GifPlainTextExtension();
			gifPlainTextExtension.Read(stream, controlExtensions, metadataOnly);
			return gifPlainTextExtension;
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00044578 File Offset: 0x00042778
		private void Read(Stream stream, IEnumerable<GifExtension> controlExtensions, bool metadataOnly)
		{
			byte[] array = new byte[13];
			stream.ReadAll(array, 0, array.Length);
			this.BlockSize = (int)array[0];
			if (this.BlockSize != 12)
			{
				throw GifHelpers.InvalidBlockSizeException("Plain Text Extension", 12, this.BlockSize);
			}
			this.Left = (int)BitConverter.ToUInt16(array, 1);
			this.Top = (int)BitConverter.ToUInt16(array, 3);
			this.Width = (int)BitConverter.ToUInt16(array, 5);
			this.Height = (int)BitConverter.ToUInt16(array, 7);
			this.CellWidth = (int)array[9];
			this.CellHeight = (int)array[10];
			this.ForegroundColorIndex = (int)array[11];
			this.BackgroundColorIndex = (int)array[12];
			byte[] bytes = GifHelpers.ReadDataBlocks(stream, metadataOnly);
			this.Text = Encoding.ASCII.GetString(bytes);
			this.Extensions = controlExtensions.ToList<GifExtension>().AsReadOnly();
		}

		// Token: 0x04000E77 RID: 3703
		internal const int ExtensionLabel = 1;
	}
}
