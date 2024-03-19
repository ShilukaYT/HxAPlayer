using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace BlueStacks.Common
{
	// Token: 0x0200005D RID: 93
	public static class Globalization
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00005E28 File Offset: 0x00004028
		public static void InitLocalization(string resourceLocation = null)
		{
			bool flag = string.IsNullOrEmpty(resourceLocation);
			if (flag)
			{
				Globalization.sResourceLocation = Path.Combine(Globalization.sUserDefinedDir, "Locales");
			}
			else
			{
				Globalization.sResourceLocation = resourceLocation;
			}
			Globalization.sLocalizedStringsDict = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
			Globalization.sLocale = Globalization.GetCurrentCultureSupportedLocaleName();
			bool flag2 = Globalization.PopulateLocaleStrings(Globalization.sResourceLocation, Globalization.sLocalizedStringsDict, "en-US");
			if (flag2)
			{
				Logger.Info("Successfully populated {0} strings", new object[]
				{
					"en-US"
				});
			}
			bool flag3 = string.Compare(Globalization.sLocale, "en-US", StringComparison.OrdinalIgnoreCase) != 0;
			if (flag3)
			{
				bool flag4 = Globalization.PopulateLocaleStrings(Globalization.sResourceLocation, Globalization.sLocalizedStringsDict, Globalization.sLocale);
				Logger.Info("Populated strings for {0}: {1}", new object[]
				{
					Globalization.sLocale,
					flag4
				});
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00005EFC File Offset: 0x000040FC
		private static string GetCurrentCultureSupportedLocaleName()
		{
			string text = Thread.CurrentThread.CurrentCulture.Name;
			bool flag = !Globalization.sSupportedLocales.ContainsKey(text);
			if (flag)
			{
				text = "en-US";
				string text2 = Globalization.sSupportedLocales.Keys.FirstOrDefault((string x) => x.StartsWith(Thread.CurrentThread.CurrentCulture.Parent.Name, StringComparison.OrdinalIgnoreCase));
				bool flag2 = !string.IsNullOrEmpty(text2);
				if (flag2)
				{
					text = text2;
				}
			}
			return text;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005F7C File Offset: 0x0000417C
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
					bool flag2 = string.Equals(regionFromLocale, Globalization.GetRegionFromLocale(text), StringComparison.InvariantCultureIgnoreCase);
					if (flag2)
					{
						Logger.Info("Match found by region: {0}", new object[]
						{
							text
						});
						result = text;
						flag = true;
						break;
					}
					bool flag3 = string.Equals(twoLetterISOLanguageNameFromLocale, Globalization.GetTwoLetterISOLanguageNameFromLocale(text), StringComparison.InvariantCultureIgnoreCase);
					if (flag3)
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
				bool flag4 = !flag;
				if (flag4)
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

		// Token: 0x060000FA RID: 250 RVA: 0x000060F0 File Offset: 0x000042F0
		public static string GetTwoLetterISOLanguageNameFromLocale(string requestedLocale)
		{
			try
			{
				return new CultureInfo(requestedLocale).TwoLetterISOLanguageName;
			}
			catch
			{
			}
			string[] array = (requestedLocale != null) ? requestedLocale.Split(new char[]
			{
				'-'
			}) : null;
			return array[0];
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006144 File Offset: 0x00004344
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

		// Token: 0x060000FC RID: 252 RVA: 0x000061A4 File Offset: 0x000043A4
		public static string GetLocalizedString(string id)
		{
			string result = (id != null) ? id.Trim() : null;
			try
			{
				bool flag = Globalization.sLocalizedStringsDict == null;
				if (flag)
				{
					Globalization.InitLocalization(null);
				}
				bool flag2 = Globalization.sLocalizedStringsDict.ContainsKey((id != null) ? id.ToUpper(CultureInfo.InvariantCulture) : null);
				if (flag2)
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

		// Token: 0x060000FD RID: 253 RVA: 0x00006250 File Offset: 0x00004450
		public static bool PopulateLocaleStrings(string resourceLocation, Dictionary<string, string> dict, string locale)
		{
			bool result;
			try
			{
				string text = Path.Combine(resourceLocation, "i18n." + locale + ".txt");
				bool flag = !File.Exists(text);
				if (flag)
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

		// Token: 0x060000FE RID: 254 RVA: 0x000062EC File Offset: 0x000044EC
		public static void FillDictionary(Dictionary<string, string> dict, string filePath)
		{
			try
			{
				bool flag = dict == null;
				if (flag)
				{
					throw new NullReferenceException("Dictionary to fill cannot be null");
				}
				string[] array = File.ReadAllLines(filePath);
				foreach (string text in array)
				{
					bool flag2 = text.IndexOf("=", StringComparison.OrdinalIgnoreCase) == -1;
					if (!flag2)
					{
						string[] array3 = text.Split(new char[]
						{
							'='
						});
						string text2 = array3[1].Trim();
						bool flag3 = text2.Contains("@@STRING_PRODUCT_NAME@@");
						if (flag3)
						{
							text2 = text2.Replace("@@STRING_PRODUCT_NAME@@", Strings.ProductDisplayName);
						}
						dict[array3[0].Trim().ToUpper(CultureInfo.InvariantCulture)] = text2;
					}
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x040001C0 RID: 448
		public const string DEFAULT_LOCALE = "en-US";

		// Token: 0x040001C1 RID: 449
		private static string sLocale;

		// Token: 0x040001C2 RID: 450
		private static string sResourceLocation;

		// Token: 0x040001C3 RID: 451
		private static string sUserDefinedDir = (string)RegistryUtils.GetRegistryValue(Strings.RegistryBaseKeyPath, "UserDefinedDir", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);

		// Token: 0x040001C4 RID: 452
		private static Dictionary<string, string> sLocalizedStringsDict = null;

		// Token: 0x040001C5 RID: 453
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
