using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x0200008B RID: 139
	public class OemDownloadStartedEventArgs : BrowserEventArgs
	{
		// Token: 0x06000310 RID: 784 RVA: 0x0000398F File Offset: 0x00001B8F
		public OemDownloadStartedEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
