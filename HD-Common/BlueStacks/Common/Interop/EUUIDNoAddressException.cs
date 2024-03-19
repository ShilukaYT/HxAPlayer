using System;
using System.Runtime.Serialization;

namespace BlueStacks.Common.Interop
{
	// Token: 0x0200025F RID: 607
	[Serializable]
	public class EUUIDNoAddressException : EUUIDException
	{
		// Token: 0x060012B3 RID: 4787 RVA: 0x0000F460 File Offset: 0x0000D660
		public EUUIDNoAddressException()
		{
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0000F468 File Offset: 0x0000D668
		public EUUIDNoAddressException(string message) : base(message)
		{
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0000F471 File Offset: 0x0000D671
		public EUUIDNoAddressException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x0000F47B File Offset: 0x0000D67B
		protected EUUIDNoAddressException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
