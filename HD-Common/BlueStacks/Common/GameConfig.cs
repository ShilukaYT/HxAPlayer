using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x020000D0 RID: 208
	public class GameConfig
	{
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x00019AF4 File Offset: 0x00017CF4
		public static GameConfig Instance
		{
			get
			{
				if (GameConfig.sInstance == null)
				{
					object obj = GameConfig.syncRoot;
					lock (obj)
					{
						GameConfig.sInstance = new GameConfig();
						GameConfig.Init();
					}
				}
				return GameConfig.sInstance;
			}
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00019B48 File Offset: 0x00017D48
		private static void Init()
		{
			GameConfig.sFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "game_config.json");
			if (File.Exists(GameConfig.sFilePath))
			{
				Logger.Info("Loading cfg from : " + GameConfig.sFilePath);
				GameConfig.LoadFile(GameConfig.sFilePath);
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00019B98 File Offset: 0x00017D98
		private static void LoadFile(string sFilePath)
		{
			try
			{
				JObject obj = JObject.Parse(File.ReadAllText(sFilePath));
				GameConfig.Instance.AppName = GameConfig.GetJsonStringValue(obj, "app_name");
				GameConfig.Instance.PkgName = GameConfig.GetJsonStringValue(obj, "pkg_name");
				GameConfig.Instance.ActivityName = GameConfig.GetJsonStringValue(obj, "activity_name");
				GameConfig.Instance.ControlPanelEntryName = GameConfig.GetJsonStringValue(obj, "control_panel_name");
				GameConfig.Instance.ControlPanelPublisher = GameConfig.GetJsonStringValue(obj, "control_panel_publisher");
				GameConfig.Instance.InstallerCopyrightText = GameConfig.GetJsonStringValue(obj, "installer_copyright");
				GameConfig.Instance.AppGenericAction = (GenericAction)Enum.Parse(typeof(GenericAction), GameConfig.GetJsonStringValue(obj, "app_generic_action"));
				GameConfig.Instance.AppCDNURL = GameConfig.GetJsonStringValue(obj, "app_cdn_url");
			}
			catch (Exception ex)
			{
				Logger.Error("Some error while parsing config, maybe an invalid file. Ex: {0}", new object[]
				{
					ex.Message
				});
			}
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00019C9C File Offset: 0x00017E9C
		private static string GetJsonStringValue(JObject obj, string keyName)
		{
			string empty = string.Empty;
			if (obj.ContainsKey(keyName))
			{
				return obj[keyName].ToString().Trim();
			}
			return empty;
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x00004E38 File Offset: 0x00003038
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x00004E40 File Offset: 0x00003040
		public string ControlPanelEntryName { get; private set; } = string.Empty;

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x00004E49 File Offset: 0x00003049
		// (set) Token: 0x0600050E RID: 1294 RVA: 0x00004E51 File Offset: 0x00003051
		public string ControlPanelPublisher { get; private set; } = string.Empty;

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x00004E5A File Offset: 0x0000305A
		// (set) Token: 0x06000510 RID: 1296 RVA: 0x00004E62 File Offset: 0x00003062
		public string InstallerCopyrightText { get; private set; } = string.Empty;

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x00004E6B File Offset: 0x0000306B
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x00004E73 File Offset: 0x00003073
		public string AppName { get; private set; } = string.Empty;

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00004E7C File Offset: 0x0000307C
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x00004E84 File Offset: 0x00003084
		public string PkgName { get; private set; } = string.Empty;

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00004E8D File Offset: 0x0000308D
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x00004E95 File Offset: 0x00003095
		public string ActivityName { get; private set; } = string.Empty;

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00004E9E File Offset: 0x0000309E
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x00004EA6 File Offset: 0x000030A6
		public GenericAction AppGenericAction { get; private set; } = GenericAction.InstallPlay;

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x00004EAF File Offset: 0x000030AF
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x00004EB7 File Offset: 0x000030B7
		public string AppCDNURL { get; private set; } = string.Empty;

		// Token: 0x0400023E RID: 574
		public const string sConfigFilename = "game_config.json";

		// Token: 0x0400023F RID: 575
		private static string sFilePath = string.Empty;

		// Token: 0x04000240 RID: 576
		private static volatile GameConfig sInstance;

		// Token: 0x04000241 RID: 577
		private static object syncRoot = new object();
	}
}
