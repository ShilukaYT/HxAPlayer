using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using BlueStacks.Common;

// Token: 0x0200001A RID: 26
public class LegacyDownloader
{
	// Token: 0x06000073 RID: 115 RVA: 0x000024AA File Offset: 0x000006AA
	public LegacyDownloader(int nrWorkers, string url, string fileName)
	{
		this.mUrl = url;
		this.mFileName = fileName;
		this.mNrWorkers = nrWorkers;
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00010914 File Offset: 0x0000EB14
	public void Download(LegacyDownloader.UpdateProgressCallback updateProgressCb, LegacyDownloader.DownloadCompletedCallback downloadedCb, LegacyDownloader.ExceptionCallback exceptionCb, LegacyDownloader.ContentTypeCallback contentTypeCb = null, LegacyDownloader.SizeDownloadedCallback sizeDownloadedCb = null, LegacyDownloader.PayloadInfoCallback pInfoCb = null)
	{
		this.mUpdateProgressCallback = updateProgressCb;
		this.mDownloadCompletedCallback = downloadedCb;
		this.mExceptionCallback = exceptionCb;
		this.mContentTypeCallback = contentTypeCb;
		this.mSizeDownloadedCallback = sizeDownloadedCb;
		this.mPayloadInfoCallback = pInfoCb;
		Logger.Info("Downloading {0} to: {1}", new object[]
		{
			this.mUrl,
			this.mFileName
		});
		string filePath = this.mFileName;
		global::PayloadInfo payloadInfo = null;
		try
		{
			string text = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), Path.GetFileName(this.mFileName));
			if (File.Exists(text))
			{
				Logger.Info("{0} already downloaded to {1}", new object[]
				{
					this.mUrl,
					text
				});
				filePath = text;
			}
			else
			{
				try
				{
					payloadInfo = this.GetRemotePayloadInfo(this.mUrl);
					if (pInfoCb != null)
					{
						pInfoCb(payloadInfo.Size);
					}
					if (payloadInfo.InvalidHTTPStatusCode)
					{
						Logger.Error("Invalid http status code.");
						exceptionCb(new Exception(Convert.ToString(ReturnCodesUInt.DOWNLOAD_FAILED_INVALID_STATUS_CODE, CultureInfo.InvariantCulture)));
						return;
					}
					string text2 = this.mResponseHeaders["Content-Type"];
					if (text2 == "application/vnd.android.package-archive")
					{
						this.mFileName = Path.ChangeExtension(this.mFileName, ".apk");
					}
					filePath = this.mFileName;
					if (contentTypeCb != null && !contentTypeCb(text2))
					{
						Logger.Info("Cancelling download");
						return;
					}
				}
				catch (WebException ex)
				{
					if (ex.Status == WebExceptionStatus.NameResolutionFailure)
					{
						Logger.Error("The hostname could not be resolved. Url = " + this.mUrl);
						exceptionCb(new Exception(Convert.ToString(ReturnCodesUInt.DOWNLOAD_FAILED_HOSTNAME_NOT_RESOLVED, CultureInfo.InvariantCulture)));
						return;
					}
					if (ex.Status == WebExceptionStatus.Timeout)
					{
						Logger.Error("The operation has timed out. Url = " + this.mUrl);
						exceptionCb(new Exception(Convert.ToString(ReturnCodesUInt.DOWNLOAD_FAILED_OPERATION_TIMEOUT, CultureInfo.InvariantCulture)));
						return;
					}
					Logger.Error("A WebException has occured. Url = " + this.mUrl);
					exceptionCb(ex);
					return;
				}
				catch (Exception)
				{
					Logger.Error(string.Format(CultureInfo.InvariantCulture, "Unable to send to {0}", new object[]
					{
						this.mUrl
					}));
					throw;
				}
				if (File.Exists(this.mFileName))
				{
					if (LegacyDownloader.IsPayloadOk(this.mFileName, payloadInfo.Size))
					{
						Logger.Info(this.mUrl + " already downloaded");
						goto IL_34C;
					}
					File.Delete(this.mFileName);
				}
				if (!payloadInfo.SupportsRangeRequest)
				{
					this.mNrWorkers = 1;
				}
				this.mWorkers = this.MakeWorkers(this.mNrWorkers, this.mUrl, this.mFileName, payloadInfo.Size);
				Logger.Info("Starting download of " + this.mFileName);
				int prevAverageTotalPercent = 0;
				LegacyDownloader.StartWorkers(this.mWorkers, delegate
				{
					int num = 0;
					long num2 = 0L;
					foreach (KeyValuePair<Thread, LegacyDownloader.Worker> keyValuePair in this.mWorkers)
					{
						num += keyValuePair.Value.PercentComplete;
						num2 += keyValuePair.Value.TotalFileDownloaded;
					}
					LegacyDownloader.SizeDownloadedCallback sizeDownloadedCb2 = sizeDownloadedCb;
					if (sizeDownloadedCb2 != null)
					{
						sizeDownloadedCb2(num2);
					}
					int num3 = num / this.mWorkers.Count;
					if (num3 != prevAverageTotalPercent)
					{
						updateProgressCb(num3);
					}
					prevAverageTotalPercent = num3;
				});
				LegacyDownloader.WaitForWorkers(this.mWorkers);
				LegacyDownloader.MakePayload(this.mNrWorkers, this.mFileName);
				if (!LegacyDownloader.IsPayloadOk(this.mFileName, payloadInfo.Size))
				{
					string text3 = "Downloaded file not of the correct size";
					Logger.Info(text3);
					File.Delete(this.mFileName);
					throw new Exception(text3);
				}
				Logger.Info("File downloaded correctly");
				LegacyDownloader.DeletePayloadParts(this.mNrWorkers, this.mFileName);
			}
			IL_34C:
			downloadedCb(filePath);
		}
		catch (Exception ex2)
		{
			Logger.Error("Exception in Download. err: " + ex2.ToString());
			exceptionCb(ex2);
		}
	}

	// Token: 0x06000075 RID: 117 RVA: 0x000024C7 File Offset: 0x000006C7
	public static string MakePartFileName(string fileName, int id)
	{
		return string.Format(CultureInfo.InvariantCulture, "{0}_part_{1}", new object[]
		{
			fileName,
			id
		});
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00010CE4 File Offset: 0x0000EEE4
	private static long GetSizeFromContentRange(HttpWebResponse res)
	{
		string text = res.Headers["Content-Range"];
		char[] separator = new char[]
		{
			'/'
		};
		string[] array = text.Split(separator);
		return Convert.ToInt64(array[array.Length - 1], CultureInfo.InvariantCulture);
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00010D28 File Offset: 0x0000EF28
	private global::PayloadInfo GetRemotePayloadInfo(string url)
	{
		HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
		httpWebRequest.Method = "Head";
		httpWebRequest.KeepAlive = false;
		HttpWebResponse httpWebResponse = null;
		global::PayloadInfo result = null;
		try
		{
			LegacyDownloader.Add64BitRange(httpWebRequest, 0L, 0L);
			httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			string httpresponseHeaders = LegacyDownloader.GetHTTPResponseHeaders(httpWebResponse);
			this.mResponseHeaders = httpWebResponse.Headers;
			Logger.Warning(httpresponseHeaders);
			if (httpWebResponse.StatusCode == HttpStatusCode.PartialContent)
			{
				long sizeFromContentRange = LegacyDownloader.GetSizeFromContentRange(httpWebResponse);
				result = new global::PayloadInfo(true, sizeFromContentRange, false);
			}
			else if (httpWebResponse.StatusCode == HttpStatusCode.OK)
			{
				if (httpresponseHeaders.Contains("Accept-Ranges: bytes"))
				{
					result = new global::PayloadInfo(true, httpWebResponse.ContentLength, false);
				}
				else
				{
					result = new global::PayloadInfo(false, httpWebResponse.ContentLength, false);
				}
			}
			else
			{
				result = new global::PayloadInfo(false, 0L, true);
			}
		}
		catch (Exception ex)
		{
			Logger.Error(ex.ToString());
			throw;
		}
		httpWebResponse.Close();
		return result;
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00010E14 File Offset: 0x0000F014
	private List<KeyValuePair<Thread, LegacyDownloader.Worker>> MakeWorkers(int nrWorkers, string url, string payloadFileName, long payloadSize)
	{
		long num = payloadSize / (long)nrWorkers;
		List<KeyValuePair<Thread, LegacyDownloader.Worker>> list = new List<KeyValuePair<Thread, LegacyDownloader.Worker>>();
		for (int i = 0; i < nrWorkers; i++)
		{
			long from = (long)i * num;
			long to;
			if (i == nrWorkers - 1)
			{
				to = (long)(i + 1) * num + payloadSize % (long)nrWorkers - 1L;
			}
			else
			{
				to = (long)(i + 1) * num - 1L;
			}
			Thread key = new Thread(new ParameterizedThreadStart(this.DoWork))
			{
				IsBackground = true
			};
			LegacyDownloader.Worker value = new LegacyDownloader.Worker(i, url, payloadFileName, new Range(from, to));
			KeyValuePair<Thread, LegacyDownloader.Worker> item = new KeyValuePair<Thread, LegacyDownloader.Worker>(key, value);
			list.Add(item);
		}
		return list;
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00010EA8 File Offset: 0x0000F0A8
	private static void StartWorkers(List<KeyValuePair<Thread, LegacyDownloader.Worker>> workers, LegacyDownloader.ProgressCallback progressCallback)
	{
		foreach (KeyValuePair<Thread, LegacyDownloader.Worker> keyValuePair in workers)
		{
			keyValuePair.Value.ProgressCallback = progressCallback;
			keyValuePair.Key.Start(keyValuePair.Value);
		}
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00010F10 File Offset: 0x0000F110
	private static void MakePayload(int nrWorkers, string payloadName)
	{
		Stream stream = new FileStream(payloadName, FileMode.Create, FileAccess.Write, FileShare.None);
		int num = 16384;
		byte[] buffer = new byte[num];
		for (int i = 0; i < nrWorkers; i++)
		{
			Stream stream2 = new FileStream(LegacyDownloader.MakePartFileName(payloadName, i), FileMode.Open, FileAccess.Read);
			int count;
			while ((count = stream2.Read(buffer, 0, num)) > 0)
			{
				stream.Write(buffer, 0, count);
			}
			stream2.Close();
		}
		stream.Flush();
		stream.Close();
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00010F84 File Offset: 0x0000F184
	private static void DeletePayloadParts(int nrParts, string payloadName)
	{
		for (int i = 0; i < nrParts; i++)
		{
			File.Delete(LegacyDownloader.MakePartFileName(payloadName, i));
		}
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00010FAC File Offset: 0x0000F1AC
	private static string GetHTTPResponseHeaders(HttpWebResponse res)
	{
		string str = "HTTP Response Headers\n" + string.Format(CultureInfo.InvariantCulture, "StatusCode: {0}\n", new object[]
		{
			(int)res.StatusCode
		});
		WebHeaderCollection headers = res.Headers;
		return str + ((headers != null) ? headers.ToString() : null);
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00011000 File Offset: 0x0000F200
	public void DoWork(object data)
	{
		LegacyDownloader.Worker worker = (LegacyDownloader.Worker)data;
		Range range = (worker != null) ? worker.Range : null;
		Stream stream = null;
		HttpWebResponse httpWebResponse = null;
		Stream stream2 = null;
		try
		{
			Logger.Info("WorkerId {0} range.From = {1}, range.To = {2}", new object[]
			{
				worker.Id,
				range.From,
				range.To
			});
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(worker.URL);
			httpWebRequest.KeepAlive = true;
			if (File.Exists(worker.PartFileName))
			{
				stream = new FileStream(worker.PartFileName, FileMode.Append, FileAccess.Write, FileShare.None);
				if (stream.Length == range.Length)
				{
					worker.TotalFileDownloaded = stream.Length;
					worker.PercentComplete = 100;
					Logger.Info("WorkerId {0} already downloaded", new object[]
					{
						worker.Id
					});
					return;
				}
				worker.TotalFileDownloaded = stream.Length;
				worker.PercentComplete = (int)(stream.Length * 100L / range.Length);
				Logger.Info("WorkerId {0} Resuming from range.From = {1}, range.To = {2}", new object[]
				{
					worker.Id,
					range.From + stream.Length,
					range.To
				});
				if (this.mNrWorkers > 1)
				{
					LegacyDownloader.Add64BitRange(httpWebRequest, range.From + stream.Length, range.To);
				}
			}
			else
			{
				worker.TotalFileDownloaded = 0L;
				worker.PercentComplete = 0;
				stream = new FileStream(worker.PartFileName, FileMode.Create, FileAccess.Write, FileShare.None);
				if (this.mNrWorkers > 1)
				{
					LegacyDownloader.Add64BitRange(httpWebRequest, range.From + stream.Length, range.To);
				}
			}
			httpWebRequest.ReadWriteTimeout = 60000;
			httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			long contentLength = httpWebResponse.ContentLength;
			stream2 = httpWebResponse.GetResponseStream();
			int num = 65536;
			byte[] buffer = new byte[num];
			long num2 = 0L;
			Logger.Warning(string.Format(CultureInfo.InvariantCulture, "WorkerId {0}\n", new object[]
			{
				worker.Id
			}) + LegacyDownloader.GetHTTPResponseHeaders(httpWebResponse));
			int num3;
			while ((num3 = stream2.Read(buffer, 0, num)) > 0)
			{
				if (worker.Cancelled)
				{
					throw new OperationCanceledException("Download cancelled by user.");
				}
				stream.Write(buffer, 0, num3);
				num2 += (long)num3;
				worker.TotalFileDownloaded = stream.Length;
				worker.PercentComplete = (int)(stream.Length * 100L / range.Length);
			}
			if (contentLength != num2)
			{
				throw new Exception(string.Format(CultureInfo.InvariantCulture, "totalContentRead({0}) != contentLength({1})", new object[]
				{
					num2,
					contentLength
				}));
			}
		}
		catch (Exception ex)
		{
			worker.Exception = ex;
			Logger.Error(ex.ToString());
		}
		finally
		{
			if (stream2 != null)
			{
				stream2.Close();
			}
			if (httpWebResponse != null)
			{
				httpWebResponse.Close();
			}
			if (stream != null)
			{
				stream.Flush();
				stream.Close();
			}
		}
		Logger.Info("WorkerId {0} Finished", new object[]
		{
			worker.Id
		});
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00011348 File Offset: 0x0000F548
	private static bool IsPayloadOk(string payloadFileName, long remoteSize)
	{
		long length = new FileInfo(payloadFileName).Length;
		Logger.Info("payloadSize = " + length.ToString() + " remoteSize = " + remoteSize.ToString());
		return length == remoteSize;
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00011388 File Offset: 0x0000F588
	public void AbortDownload()
	{
		if (this.mWorkers == null)
		{
			return;
		}
		Logger.Info("Downloader: Aborting all threads...");
		foreach (KeyValuePair<Thread, LegacyDownloader.Worker> keyValuePair in this.mWorkers)
		{
			try
			{
				keyValuePair.Value.Cancelled = true;
			}
			catch (Exception ex)
			{
				Logger.Error("Downloader: could not abort thread. Error: " + ex.Message);
			}
		}
	}

	// Token: 0x06000080 RID: 128 RVA: 0x0001141C File Offset: 0x0000F61C
	private static void WaitForWorkers(List<KeyValuePair<Thread, LegacyDownloader.Worker>> workers)
	{
		foreach (KeyValuePair<Thread, LegacyDownloader.Worker> keyValuePair in workers)
		{
			keyValuePair.Key.Join();
		}
		foreach (KeyValuePair<Thread, LegacyDownloader.Worker> keyValuePair2 in workers)
		{
			if (keyValuePair2.Value.Exception != null)
			{
				throw new WorkerException(keyValuePair2.Value.Exception.Message, keyValuePair2.Value.Exception);
			}
		}
	}

	// Token: 0x06000081 RID: 129 RVA: 0x000114D8 File Offset: 0x0000F6D8
	private static void Add64BitRange(HttpWebRequest req, long start, long end)
	{
		MethodInfo method = typeof(WebHeaderCollection).GetMethod("AddWithoutValidate", BindingFlags.Instance | BindingFlags.NonPublic);
		string text = "Range";
		string text2 = string.Format(CultureInfo.InvariantCulture, "bytes={0}-{1}", new object[]
		{
			start,
			end
		});
		method.Invoke(req.Headers, new object[]
		{
			text,
			text2
		});
	}

	// Token: 0x0400002E RID: 46
	private List<KeyValuePair<Thread, LegacyDownloader.Worker>> mWorkers;

	// Token: 0x0400002F RID: 47
	private WebHeaderCollection mResponseHeaders;

	// Token: 0x04000030 RID: 48
	private readonly string mUrl;

	// Token: 0x04000031 RID: 49
	private string mFileName;

	// Token: 0x04000032 RID: 50
	private int mNrWorkers;

	// Token: 0x04000033 RID: 51
	private LegacyDownloader.UpdateProgressCallback mUpdateProgressCallback;

	// Token: 0x04000034 RID: 52
	private LegacyDownloader.DownloadCompletedCallback mDownloadCompletedCallback;

	// Token: 0x04000035 RID: 53
	private LegacyDownloader.ExceptionCallback mExceptionCallback;

	// Token: 0x04000036 RID: 54
	private LegacyDownloader.ContentTypeCallback mContentTypeCallback;

	// Token: 0x04000037 RID: 55
	private LegacyDownloader.SizeDownloadedCallback mSizeDownloadedCallback;

	// Token: 0x04000038 RID: 56
	private LegacyDownloader.PayloadInfoCallback mPayloadInfoCallback;

	// Token: 0x0200001B RID: 27
	// (Invoke) Token: 0x06000083 RID: 131
	public delegate void UpdateProgressCallback(int percent);

	// Token: 0x0200001C RID: 28
	// (Invoke) Token: 0x06000087 RID: 135
	public delegate void DownloadCompletedCallback(string filePath);

	// Token: 0x0200001D RID: 29
	// (Invoke) Token: 0x0600008B RID: 139
	public delegate void ExceptionCallback(Exception e);

	// Token: 0x0200001E RID: 30
	// (Invoke) Token: 0x0600008F RID: 143
	public delegate bool ContentTypeCallback(string contentType);

	// Token: 0x0200001F RID: 31
	// (Invoke) Token: 0x06000093 RID: 147
	public delegate void SizeDownloadedCallback(long size);

	// Token: 0x02000020 RID: 32
	// (Invoke) Token: 0x06000097 RID: 151
	public delegate void PayloadInfoCallback(long pInfo);

	// Token: 0x02000021 RID: 33
	// (Invoke) Token: 0x0600009B RID: 155
	private delegate void ProgressCallback();

	// Token: 0x02000022 RID: 34
	private class Worker
	{
		// Token: 0x0600009E RID: 158 RVA: 0x000024EB File Offset: 0x000006EB
		public Worker(int id, string url, string payloadName, Range range)
		{
			this.Id = id;
			this.URL = url;
			this.m_PayloadName = payloadName;
			this.Range = range;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00002510 File Offset: 0x00000710
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00002518 File Offset: 0x00000718
		public bool Cancelled { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00002521 File Offset: 0x00000721
		public int Id { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00002529 File Offset: 0x00000729
		public string PartFileName
		{
			get
			{
				return LegacyDownloader.MakePartFileName(this.m_PayloadName, this.Id);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000253C File Offset: 0x0000073C
		public Range Range { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00002544 File Offset: 0x00000744
		public string URL { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000254C File Offset: 0x0000074C
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00002554 File Offset: 0x00000754
		public int PercentComplete
		{
			get
			{
				return this.m_PercentComplete;
			}
			set
			{
				this.m_PercentComplete = value;
				this.ProgressCallback();
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002568 File Offset: 0x00000768
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00002570 File Offset: 0x00000770
		public long TotalFileDownloaded { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002579 File Offset: 0x00000779
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00002581 File Offset: 0x00000781
		public LegacyDownloader.ProgressCallback ProgressCallback { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000258A File Offset: 0x0000078A
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00002592 File Offset: 0x00000792
		public Exception Exception { get; set; }

		// Token: 0x04000039 RID: 57
		private readonly string m_PayloadName;

		// Token: 0x0400003A RID: 58
		private int m_PercentComplete;
	}
}
