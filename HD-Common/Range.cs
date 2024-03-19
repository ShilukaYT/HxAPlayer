using System;

// Token: 0x02000018 RID: 24
public class Range
{
	// Token: 0x0600006B RID: 107 RVA: 0x0000243D File Offset: 0x0000063D
	public Range(long from, long to)
	{
		this.From = from;
		this.To = to;
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x0600006C RID: 108 RVA: 0x00002453 File Offset: 0x00000653
	public long From { get; }

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x0600006D RID: 109 RVA: 0x0000245B File Offset: 0x0000065B
	public long To { get; }

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x0600006E RID: 110 RVA: 0x00002463 File Offset: 0x00000663
	public long Length
	{
		get
		{
			return this.To - this.From + 1L;
		}
	}
}
