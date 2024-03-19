using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x0200003B RID: 59
	[XmlRoot(ElementName = "Controller", Namespace = "http://www.virtualbox.org/")]
	public class Controller
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000280A File Offset: 0x00000A0A
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00002812 File Offset: 0x00000A12
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000112 RID: 274 RVA: 0x0000281B File Offset: 0x00000A1B
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00002823 File Offset: 0x00000A23
		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }
	}
}
