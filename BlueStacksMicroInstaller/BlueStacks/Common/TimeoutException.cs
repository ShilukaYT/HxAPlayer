using System;
using System.Runtime.Serialization;

namespace BlueStacks.Common
{
	// Token: 0x0200004D RID: 77
	[Serializable]
	public class TimeoutException : Exception
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000042BA File Offset: 0x000024BA
		// (set) Token: 0x06000099 RID: 153 RVA: 0x000042C2 File Offset: 0x000024C2
		public int ErrorCode { get; set; } = 3;

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000042CB File Offset: 0x000024CB
		public override string Message
		{
			get
			{
				return "No response was received during the time-out period for the request.";
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000042D2 File Offset: 0x000024D2
		public TimeoutException()
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000042E3 File Offset: 0x000024E3
		public TimeoutException(string message) : base(message)
		{
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000042F5 File Offset: 0x000024F5
		public TimeoutException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004308 File Offset: 0x00002508
		protected TimeoutException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
