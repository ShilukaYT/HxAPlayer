using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x0200008F RID: 143
	public class OemInstallStartedEventArgs : BrowserEventArgs
	{
		// Token: 0x06000314 RID: 788 RVA: 0x0000398F File Offset: 0x00001B8F
		public OemInstallStartedEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
