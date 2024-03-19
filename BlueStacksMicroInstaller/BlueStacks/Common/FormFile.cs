using System;
using System.IO;

namespace BlueStacks.Common
{
	// Token: 0x02000059 RID: 89
	public class FormFile
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000054C4 File Offset: 0x000036C4
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x000054CC File Offset: 0x000036CC
		public string Name { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x000054D5 File Offset: 0x000036D5
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x000054DD File Offset: 0x000036DD
		public string ContentType { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000054E6 File Offset: 0x000036E6
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x000054EE File Offset: 0x000036EE
		public string FilePath { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x000054F7 File Offset: 0x000036F7
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x000054FF File Offset: 0x000036FF
		public Stream Stream { get; set; }
	}
}
