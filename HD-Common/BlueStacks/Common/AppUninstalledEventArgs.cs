using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x0200007F RID: 127
	public class AppUninstalledEventArgs : BrowserEventArgs
	{
		// Token: 0x06000304 RID: 772 RVA: 0x0000398F File Offset: 0x00001B8F
		public AppUninstalledEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
