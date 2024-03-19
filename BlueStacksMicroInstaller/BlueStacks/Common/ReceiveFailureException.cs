using System;
using System.Runtime.Serialization;

namespace BlueStacks.Common
{
	// Token: 0x0200004A RID: 74
	[Serializable]
	public class ReceiveFailureException : Exception
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00004123 File Offset: 0x00002323
		// (set) Token: 0x0600007D RID: 125 RVA: 0x0000412B File Offset: 0x0000232B
		public int ErrorCode { get; set; } = 2;

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00004134 File Offset: 0x00002334
		// (set) Token: 0x0600007F RID: 127 RVA: 0x0000413C File Offset: 0x0000233C
		public long BytesRecieved { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00004145 File Offset: 0x00002345
		public override string Message
		{
			get
			{
				return "A complete response was not received from the remote server.";
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000414C File Offset: 0x0000234C
		public ReceiveFailureException(long bytesRecieved, Exception innerException) : base("", innerException)
		{
			this.BytesRecieved = bytesRecieved;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000416B File Offset: 0x0000236B
		public ReceiveFailureException()
		{
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000417C File Offset: 0x0000237C
		public ReceiveFailureException(string message) : base(message)
		{
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000418E File Offset: 0x0000238E
		public ReceiveFailureException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000041A1 File Offset: 0x000023A1
		protected ReceiveFailureException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
