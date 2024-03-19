using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000034 RID: 52
	[XmlRoot(ElementName = "Boot", Namespace = "http://www.virtualbox.org/")]
	public class Boot
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000274F File Offset: 0x0000094F
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00002757 File Offset: 0x00000957
		[XmlElement(ElementName = "Order", Namespace = "http://www.virtualbox.org/")]
		public List<Order> Order { get; set; }
	}
}
