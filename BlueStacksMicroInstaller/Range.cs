using System;

// Token: 0x02000004 RID: 4
public class Range
{
	// Token: 0x06000003 RID: 3 RVA: 0x00002059 File Offset: 0x00000259
	public Range(long from, long to)
	{
		this.From = from;
		this.To = to;
	}

	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000004 RID: 4 RVA: 0x00002071 File Offset: 0x00000271
	public long From { get; }

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000005 RID: 5 RVA: 0x00002079 File Offset: 0x00000279
	public long To { get; }

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000006 RID: 6 RVA: 0x00002081 File Offset: 0x00000281
	public long Length
	{
		get
		{
			return this.To - this.From + 1L;
		}
	}
}
