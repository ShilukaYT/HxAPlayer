using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000090 RID: 144
	public class OemInstallFailedEventArgs : BrowserEventArgs
	{
		// Token: 0x06000315 RID: 789 RVA: 0x0000398F File Offset: 0x00001B8F
		public OemInstallFailedEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
