using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000045 RID: 69
	[XmlRoot(ElementName = "AudioAdapter", Namespace = "http://www.virtualbox.org/")]
	public class AudioAdapter
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000296F File Offset: 0x00000B6F
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00002977 File Offset: 0x00000B77
		[XmlAttribute(AttributeName = "driver")]
		public string Driver { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000146 RID: 326 RVA: 0x00002980 File Offset: 0x00000B80
		// (set) Token: 0x06000147 RID: 327 RVA: 0x00002988 File Offset: 0x00000B88
		[XmlAttribute(AttributeName = "enabled")]
		public string Enabled { get; set; }
	}
}
