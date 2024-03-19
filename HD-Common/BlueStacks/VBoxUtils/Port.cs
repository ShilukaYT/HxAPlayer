using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000043 RID: 67
	[XmlRoot(ElementName = "Port", Namespace = "http://www.virtualbox.org/")]
	public class Port
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000291A File Offset: 0x00000B1A
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00002922 File Offset: 0x00000B22
		[XmlAttribute(AttributeName = "slot")]
		public string Slot { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000292B File Offset: 0x00000B2B
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00002933 File Offset: 0x00000B33
		[XmlAttribute(AttributeName = "enabled")]
		public string Enabled { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000293C File Offset: 0x00000B3C
		// (set) Token: 0x0600013D RID: 317 RVA: 0x00002944 File Offset: 0x00000B44
		[XmlAttribute(AttributeName = "IOBase")]
		public string IOBase { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000294D File Offset: 0x00000B4D
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00002955 File Offset: 0x00000B55
		[XmlAttribute(AttributeName = "IRQ")]
		public string IRQ { get; set; }
	}
}
