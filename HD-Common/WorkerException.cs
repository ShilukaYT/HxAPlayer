using System;
using System.Runtime.Serialization;

// Token: 0x02000024 RID: 36
[Serializable]
public class WorkerException : Exception
{
	// Token: 0x060000AF RID: 175 RVA: 0x00002429 File Offset: 0x00000629
	public WorkerException(string msg, Exception e) : base(msg, e)
	{
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x00002418 File Offset: 0x00000618
	public WorkerException()
	{
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x00002420 File Offset: 0x00000620
	public WorkerException(string message) : base(message)
	{
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x00002433 File Offset: 0x00000633
	protected WorkerException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
	{
	}
}
