using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x0200008C RID: 140
	public class OemDownloadFailedEventArgs : BrowserEventArgs
	{
		// Token: 0x06000311 RID: 785 RVA: 0x0000398F File Offset: 0x00001B8F
		public OemDownloadFailedEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
