using System;
using System.Net;

namespace BlueStacks.Common
{
	// Token: 0x02000196 RID: 406
	public class PayloadInfo
	{
		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x0000D68F File Offset: 0x0000B88F
		// (set) Token: 0x06000F21 RID: 3873 RVA: 0x0000D697 File Offset: 0x0000B897
		public HttpStatusCode StatusCode { get; private set; }

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x0000D6A0 File Offset: 0x0000B8A0
		// (set) Token: 0x06000F23 RID: 3875 RVA: 0x0000D6A8 File Offset: 0x0000B8A8
		public bool SupportsRangeRequest { get; set; }

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0000D6B1 File Offset: 0x0000B8B1
		// (set) Token: 0x06000F25 RID: 3877 RVA: 0x0000D6B9 File Offset: 0x0000B8B9
		public long Size { get; set; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x0000D6C2 File Offset: 0x0000B8C2
		// (set) Token: 0x06000F27 RID: 3879 RVA: 0x0000D6CA File Offset: 0x0000B8CA
		public long FileSize { get; set; }

		// Token: 0x06000F28 RID: 3880 RVA: 0x0000D6D3 File Offset: 0x0000B8D3
		public PayloadInfo(HttpStatusCode statusCode, long size, long fileSize = 0L, bool supportsRangeRequest = false)
		{
			this.SupportsRangeRequest = supportsRangeRequest;
			this.Size = size;
			this.StatusCode = statusCode;
			this.FileSize = ((fileSize == 0L) ? size : fileSize);
		}
	}
}
