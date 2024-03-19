using System;
using System.Runtime.Serialization;

// Token: 0x02000015 RID: 21
[Serializable]
public class CheckFailedException : Exception
{
	// Token: 0x06000060 RID: 96 RVA: 0x00002418 File Offset: 0x00000618
	public CheckFailedException()
	{
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00002420 File Offset: 0x00000620
	public CheckFailedException(string message) : base(message)
	{
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00002429 File Offset: 0x00000629
	public CheckFailedException(string message, Exception innerException) : base(message, innerException)
	{
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00002433 File Offset: 0x00000633
	protected CheckFailedException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
	{
	}
}
