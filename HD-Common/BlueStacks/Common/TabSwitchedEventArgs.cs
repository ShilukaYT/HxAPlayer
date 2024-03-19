using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x0200007D RID: 125
	public class TabSwitchedEventArgs : BrowserEventArgs
	{
		// Token: 0x06000302 RID: 770 RVA: 0x0000398F File Offset: 0x00001B8F
		public TabSwitchedEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
