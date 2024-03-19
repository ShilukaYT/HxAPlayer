using System;
using System.IO;

namespace BlueStacks.Common
{
	// Token: 0x02000101 RID: 257
	public static class Constants
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00005F75 File Offset: 0x00004175
		public static string MOBACursorPath
		{
			get
			{
				return Path.Combine(Path.Combine(RegistryManager.Instance.ClientInstallDir, RegistryManager.Instance.GetClientThemeNameFromRegistry()), "Mouse_cursor_MOBA.cur");
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x00005F9A File Offset: 0x0000419A
		public static string CustomCursorPath
		{
			get
			{
				return Path.Combine(Path.Combine(RegistryManager.Instance.ClientInstallDir, RegistryManager.Instance.GetClientThemeNameFromRegistry()), "Mouse_cursor.cur");
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00005FBF File Offset: 0x000041BF
		public static string BrawlStarsMOBACursorPath
		{
			get
			{
				return Path.Combine(Path.Combine(RegistryManager.Instance.ClientInstallDir, "Assets"), "Mouse_cursor_MOBA_brawl.cur");
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00005FDF File Offset: 0x000041DF
		public static string BrawlStarsCustomCursorPath
		{
			get
			{
				return Path.Combine(Path.Combine(RegistryManager.Instance.ClientInstallDir, "Assets"), "Mouse_cursor_brawl.cur");
			}
		}

		// Token: 0x04000369 RID: 873
		public const string MacroPostFix = "_macro";

		// Token: 0x0400036A RID: 874
		public const string dateFormat = "yyyy-MM-dd HH:mm";

		// Token: 0x0400036B RID: 875
		public static readonly Version win8version = new Version(6, 2, 9200, 0);

		// Token: 0x0400036C RID: 876
		public const int IDENTITY_OFFSET = 16;

		// Token: 0x0400036D RID: 877
		public const int GUEST_ABS_MAX_X = 32768;

		// Token: 0x0400036E RID: 878
		public const int GUEST_ABS_MAX_Y = 32768;

		// Token: 0x0400036F RID: 879
		public const int MaxAllowedCPUCores = 8;

		// Token: 0x04000370 RID: 880
		public const int TOUCH_POINTS_MAX = 16;

		// Token: 0x04000371 RID: 881
		public const int SWIPE_TOUCH_POINTS_MAX = 1;

		// Token: 0x04000372 RID: 882
		public const long LWIN_TIMEOUT_TICKS = 1000000L;

		// Token: 0x04000373 RID: 883
		public const int CURSOR_HIDE_CLIP_LEN = 15;

		// Token: 0x04000374 RID: 884
		public static readonly string LocaleStringsConstant = "STRING_";

		// Token: 0x04000375 RID: 885
		public static readonly string ImapLocaleStringsConstant = "IMAP_" + Constants.LocaleStringsConstant;

		// Token: 0x04000376 RID: 886
		public const string ImapDependent = "Dependent";

		// Token: 0x04000377 RID: 887
		public const string ImapIndependent = "Independent";

		// Token: 0x04000378 RID: 888
		public const string ImapSubElement = "SubElement";

		// Token: 0x04000379 RID: 889
		public const string ImapParentElement = "ParentElement";

		// Token: 0x0400037A RID: 890
		public const string ImapNotCommon = "NotCommon";

		// Token: 0x0400037B RID: 891
		public const string ImapLinked = "Linked";

		// Token: 0x0400037C RID: 892
		public const string ImapCanvasElementY = "IMAP_CanvasElementX";

		// Token: 0x0400037D RID: 893
		public const string ImapCanvasElementX = "IMAP_CanvasElementY";

		// Token: 0x0400037E RID: 894
		public const string ImapCanvasElementRadius = "IMAP_CanvasElementRadius";

		// Token: 0x0400037F RID: 895
		public const string IMAPPopupUIElement = "IMAP_PopupUIElement";

		// Token: 0x04000380 RID: 896
		public const string IMAPKeypropertyPrefix = "Key";

		// Token: 0x04000381 RID: 897
		public const string IMAPUserDefined = "User-Defined";

		// Token: 0x04000382 RID: 898
		public const string ImapVideoHeaderConstant = "AAVideo";

		// Token: 0x04000383 RID: 899
		public const string ImapMiscHeaderConstant = "MISC";

		// Token: 0x04000384 RID: 900
		public const string ImapGlobalValid = "GlobalValidTag";

		// Token: 0x04000385 RID: 901
		public const string ImapDeveloperModeUIElemnt = "IMAP_DeveloperModeUIElemnt";

		// Token: 0x04000386 RID: 902
		public const string ImapGamepadStartKey = "GamepadStart";

		// Token: 0x04000387 RID: 903
		public const string ImapGamepadBackKey = "GamepadBack";

		// Token: 0x04000388 RID: 904
		public const string ImapGamepadLeftStickKey = "LeftStick";

		// Token: 0x04000389 RID: 905
		public const string ImapGamepadRightStickKey = "RightStick";

		// Token: 0x0400038A RID: 906
		public static readonly string[] ImapGamepadEvents = new string[]
		{
			"GamepadDpadUp",
			"GamepadDpadDown",
			"GamepadDpadLeft",
			"GamepadDpadRight",
			"GamepadStart",
			"GamepadStop",
			"GamepadLeftThumb",
			"GamepadRightThumb",
			"GamepadLeftShoulder",
			"GamepadRightShoulder",
			"GamepadA",
			"GamepadB",
			"GamepadX",
			"GamepadY",
			"GamepadLStickUp",
			"GamepadLStickDown",
			"GamepadLStickLeft",
			"GamepadLStickRight",
			"GamepadRtickUp",
			"GamepadRStickDown",
			"GamepadRStickLeft",
			"GamepadRStickRight",
			"GamepadLTrigger",
			"GamepadRTrigger",
			"LeftStick",
			"RightStick"
		};

		// Token: 0x0400038B RID: 907
		public static readonly string[] ReservedFileNamesList = new string[]
		{
			"con",
			"prn",
			"aux",
			"nul",
			"clock$",
			"com1",
			"com2",
			"com3",
			"com4",
			"com5",
			"com6",
			"com7",
			"com8",
			"com9",
			"lpt1",
			"lpt3",
			"lpt3",
			"lpt4",
			"lpt5",
			"lpt6",
			"lpt7",
			"lpt8",
			"lpt9"
		};

		// Token: 0x0400038C RID: 908
		public const string CustomCursorImageName = "yellow_cursor";

		// Token: 0x0400038D RID: 909
		public const string BrawlStarsCustomCursorImageName = "yellow_cursor_brawl";

		// Token: 0x0400038E RID: 910
		public static readonly string[] ImapGameControlsHiddenInOverlayList = new string[]
		{
			"Zoom",
			"Tilt",
			"Swipe",
			"State",
			"MouseZoom"
		};

		// Token: 0x0400038F RID: 911
		public static readonly string DefaultAppPlayerEngineInfo = "[{\"oem\":\"bgp64_hyperv\",\"prod_ver\":\"\",\"display_name\":\"Hyper-V\",\"download_url\":\"\",\"abi_value\":7,\"suffix\":\"\"},{\"oem\":\"bgp\",\"prod_ver\":\"\",\"display_name\":\"Nougat 32-bit\",\"download_url\":\"\",\"abi_value\":15,\"suffix\":\"\"},{\"oem\":\"bgp64\",\"prod_ver\":\"\",\"display_name\":\"Nougat 32-bit (Large virtual address)\",\"download_url\":\"\",\"abi_value\":7,\"suffix\":\"N-32 (Large virtual address)\"},{\"oem\":\"bgp64\",\"prod_ver\":\"\",\"display_name\":\"Nougat 64-bit\",\"download_url\":\"\",\"abi_value\":15,\"suffix\":\"N-64\"}]";

		// Token: 0x04000390 RID: 912
		public static readonly string[] All64BitOems = new string[]
		{
			"bgp64",
			"bgp64_hyperv",
			"msi64_hyperv",
			"china_gmgr64"
		};
	}
}
