using System;
using System.IO;

namespace BlueStacks.Common.Decoding
{
	// Token: 0x02000252 RID: 594
	internal class GifGraphicControlExtension : GifExtension
	{
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x0000F02B File Offset: 0x0000D22B
		// (set) Token: 0x06001233 RID: 4659 RVA: 0x0000F033 File Offset: 0x0000D233
		public int BlockSize { get; private set; }

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x0000F03C File Offset: 0x0000D23C
		// (set) Token: 0x06001235 RID: 4661 RVA: 0x0000F044 File Offset: 0x0000D244
		public int DisposalMethod { get; private set; }

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x0000F04D File Offset: 0x0000D24D
		// (set) Token: 0x06001237 RID: 4663 RVA: 0x0000F055 File Offset: 0x0000D255
		public bool UserInput { get; private set; }

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x0000F05E File Offset: 0x0000D25E
		// (set) Token: 0x06001239 RID: 4665 RVA: 0x0000F066 File Offset: 0x0000D266
		public bool HasTransparency { get; private set; }

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x0000F06F File Offset: 0x0000D26F
		// (set) Token: 0x0600123B RID: 4667 RVA: 0x0000F077 File Offset: 0x0000D277
		public int Delay { get; private set; }

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x0000F080 File Offset: 0x0000D280
		// (set) Token: 0x0600123D RID: 4669 RVA: 0x0000F088 File Offset: 0x0000D288
		public int TransparencyIndex { get; private set; }

		// Token: 0x0600123E RID: 4670 RVA: 0x0000EF1C File Offset: 0x0000D11C
		private GifGraphicControlExtension()
		{
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x00006E92 File Offset: 0x00005092
		internal override GifBlockKind Kind
		{
			get
			{
				return GifBlockKind.Control;
			}
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0000F091 File Offset: 0x0000D291
		internal static GifGraphicControlExtension ReadGraphicsControl(Stream stream)
		{
			GifGraphicControlExtension gifGraphicControlExtension = new GifGraphicControlExtension();
			gifGraphicControlExtension.Read(stream);
			return gifGraphicControlExtension;
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00044210 File Offset: 0x00042410
		private void Read(Stream stream)
		{
			byte[] array = new byte[6];
			stream.ReadAll(array, 0, array.Length);
			this.BlockSize = (int)array[0];
			if (this.BlockSize != 4)
			{
				throw GifHelpers.InvalidBlockSizeException("Graphic Control Extension", 4, this.BlockSize);
			}
			byte b = array[1];
			this.DisposalMethod = (b & 28) >> 2;
			this.UserInput = ((b & 2) > 0);
			this.HasTransparency = ((b & 1) > 0);
			this.Delay = (int)(BitConverter.ToUInt16(array, 2) * 10);
			this.TransparencyIndex = (int)array[4];
		}

		// Token: 0x04000E5B RID: 3675
		internal const int ExtensionLabel = 249;
	}
}
