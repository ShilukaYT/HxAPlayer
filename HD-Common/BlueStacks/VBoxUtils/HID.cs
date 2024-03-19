using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000031 RID: 49
	[XmlRoot(ElementName = "HID", Namespace = "http://www.virtualbox.org/")]
	public class HID
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000270B File Offset: 0x0000090B
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00002713 File Offset: 0x00000913
		[XmlAttribute(AttributeName = "Pointing")]
		public string Pointing { get; set; }
	}
}
