using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x0200004C RID: 76
	[XmlRoot(ElementName = "StorageController", Namespace = "http://www.virtualbox.org/")]
	public class StorageController
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00002B3A File Offset: 0x00000D3A
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00002B42 File Offset: 0x00000D42
		[XmlElement(ElementName = "AttachedDevice", Namespace = "http://www.virtualbox.org/")]
		public List<AttachedDevice> AttachedDevice { get; set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00002B4B File Offset: 0x00000D4B
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00002B53 File Offset: 0x00000D53
		[XmlAttribute(AttributeName = "name")]
		public string Name { get; set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00002B5C File Offset: 0x00000D5C
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00002B64 File Offset: 0x00000D64
		[XmlAttribute(AttributeName = "type")]
		public string Type { get; set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00002B6D File Offset: 0x00000D6D
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00002B75 File Offset: 0x00000D75
		[XmlAttribute(AttributeName = "PortCount")]
		public string PortCount { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00002B7E File Offset: 0x00000D7E
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00002B86 File Offset: 0x00000D86
		[XmlAttribute(AttributeName = "useHostIOCache")]
		public string UseHostIOCache { get; set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00002B8F File Offset: 0x00000D8F
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00002B97 File Offset: 0x00000D97
		[XmlAttribute(AttributeName = "Bootable")]
		public string Bootable { get; set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00002BA0 File Offset: 0x00000DA0
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00002BA8 File Offset: 0x00000DA8
		[XmlAttribute(AttributeName = "IDE0MasterEmulationPort")]
		public string IDE0MasterEmulationPort { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00002BB1 File Offset: 0x00000DB1
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00002BB9 File Offset: 0x00000DB9
		[XmlAttribute(AttributeName = "IDE0SlaveEmulationPort")]
		public string IDE0SlaveEmulationPort { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00002BC2 File Offset: 0x00000DC2
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00002BCA File Offset: 0x00000DCA
		[XmlAttribute(AttributeName = "IDE1MasterEmulationPort")]
		public string IDE1MasterEmulationPort { get; set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00002BD3 File Offset: 0x00000DD3
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00002BDB File Offset: 0x00000DDB
		[XmlAttribute(AttributeName = "IDE1SlaveEmulationPort")]
		public string IDE1SlaveEmulationPort { get; set; }
	}
}
