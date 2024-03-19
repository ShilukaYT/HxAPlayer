using System;

namespace BlueStacks.Common
{
	// Token: 0x0200011D RID: 285
	public static class Features
	{
		// Token: 0x0600083B RID: 2107 RVA: 0x000265BC File Offset: 0x000247BC
		public static ulong GetEnabledFeatures()
		{
			ulong num = (ulong)Convert.ToUInt32(RegistryManager.Instance.Features);
			return (ulong)Convert.ToUInt32(RegistryManager.Instance.FeaturesHigh) << 32 | num;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x000265F0 File Offset: 0x000247F0
		public static void SetEnabledFeatures(ulong feature)
		{
			uint featuresHigh;
			uint features;
			Features.GetHighLowFeatures(feature, out featuresHigh, out features);
			RegistryManager.Instance.Features = (int)features;
			RegistryManager.Instance.FeaturesHigh = (int)featuresHigh;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00006E3D File Offset: 0x0000503D
		public static void GetHighLowFeatures(ulong features, out uint featuresHigh, out uint featuresLow)
		{
			featuresLow = (uint)(features & (ulong)-1);
			featuresHigh = (uint)(features >> 32);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00026620 File Offset: 0x00024820
		public static bool IsFeatureEnabled(ulong featureMask)
		{
			ulong num = Features.GetEnabledFeatures();
			if (num == 0UL)
			{
				num = Oem.Instance.WindowsOEMFeatures;
			}
			return Features.IsFeatureEnabled(featureMask, num);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00006E4D File Offset: 0x0000504D
		public static bool IsFeatureEnabled(ulong featureMask, ulong features)
		{
			return (features & featureMask) != 0UL;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00026648 File Offset: 0x00024848
		public static void DisableFeature(ulong featureMask)
		{
			ulong enabledFeatures = Features.GetEnabledFeatures();
			if ((enabledFeatures & featureMask) == 0UL)
			{
				return;
			}
			Features.SetEnabledFeatures(enabledFeatures & ~featureMask);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0002666C File Offset: 0x0002486C
		public static void EnableFeature(ulong featureMask)
		{
			ulong enabledFeatures = Features.GetEnabledFeatures();
			if ((enabledFeatures & featureMask) != 0UL)
			{
				return;
			}
			Features.SetEnabledFeatures(enabledFeatures | featureMask);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00006E57 File Offset: 0x00005057
		public static void EnableAllFeatures()
		{
			Features.SetEnabledFeatures(9223372034707292159UL);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00006E67 File Offset: 0x00005067
		public static void EnableFeaturesOfOem()
		{
			Features.SetEnabledFeatures(Oem.Instance.WindowsOEMFeatures);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00006E78 File Offset: 0x00005078
		public static bool IsFullScreenToggleEnabled()
		{
			return Features.IsFeatureEnabled(2097152UL);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00006E85 File Offset: 0x00005085
		public static bool IsHomeButtonEnabled()
		{
			return Features.IsFeatureEnabled(32768UL);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00006E92 File Offset: 0x00005092
		public static bool IsShareButtonEnabled()
		{
			return false;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00006E95 File Offset: 0x00005095
		public static bool IsGraphicsDriverReminderEnabled()
		{
			return Features.IsFeatureEnabled(65536UL);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00006E92 File Offset: 0x00005092
		public static bool IsSettingsButtonEnabled()
		{
			return false;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00006E92 File Offset: 0x00005092
		public static bool IsBackButtonEnabled()
		{
			return false;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00006E92 File Offset: 0x00005092
		public static bool IsMenuButtonEnabled()
		{
			return false;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00006EA2 File Offset: 0x000050A2
		public static bool ExitOnHome()
		{
			return Features.IsFeatureEnabled(131072UL);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00006EAF File Offset: 0x000050AF
		public static bool UpdateFrontendAppTitle()
		{
			return Features.IsFeatureEnabled(524288UL);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00006EBC File Offset: 0x000050BC
		public static bool UseDefaultNetworkText()
		{
			return Features.IsFeatureEnabled(1048576UL);
		}

		// Token: 0x04000455 RID: 1109
		public const ulong BROADCAST_MESSAGES = 1UL;

		// Token: 0x04000456 RID: 1110
		public const ulong INSTALL_NOTIFICATIONS = 2UL;

		// Token: 0x04000457 RID: 1111
		public const ulong UNINSTALL_NOTIFICATIONS = 4UL;

		// Token: 0x04000458 RID: 1112
		public const ulong CREATE_APP_SHORTCUTS = 8UL;

		// Token: 0x04000459 RID: 1113
		public const ulong LAUNCH_SETUP_APP = 16UL;

		// Token: 0x0400045A RID: 1114
		public const ulong SHOW_USAGE_STATS = 32UL;

		// Token: 0x0400045B RID: 1115
		public const ulong SYS_TRAY_SUPPORT = 64UL;

		// Token: 0x0400045C RID: 1116
		public const ulong SUGGESTED_APPS_SUPPORT = 128UL;

		// Token: 0x0400045D RID: 1117
		public const ulong OTA_SUPPORT = 256UL;

		// Token: 0x0400045E RID: 1118
		public const ulong SHOW_RESTART = 512UL;

		// Token: 0x0400045F RID: 1119
		public const ulong ANDROID_NOTIFICATIONS = 1024UL;

		// Token: 0x04000460 RID: 1120
		public const ulong RIGHT_ALIGN_PORTRAIT_MODE = 2048UL;

		// Token: 0x04000461 RID: 1121
		public const ulong LAUNCH_FRONTEND_AFTER_INSTALLTION = 4096UL;

		// Token: 0x04000462 RID: 1122
		public const ulong CREATE_LIBRARY = 8192UL;

		// Token: 0x04000463 RID: 1123
		public const ulong SHOW_AGENT_ICON_IN_SYSTRAY = 16384UL;

		// Token: 0x04000464 RID: 1124
		public const ulong IS_HOME_BUTTON_ENABLED = 32768UL;

		// Token: 0x04000465 RID: 1125
		public const ulong IS_GRAPHICS_DRIVER_REMINDER_ENABLED = 65536UL;

		// Token: 0x04000466 RID: 1126
		public const ulong EXIT_ON_HOME = 131072UL;

		// Token: 0x04000467 RID: 1127
		public const ulong MULTI_INSTANCE_SUPPORT = 262144UL;

		// Token: 0x04000468 RID: 1128
		public const ulong UPDATE_FRONTEND_APP_TITLE = 524288UL;

		// Token: 0x04000469 RID: 1129
		public const ulong USE_DEFAULT_NETWORK_TEXT = 1048576UL;

		// Token: 0x0400046A RID: 1130
		public const ulong IS_FULL_SCREEN_TOGGLE_ENABLED = 2097152UL;

		// Token: 0x0400046B RID: 1131
		public const ulong SET_CHINA_LOCALE_AND_TIMEZONE = 4194304UL;

		// Token: 0x0400046C RID: 1132
		public const ulong SHOW_TOGGLE_BUTTON_IN_LOADING_SCREEN = 8388608UL;

		// Token: 0x0400046D RID: 1133
		public const ulong ENABLE_ALT_CTRL_I_SHORTCUTS = 16777216UL;

		// Token: 0x0400046E RID: 1134
		public const ulong CREATE_LIBRARY_SHORTCUT_AT_DESKTOP = 33554432UL;

		// Token: 0x0400046F RID: 1135
		public const ulong CREATE_START_LAUNCHER_SHORTCUT = 67108864UL;

		// Token: 0x04000470 RID: 1136
		public const ulong WRITE_APP_CRASH_LOGS = 268435456UL;

		// Token: 0x04000471 RID: 1137
		public const ulong CHINA_CLOUD = 536870912UL;

		// Token: 0x04000472 RID: 1138
		public const ulong FORCE_DESKTOP_MODE = 1073741824UL;

		// Token: 0x04000473 RID: 1139
		public const ulong NOT_TO_BE_USED = 2147483648UL;

		// Token: 0x04000474 RID: 1140
		public const ulong ENABLE_ALT_CTRL_M_SHORTCUTS = 4294967296UL;

		// Token: 0x04000475 RID: 1141
		public const ulong COLLECT_APK_HANDLER_LOGS = 8589934592UL;

		// Token: 0x04000476 RID: 1142
		public const ulong SHOW_FRONTEND_FULL_SCREEN_TOAST = 17179869184UL;

		// Token: 0x04000477 RID: 1143
		public const ulong IS_CHINA_UI = 34359738368UL;

		// Token: 0x04000478 RID: 1144
		public const ulong NOT_TO_BE_USED_2 = 9223372036854775808UL;

		// Token: 0x04000479 RID: 1145
		public const ulong ALL_FEATURES = 9223372034707292159UL;

		// Token: 0x0400047A RID: 1146
		public const uint BST_HIDE_NAVIGATIONBAR = 1U;

		// Token: 0x0400047B RID: 1147
		public const uint BST_HIDE_STATUSBAR = 2U;

		// Token: 0x0400047C RID: 1148
		public const uint BST_HIDE_BACKBUTTON = 4U;

		// Token: 0x0400047D RID: 1149
		public const uint BST_HIDE_HOMEBUTTON = 8U;

		// Token: 0x0400047E RID: 1150
		public const uint BST_HIDE_RECENTSBUTTON = 16U;

		// Token: 0x0400047F RID: 1151
		public const uint BST_HIDE_SCREENSHOTBUTTON = 32U;

		// Token: 0x04000480 RID: 1152
		public const uint BST_HIDE_TOGGLEBUTTON = 64U;

		// Token: 0x04000481 RID: 1153
		public const uint BST_HIDE_CLOSEBUTTON = 128U;

		// Token: 0x04000482 RID: 1154
		public const uint BST_HIDE_GPS = 512U;

		// Token: 0x04000483 RID: 1155
		public const uint BST_SHOW_APKINSTALLBUTTON = 2048U;

		// Token: 0x04000484 RID: 1156
		public const uint BST_HIDE_HOMEAPPNEWLOADER = 65536U;

		// Token: 0x04000485 RID: 1157
		public const uint BST_SENDLETSGOS2PCLICKREPORT = 131072U;

		// Token: 0x04000486 RID: 1158
		public const uint BST_DISABLE_P2DM = 262144U;

		// Token: 0x04000487 RID: 1159
		public const uint BST_DISABLE_ARMTIPS = 524288U;

		// Token: 0x04000488 RID: 1160
		public const uint BST_DISABLE_S2P = 1048576U;

		// Token: 0x04000489 RID: 1161
		public const uint BST_SOGOUIME = 268435456U;

		// Token: 0x0400048A RID: 1162
		public const uint BST_BAIDUIME = 1073741824U;

		// Token: 0x0400048B RID: 1163
		public const uint BST_QQIME = 2147483648U;

		// Token: 0x0400048C RID: 1164
		public const uint BST_QEMU_3BT_COEXISTENCE_BIT = 536870912U;

		// Token: 0x0400048D RID: 1165
		public const uint BST_HIDE_S2P_SEARCH_BAIDU_IN_HOMEAPPNEW = 4194304U;

		// Token: 0x0400048E RID: 1166
		public const uint BST_NEW_TASK_ON_HOME = 2097152U;

		// Token: 0x0400048F RID: 1167
		public const uint BST_NO_REINSTALL = 67108864U;

		// Token: 0x04000490 RID: 1168
		public const int BST_HIDE_GUIDANCESCREEN = 1024;

		// Token: 0x04000491 RID: 1169
		public const int BST_USE_CHINESE_CDN = 4096;

		// Token: 0x04000492 RID: 1170
		public const int BST_ENALBE_ABOUT_PHONE_OPTION = 16777216;

		// Token: 0x04000493 RID: 1171
		public const int BST_ENABLE_SECURITY_OPTION = 33554432;

		// Token: 0x04000494 RID: 1172
		public const uint BST_SKIP_S2P_WHILE_LAUNCHING_APP = 2048U;

		// Token: 0x04000495 RID: 1173
		internal static string ConfigFeature = "net.";
	}
}
