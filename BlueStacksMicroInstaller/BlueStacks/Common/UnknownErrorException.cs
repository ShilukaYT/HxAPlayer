using System;
using System.Runtime.Serialization;

namespace BlueStacks.Common
{
	// Token: 0x0200004C RID: 76
	[Serializable]
	public class UnknownErrorException : Exception
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000423D File Offset: 0x0000243D
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00004245 File Offset: 0x00002445
		public int ErrorCode { get; set; } = 99;

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000092 RID: 146 RVA: 0x0000424E File Offset: 0x0000244E
		public override string Message
		{
			get
			{
				return "An exception of an unknown type has occurred.";
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004255 File Offset: 0x00002455
		public UnknownErrorException(Exception innerException) : base("", innerException)
		{
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000426D File Offset: 0x0000246D
		public UnknownErrorException()
		{
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000427F File Offset: 0x0000247F
		public UnknownErrorException(string message) : base(message)
		{
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004292 File Offset: 0x00002492
		public UnknownErrorException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000042A6 File Offset: 0x000024A6
		protected UnknownErrorException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
