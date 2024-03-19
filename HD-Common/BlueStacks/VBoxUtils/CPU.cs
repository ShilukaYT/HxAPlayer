using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x0200002F RID: 47
	[XmlRoot(ElementName = "CPU", Namespace = "http://www.virtualbox.org/")]
	public class CPU
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000DC RID: 220 RVA: 0x000026B6 File Offset: 0x000008B6
		// (set) Token: 0x060000DD RID: 221 RVA: 0x000026BE File Offset: 0x000008BE
		[XmlElement(ElementName = "PAE", Namespace = "http://www.virtualbox.org/")]
		public PAE PAE { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000026C7 File Offset: 0x000008C7
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000026CF File Offset: 0x000008CF
		[XmlElement(ElementName = "LongMode", Namespace = "http://www.virtualbox.org/")]
		public LongMode LongMode { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000026D8 File Offset: 0x000008D8
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000026E0 File Offset: 0x000008E0
		[XmlElement(ElementName = "HardwareVirtExLargePages", Namespace = "http://www.virtualbox.org/")]
		public HardwareVirtExLargePages HardwareVirtExLargePages { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000026E9 File Offset: 0x000008E9
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x000026F1 File Offset: 0x000008F1
		[XmlAttribute(AttributeName = "count")]
		public string Count { get; set; }
	}
}
