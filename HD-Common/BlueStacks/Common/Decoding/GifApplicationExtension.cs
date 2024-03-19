using System;
using System.IO;
using System.Text;

namespace BlueStacks.Common.Decoding
{
	// Token: 0x02000249 RID: 585
	internal class GifApplicationExtension : GifExtension
	{
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x0000EED8 File Offset: 0x0000D0D8
		// (set) Token: 0x060011FC RID: 4604 RVA: 0x0000EEE0 File Offset: 0x0000D0E0
		public int BlockSize { get; private set; }

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x0000EEE9 File Offset: 0x0000D0E9
		// (set) Token: 0x060011FE RID: 4606 RVA: 0x0000EEF1 File Offset: 0x0000D0F1
		public string ApplicationIdentifier { get; private set; }

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x0000EEFA File Offset: 0x0000D0FA
		// (set) Token: 0x06001200 RID: 4608 RVA: 0x0000EF02 File Offset: 0x0000D102
		public byte[] AuthenticationCode { get; private set; }

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x0000EF0B File Offset: 0x0000D10B
		// (set) Token: 0x06001202 RID: 4610 RVA: 0x0000EF13 File Offset: 0x0000D113
		public byte[] Data { get; private set; }

		// Token: 0x06001203 RID: 4611 RVA: 0x0000EF1C File Offset: 0x0000D11C
		private GifApplicationExtension()
		{
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x0000EF24 File Offset: 0x0000D124
		internal override GifBlockKind Kind
		{
			get
			{
				return GifBlockKind.SpecialPurpose;
			}
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0000EF27 File Offset: 0x0000D127
		internal static GifApplicationExtension ReadApplication(Stream stream)
		{
			GifApplicationExtension gifApplicationExtension = new GifApplicationExtension();
			gifApplicationExtension.Read(stream);
			return gifApplicationExtension;
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00043ED8 File Offset: 0x000420D8
		private void Read(Stream stream)
		{
			byte[] array = new byte[12];
			stream.ReadAll(array, 0, array.Length);
			this.BlockSize = (int)array[0];
			if (this.BlockSize != 11)
			{
				throw GifHelpers.InvalidBlockSizeException("Application Extension", 11, this.BlockSize);
			}
			this.ApplicationIdentifier = Encoding.ASCII.GetString(array, 1, 8);
			byte[] array2 = new byte[3];
			Array.Copy(array, 9, array2, 0, 3);
			this.AuthenticationCode = array2;
			this.Data = GifHelpers.ReadDataBlocks(stream, false);
		}

		// Token: 0x04000E41 RID: 3649
		internal const int ExtensionLabel = 255;
	}
}
