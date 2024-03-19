using System;
using System.Net;
using System.Runtime.Serialization;

namespace BlueStacks.Common
{
	// Token: 0x02000049 RID: 73
	[Serializable]
	public class UnexpectedHttpStatusCodeException : Exception
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00004061 File Offset: 0x00002261
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00004069 File Offset: 0x00002269
		public int ErrorCode { get; set; } = 4;

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00004072 File Offset: 0x00002272
		// (set) Token: 0x06000075 RID: 117 RVA: 0x0000407A File Offset: 0x0000227A
		public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00004083 File Offset: 0x00002283
		public override string Message
		{
			get
			{
				return "The remote server returned an unexpected status code.";
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000408A File Offset: 0x0000228A
		public UnexpectedHttpStatusCodeException(HttpStatusCode statusCode)
		{
			this.StatusCode = statusCode;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000040AE File Offset: 0x000022AE
		public UnexpectedHttpStatusCodeException()
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000040CA File Offset: 0x000022CA
		public UnexpectedHttpStatusCodeException(string message) : base(message)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000040E7 File Offset: 0x000022E7
		public UnexpectedHttpStatusCodeException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00004105 File Offset: 0x00002305
		protected UnexpectedHttpStatusCodeException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
