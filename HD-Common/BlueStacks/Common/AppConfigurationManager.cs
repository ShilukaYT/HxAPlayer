using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlueStacks.Common
{
	// Token: 0x02000053 RID: 83
	[Serializable]
	public class AppConfigurationManager
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00011BA8 File Offset: 0x0000FDA8
		public static AppConfigurationManager Instance
		{
			get
			{
				if (AppConfigurationManager.sInstance == null)
				{
					object obj = AppConfigurationManager.syncRoot;
					lock (obj)
					{
						if (AppConfigurationManager.sInstance == null)
						{
							AppConfigurationManager.Init();
						}
					}
				}
				return AppConfigurationManager.sInstance;
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00002E1A File Offset: 0x0000101A
		private AppConfigurationManager()
		{
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00011BF8 File Offset: 0x0000FDF8
		private static void Init()
		{
			try
			{
				AppConfigurationManager.sInstance = JsonConvert.DeserializeObject<AppConfigurationManager>(RegistryManager.Instance.AppConfiguration, Utils.GetSerializerSettings());
			}
			catch (Exception ex)
			{
				Logger.Warning("Error loading app configurations. Ex: " + ex.ToString());
			}
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00002E2D File Offset: 0x0000102D
		public static void Save()
		{
			if (AppConfigurationManager.sInstance != null)
			{
				RegistryManager.Instance.AppConfiguration = JsonConvert.SerializeObject(AppConfigurationManager.sInstance, Utils.GetSerializerSettings());
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00002E53 File Offset: 0x00001053
		[JsonProperty("VmAppConfig", DefaultValueHandling = DefaultValueHandling.Populate, NullValueHandling = NullValueHandling.Ignore)]
		public Dictionary<string, Dictionary<string, AppSettings>> VmAppConfig { get; } = new Dictionary<string, Dictionary<string, AppSettings>>();

		// Token: 0x060001DB RID: 475 RVA: 0x00011C4C File Offset: 0x0000FE4C
		public bool CheckIfTrueInAnyVm(string package, Predicate<AppSettings> rule, out string vmName)
		{
			vmName = string.Empty;
			foreach (KeyValuePair<string, Dictionary<string, AppSettings>> keyValuePair in this.VmAppConfig)
			{
				if (keyValuePair.Value.ContainsKey(package) && rule(keyValuePair.Value[package]))
				{
					vmName = keyValuePair.Key;
					return true;
				}
			}
			return false;
		}

		// Token: 0x040000B9 RID: 185
		private static volatile AppConfigurationManager sInstance;

		// Token: 0x040000BA RID: 186
		private static object syncRoot = new object();
	}
}
