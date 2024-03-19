using System;
using System.IO;

namespace BlueStacks.Common.Decoding
{
	// Token: 0x02000256 RID: 598
	internal class GifImageDescriptor
	{
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x0000F221 File Offset: 0x0000D421
		// (set) Token: 0x06001260 RID: 4704 RVA: 0x0000F229 File Offset: 0x0000D429
		public int Left { get; private set; }

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x0000F232 File Offset: 0x0000D432
		// (set) Token: 0x06001262 RID: 4706 RVA: 0x0000F23A File Offset: 0x0000D43A
		public int Top { get; private set; }

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x0000F243 File Offset: 0x0000D443
		// (set) Token: 0x06001264 RID: 4708 RVA: 0x0000F24B File Offset: 0x0000D44B
		public int Width { get; private set; }

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x0000F254 File Offset: 0x0000D454
		// (set) Token: 0x06001266 RID: 4710 RVA: 0x0000F25C File Offset: 0x0000D45C
		public int Height { get; private set; }

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x0000F265 File Offset: 0x0000D465
		// (set) Token: 0x06001268 RID: 4712 RVA: 0x0000F26D File Offset: 0x0000D46D
		public bool HasLocalColorTable { get; private set; }

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x0000F276 File Offset: 0x0000D476
		// (set) Token: 0x0600126A RID: 4714 RVA: 0x0000F27E File Offset: 0x0000D47E
		public bool Interlace { get; private set; }

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x0000F287 File Offset: 0x0000D487
		// (set) Token: 0x0600126C RID: 4716 RVA: 0x0000F28F File Offset: 0x0000D48F
		public bool IsLocalColorTableSorted { get; private set; }

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x0000F298 File Offset: 0x0000D498
		// (set) Token: 0x0600126E RID: 4718 RVA: 0x0000F2A0 File Offset: 0x0000D4A0
		public int LocalColorTableSize { get; private set; }

		// Token: 0x0600126F RID: 4719 RVA: 0x0000225B File Offset: 0x0000045B
		private GifImageDescriptor()
		{
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0000F2A9 File Offset: 0x0000D4A9
		internal static GifImageDescriptor ReadImageDescriptor(Stream stream)
		{
			GifImageDescriptor gifImageDescriptor = new GifImageDescriptor();
			gifImageDescriptor.Read(stream);
			return gifImageDescriptor;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0004443C File Offset: 0x0004263C
		private void Read(Stream stream)
		{
			byte[] array = new byte[9];
			stream.ReadAll(array, 0, array.Length);
			this.Left = (int)BitConverter.ToUInt16(array, 0);
			this.Top = (int)BitConverter.ToUInt16(array, 2);
			this.Width = (int)BitConverter.ToUInt16(array, 4);
			this.Height = (int)BitConverter.ToUInt16(array, 6);
			byte b = array[8];
			this.HasLocalColorTable = ((b & 128) > 0);
			this.Interlace = ((b & 64) > 0);
			this.IsLocalColorTableSorted = ((b & 32) > 0);
			this.LocalColorTableSize = 1 << (int)((b & 7) + 1);
		}
	}
}
