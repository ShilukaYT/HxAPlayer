using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000035 RID: 53
	[XmlRoot(ElementName = "Display", Namespace = "http://www.virtualbox.org/")]
	public class Display
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00002760 File Offset: 0x00000960
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00002768 File Offset: 0x00000968
		[XmlAttribute(AttributeName = "VRAMSize")]
		public string VRAMSize { get; set; }
	}
}
