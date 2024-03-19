using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace BlueStacks.Common
{
	// Token: 0x020000EB RID: 235
	public static class LocaleStrings
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000604 RID: 1540 RVA: 0x0001C654 File Offset: 0x0001A854
		// (remove) Token: 0x06000605 RID: 1541 RVA: 0x0001C688 File Offset: 0x0001A888
		public static event EventHandler SourceUpdatedEvent;

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x000056E2 File Offset: 0x000038E2
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x000056FC File Offset: 0x000038FC
		public static Dictionary<string, string> DictLocalizedString
		{
			get
			{
				if (LocaleStrings.sDictLocalizedString == null)
				{
					LocaleStrings.InitLocalization(null, "Android", false);
				}
				return LocaleStrings.sDictLocalizedString;
			}
			set
			{
				LocaleStrings.sDictLocalizedString = value;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00005704 File Offset: 0x00003904
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x0000570B File Offset: 0x0000390B
		public static string Locale { get; set; }

		// Token: 0x0600060A RID: 1546 RVA: 0x0001C6BC File Offset: 0x0001A8BC
		public static void InitLocalization(string localeDir = null, string vmName = "Android", bool skipLocalePickFromRegistry = false)
		{
			if (localeDir == null)
			{
				LocaleStrings.sResourceLocation = Path.Combine(RegistryManager.Instance.UserDefinedDir, "Locales");
			}
			else
			{
				LocaleStrings.sResourceLocation = localeDir;
			}
			LocaleStrings.sDictLocalizedString = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
			LocaleStrings.Locale = LocaleStrings.GetLocaleName(vmName, skipLocalePickFromRegistry);
			Globalization.PopulateLocaleStrings(LocaleStrings.sResourceLocation, LocaleStrings.sDictLocalizedString, "en-US");
			if (string.Compare(LocaleStrings.Locale, "en-US", StringComparison.OrdinalIgnoreCase) != 0)
			{
				Globalization.PopulateLocaleStrings(LocaleStrings.sResourceLocation, LocaleStrings.sDictLocalizedString, LocaleStrings.Locale);
			}
			EventHandler sourceUpdatedEvent = LocaleStrings.SourceUpdatedEvent;
			if (sourceUpdatedEvent == null)
			{
				return;
			}
			sourceUpdatedEvent("Locale_Updated", null);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0001C75C File Offset: 0x0001A95C
		public static string GetLocaleName(string vmName, bool skipLocalePickFromRegistry = false)
		{
			string text = skipLocalePickFromRegistry ? null : RegistryManager.Instance.Guest[vmName].Locale;
			if (string.IsNullOrEmpty(text))
			{
				if (Oem.IsOEMDmm)
				{
					return "ja-JP";
				}
				text = Globalization.FindClosestMatchingLocale(Thread.CurrentThread.CurrentCulture.Name);
			}
			return text;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001C7B0 File Offset: 0x0001A9B0
		public static string GetLocalizedString(string id, string fallbackValue = "")
		{
			if (id == null)
			{
				return string.Empty;
			}
			string result = id.Trim();
			try
			{
				if (LocaleStrings.sDictLocalizedString == null)
				{
					LocaleStrings.InitLocalization(null, "Android", false);
				}
				if (LocaleStrings.sDictLocalizedString.ContainsKey(id.ToUpper(CultureInfo.InvariantCulture)))
				{
					result = LocaleStrings.sDictLocalizedString[id.ToUpper(CultureInfo.InvariantCulture)];
				}
				else if (string.IsNullOrEmpty(fallbackValue))
				{
					result = LocaleStrings.RemoveConstants(id);
				}
				else
				{
					result = fallbackValue;
				}
			}
			catch
			{
				Logger.Warning("Localized string not available for: {0}", new object[]
				{
					id
				});
			}
			return result;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001C84C File Offset: 0x0001AA4C
		internal static string RemoveConstants(string path)
		{
			if (path.Contains(Constants.ImapLocaleStringsConstant))
			{
				path = path.Replace(Constants.ImapLocaleStringsConstant, "");
				path = path.Replace("_", " ");
			}
			else if (path.Contains(Constants.LocaleStringsConstant))
			{
				path = path.Replace(Constants.LocaleStringsConstant, "");
				path = path.Replace("_", " ");
			}
			return path;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001C8C0 File Offset: 0x0001AAC0
		public static bool AppendLocaleIfDoesntExist(string key, string value)
		{
			bool result = false;
			try
			{
				if (!LocaleStrings.sDictLocalizedString.ContainsKey(key))
				{
					LocaleStrings.sDictLocalizedString.Add(key, value);
					result = true;
				}
			}
			catch (Exception ex)
			{
				Logger.Warning("Error appending locale entry: {0}" + ex.Message);
			}
			return result;
		}

		// Token: 0x040002FC RID: 764
		private static string sResourceLocation;

		// Token: 0x040002FE RID: 766
		private static Dictionary<string, string> sDictLocalizedString;
	}
}
