using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000079 RID: 121
	public class BootCompleteEventArgs : BrowserEventArgs
	{
		// Token: 0x060002FE RID: 766 RVA: 0x0000398F File Offset: 0x00001B8F
		public BootCompleteEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
