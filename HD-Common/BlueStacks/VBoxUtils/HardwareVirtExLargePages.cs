using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x0200002E RID: 46
	[XmlRoot(ElementName = "HardwareVirtExLargePages", Namespace = "http://www.virtualbox.org/")]
	public class HardwareVirtExLargePages
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x000026A5 File Offset: 0x000008A5
		// (set) Token: 0x060000DA RID: 218 RVA: 0x000026AD File Offset: 0x000008AD
		[XmlAttribute(AttributeName = "enabled")]
		public string Enabled { get; set; }
	}
}
