using System;
using System.Runtime.Serialization;

// Token: 0x02000007 RID: 7
[Serializable]
public class WorkerException : Exception
{
	// Token: 0x0600001A RID: 26 RVA: 0x00002F06 File Offset: 0x00001106
	public WorkerException(string msg, Exception e) : base(msg, e)
	{
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002F12 File Offset: 0x00001112
	public WorkerException()
	{
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00002F1C File Offset: 0x0000111C
	public WorkerException(string message) : base(message)
	{
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00002F27 File Offset: 0x00001127
	protected WorkerException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
	{
	}
}
