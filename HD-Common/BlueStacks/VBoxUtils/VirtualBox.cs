using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x0200004F RID: 79
	[XmlRoot(ElementName = "VirtualBox", Namespace = "http://www.virtualbox.org/")]
	public class VirtualBox
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00002C8E File Offset: 0x00000E8E
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00002C96 File Offset: 0x00000E96
		[XmlElement(ElementName = "Machine", Namespace = "http://www.virtualbox.org/")]
		public Machine Machine { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00002C9F File Offset: 0x00000E9F
		// (set) Token: 0x060001AF RID: 431 RVA: 0x00002CA7 File Offset: 0x00000EA7
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00002CB0 File Offset: 0x00000EB0
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x00002CB8 File Offset: 0x00000EB8
		[XmlAttribute(AttributeName = "version")]
		public string Version { get; set; }
	}
}
