using System;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000092 RID: 146
	public class ShowFlePopupEventArgs : BrowserEventArgs
	{
		// Token: 0x06000317 RID: 791 RVA: 0x0000398F File Offset: 0x00001B8F
		public ShowFlePopupEventArgs(BrowserControlTags tag, string vmName, JObject extraData) : base(tag, vmName, extraData)
		{
		}
	}
}
