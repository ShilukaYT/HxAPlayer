using System;
using System.Collections.Generic;
using System.IO;

namespace BlueStacks.Common
{
	// Token: 0x020000FE RID: 254
	public sealed class BlueStacksUIColorManager
	{
		// Token: 0x060006A1 RID: 1697 RVA: 0x0000225B File Offset: 0x0000045B
		private BlueStacksUIColorManager()
		{
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00020E30 File Offset: 0x0001F030
		public static string GetThemeFilePath(string themeName)
		{
			string text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ThemeFile");
			if (File.Exists(text))
			{
				return text;
			}
			return Path.Combine(Path.Combine(RegistryManager.Instance.ClientInstallDir, themeName), "ThemeFile");
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x00020E78 File Offset: 0x0001F078
		public static BlueStacksUIColorManager Instance
		{
			get
			{
				if (BlueStacksUIColorManager.mInstance == null)
				{
					object obj = BlueStacksUIColorManager.syncRoot;
					lock (obj)
					{
						if (BlueStacksUIColorManager.mInstance == null)
						{
							BlueStacksUIColorManager.mInstance = new BlueStacksUIColorManager();
						}
					}
				}
				return BlueStacksUIColorManager.mInstance;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x00020ED0 File Offset: 0x0001F0D0
		// (set) Token: 0x060006A5 RID: 1701 RVA: 0x00005F30 File Offset: 0x00004130
		public static BluestacksUIColor AppliedTheme
		{
			get
			{
				if (BlueStacksUIColorManager.mAppliedTheme == null)
				{
					object obj = BlueStacksUIColorManager.syncRoot;
					lock (obj)
					{
						if (BlueStacksUIColorManager.mAppliedTheme == null)
						{
							BluestacksUIColor bluestacksUIColor = BluestacksUIColor.Load(BlueStacksUIColorManager.GetThemeFilePath(RegistryManager.ClientThemeName));
							if (bluestacksUIColor != null && bluestacksUIColor.DictBrush.Count > 0)
							{
								BlueStacksUIColorManager.mAppliedTheme = bluestacksUIColor;
							}
							if (BlueStacksUIColorManager.mAppliedTheme != null)
							{
								BlueStacksUIColorManager.mAppliedTheme.NotifyUIElements();
							}
						}
					}
				}
				return BlueStacksUIColorManager.mAppliedTheme;
			}
			private set
			{
				if (value != null)
				{
					BlueStacksUIColorManager.mAppliedTheme = value;
					BlueStacksUIColorManager.mAppliedTheme.NotifyUIElements();
				}
			}
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00020F50 File Offset: 0x0001F150
		public static void ReloadAppliedTheme(string themeName)
		{
			BluestacksUIColor bluestacksUIColor = BluestacksUIColor.Load(BlueStacksUIColorManager.GetThemeFilePath(themeName));
			if (bluestacksUIColor != null && bluestacksUIColor.DictBrush.Count > 0)
			{
				BlueStacksUIColorManager.AppliedTheme = bluestacksUIColor;
				RegistryManager.Instance.SetClientThemeNameInRegistry(themeName);
				CustomPictureBox.UpdateImagesFromNewDirectory("");
			}
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00020F98 File Offset: 0x0001F198
		public static IEnumerable<string> GetThemes()
		{
			List<string> list = new List<string>();
			foreach (string text in Directory.GetDirectories(RegistryManager.Instance.ClientInstallDir))
			{
				if (File.Exists(Path.Combine(text, "ThemeFile")))
				{
					list.Add(Path.GetFileName(text));
				}
			}
			return list;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00020FEC File Offset: 0x0001F1EC
		public static string GetThemeName(string themeName)
		{
			string text = "";
			try
			{
				if (!File.Exists(BlueStacksUIColorManager.GetThemeFilePath(themeName)))
				{
					throw new Exception("Theme file not found exception " + themeName);
				}
				text = BluestacksUIColor.Load(BlueStacksUIColorManager.GetThemeFilePath(themeName)).DictThemeAvailable["ThemeDisplayName"];
				text = LocaleStrings.GetLocalizedString(text, "");
			}
			catch (Exception ex)
			{
				Logger.Warning("Error checking for theme availability in Theme file " + themeName + Environment.NewLine + ex.ToString());
				text = "";
			}
			return text;
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00021080 File Offset: 0x0001F280
		public static void ApplyTheme(string themeName)
		{
			try
			{
				if (!File.Exists(BlueStacksUIColorManager.GetThemeFilePath(themeName)))
				{
					throw new Exception("Theme file not found exception " + themeName);
				}
				BluestacksUIColor bluestacksUIColor = BluestacksUIColor.Load(BlueStacksUIColorManager.GetThemeFilePath(themeName));
				if (bluestacksUIColor != null && bluestacksUIColor.DictBrush.Count > 0)
				{
					BlueStacksUIColorManager.AppliedTheme = bluestacksUIColor;
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error checking for theme availability in Theme file " + themeName + Environment.NewLine + ex.ToString());
			}
		}

		// Token: 0x04000363 RID: 867
		public const string ThemeFileName = "ThemeFile";

		// Token: 0x04000364 RID: 868
		private static volatile BlueStacksUIColorManager mInstance = null;

		// Token: 0x04000365 RID: 869
		private static object syncRoot = new object();

		// Token: 0x04000366 RID: 870
		private static BluestacksUIColor mAppliedTheme = null;
	}
}
