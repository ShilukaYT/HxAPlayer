using System;
using System.Net;

namespace BlueStacks.Common
{
	// Token: 0x0200000D RID: 13
	public class PayloadInfo
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00003294 File Offset: 0x00001494
		// (set) Token: 0x0600002C RID: 44 RVA: 0x0000329C File Offset: 0x0000149C
		public HttpStatusCode StatusCode { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000032A5 File Offset: 0x000014A5
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000032AD File Offset: 0x000014AD
		public bool SupportsRangeRequest { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000032B6 File Offset: 0x000014B6
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000032BE File Offset: 0x000014BE
		public long Size { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000032C7 File Offset: 0x000014C7
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000032CF File Offset: 0x000014CF
		public long FileSize { get; set; }

		// Token: 0x06000033 RID: 51 RVA: 0x000032D8 File Offset: 0x000014D8
		public PayloadInfo(HttpStatusCode statusCode, long size, long fileSize = 0L, bool supportsRangeRequest = false)
		{
			this.SupportsRangeRequest = supportsRangeRequest;
			this.Size = size;
			this.StatusCode = statusCode;
			this.FileSize = ((fileSize == 0L) ? size : fileSize);
		}
	}
}
