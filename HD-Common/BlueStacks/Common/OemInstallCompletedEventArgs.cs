using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000091 RID: 145
	public class OemInstallCompletedEventArgs : BrowserEventArgs
	{
		// Token: 0x06000316 RID: 790 RVA: 0x0000398F File Offset: 0x00001B8F
		public OemInstallCompletedEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
