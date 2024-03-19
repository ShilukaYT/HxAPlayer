using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x0200004B RID: 75
	[XmlRoot(ElementName = "AttachedDevice", Namespace = "http://www.virtualbox.org/")]
	public class AttachedDevice
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00002AF6 File Offset: 0x00000CF6
		// (set) Token: 0x06000179 RID: 377 RVA: 0x00002AFE File Offset: 0x00000CFE
		[XmlElement(ElementName = "Image", Namespace = "http://www.virtualbox.org/")]
		public Image Image { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00002B07 File Offset: 0x00000D07
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00002B0F File Offset: 0x00000D0F
		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00002B18 File Offset: 0x00000D18
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00002B20 File Offset: 0x00000D20
		[XmlAttribute(AttributeName = "port")]
		public string Port { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00002B29 File Offset: 0x00000D29
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00002B31 File Offset: 0x00000D31
		[XmlAttribute(AttributeName = "device")]
		public string Device { get; set; }
	}
}
