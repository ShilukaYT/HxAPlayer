using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000044 RID: 68
	[XmlRoot(ElementName = "LPT", Namespace = "http://www.virtualbox.org/")]
	public class LPT
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000295E File Offset: 0x00000B5E
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00002966 File Offset: 0x00000B66
		[XmlElement(ElementName = "Port", Namespace = "http://www.virtualbox.org/")]
		public Port Port { get; set; }
	}
}
