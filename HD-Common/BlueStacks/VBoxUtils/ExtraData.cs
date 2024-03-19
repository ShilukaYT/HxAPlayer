using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x0200002B RID: 43
	[XmlRoot(ElementName = "ExtraData", Namespace = "http://www.virtualbox.org/")]
	public class ExtraData
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00002672 File Offset: 0x00000872
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x0000267A File Offset: 0x0000087A
		[XmlElement(ElementName = "ExtraDataItem", Namespace = "http://www.virtualbox.org/")]
		public List<ExtraDataItem> ExtraDataItem { get; set; }
	}
}
