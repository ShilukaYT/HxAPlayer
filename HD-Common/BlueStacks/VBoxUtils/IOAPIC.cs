using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000037 RID: 55
	[XmlRoot(ElementName = "IOAPIC", Namespace = "http://www.virtualbox.org/")]
	public class IOAPIC
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00002782 File Offset: 0x00000982
		// (set) Token: 0x060000FD RID: 253 RVA: 0x0000278A File Offset: 0x0000098A
		[XmlAttribute(AttributeName = "enabled")]
		public string Enabled { get; set; }
	}
}
