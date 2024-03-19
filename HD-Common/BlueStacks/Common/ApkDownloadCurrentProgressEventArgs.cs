using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000084 RID: 132
	public class ApkDownloadCurrentProgressEventArgs : BrowserEventArgs
	{
		// Token: 0x06000309 RID: 777 RVA: 0x0000398F File Offset: 0x00001B8F
		public ApkDownloadCurrentProgressEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
