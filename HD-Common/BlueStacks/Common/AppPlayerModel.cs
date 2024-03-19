using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using Newtonsoft.Json;

namespace BlueStacks.Common
{
	// Token: 0x02000055 RID: 85
	[Serializable]
	public class AppPlayerModel
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00002F71 File Offset: 0x00001171
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x00002F79 File Offset: 0x00001179
		[JsonProperty(PropertyName = "app_player_win_version", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Include)]
		public string AppPlayerWinVersion { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00002F82 File Offset: 0x00001182
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x00002F8A File Offset: 0x0000118A
		[JsonProperty(PropertyName = "source", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Include)]
		public string Source { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x00002F93 File Offset: 0x00001193
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x00002F9B File Offset: 0x0000119B
		[JsonProperty(PropertyName = "app_player_os_arch", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Include)]
		public string AppPlayerOsArch { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x00002FA4 File Offset: 0x000011A4
		// (set) Token: 0x060001F9 RID: 505 RVA: 0x00002FAC File Offset: 0x000011AC
		[JsonProperty(PropertyName = "oem", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Include)]
		public string AppPlayerOem { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00002FB5 File Offset: 0x000011B5
		// (set) Token: 0x060001FB RID: 507 RVA: 0x00002FBD File Offset: 0x000011BD
		[JsonProperty(PropertyName = "prod_ver", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Include)]
		public string AppPlayerProdVer { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001FC RID: 508 RVA: 0x00002FC6 File Offset: 0x000011C6
		// (set) Token: 0x060001FD RID: 509 RVA: 0x00002FCE File Offset: 0x000011CE
		[JsonProperty(PropertyName = "app_player_language", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Include)]
		public string AppPlayerLanguage { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00002FD7 File Offset: 0x000011D7
		// (set) Token: 0x060001FF RID: 511 RVA: 0x00002FDF File Offset: 0x000011DF
		[JsonProperty(PropertyName = "display_name", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Include)]
		public string AppPlayerOemDisplayName { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00002FE8 File Offset: 0x000011E8
		// (set) Token: 0x06000201 RID: 513 RVA: 0x00002FF0 File Offset: 0x000011F0
		[JsonProperty(PropertyName = "download_url", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Include)]
		public string DownLoadUrl { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000202 RID: 514 RVA: 0x00002FF9 File Offset: 0x000011F9
		// (set) Token: 0x06000203 RID: 515 RVA: 0x00003001 File Offset: 0x00001201
		[JsonProperty(PropertyName = "abi_value", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Include)]
		public int AbiValue { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000204 RID: 516 RVA: 0x0000300A File Offset: 0x0000120A
		// (set) Token: 0x06000205 RID: 517 RVA: 0x00003012 File Offset: 0x00001212
		[JsonProperty(PropertyName = "suffix", DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate, NullValueHandling = NullValueHandling.Include)]
		public string Suffix { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000301B File Offset: 0x0000121B
		// (set) Token: 0x06000207 RID: 519 RVA: 0x00003023 File Offset: 0x00001223
		[JsonIgnore]
		private string DownloadPath { get; set; } = string.Empty;

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000208 RID: 520 RVA: 0x0000302C File Offset: 0x0000122C
		// (set) Token: 0x06000209 RID: 521 RVA: 0x00003034 File Offset: 0x00001234
		[JsonIgnore]
		private Downloader MDownloader { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000303D File Offset: 0x0000123D
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00003045 File Offset: 0x00001245
		[JsonIgnore]
		private bool IsOemDownloadCancelling { get; set; }

		// Token: 0x0600020C RID: 524 RVA: 0x00011CD4 File Offset: 0x0000FED4
		public bool DownLoadOem(Downloader.DownloadExceptionEventHandler downloadException, Downloader.DownloadProgressChangedEventHandler downloadProgressChanged, Downloader.DownloadFileCompletedEventHandler downloadFileCompleted, Downloader.FilePayloadInfoReceivedHandler filePayloadInfoReceived, Downloader.UnsupportedResumeEventHandler unsupportedResume, bool isRetry = false)
		{
			try
			{
				if (!string.IsNullOrEmpty(this.DownLoadUrl))
				{
					Logger.Info("The new engine url is : " + this.DownLoadUrl);
					if (!isRetry)
					{
						string fileName = Path.GetFileName(new Uri(this.DownLoadUrl).LocalPath);
						this.DownloadPath = Path.Combine(Path.GetTempPath(), fileName);
					}
					new Thread(delegate()
					{
						while (this.IsOemDownloadCancelling)
						{
							Thread.Sleep(1000);
						}
						if (isRetry || !File.Exists(this.DownloadPath))
						{
							this.MDownloader = new Downloader();
							this.MDownloader.DownloadException += this.Downloader_DownloadException;
							this.MDownloader.DownloadException += downloadException;
							this.MDownloader.DownloadProgressChanged += downloadProgressChanged;
							this.MDownloader.DownloadFileCompleted += downloadFileCompleted;
							this.MDownloader.FilePayloadInfoReceived += filePayloadInfoReceived;
							this.MDownloader.UnsupportedResume += this.Downloader_UnsupportedResume;
							this.MDownloader.UnsupportedResume += unsupportedResume;
							this.MDownloader.DownloadCancelled += this.Downloader_Cancelled;
							this.MDownloader.DownloadFile(this.DownLoadUrl, this.DownloadPath);
							return;
						}
						Downloader.DownloadFileCompletedEventHandler downloadFileCompleted2 = downloadFileCompleted;
						if (downloadFileCompleted2 == null)
						{
							return;
						}
						downloadFileCompleted2(null, null);
					}).Start();
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error while download file: {0}", new object[]
				{
					ex
				});
				return false;
			}
			return true;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000304E File Offset: 0x0000124E
		private void Downloader_DownloadException(Exception e)
		{
			this.DeleteFiles();
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000304E File Offset: 0x0000124E
		private void Downloader_UnsupportedResume(HttpStatusCode sc)
		{
			this.DeleteFiles();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00003056 File Offset: 0x00001256
		private void Downloader_Cancelled()
		{
			this.IsOemDownloadCancelling = false;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000305F File Offset: 0x0000125F
		public void CancelOemDownload()
		{
			if (this.MDownloader != null && this.MDownloader.IsDownloadInProgress)
			{
				this.IsOemDownloadCancelling = true;
				this.MDownloader.IsDownLoadCanceled = true;
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00011DB0 File Offset: 0x0000FFB0
		private void DeleteFiles()
		{
			try
			{
				if (File.Exists(this.DownloadPath))
				{
					File.Delete(this.DownloadPath);
				}
				if (File.Exists(this.DownloadPath + ".tmp"))
				{
					File.Delete(this.DownloadPath + ".tmp");
				}
			}
			catch (Exception ex)
			{
				string str = "Error while deleting files from temp folder ";
				string downloadPath = this.DownloadPath;
				string str2 = " ";
				Exception ex2 = ex;
				Logger.Error(str + downloadPath + str2 + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00011E40 File Offset: 0x00010040
		public int InstallOem()
		{
			string text = Path.Combine(Path.GetDirectoryName(RegistryManager.Instance.UserDefinedDir), "BlueStacks" + ((this.AppPlayerOem == "bgp") ? string.Empty : ("_" + this.AppPlayerOem)));
			Process process = Process.Start(new ProcessStartInfo
			{
				Arguments = string.Format(CultureInfo.InvariantCulture, "-s -pddir:\"{0}\"", new object[]
				{
					text
				}),
				CreateNoWindow = true,
				WindowStyle = ProcessWindowStyle.Hidden,
				FileName = this.DownloadPath,
				UseShellExecute = false
			});
			process.WaitForExit();
			if (process.ExitCode == 0 && RegistryManager.CheckOemInRegistry(this.AppPlayerOem, "Android"))
			{
				this.DeleteFiles();
			}
			return process.ExitCode;
		}
	}
}
