using System;
using System.Runtime.Serialization;

namespace BlueStacks.Common
{
	// Token: 0x0200004B RID: 75
	[Serializable]
	public class NoResponseStreamException : Exception
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000041B4 File Offset: 0x000023B4
		// (set) Token: 0x06000087 RID: 135 RVA: 0x000041BC File Offset: 0x000023BC
		public int ErrorCode { get; set; } = 2;

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000041C5 File Offset: 0x000023C5
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000041CD File Offset: 0x000023CD
		public long BytesRecieved { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000041D6 File Offset: 0x000023D6
		public override string Message
		{
			get
			{
				return "Could not get a response stream from the remote server.";
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000041DD File Offset: 0x000023DD
		public NoResponseStreamException(Exception innerException) : base("", innerException)
		{
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000041F4 File Offset: 0x000023F4
		public NoResponseStreamException()
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004205 File Offset: 0x00002405
		public NoResponseStreamException(string message) : base(message)
		{
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004217 File Offset: 0x00002417
		public NoResponseStreamException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000422A File Offset: 0x0000242A
		protected NoResponseStreamException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
