using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000030 RID: 48
	[XmlRoot(ElementName = "Memory", Namespace = "http://www.virtualbox.org/")]
	public class Memory
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000026FA File Offset: 0x000008FA
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00002702 File Offset: 0x00000902
		[XmlAttribute(AttributeName = "RAMSize")]
		public string RAMSize { get; set; }
	}
}
