using System;
using System.IO;

namespace BlueStacks.Common.Decoding
{
	// Token: 0x02000253 RID: 595
	internal class GifHeader : GifBlock
	{
		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x0000F09F File Offset: 0x0000D29F
		// (set) Token: 0x06001243 RID: 4675 RVA: 0x0000F0A7 File Offset: 0x0000D2A7
		public string Signature { get; private set; }

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x0000F0B0 File Offset: 0x0000D2B0
		// (set) Token: 0x06001245 RID: 4677 RVA: 0x0000F0B8 File Offset: 0x0000D2B8
		public string Version { get; private set; }

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x0000F0C1 File Offset: 0x0000D2C1
		// (set) Token: 0x06001247 RID: 4679 RVA: 0x0000F0C9 File Offset: 0x0000D2C9
		public GifLogicalScreenDescriptor LogicalScreenDescriptor { get; private set; }

		// Token: 0x06001248 RID: 4680 RVA: 0x0000EF6B File Offset: 0x0000D16B
		private GifHeader()
		{
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x0000F0D2 File Offset: 0x0000D2D2
		internal override GifBlockKind Kind
		{
			get
			{
				return GifBlockKind.Other;
			}
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x0000F0D5 File Offset: 0x0000D2D5
		internal static GifHeader ReadHeader(Stream stream)
		{
			GifHeader gifHeader = new GifHeader();
			gifHeader.Read(stream);
			return gifHeader;
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00044294 File Offset: 0x00042494
		private void Read(Stream stream)
		{
			this.Signature = GifHelpers.ReadString(stream, 3);
			if (this.Signature != "GIF")
			{
				throw GifHelpers.InvalidSignatureException(this.Signature);
			}
			this.Version = GifHelpers.ReadString(stream, 3);
			if (this.Version != "87a" && this.Version != "89a")
			{
				throw GifHelpers.UnsupportedVersionException(this.Version);
			}
			this.LogicalScreenDescriptor = GifLogicalScreenDescriptor.ReadLogicalScreenDescriptor(stream);
		}
	}
}
