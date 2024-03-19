using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x0200007C RID: 124
	public class TabClosingEventArgs : BrowserEventArgs
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0000398F File Offset: 0x00001B8F
		public TabClosingEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
