using System;

namespace BlueStacks.Common
{
	// Token: 0x0200022A RID: 554
	public static class InstallerStatsEvent
	{
		// Token: 0x04000C93 RID: 3219
		public const string InstallStarted = "install_launched";

		// Token: 0x04000C94 RID: 3220
		public const string InstallAborted = "install_aborted_by_user";

		// Token: 0x04000C95 RID: 3221
		public const string InstallLicenseAgreed = "install_license_agreed";

		// Token: 0x04000C96 RID: 3222
		public const string InstallChecksPassed = "install_checks_passed";

		// Token: 0x04000C97 RID: 3223
		public const string InstallCompleted = "install_completed";

		// Token: 0x04000C98 RID: 3224
		public const string InstallFailed = "install_failed";

		// Token: 0x04000C99 RID: 3225
		public const string UpgradeLaunched = "upgrade_launched";

		// Token: 0x04000C9A RID: 3226
		public const string BackupCancel = "backup_cancel";

		// Token: 0x04000C9B RID: 3227
		public const string BackupContinue = "backup_continue";

		// Token: 0x04000C9C RID: 3228
		public const string BackupCross = "backup_cross";

		// Token: 0x04000C9D RID: 3229
		public const string UpgradeStart = "upgrade_start";

		// Token: 0x04000C9E RID: 3230
		public const string UpgradeAborted = "upgrade_aborted_by_user";

		// Token: 0x04000C9F RID: 3231
		public const string UpgradeCleaned = "upgrade_cleaned";

		// Token: 0x04000CA0 RID: 3232
		public const string UpgradeChecksPassed = "upgrade_checks_passed";

		// Token: 0x04000CA1 RID: 3233
		public const string UpgradeCompleted = "upgrade_completed";

		// Token: 0x04000CA2 RID: 3234
		public const string UpgradeFailed = "upgrade_failed";

		// Token: 0x04000CA3 RID: 3235
		public const string SysprepStarted = "install_sysprep_started";

		// Token: 0x04000CA4 RID: 3236
		public const string SilentBootCompleted = "install_silentboot_completed";

		// Token: 0x04000CA5 RID: 3237
		public const string SysprepCompleted = "install_sysprep_completed";

		// Token: 0x04000CA6 RID: 3238
		public const string MiLaunched = "mi_launched";

		// Token: 0x04000CA7 RID: 3239
		public const string MiUacPrompted = "mi_uac_prompted";

		// Token: 0x04000CA8 RID: 3240
		public const string MiUacPromptRetried = "mi_uac_prompt_retried";

		// Token: 0x04000CA9 RID: 3241
		public const string MiAdminLaunched = "mi_admin_launched";

		// Token: 0x04000CAA RID: 3242
		public const string MiBackupContinue = "mi_backup_continue";

		// Token: 0x04000CAB RID: 3243
		public const string MiBackupCancel = "mi_backup_cancel";

		// Token: 0x04000CAC RID: 3244
		public const string MiBackupCross = "mi_backup_cross";

		// Token: 0x04000CAD RID: 3245
		public const string MiInstallLicenseAgreed = "mi_license_agreed";

		// Token: 0x04000CAE RID: 3246
		public const string MiLowDiskSpaceRetried = "mi_low_disk_space_retried";

		// Token: 0x04000CAF RID: 3247
		public const string MiChecksPassed = "mi_checks_passed";

		// Token: 0x04000CB0 RID: 3248
		public const string MiDownloadStarted = "mi_download_started";

		// Token: 0x04000CB1 RID: 3249
		public const string MiDownloadFailed = "mi_download_failed";

		// Token: 0x04000CB2 RID: 3250
		public const string MiDownloadRetried = "mi_download_retried";

		// Token: 0x04000CB3 RID: 3251
		public const string MiDownloadCompleted = "mi_download_completed";

		// Token: 0x04000CB4 RID: 3252
		public const string MiMinimizePopupInit = "mi_minimizepopup_init";

		// Token: 0x04000CB5 RID: 3253
		public const string MiMinimizePopupYes = "mi_minimizepopup_yes";

		// Token: 0x04000CB6 RID: 3254
		public const string MiMinimizePopupNo = "mi_minimizepopup_no";

		// Token: 0x04000CB7 RID: 3255
		public const string MiClosed = "mi_closed";

		// Token: 0x04000CB8 RID: 3256
		public const string MiFailed = "mi_failed";

		// Token: 0x04000CB9 RID: 3257
		public const string MiFullInstallerLaunched = "mi_full_installer_launched";

		// Token: 0x04000CBA RID: 3258
		public const string MiAdminProcCompleted = "mi_admin_proc_completed";

		// Token: 0x04000CBB RID: 3259
		public const string MiRegistryNotFound = "mi_registry_not_found";

		// Token: 0x04000CBC RID: 3260
		public const string MiClientLaunchFailed = "mi_client_launch_failed";

		// Token: 0x04000CBD RID: 3261
		public const string MiClientLaunched = "mi_client_launched";

		// Token: 0x04000CBE RID: 3262
		public const string DeviceProvisioned = "device_provisioned";

		// Token: 0x04000CBF RID: 3263
		public const string GoogleLoginCompleted = "google_login_completed";

		// Token: 0x04000CC0 RID: 3264
		public const string BlueStacksLoginCompleted = "bluestacks_login_completed";
	}
}
