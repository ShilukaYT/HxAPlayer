using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x0200002A RID: 42
	[XmlRoot(ElementName = "ExtraDataItem", Namespace = "http://www.virtualbox.org/")]
	public class ExtraDataItem
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00002650 File Offset: 0x00000850
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00002658 File Offset: 0x00000858
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00002661 File Offset: 0x00000861
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00002669 File Offset: 0x00000869
		[XmlAttribute(AttributeName = "value")]
		public string Value { get; set; }
	}
}
