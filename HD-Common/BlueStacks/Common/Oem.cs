using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace BlueStacks.Common
{
	// Token: 0x02000130 RID: 304
	public sealed class Oem
	{
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x0002A5E4 File Offset: 0x000287E4
		public static Oem Instance
		{
			get
			{
				if (Oem.sInstance == null)
				{
					object obj = Oem.syncRoot;
					lock (obj)
					{
						if (Oem.sInstance == null)
						{
							Oem oem = new Oem();
							oem.LoadOem();
							Oem.sInstance = oem;
						}
					}
				}
				return Oem.sInstance;
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0002A644 File Offset: 0x00028844
		private void LoadOem()
		{
			try
			{
				string text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Oem.cfg");
				if (!File.Exists(text))
				{
					if (!File.Exists(Oem.CurrentOemFilePath))
					{
						throw new Exception(string.Format(CultureInfo.InvariantCulture, "{0} file not found", new object[]
						{
							"Oem.cfg"
						}));
					}
				}
				else
				{
					Oem.CurrentOemFilePath = text;
				}
				Logger.Info("Attempting to load {0} from path: {1}", new object[]
				{
					"Oem.cfg",
					Oem.CurrentOemFilePath
				});
				foreach (string text2 in File.ReadAllLines(Oem.CurrentOemFilePath))
				{
					if (text2.IndexOf("=", StringComparison.OrdinalIgnoreCase) != -1)
					{
						string[] array2 = text2.Split(new char[]
						{
							'='
						});
						this.mPropertiesDictionary[array2[0].Trim()] = array2[1].Trim();
					}
				}
				this.LoadProperties();
			}
			catch (Exception ex)
			{
				Logger.Error("An exception occured while loading Oem config file, loading default values: " + ex.Message);
				Oem.sInstance = new Oem();
			}
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x00008D62 File Offset: 0x00006F62
		private T GetObjectValueForKey<T>(string propertyName, T defaultValue)
		{
			if (this.mPropertiesDictionary.ContainsKey(propertyName))
			{
				return this.mPropertiesDictionary[propertyName].GetObjectOfType(defaultValue);
			}
			return defaultValue;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00008D86 File Offset: 0x00006F86
		private string GetStringValueOrDefault(string propertyName, string defaultValue)
		{
			if (this.mPropertiesDictionary.ContainsKey(propertyName))
			{
				return this.mPropertiesDictionary[propertyName];
			}
			return defaultValue;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0002A760 File Offset: 0x00028960
		private void LoadProperties()
		{
			this.WindowsOEMFeatures = this.GetObjectValueForKey<ulong>("WindowsOEMFeatures", 0UL);
			this.AppsOEMFeaturesBits = this.GetObjectValueForKey<ulong>("AppsOEMFeaturesBits", 0UL);
			this.PartnerControlBarHeight = this.GetObjectValueForKey<int>("PartnerControlBarHeight", 0);
			this.AndroidFeatureBits = this.GetObjectValueForKey<uint>("AndroidFeatureBits", 0U);
			this.SendAppClickStatsFromClient = this.GetObjectValueForKey<bool>("SendAppClickStatsFromClient", false);
			this.IsPixelParityToBeIgnored = this.GetObjectValueForKey<bool>("IsPixelParityToBeIgnored", false);
			this.IsShowVersionOnSysTrayToolTip = this.GetObjectValueForKey<bool>("IsShowVersionOnSysTrayToolTip", true);
			this.IsVTPopupEnabled = this.GetObjectValueForKey<bool>("IsVTPopupEnabled", false);
			this.IsUseFrontendBanner = this.GetObjectValueForKey<bool>("IsUseFrontendBanner", false);
			this.IsFrontendFormLocation6 = this.GetObjectValueForKey<bool>("IsFrontendFormLocation6", false);
			this.IsMessageBoxToBeDisplayed = this.GetObjectValueForKey<bool>("IsMessageBoxToBeDisplayed", true);
			this.IsResizeFrontendWindow = this.GetObjectValueForKey<bool>("IsResizeFrontendWindow", true);
			this.IsFrontendBorderHidden = this.GetObjectValueForKey<bool>("IsFrontendBorderHidden", false);
			this.IsOnlyStopButtonToBeAddedInContextMenuOFSysTray = this.GetObjectValueForKey<bool>("IsOnlyStopButtonToBeAddedInContextMenuOFSysTray", false);
			this.IsCountryChina = this.GetObjectValueForKey<bool>("IsCountryChina", false);
			this.IsLoadCACodeFromCloud = this.GetObjectValueForKey<bool>("IsLoadCACodeFromCloud", true);
			this.IsSendGameManagerRequest = this.GetObjectValueForKey<bool>("IsSendGameManagerRequest", false);
			this.IsHideMessageBoxIconInTaskBar = this.GetObjectValueForKey<bool>("IsHideMessageBoxIconInTaskBar", false);
			this.IsLefClickOnTrayIconLaunchPartner = this.GetObjectValueForKey<bool>("IsLefClickOnTrayIconLaunchPartner", false);
			this.IsGameManagerToBeStartedOnRunApp = this.GetObjectValueForKey<bool>("IsGameManagerToBeStartedOnRunApp", false);
			this.IsCreateDesktopIconForApp = this.GetObjectValueForKey<bool>("IsCreateDesktopIconForApp", false);
			this.IsDownloadIconFromWeb = this.GetObjectValueForKey<bool>("IsDownloadIconFromWeb", false);
			this.IsSysTrayIconTextToBeBlueStacks3 = this.GetObjectValueForKey<bool>("IsSysTrayIconTextToBeBlueStacks3", false);
			this.IsDownloadBstConfigWhileInstalling = this.GetObjectValueForKey<bool>("IsDownloadBstConfigWhileInstalling", true);
			this.IsOEMWithBGPClient = this.GetObjectValueForKey<bool>("IsOEMWithBGPClient", false);
			this.IsLaunchUIOnRunApp = this.GetObjectValueForKey<bool>("IsLaunchUIOnRunApp", false);
			this.IsRemoveAccountOnExit = this.GetObjectValueForKey<bool>("IsRemoveAccountOnExit", false);
			this.IsFormBorderStyleFixedSingle = this.GetObjectValueForKey<bool>("IsFormBorderStyleFixedSingle", false);
			this.CreateDesktopIcons = this.GetObjectValueForKey<bool>("CreateDesktopIcons", true);
			this.CreateMultiInstanceManagerIcon = this.GetObjectValueForKey<bool>("CreateMultiInstanceManagerIcon", false);
			this.IsBackupWarningVisible = this.GetObjectValueForKey<bool>("IsBackupWarningVisible", true);
			this.IsWriteRegistryInfoInFile = this.GetObjectValueForKey<bool>("IsWriteRegistryInfoInFile", false);
			this.IsCreateInstallApkRegistry = this.GetObjectValueForKey<bool>("IsCreateInstallApkRegistry", true);
			this.IsCreateDesktopAndStartMenuShortcut = this.GetObjectValueForKey<bool>("IsCreateDesktopAndStartMenuShortcut", true);
			this.IsDragDropEnabled = this.GetObjectValueForKey<bool>("IsDragDropEnabled", true);
			this.IsProductBeta = this.GetObjectValueForKey<bool>("IsProductBeta", false);
			this.IsSwitchToAndroidHome = this.GetObjectValueForKey<bool>("IsSwitchToAndroidHome", false);
			this.IsResetSigninRegistryForFreshVM = this.GetObjectValueForKey<bool>("IsResetSigninRegistryForFreshVM", true);
			this.CreateUninstallEntry = this.GetObjectValueForKey<bool>("CreateUninstallEntry", true);
			this.CheckForAGAInInstaller = this.GetObjectValueForKey<bool>("CheckForAGAInInstaller", false);
			this.IsReportExeAppCrashLogs = this.GetObjectValueForKey<bool>("IsReportExeAppCrashLogs", false);
			this.IsPortableInstaller = this.GetObjectValueForKey<bool>("IsPortableInstaller", false);
			this.IsAndroid64Bit = this.GetObjectValueForKey<bool>("IsAndroid64Bit", false);
			this.ChangePerformanceSettingOnInstanceCountChange = this.GetObjectValueForKey<bool>("ChangePerformanceSettingOnInstanceCountChange", false);
			this.SetDefaultPreferenceHigh = this.GetObjectValueForKey<bool>("SetDefaultPreferenceHigh", false);
			this.IsShowMimPromotionalTeaser = this.GetObjectValueForKey<bool>("IsShowMimPromotionalTeaser", true);
			this.IsShowMimHelpIcon = this.GetObjectValueForKey<bool>("IsShowMimHelpIcon", true);
			this.IsShowMimUpdateIcon = this.GetObjectValueForKey<bool>("IsShowMimUpdateIcon", true);
			this.IsShowMimOtherOEM = this.GetObjectValueForKey<bool>("IsShowMimOtherOEM", true);
			this.IsShowGameBlurb = this.GetObjectValueForKey<bool>("IsShowGameBlurb", true);
			this.MaxBatchInstanceCreationCount = this.GetObjectValueForKey<int>("MaxBatchInstanceCreationCount", 5);
			this.MimAllowPerformanceResetForInstanceCreation = this.GetObjectValueForKey<bool>("MimAllowPerformanceResetForInstanceCreation", false);
			this.MimHighPerformanceInstanceMaxCount = this.GetObjectValueForKey<int>("MimHighPerformanceInstanceMaxCount", int.MaxValue);
			this.MimMediumPerformanceInstanceMaxCount = this.GetObjectValueForKey<int>("MimMediumPerformanceInstanceMaxCount", int.MaxValue);
			this.MsgWindowClassName = this.GetStringValueOrDefault("MsgWindowClassName", null);
			this.MsgWindowTitle = this.GetStringValueOrDefault("MsgWindowTitle", null);
			this.OEM = this.GetStringValueOrDefault("OEM", "gamemanager");
			this.DNSValue = this.GetStringValueOrDefault("DNSValue", "8.8.8.8");
			this.DNS2Value = this.GetStringValueOrDefault("DNS2Value", "10.0.2.3");
			this.mDefaultTitle = this.GetStringValueOrDefault("DefaultTitle", "DefaultTitle");
			this.DesktopShortcutFileName = this.GetStringValueOrDefault("DesktopShortcutFileName", "");
			this.mBlueStacksApkHandlerTitle = this.GetStringValueOrDefault("BlueStacksApkHandlerTitle", "BlueStacksApkHandlerTitle");
			this.CommonAppTitleText = this.GetStringValueOrDefault("CommonAppTitleText", "BlueStacks Android Plugin");
			this.mSnapShotShareString = this.GetStringValueOrDefault("SnapShotShareString", "SnapShotShareString");
			this.DMMApiPrefix = this.GetStringValueOrDefault("DMMApiPrefix", string.Empty);
			this.ControlPanelDisplayName = this.GetStringValueOrDefault("ControlPanelDisplayName", "BlueStacks App Player");
			this.SendCrashLogForApkWithString = this.GetStringValueOrDefault("SendCrashLogForApkWithString", "");
			this.MultiInstanceManagerShortcutFileName = this.GetStringValueOrDefault("MultiInstanceManagerShortcutFileName", "BlueStacks Multi-Instance Manager.lnk");
			this.IsShowAllInstancesToBeAddedInContextMenuOfSysTray = this.GetObjectValueForKey<bool>("IsShowAllInstancesToBeAddedInContextMenuOfSysTray", false);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00008DA4 File Offset: 0x00006FA4
		public string GetTitle(string title)
		{
			if (this.DefaultTitle.Equals("DefaultTitle", StringComparison.OrdinalIgnoreCase))
			{
				return title;
			}
			return this.DefaultTitle;
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x00008DC1 File Offset: 0x00006FC1
		// (set) Token: 0x060009EC RID: 2540 RVA: 0x00008DC9 File Offset: 0x00006FC9
		public ulong WindowsOEMFeatures { get; set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x00008DD2 File Offset: 0x00006FD2
		// (set) Token: 0x060009EE RID: 2542 RVA: 0x00008DDA File Offset: 0x00006FDA
		public ulong AppsOEMFeaturesBits { get; set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00008DE3 File Offset: 0x00006FE3
		// (set) Token: 0x060009F0 RID: 2544 RVA: 0x00008DEB File Offset: 0x00006FEB
		public string MsgWindowClassName { get; set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x00008DF4 File Offset: 0x00006FF4
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x00008DFC File Offset: 0x00006FFC
		public string MsgWindowTitle { get; set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x00008E05 File Offset: 0x00007005
		// (set) Token: 0x060009F4 RID: 2548 RVA: 0x00008E0D File Offset: 0x0000700D
		public uint AndroidFeatureBits { get; set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x00008E16 File Offset: 0x00007016
		// (set) Token: 0x060009F6 RID: 2550 RVA: 0x00008E1E File Offset: 0x0000701E
		public bool SendAppClickStatsFromClient { get; set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x00008E27 File Offset: 0x00007027
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x00008E2F File Offset: 0x0000702F
		public bool IsPixelParityToBeIgnored { get; set; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x00008E38 File Offset: 0x00007038
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x00008E40 File Offset: 0x00007040
		public bool IsShowVersionOnSysTrayToolTip { get; set; } = true;

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x00008E49 File Offset: 0x00007049
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x00008E51 File Offset: 0x00007051
		public bool IsVTPopupEnabled { get; set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x00008E5A File Offset: 0x0000705A
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x00008E62 File Offset: 0x00007062
		public bool IsUseFrontendBanner { get; set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00008E6B File Offset: 0x0000706B
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x00008E73 File Offset: 0x00007073
		public bool IsFrontendFormLocation6 { get; set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00008E7C File Offset: 0x0000707C
		// (set) Token: 0x06000A02 RID: 2562 RVA: 0x00008E84 File Offset: 0x00007084
		public bool IsMessageBoxToBeDisplayed { get; set; } = true;

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00008E8D File Offset: 0x0000708D
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x00008E95 File Offset: 0x00007095
		public int PartnerControlBarHeight { get; set; }

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00008E9E File Offset: 0x0000709E
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x00008EA6 File Offset: 0x000070A6
		public bool IsResizeFrontendWindow { get; set; } = true;

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00008EAF File Offset: 0x000070AF
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x00008EB7 File Offset: 0x000070B7
		public bool IsFrontendBorderHidden { get; set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00008EC0 File Offset: 0x000070C0
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x00008EC8 File Offset: 0x000070C8
		public bool IsOnlyStopButtonToBeAddedInContextMenuOFSysTray { get; set; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x00008ED1 File Offset: 0x000070D1
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x00008ED9 File Offset: 0x000070D9
		public bool IsCountryChina { get; set; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00008EE2 File Offset: 0x000070E2
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x00008EEA File Offset: 0x000070EA
		public bool IsLoadCACodeFromCloud { get; set; } = true;

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x00008EF3 File Offset: 0x000070F3
		// (set) Token: 0x06000A10 RID: 2576 RVA: 0x00008EFB File Offset: 0x000070FB
		public bool IsSendGameManagerRequest { get; set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00008F04 File Offset: 0x00007104
		// (set) Token: 0x06000A12 RID: 2578 RVA: 0x00008F0C File Offset: 0x0000710C
		public bool IsHideMessageBoxIconInTaskBar { get; set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x00008F15 File Offset: 0x00007115
		// (set) Token: 0x06000A14 RID: 2580 RVA: 0x00008F1D File Offset: 0x0000711D
		public bool IsLefClickOnTrayIconLaunchPartner { get; set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x00008F26 File Offset: 0x00007126
		// (set) Token: 0x06000A16 RID: 2582 RVA: 0x00008F2E File Offset: 0x0000712E
		public bool IsGameManagerToBeStartedOnRunApp { get; set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x00008F37 File Offset: 0x00007137
		// (set) Token: 0x06000A18 RID: 2584 RVA: 0x00008F3F File Offset: 0x0000713F
		public bool IsCreateDesktopIconForApp { get; set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x00008F48 File Offset: 0x00007148
		// (set) Token: 0x06000A1A RID: 2586 RVA: 0x00008F50 File Offset: 0x00007150
		public bool IsDownloadBstConfigWhileInstalling { get; set; } = true;

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x00008F59 File Offset: 0x00007159
		// (set) Token: 0x06000A1C RID: 2588 RVA: 0x00008F61 File Offset: 0x00007161
		public bool IsDownloadIconFromWeb { get; set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x00008F6A File Offset: 0x0000716A
		// (set) Token: 0x06000A1E RID: 2590 RVA: 0x00008F72 File Offset: 0x00007172
		public bool IsSysTrayIconTextToBeBlueStacks3 { get; set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00008F7B File Offset: 0x0000717B
		// (set) Token: 0x06000A20 RID: 2592 RVA: 0x00008F83 File Offset: 0x00007183
		public bool IsOEMWithBGPClient { get; set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x00008F8C File Offset: 0x0000718C
		// (set) Token: 0x06000A22 RID: 2594 RVA: 0x00008F94 File Offset: 0x00007194
		public bool IsLaunchUIOnRunApp { get; set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x00008F9D File Offset: 0x0000719D
		// (set) Token: 0x06000A24 RID: 2596 RVA: 0x00008FA5 File Offset: 0x000071A5
		public bool IsRemoveAccountOnExit { get; set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x00008FAE File Offset: 0x000071AE
		// (set) Token: 0x06000A26 RID: 2598 RVA: 0x00008FB6 File Offset: 0x000071B6
		public bool IsFormBorderStyleFixedSingle { get; set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x00008FBF File Offset: 0x000071BF
		// (set) Token: 0x06000A28 RID: 2600 RVA: 0x00008FC7 File Offset: 0x000071C7
		public bool IsBackupWarningVisible { get; set; } = true;

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x00008FD0 File Offset: 0x000071D0
		// (set) Token: 0x06000A2A RID: 2602 RVA: 0x00008FD8 File Offset: 0x000071D8
		public bool IsWriteRegistryInfoInFile { get; set; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x00008FE1 File Offset: 0x000071E1
		// (set) Token: 0x06000A2C RID: 2604 RVA: 0x00008FE9 File Offset: 0x000071E9
		public bool IsCreateInstallApkRegistry { get; set; } = true;

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00008FF2 File Offset: 0x000071F2
		// (set) Token: 0x06000A2E RID: 2606 RVA: 0x00008FFA File Offset: 0x000071FA
		public bool IsCreateDesktopAndStartMenuShortcut { get; set; } = true;

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x00009003 File Offset: 0x00007203
		// (set) Token: 0x06000A30 RID: 2608 RVA: 0x0000900B File Offset: 0x0000720B
		public bool IsDragDropEnabled { get; set; } = true;

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00009014 File Offset: 0x00007214
		// (set) Token: 0x06000A32 RID: 2610 RVA: 0x0000901C File Offset: 0x0000721C
		public bool IsProductBeta { get; set; }

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x00009025 File Offset: 0x00007225
		// (set) Token: 0x06000A34 RID: 2612 RVA: 0x0000902D File Offset: 0x0000722D
		public bool IsResetSigninRegistryForFreshVM { get; set; } = true;

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x00009036 File Offset: 0x00007236
		// (set) Token: 0x06000A36 RID: 2614 RVA: 0x0000903E File Offset: 0x0000723E
		public bool IsReportExeAppCrashLogs { get; set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x00009047 File Offset: 0x00007247
		// (set) Token: 0x06000A38 RID: 2616 RVA: 0x0000904F File Offset: 0x0000724F
		public bool CreateUninstallEntry { get; private set; } = true;

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x00009058 File Offset: 0x00007258
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x00009060 File Offset: 0x00007260
		public bool CheckForAGAInInstaller { get; private set; }

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x00009069 File Offset: 0x00007269
		// (set) Token: 0x06000A3C RID: 2620 RVA: 0x00009071 File Offset: 0x00007271
		public bool CreateDesktopIcons { get; set; } = true;

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0000907A File Offset: 0x0000727A
		// (set) Token: 0x06000A3E RID: 2622 RVA: 0x00009082 File Offset: 0x00007282
		public bool CreateMultiInstanceManagerIcon { get; set; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0000908B File Offset: 0x0000728B
		// (set) Token: 0x06000A40 RID: 2624 RVA: 0x00009093 File Offset: 0x00007293
		public bool IsSwitchToAndroidHome { get; set; }

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x0000909C File Offset: 0x0000729C
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x000090A4 File Offset: 0x000072A4
		public bool IsPortableInstaller { get; set; }

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x000090AD File Offset: 0x000072AD
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x000090B5 File Offset: 0x000072B5
		public bool IsAndroid64Bit { get; set; }

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x000090BE File Offset: 0x000072BE
		// (set) Token: 0x06000A46 RID: 2630 RVA: 0x000090C6 File Offset: 0x000072C6
		public bool ChangePerformanceSettingOnInstanceCountChange { get; set; }

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x000090CF File Offset: 0x000072CF
		// (set) Token: 0x06000A48 RID: 2632 RVA: 0x000090D7 File Offset: 0x000072D7
		public bool SetDefaultPreferenceHigh { get; set; }

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x000090E0 File Offset: 0x000072E0
		// (set) Token: 0x06000A4A RID: 2634 RVA: 0x000090E8 File Offset: 0x000072E8
		public bool IsShowGameBlurb { get; set; } = true;

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x000090F1 File Offset: 0x000072F1
		// (set) Token: 0x06000A4C RID: 2636 RVA: 0x000090F9 File Offset: 0x000072F9
		public string OEM { get; set; } = "gamemanager";

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x00009102 File Offset: 0x00007302
		// (set) Token: 0x06000A4E RID: 2638 RVA: 0x0000910A File Offset: 0x0000730A
		public string DNSValue { get; set; } = "8.8.8.8";

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00009113 File Offset: 0x00007313
		// (set) Token: 0x06000A50 RID: 2640 RVA: 0x0000911B File Offset: 0x0000731B
		public string DNS2Value { get; set; } = "10.0.2.3";

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00009124 File Offset: 0x00007324
		// (set) Token: 0x06000A52 RID: 2642 RVA: 0x00009161 File Offset: 0x00007361
		public string DefaultTitle
		{
			get
			{
				if (string.IsNullOrEmpty(this.mDefaultTitle) || this.mDefaultTitle.Equals("DefaultTitle", StringComparison.OrdinalIgnoreCase))
				{
					this.mDefaultTitle = LocaleStrings.GetLocalizedString("DefaultTitle", "");
				}
				return this.mDefaultTitle;
			}
			set
			{
				this.mDefaultTitle = value;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0000916A File Offset: 0x0000736A
		// (set) Token: 0x06000A54 RID: 2644 RVA: 0x00009172 File Offset: 0x00007372
		public string DesktopShortcutFileName { get; set; } = "";

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x0000917B File Offset: 0x0000737B
		// (set) Token: 0x06000A56 RID: 2646 RVA: 0x00009183 File Offset: 0x00007383
		public string MultiInstanceManagerShortcutFileName { get; set; } = "BlueStacks Multi-Instance Manager.lnk";

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x0000918C File Offset: 0x0000738C
		// (set) Token: 0x06000A58 RID: 2648 RVA: 0x000091C9 File Offset: 0x000073C9
		public string BlueStacksApkHandlerTitle
		{
			get
			{
				if (string.IsNullOrEmpty(this.mBlueStacksApkHandlerTitle) || this.mBlueStacksApkHandlerTitle.Equals("BlueStacksApkHandlerTitle", StringComparison.OrdinalIgnoreCase))
				{
					this.mBlueStacksApkHandlerTitle = LocaleStrings.GetLocalizedString("STRING_BLUESTACKS_APK_HANDLER_TITLE", "");
				}
				return this.mBlueStacksApkHandlerTitle;
			}
			set
			{
				this.mBlueStacksApkHandlerTitle = value;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x000091D2 File Offset: 0x000073D2
		// (set) Token: 0x06000A5A RID: 2650 RVA: 0x000091DA File Offset: 0x000073DA
		public string CommonAppTitleText { get; set; } = "BlueStacks Android Plugin";

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x000091E3 File Offset: 0x000073E3
		// (set) Token: 0x06000A5C RID: 2652 RVA: 0x00009220 File Offset: 0x00007420
		public string SnapShotShareString
		{
			get
			{
				if (string.IsNullOrEmpty(this.mSnapShotShareString) || this.mSnapShotShareString.Equals("SnapShotShareString", StringComparison.OrdinalIgnoreCase))
				{
					this.mSnapShotShareString = LocaleStrings.GetLocalizedString("STRING_SNAPSHOT_SHARE_STRING", "");
				}
				return this.mSnapShotShareString;
			}
			set
			{
				this.mSnapShotShareString = value;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x00009229 File Offset: 0x00007429
		// (set) Token: 0x06000A5E RID: 2654 RVA: 0x00009231 File Offset: 0x00007431
		public string DMMApiPrefix { get; set; } = string.Empty;

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x0000923A File Offset: 0x0000743A
		// (set) Token: 0x06000A60 RID: 2656 RVA: 0x00009242 File Offset: 0x00007442
		public string ControlPanelDisplayName { get; set; } = "BlueStacks App Player";

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0000924B File Offset: 0x0000744B
		// (set) Token: 0x06000A62 RID: 2658 RVA: 0x00009253 File Offset: 0x00007453
		public string SendCrashLogForApkWithString { get; set; } = "";

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0000925C File Offset: 0x0000745C
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x00009264 File Offset: 0x00007464
		public bool IsShowMimPromotionalTeaser { get; set; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x0000926D File Offset: 0x0000746D
		// (set) Token: 0x06000A66 RID: 2662 RVA: 0x00009275 File Offset: 0x00007475
		public bool IsShowMimUpdateIcon { get; set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x0000927E File Offset: 0x0000747E
		// (set) Token: 0x06000A68 RID: 2664 RVA: 0x00009286 File Offset: 0x00007486
		public bool IsShowMimHelpIcon { get; set; }

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0000928F File Offset: 0x0000748F
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x00009297 File Offset: 0x00007497
		public bool IsShowMimOtherOEM { get; set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x000092A0 File Offset: 0x000074A0
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x000092A8 File Offset: 0x000074A8
		public bool MimAllowPerformanceResetForInstanceCreation { get; set; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x000092B1 File Offset: 0x000074B1
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x000092B9 File Offset: 0x000074B9
		public int MimHighPerformanceInstanceMaxCount { get; set; } = int.MaxValue;

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x000092C2 File Offset: 0x000074C2
		// (set) Token: 0x06000A70 RID: 2672 RVA: 0x000092CA File Offset: 0x000074CA
		public int MimMediumPerformanceInstanceMaxCount { get; set; } = int.MaxValue;

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x000092D3 File Offset: 0x000074D3
		// (set) Token: 0x06000A72 RID: 2674 RVA: 0x000092DB File Offset: 0x000074DB
		public int MaxBatchInstanceCreationCount { get; set; } = 5;

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x000092E4 File Offset: 0x000074E4
		// (set) Token: 0x06000A74 RID: 2676 RVA: 0x000092EC File Offset: 0x000074EC
		public bool IsShowAllInstancesToBeAddedInContextMenuOfSysTray { get; set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00006E92 File Offset: 0x00005092
		public static bool IsOEMDmm
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x000092F5 File Offset: 0x000074F5
		// (set) Token: 0x06000A77 RID: 2679 RVA: 0x000092FC File Offset: 0x000074FC
		public static string CurrentOemFilePath { get; set; } = Path.Combine(RegistryStrings.DataDir, "Oem.cfg");

		// Token: 0x04000504 RID: 1284
		private static volatile Oem sInstance;

		// Token: 0x04000505 RID: 1285
		private static object syncRoot = new object();

		// Token: 0x04000506 RID: 1286
		public const string sFileName = "Oem.cfg";

		// Token: 0x04000507 RID: 1287
		private Dictionary<string, string> mPropertiesDictionary = new Dictionary<string, string>();

		// Token: 0x0400053B RID: 1339
		private string mDefaultTitle = "DefaultTitle";

		// Token: 0x0400053E RID: 1342
		private string mBlueStacksApkHandlerTitle = "BlueStacksApkHandlerTitle";

		// Token: 0x04000540 RID: 1344
		private string mSnapShotShareString = "SnapShotShareString";
	}
}
