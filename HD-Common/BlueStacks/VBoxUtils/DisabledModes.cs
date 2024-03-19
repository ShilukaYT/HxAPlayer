using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000040 RID: 64
	[XmlRoot(ElementName = "DisabledModes", Namespace = "http://www.virtualbox.org/")]
	public class DisabledModes
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00002870 File Offset: 0x00000A70
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00002878 File Offset: 0x00000A78
		[XmlElement(ElementName = "InternalNetwork", Namespace = "http://www.virtualbox.org/")]
		public InternalNetwork InternalNetwork { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00002881 File Offset: 0x00000A81
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00002889 File Offset: 0x00000A89
		[XmlElement(ElementName = "NATNetwork", Namespace = "http://www.virtualbox.org/")]
		public NATNetwork NATNetwork { get; set; }
	}
}
