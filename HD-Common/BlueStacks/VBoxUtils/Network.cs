using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000042 RID: 66
	[XmlRoot(ElementName = "Network", Namespace = "http://www.virtualbox.org/")]
	public class Network
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00002909 File Offset: 0x00000B09
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00002911 File Offset: 0x00000B11
		[XmlElement(ElementName = "Adapter", Namespace = "http://www.virtualbox.org/")]
		public List<Adapter> Adapter { get; set; }
	}
}
