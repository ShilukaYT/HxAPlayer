using System;
using System.Globalization;
using System.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000073 RID: 115
	public static class InstallerErrorHandling
	{
		// Token: 0x060002AA RID: 682 RVA: 0x00014238 File Offset: 0x00012438
		public static string AssignErrorStringForInstallerExitCodes(int mInstallFailedErrorCode, string prefixKey)
		{
			string text = LocaleStrings.GetLocalizedString(prefixKey, "");
			InstallerCodes installerCodes = (InstallerCodes)mInstallFailedErrorCode;
			string text2 = installerCodes.ToString();
			string text3 = string.Empty;
			bool flag = true;
			if (prefixKey != "STRING_ROLLBACK_FAILED_SORRY_MESSAGE")
			{
				switch (mInstallFailedErrorCode)
				{
				case -59:
				case -58:
					text3 = string.Format(CultureInfo.InvariantCulture, "{0}\n{1}", new object[]
					{
						LocaleStrings.GetLocalizedString("STRING_OLD_INSTALLATION_INTERFERING", ""),
						LocaleStrings.GetLocalizedString("STRING_TRY_RESTARTING_MACHINE", "")
					});
					goto IL_264;
				case -55:
				case -54:
				case -53:
				case -52:
					text3 = LocaleStrings.GetLocalizedString("STRING_COULDNT_RESTORE_UNUSABLE", "");
					goto IL_264;
				case -51:
				case -49:
				case -43:
				case -42:
				case -41:
				case -40:
				case -39:
				case -38:
				case -37:
				case -36:
				case -35:
				case -33:
					text3 = LocaleStrings.GetLocalizedString("STRING_ERROR_OCCURED_DEPLOYING_FILES", "");
					goto IL_264;
				case -46:
				case -45:
				case -44:
				case -32:
					text3 = LocaleStrings.GetLocalizedString("STRING_FAILED_TO_RESTORE_OLD_DATA", "");
					goto IL_264;
				case -30:
					text3 = LocaleStrings.GetLocalizedString("STRING_OLD_INSTALLATION_INTERFERING", "");
					goto IL_264;
				case -19:
					text3 = LocaleStrings.GetLocalizedString("STRING_HYPERV_DISABLED_WARNING", "");
					flag = false;
					text = string.Empty;
					goto IL_264;
				case -18:
					text3 = LocaleStrings.GetLocalizedString("STRING_HYPERV_INSTALLER_WARNING", "");
					flag = false;
					text = string.Empty;
					goto IL_264;
				case -17:
					text3 = LocaleStrings.GetLocalizedString("STRING_DISABLED_VT", "");
					flag = false;
					text = string.Empty;
					goto IL_264;
				case -14:
				{
					string text4 = string.Format(CultureInfo.InvariantCulture, "{0}GB", new object[]
					{
						5L
					});
					text3 = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
					{
						LocaleStrings.GetLocalizedString("STRING_INSUFFICIENT_DISKSPACE", ""),
						text4
					});
					goto IL_264;
				}
				}
				string text5 = "STRING_" + text2;
				text3 = LocaleStrings.GetLocalizedString(text5, "");
				if (text3.Equals(text5, StringComparison.InvariantCultureIgnoreCase))
				{
					text3 = LocaleStrings.GetLocalizedString("STRING_ERROR_OCCURED_DEPLOYING_FILES", "");
				}
			}
			IL_264:
			if (Enumerable.Range(-30, 20).Contains(mInstallFailedErrorCode) && flag)
			{
				text3 = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
				{
					LocaleStrings.GetLocalizedString("STRING_PREINSTALL_FAIL", ""),
					text3
				});
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}\n{1}\n\n{2} {3}", new object[]
			{
				text,
				text3,
				LocaleStrings.GetLocalizedString("STRING_ERROR_CODE_COLON", ""),
				text2
			}).TrimStart(new char[]
			{
				'\n'
			});
		}
	}
}
