using System;
using System.Net;
using System.Runtime.Serialization;

namespace BlueStacks.Common
{
	// Token: 0x020001D9 RID: 473
	[Serializable]
	public class NonResumableStreamException : Exception
	{
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x0000D8BF File Offset: 0x0000BABF
		// (set) Token: 0x06000F7E RID: 3966 RVA: 0x0000D8C7 File Offset: 0x0000BAC7
		public int ErrorCode { get; set; } = 5;

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x0000D8D0 File Offset: 0x0000BAD0
		// (set) Token: 0x06000F80 RID: 3968 RVA: 0x0000D8D8 File Offset: 0x0000BAD8
		public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x0000D8E1 File Offset: 0x0000BAE1
		public override string Message
		{
			get
			{
				return "The remote server does not support partial content stream.";
			}
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0000D8E8 File Offset: 0x0000BAE8
		public NonResumableStreamException(HttpStatusCode statusCode)
		{
			this.StatusCode = statusCode;
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0000D909 File Offset: 0x0000BB09
		public NonResumableStreamException()
		{
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0000D923 File Offset: 0x0000BB23
		public NonResumableStreamException(string message) : base(message)
		{
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0000D93E File Offset: 0x0000BB3E
		public NonResumableStreamException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0000D95A File Offset: 0x0000BB5A
		protected NonResumableStreamException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
