using System;
using System.IO;
using System.Text;

namespace BlueStacks.Common.Decoding
{
	// Token: 0x0200024D RID: 589
	internal class GifCommentExtension : GifExtension
	{
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x0000EF4C File Offset: 0x0000D14C
		// (set) Token: 0x0600120D RID: 4621 RVA: 0x0000EF54 File Offset: 0x0000D154
		public string Text { get; private set; }

		// Token: 0x0600120E RID: 4622 RVA: 0x0000EF1C File Offset: 0x0000D11C
		private GifCommentExtension()
		{
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x0000EF24 File Offset: 0x0000D124
		internal override GifBlockKind Kind
		{
			get
			{
				return GifBlockKind.SpecialPurpose;
			}
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0000EF5D File Offset: 0x0000D15D
		internal static GifCommentExtension ReadComment(Stream stream)
		{
			GifCommentExtension gifCommentExtension = new GifCommentExtension();
			gifCommentExtension.Read(stream);
			return gifCommentExtension;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00043FF4 File Offset: 0x000421F4
		private void Read(Stream stream)
		{
			byte[] array = GifHelpers.ReadDataBlocks(stream, false);
			if (array != null)
			{
				this.Text = Encoding.ASCII.GetString(array);
			}
		}

		// Token: 0x04000E4E RID: 3662
		internal const int ExtensionLabel = 254;
	}
}
