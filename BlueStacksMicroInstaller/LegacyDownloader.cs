using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using BlueStacks.Common;

// Token: 0x02000006 RID: 6
public class LegacyDownloader
{
	// Token: 0x0600000B RID: 11 RVA: 0x000020CA File Offset: 0x000002CA
	public LegacyDownloader(int nrWorkers, string url, string fileName)
	{
		this.mUrl = url;
		this.mFileName = fileName;
		this.mNrWorkers = nrWorkers;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000020EC File Offset: 0x000002EC
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
			string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
			string text = Path.Combine(directoryName, Path.GetFileName(this.mFileName));
			bool flag = File.Exists(text);
			if (flag)
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
					bool invalidHTTPStatusCode = payloadInfo.InvalidHTTPStatusCode;
					if (invalidHTTPStatusCode)
					{
						Logger.Error("Invalid http status code.");
						exceptionCb(new Exception(Convert.ToString(ReturnCodesUInt.DOWNLOAD_FAILED_INVALID_STATUS_CODE, CultureInfo.InvariantCulture)));
						return;
					}
					string text2 = this.mResponseHeaders["Content-Type"];
					bool flag2 = text2 == "application/vnd.android.package-archive";
					if (flag2)
					{
						this.mFileName = Path.ChangeExtension(this.mFileName, ".apk");
					}
					filePath = this.mFileName;
					bool flag3 = contentTypeCb != null && !contentTypeCb(text2);
					if (flag3)
					{
						Logger.Info("Cancelling download");
						return;
					}
				}
				catch (WebException ex)
				{
					bool flag4 = ex.Status == WebExceptionStatus.NameResolutionFailure;
					if (flag4)
					{
						Logger.Error("The hostname could not be resolved. Url = " + this.mUrl);
						exceptionCb(new Exception(Convert.ToString(ReturnCodesUInt.DOWNLOAD_FAILED_HOSTNAME_NOT_RESOLVED, CultureInfo.InvariantCulture)));
						return;
					}
					bool flag5 = ex.Status == WebExceptionStatus.Timeout;
					if (flag5)
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
				bool flag6 = File.Exists(this.mFileName);
				if (flag6)
				{
					bool flag7 = LegacyDownloader.IsPayloadOk(this.mFileName, payloadInfo.Size);
					if (flag7)
					{
						Logger.Info(this.mUrl + " already downloaded");
						goto IL_3D0;
					}
					File.Delete(this.mFileName);
				}
				bool flag8 = !payloadInfo.SupportsRangeRequest;
				if (flag8)
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
					bool flag10 = num3 != prevAverageTotalPercent;
					if (flag10)
					{
						updateProgressCb(num3);
					}
					prevAverageTotalPercent = num3;
				});
				LegacyDownloader.WaitForWorkers(this.mWorkers);
				LegacyDownloader.MakePayload(this.mNrWorkers, this.mFileName);
				bool flag9 = !LegacyDownloader.IsPayloadOk(this.mFileName, payloadInfo.Size);
				if (flag9)
				{
					string text3 = "Downloaded file not of the correct size";
					Logger.Info(text3);
					File.Delete(this.mFileName);
					throw new Exception(text3);
				}
				Logger.Info("File downloaded correctly");
				LegacyDownloader.DeletePayloadParts(this.mNrWorkers, this.mFileName);
			}
			IL_3D0:
			downloadedCb(filePath);
		}
		catch (Exception ex2)
		{
			Logger.Error("Exception in Download. err: " + ex2.ToString());
			exceptionCb(ex2);
		}
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002548 File Offset: 0x00000748
	public static string MakePartFileName(string fileName, int id)
	{
		return string.Format(CultureInfo.InvariantCulture, "{0}_part_{1}", new object[]
		{
			fileName,
			id
		});
	}

	// Token: 0x0600000E RID: 14 RVA: 0x0000257C File Offset: 0x0000077C
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

	// Token: 0x0600000F RID: 15 RVA: 0x000025C4 File Offset: 0x000007C4
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
			bool flag = httpWebResponse.StatusCode == HttpStatusCode.PartialContent;
			if (flag)
			{
				long sizeFromContentRange = LegacyDownloader.GetSizeFromContentRange(httpWebResponse);
				result = new global::PayloadInfo(true, sizeFromContentRange, false);
			}
			else
			{
				bool flag2 = httpWebResponse.StatusCode == HttpStatusCode.OK;
				if (flag2)
				{
					bool flag3 = httpresponseHeaders.Contains("Accept-Ranges: bytes");
					if (flag3)
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
		}
		catch (Exception ex)
		{
			Logger.Error(ex.ToString());
			throw;
		}
		httpWebResponse.Close();
		return result;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x000026DC File Offset: 0x000008DC
	private List<KeyValuePair<Thread, LegacyDownloader.Worker>> MakeWorkers(int nrWorkers, string url, string payloadFileName, long payloadSize)
	{
		long num = payloadSize / (long)nrWorkers;
		List<KeyValuePair<Thread, LegacyDownloader.Worker>> list = new List<KeyValuePair<Thread, LegacyDownloader.Worker>>();
		for (int i = 0; i < nrWorkers; i++)
		{
			long from = (long)i * num;
			bool flag = i == nrWorkers - 1;
			long to;
			if (flag)
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

	// Token: 0x06000011 RID: 17 RVA: 0x0000278C File Offset: 0x0000098C
	private static void StartWorkers(List<KeyValuePair<Thread, LegacyDownloader.Worker>> workers, LegacyDownloader.ProgressCallback progressCallback)
	{
		foreach (KeyValuePair<Thread, LegacyDownloader.Worker> keyValuePair in workers)
		{
			keyValuePair.Value.ProgressCallback = progressCallback;
			keyValuePair.Key.Start(keyValuePair.Value);
		}
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000027FC File Offset: 0x000009FC
	private static void MakePayload(int nrWorkers, string payloadName)
	{
		Stream stream = new FileStream(payloadName, FileMode.Create, FileAccess.Write, FileShare.None);
		int num = 16384;
		byte[] buffer = new byte[num];
		for (int i = 0; i < nrWorkers; i++)
		{
			string path = LegacyDownloader.MakePartFileName(payloadName, i);
			Stream stream2 = new FileStream(path, FileMode.Open, FileAccess.Read);
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

	// Token: 0x06000013 RID: 19 RVA: 0x0000288C File Offset: 0x00000A8C
	private static void DeletePayloadParts(int nrParts, string payloadName)
	{
		for (int i = 0; i < nrParts; i++)
		{
			string path = LegacyDownloader.MakePartFileName(payloadName, i);
			File.Delete(path);
		}
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000028BC File Offset: 0x00000ABC
	private static string GetHTTPResponseHeaders(HttpWebResponse res)
	{
		string text = "HTTP Response Headers\n";
		text += string.Format(CultureInfo.InvariantCulture, "StatusCode: {0}\n", new object[]
		{
			(int)res.StatusCode
		});
		string str = text;
		WebHeaderCollection headers = res.Headers;
		return str + ((headers != null) ? headers.ToString() : null);
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00002918 File Offset: 0x00000B18
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
			bool flag = File.Exists(worker.PartFileName);
			if (flag)
			{
				stream = new FileStream(worker.PartFileName, FileMode.Append, FileAccess.Write, FileShare.None);
				bool flag2 = stream.Length == range.Length;
				if (flag2)
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
				bool flag3 = this.mNrWorkers > 1;
				if (flag3)
				{
					LegacyDownloader.Add64BitRange(httpWebRequest, range.From + stream.Length, range.To);
				}
			}
			else
			{
				worker.TotalFileDownloaded = 0L;
				worker.PercentComplete = 0;
				stream = new FileStream(worker.PartFileName, FileMode.Create, FileAccess.Write, FileShare.None);
				bool flag4 = this.mNrWorkers > 1;
				if (flag4)
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
			string text = string.Format(CultureInfo.InvariantCulture, "WorkerId {0}\n", new object[]
			{
				worker.Id
			});
			text += LegacyDownloader.GetHTTPResponseHeaders(httpWebResponse);
			Logger.Warning(text);
			int num3;
			while ((num3 = stream2.Read(buffer, 0, num)) > 0)
			{
				bool cancelled = worker.Cancelled;
				if (cancelled)
				{
					throw new OperationCanceledException("Download cancelled by user.");
				}
				stream.Write(buffer, 0, num3);
				num2 += (long)num3;
				worker.TotalFileDownloaded = stream.Length;
				worker.PercentComplete = (int)(stream.Length * 100L / range.Length);
			}
			bool flag5 = contentLength != num2;
			if (flag5)
			{
				string message = string.Format(CultureInfo.InvariantCulture, "totalContentRead({0}) != contentLength({1})", new object[]
				{
					num2,
					contentLength
				});
				throw new Exception(message);
			}
		}
		catch (Exception ex)
		{
			worker.Exception = ex;
			Logger.Error(ex.ToString());
		}
		finally
		{
			bool flag6 = stream2 != null;
			if (flag6)
			{
				stream2.Close();
			}
			bool flag7 = httpWebResponse != null;
			if (flag7)
			{
				httpWebResponse.Close();
			}
			bool flag8 = stream != null;
			if (flag8)
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

	// Token: 0x06000016 RID: 22 RVA: 0x00002CDC File Offset: 0x00000EDC
	private static bool IsPayloadOk(string payloadFileName, long remoteSize)
	{
		long length = new FileInfo(payloadFileName).Length;
		Logger.Info("payloadSize = " + length.ToString() + " remoteSize = " + remoteSize.ToString());
		return length == remoteSize;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002D24 File Offset: 0x00000F24
	public void AbortDownload()
	{
		bool flag = this.mWorkers == null;
		if (!flag)
		{
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
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002DCC File Offset: 0x00000FCC
	private static void WaitForWorkers(List<KeyValuePair<Thread, LegacyDownloader.Worker>> workers)
	{
		foreach (KeyValuePair<Thread, LegacyDownloader.Worker> keyValuePair in workers)
		{
			keyValuePair.Key.Join();
		}
		foreach (KeyValuePair<Thread, LegacyDownloader.Worker> keyValuePair2 in workers)
		{
			bool flag = keyValuePair2.Value.Exception != null;
			if (flag)
			{
				throw new WorkerException(keyValuePair2.Value.Exception.Message, keyValuePair2.Value.Exception);
			}
		}
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002E98 File Offset: 0x00001098
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

	// Token: 0x04000006 RID: 6
	private List<KeyValuePair<Thread, LegacyDownloader.Worker>> mWorkers;

	// Token: 0x04000007 RID: 7
	private WebHeaderCollection mResponseHeaders;

	// Token: 0x04000008 RID: 8
	private readonly string mUrl;

	// Token: 0x04000009 RID: 9
	private string mFileName;

	// Token: 0x0400000A RID: 10
	private int mNrWorkers;

	// Token: 0x0400000B RID: 11
	private LegacyDownloader.UpdateProgressCallback mUpdateProgressCallback;

	// Token: 0x0400000C RID: 12
	private LegacyDownloader.DownloadCompletedCallback mDownloadCompletedCallback;

	// Token: 0x0400000D RID: 13
	private LegacyDownloader.ExceptionCallback mExceptionCallback;

	// Token: 0x0400000E RID: 14
	private LegacyDownloader.ContentTypeCallback mContentTypeCallback;

	// Token: 0x0400000F RID: 15
	private LegacyDownloader.SizeDownloadedCallback mSizeDownloadedCallback;

	// Token: 0x04000010 RID: 16
	private LegacyDownloader.PayloadInfoCallback mPayloadInfoCallback;

	// Token: 0x020000A3 RID: 163
	// (Invoke) Token: 0x060003CC RID: 972
	public delegate void UpdateProgressCallback(int percent);

	// Token: 0x020000A4 RID: 164
	// (Invoke) Token: 0x060003D0 RID: 976
	public delegate void DownloadCompletedCallback(string filePath);

	// Token: 0x020000A5 RID: 165
	// (Invoke) Token: 0x060003D4 RID: 980
	public delegate void ExceptionCallback(Exception e);

	// Token: 0x020000A6 RID: 166
	// (Invoke) Token: 0x060003D8 RID: 984
	public delegate bool ContentTypeCallback(string contentType);

	// Token: 0x020000A7 RID: 167
	// (Invoke) Token: 0x060003DC RID: 988
	public delegate void SizeDownloadedCallback(long size);

	// Token: 0x020000A8 RID: 168
	// (Invoke) Token: 0x060003E0 RID: 992
	public delegate void PayloadInfoCallback(long pInfo);

	// Token: 0x020000A9 RID: 169
	// (Invoke) Token: 0x060003E4 RID: 996
	private delegate void ProgressCallback();

	// Token: 0x020000AA RID: 170
	private class Worker
	{
		// Token: 0x060003E7 RID: 999 RVA: 0x00018ACE File Offset: 0x00016CCE
		public Worker(int id, string url, string payloadName, Range range)
		{
			this.Id = id;
			this.URL = url;
			this.m_PayloadName = payloadName;
			this.Range = range;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00018AFC File Offset: 0x00016CFC
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x00018B04 File Offset: 0x00016D04
		public bool Cancelled { get; set; } = false;

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x00018B0D File Offset: 0x00016D0D
		public int Id { get; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00018B15 File Offset: 0x00016D15
		public string PartFileName
		{
			get
			{
				return LegacyDownloader.MakePartFileName(this.m_PayloadName, this.Id);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00018B28 File Offset: 0x00016D28
		public Range Range { get; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00018B30 File Offset: 0x00016D30
		public string URL { get; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00018B38 File Offset: 0x00016D38
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x00018B40 File Offset: 0x00016D40
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

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00018B56 File Offset: 0x00016D56
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x00018B5E File Offset: 0x00016D5E
		public long TotalFileDownloaded { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00018B67 File Offset: 0x00016D67
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x00018B6F File Offset: 0x00016D6F
		public LegacyDownloader.ProgressCallback ProgressCallback { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00018B78 File Offset: 0x00016D78
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x00018B80 File Offset: 0x00016D80
		public Exception Exception { get; set; }

		// Token: 0x04000585 RID: 1413
		private readonly string m_PayloadName;

		// Token: 0x04000586 RID: 1414
		private int m_PercentComplete;
	}
}
