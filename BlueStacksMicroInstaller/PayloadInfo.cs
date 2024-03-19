using System;

// Token: 0x02000005 RID: 5
public class PayloadInfo
{
	// Token: 0x06000007 RID: 7 RVA: 0x00002093 File Offset: 0x00000293
	public PayloadInfo(bool supportsRangeRequest, long size, bool invalidStatusCode = false)
	{
		this.SupportsRangeRequest = supportsRangeRequest;
		this.Size = size;
		this.InvalidHTTPStatusCode = invalidStatusCode;
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000008 RID: 8 RVA: 0x000020B2 File Offset: 0x000002B2
	public long Size { get; }

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000009 RID: 9 RVA: 0x000020BA File Offset: 0x000002BA
	public bool SupportsRangeRequest { get; }

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x0600000A RID: 10 RVA: 0x000020C2 File Offset: 0x000002C2
	public bool InvalidHTTPStatusCode { get; }
}
