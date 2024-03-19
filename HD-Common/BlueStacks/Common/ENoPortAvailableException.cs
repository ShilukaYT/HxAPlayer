using System;
using System.Runtime.Serialization;

namespace BlueStacks.Common
{
	// Token: 0x020000C9 RID: 201
	[Serializable]
	public class ENoPortAvailableException : Exception
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x00002420 File Offset: 0x00000620
		public ENoPortAvailableException(string reason) : base(reason)
		{
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00002418 File Offset: 0x00000618
		public ENoPortAvailableException()
		{
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00002429 File Offset: 0x00000629
		public ENoPortAvailableException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00002433 File Offset: 0x00000633
		protected ENoPortAvailableException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
