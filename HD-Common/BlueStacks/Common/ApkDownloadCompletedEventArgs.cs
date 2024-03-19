using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000083 RID: 131
	public class ApkDownloadCompletedEventArgs : BrowserEventArgs
	{
		// Token: 0x06000308 RID: 776 RVA: 0x0000398F File Offset: 0x00001B8F
		public ApkDownloadCompletedEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
