using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x0200002D RID: 45
	[XmlRoot(ElementName = "LongMode", Namespace = "http://www.virtualbox.org/")]
	public class LongMode
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00002694 File Offset: 0x00000894
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x0000269C File Offset: 0x0000089C
		[XmlAttribute(AttributeName = "enabled")]
		public string Enabled { get; set; }
	}
}
