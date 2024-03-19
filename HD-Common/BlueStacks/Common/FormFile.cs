using System;
using System.IO;

namespace BlueStacks.Common
{
	// Token: 0x020001EB RID: 491
	public class FormFile
	{
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x0000DDC0 File Offset: 0x0000BFC0
		// (set) Token: 0x06000FE8 RID: 4072 RVA: 0x0000DDC8 File Offset: 0x0000BFC8
		public string Name { get; set; }

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x0000DDD1 File Offset: 0x0000BFD1
		// (set) Token: 0x06000FEA RID: 4074 RVA: 0x0000DDD9 File Offset: 0x0000BFD9
		public string ContentType { get; set; }

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x0000DDE2 File Offset: 0x0000BFE2
		// (set) Token: 0x06000FEC RID: 4076 RVA: 0x0000DDEA File Offset: 0x0000BFEA
		public string FilePath { get; set; }

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x0000DDF3 File Offset: 0x0000BFF3
		// (set) Token: 0x06000FEE RID: 4078 RVA: 0x0000DDFB File Offset: 0x0000BFFB
		public Stream Stream { get; set; }
	}
}
