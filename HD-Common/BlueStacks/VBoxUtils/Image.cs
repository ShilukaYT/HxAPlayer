using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x0200004A RID: 74
	[XmlRoot(ElementName = "Image", Namespace = "http://www.virtualbox.org/")]
	public class Image
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00002AE5 File Offset: 0x00000CE5
		// (set) Token: 0x06000176 RID: 374 RVA: 0x00002AED File Offset: 0x00000CED
		[XmlAttribute(AttributeName = "uuid")]
		public string Uuid { get; set; }
	}
}
