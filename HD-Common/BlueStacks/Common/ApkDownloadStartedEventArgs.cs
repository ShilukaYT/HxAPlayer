using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000081 RID: 129
	public class ApkDownloadStartedEventArgs : BrowserEventArgs
	{
		// Token: 0x06000306 RID: 774 RVA: 0x0000398F File Offset: 0x00001B8F
		public ApkDownloadStartedEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
