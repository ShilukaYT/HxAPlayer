using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000028 RID: 40
	[XmlRoot(ElementName = "HardDisks", Namespace = "http://www.virtualbox.org/")]
	public class HardDisks
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000262E File Offset: 0x0000082E
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00002636 File Offset: 0x00000836
		[XmlElement(ElementName = "HardDisk", Namespace = "http://www.virtualbox.org/")]
		public List<HardDisk> HardDisk { get; set; }
	}
}
