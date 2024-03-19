using System;
using System.Runtime.Serialization;

namespace BlueStacks.Common.Interop
{
	// Token: 0x0200025D RID: 605
	[Serializable]
	public class EUUIDException : Exception
	{
		// Token: 0x060012AB RID: 4779 RVA: 0x00002418 File Offset: 0x00000618
		public EUUIDException()
		{
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00002420 File Offset: 0x00000620
		public EUUIDException(string reason) : base(reason)
		{
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00002429 File Offset: 0x00000629
		public EUUIDException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00002433 File Offset: 0x00000633
		protected EUUIDException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
