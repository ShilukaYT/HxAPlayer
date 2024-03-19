using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x0200004E RID: 78
	[XmlRoot(ElementName = "Machine", Namespace = "http://www.virtualbox.org/")]
	public class Machine
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00002BF5 File Offset: 0x00000DF5
		// (set) Token: 0x0600019A RID: 410 RVA: 0x00002BFD File Offset: 0x00000DFD
		[XmlElement(ElementName = "MediaRegistry", Namespace = "http://www.virtualbox.org/")]
		public MediaRegistry MediaRegistry { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00002C06 File Offset: 0x00000E06
		// (set) Token: 0x0600019C RID: 412 RVA: 0x00002C0E File Offset: 0x00000E0E
		[XmlElement(ElementName = "ExtraData", Namespace = "http://www.virtualbox.org/")]
		public ExtraData ExtraData { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00002C17 File Offset: 0x00000E17
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00002C1F File Offset: 0x00000E1F
		[XmlElement(ElementName = "Hardware", Namespace = "http://www.virtualbox.org/")]
		public Hardware Hardware { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00002C28 File Offset: 0x00000E28
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00002C30 File Offset: 0x00000E30
		[XmlElement(ElementName = "StorageControllers", Namespace = "http://www.virtualbox.org/")]
		public StorageControllers StorageControllers { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00002C39 File Offset: 0x00000E39
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00002C41 File Offset: 0x00000E41
		[XmlAttribute(AttributeName = "uuid")]
		public string Uuid { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00002C4A File Offset: 0x00000E4A
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00002C52 File Offset: 0x00000E52
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00002C5B File Offset: 0x00000E5B
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00002C63 File Offset: 0x00000E63
		[XmlAttribute(AttributeName = "OSType")]
		public string OSType { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00002C6C File Offset: 0x00000E6C
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00002C74 File Offset: 0x00000E74
		[XmlAttribute(AttributeName = "snapshotFolder")]
		public string SnapshotFolder { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00002C7D File Offset: 0x00000E7D
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00002C85 File Offset: 0x00000E85
		[XmlAttribute(AttributeName = "lastStateChange")]
		public string LastStateChange { get; set; }
	}
}
