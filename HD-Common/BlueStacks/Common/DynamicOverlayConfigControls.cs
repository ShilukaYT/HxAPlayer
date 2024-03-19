using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlueStacks.Common
{
	// Token: 0x0200006F RID: 111
	[Serializable]
	public class DynamicOverlayConfigControls
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00013504 File Offset: 0x00011704
		public static DynamicOverlayConfigControls Instance
		{
			get
			{
				if (DynamicOverlayConfigControls.sInstance == null)
				{
					object obj = DynamicOverlayConfigControls.syncRoot;
					lock (obj)
					{
						if (DynamicOverlayConfigControls.sInstance == null)
						{
							DynamicOverlayConfigControls.sInstance = new DynamicOverlayConfigControls();
						}
					}
				}
				return DynamicOverlayConfigControls.sInstance;
			}
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000354F File Offset: 0x0000174F
		private DynamicOverlayConfigControls()
		{
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0001355C File Offset: 0x0001175C
		public static void Init(string data)
		{
			try
			{
				DynamicOverlayConfigControls.sInstance = JsonConvert.DeserializeObject<DynamicOverlayConfigControls>(data, Utils.GetSerializerSettings());
			}
			catch (Exception ex)
			{
				Logger.Warning("Error loading dynamic overlay data. Ex: " + ex.ToString());
			}
		}

		// Token: 0x0400012C RID: 300
		private static volatile DynamicOverlayConfigControls sInstance;

		// Token: 0x0400012D RID: 301
		private static object syncRoot = new object();

		// Token: 0x0400012E RID: 302
		public List<DynamicOverlayConfig> GameControls = new List<DynamicOverlayConfig>();
	}
}
