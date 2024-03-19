using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000036 RID: 54
	[XmlRoot(ElementName = "RemoteDisplay", Namespace = "http://www.virtualbox.org/")]
	public class RemoteDisplay
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00002771 File Offset: 0x00000971
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00002779 File Offset: 0x00000979
		[XmlAttribute(AttributeName = "enabled")]
		public string Enabled { get; set; }
	}
}
