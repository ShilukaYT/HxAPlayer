using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x0200003F RID: 63
	[XmlRoot(ElementName = "NATNetwork", Namespace = "http://www.virtualbox.org/")]
	public class NATNetwork
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000285F File Offset: 0x00000A5F
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00002867 File Offset: 0x00000A67
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }
	}
}
