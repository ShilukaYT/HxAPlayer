using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlueStacks.Common
{
	// Token: 0x020000E3 RID: 227
	[Serializable]
	public class MacroEvents
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x00005651 File Offset: 0x00003851
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x00005659 File Offset: 0x00003859
		[JsonProperty("Timestamp", NullValueHandling = NullValueHandling.Ignore)]
		public long Timestamp { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00005662 File Offset: 0x00003862
		// (set) Token: 0x060005F1 RID: 1521 RVA: 0x0000566A File Offset: 0x0000386A
		[JsonExtensionData]
		public IDictionary<string, object> ExtraData { get; set; }
	}
}
