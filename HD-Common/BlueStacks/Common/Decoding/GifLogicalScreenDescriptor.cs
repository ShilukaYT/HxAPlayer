using System;
using System.IO;

namespace BlueStacks.Common.Decoding
{
	// Token: 0x02000257 RID: 599
	internal class GifLogicalScreenDescriptor
	{
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x0000F2B7 File Offset: 0x0000D4B7
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x0000F2BF File Offset: 0x0000D4BF
		public int Width { get; private set; }

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x0000F2C8 File Offset: 0x0000D4C8
		// (set) Token: 0x06001275 RID: 4725 RVA: 0x0000F2D0 File Offset: 0x0000D4D0
		public int Height { get; private set; }

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x0000F2D9 File Offset: 0x0000D4D9
		// (set) Token: 0x06001277 RID: 4727 RVA: 0x0000F2E1 File Offset: 0x0000D4E1
		public bool HasGlobalColorTable { get; private set; }

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x0000F2EA File Offset: 0x0000D4EA
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x0000F2F2 File Offset: 0x0000D4F2
		public int ColorResolution { get; private set; }

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x0000F2FB File Offset: 0x0000D4FB
		// (set) Token: 0x0600127B RID: 4731 RVA: 0x0000F303 File Offset: 0x0000D503
		public bool IsGlobalColorTableSorted { get; private set; }

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x0600127C RID: 4732 RVA: 0x0000F30C File Offset: 0x0000D50C
		// (set) Token: 0x0600127D RID: 4733 RVA: 0x0000F314 File Offset: 0x0000D514
		public int GlobalColorTableSize { get; private set; }

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600127E RID: 4734 RVA: 0x0000F31D File Offset: 0x0000D51D
		// (set) Token: 0x0600127F RID: 4735 RVA: 0x0000F325 File Offset: 0x0000D525
		public int BackgroundColorIndex { get; private set; }

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0000F32E File Offset: 0x0000D52E
		// (set) Token: 0x06001281 RID: 4737 RVA: 0x0000F336 File Offset: 0x0000D536
		public double PixelAspectRatio { get; private set; }

		// Token: 0x06001282 RID: 4738 RVA: 0x0000F33F File Offset: 0x0000D53F
		internal static GifLogicalScreenDescriptor ReadLogicalScreenDescriptor(Stream stream)
		{
			GifLogicalScreenDescriptor gifLogicalScreenDescriptor = new GifLogicalScreenDescriptor();
			gifLogicalScreenDescriptor.Read(stream);
			return gifLogicalScreenDescriptor;
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x000444D0 File Offset: 0x000426D0
		private void Read(Stream stream)
		{
			byte[] array = new byte[7];
			stream.ReadAll(array, 0, array.Length);
			this.Width = (int)BitConverter.ToUInt16(array, 0);
			this.Height = (int)BitConverter.ToUInt16(array, 2);
			byte b = array[4];
			this.HasGlobalColorTable = ((b & 128) > 0);
			this.ColorResolution = ((b & 112) >> 4) + 1;
			this.IsGlobalColorTableSorted = ((b & 8) > 0);
			this.GlobalColorTableSize = 1 << (int)((b & 7) + 1);
			this.BackgroundColorIndex = (int)array[5];
			this.PixelAspectRatio = ((array[5] == 0) ? 0.0 : ((double)(15 + array[5]) / 64.0));
		}
	}
}
