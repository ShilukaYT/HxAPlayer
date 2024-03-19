using System;
using Newtonsoft.Json;

namespace BlueStacks.Common
{
	// Token: 0x02000075 RID: 117
	[Serializable]
	public class AppPackageObject
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00003694 File Offset: 0x00001894
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000369C File Offset: 0x0000189C
		[JsonProperty(PropertyName = "app_pkg", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Include)]
		public string Package { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x000036A5 File Offset: 0x000018A5
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x000036AD File Offset: 0x000018AD
		[JsonProperty(PropertyName = "extra_info", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Include)]
		public SerializableDictionary<string, string> ExtraInfo { get; set; } = new SerializableDictionary<string, string>();
	}
}
