using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x0200002C RID: 44
	[XmlRoot(ElementName = "PAE", Namespace = "http://www.virtualbox.org/")]
	public class PAE
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00002683 File Offset: 0x00000883
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x0000268B File Offset: 0x0000088B
		[XmlAttribute(AttributeName = "enabled")]
		public string Enabled { get; set; }
	}
}
