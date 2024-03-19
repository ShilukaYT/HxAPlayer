using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000047 RID: 71
	[XmlRoot(ElementName = "SharedFolder", Namespace = "http://www.virtualbox.org/")]
	public class SharedFolder
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000029A2 File Offset: 0x00000BA2
		// (set) Token: 0x0600014D RID: 333 RVA: 0x000029AA File Offset: 0x00000BAA
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000029B3 File Offset: 0x00000BB3
		// (set) Token: 0x0600014F RID: 335 RVA: 0x000029BB File Offset: 0x00000BBB
		[XmlAttribute(AttributeName = "hostPath")]
		public string HostPath { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000029C4 File Offset: 0x00000BC4
		// (set) Token: 0x06000151 RID: 337 RVA: 0x000029CC File Offset: 0x00000BCC
		[XmlAttribute(AttributeName = "writable")]
		public string Writable { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000152 RID: 338 RVA: 0x000029D5 File Offset: 0x00000BD5
		// (set) Token: 0x06000153 RID: 339 RVA: 0x000029DD File Offset: 0x00000BDD
		[XmlAttribute(AttributeName = "autoMount")]
		public string AutoMount { get; set; }
	}
}
