using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace BlueStacks.Common
{
	// Token: 0x0200011B RID: 283
	public class FeatureManager
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x000261D0 File Offset: 0x000243D0
		public static FeatureManager Instance
		{
			get
			{
				if (FeatureManager.sInstance == null)
				{
					object obj = FeatureManager.syncRoot;
					lock (obj)
					{
						if (FeatureManager.sInstance == null)
						{
							FeatureManager.sInstance = new FeatureManager();
							FeatureManager.Init(true, true);
						}
					}
				}
				return FeatureManager.sInstance;
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00026230 File Offset: 0x00024430
		public static void Init(bool isAsync = true, bool downloadBstConfig = true)
		{
			try
			{
				string clientInstallDir = RegistryManager.Instance.ClientInstallDir;
				if (string.IsNullOrEmpty(clientInstallDir))
				{
					Logger.Warning("ClientInstallDir is not set. FeatureManager should not be used before creating directory");
					FeatureManager.SetDefaultSettings();
				}
				else
				{
					FeatureManager.sFilePath = Path.Combine(clientInstallDir, "bst_config");
					Logger.Info(string.Format("Feature Manager Init - isAync:{0} downloadBstConfig:{1}", isAsync, downloadBstConfig));
					Logger.Info(string.Format("Feature Manager Init - UpdateBstConfig:{0} - {1} Path:{2} DoesExist: {3}", new object[]
					{
						RegistryManager.Instance.UpdateBstConfig,
						"bst_config",
						FeatureManager.sFilePath,
						!File.Exists(FeatureManager.sFilePath)
					}));
					if ((downloadBstConfig && RegistryManager.Instance.UpdateBstConfig) || (downloadBstConfig && !RegistryManager.Instance.UpdateBstConfig && !File.Exists(FeatureManager.sFilePath)))
					{
						FeatureManager.DownloadBstConfig(isAsync);
					}
					else if (File.Exists(FeatureManager.sFilePath))
					{
						FeatureManager.LoadFile(FeatureManager.sFilePath, true);
					}
					else
					{
						FeatureManager.SetDefaultSettings();
					}
				}
			}
			catch (Exception arg)
			{
				Logger.Info(string.Format("Error loading {0}: {1}", "bst_config", arg));
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00006A6B File Offset: 0x00004C6B
		private static void DownloadBstConfig(bool isAsync)
		{
			FeatureManager.SetDefaultSettings();
			if (isAsync)
			{
				FeatureManager.DownloadFileAsync();
				return;
			}
			FeatureManager.DownloadFile();
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00006A80 File Offset: 0x00004C80
		private static void DownloadFileAsync()
		{
			new Thread(delegate()
			{
				FeatureManager.DownloadFile();
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00026358 File Offset: 0x00024558
		private static void DownloadFile()
		{
			try
			{
				string urlWithParams = WebHelper.GetUrlWithParams(WebHelper.GetServerHost() + "/serve_config_file", null, null, null);
				string directoryName = Path.GetDirectoryName(FeatureManager.sFilePath);
				Logger.Info("bst_config file download url: " + urlWithParams + " and file path: " + FeatureManager.sFilePath);
				if (!Directory.Exists(directoryName))
				{
					Logger.Info("--------- For debugging -------. FeatureManager was used before creating directory. This should not happen.");
					Directory.CreateDirectory(directoryName);
				}
				using (WebClient webClient = new WebClient())
				{
					webClient.DownloadFile(urlWithParams, FeatureManager.sFilePath);
					RegistryManager.Instance.UpdateBstConfig = false;
				}
			}
			catch (Exception arg)
			{
				Logger.Warning(string.Format("Failed to download {0} file. Err: {1}", "bst_config", arg));
				RegistryManager.Instance.UpdateBstConfig = true;
				return;
			}
			FeatureManager.LoadFile(FeatureManager.sFilePath, false);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00006AB2 File Offset: 0x00004CB2
		private static void SetDefaultSettings()
		{
			Logger.Info("FeatureManager->SetDefaultSettings");
			if (FeatureManager.sInstance == null)
			{
				FeatureManager.sInstance = new FeatureManager();
			}
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00026430 File Offset: 0x00024630
		public static void LoadFile(string filePath, bool retryOnError = true)
		{
			try
			{
				Logger.Info("Loading bst_config Settings from " + filePath);
				using (XmlReader xmlReader = XmlReader.Create(File.OpenRead(filePath)))
				{
					FeatureManager.sInstance = (FeatureManager)new XmlSerializer(typeof(FeatureManager)).Deserialize(xmlReader);
				}
			}
			catch (Exception)
			{
				FeatureManager.SetDefaultSettings();
				try
				{
					if (retryOnError)
					{
						if (File.Exists(filePath))
						{
							File.Delete(filePath);
						}
						FeatureManager.DownloadFileAsync();
					}
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00006AD3 File Offset: 0x00004CD3
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x00006ADB File Offset: 0x00004CDB
		public bool IsBTVEnabled { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x00006AE4 File Offset: 0x00004CE4
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x00006AEC File Offset: 0x00004CEC
		public bool IsWallpaperChangeDisabled { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00006AF5 File Offset: 0x00004CF5
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x00006AFD File Offset: 0x00004CFD
		public bool IsCreateBrowserOnStart { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00006B06 File Offset: 0x00004D06
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x00006B0E File Offset: 0x00004D0E
		public bool IsOpenActivityFromAccountIcon { get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x00006B17 File Offset: 0x00004D17
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x00006B1F File Offset: 0x00004D1F
		public bool IsBrowserKilledOnTabSwitch { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x00006B28 File Offset: 0x00004D28
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x00006B30 File Offset: 0x00004D30
		public bool IsPromotionDisabled { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x00006B39 File Offset: 0x00004D39
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x00006B41 File Offset: 0x00004D41
		public bool IsGuidBackUpEnable { get; set; } = true;

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x00006B4A File Offset: 0x00004D4A
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x00006B52 File Offset: 0x00004D52
		public bool IsCustomUIForDMMSandbox { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00006B5B File Offset: 0x00004D5B
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x00006B63 File Offset: 0x00004D63
		public bool IsThemeEnabled { get; set; } = true;

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00006B6C File Offset: 0x00004D6C
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x00006B74 File Offset: 0x00004D74
		public bool IsSearchBarVisible { get; set; } = true;

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00006B7D File Offset: 0x00004D7D
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x00006B85 File Offset: 0x00004D85
		public bool IsCustomResolutionInputAllowed { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x00006B8E File Offset: 0x00004D8E
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x00006B96 File Offset: 0x00004D96
		public bool ShowBeginnersGuidePreference { get; set; } = true;

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x00006B9F File Offset: 0x00004D9F
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x00006BA7 File Offset: 0x00004DA7
		public bool IsShowNotificationCentre { get; set; } = true;

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x00006BB0 File Offset: 0x00004DB0
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x00006BB8 File Offset: 0x00004DB8
		public bool IsUseWpfTextbox { get; set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x00006BC1 File Offset: 0x00004DC1
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x00006BC9 File Offset: 0x00004DC9
		public bool IsComboKeysDisabled { get; set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00006BD2 File Offset: 0x00004DD2
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x00006BDA File Offset: 0x00004DDA
		public bool IsMacroRecorderEnabled { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00006BE3 File Offset: 0x00004DE3
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x00006BEB File Offset: 0x00004DEB
		public bool IsFarmingModeDisabled { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x00006BF4 File Offset: 0x00004DF4
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x00006BFC File Offset: 0x00004DFC
		public bool IsOperationsSyncEnabled { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x00006C05 File Offset: 0x00004E05
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x00006C0D File Offset: 0x00004E0D
		public bool IsRotateScreenDisabled { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x00006C16 File Offset: 0x00004E16
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x00006C1E File Offset: 0x00004E1E
		public bool IsUserAccountBtnEnabled { get; set; } = true;

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x00006C27 File Offset: 0x00004E27
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x00006C2F File Offset: 0x00004E2F
		public bool IsWarningBtnEnabled { get; set; } = true;

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x00006C38 File Offset: 0x00004E38
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x00006C40 File Offset: 0x00004E40
		public bool IsAppCenterTabVisible { get; set; } = true;

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x00006C49 File Offset: 0x00004E49
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x00006C51 File Offset: 0x00004E51
		public bool IsMultiInstanceControlsGridVisible { get; set; } = true;

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00006C5A File Offset: 0x00004E5A
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x00006C62 File Offset: 0x00004E62
		public bool IsPromotionFixed { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x00006C6B File Offset: 0x00004E6B
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x00006C73 File Offset: 0x00004E73
		public bool IsShowLanguagePreference { get; set; } = true;

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00006C7C File Offset: 0x00004E7C
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x00006C84 File Offset: 0x00004E84
		public bool IsShowDesktopShortcutPreference { get; set; } = true;

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00006C8D File Offset: 0x00004E8D
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x00006C95 File Offset: 0x00004E95
		public bool IsShowGamingSummaryPreference { get; set; } = true;

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x00006C9E File Offset: 0x00004E9E
		// (set) Token: 0x0600080B RID: 2059 RVA: 0x00006CA6 File Offset: 0x00004EA6
		public bool IsShowSpeedUpTips { get; set; } = true;

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x00006CAF File Offset: 0x00004EAF
		// (set) Token: 0x0600080D RID: 2061 RVA: 0x00006CB7 File Offset: 0x00004EB7
		public bool IsShowPostOTSScreen { get; set; } = true;

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x00006CC0 File Offset: 0x00004EC0
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x00006CC8 File Offset: 0x00004EC8
		public bool IsShowHelpCenter { get; set; } = true;

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x00006CD1 File Offset: 0x00004ED1
		// (set) Token: 0x06000811 RID: 2065 RVA: 0x00006CD9 File Offset: 0x00004ED9
		public bool IsAppSettingsAvailable { get; set; } = true;

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x00006CE2 File Offset: 0x00004EE2
		// (set) Token: 0x06000813 RID: 2067 RVA: 0x00006CEA File Offset: 0x00004EEA
		public bool IsShowPerformancePreference { get; set; } = true;

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x00006CF3 File Offset: 0x00004EF3
		// (set) Token: 0x06000815 RID: 2069 RVA: 0x00006CFB File Offset: 0x00004EFB
		public bool IsShowDiscordPreference { get; set; } = true;

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00006D04 File Offset: 0x00004F04
		// (set) Token: 0x06000817 RID: 2071 RVA: 0x00006D0C File Offset: 0x00004F0C
		public bool IsCustomUIForNCSoft { get; set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x00006D15 File Offset: 0x00004F15
		// (set) Token: 0x06000819 RID: 2073 RVA: 0x00006D1D File Offset: 0x00004F1D
		public bool AllowADBSettingToggle { get; set; } = true;

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x00006D26 File Offset: 0x00004F26
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x00006D2E File Offset: 0x00004F2E
		public bool ShowClientOnTopPreference { get; set; } = true;

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x00006D37 File Offset: 0x00004F37
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x00006D3F File Offset: 0x00004F3F
		public bool IsAllowGameRecording { get; set; } = true;

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00006D48 File Offset: 0x00004F48
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x00006D50 File Offset: 0x00004F50
		public bool IsShowAppRecommendations { get; set; } = true;

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00006D59 File Offset: 0x00004F59
		// (set) Token: 0x06000821 RID: 2081 RVA: 0x00006D61 File Offset: 0x00004F61
		public bool IsCheckForQuitPopup { get; set; } = true;

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x00006D6A File Offset: 0x00004F6A
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x00006D72 File Offset: 0x00004F72
		public bool IsCustomCursorEnabled { get; set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x00006D7B File Offset: 0x00004F7B
		// (set) Token: 0x06000825 RID: 2085 RVA: 0x00006D83 File Offset: 0x00004F83
		public bool ForceEnableMacroAndSync { get; set; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x00006D8C File Offset: 0x00004F8C
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x00006D94 File Offset: 0x00004F94
		public bool IsHtmlSideBar { get; set; } = true;

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x00006D9D File Offset: 0x00004F9D
		// (set) Token: 0x06000829 RID: 2089 RVA: 0x00006DA5 File Offset: 0x00004FA5
		public bool IsTimelineStatsEnabled { get; set; } = true;

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x0600082A RID: 2090 RVA: 0x00006DAE File Offset: 0x00004FAE
		// (set) Token: 0x0600082B RID: 2091 RVA: 0x00006DB6 File Offset: 0x00004FB6
		public bool IsShowAdvanceExitOption { get; set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x00006DBF File Offset: 0x00004FBF
		// (set) Token: 0x0600082D RID: 2093 RVA: 0x00006DC7 File Offset: 0x00004FC7
		public bool IsShowAndroidInputDebugSetting { get; set; } = true;

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x00006DD0 File Offset: 0x00004FD0
		// (set) Token: 0x0600082F RID: 2095 RVA: 0x00006DD8 File Offset: 0x00004FD8
		public bool IsTopbarHelpEnabled { get; set; } = true;

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x00006DE1 File Offset: 0x00004FE1
		// (set) Token: 0x06000831 RID: 2097 RVA: 0x00006DE9 File Offset: 0x00004FE9
		public bool ShowMiManagerMenuButton { get; set; } = true;

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x00006DF2 File Offset: 0x00004FF2
		// (set) Token: 0x06000833 RID: 2099 RVA: 0x00006DFA File Offset: 0x00004FFA
		public bool IsHtmlHome { get; set; } = true;

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x00006E03 File Offset: 0x00005003
		// (set) Token: 0x06000835 RID: 2101 RVA: 0x00006E0B File Offset: 0x0000500B
		public bool IsShowTouchSoundSetting { get; set; } = true;

		// Token: 0x0400041E RID: 1054
		private const string sConfigFilename = "bst_config";

		// Token: 0x0400041F RID: 1055
		private static string sFilePath = string.Empty;

		// Token: 0x04000420 RID: 1056
		private static volatile FeatureManager sInstance;

		// Token: 0x04000421 RID: 1057
		private static object syncRoot = new object();
	}
}
