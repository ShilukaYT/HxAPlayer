﻿using System;

namespace BlueStacks.Common
{
	// Token: 0x02000079 RID: 121
	public enum InstallerCodes
	{
		// Token: 0x040003BA RID: 954
		SUCCESS,
		// Token: 0x040003BB RID: 955
		INCOMPATIBLE_ARCHITECTURE_INSTALLER_STARTED = 0,
		// Token: 0x040003BC RID: 956
		LAUNCH_BLUESTACKS_ON_SUCCESSFUL_INSTALLATION = 2,
		// Token: 0x040003BD RID: 957
		EXITING_WITHOUT_ADDING_USER_TO_HYPERV_ADMIN_GROUP,
		// Token: 0x040003BE RID: 958
		EXITING_AFTER_ADDING_USER_TO_HYPERV_ADMIN_GROUP,
		// Token: 0x040003BF RID: 959
		INSTALLER_ALREADY_RUNNING = -1,
		// Token: 0x040003C0 RID: 960
		INSTALLERPROPERTIES_INIT_FAILED = -2,
		// Token: 0x040003C1 RID: 961
		VERSION_DOWNGRADE = -3,
		// Token: 0x040003C2 RID: 962
		UPGRADE_NOT_SUPPORTED = -4,
		// Token: 0x040003C3 RID: 963
		COMMON_UTILS_EXTRACT_FAILED = -5,
		// Token: 0x040003C4 RID: 964
		CANCELLED_WHILE_UPGRADE = -6,
		// Token: 0x040003C5 RID: 965
		CANCELLED_PRESERVE_KEYMAPPINGS = -7,
		// Token: 0x040003C6 RID: 966
		DISALLOWED_CHARS_IN_DIR = -8,
		// Token: 0x040003C7 RID: 967
		CANCELLED_WHILE_INSUFFICIENT_STORAGE = -9,
		// Token: 0x040003C8 RID: 968
		OS_VERSION_NOT_SUPPORTED = -10,
		// Token: 0x040003C9 RID: 969
		INSUFFICIENT_PHYSICALMEMORY = -11,
		// Token: 0x040003CA RID: 970
		GL_UNSUPPORTED = -12,
		// Token: 0x040003CB RID: 971
		REGISTRY_EDITING_DISABLED = -13,
		// Token: 0x040003CC RID: 972
		INSUFFICIENT_DISKSPACE = -14,
		// Token: 0x040003CD RID: 973
		BLUESTACKS_POSSIBLY_CORRUPT = -15,
		// Token: 0x040003CE RID: 974
		SERVICE_RUNNING_EXIT = -16,
		// Token: 0x040003CF RID: 975
		VTX_DISABLED = -17,
		// Token: 0x040003D0 RID: 976
		HYPER_V_ENABLED = -18,
		// Token: 0x040003D1 RID: 977
		HYPER_V_DISABLED = -19,
		// Token: 0x040003D2 RID: 978
		HYPER_V_COMPUTE_PLATFORM_NOTAVAIL = -20,
		// Token: 0x040003D3 RID: 979
		SERVICE_MARKED_FOR_DELETION = -30,
		// Token: 0x040003D4 RID: 980
		FAILED_CREATING_REGISTRY = -31,
		// Token: 0x040003D5 RID: 981
		FAILED_TO_RESTORE_REGISTRY_DATA = -32,
		// Token: 0x040003D6 RID: 982
		PROGRAM_FILES_ZIP_EXTRACT_FAILED = -33,
		// Token: 0x040003D7 RID: 983
		PROGRAM_DATA_ZIP_EXTRACT_FAILED = -35,
		// Token: 0x040003D8 RID: 984
		CLIENT_FILES_DEPLOY_FAILED = -36,
		// Token: 0x040003D9 RID: 985
		CEF_DATA_DEPLOY_FAILED = -37,
		// Token: 0x040003DA RID: 986
		LOCALE_FILES_DEPLOY_FAILED = -38,
		// Token: 0x040003DB RID: 987
		ENGINE_FILES_DEPLOY_FAILED = -39,
		// Token: 0x040003DC RID: 988
		FAILED_TO_INSTALL_DRIVER = -40,
		// Token: 0x040003DD RID: 989
		FAILED_TO_CREATE_BSTKGLOBAL = -41,
		// Token: 0x040003DE RID: 990
		FAILED_TO_CREATE_VMCONFIG = -42,
		// Token: 0x040003DF RID: 991
		FAILED_TO_RESERVE_HTTP_PORTS = -43,
		// Token: 0x040003E0 RID: 992
		OLD_DATA_DIR_DOESNT_EXIST = -44,
		// Token: 0x040003E1 RID: 993
		FAILED_TO_RESTORE_CLIENT_DATA = -45,
		// Token: 0x040003E2 RID: 994
		FAILED_TO_RESTORE_ANDROID_DATA = -46,
		// Token: 0x040003E3 RID: 995
		FAILED_TO_MOVE_OLD_ITEMS = -49,
		// Token: 0x040003E4 RID: 996
		FAILED_TO_RENAME_NEW_ITEMS = -51,
		// Token: 0x040003E5 RID: 997
		ROLLBACK_FRESH_FAILED = -52,
		// Token: 0x040003E6 RID: 998
		ROLLBACK_REGISTRY_FAILED = -53,
		// Token: 0x040003E7 RID: 999
		ROLLBACK_PROGRAMFILES_FAILED = -54,
		// Token: 0x040003E8 RID: 1000
		ROLLBACK_PROGRAMDATA_FAILED = -55,
		// Token: 0x040003E9 RID: 1001
		UNHANDLED_EXCEPTION = -56,
		// Token: 0x040003EA RID: 1002
		GAMEDATA_DEPLOY_FAILED = -57,
		// Token: 0x040003EB RID: 1003
		ERROR_WHILE_UNINSTALLING_SERVICE = -58,
		// Token: 0x040003EC RID: 1004
		UNABLE_TO_STOP_SERVICE_BEFORE_UNINSTALLING = -59,
		// Token: 0x040003ED RID: 1005
		FAILED_TO_BUILD_HYPERV_CONFIGS = -60,
		// Token: 0x040003EE RID: 1006
		TEST_ROLLBACK = -255,
		// Token: 0x040003EF RID: 1007
		TEST_ROLLBACK_FAIL = -256
	}
}
