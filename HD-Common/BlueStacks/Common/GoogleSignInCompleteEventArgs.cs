using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x0200007A RID: 122
	public class GoogleSignInCompleteEventArgs : BrowserEventArgs
	{
		// Token: 0x060002FF RID: 767 RVA: 0x0000398F File Offset: 0x00001B8F
		public GoogleSignInCompleteEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
