using System;
using System.Net;
using System.Runtime.Serialization;

namespace BlueStacks.Common
{
	// Token: 0x020001DA RID: 474
	[Serializable]
	public class UnexpectedHttpStatusCodeException : Exception
	{
		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x0000D976 File Offset: 0x0000BB76
		// (set) Token: 0x06000F88 RID: 3976 RVA: 0x0000D97E File Offset: 0x0000BB7E
		public int ErrorCode { get; set; } = 4;

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x0000D987 File Offset: 0x0000BB87
		// (set) Token: 0x06000F8A RID: 3978 RVA: 0x0000D98F File Offset: 0x0000BB8F
		public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x0000D998 File Offset: 0x0000BB98
		public override string Message
		{
			get
			{
				return "The remote server returned an unexpected status code.";
			}
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0000D99F File Offset: 0x0000BB9F
		public UnexpectedHttpStatusCodeException(HttpStatusCode statusCode)
		{
			this.StatusCode = statusCode;
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0000D9C0 File Offset: 0x0000BBC0
		public UnexpectedHttpStatusCodeException()
		{
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0000D9DA File Offset: 0x0000BBDA
		public UnexpectedHttpStatusCodeException(string message) : base(message)
		{
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0000D9F5 File Offset: 0x0000BBF5
		public UnexpectedHttpStatusCodeException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0000DA11 File Offset: 0x0000BC11
		protected UnexpectedHttpStatusCodeException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
