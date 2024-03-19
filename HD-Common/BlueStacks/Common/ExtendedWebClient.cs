using System;
using System.Net;

namespace BlueStacks.Common
{
	// Token: 0x020000C1 RID: 193
	public class ExtendedWebClient : WebClient
	{
		// Token: 0x060004C4 RID: 1220 RVA: 0x00004C87 File Offset: 0x00002E87
		public ExtendedWebClient(int timeout)
		{
			this.mTimeout = timeout;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00004C96 File Offset: 0x00002E96
		protected override WebRequest GetWebRequest(Uri address)
		{
			WebRequest webRequest = base.GetWebRequest(address);
			webRequest.Timeout = this.mTimeout;
			return webRequest;
		}

		// Token: 0x04000227 RID: 551
		private int mTimeout;
	}
}
