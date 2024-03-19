using System;

// Token: 0x02000019 RID: 25
public class PayloadInfo
{
	// Token: 0x0600006F RID: 111 RVA: 0x00002475 File Offset: 0x00000675
	public PayloadInfo(bool supportsRangeRequest, long size, bool invalidStatusCode = false)
	{
		this.SupportsRangeRequest = supportsRangeRequest;
		this.Size = size;
		this.InvalidHTTPStatusCode = invalidStatusCode;
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x06000070 RID: 112 RVA: 0x00002492 File Offset: 0x00000692
	public long Size { get; }

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x06000071 RID: 113 RVA: 0x0000249A File Offset: 0x0000069A
	public bool SupportsRangeRequest { get; }

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x06000072 RID: 114 RVA: 0x000024A2 File Offset: 0x000006A2
	public bool InvalidHTTPStatusCode { get; }
}
