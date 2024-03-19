using System;
using System.IO;

namespace BlueStacks.Common.Decoding
{
	// Token: 0x02000255 RID: 597
	internal class GifImageData
	{
		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x0000F1D4 File Offset: 0x0000D3D4
		// (set) Token: 0x06001259 RID: 4697 RVA: 0x0000F1DC File Offset: 0x0000D3DC
		public byte LzwMinimumCodeSize { get; set; }

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x0000F1E5 File Offset: 0x0000D3E5
		// (set) Token: 0x0600125B RID: 4699 RVA: 0x0000F1ED File Offset: 0x0000D3ED
		public byte[] CompressedData { get; set; }

		// Token: 0x0600125C RID: 4700 RVA: 0x0000225B File Offset: 0x0000045B
		private GifImageData()
		{
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0000F1F6 File Offset: 0x0000D3F6
		internal static GifImageData ReadImageData(Stream stream, bool metadataOnly)
		{
			GifImageData gifImageData = new GifImageData();
			gifImageData.Read(stream, metadataOnly);
			return gifImageData;
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0000F205 File Offset: 0x0000D405
		private void Read(Stream stream, bool metadataOnly)
		{
			this.LzwMinimumCodeSize = (byte)stream.ReadByte();
			this.CompressedData = GifHelpers.ReadDataBlocks(stream, metadataOnly);
		}
	}
}
