using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000095 RID: 149
	public class GetVolumeLevelEventArgs : BrowserEventArgs
	{
		// Token: 0x0600031A RID: 794 RVA: 0x0000398F File Offset: 0x00001B8F
		public GetVolumeLevelEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
