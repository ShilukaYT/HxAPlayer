using System;
using System.Globalization;
using System.IO;

namespace BlueStacks.Common
{
	// Token: 0x02000224 RID: 548
	public static class Strings
	{
		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x0000E6A8 File Offset: 0x0000C8A8
		public static string OEMTag
		{
			get
			{
				return Strings.GetOemTag();
			}
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0000E6AF File Offset: 0x0000C8AF
		public static string GetOemTag()
		{
			if (Strings.sOemTag == null)
			{
				Strings.sOemTag = ("bgp".Equals("bgp", StringComparison.InvariantCultureIgnoreCase) ? "" : "_bgp");
			}
			return Strings.sOemTag;
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x0000E6E0 File Offset: 0x0000C8E0
		public static string OEMTagWithDefaultOemHandling
		{
			get
			{
				return Strings.GetOemTagWithoutDefaultOemCheck();
			}
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0000E6E7 File Offset: 0x0000C8E7
		public static string GetOemTagWithoutDefaultOemCheck()
		{
			return "_bgp";
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x0000E6EE File Offset: 0x0000C8EE
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

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x0000E70D File Offset: 0x0000C90D
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

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x0000E72C File Offset: 0x0000C92C
		// (set) Token: 0x06001114 RID: 4372 RVA: 0x0000E733 File Offset: 0x0000C933
		public static string CurrentDefaultVmName
		{
			get
			{
				return Strings.mCurrentDefaultVmName;
			}
			set
			{
				Strings.mCurrentDefaultVmName = value;
				if (string.IsNullOrEmpty(Strings.mCurrentDefaultVmName))
				{
					Strings.mCurrentDefaultVmName = "Android";
				}
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x0000E751 File Offset: 0x0000C951
		public static string DefaultWindowTitle
		{
			get
			{
				return "App Player";
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x0000E758 File Offset: 0x0000C958
		// (set) Token: 0x06001117 RID: 4375 RVA: 0x0000E76C File Offset: 0x0000C96C
		public static string AppTitle
		{
			get
			{
				if (Strings.s_AppTitle == null)
				{
					return Strings.DefaultWindowTitle;
				}
				return Strings.s_AppTitle;
			}
			set
			{
				Strings.s_AppTitle = value;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x0000E774 File Offset: 0x0000C974
		public static string BlueStacksSetupFolder
		{
			get
			{
				if (string.IsNullOrEmpty(Strings.sBlueStacksSetupFolder))
				{
					Strings.sBlueStacksSetupFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BlueStacksSetup");
				}
				return Strings.sBlueStacksSetupFolder;
			}
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00040C0C File Offset: 0x0003EE0C
		public static string AddOrdinal(int num)
		{
			if (num < 0)
			{
				return num.ToString(CultureInfo.InvariantCulture);
			}
			int num2 = num % 100;
			if (num2 - 11 <= 2)
			{
				return num.ToString() + "th";
			}
			string result;
			switch (num % 10)
			{
			case 1:
				result = num.ToString() + "st";
				break;
			case 2:
				result = num.ToString() + "nd";
				break;
			case 3:
				result = num.ToString() + "rd";
				break;
			default:
				result = num.ToString() + "th";
				break;
			}
			return result;
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0000E79D File Offset: 0x0000C99D
		public static string GetPlayerLockName(string vmName, string oem = "bgp")
		{
			return string.Format(CultureInfo.InvariantCulture, "Global\\BlueStacks_{0}{1}_Player_Lock", new object[]
			{
				vmName,
				oem
			});
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0000E7BC File Offset: 0x0000C9BC
		public static string GetHDApkInstallerLockName(string vmName)
		{
			return string.Format(CultureInfo.InvariantCulture, "Global\\BlueStacks_HDApkInstaller_{0}{1}_Lock", new object[]
			{
				vmName,
				"bgp"
			});
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0000E7DF File Offset: 0x0000C9DF
		public static string GetHDXapkInstallerLockName(string vmName)
		{
			return string.Format(CultureInfo.InvariantCulture, "Global\\BlueStacks_HDXapkInstaller_{0}{1}_Lock", new object[]
			{
				vmName,
				"bgp"
			});
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0000E802 File Offset: 0x0000CA02
		public static string GetClientInstanceLockName(string vmName, string oem = "bgp")
		{
			return string.Format(CultureInfo.InvariantCulture, "Global\\BlueStacks_Client_Instance_{0}{1}_Lock", new object[]
			{
				vmName,
				oem
			});
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0000E821 File Offset: 0x0000CA21
		public static string GetBlueStacksUILockNameOem(string oem = "bgp")
		{
			return "Global\\BlueStacks_BlueStacksUI_Lock" + oem;
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x0000E82E File Offset: 0x0000CA2E
		public static string DisableHyperVSupportArticle
		{
			get
			{
				return "https://support.bluestacks.com/hc/articles/115004254383";
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x0000E835 File Offset: 0x0000CA35
		// (set) Token: 0x06001121 RID: 4385 RVA: 0x0000E83C File Offset: 0x0000CA3C
		public static double? TitleBarProductIconWidth { get; set; } = null;

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x0000E844 File Offset: 0x0000CA44
		// (set) Token: 0x06001123 RID: 4387 RVA: 0x0000E84B File Offset: 0x0000CA4B
		public static double? TitleBarTextMaxWidth { get; set; } = null;

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x0000E853 File Offset: 0x0000CA53
		// (set) Token: 0x06001125 RID: 4389 RVA: 0x0000E85A File Offset: 0x0000CA5A
		public static string ProductDisplayName { get; set; } = "HxA Player";

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x0000E862 File Offset: 0x0000CA62
		// (set) Token: 0x06001127 RID: 4391 RVA: 0x0000E869 File Offset: 0x0000CA69
		public static string TitleBarIconImageName { get; set; } = "ProductLogo";

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x0000E871 File Offset: 0x0000CA71
		// (set) Token: 0x06001129 RID: 4393 RVA: 0x0000E878 File Offset: 0x0000CA78
		public static string ProductTopBarDisplayName { get; set; } = Strings.ProductDisplayName;

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x0000E880 File Offset: 0x0000CA80
		// (set) Token: 0x0600112B RID: 4395 RVA: 0x0000E887 File Offset: 0x0000CA87
		public static string UninstallerTitleName { get; set; } = "HxA Player Uninstaller";

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x0000E88F File Offset: 0x0000CA8F
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x0000E896 File Offset: 0x0000CA96
		public static string UninstallCancelBtnColor { get; set; } = "White";

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x0000E89E File Offset: 0x0000CA9E
		// (set) Token: 0x0600112F RID: 4399 RVA: 0x0000E8A5 File Offset: 0x0000CAA5
		public static string MaterialDesignPrimaryBtnStyle { get; set; } = "MaterialDesignButton";

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x0000E8AD File Offset: 0x0000CAAD
		// (set) Token: 0x06001131 RID: 4401 RVA: 0x0000E8B4 File Offset: 0x0000CAB4
		public static int ForceDedicatedGPUDefaultValue { get; set; } = 1;

		// Token: 0x04000B4D RID: 2893
		public const string DefaultOEM = "bgp";

		// Token: 0x04000B4E RID: 2894
		public const string BGPOEM64 = "bgp64";

		// Token: 0x04000B4F RID: 2895
		public const string BGPHYPERV = "bgp64_hyperv";

		// Token: 0x04000B50 RID: 2896
		public const string MSI2 = "msi2";

		// Token: 0x04000B51 RID: 2897
		public const string MSIHYPERV = "msi64_hyperv";

		// Token: 0x04000B52 RID: 2898
		private static string sOemTag = null;

		// Token: 0x04000B53 RID: 2899
		public const string DefaultVmName = "Android";

		// Token: 0x04000B54 RID: 2900
		private static string mCurrentDefaultVmName = "Android";

		// Token: 0x04000B55 RID: 2901
		private static string s_AppTitle = null;

		// Token: 0x04000B56 RID: 2902
		private static string sBlueStacksSetupFolder = "";

		// Token: 0x04000B57 RID: 2903
		public static readonly string BlueStacksOldDriverName = "BstkDrv" + Strings.OEMTag;

		// Token: 0x04000B58 RID: 2904
		public const string BlueStacksDriverNameWithoutOEM = "BlueStacksDrv";

		// Token: 0x04000B59 RID: 2905
		public static readonly string BlueStacksDriverName = "BlueStacksDrv" + Strings.OEMTag;

		// Token: 0x04000B5A RID: 2906
		public const string BlueStacksIdRegistryPath = "Software\\BlueStacksInstaller";

		// Token: 0x04000B5B RID: 2907
		public static readonly string ClientRegistry32BitPath = "Software\\BlueStacksGP" + Strings.OEMTag;

		// Token: 0x04000B5C RID: 2908
		public static readonly string ClientRegistry64BitPath = "Software\\WOW6432Node\\BlueStacksGP" + Strings.OEMTag;

		// Token: 0x04000B5D RID: 2909
		public const string RegistryBaseKeyPathWithoutOEM = "Software\\BlueStacks";

		// Token: 0x04000B5E RID: 2910
		public static readonly string RegistryBaseKeyPath = "Software\\BlueStacks" + Strings.OEMTag;

		// Token: 0x04000B5F RID: 2911
		public static readonly string UninstallRegistryExportedFilePath = Path.Combine(Path.GetTempPath(), "BSTUninstall.reg");

		// Token: 0x04000B60 RID: 2912
		public static readonly string BGPKeyName = "BlueStacksGP" + Strings.OEMTag;

		// Token: 0x04000B61 RID: 2913
		public const string BlueStacksUIClosingLockName = "Global\\BlueStacks_BlueStacksUI_Closing_Lockbgp";

		// Token: 0x04000B62 RID: 2914
		public const string MultiInsLockName = "Global\\BlueStacks_MULTI_INS_Frontend_Lockbgp";

		// Token: 0x04000B63 RID: 2915
		public const string LogCollectorLockName = "Global\\BlueStacks_Log_Collector_Lockbgp";

		// Token: 0x04000B64 RID: 2916
		public const string HDAgentLockName = "Global\\BlueStacks_HDAgent_Lockbgp";

		// Token: 0x04000B65 RID: 2917
		public const string HDQuitMultiInstallLockName = "Global\\BlueStacks_HDQuitMultiInstall_Lockbgp";

		// Token: 0x04000B66 RID: 2918
		public const string ComRegistrarLockName = "Global\\BlueStacks_UnRegRegCom_Lockbgp";

		// Token: 0x04000B67 RID: 2919
		public const string GetBlueStacksUILockName = "Global\\BlueStacks_BlueStacksUI_Lockbgp";

		// Token: 0x04000B68 RID: 2920
		public const string DataManagerLock = "Global\\BlueStacks_Downloader_Lockbgp";

		// Token: 0x04000B69 RID: 2921
		public const string UninstallerLock = "Global\\BlueStacks_Uninstaller_Lockbgp";

		// Token: 0x04000B6A RID: 2922
		public const string MultiInstanceManagerLock = "Global\\BlueStacks_MultiInstanceManager_Lockbgp";

		// Token: 0x04000B6B RID: 2923
		public const string InstallerLockName = "Global\\BlueStacks_Installer_Lockbgp";

		// Token: 0x04000B6C RID: 2924
		public const string CloudWatcherLock = "Global\\BlueStacks_CloudWatcher_Lockbgp";

		// Token: 0x04000B6D RID: 2925
		public const string DiskCompactorLock = "Global\\BlueStacks_DiskCompactor_Lockbgp";

		// Token: 0x04000B6E RID: 2926
		public const string MicroInstallerLock = "Global\\BlueStacks_MicroInstaller_Lockbgp";

		// Token: 0x04000B6F RID: 2927
		public const string GetPlayerClosingLockName = "Global\\BlueStacks_PlayerClosing_Lockbgp";

		// Token: 0x04000B70 RID: 2928
		public const string InputMapperDLL = "HD-Imap-Native.dll";

		// Token: 0x04000B71 RID: 2929
		public const string MSIVibrationDLL = "MsiKBVibration.dll";

		// Token: 0x04000B72 RID: 2930
		public const string MSIVibration64DLL = "MsiKBVibration64.dll";

		// Token: 0x04000B73 RID: 2931
		public const string InstancePrefix = "Android_";

		// Token: 0x04000B74 RID: 2932
		public const string AppInstallFinished = "appinstallfinished";

		// Token: 0x04000B75 RID: 2933
		public const string AppInstallProgress = "appinstallprogress";

		// Token: 0x04000B76 RID: 2934
		public const string BluestacksMultiInstanceManager = "BlueStacks Multi-Instance Manager";

		// Token: 0x04000B77 RID: 2935
		public const string DiskCleanup = "Disk cleanup";

		// Token: 0x04000B78 RID: 2936
		public const string MultiInstanceManagerBinName = "HD-MultiInstanceManager.exe";

		// Token: 0x04000B79 RID: 2937
		public const string ProductIconName = "ProductLogo.ico";

		// Token: 0x04000B7A RID: 2938
		public const string ProductImageName = "ProductLogo.png";

		// Token: 0x04000B7B RID: 2939
		public const string GameIconFileName = "app_icon.ico";

		// Token: 0x04000B7C RID: 2940
		public const string ApkHandlerBaseKeyName = "BlueStacks.Apk";

		// Token: 0x04000B7D RID: 2941
		public const string ScriptFileWhichRemovesGamingEdition = "RemoveGamingFiles.bat";

		// Token: 0x04000B7E RID: 2942
		public const string UninstallCurrentVersionRegPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall";

		// Token: 0x04000B7F RID: 2943
		public const string UninstallCurrentVersionRegPath32Bit = "SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall";

		// Token: 0x04000B80 RID: 2944
		public const string UninstallCurrentVersion32RegPath = "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall";

		// Token: 0x04000B81 RID: 2945
		public const string UninstallKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Uninstall";

		// Token: 0x04000B82 RID: 2946
		public const string ShowEnableVtPopupUrl = "showenablevtpopup";

		// Token: 0x04000B83 RID: 2947
		public const string SharePicUrl = "sharepic";

		// Token: 0x04000B84 RID: 2948
		public const string ShowGuidanceUrl = "controller_guidance_pressed";

		// Token: 0x04000B85 RID: 2949
		public const string AgentCrashReportUrl = "stats/agentcrashreport";

		// Token: 0x04000B86 RID: 2950
		public const string AppClickStatsUrl = "stats/appclickstats";

		// Token: 0x04000B87 RID: 2951
		public const string WebAppChannelClickStatsUrl = "stats/webappchannelclickstats";

		// Token: 0x04000B88 RID: 2952
		public const string FrontendStatusUpdateUrl = "FrontendStatusUpdate";

		// Token: 0x04000B89 RID: 2953
		public const string SearchAppStatsUrl = "stats/searchappstats";

		// Token: 0x04000B8A RID: 2954
		public const string AppInstallStatsUrl = "stats/appinstallstats";

		// Token: 0x04000B8B RID: 2955
		public const string SystemInfoStatsUrl = "stats/systeminfostats";

		// Token: 0x04000B8C RID: 2956
		public const string BootStatsUrl = "stats/bootstats";

		// Token: 0x04000B8D RID: 2957
		public const string TimelineStatsUrl = "stats/timelinestats4";

		// Token: 0x04000B8E RID: 2958
		public const string HomeScreenStatsUrl = "stats/homescreenstats";

		// Token: 0x04000B8F RID: 2959
		public const string BsInstallStatsUrl = "stats/bsinstallstats";

		// Token: 0x04000B90 RID: 2960
		public const string MiscellaneousStatsUrl = "/stats/miscellaneousstats";

		// Token: 0x04000B91 RID: 2961
		public const string KeyMappingUIStatsStatsUrl = "/stats/keymappinguistats";

		// Token: 0x04000B92 RID: 2962
		public const string ClientStatsUrl = "bs4/stats/clientstats";

		// Token: 0x04000B93 RID: 2963
		public const string UploadtobigqueryUrl = "bigquery/uploadtobigquery";

		// Token: 0x04000B94 RID: 2964
		public const string BtvFunnelStatsUrl = "stats/btvfunnelstats";

		// Token: 0x04000B95 RID: 2965
		public const string TroubleshooterStatsUrl = "stats/troubleshooterlogs";

		// Token: 0x04000B96 RID: 2966
		public const string GetCACodeUrl = "api/getcacode";

		// Token: 0x04000B97 RID: 2967
		public const string GetCountryUrl = "api/getcountryforip";

		// Token: 0x04000B98 RID: 2968
		public const string UploadDebugLogsUrl = "uploaddebuglogs";

		// Token: 0x04000B99 RID: 2969
		public const string UploadDebugLogsApkInstallFailureUrl = "logs/appinstallfailurelog";

		// Token: 0x04000B9A RID: 2970
		public const string UploadDebugLogsBootFailureUrl = "logs/bootfailurelog";

		// Token: 0x04000B9B RID: 2971
		public const string UploadDebugLogsCrashUrl = "logs/crashlog";

		// Token: 0x04000B9C RID: 2972
		public const string UserDataDir = "UserData";

		// Token: 0x04000B9D RID: 2973
		public const string UsersPath = "UsersPath";

		// Token: 0x04000B9E RID: 2974
		public const string UserProfile = "userprofile";

		// Token: 0x04000B9F RID: 2975
		public const string IdSeparator = "##";

		// Token: 0x04000BA0 RID: 2976
		public const string UserDefinedDir = "UserDefinedDir";

		// Token: 0x04000BA1 RID: 2977
		public const string StoreAppsDir = "App Stores";

		// Token: 0x04000BA2 RID: 2978
		public const string IconsDir = "Icons";

		// Token: 0x04000BA3 RID: 2979
		public const string CloudHost2 = "https://23.23.194.123";

		// Token: 0x04000BA4 RID: 2980
		public const string CloudHost = "https://cloud.bluestacks.com";

		// Token: 0x04000BA5 RID: 2981
		public const string LibraryName = "Apps";

		// Token: 0x04000BA6 RID: 2982
		public const string BstPrefix = "Bst-";

		// Token: 0x04000BA7 RID: 2983
		public const string GameManagerBannerImageDir = "sendappdisplayed";

		// Token: 0x04000BA8 RID: 2984
		public const string SharedFolderName = "BstSharedFolder";

		// Token: 0x04000BA9 RID: 2985
		public const string SharedFolder = "SharedFolder";

		// Token: 0x04000BAA RID: 2986
		public const string Library = "Library";

		// Token: 0x04000BAB RID: 2987
		public const string InputMapperFolderName = "InputMapper";

		// Token: 0x04000BAC RID: 2988
		public const string CaCodeBackUpFileName = "Bst_CaCode_Backup";

		// Token: 0x04000BAD RID: 2989
		public const string PCodeBackUpFileName = "Bst_PCode_Backup";

		// Token: 0x04000BAE RID: 2990
		public const string CaSelectorBackUpFileName = "Bst_CaSelector_Backup";

		// Token: 0x04000BAF RID: 2991
		public const string UserInfoBackupFileName = "Bst_UserInfo_Backup";

		// Token: 0x04000BB0 RID: 2992
		public const string BlueStacksPackagePrefix = "com.bluestacks";

		// Token: 0x04000BB1 RID: 2993
		public const string LatinImeId = "com.android.inputmethod.latin/.LatinIME";

		// Token: 0x04000BB2 RID: 2994
		public const string FrontendPortBootParam = "WINDOWSFRONTEND";

		// Token: 0x04000BB3 RID: 2995
		public const string AgentPortBootParam = "WINDOWSAGENT";

		// Token: 0x04000BB4 RID: 2996
		public const string VMXBitIsOn = "Cannot run guest while VMX is in use";

		// Token: 0x04000BB5 RID: 2997
		public const string InvalidOpCode = "invalid_op";

		// Token: 0x04000BB6 RID: 2998
		public const string KernelPanic = "Kernel panic";

		// Token: 0x04000BB7 RID: 2999
		public const string Ext4Error = ".*EXT4-fs error \\(device sd[a-b]1\\): (mb_free_blocks|ext4_mb_generate_buddy|ext4_lookup|.*deleted inode referenced):";

		// Token: 0x04000BB8 RID: 3000
		public const string PgaCtlInitFailedString = "BlueStacks.Frontend.Interop.Opengl.GetPgaServerInitStatus()";

		// Token: 0x04000BB9 RID: 3001
		public const string AppCrashInfoFile = "App_C_Info.txt";

		// Token: 0x04000BBA RID: 3002
		public const string LogDirName = "Logs";

		// Token: 0x04000BBB RID: 3003
		public const string AppNotInstalledString = "package not installed";

		// Token: 0x04000BBC RID: 3004
		public const string NetEaseReportProblemUrl = "http://gh.163.com/m";

		// Token: 0x04000BBD RID: 3005
		public const string NetEaseOpenBrowserString = "问题咨询";

		// Token: 0x04000BBE RID: 3006
		public const string InstallDirKeyName = "InstallDir";

		// Token: 0x04000BBF RID: 3007
		public const string StyleThemeStatsTag = "StyleAndThemeData";

		// Token: 0x04000BC0 RID: 3008
		public const string ConfigParam = "32";

		// Token: 0x04000BC1 RID: 3009
		public const string OemPrimaryInfo = "e";

		// Token: 0x04000BC2 RID: 3010
		public const string WorldWideToggleDisabledAppListUrl = "http://cdn3.bluestacks.com/public/appsettings/ToggleDisableAppList.cfg";

		// Token: 0x04000BC3 RID: 3011
		public const string TombStoneFilePrefix = "tombstone";

		// Token: 0x04000BC4 RID: 3012
		public const string MultiInstanceStatsUrl = "stats/multiinstancestats";

		// Token: 0x04000BC5 RID: 3013
		public const string VmManagerExe = "HD-VmManager.exe";

		// Token: 0x04000BC6 RID: 3014
		public const string BluestacksServiceString = "BlueStacks Service";

		// Token: 0x04000BC7 RID: 3015
		public const string ReportProblemWindowTitle = "BlueStacks Report Problem";

		// Token: 0x04000BC8 RID: 3016
		public const string XapkHandlerBaseKeyName = "BlueStacks.Xapk";

		// Token: 0x04000BC9 RID: 3017
		public const int AgentServerStartingPort = 2861;

		// Token: 0x04000BCA RID: 3018
		public const int HTTPServerPortRangeAgent = 10;

		// Token: 0x04000BCB RID: 3019
		public const int ClientServerStartingPort = 2871;

		// Token: 0x04000BCC RID: 3020
		public const int HTTPServerPortRangeClient = 10;

		// Token: 0x04000BCD RID: 3021
		public const int PlayerServerStartingPort = 2881;

		// Token: 0x04000BCE RID: 3022
		public const int VmMonitorServerStartingPort = 2921;

		// Token: 0x04000BCF RID: 3023
		public const int HTTPServerVmPortRange = 40;

		// Token: 0x04000BD0 RID: 3024
		public const int MultiInstanceServerStartingPort = 2961;

		// Token: 0x04000BD1 RID: 3025
		public const int MultiInstanceServerPortRange = 10;

		// Token: 0x04000BD2 RID: 3026
		public const int DefaultBlueStacksSize = 2047;

		// Token: 0x04000BD3 RID: 3027
		public const string ChannelsProdTwitchServerUrl = "https://cloud.bluestacks.com/btv/GetTwitchServers";

		// Token: 0x04000BD4 RID: 3028
		public const string VideoTutorialUrl = "videoTutorial";

		// Token: 0x04000BD5 RID: 3029
		public const string OnboardingUrl = "bs3/page/onboarding-tutorial";

		// Token: 0x04000BD6 RID: 3030
		public const string GameFeaturePopupUrl = "bs3/page/game-feature-tutorial";

		// Token: 0x04000BD7 RID: 3031
		public const string UtcConverterUrl = "bs3/page/utcConverter";

		// Token: 0x04000BD8 RID: 3032
		public const string ComExceptionErrorString = "com exception";

		// Token: 0x04000BD9 RID: 3033
		public const string BootFailedTimeoutString = "boot timeout exception";

		// Token: 0x04000BDA RID: 3034
		public const long MinimumRAMRequiredForInstallationInGB = 1L;

		// Token: 0x04000BDB RID: 3035
		public const long MinimumSpaceRequiredFreshInstallationInGB = 5L;

		// Token: 0x04000BDC RID: 3036
		public const long MinimumSpaceRequiredFreshInstallationInInstallDirMB = 500L;

		// Token: 0x04000BDD RID: 3037
		public const long MinimumSpaceRequiredFreshInstallationInMB = 5120L;

		// Token: 0x04000BDE RID: 3038
		public const string UninstallerFileName = "BlueStacksUninstaller.exe";

		// Token: 0x04000BDF RID: 3039
		public const string ArchInstallerFile64 = "64bit";

		// Token: 0x04000BE0 RID: 3040
		public const string TestRollbackRegistryKeyName = "TestRollback";

		// Token: 0x04000BE1 RID: 3041
		public const string TestRollbackFailRegistryKeyName = "TestRollbackFail";

		// Token: 0x04000BE2 RID: 3042
		public const string UnifiedInstallStats = "/bs3/stats/unified_install_stats";

		// Token: 0x04000BE3 RID: 3043
		public const string OEMConfigFileName = "Oem.cfg";

		// Token: 0x04000BE4 RID: 3044
		public const string RunAppBinaryName = "HD-RunApp.exe";

		// Token: 0x04000BE5 RID: 3045
		public const string BlueStacksBinaryName = "BlueStacks.exe";

		// Token: 0x04000BE6 RID: 3046
		public const string BlueStacksSetupFolderName = "BlueStacksSetup";

		// Token: 0x04000BE7 RID: 3047
		public const string GlModeString = "GlMode";

		// Token: 0x04000BE8 RID: 3048
		public const string MinimumClientVersionForUpgrade = "3.52.66.1905";

		// Token: 0x04000BE9 RID: 3049
		public const string MinimumEngineVersionForUpgrade = "2.52.66.8704";

		// Token: 0x04000BEA RID: 3050
		public const string DiskCompactionToolMinSupportVersion = "4.60.00.0000";

		// Token: 0x04000BEB RID: 3051
		public const string FirstIMapBuildVersion = "4.30.33.1590";

		// Token: 0x04000BEC RID: 3052
		public const string FirstUnifiedInstallerBuildVersion = "4.20.21";

		// Token: 0x04000BED RID: 3053
		public const string CommonInstallUtilsZipFileName = "CommonInstallUtils.zip";

		// Token: 0x04000BEE RID: 3054
		public const string RollbackCompletedStatString = "ROLLBACK_COMPLETED";

		// Token: 0x04000BEF RID: 3055
		public const string HandleBinaryName = "HD-Handle.exe";

		// Token: 0x04000BF0 RID: 3056
		public const string HandleRegistryKeyPath = "Software\\Sysinternals\\Handle";

		// Token: 0x04000BF1 RID: 3057
		public const string HandleEULAKeyName = "EulaAccepted";

		// Token: 0x04000BF2 RID: 3058
		public const string BootStrapperFileName = "Bootstrapper.exe";

		// Token: 0x04000BF3 RID: 3059
		public const string TakeBackupURL = "Backup_and_Restore";

		// Token: 0x04000BF4 RID: 3060
		public const string CloudImageTranslateUrl = "/translate/postimage";

		// Token: 0x04000BF5 RID: 3061
		public const string RemoteAccessRequestString = "May I please have remote access?";

		// Token: 0x04000BF6 RID: 3062
		public const string UninstallRegistryFileName = "BSTUninstall.reg";

		// Token: 0x04000BF7 RID: 3063
		public const string BuildVersionForUpgradeFromParserVersion13To14 = "4.140.00.0000";

		// Token: 0x04000BF8 RID: 3064
		public const string MultiInstanceBuildUrl = "/bs4/getmultiinstancebuild?";

		// Token: 0x04000BF9 RID: 3065
		public const string MultiInstanceCheckUpgrade = "/bs4/check_upgrade?";

		// Token: 0x04000BFA RID: 3066
		public const string RemoveDiskCommand = "removedisk";

		// Token: 0x04000BFB RID: 3067
		public const string ResetSharedFolders = "resetSharedFolders";

		// Token: 0x04000BFC RID: 3068
		public const string AnnouncementActionPopupSimple = "simple_popup";

		// Token: 0x04000BFD RID: 3069
		public const string AnnouncementActionPopupRich = "rich_popup";

		// Token: 0x04000BFE RID: 3070
		public const string AnnouncementActionPopupCenter = "center_popup";

		// Token: 0x04000BFF RID: 3071
		public const string AnnouncementActionDownloadExecute = "download_execute";

		// Token: 0x04000C00 RID: 3072
		public const string AnnouncementActionSilentLogCollect = "silent_logcollect";

		// Token: 0x04000C01 RID: 3073
		public const string AnnouncementActionSilentExecute = "silent_execute";

		// Token: 0x04000C02 RID: 3074
		public const string AnnouncementActionSelfUpdate = "self_update";

		// Token: 0x04000C03 RID: 3075
		public const string AnnouncementActionWebURLGM = "web_url_gm";

		// Token: 0x04000C04 RID: 3076
		public const string AnnouncementActionWebURL = "web_url";

		// Token: 0x04000C05 RID: 3077
		public const string AnnouncementActionStartAndroidApp = "start_android_app";

		// Token: 0x04000C06 RID: 3078
		public const string AnnouncementActionSilentInstall = "silent_install";

		// Token: 0x04000C07 RID: 3079
		public const string AnnouncementActionDisablePermanently = "disable_permanently";

		// Token: 0x04000C08 RID: 3080
		public const string AnnouncementActionNoPopup = "no_popup";

		// Token: 0x04000C09 RID: 3081
		public const string CloudStuckAtBoot = "https://cloud.bluestacks.com/bs3/page/stuck_at_boot";

		// Token: 0x04000C0A RID: 3082
		public const string CloudEnhancePerformance = "https://cloud.bluestacks.com/bs3/page/enhance_performance";

		// Token: 0x04000C0B RID: 3083
		public const string CloudWhyGoogle = "https://cloud.bluestacks.com/bs3/page/why_google";

		// Token: 0x04000C0C RID: 3084
		public const string CloudTroubleSigningIn = "https://cloud.bluestacks.com/bs3/page/trouble_signing";

		// Token: 0x04000C0D RID: 3085
		public const string ExitPopupTagBoot = "exit_popup_boot";

		// Token: 0x04000C0E RID: 3086
		public const string ExitPopupTagOTS = "exit_popup_ots";

		// Token: 0x04000C0F RID: 3087
		public const string ExitPopupTagNoApp = "exit_popup_no_app";

		// Token: 0x04000C10 RID: 3088
		public const string ExitPopupEventShown = "popup_shown";

		// Token: 0x04000C11 RID: 3089
		public const string ExitPopupEventClosedButton = "popup_closed";

		// Token: 0x04000C12 RID: 3090
		public const string ExitPopupEventClosedCross = "click_action_close";

		// Token: 0x04000C13 RID: 3091
		public const string ExitPopupEventReturnBlueStacks = "click_action_return_bluestacks";

		// Token: 0x04000C14 RID: 3092
		public const string ExitPopupEventClosedContinue = "click_action_continue_bluestacks";

		// Token: 0x04000C15 RID: 3093
		public const string ExitPopupEventAutoHidden = "popup_auto_hidden";

		// Token: 0x04000C16 RID: 3094
		public const string GL_MODE = "GlMode";

		// Token: 0x04000C17 RID: 3095
		public const string BootPromotionImageName = "BootPromo";

		// Token: 0x04000C18 RID: 3096
		public const string BackgroundPromotionImageName = "BackPromo";

		// Token: 0x04000C19 RID: 3097
		public const string AppSuggestionImageName = "AppSuggestion";

		// Token: 0x04000C1A RID: 3098
		public const string AppSuggestionRemovedFileName = "app_suggestion_removed";

		// Token: 0x04000C1B RID: 3099
		public const string ClientPromoDir = "Promo";

		// Token: 0x04000C1C RID: 3100
		public const string BGPDataDirFolderName = "Engine";

		// Token: 0x04000C1D RID: 3101
		public const string EngineActivityUri = "engine_activity";

		// Token: 0x04000C1E RID: 3102
		public const string EmulatorActivityUri = "emulator_activity";

		// Token: 0x04000C1F RID: 3103
		public const string AppInstallUri = "app_install";

		// Token: 0x04000C20 RID: 3104
		public const string DeviceProfileListUrl = "get_device_profile_list";

		// Token: 0x04000C21 RID: 3105
		public const string AppActivityUri = "app_activity";

		// Token: 0x04000C22 RID: 3106
		public const string HelpArticles = "help_articles";

		// Token: 0x04000C23 RID: 3107
		public const string EnableVirtualization = "enable_virtualization";

		// Token: 0x04000C24 RID: 3108
		public const string CloudWatcherFolderName = "Helper";

		// Token: 0x04000C25 RID: 3109
		public const string CloudWatcherBinaryName = "BlueStacksHelper.exe";

		// Token: 0x04000C26 RID: 3110
		public const string CloudWatcherTaskName = "BlueStacksHelper";

		// Token: 0x04000C27 RID: 3111
		public const string CloudWatcherIdleTaskName = "BlueStacksHelperTask";

		// Token: 0x04000C28 RID: 3112
		public const string DirectX = "DirectX";

		// Token: 0x04000C29 RID: 3113
		public const string OpenGL = "OpenGL";

		// Token: 0x04000C2A RID: 3114
		public const string LogCollectorZipFileName = "BlueStacks-Support.7z";

		// Token: 0x04000C2B RID: 3115
		public const string BstClient = "BstClient";

		// Token: 0x04000C2C RID: 3116
		public const string OperationSynchronization = "operation_synchronization";

		// Token: 0x04000C2D RID: 3117
		public const string SetIframeUrl = "setIframeUrl";

		// Token: 0x04000C2E RID: 3118
		public const string OnBrowserVisibilityChange = "onBrowserVisibilityChange";

		// Token: 0x04000C2F RID: 3119
		public const string CloudAnnouncementTimeFormat = "dd/MM/yyyy HH:mm:ss";

		// Token: 0x04000C30 RID: 3120
		public const string GadgetDir = "Gadget";

		// Token: 0x04000C31 RID: 3121
		public const string MachineID = "MachineID";

		// Token: 0x04000C32 RID: 3122
		public const string VersionMachineID = "VersionMachineId_4.250.0.1070";

		// Token: 0x04000C33 RID: 3123
		public const string UserScripts = "UserScripts";

		// Token: 0x04000C34 RID: 3124
		public const string PcodeString = "pcode";

		// Token: 0x04000C35 RID: 3125
		public const string ROOT_VDI_UUID = "fca296ce-8268-4ed7-a57f-d32ec11ab304";

		// Token: 0x04000C36 RID: 3126
		public const string LocaleFileNameFormat = "i18n.{0}.txt";

		// Token: 0x04000C37 RID: 3127
		public const string MicroInstallerWindowTitle = "BlueStacks Installer";

		// Token: 0x04000C38 RID: 3128
		public const string Bit64 = "x64 (64-bit)";

		// Token: 0x04000C39 RID: 3129
		public const string Bit32 = "x86 (32-bit)";

		// Token: 0x04000C3A RID: 3130
		public const int MaxRequiredAvailablePhysicalMemory = 2148;

		// Token: 0x04000C3B RID: 3131
		public const string MediaFilesPathSetStatsTag = "MediaFilesPathSet";

		// Token: 0x04000C3C RID: 3132
		public const string MediaFileSaveSuccess = "MediaFileSaveSuccess";

		// Token: 0x04000C3D RID: 3133
		public const string VideoRecording = "VideoRecording";

		// Token: 0x04000C3E RID: 3134
		public const string RestoreDefaultKeymappingStatsTag = "RestoreDefaultKeymappingClicked";

		// Token: 0x04000C3F RID: 3135
		public const string ImportKeymappingStatsTag = "ImportKeymappingClicked";

		// Token: 0x04000C40 RID: 3136
		public const string ExportKeymappingStatsTag = "ExportKeymappingClicked";

		// Token: 0x04000C41 RID: 3137
		public const string ForceGPUBinaryName = "HD-ForceGPU.exe";

		// Token: 0x04000C42 RID: 3138
		public const string DMMEnableVtUrl = "http://help.dmm.com/-/detail/=/qid=45997/";

		// Token: 0x04000C43 RID: 3139
		public const string ObsBinaryName = "HD-OBS.exe";

		// Token: 0x04000C44 RID: 3140
		public const string NCSoftSharedMemoryName = "ngpmmf";

		// Token: 0x04000C45 RID: 3141
		public const string SidebarConfigFileName = "sidebar_config.json";

		// Token: 0x04000C46 RID: 3142
		public const string AnotherInstanceRunningPromptText1ForDMM = "同時に起動できないプログラムが既に動いています。";

		// Token: 0x04000C47 RID: 3143
		public const string AnotherInstanceRunningPromptText2ForDMM = "既に動いているプログラムを閉じて続行しますか？";

		// Token: 0x04000C48 RID: 3144
		public const string GlTextureFolder = "UCT";

		// Token: 0x04000C49 RID: 3145
		public const string ServiceInstallerBinaryName = "HD-ServiceInstaller.exe";

		// Token: 0x04000C4A RID: 3146
		public const string MacroRecorder = "MacroOperations";

		// Token: 0x04000C4B RID: 3147
		public const string GLCheckBinaryName = "HD-GLCheck.exe";

		// Token: 0x04000C4C RID: 3148
		public const string BTVFolderName = "BTV";

		// Token: 0x04000C4D RID: 3149
		public const string KeyboardShortcuts = "KeyboardShortcuts";

		// Token: 0x04000C4E RID: 3150
		public const string ComRegistrationBinaryName = "HD-ComRegistrar.exe";

		// Token: 0x04000C4F RID: 3151
		public const string CurrentParserVersion = "17";

		// Token: 0x04000C50 RID: 3152
		public const string MinimumParserVersion = "14";

		// Token: 0x04000C51 RID: 3153
		public const string InternetConnectivityCheckUrl = "http://connectivitycheck.gstatic.com/generate_204";

		// Token: 0x04000C52 RID: 3154
		public const string AbiValueString = "abivalue";

		// Token: 0x04000C53 RID: 3155
		public const string MemAllocator = "MEMALLOCATOR";

		// Token: 0x04000C54 RID: 3156
		public const string MacroCommunityUrlKey = "macro-share";

		// Token: 0x04000C55 RID: 3157
		public const string HyperVAndroidConfigName = "Android.json";

		// Token: 0x04000C56 RID: 3158
		public const string HyperVNetworkConfigName = "Android.Network.json";

		// Token: 0x04000C57 RID: 3159
		public const string HyperVEndpointConfigName = "Android.Endpoint.json";

		// Token: 0x04000C58 RID: 3160
		public const string HyperVAdminGroupName = "Hyper-V Administrators";

		// Token: 0x04000C59 RID: 3161
		public const int DefaultMaxBatchInstanceCreationCount = 5;

		// Token: 0x04000C5A RID: 3162
		public const string MOBATag = "MOBA";

		// Token: 0x04000C5B RID: 3163
		public const string DefaultScheme = "DefaultScheme";

		// Token: 0x04000C5C RID: 3164
		public const string SchemeChanged = "SchemeChanged";

		// Token: 0x04000C5D RID: 3165
		public const string GameControlBlurbTitle = "GameControlBlurb";

		// Token: 0x04000C5E RID: 3166
		public const string GuidanceVideoBlurbTitle = "GuidanceVideoBlurb";

		// Token: 0x04000C5F RID: 3167
		public const string FullScreenBlurbTitle = "FullScreenBlurb";

		// Token: 0x04000C60 RID: 3168
		public const string ViewGuideControlBlurbTitle = "ViewControlBlurb";

		// Token: 0x04000C61 RID: 3169
		public const string EcoModeBlurbTitle = "EcoModeBlurb";

		// Token: 0x04000C62 RID: 3170
		public const string SelectedGameSchemeBlurbTitle = "SelectedGameSchemeBlurb";

		// Token: 0x04000C63 RID: 3171
		public const string WinId = "winid";

		// Token: 0x04000C64 RID: 3172
		public const string SelectedSchemeName = "SelectedSchemeName";

		// Token: 0x04000C65 RID: 3173
		public const string OnScreenControlsBlurbTitle = "OnScreenControlsBlurb";

		// Token: 0x04000C66 RID: 3174
		public const string SourceAppCenter = "BSAppCenter";

		// Token: 0x04000C67 RID: 3175
		public const string DecreaseVolumeImageName = "decrease";

		// Token: 0x04000C68 RID: 3176
		public const string DecreaseVolumeDisableImageName = "decrease_disable";

		// Token: 0x04000C69 RID: 3177
		public const string IncreaseVolumeImageName = "increase";

		// Token: 0x04000C6A RID: 3178
		public const string IncreseVolumeDisableImageName = "increase_disable";

		// Token: 0x04000C6B RID: 3179
		public const string MuteVolumeImageName = "volume_switch_off";

		// Token: 0x04000C6C RID: 3180
		public const string UnmuteVolumeImageName = "volume_switch_on";

		// Token: 0x04000C6D RID: 3181
		public const string Custom = "Custom";

		// Token: 0x04000C77 RID: 3191
		public const string ProductMajorVersion = " 4";

		// Token: 0x02000225 RID: 549
		public static class VersionConstants
		{
			// Token: 0x04000C78 RID: 3192
			public const string NotificationModeVersion = "4.210.0.1000";

			// Token: 0x04000C79 RID: 3193
			public const string DesktopNotificationsForChatAppsVersion = "4.240.0.1000";
		}

		// Token: 0x02000226 RID: 550
		public static class EngineSettingsConfiguration
		{
			// Token: 0x04000C7A RID: 3194
			public const double HighendMachineRAMThreshold = 7782.4;

			// Token: 0x04000C7B RID: 3195
			public const int HighendHighRAM = 4096;

			// Token: 0x04000C7C RID: 3196
			public const int HighendHighRAMInGB = 4;

			// Token: 0x04000C7D RID: 3197
			public const int HighCPU = 4;

			// Token: 0x04000C7E RID: 3198
			public const int HighRAM = 3072;

			// Token: 0x04000C7F RID: 3199
			public const int HighRAMInGB = 3;

			// Token: 0x04000C80 RID: 3200
			public const int MediumCPU = 2;

			// Token: 0x04000C81 RID: 3201
			public const int MediumRAM = 2048;

			// Token: 0x04000C82 RID: 3202
			public const int MediumRAMInGB = 2;

			// Token: 0x04000C83 RID: 3203
			public const int LowCPU = 1;

			// Token: 0x04000C84 RID: 3204
			public const int LowRAM = 1024;

			// Token: 0x04000C85 RID: 3205
			public const int LowRAMInGB = 1;
		}
	}
}
