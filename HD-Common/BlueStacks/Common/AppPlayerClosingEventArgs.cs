using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x0200007B RID: 123
	public class AppPlayerClosingEventArgs : BrowserEventArgs
	{
		// Token: 0x06000300 RID: 768 RVA: 0x0000398F File Offset: 0x00001B8F
		public AppPlayerClosingEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
