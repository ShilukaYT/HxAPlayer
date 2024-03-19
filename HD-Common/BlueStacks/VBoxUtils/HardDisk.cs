using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000027 RID: 39
	[XmlRoot(ElementName = "HardDisk", Namespace = "http://www.virtualbox.org/")]
	public class HardDisk
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000025D9 File Offset: 0x000007D9
		// (set) Token: 0x060000BB RID: 187 RVA: 0x000025E1 File Offset: 0x000007E1
		[XmlAttribute(AttributeName = "uuid")]
		public string Uuid { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000025EA File Offset: 0x000007EA
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000025F2 File Offset: 0x000007F2
		[XmlAttribute(AttributeName = "location")]
		public string Location { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000025FB File Offset: 0x000007FB
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00002603 File Offset: 0x00000803
		[XmlAttribute(AttributeName = "format")]
		public string Format { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000260C File Offset: 0x0000080C
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00002614 File Offset: 0x00000814
		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000261D File Offset: 0x0000081D
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00002625 File Offset: 0x00000825
		[XmlElement(ElementName = "HardDisk", Namespace = "http://www.virtualbox.org/")]
		public List<HardDisk> HardDisk1 { get; set; }
	}
}
