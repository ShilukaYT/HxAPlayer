using System;
using System.Collections.Specialized;
using System.Net;
using System.Threading;
using BlueStacks.Common;

namespace BlueStacks.MicroInstaller
{
	// Token: 0x02000095 RID: 149
	internal class DownloaderStats
	{
		// Token: 0x060002E1 RID: 737 RVA: 0x0000FB50 File Offset: 0x0000DD50
		public static void SendStats(string eventName, string category = "", string reason = "", string downloadedBytes = "")
		{
			Logger.Info("Sending: {0}, {1}, {2}", new object[]
			{
				eventName,
				category,
				reason
			});
			try
			{
				using (WebClient webClient = new WebClient())
				{
					string value = "bgp";
					webClient.Headers.Set("x_oem", value);
					webClient.Headers.Add("x_machine_id", GuidUtils.GetBlueStacksMachineId());
					webClient.Headers.Add("x_version_machine_id", GuidUtils.GetBlueStacksVersionId());
					NameValueCollection nameValueCollection = new NameValueCollection();
					nameValueCollection["event"] = eventName;
					nameValueCollection["product_version"] = "4.250.0.1070";
					nameValueCollection["engine_version"] = "4.250.0.1070";
					nameValueCollection["client_version"] = "4.250.0.1070";
					nameValueCollection["installer_arch"] = App.sArch;
					nameValueCollection["oem"] = value;
					nameValueCollection["campaign_hash"] = App.sCampaignHash;
					nameValueCollection["locale"] = Thread.CurrentThread.CurrentCulture.ToString();
					nameValueCollection["time_since_launch"] = (InteropUtils.GetTickCount64() - App.sStartingTickCount).ToString();
					nameValueCollection["comment"] = reason;
					nameValueCollection["old_product_version"] = DownloaderStats.sOldVersion;
					nameValueCollection["receive_email_notification"] = DownloaderStats.sReceiveEmailNotification;
					nameValueCollection["error_category"] = category;
					nameValueCollection["bytes_downloaded"] = downloadedBytes;
					nameValueCollection["user_priv"] = App.sUserPriv;
					nameValueCollection["install_mode"] = DownloaderStats.sCurrentInstallMode;
					webClient.UploadValues(DownloaderStats.sStatsUrl, nameValueCollection);
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Failed to send stat for {0}. Ex: {1}", new object[]
				{
					eventName,
					ex.Message
				});
			}
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000FD58 File Offset: 0x0000DF58
		public static void SendStatsAsync(string eventName, string category = "", string reason = "", string downloadedBytes = "")
		{
			new Thread(delegate()
			{
				DownloaderStats.SendStats(eventName, category, reason, downloadedBytes);
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x04000509 RID: 1289
		private static string sStatsUrl = DownloaderUtils.Host + "/bs3/stats/unified_install_stats";

		// Token: 0x0400050A RID: 1290
		public static string sCurrentInstallMode = InstallationModes.Fresh;

		// Token: 0x0400050B RID: 1291
		public static string sReceiveEmailNotification = "";

		// Token: 0x0400050C RID: 1292
		public static string sOldVersion = "";
	}
}
