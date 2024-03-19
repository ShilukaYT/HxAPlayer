using System;
using System.Runtime.Serialization;

namespace BlueStacks.Common
{
	// Token: 0x020001DE RID: 478
	[Serializable]
	public class TimeoutException : Exception
	{
		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x0000DBA5 File Offset: 0x0000BDA5
		// (set) Token: 0x06000FAE RID: 4014 RVA: 0x0000DBAD File Offset: 0x0000BDAD
		public int ErrorCode { get; set; } = 3;

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x0000DBB6 File Offset: 0x0000BDB6
		public override string Message
		{
			get
			{
				return "No response was received during the time-out period for the request.";
			}
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0000DBBD File Offset: 0x0000BDBD
		public TimeoutException()
		{
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0000DBCC File Offset: 0x0000BDCC
		public TimeoutException(string message) : base(message)
		{
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0000DBDC File Offset: 0x0000BDDC
		public TimeoutException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0000DBED File Offset: 0x0000BDED
		protected TimeoutException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
