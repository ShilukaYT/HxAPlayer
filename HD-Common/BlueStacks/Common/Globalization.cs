using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace BlueStacks.Common
{
	// Token: 0x020001F8 RID: 504
	public static class Globalization
	{
		// Token: 0x0600100F RID: 4111 RVA: 0x0003D870 File Offset: 0x0003BA70
		public static void InitLocalization(string resourceLocation = null)
		{
			if (string.IsNullOrEmpty(resourceLocation))
			{
				Globalization.sResourceLocation = Path.Combine(Globalization.sUserDefinedDir, "Locales");
			}
			else
			{
				Globalization.sResourceLocation = resourceLocation;
			}
			Globalization.sLocalizedStringsDict = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
			Globalization.sLocale = Globalization.GetCurrentCultureSupportedLocaleName();
			if (Globalization.PopulateLocaleStrings(Globalization.sResourceLocation, Globalization.sLocalizedStringsDict, "en-US"))
			{
				Logger.Info("Successfully populated {0} strings", new object[]
				{
					"en-US"
				});
			}
			if (string.Compare(Globalization.sLocale, "en-US", StringComparison.OrdinalIgnoreCase) != 0)
			{
				bool flag = Globalization.PopulateLocaleStrings(Globalization.sResourceLocation, Globalization.sLocalizedStringsDict, Globalization.sLocale);
				Logger.Info("Populated strings for {0}: {1}", new object[]
				{
					Globalization.sLocale,
					flag
				});
			}
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0003D930 File Offset: 0x0003BB30
		private static string GetCurrentCultureSupportedLocaleName()
		{
			string text = Thread.CurrentThread.CurrentCulture.Name;
			if (!Globalization.sSupportedLocales.ContainsKey(text))
			{
				text = "en-US";
				string text2 = Globalization.sSupportedLocales.Keys.FirstOrDefault((string x) => x.StartsWith(Thread.CurrentThread.CurrentCulture.Parent.Name, StringComparison.OrdinalIgnoreCase));
				if (!string.IsNullOrEmpty(text2))
				{
					text = text2;
				}
			}
			return text;
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0003D99C File Offset: 0x0003BB9C
		public static string FindClosestMatchingLocale(string requestedLocale)
		{
			string result = "en-US";
			Logger.Info("Finding closest locale match to {0}", new object[]
			{
				requestedLocale
			});
			try
			{
				List<string> list = Globalization.sSupportedLocales.Keys.ToList<string>();
				bool flag = false;
				string twoLetterISOLanguageNameFromLocale = Globalization.GetTwoLetterISOLanguageNameFromLocale(requestedLocale);
				string regionFromLocale = Globalization.GetRegionFromLocale(requestedLocale);
				foreach (string text in list)
				{
					if (string.Equals(regionFromLocale, Globalization.GetRegionFromLocale(text), StringComparison.InvariantCultureIgnoreCase))
					{
						Logger.Info("Match found by region: {0}", new object[]
						{
							text
						});
						result = text;
						flag = true;
						break;
					}
					if (string.Equals(twoLetterISOLanguageNameFromLocale, Globalization.GetTwoLetterISOLanguageNameFromLocale(text), StringComparison.InvariantCultureIgnoreCase))
					{
						Logger.Info("Match found by ISO language name: {0}", new object[]
						{
							text
						});
						result = text;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					Logger.Warning("No locale match could be found, defaulting to: {0}", new object[]
					{
						"en-US"
					});
					result = "en-US";
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Some error occured. Ex: {0}", new object[]
				{
					ex.ToString()
				});
				Logger.Warning("Defaulting to: {0}", new object[]
				{
					"en-US"
				});
				result = "en-US";
			}
			return result;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0003DAE4 File Offset: 0x0003BCE4
		public static string GetTwoLetterISOLanguageNameFromLocale(string requestedLocale)
		{
			try
			{
				return new CultureInfo(requestedLocale).TwoLetterISOLanguageName;
			}
			catch
			{
			}
			return ((requestedLocale != null) ? requestedLocale.Split(new char[]
			{
				'-'
			}) : null)[0];
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0003DB30 File Offset: 0x0003BD30
		public static string GetRegionFromLocale(string requestedLocale)
		{
			try
			{
				return new RegionInfo(new CultureInfo(requestedLocale).LCID).Name;
			}
			catch
			{
			}
			string[] array = (requestedLocale != null) ? requestedLocale.Split(new char[]
			{
				'-'
			}) : null;
			return array[array.Length - 1];
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0003DB88 File Offset: 0x0003BD88
		public static string GetLocalizedString(string id)
		{
			string result = (id != null) ? id.Trim() : null;
			try
			{
				if (Globalization.sLocalizedStringsDict == null)
				{
					Globalization.InitLocalization(null);
				}
				if (Globalization.sLocalizedStringsDict.ContainsKey((id != null) ? id.ToUpper(CultureInfo.InvariantCulture) : null))
				{
					result = Globalization.sLocalizedStringsDict[(id != null) ? id.ToUpper(CultureInfo.InvariantCulture) : null];
				}
			}
			catch (Exception ex)
			{
				Logger.Warning("Localized string not available for: {0}. Ex: {1}", new object[]
				{
					id,
					ex.Message
				});
			}
			return result;
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0003DC1C File Offset: 0x0003BE1C
		public static bool PopulateLocaleStrings(string resourceLocation, Dictionary<string, string> dict, string locale)
		{
			bool result;
			try
			{
				string text = Path.Combine(resourceLocation, "i18n." + locale + ".txt");
				if (!File.Exists(text))
				{
					Logger.Info("String file {0} does not exist", new object[]
					{
						text
					});
					result = false;
				}
				else
				{
					Globalization.FillDictionary(dict, text);
					Logger.Info("Successfully populated {0} strings", new object[]
					{
						locale
					});
					result = true;
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Could not populate localized strings. Error: {0}", new object[]
				{
					ex
				});
				result = false;
			}
			return result;
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0003DCAC File Offset: 0x0003BEAC
		public static void FillDictionary(Dictionary<string, string> dict, string filePath)
		{
			try
			{
				if (dict == null)
				{
					throw new NullReferenceException("Dictionary to fill cannot be null");
				}
				foreach (string text in File.ReadAllLines(filePath))
				{
					if (text.IndexOf("=", StringComparison.OrdinalIgnoreCase) != -1)
					{
						string[] array2 = text.Split(new char[]
						{
							'='
						});
						string text2 = array2[1].Trim();
						if (text2.Contains("@@STRING_PRODUCT_NAME@@"))
						{
							text2 = text2.Replace("@@STRING_PRODUCT_NAME@@", Strings.ProductDisplayName);
						}
						dict[array2[0].Trim().ToUpper(CultureInfo.InvariantCulture)] = text2;
					}
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x04000AA6 RID: 2726
		public const string DEFAULT_LOCALE = "en-US";

		// Token: 0x04000AA7 RID: 2727
		private static string sLocale;

		// Token: 0x04000AA8 RID: 2728
		private static string sResourceLocation;

		// Token: 0x04000AA9 RID: 2729
		private static string sUserDefinedDir = (string)RegistryUtils.GetRegistryValue(Strings.RegistryBaseKeyPath, "UserDefinedDir", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);

		// Token: 0x04000AAA RID: 2730
		private static Dictionary<string, string> sLocalizedStringsDict = null;

		// Token: 0x04000AAB RID: 2731
		public static readonly Dictionary<string, string> sSupportedLocales = new Dictionary<string, string>
		{
			{
				"en-US",
				new CultureInfo("en-US").NativeName
			},
			{
				"ar-EG",
				new CultureInfo("ar-EG").NativeName
			},
			{
				"de-DE",
				new CultureInfo("de-DE").NativeName
			},
			{
				"es-ES",
				new CultureInfo("es-ES").NativeName
			},
			{
				"fr-FR",
				new CultureInfo("fr-FR").NativeName
			},
			{
				"it-IT",
				new CultureInfo("it-IT").NativeName
			},
			{
				"ja-JP",
				new CultureInfo("ja-JP").NativeName
			},
			{
				"ko-KR",
				new CultureInfo("ko-KR").NativeName
			},
			{
				"pl-PL",
				new CultureInfo("pl-PL").NativeName
			},
			{
				"pt-BR",
				new CultureInfo("pt-BR").NativeName
			},
			{
				"ru-RU",
				new CultureInfo("ru-RU").NativeName
			},
			{
				"th-TH",
				new CultureInfo("th-TH").NativeName
			},
			{
				"tr-TR",
				new CultureInfo("tr-TR").NativeName
			},
			{
				"vi-VN",
				new CultureInfo("vi-VN").NativeName
			},
			{
				"zh-TW",
				new CultureInfo("zh-TW").NativeName
			}
		};
	}
}
