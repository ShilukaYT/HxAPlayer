using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000041 RID: 65
	[XmlRoot(ElementName = "Adapter", Namespace = "http://www.virtualbox.org/")]
	public class Adapter
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00002892 File Offset: 0x00000A92
		// (set) Token: 0x06000127 RID: 295 RVA: 0x0000289A File Offset: 0x00000A9A
		[XmlElement(ElementName = "DisabledModes", Namespace = "http://www.virtualbox.org/")]
		public DisabledModes DisabledModes { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000028A3 File Offset: 0x00000AA3
		// (set) Token: 0x06000129 RID: 297 RVA: 0x000028AB File Offset: 0x00000AAB
		[XmlElement(ElementName = "NAT", Namespace = "http://www.virtualbox.org/")]
		public string NAT { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000028B4 File Offset: 0x00000AB4
		// (set) Token: 0x0600012B RID: 299 RVA: 0x000028BC File Offset: 0x00000ABC
		[XmlAttribute(AttributeName = "slot")]
		public string Slot { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000028C5 File Offset: 0x00000AC5
		// (set) Token: 0x0600012D RID: 301 RVA: 0x000028CD File Offset: 0x00000ACD
		[XmlAttribute(AttributeName = "enabled")]
		public string Enabled { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000028D6 File Offset: 0x00000AD6
		// (set) Token: 0x0600012F RID: 303 RVA: 0x000028DE File Offset: 0x00000ADE
		[XmlAttribute(AttributeName = "MACAddress")]
		public string MACAddress { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000028E7 File Offset: 0x00000AE7
		// (set) Token: 0x06000131 RID: 305 RVA: 0x000028EF File Offset: 0x00000AEF
		[XmlAttribute(AttributeName = "cable")]
		public string Cable { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000028F8 File Offset: 0x00000AF8
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00002900 File Offset: 0x00000B00
		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }
	}
}
