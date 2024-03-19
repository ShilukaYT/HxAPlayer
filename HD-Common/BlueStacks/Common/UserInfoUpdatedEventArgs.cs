using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000089 RID: 137
	public class UserInfoUpdatedEventArgs : BrowserEventArgs
	{
		// Token: 0x0600030E RID: 782 RVA: 0x0000398F File Offset: 0x00001B8F
		public UserInfoUpdatedEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
