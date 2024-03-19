using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000029 RID: 41
	[XmlRoot(ElementName = "MediaRegistry", Namespace = "http://www.virtualbox.org/")]
	public class MediaRegistry
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000263F File Offset: 0x0000083F
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00002647 File Offset: 0x00000847
		[XmlElement(ElementName = "HardDisks", Namespace = "http://www.virtualbox.org/")]
		public HardDisks HardDisks { get; set; }
	}
}
