using System;
using System.Runtime.Serialization;

namespace BlueStacks.Common
{
	// Token: 0x0200004E RID: 78
	[Serializable]
	public class ConnectFailureException : Exception
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000431B File Offset: 0x0000251B
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00004323 File Offset: 0x00002523
		public int ErrorCode { get; set; } = 1;

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000432C File Offset: 0x0000252C
		public override string Message
		{
			get
			{
				return "The remote service point could not be contacted.";
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004333 File Offset: 0x00002533
		public ConnectFailureException(Exception innerException) : base("", innerException)
		{
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000434A File Offset: 0x0000254A
		public ConnectFailureException()
		{
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000435B File Offset: 0x0000255B
		public ConnectFailureException(string message) : base(message)
		{
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000436D File Offset: 0x0000256D
		public ConnectFailureException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004380 File Offset: 0x00002580
		protected ConnectFailureException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
