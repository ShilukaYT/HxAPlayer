using System;
using System.Collections.Generic;
using System.Globalization;

namespace BlueStacks.Common
{
	// Token: 0x0200017B RID: 379
	public static class WebHelper
	{
		// Token: 0x06000E66 RID: 3686 RVA: 0x0000CF94 File Offset: 0x0000B194
		public static string GetServerHost()
		{
			return RegistryManager.Instance.Host + "/bs3";
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0000CFAA File Offset: 0x0000B1AA
		public static string GetServerHostForFirebase()
		{
			return "https://us-central1-bluestacks-friends.cloudfunctions.net";
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00038F68 File Offset: 0x00037168
		public static string GetUrlWithParams(string url, string clientVer = null, string engVer = null, string userLocale = null)
		{
			string str = "bgp";
			string str2 = clientVer ?? RegistryManager.Instance.ClientVersion;
			string str3 = engVer ?? RegistryManager.Instance.Version;
			string userGuid = RegistryManager.Instance.UserGuid;
			string str4 = userLocale ?? RegistryManager.Instance.UserSelectedLocale;
			string partner = RegistryManager.Instance.Partner;
			string campaignMD = RegistryManager.Instance.CampaignMD5;
			string str5 = RegistryManager.Instance.InstallationType.ToString();
			string pkgName = GameConfig.Instance.PkgName;
			string webAppVersion = RegistryManager.Instance.WebAppVersion;
			string text = "oem=";
			text += str;
			text += "&prod_ver=";
			text += str2;
			text += "&eng_ver=";
			text += str3;
			text += "&guid=";
			text += userGuid;
			text += "&locale=";
			text += str4;
			text += "&launcher_version=";
			text += webAppVersion;
			if (!string.IsNullOrEmpty(partner))
			{
				text += "&partner=";
			}
			text += partner;
			if (!string.IsNullOrEmpty(campaignMD))
			{
				text += "&campaign_md5=";
			}
			text += campaignMD;
			Uri uri = new Uri(url);
			if (uri.Host.Equals(WebHelper.sDefaultCloudHost.Host, StringComparison.InvariantCultureIgnoreCase) || uri.Host.Equals(WebHelper.sRegistryHost.Host, StringComparison.InvariantCultureIgnoreCase))
			{
				string registeredEmail = RegistryManager.Instance.RegisteredEmail;
				if (!string.IsNullOrEmpty(registeredEmail))
				{
					text += "&email=";
				}
				text += registeredEmail;
				string token = RegistryManager.Instance.Token;
				if (!string.IsNullOrEmpty(token))
				{
					text += "&token=";
				}
				text += token;
			}
			text += "&installation_type=";
			text += str5;
			if (!string.IsNullOrEmpty(pkgName))
			{
				text += "&gaming_pkg_name=";
			}
			text += pkgName;
			if (url != null && !url.Contains("://"))
			{
				url = "http://" + url;
			}
			url = HTTPUtils.MergeQueryParams(url, text, true);
			Logger.Debug("Returning updated URL: {0}", new object[]
			{
				url
			});
			return url;
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0000CFB1 File Offset: 0x0000B1B1
		public static string GetHelpArticleURL(string articleKey)
		{
			return WebHelper.GetUrlWithParams(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				WebHelper.GetServerHost(),
				"help_articles"
			}), null, null, null) + "&article=" + articleKey;
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x000391E0 File Offset: 0x000373E0
		public static Dictionary<string, string> GetCommonPOSTData()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>
			{
				{
					"oem",
					"bgp"
				},
				{
					"prod_ver",
					RegistryManager.Instance.ClientVersion
				},
				{
					"eng_ver",
					RegistryManager.Instance.Version
				},
				{
					"guid",
					RegistryManager.Instance.UserGuid
				},
				{
					"locale",
					RegistryManager.Instance.UserSelectedLocale
				},
				{
					"installation_type",
					RegistryManager.Instance.InstallationType.ToString()
				}
			};
			string partner = RegistryManager.Instance.Partner;
			string campaignMD = RegistryManager.Instance.CampaignMD5;
			string pkgName = GameConfig.Instance.PkgName;
			string registeredEmail = RegistryManager.Instance.RegisteredEmail;
			string token = RegistryManager.Instance.Token;
			if (!string.IsNullOrEmpty(partner))
			{
				dictionary.Add("partner", partner);
			}
			if (!string.IsNullOrEmpty(campaignMD))
			{
				dictionary.Add("campaign_md5", campaignMD);
			}
			if (!string.IsNullOrEmpty(registeredEmail))
			{
				dictionary.Add("email", registeredEmail);
			}
			if (!string.IsNullOrEmpty(token))
			{
				dictionary.Add("token", token);
			}
			if (!string.IsNullOrEmpty(pkgName))
			{
				dictionary.Add("gaming_pkg_name", pkgName);
			}
			return dictionary;
		}

		// Token: 0x040006B5 RID: 1717
		private static Uri sDefaultCloudHost = new Uri("https://cloud.bluestacks.com");

		// Token: 0x040006B6 RID: 1718
		private static Uri sRegistryHost = new Uri(RegistryManager.Instance.Host);
	}
}
