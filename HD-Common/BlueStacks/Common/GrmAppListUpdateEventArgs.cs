using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000080 RID: 128
	public class GrmAppListUpdateEventArgs : BrowserEventArgs
	{
		// Token: 0x06000305 RID: 773 RVA: 0x0000398F File Offset: 0x00001B8F
		public GrmAppListUpdateEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
