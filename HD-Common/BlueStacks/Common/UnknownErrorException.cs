using System;
using System.Runtime.Serialization;

namespace BlueStacks.Common
{
	// Token: 0x020001DD RID: 477
	[Serializable]
	public class UnknownErrorException : Exception
	{
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000FA5 RID: 4005 RVA: 0x0000DB32 File Offset: 0x0000BD32
		// (set) Token: 0x06000FA6 RID: 4006 RVA: 0x0000DB3A File Offset: 0x0000BD3A
		public int ErrorCode { get; set; } = 99;

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x0000DB43 File Offset: 0x0000BD43
		public override string Message
		{
			get
			{
				return "An exception of an unknown type has occurred.";
			}
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0000DB4A File Offset: 0x0000BD4A
		public UnknownErrorException(Exception innerException) : base("", innerException)
		{
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0000DB60 File Offset: 0x0000BD60
		public UnknownErrorException()
		{
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0000DB70 File Offset: 0x0000BD70
		public UnknownErrorException(string message) : base(message)
		{
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0000DB81 File Offset: 0x0000BD81
		public UnknownErrorException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0000DB93 File Offset: 0x0000BD93
		protected UnknownErrorException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
