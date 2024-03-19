using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000096 RID: 150
	public class ToggleMicrophoneMuteStateEventArgs : BrowserEventArgs
	{
		// Token: 0x0600031B RID: 795 RVA: 0x0000398F File Offset: 0x00001B8F
		public ToggleMicrophoneMuteStateEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
