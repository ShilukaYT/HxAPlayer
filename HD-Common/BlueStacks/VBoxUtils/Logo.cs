using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000038 RID: 56
	[XmlRoot(ElementName = "Logo", Namespace = "http://www.virtualbox.org/")]
	public class Logo
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00002793 File Offset: 0x00000993
		// (set) Token: 0x06000100 RID: 256 RVA: 0x0000279B File Offset: 0x0000099B
		[XmlAttribute(AttributeName = "fadeIn")]
		public string FadeIn { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000027A4 File Offset: 0x000009A4
		// (set) Token: 0x06000102 RID: 258 RVA: 0x000027AC File Offset: 0x000009AC
		[XmlAttribute(AttributeName = "fadeOut")]
		public string FadeOut { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000027B5 File Offset: 0x000009B5
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000027BD File Offset: 0x000009BD
		[XmlAttribute(AttributeName = "displayTime")]
		public string DisplayTime { get; set; }
	}
}
