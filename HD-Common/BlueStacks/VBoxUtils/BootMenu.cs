using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000039 RID: 57
	[XmlRoot(ElementName = "BootMenu", Namespace = "http://www.virtualbox.org/")]
	public class BootMenu
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000027C6 File Offset: 0x000009C6
		// (set) Token: 0x06000107 RID: 263 RVA: 0x000027CE File Offset: 0x000009CE
		[XmlAttribute(AttributeName = "mode")]
		public string Mode { get; set; }
	}
}
