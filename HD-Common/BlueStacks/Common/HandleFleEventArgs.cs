using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000094 RID: 148
	public class HandleFleEventArgs : BrowserEventArgs
	{
		// Token: 0x06000319 RID: 793 RVA: 0x0000398F File Offset: 0x00001B8F
		public HandleFleEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
