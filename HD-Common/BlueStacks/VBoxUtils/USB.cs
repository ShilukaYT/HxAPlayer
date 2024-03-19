using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x0200003D RID: 61
	[XmlRoot(ElementName = "USB", Namespace = "http://www.virtualbox.org/")]
	public class USB
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000283D File Offset: 0x00000A3D
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00002845 File Offset: 0x00000A45
		[XmlElement(ElementName = "Controllers", Namespace = "http://www.virtualbox.org/")]
		public Controllers Controllers { get; set; }
	}
}
