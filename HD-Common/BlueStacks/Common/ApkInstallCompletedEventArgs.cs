using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000087 RID: 135
	public class ApkInstallCompletedEventArgs : BrowserEventArgs
	{
		// Token: 0x0600030C RID: 780 RVA: 0x0000398F File Offset: 0x00001B8F
		public ApkInstallCompletedEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
