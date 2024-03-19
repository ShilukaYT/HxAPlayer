using System;
using System.Runtime.Serialization;

namespace BlueStacks.Common.Interop
{
	// Token: 0x0200025E RID: 606
	[Serializable]
	public class EUUIDLocalOnlyException : EUUIDException
	{
		// Token: 0x060012AF RID: 4783 RVA: 0x0000F460 File Offset: 0x0000D660
		public EUUIDLocalOnlyException()
		{
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0000F468 File Offset: 0x0000D668
		public EUUIDLocalOnlyException(string message) : base(message)
		{
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0000F471 File Offset: 0x0000D671
		public EUUIDLocalOnlyException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0000F47B File Offset: 0x0000D67B
		protected EUUIDLocalOnlyException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
