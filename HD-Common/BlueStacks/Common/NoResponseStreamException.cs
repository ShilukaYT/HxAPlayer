using System;
using System.Runtime.Serialization;

namespace BlueStacks.Common
{
	// Token: 0x020001DC RID: 476
	[Serializable]
	public class NoResponseStreamException : Exception
	{
		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x0000DAB3 File Offset: 0x0000BCB3
		// (set) Token: 0x06000F9C RID: 3996 RVA: 0x0000DABB File Offset: 0x0000BCBB
		public int ErrorCode { get; set; } = 2;

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x0000DAC4 File Offset: 0x0000BCC4
		// (set) Token: 0x06000F9E RID: 3998 RVA: 0x0000DACC File Offset: 0x0000BCCC
		public long BytesRecieved { get; private set; }

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x0000DAD5 File Offset: 0x0000BCD5
		public override string Message
		{
			get
			{
				return "Could not get a response stream from the remote server.";
			}
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0000DADC File Offset: 0x0000BCDC
		public NoResponseStreamException(Exception innerException) : base("", innerException)
		{
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0000DAF1 File Offset: 0x0000BCF1
		public NoResponseStreamException()
		{
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0000DB00 File Offset: 0x0000BD00
		public NoResponseStreamException(string message) : base(message)
		{
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0000DB10 File Offset: 0x0000BD10
		public NoResponseStreamException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0000DB21 File Offset: 0x0000BD21
		protected NoResponseStreamException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
