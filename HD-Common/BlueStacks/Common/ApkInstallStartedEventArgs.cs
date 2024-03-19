using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000085 RID: 133
	public class ApkInstallStartedEventArgs : BrowserEventArgs
	{
		// Token: 0x0600030A RID: 778 RVA: 0x0000398F File Offset: 0x00001B8F
		public ApkInstallStartedEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
