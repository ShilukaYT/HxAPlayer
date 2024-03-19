using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x0200003A RID: 58
	[XmlRoot(ElementName = "BIOS", Namespace = "http://www.virtualbox.org/")]
	public class BIOS
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000027D7 File Offset: 0x000009D7
		// (set) Token: 0x0600010A RID: 266 RVA: 0x000027DF File Offset: 0x000009DF
		[XmlElement(ElementName = "IOAPIC", Namespace = "http://www.virtualbox.org/")]
		public IOAPIC IOAPIC { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000027E8 File Offset: 0x000009E8
		// (set) Token: 0x0600010C RID: 268 RVA: 0x000027F0 File Offset: 0x000009F0
		[XmlElement(ElementName = "Logo", Namespace = "http://www.virtualbox.org/")]
		public Logo Logo { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000027F9 File Offset: 0x000009F9
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00002801 File Offset: 0x00000A01
		[XmlElement(ElementName = "BootMenu", Namespace = "http://www.virtualbox.org/")]
		public BootMenu BootMenu { get; set; }
	}
}
