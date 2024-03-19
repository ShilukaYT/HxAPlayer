using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000046 RID: 70
	[XmlRoot(ElementName = "RTC", Namespace = "http://www.virtualbox.org/")]
	public class RTC
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00002991 File Offset: 0x00000B91
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00002999 File Offset: 0x00000B99
		[XmlAttribute(AttributeName = "localOrUTC")]
		public string LocalOrUTC { get; set; }
	}
}
