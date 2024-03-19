using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x0200003C RID: 60
	[XmlRoot(ElementName = "Controllers", Namespace = "http://www.virtualbox.org/")]
	public class Controllers
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000282C File Offset: 0x00000A2C
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00002834 File Offset: 0x00000A34
		[XmlElement(ElementName = "Controller", Namespace = "http://www.virtualbox.org/")]
		public Controller Controller { get; set; }
	}
}
