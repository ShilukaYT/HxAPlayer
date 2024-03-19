using System;
using System.Xml.Serialization;

namespace BlueStacks.VBoxUtils
{
	// Token: 0x02000049 RID: 73
	[XmlRoot(ElementName = "Hardware", Namespace = "http://www.virtualbox.org/")]
	public class Hardware
	{
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000029F7 File Offset: 0x00000BF7
		// (set) Token: 0x06000159 RID: 345 RVA: 0x000029FF File Offset: 0x00000BFF
		[XmlElement(ElementName = "CPU", Namespace = "http://www.virtualbox.org/")]
		public CPU CPU { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00002A08 File Offset: 0x00000C08
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00002A10 File Offset: 0x00000C10
		[XmlElement(ElementName = "Memory", Namespace = "http://www.virtualbox.org/")]
		public Memory Memory { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00002A19 File Offset: 0x00000C19
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00002A21 File Offset: 0x00000C21
		[XmlElement(ElementName = "HID", Namespace = "http://www.virtualbox.org/")]
		public HID HID { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00002A2A File Offset: 0x00000C2A
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00002A32 File Offset: 0x00000C32
		[XmlElement(ElementName = "HPET", Namespace = "http://www.virtualbox.org/")]
		public HPET HPET { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00002A3B File Offset: 0x00000C3B
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00002A43 File Offset: 0x00000C43
		[XmlElement(ElementName = "Boot", Namespace = "http://www.virtualbox.org/")]
		public Boot Boot { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00002A4C File Offset: 0x00000C4C
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00002A54 File Offset: 0x00000C54
		[XmlElement(ElementName = "Display", Namespace = "http://www.virtualbox.org/")]
		public Display Display { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00002A5D File Offset: 0x00000C5D
		// (set) Token: 0x06000165 RID: 357 RVA: 0x00002A65 File Offset: 0x00000C65
		[XmlElement(ElementName = "RemoteDisplay", Namespace = "http://www.virtualbox.org/")]
		public RemoteDisplay RemoteDisplay { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00002A6E File Offset: 0x00000C6E
		// (set) Token: 0x06000167 RID: 359 RVA: 0x00002A76 File Offset: 0x00000C76
		[XmlElement(ElementName = "BIOS", Namespace = "http://www.virtualbox.org/")]
		public BIOS BIOS { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00002A7F File Offset: 0x00000C7F
		// (set) Token: 0x06000169 RID: 361 RVA: 0x00002A87 File Offset: 0x00000C87
		[XmlElement(ElementName = "USB", Namespace = "http://www.virtualbox.org/")]
		public USB USB { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00002A90 File Offset: 0x00000C90
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00002A98 File Offset: 0x00000C98
		[XmlElement(ElementName = "Network", Namespace = "http://www.virtualbox.org/")]
		public Network Network { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00002AA1 File Offset: 0x00000CA1
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00002AA9 File Offset: 0x00000CA9
		[XmlElement(ElementName = "LPT", Namespace = "http://www.virtualbox.org/")]
		public LPT LPT { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00002AB2 File Offset: 0x00000CB2
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00002ABA File Offset: 0x00000CBA
		[XmlElement(ElementName = "AudioAdapter", Namespace = "http://www.virtualbox.org/")]
		public AudioAdapter AudioAdapter { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00002AC3 File Offset: 0x00000CC3
		// (set) Token: 0x06000171 RID: 369 RVA: 0x00002ACB File Offset: 0x00000CCB
		[XmlElement(ElementName = "RTC", Namespace = "http://www.virtualbox.org/")]
		public RTC RTC { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00002AD4 File Offset: 0x00000CD4
		// (set) Token: 0x06000173 RID: 371 RVA: 0x00002ADC File Offset: 0x00000CDC
		[XmlElement(ElementName = "SharedFolders", Namespace = "http://www.virtualbox.org/")]
		public SharedFolders SharedFolders { get; set; }
	}
}
