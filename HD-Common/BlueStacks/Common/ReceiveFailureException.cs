using System;
using System.Runtime.Serialization;

namespace BlueStacks.Common
{
	// Token: 0x020001DB RID: 475
	[Serializable]
	public class ReceiveFailureException : Exception
	{
		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0000DA2D File Offset: 0x0000BC2D
		// (set) Token: 0x06000F92 RID: 3986 RVA: 0x0000DA35 File Offset: 0x0000BC35
		public int ErrorCode { get; set; } = 2;

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x0000DA3E File Offset: 0x0000BC3E
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x0000DA46 File Offset: 0x0000BC46
		public long BytesRecieved { get; private set; }

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0000DA4F File Offset: 0x0000BC4F
		public override string Message
		{
			get
			{
				return "A complete response was not received from the remote server.";
			}
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0000DA56 File Offset: 0x0000BC56
		public ReceiveFailureException(long bytesRecieved, Exception innerException) : base("", innerException)
		{
			this.BytesRecieved = bytesRecieved;
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0000DA72 File Offset: 0x0000BC72
		public ReceiveFailureException()
		{
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0000DA81 File Offset: 0x0000BC81
		public ReceiveFailureException(string message) : base(message)
		{
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0000DA91 File Offset: 0x0000BC91
		public ReceiveFailureException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0000DAA2 File Offset: 0x0000BCA2
		protected ReceiveFailureException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
