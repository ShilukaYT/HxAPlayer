using System;
using System.Collections.Generic;

namespace BlueStacks.Common
{
	// Token: 0x02000113 RID: 275
	public class CustomVolumeEventArgs : EventArgs
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x000065B5 File Offset: 0x000047B5
		// (set) Token: 0x06000769 RID: 1897 RVA: 0x000065BD File Offset: 0x000047BD
		public int Volume { get; set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x000065C6 File Offset: 0x000047C6
		// (set) Token: 0x0600076B RID: 1899 RVA: 0x000065CE File Offset: 0x000047CE
		public Dictionary<string, string> dictData { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x000065D7 File Offset: 0x000047D7
		// (set) Token: 0x0600076D RID: 1901 RVA: 0x000065DF File Offset: 0x000047DF
		public string mSelected { get; set; }

		// Token: 0x0600076E RID: 1902 RVA: 0x000065E8 File Offset: 0x000047E8
		public CustomVolumeEventArgs(int volume)
		{
			this.Volume = volume;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x000065F7 File Offset: 0x000047F7
		public CustomVolumeEventArgs(Dictionary<string, string> dict, string selected)
		{
			this.dictData = dict;
			this.mSelected = selected;
		}
	}
}
