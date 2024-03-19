using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000088 RID: 136
	public class GetVmInfoEventArgs : BrowserEventArgs
	{
		// Token: 0x0600030D RID: 781 RVA: 0x0000398F File Offset: 0x00001B8F
		public GetVmInfoEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
