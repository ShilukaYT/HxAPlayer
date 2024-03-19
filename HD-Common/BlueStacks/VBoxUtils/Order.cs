using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000033 RID: 51
	[XmlRoot(ElementName = "Order", Namespace = "http://www.virtualbox.org/")]
	public class Order
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000272D File Offset: 0x0000092D
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00002735 File Offset: 0x00000935
		[XmlAttribute(AttributeName = "position")]
		public string Position { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000273E File Offset: 0x0000093E
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00002746 File Offset: 0x00000946
		[XmlAttribute(AttributeName = "device")]
		public string Device { get; set; }
	}
}
