using System;
using System.Runtime.Serialization;

namespace BlueStacks.Common
{
	// Token: 0x020001DF RID: 479
	[Serializable]
	public class ConnectFailureException : Exception
	{
		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x0000DBFE File Offset: 0x0000BDFE
		// (set) Token: 0x06000FB5 RID: 4021 RVA: 0x0000DC06 File Offset: 0x0000BE06
		public int ErrorCode { get; set; } = 1;

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x0000DC0F File Offset: 0x0000BE0F
		public override string Message
		{
			get
			{
				return "The remote service point could not be contacted.";
			}
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0000DC16 File Offset: 0x0000BE16
		public ConnectFailureException(Exception innerException) : base("", innerException)
		{
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0000DC2B File Offset: 0x0000BE2B
		public ConnectFailureException()
		{
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0000DC3A File Offset: 0x0000BE3A
		public ConnectFailureException(string message) : base(message)
		{
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0000DC4A File Offset: 0x0000BE4A
		public ConnectFailureException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0000DC5B File Offset: 0x0000BE5B
		protected ConnectFailureException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
