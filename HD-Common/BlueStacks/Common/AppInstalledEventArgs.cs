using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x0200007E RID: 126
	public class AppInstalledEventArgs : BrowserEventArgs
	{
		// Token: 0x06000303 RID: 771 RVA: 0x0000398F File Offset: 0x00001B8F
		public AppInstalledEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
