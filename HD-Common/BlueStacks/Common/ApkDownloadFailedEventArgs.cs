using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000082 RID: 130
	public class ApkDownloadFailedEventArgs : BrowserEventArgs
	{
		// Token: 0x06000307 RID: 775 RVA: 0x0000398F File Offset: 0x00001B8F
		public ApkDownloadFailedEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
