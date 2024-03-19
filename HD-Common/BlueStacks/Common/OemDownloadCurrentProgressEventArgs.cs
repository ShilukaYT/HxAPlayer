using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x0200008E RID: 142
	public class OemDownloadCurrentProgressEventArgs : BrowserEventArgs
	{
		// Token: 0x06000313 RID: 787 RVA: 0x0000398F File Offset: 0x00001B8F
		public OemDownloadCurrentProgressEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
