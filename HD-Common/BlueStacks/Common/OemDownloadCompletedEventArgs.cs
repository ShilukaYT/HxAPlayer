using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x0200008D RID: 141
	public class OemDownloadCompletedEventArgs : BrowserEventArgs
	{
		// Token: 0x06000312 RID: 786 RVA: 0x0000398F File Offset: 0x00001B8F
		public OemDownloadCompletedEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
