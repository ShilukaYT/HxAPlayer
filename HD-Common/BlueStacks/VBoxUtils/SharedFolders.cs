using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000048 RID: 72
	[XmlRoot(ElementName = "SharedFolders", Namespace = "http://www.virtualbox.org/")]
	public class SharedFolders
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000029E6 File Offset: 0x00000BE6
		// (set) Token: 0x06000156 RID: 342 RVA: 0x000029EE File Offset: 0x00000BEE
		[XmlElement(ElementName = "SharedFolder", Namespace = "http://www.virtualbox.org/")]
		public List<SharedFolder> SharedFolder { get; set; }
	}
}
