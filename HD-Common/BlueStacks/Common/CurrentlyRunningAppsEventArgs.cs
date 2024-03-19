using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000093 RID: 147
	public class CurrentlyRunningAppsEventArgs : BrowserEventArgs
	{
		// Token: 0x06000318 RID: 792 RVA: 0x0000398F File Offset: 0x00001B8F
		public CurrentlyRunningAppsEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
