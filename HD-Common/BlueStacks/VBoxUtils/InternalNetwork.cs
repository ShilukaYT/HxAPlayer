using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x0200003E RID: 62
	[XmlRoot(ElementName = "InternalNetwork", Namespace = "http://www.virtualbox.org/")]
	public class InternalNetwork
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000284E File Offset: 0x00000A4E
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00002856 File Offset: 0x00000A56
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }
	}
}
