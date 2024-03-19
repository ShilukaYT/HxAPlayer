using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x0200008A RID: 138
	public class ThemeChangeEventArgs : BrowserEventArgs
	{
		// Token: 0x0600030F RID: 783 RVA: 0x0000398F File Offset: 0x00001B8F
		public ThemeChangeEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
