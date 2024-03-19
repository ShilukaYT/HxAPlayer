using System;
using System.Net;
using System.Runtime.Serialization;

namespace BlueStacks.Common
{
	// Token: 0x02000048 RID: 72
	[Serializable]
	public class NonResumableStreamException : Exception
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003F9F File Offset: 0x0000219F
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00003FA7 File Offset: 0x000021A7
		public int ErrorCode { get; set; } = 5;

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003FB0 File Offset: 0x000021B0
		// (set) Token: 0x0600006B RID: 107 RVA: 0x00003FB8 File Offset: 0x000021B8
		public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003FC1 File Offset: 0x000021C1
		public override string Message
		{
			get
			{
				return "The remote server does not support partial content stream.";
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003FC8 File Offset: 0x000021C8
		public NonResumableStreamException(HttpStatusCode statusCode)
		{
			this.StatusCode = statusCode;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003FEC File Offset: 0x000021EC
		public NonResumableStreamException()
		{
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004008 File Offset: 0x00002208
		public NonResumableStreamException(string message) : base(message)
		{
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004025 File Offset: 0x00002225
		public NonResumableStreamException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004043 File Offset: 0x00002243
		protected NonResumableStreamException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
