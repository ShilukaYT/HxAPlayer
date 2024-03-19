using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000032 RID: 50
	[XmlRoot(ElementName = "HPET", Namespace = "http://www.virtualbox.org/")]
	public class HPET
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000EB RID: 235 RVA: 0x0000271C File Offset: 0x0000091C
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00002724 File Offset: 0x00000924
		[XmlAttribute(AttributeName = "enabled")]
		public string Enabled { get; set; }
	}
}
