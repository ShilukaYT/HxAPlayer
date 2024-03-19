using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x0200004D RID: 77
	[XmlRoot(ElementName = "StorageControllers", Namespace = "http://www.virtualbox.org/")]
	public class StorageControllers
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00002BE4 File Offset: 0x00000DE4
		// (set) Token: 0x06000197 RID: 407 RVA: 0x00002BEC File Offset: 0x00000DEC
		[XmlElement(ElementName = "StorageController", Namespace = "http://www.virtualbox.org/")]
		public List<StorageController> StorageController { get; set; }
	}
}
