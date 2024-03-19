using System;
using System.Globalization;
using System.IO;

namespace BlueStacks.Common
{
	// Token: 0x02000071 RID: 113
	public static class Strings
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000A49F File Offset: 0x0000869F
		public static string OEMTag
		{
			get
			{
				return Strings.GetOemTag();
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000A4A8 File Offset: 0x000086A8
		public static string GetOemTag()
		{
			bool flag = Strings.sOemTag == null;
			if (flag)
			{
				Strings.sOemTag = ("bgp".Equals("bgp", StringComparison.InvariantCultureIgnoreCase) ? "" : "_bgp");
			}
			return Strings.sOemTag;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000A4F0 File Offset: 0x000086F0
		public static string OEMTagWithDefaultOemHandling
		{
			get
			{
				return Strings.GetOemTagWithoutDefaultOemCheck();
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000A4F8 File Offset: 0x000086F8
		public static string GetOemTagWithoutDefaultOemCheck()
		{
			return "_bgp";
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000A50F File Offset: 0x0000870F
		public static string BlueStacksDriverDisplayName
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "BlueStacks Hypervisor{0}", new object[]
				{
					Strings.GetOemTag()
				});
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x0000A52E File Offset: 0x0000872E
		public static string BlueStacksDriverFileName
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "BstkDrv{0}.sys", new object[]
				{
					Strings.GetOemTagWithoutDefaultOemCheck()
				});
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000A54D File Offset: 0x0000874D
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0000A554 File Offset: 0x00008754
		public static string CurrentDefaultVmName
		{
			get
			{
				return Strings.mCurrentDefaultVmName;
			}
			set
			{
				Strings.mCurrentDefaultVmName = value;
				bool flag = string.IsNullOrEmpty(Strings.mCurrentDefaultVmName);
				if (flag)
				{
					Strings.mCurrentDefaultVmName = "Android";
				}
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0000A582 File Offset: 0x00008782
		public static string DefaultWindowTitle
		{
			get
			{
				return "App Player";
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x0000A589 File Offset: 0x00008789
		// (set) Token: 0x060001CA RID: 458 RVA: 0x0000A59E File Offset: 0x0000879E
		public static string AppTitle
		{
			get
			{
				return (Strings.s_AppTitle != null) ? Strings.s_AppTitle : Strings.DefaultWindowTitle;
			}
			set
			{
				Strings.s_AppTitle = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001CB RID: 459 RVA: 0x0000A5A8 File Offset: 0x000087A8
		public static string BlueStacksSetupFolder
		{
			get
			{
				bool flag = string.IsNullOrEmpty(Strings.sBlueStacksSetupFolder);
				if (flag)
				{
					Strings.sBlueStacksSetupFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BlueStacksSetup");
				}
				return Strings.sBlueStacksSetupFolder;
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000A5E8 File Offset: 0x000087E8
		public static string AddOrdinal(int num)
		{
			bool flag = num < 0;
			string result;
			if (flag)
			{
				result = num.ToString(CultureInfo.InvariantCulture);
			}
			else
			{
				int num2 = num % 100;
				int num3 = num2;
				if (num3 - 11 > 2)
				{
					string text;
					switch (num % 10)
					{
					case 1:
						text = num.ToString() + "st";
						break;
					case 2:
						text = num.ToString() + "nd";
						break;
					case 3:
						text = num.ToString() + "rd";
						break;
					default:
						text = num.ToString() + "th";
						break;
					}
					string text2 = text;
					result = text2;
				}
				else
				{
					result = num.ToString() + "th";
				}
			}
			return result;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000A6B0 File Offset: 0x000088B0
		public static string GetPlayerLockName(string vmName, string oem = "bgp")
		{
			return string.Format(CultureInfo.InvariantCulture, "Global\\BlueStacks_{0}{1}_Player_Lock", new object[]
			{
				vmName,
				oem
			});
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000A6E0 File Offset: 0x000088E0
		public static string GetHDApkInstallerLockName(string vmName)
		{
			return string.Format(CultureInfo.InvariantCulture, "Global\\BlueStacks_HDApkInstaller_{0}{1}_Lock", new object[]
			{
				vmName,
				"bgp"
			});
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000A714 File Offset: 0x00008914
		public static string GetHDXapkInstallerLockName(string vmName)
		{
			return string.Format(CultureInfo.InvariantCulture, "Global\\BlueStacks_HDXapkInstaller_{0}{1}_Lock", new object[]
			{
				vmName,
				"bgp"
			});
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000A748 File Offset: 0x00008948
		public static string GetClientInstanceLockName(string vmName, string oem = "bgp")
		{
			return string.Format(CultureInfo.InvariantCulture, "Global\\BlueStacks_Client_Instance_{0}{1}_Lock", new object[]
			{
				vmName,
				oem
			});
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000A778 File Offset: 0x00008978
		public static string GetBlueStacksUILockNameOem(string oem = "bgp")
		{
			return "Global\\BlueStacks_BlueStacksUI_Lock" + oem;
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000A798 File Offset: 0x00008998
		public static string DisableHyperVSupportArticle
		{
			get
			{
				return "https://support.bluestacks.com/hc/articles/115004254383";
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000A7AF File Offset: 0x000089AF
		// (set) Token: 0x060001D4 RID: 468 RVA: 0x0000A7B6 File Offset: 0x000089B6
		public static double? TitleBarProductIconWidth { get; set; } = null;

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000A7BE File Offset: 0x000089BE
		// (set) Token: 0x060001D6 RID: 470 RVA: 0x0000A7C5 File Offset: 0x000089C5
		public static double? TitleBarTextMaxWidth { get; set; } = null;

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000A7CD File Offset: 0x000089CD
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x0000A7D4 File Offset: 0x000089D4
		public static string ProductDisplayName { get; set; } = "BlueStacks";

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000A7DC File Offset: 0x000089DC
		// (set) Token: 0x060001DA RID: 474 RVA: 0x0000A7E3 File Offset: 0x000089E3
		public static string TitleBarIconImageName { get; set; } = "ProductLogo";

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000A7EB File Offset: 0x000089EB
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000A7F2 File Offset: 0x000089F2
		public static string ProductTopBarDisplayName { get; set; } = Strings.ProductDisplayName;

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000A7FA File Offset: 0x000089FA
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000A801 File Offset: 0x00008A01
		public static string UninstallerTitleName { get; set; } = "Bluestacks Uninstaller";

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000A809 File Offset: 0x00008A09
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000A810 File Offset: 0x00008A10
		public static string UninstallCancelBtnColor { get; set; } = "White";

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000A818 File Offset: 0x00008A18
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x0000A81F File Offset: 0x00008A1F
		public static string MaterialDesignPrimaryBtnStyle { get; set; } = "MaterialDesignButton";

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000A827 File Offset: 0x00008A27
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0000A82E File Offset: 0x00008A2E
		public static int ForceDedicatedGPUDefaultValue { get; set; } = 1;

		// Token: 0x04000207 RID: 519
		public const string DefaultOEM = "bgp";

		// Token: 0x04000208 RID: 520
		public const string BGPOEM64 = "bgp64";

		// Token: 0x04000209 RID: 521
		public const string BGPHYPERV = "bgp64_hyperv";

		// Token: 0x0400020A RID: 522
		public const string MSI2 = "msi2";

		// Token: 0x0400020B RID: 523
		public const string MSIHYPERV = "msi64_hyperv";

		// Token: 0x0400020C RID: 524
		private static string sOemTag = null;

		// Token: 0x0400020D RID: 525
		public const string DefaultVmName = "Android";

		// Token: 0x0400020E RID: 526
		private static string mCurrentDefaultVmName = "Android";

		// Token: 0x0400020F RID: 527
		private static string s_AppTitle = null;

		// Token: 0x04000210 RID: 528
		private static string sBlueStacksSetupFolder = "";

		// Token: 0x04000211 RID: 529
		public static readonly string BlueStacksOldDriverName = "BstkDrv" + Strings.OEMTag;

		// Token: 0x04000212 RID: 530
		public const string BlueStacksDriverNameWithoutOEM = "BlueStacksDrv";

		// Token: 0x04000213 RID: 531
		public static readonly string BlueStacksDriverName = "BlueStacksDrv" + Strings.OEMTag;

		// Token: 0x04000214 RID: 532
		public const string BlueStacksIdRegistryPath = "Software\\BlueStacksInstaller";

		// Token: 0x04000215 RID: 533
		public static readonly string ClientRegistry32BitPath = "Software\\BlueStacksGP" + Strings.OEMTag;

		// Token: 0x04000216 RID: 534
		public static readonly string ClientRegistry64BitPath = "Software\\WOW6432Node\\BlueStacksGP" + Strings.OEMTag;

		// Token: 0x04000217 RID: 535
		public const string RegistryBaseKeyPathWithoutOEM = "Software\\BlueStacks";

		// Token: 0x04000218 RID: 536
		public static readonly string RegistryBaseKeyPath = "Software\\BlueStacks" + Strings.OEMTag;

		// Token: 0x04000219 RID: 537
		public static readonly string UninstallRegistryExportedFilePath = Path.Combine(Path.GetTempPath(), "BSTUninstall.reg");

		// Token: 0x0400021A RID: 538
		public static readonly string BGPKeyName = "BlueStacksGP" + Strings.OEMTag;

		// Token: 0x0400021B RID: 539
		public const string BlueStacksUIClosingLockName = "Global\\BlueStacks_BlueStacksUI_Closing_Lockbgp";

		// Token: 0x0400021C RID: 540
		public const string MultiInsLockName = "Global\\BlueStacks_MULTI_INS_Frontend_Lockbgp";

		// Token: 0x0400021D RID: 541
		public const string LogCollectorLockName = "Global\\BlueStacks_Log_Collector_Lockbgp";

		// Token: 0x0400021E RID: 542
		public const string HDAgentLockName = "Global\\BlueStacks_HDAgent_Lockbgp";

		// Token: 0x0400021F RID: 543
		public const string HDQuitMultiInstallLockName = "Global\\BlueStacks_HDQuitMultiInstall_Lockbgp";

		// Token: 0x04000220 RID: 544
		public const string ComRegistrarLockName = "Global\\BlueStacks_UnRegRegCom_Lockbgp";

		// Token: 0x04000221 RID: 545
		public const string GetBlueStacksUILockName = "Global\\BlueStacks_BlueStacksUI_Lockbgp";

		// Token: 0x04000222 RID: 546
		public const string DataManagerLock = "Global\\BlueStacks_Downloader_Lockbgp";

		// Token: 0x04000223 RID: 547
		public const string UninstallerLock = "Global\\BlueStacks_Uninstaller_Lockbgp";

		// Token: 0x04000224 RID: 548
		public const string MultiInstanceManagerLock = "Global\\BlueStacks_MultiInstanceManager_Lockbgp";

		// Token: 0x04000225 RID: 549
		public const string InstallerLockName = "Global\\BlueStacks_Installer_Lockbgp";

		// Token: 0x04000226 RID: 550
		public const string CloudWatcherLock = "Global\\BlueStacks_CloudWatcher_Lockbgp";

		// Token: 0x04000227 RID: 551
		public const string DiskCompactorLock = "Global\\BlueStacks_DiskCompactor_Lockbgp";

		// Token: 0x04000228 RID: 552
		public const string MicroInstallerLock = "Global\\BlueStacks_MicroInstaller_Lockbgp";

		// Token: 0x04000229 RID: 553
		public const string GetPlayerClosingLockName = "Global\\BlueStacks_PlayerClosing_Lockbgp";

		// Token: 0x0400022A RID: 554
		public const string InputMapperDLL = "HD-Imap-Native.dll";

		// Token: 0x0400022B RID: 555
		public const string MSIVibrationDLL = "MsiKBVibration.dll";

		// Token: 0x0400022C RID: 556
		public const string MSIVibration64DLL = "MsiKBVibration64.dll";

		// Token: 0x0400022D RID: 557
		public const string InstancePrefix = "Android_";

		// Token: 0x0400022E RID: 558
		public const string AppInstallFinished = "appinstallfinished";

		// Token: 0x0400022F RID: 559
		public const string AppInstallProgress = "appinstallprogress";

		// Token: 0x04000230 RID: 560
		public const string BluestacksMultiInstanceManager = "BlueStacks Multi-Instance Manager";

		// Token: 0x04000231 RID: 561
		public const string DiskCleanup = "Disk cleanup";

		// Token: 0x04000232 RID: 562
		public const string MultiInstanceManagerBinName = "HD-MultiInstanceManager.exe";

		// Token: 0x04000233 RID: 563
		public const string ProductIconName = "ProductLogo.ico";

		// Token: 0x04000234 RID: 564
		public const string ProductImageName = "ProductLogo.png";

		// Token: 0x04000235 RID: 565
		public const string GameIconFileName = "app_icon.ico";

		// Token: 0x04000236 RID: 566
		public const string ApkHandlerBaseKeyName = "BlueStacks.Apk";

		// Token: 0x04000237 RID: 567
		public const string ScriptFileWhichRemovesGamingEdition = "RemoveGamingFiles.bat";

		// Token: 0x04000238 RID: 568
		public const string UninstallCurrentVersionRegPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";

		// Token: 0x04000239 RID: 569
		public const string UninstallCurrentVersionRegPath32Bit = "SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall";

		// Token: 0x0400023A RID: 570
		public const string UninstallCurrentVersion32RegPath = "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall";

		// Token: 0x0400023B RID: 571
		public const string UninstallKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall";

		// Token: 0x0400023C RID: 572
		public const string ShowEnableVtPopupUrl = "showenablevtpopup";

		// Token: 0x0400023D RID: 573
		public const string SharePicUrl = "sharepic";

		// Token: 0x0400023E RID: 574
		public const string ShowGuidanceUrl = "controller_guidance_pressed";

		// Token: 0x0400023F RID: 575
		public const string AgentCrashReportUrl = "stats/agentcrashreport";

		// Token: 0x04000240 RID: 576
		public const string AppClickStatsUrl = "stats/appclickstats";

		// Token: 0x04000241 RID: 577
		public const string WebAppChannelClickStatsUrl = "stats/webappchannelclickstats";

		// Token: 0x04000242 RID: 578
		public const string FrontendStatusUpdateUrl = "FrontendStatusUpdate";

		// Token: 0x04000243 RID: 579
		public const string SearchAppStatsUrl = "stats/searchappstats";

		// Token: 0x04000244 RID: 580
		public const string AppInstallStatsUrl = "stats/appinstallstats";

		// Token: 0x04000245 RID: 581
		public const string SystemInfoStatsUrl = "stats/systeminfostats";

		// Token: 0x04000246 RID: 582
		public const string BootStatsUrl = "stats/bootstats";

		// Token: 0x04000247 RID: 583
		public const string TimelineStatsUrl = "stats/timelinestats4";

		// Token: 0x04000248 RID: 584
		public const string HomeScreenStatsUrl = "stats/homescreenstats";

		// Token: 0x04000249 RID: 585
		public const string BsInstallStatsUrl = "stats/bsinstallstats";

		// Token: 0x0400024A RID: 586
		public const string MiscellaneousStatsUrl = "/stats/miscellaneousstats";

		// Token: 0x0400024B RID: 587
		public const string KeyMappingUIStatsStatsUrl = "/stats/keymappinguistats";

		// Token: 0x0400024C RID: 588
		public const string ClientStatsUrl = "bs4/stats/clientstats";

		// Token: 0x0400024D RID: 589
		public const string UploadtobigqueryUrl = "bigquery/uploadtobigquery";

		// Token: 0x0400024E RID: 590
		public const string BtvFunnelStatsUrl = "stats/btvfunnelstats";

		// Token: 0x0400024F RID: 591
		public const string TroubleshooterStatsUrl = "stats/troubleshooterlogs";

		// Token: 0x04000250 RID: 592
		public const string GetCACodeUrl = "api/getcacode";

		// Token: 0x04000251 RID: 593
		public const string GetCountryUrl = "api/getcountryforip";

		// Token: 0x04000252 RID: 594
		public const string UploadDebugLogsUrl = "uploaddebuglogs";

		// Token: 0x04000253 RID: 595
		public const string UploadDebugLogsApkInstallFailureUrl = "logs/appinstallfailurelog";

		// Token: 0x04000254 RID: 596
		public const string UploadDebugLogsBootFailureUrl = "logs/bootfailurelog";

		// Token: 0x04000255 RID: 597
		public const string UploadDebugLogsCrashUrl = "logs/crashlog";

		// Token: 0x04000256 RID: 598
		public const string UserDataDir = "UserData";

		// Token: 0x04000257 RID: 599
		public const string UsersPath = "UsersPath";

		// Token: 0x04000258 RID: 600
		public const string UserProfile = "userprofile";

		// Token: 0x04000259 RID: 601
		public const string IdSeparator = "##";

		// Token: 0x0400025A RID: 602
		public const string UserDefinedDir = "UserDefinedDir";

		// Token: 0x0400025B RID: 603
		public const string StoreAppsDir = "App Stores";

		// Token: 0x0400025C RID: 604
		public const string IconsDir = "Icons";

		// Token: 0x0400025D RID: 605
		public const string CloudHost2 = "https://23.23.194.123";

		// Token: 0x0400025E RID: 606
		public const string CloudHost = "https://cloud.bluestacks.com";

		// Token: 0x0400025F RID: 607
		public const string LibraryName = "Apps";

		// Token: 0x04000260 RID: 608
		public const string BstPrefix = "Bst-";

		// Token: 0x04000261 RID: 609
		public const string GameManagerBannerImageDir = "sendappdisplayed";

		// Token: 0x04000262 RID: 610
		public const string SharedFolderName = "BstSharedFolder";

		// Token: 0x04000263 RID: 611
		public const string SharedFolder = "SharedFolder";

		// Token: 0x04000264 RID: 612
		public const string Library = "Library";

		// Token: 0x04000265 RID: 613
		public const string InputMapperFolderName = "InputMapper";

		// Token: 0x04000266 RID: 614
		public const string CaCodeBackUpFileName = "Bst_CaCode_Backup";

		// Token: 0x04000267 RID: 615
		public const string PCodeBackUpFileName = "Bst_PCode_Backup";

		// Token: 0x04000268 RID: 616
		public const string CaSelectorBackUpFileName = "Bst_CaSelector_Backup";

		// Token: 0x04000269 RID: 617
		public const string UserInfoBackupFileName = "Bst_UserInfo_Backup";

		// Token: 0x0400026A RID: 618
		public const string BlueStacksPackagePrefix = "com.bluestacks";

		// Token: 0x0400026B RID: 619
		public const string LatinImeId = "com.android.inputmethod.latin/.LatinIME";

		// Token: 0x0400026C RID: 620
		public const string FrontendPortBootParam = "WINDOWSFRONTEND";

		// Token: 0x0400026D RID: 621
		public const string AgentPortBootParam = "WINDOWSAGENT";

		// Token: 0x0400026E RID: 622
		public const string VMXBitIsOn = "Cannot run guest while VMX is in use";

		// Token: 0x0400026F RID: 623
		public const string InvalidOpCode = "invalid_op";

		// Token: 0x04000270 RID: 624
		public const string KernelPanic = "Kernel panic";

		// Token: 0x04000271 RID: 625
		public const string Ext4Error = ".*EXT4-fs error \\(device sd[a-b]1\\): (mb_free_blocks|ext4_mb_generate_buddy|ext4_lookup|.*deleted inode referenced):";

		// Token: 0x04000272 RID: 626
		public const string PgaCtlInitFailedString = "BlueStacks.Frontend.Interop.Opengl.GetPgaServerInitStatus()";

		// Token: 0x04000273 RID: 627
		public const string AppCrashInfoFile = "App_C_Info.txt";

		// Token: 0x04000274 RID: 628
		public const string LogDirName = "Logs";

		// Token: 0x04000275 RID: 629
		public const string AppNotInstalledString = "package not installed";

		// Token: 0x04000276 RID: 630
		public const string NetEaseReportProblemUrl = "http://gh.163.com/m";

		// Token: 0x04000277 RID: 631
		public const string NetEaseOpenBrowserString = "问题咨询";

		// Token: 0x04000278 RID: 632
		public const string InstallDirKeyName = "InstallDir";

		// Token: 0x04000279 RID: 633
		public const string StyleThemeStatsTag = "StyleAndThemeData";

		// Token: 0x0400027A RID: 634
		public const string ConfigParam = "32";

		// Token: 0x0400027B RID: 635
		public const string OemPrimaryInfo = "e";

		// Token: 0x0400027C RID: 636
		public const string WorldWideToggleDisabledAppListUrl = "http://cdn3.bluestacks.com/public/appsettings/ToggleDisableAppList.cfg";

		// Token: 0x0400027D RID: 637
		public const string TombStoneFilePrefix = "tombstone";

		// Token: 0x0400027E RID: 638
		public const string MultiInstanceStatsUrl = "stats/multiinstancestats";

		// Token: 0x0400027F RID: 639
		public const string VmManagerExe = "HD-VmManager.exe";

		// Token: 0x04000280 RID: 640
		public const string BluestacksServiceString = "BlueStacks Service";

		// Token: 0x04000281 RID: 641
		public const string ReportProblemWindowTitle = "BlueStacks Report Problem";

		// Token: 0x04000282 RID: 642
		public const string XapkHandlerBaseKeyName = "BlueStacks.Xapk";

		// Token: 0x04000283 RID: 643
		public const int AgentServerStartingPort = 2861;

		// Token: 0x04000284 RID: 644
		public const int HTTPServerPortRangeAgent = 10;

		// Token: 0x04000285 RID: 645
		public const int ClientServerStartingPort = 2871;

		// Token: 0x04000286 RID: 646
		public const int HTTPServerPortRangeClient = 10;

		// Token: 0x04000287 RID: 647
		public const int PlayerServerStartingPort = 2881;

		// Token: 0x04000288 RID: 648
		public const int VmMonitorServerStartingPort = 2921;

		// Token: 0x04000289 RID: 649
		public const int HTTPServerVmPortRange = 40;

		// Token: 0x0400028A RID: 650
		public const int MultiInstanceServerStartingPort = 2961;

		// Token: 0x0400028B RID: 651
		public const int MultiInstanceServerPortRange = 10;

		// Token: 0x0400028C RID: 652
		public const int DefaultBlueStacksSize = 2047;

		// Token: 0x0400028D RID: 653
		public const string ChannelsProdTwitchServerUrl = "https://cloud.bluestacks.com/btv/GetTwitchServers";

		// Token: 0x0400028E RID: 654
		public const string VideoTutorialUrl = "videoTutorial";

		// Token: 0x0400028F RID: 655
		public const string OnboardingUrl = "bs3/page/onboarding-tutorial";

		// Token: 0x04000290 RID: 656
		public const string GameFeaturePopupUrl = "bs3/page/game-feature-tutorial";

		// Token: 0x04000291 RID: 657
		public const string UtcConverterUrl = "bs3/page/utcConverter";

		// Token: 0x04000292 RID: 658
		public const string ComExceptionErrorString = "com exception";

		// Token: 0x04000293 RID: 659
		public const string BootFailedTimeoutString = "boot timeout exception";

		// Token: 0x04000294 RID: 660
		public const long MinimumRAMRequiredForInstallationInGB = 1L;

		// Token: 0x04000295 RID: 661
		public const long MinimumSpaceRequiredFreshInstallationInGB = 5L;

		// Token: 0x04000296 RID: 662
		public const long MinimumSpaceRequiredFreshInstallationInInstallDirMB = 500L;

		// Token: 0x04000297 RID: 663
		public const long MinimumSpaceRequiredFreshInstallationInMB = 5120L;

		// Token: 0x04000298 RID: 664
		public const string UninstallerFileName = "BlueStacksUninstaller.exe";

		// Token: 0x04000299 RID: 665
		public const string ArchInstallerFile64 = "64bit";

		// Token: 0x0400029A RID: 666
		public const string TestRollbackRegistryKeyName = "TestRollback";

		// Token: 0x0400029B RID: 667
		public const string TestRollbackFailRegistryKeyName = "TestRollbackFail";

		// Token: 0x0400029C RID: 668
		public const string UnifiedInstallStats = "/bs3/stats/unified_install_stats";

		// Token: 0x0400029D RID: 669
		public const string OEMConfigFileName = "Oem.cfg";

		// Token: 0x0400029E RID: 670
		public const string RunAppBinaryName = "HD-RunApp.exe";

		// Token: 0x0400029F RID: 671
		public const string BlueStacksBinaryName = "BlueStacks.exe";

		// Token: 0x040002A0 RID: 672
		public const string BlueStacksSetupFolderName = "BlueStacksSetup";

		// Token: 0x040002A1 RID: 673
		public const string GlModeString = "GlMode";

		// Token: 0x040002A2 RID: 674
		public const string MinimumClientVersionForUpgrade = "3.52.66.1905";

		// Token: 0x040002A3 RID: 675
		public const string MinimumEngineVersionForUpgrade = "2.52.66.8704";

		// Token: 0x040002A4 RID: 676
		public const string DiskCompactionToolMinSupportVersion = "4.60.00.0000";

		// Token: 0x040002A5 RID: 677
		public const string FirstIMapBuildVersion = "4.30.33.1590";

		// Token: 0x040002A6 RID: 678
		public const string FirstUnifiedInstallerBuildVersion = "4.20.21";

		// Token: 0x040002A7 RID: 679
		public const string CommonInstallUtilsZipFileName = "CommonInstallUtils.zip";

		// Token: 0x040002A8 RID: 680
		public const string RollbackCompletedStatString = "ROLLBACK_COMPLETED";

		// Token: 0x040002A9 RID: 681
		public const string HandleBinaryName = "HD-Handle.exe";

		// Token: 0x040002AA RID: 682
		public const string HandleRegistryKeyPath = "Software\\Sysinternals\\Handle";

		// Token: 0x040002AB RID: 683
		public const string HandleEULAKeyName = "EulaAccepted";

		// Token: 0x040002AC RID: 684
		public const string BootStrapperFileName = "Bootstrapper.exe";

		// Token: 0x040002AD RID: 685
		public const string TakeBackupURL = "Backup_and_Restore";

		// Token: 0x040002AE RID: 686
		public const string CloudImageTranslateUrl = "/translate/postimage";

		// Token: 0x040002AF RID: 687
		public const string RemoteAccessRequestString = "May I please have remote access?";

		// Token: 0x040002B0 RID: 688
		public const string UninstallRegistryFileName = "BSTUninstall.reg";

		// Token: 0x040002B1 RID: 689
		public const string BuildVersionForUpgradeFromParserVersion13To14 = "4.140.00.0000";

		// Token: 0x040002B2 RID: 690
		public const string MultiInstanceBuildUrl = "/bs4/getmultiinstancebuild?";

		// Token: 0x040002B3 RID: 691
		public const string MultiInstanceCheckUpgrade = "/bs4/check_upgrade?";

		// Token: 0x040002B4 RID: 692
		public const string RemoveDiskCommand = "removedisk";

		// Token: 0x040002B5 RID: 693
		public const string ResetSharedFolders = "resetSharedFolders";

		// Token: 0x040002B6 RID: 694
		public const string AnnouncementActionPopupSimple = "simple_popup";

		// Token: 0x040002B7 RID: 695
		public const string AnnouncementActionPopupRich = "rich_popup";

		// Token: 0x040002B8 RID: 696
		public const string AnnouncementActionPopupCenter = "center_popup";

		// Token: 0x040002B9 RID: 697
		public const string AnnouncementActionDownloadExecute = "download_execute";

		// Token: 0x040002BA RID: 698
		public const string AnnouncementActionSilentLogCollect = "silent_logcollect";

		// Token: 0x040002BB RID: 699
		public const string AnnouncementActionSilentExecute = "silent_execute";

		// Token: 0x040002BC RID: 700
		public const string AnnouncementActionSelfUpdate = "self_update";

		// Token: 0x040002BD RID: 701
		public const string AnnouncementActionWebURLGM = "web_url_gm";

		// Token: 0x040002BE RID: 702
		public const string AnnouncementActionWebURL = "web_url";

		// Token: 0x040002BF RID: 703
		public const string AnnouncementActionStartAndroidApp = "start_android_app";

		// Token: 0x040002C0 RID: 704
		public const string AnnouncementActionSilentInstall = "silent_install";

		// Token: 0x040002C1 RID: 705
		public const string AnnouncementActionDisablePermanently = "disable_permanently";

		// Token: 0x040002C2 RID: 706
		public const string AnnouncementActionNoPopup = "no_popup";

		// Token: 0x040002C3 RID: 707
		public const string CloudStuckAtBoot = "https://cloud.bluestacks.com/bs3/page/stuck_at_boot";

		// Token: 0x040002C4 RID: 708
		public const string CloudEnhancePerformance = "https://cloud.bluestacks.com/bs3/page/enhance_performance";

		// Token: 0x040002C5 RID: 709
		public const string CloudWhyGoogle = "https://cloud.bluestacks.com/bs3/page/why_google";

		// Token: 0x040002C6 RID: 710
		public const string CloudTroubleSigningIn = "https://cloud.bluestacks.com/bs3/page/trouble_signing";

		// Token: 0x040002C7 RID: 711
		public const string ExitPopupTagBoot = "exit_popup_boot";

		// Token: 0x040002C8 RID: 712
		public const string ExitPopupTagOTS = "exit_popup_ots";

		// Token: 0x040002C9 RID: 713
		public const string ExitPopupTagNoApp = "exit_popup_no_app";

		// Token: 0x040002CA RID: 714
		public const string ExitPopupEventShown = "popup_shown";

		// Token: 0x040002CB RID: 715
		public const string ExitPopupEventClosedButton = "popup_closed";

		// Token: 0x040002CC RID: 716
		public const string ExitPopupEventClosedCross = "click_action_close";

		// Token: 0x040002CD RID: 717
		public const string ExitPopupEventReturnBlueStacks = "click_action_return_bluestacks";

		// Token: 0x040002CE RID: 718
		public const string ExitPopupEventClosedContinue = "click_action_continue_bluestacks";

		// Token: 0x040002CF RID: 719
		public const string ExitPopupEventAutoHidden = "popup_auto_hidden";

		// Token: 0x040002D0 RID: 720
		public const string GL_MODE = "GlMode";

		// Token: 0x040002D1 RID: 721
		public const string BootPromotionImageName = "BootPromo";

		// Token: 0x040002D2 RID: 722
		public const string BackgroundPromotionImageName = "BackPromo";

		// Token: 0x040002D3 RID: 723
		public const string AppSuggestionImageName = "AppSuggestion";

		// Token: 0x040002D4 RID: 724
		public const string AppSuggestionRemovedFileName = "app_suggestion_removed";

		// Token: 0x040002D5 RID: 725
		public const string ClientPromoDir = "Promo";

		// Token: 0x040002D6 RID: 726
		public const string BGPDataDirFolderName = "Engine";

		// Token: 0x040002D7 RID: 727
		public const string EngineActivityUri = "engine_activity";

		// Token: 0x040002D8 RID: 728
		public const string EmulatorActivityUri = "emulator_activity";

		// Token: 0x040002D9 RID: 729
		public const string AppInstallUri = "app_install";

		// Token: 0x040002DA RID: 730
		public const string DeviceProfileListUrl = "get_device_profile_list";

		// Token: 0x040002DB RID: 731
		public const string AppActivityUri = "app_activity";

		// Token: 0x040002DC RID: 732
		public const string HelpArticles = "help_articles";

		// Token: 0x040002DD RID: 733
		public const string EnableVirtualization = "enable_virtualization";

		// Token: 0x040002DE RID: 734
		public const string CloudWatcherFolderName = "Helper";

		// Token: 0x040002DF RID: 735
		public const string CloudWatcherBinaryName = "BlueStacksHelper.exe";

		// Token: 0x040002E0 RID: 736
		public const string CloudWatcherTaskName = "BlueStacksHelper";

		// Token: 0x040002E1 RID: 737
		public const string CloudWatcherIdleTaskName = "BlueStacksHelperTask";

		// Token: 0x040002E2 RID: 738
		public const string DirectX = "DirectX";

		// Token: 0x040002E3 RID: 739
		public const string OpenGL = "OpenGL";

		// Token: 0x040002E4 RID: 740
		public const string LogCollectorZipFileName = "BlueStacks-Support.7z";

		// Token: 0x040002E5 RID: 741
		public const string BstClient = "BstClient";

		// Token: 0x040002E6 RID: 742
		public const string OperationSynchronization = "operation_synchronization";

		// Token: 0x040002E7 RID: 743
		public const string SetIframeUrl = "setIframeUrl";

		// Token: 0x040002E8 RID: 744
		public const string OnBrowserVisibilityChange = "onBrowserVisibilityChange";

		// Token: 0x040002E9 RID: 745
		public const string CloudAnnouncementTimeFormat = "dd/MM/yyyy HH:mm:ss";

		// Token: 0x040002EA RID: 746
		public const string GadgetDir = "Gadget";

		// Token: 0x040002EB RID: 747
		public const string MachineID = "MachineID";

		// Token: 0x040002EC RID: 748
		public const string VersionMachineID = "VersionMachineId_4.250.0.1070";

		// Token: 0x040002ED RID: 749
		public const string UserScripts = "UserScripts";

		// Token: 0x040002EE RID: 750
		public const string PcodeString = "pcode";

		// Token: 0x040002EF RID: 751
		public const string ROOT_VDI_UUID = "fca296ce-8268-4ed7-a57f-d32ec11ab304";

		// Token: 0x040002F0 RID: 752
		public const string LocaleFileNameFormat = "i18n.{0}.txt";

		// Token: 0x040002F1 RID: 753
		public const string MicroInstallerWindowTitle = "BlueStacks Installer";

		// Token: 0x040002F2 RID: 754
		public const string Bit64 = "x64 (64-bit)";

		// Token: 0x040002F3 RID: 755
		public const string Bit32 = "x86 (32-bit)";

		// Token: 0x040002F4 RID: 756
		public const int MaxRequiredAvailablePhysicalMemory = 2148;

		// Token: 0x040002F5 RID: 757
		public const string MediaFilesPathSetStatsTag = "MediaFilesPathSet";

		// Token: 0x040002F6 RID: 758
		public const string MediaFileSaveSuccess = "MediaFileSaveSuccess";

		// Token: 0x040002F7 RID: 759
		public const string VideoRecording = "VideoRecording";

		// Token: 0x040002F8 RID: 760
		public const string RestoreDefaultKeymappingStatsTag = "RestoreDefaultKeymappingClicked";

		// Token: 0x040002F9 RID: 761
		public const string ImportKeymappingStatsTag = "ImportKeymappingClicked";

		// Token: 0x040002FA RID: 762
		public const string ExportKeymappingStatsTag = "ExportKeymappingClicked";

		// Token: 0x040002FB RID: 763
		public const string ForceGPUBinaryName = "HD-ForceGPU.exe";

		// Token: 0x040002FC RID: 764
		public const string DMMEnableVtUrl = "http://help.dmm.com/-/detail/=/qid=45997/";

		// Token: 0x040002FD RID: 765
		public const string ObsBinaryName = "HD-OBS.exe";

		// Token: 0x040002FE RID: 766
		public const string NCSoftSharedMemoryName = "ngpmmf";

		// Token: 0x040002FF RID: 767
		public const string SidebarConfigFileName = "sidebar_config.json";

		// Token: 0x04000300 RID: 768
		public const string AnotherInstanceRunningPromptText1ForDMM = "同時に起動できないプログラムが既に動いています。";

		// Token: 0x04000301 RID: 769
		public const string AnotherInstanceRunningPromptText2ForDMM = "既に動いているプログラムを閉じて続行しますか？";

		// Token: 0x04000302 RID: 770
		public const string GlTextureFolder = "UCT";

		// Token: 0x04000303 RID: 771
		public const string ServiceInstallerBinaryName = "HD-ServiceInstaller.exe";

		// Token: 0x04000304 RID: 772
		public const string MacroRecorder = "MacroOperations";

		// Token: 0x04000305 RID: 773
		public const string GLCheckBinaryName = "HD-GLCheck.exe";

		// Token: 0x04000306 RID: 774
		public const string BTVFolderName = "BTV";

		// Token: 0x04000307 RID: 775
		public const string KeyboardShortcuts = "KeyboardShortcuts";

		// Token: 0x04000308 RID: 776
		public const string ComRegistrationBinaryName = "HD-ComRegistrar.exe";

		// Token: 0x04000309 RID: 777
		public const string CurrentParserVersion = "17";

		// Token: 0x0400030A RID: 778
		public const string MinimumParserVersion = "14";

		// Token: 0x0400030B RID: 779
		public const string InternetConnectivityCheckUrl = "http://connectivitycheck.gstatic.com/generate_204";

		// Token: 0x0400030C RID: 780
		public const string AbiValueString = "abivalue";

		// Token: 0x0400030D RID: 781
		public const string MemAllocator = "MEMALLOCATOR";

		// Token: 0x0400030E RID: 782
		public const string MacroCommunityUrlKey = "macro-share";

		// Token: 0x0400030F RID: 783
		public const string HyperVAndroidConfigName = "Android.json";

		// Token: 0x04000310 RID: 784
		public const string HyperVNetworkConfigName = "Android.Network.json";

		// Token: 0x04000311 RID: 785
		public const string HyperVEndpointConfigName = "Android.Endpoint.json";

		// Token: 0x04000312 RID: 786
		public const string HyperVAdminGroupName = "Hyper-V Administrators";

		// Token: 0x04000313 RID: 787
		public const int DefaultMaxBatchInstanceCreationCount = 5;

		// Token: 0x04000314 RID: 788
		public const string MOBATag = "MOBA";

		// Token: 0x04000315 RID: 789
		public const string DefaultScheme = "DefaultScheme";

		// Token: 0x04000316 RID: 790
		public const string SchemeChanged = "SchemeChanged";

		// Token: 0x04000317 RID: 791
		public const string GameControlBlurbTitle = "GameControlBlurb";

		// Token: 0x04000318 RID: 792
		public const string GuidanceVideoBlurbTitle = "GuidanceVideoBlurb";

		// Token: 0x04000319 RID: 793
		public const string FullScreenBlurbTitle = "FullScreenBlurb";

		// Token: 0x0400031A RID: 794
		public const string ViewGuideControlBlurbTitle = "ViewControlBlurb";

		// Token: 0x0400031B RID: 795
		public const string EcoModeBlurbTitle = "EcoModeBlurb";

		// Token: 0x0400031C RID: 796
		public const string SelectedGameSchemeBlurbTitle = "SelectedGameSchemeBlurb";

		// Token: 0x0400031D RID: 797
		public const string WinId = "winid";

		// Token: 0x0400031E RID: 798
		public const string SelectedSchemeName = "SelectedSchemeName";

		// Token: 0x0400031F RID: 799
		public const string OnScreenControlsBlurbTitle = "OnScreenControlsBlurb";

		// Token: 0x04000320 RID: 800
		public const string SourceAppCenter = "BSAppCenter";

		// Token: 0x04000321 RID: 801
		public const string DecreaseVolumeImageName = "decrease";

		// Token: 0x04000322 RID: 802
		public const string DecreaseVolumeDisableImageName = "decrease_disable";

		// Token: 0x04000323 RID: 803
		public const string IncreaseVolumeImageName = "increase";

		// Token: 0x04000324 RID: 804
		public const string IncreseVolumeDisableImageName = "increase_disable";

		// Token: 0x04000325 RID: 805
		public const string MuteVolumeImageName = "volume_switch_off";

		// Token: 0x04000326 RID: 806
		public const string UnmuteVolumeImageName = "volume_switch_on";

		// Token: 0x04000327 RID: 807
		public const string Custom = "Custom";

		// Token: 0x04000331 RID: 817
		public const string ProductMajorVersion = " 4";

		// Token: 0x020000DD RID: 221
		public static class VersionConstants
		{
			// Token: 0x040007E7 RID: 2023
			public const string NotificationModeVersion = "4.210.0.1000";

			// Token: 0x040007E8 RID: 2024
			public const string DesktopNotificationsForChatAppsVersion = "4.240.0.1000";
		}

		// Token: 0x020000DE RID: 222
		public static class EngineSettingsConfiguration
		{
			// Token: 0x040007E9 RID: 2025
			public const double HighendMachineRAMThreshold = 7782.4;

			// Token: 0x040007EA RID: 2026
			public const int HighendHighRAM = 4096;

			// Token: 0x040007EB RID: 2027
			public const int HighendHighRAMInGB = 4;

			// Token: 0x040007EC RID: 2028
			public const int HighCPU = 4;

			// Token: 0x040007ED RID: 2029
			public const int HighRAM = 3072;

			// Token: 0x040007EE RID: 2030
			public const int HighRAMInGB = 3;

			// Token: 0x040007EF RID: 2031
			public const int MediumCPU = 2;

			// Token: 0x040007F0 RID: 2032
			public const int MediumRAM = 2048;

			// Token: 0x040007F1 RID: 2033
			public const int MediumRAMInGB = 2;

			// Token: 0x040007F2 RID: 2034
			public const int LowCPU = 1;

			// Token: 0x040007F3 RID: 2035
			public const int LowRAM = 1024;

			// Token: 0x040007F4 RID: 2036
			public const int LowRAMInGB = 1;
		}
	}
}
