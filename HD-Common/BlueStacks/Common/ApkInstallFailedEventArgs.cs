using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000086 RID: 134
	public class ApkInstallFailedEventArgs : BrowserEventArgs
	{
		// Token: 0x0600030B RID: 779 RVA: 0x0000398F File Offset: 0x00001B8F
		public ApkInstallFailedEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
