using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using BlueStacks.Common;

// Token: 0x02000007 RID: 7
public class SplitDownloader
{
	// Token: 0x06000017 RID: 23 RVA: 0x0000FC54 File Offset: 0x0000DE54
	public SplitDownloader(string manifestURL, string dirPath, string userGUID, int nrWorkers)
	{
		this.m_ManifestURL = manifestURL;
		this.m_DirPath = dirPath;
		this.m_UserGUID = userGUID;
		this.m_UserAgent = string.Format(CultureInfo.InvariantCulture, "SplitDownloader {0}/{1}/{2}", new object[]
		{
			"BlueStacks",
			"4.250.0.1070",
			this.m_UserGUID
		});
		this.m_NrWorkers = nrWorkers;
		this.m_Workers = new SerialWorkQueue[nrWorkers];
		for (int i = 0; i < this.m_NrWorkers; i++)
		{
			this.m_Workers[i] = new SerialWorkQueue();
		}
		this.m_WorkersStarted = false;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x0000222F File Offset: 0x0000042F
	public void Download(SplitDownloader.ProgressCb progressCb, SplitDownloader.CompletedCb completedCb, SplitDownloader.ExceptionCb exceptionCb)
	{
		this.Download(progressCb, completedCb, exceptionCb, null);
	}

	// Token: 0x06000019 RID: 25 RVA: 0x0000FCEC File Offset: 0x0000DEEC
	public void Download(SplitDownloader.ProgressCb progressCb, SplitDownloader.CompletedCb completedCb, SplitDownloader.ExceptionCb exceptionCb, SplitDownloader.FileSizeCb fileSizeCb)
	{
		this.m_ProgressCb = progressCb;
		this.m_CompletedCb = completedCb;
		this.m_ExceptionCb = exceptionCb;
		this.m_FileSizeCb = fileSizeCb;
		try
		{
			this.m_Manifest = this.GetManifest();
			this.GetManifestFilePath();
			if (this.m_FileSizeCb != null)
			{
				this.m_FileSizeCb(this.m_Manifest.FileSize);
			}
			this.StartWorkers();
			this.m_ProgressCb(this.m_Manifest.PercentDownloaded());
			int num = 0;
			while ((long)num < this.m_Manifest.Count)
			{
				FilePart filePart = this.m_Manifest[num];
				SerialWorkQueue.Work work = this.MakeWork(filePart);
				this.m_Workers[num % this.m_NrWorkers].Enqueue(work);
				num++;
			}
			this.StopAndWaitWorkers();
			if (!this.m_Manifest.Check())
			{
				throw new CheckFailedException();
			}
			string filePath = this.m_Manifest.MakeFile();
			this.m_Manifest.DeleteFileParts();
			this.m_Manifest.DeleteManifest();
			this.m_CompletedCb(filePath);
		}
		catch (Exception ex)
		{
			Logger.Error(ex.ToString());
			this.m_ExceptionCb(ex);
		}
		finally
		{
			if (this.m_WorkersStarted)
			{
				this.StopAndWaitWorkers();
			}
		}
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000FE38 File Offset: 0x0000E038
	private void StartWorkers()
	{
		for (int i = 0; i < this.m_NrWorkers; i++)
		{
			this.m_Workers[i].Start();
		}
		this.m_WorkersStarted = true;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x0000FE6C File Offset: 0x0000E06C
	private void StopAndWaitWorkers()
	{
		for (int i = 0; i < this.m_NrWorkers; i++)
		{
			this.m_Workers[i].Stop();
		}
		for (int j = 0; j < this.m_NrWorkers; j++)
		{
			this.m_Workers[j].Join();
		}
		this.m_WorkersStarted = false;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x0000223B File Offset: 0x0000043B
	private SerialWorkQueue.Work MakeWork(FilePart filePart)
	{
		return delegate()
		{
			try
			{
				if (filePart.Check())
				{
					Logger.Info(filePart.Path + " is already downloaded");
				}
				else
				{
					this.DownloadFilePart(filePart);
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex.ToString());
			}
		};
	}

	// Token: 0x0600001D RID: 29 RVA: 0x0000FEBC File Offset: 0x0000E0BC
	private string GetManifestFilePath()
	{
		string fileName = Path.GetFileName(new Uri(this.m_ManifestURL).AbsolutePath);
		return Path.Combine(this.m_DirPath, fileName);
	}

	// Token: 0x0600001E RID: 30 RVA: 0x0000FEEC File Offset: 0x0000E0EC
	private Manifest GetManifest()
	{
		string manifestFilePath = this.GetManifestFilePath();
		Logger.Info("Downloading " + this.m_ManifestURL + " to " + manifestFilePath);
		bool downloaded = false;
		Exception capturedException = null;
		SplitDownloader.DownloadFile(this.m_ManifestURL, manifestFilePath, this.m_UserAgent, delegate(long downloadedSize, long totalSize)
		{
			Logger.Info("Downloaded (" + downloadedSize.ToString() + " bytes) out of " + totalSize.ToString());
		}, delegate(string filePath)
		{
			downloaded = true;
			Logger.Info("Downloaded " + this.m_ManifestURL + " to " + filePath);
		}, delegate(Exception e)
		{
			downloaded = false;
			capturedException = e;
			Logger.Error(e.ToString());
		});
		if (!downloaded)
		{
			throw capturedException;
		}
		Manifest manifest = new Manifest(manifestFilePath);
		manifest.Build();
		return manifest;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x0000FF9C File Offset: 0x0000E19C
	private void DownloadFilePart(FilePart filePart)
	{
		string filePartURL = filePart.URL(this.m_ManifestURL);
		Logger.Info("Downloading " + filePartURL + " to " + filePart.Path);
		bool downloaded = false;
		Exception capturedException = null;
		SplitDownloader.DownloadFile(filePartURL, filePart.Path, this.m_UserAgent, delegate(long downloadedSize, long totalSize)
		{
			filePart.DownloadedSize = downloadedSize;
			if (this.m_PercentDownloaded != this.m_Manifest.PercentDownloaded())
			{
				this.m_ProgressCb(this.m_Manifest.PercentDownloaded());
			}
			this.m_PercentDownloaded = this.m_Manifest.PercentDownloaded();
		}, delegate(string filePath)
		{
			downloaded = true;
			Logger.Info("Downloaded " + filePartURL + " to " + filePart.Path);
		}, delegate(Exception e)
		{
			downloaded = false;
			capturedException = e;
			Logger.Error(e.ToString());
		});
		if (!downloaded)
		{
			throw capturedException;
		}
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00010058 File Offset: 0x0000E258
	private static void DownloadFile(string url, string filePath, string userAgent, SplitDownloader.DownloadFileProgressCb progressCb, SplitDownloader.DownloadFileCompletedCb completedCb, SplitDownloader.DownloadFileExceptionCb exceptionCb)
	{
		FileStream fileStream = null;
		HttpWebResponse httpWebResponse = null;
		Stream stream = null;
		bool flag = false;
		try
		{
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.UserAgent = userAgent;
			httpWebRequest.KeepAlive = false;
			httpWebRequest.ReadWriteTimeout = 60000;
			httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			long contentLength = httpWebResponse.ContentLength;
			stream = httpWebResponse.GetResponseStream();
			Logger.Warning(string.Format(CultureInfo.InvariantCulture, "HTTP Response Header\nStatusCode: {0}\n{1}", new object[]
			{
				(int)httpWebResponse.StatusCode,
				httpWebResponse.Headers
			}));
			int num = 4096;
			byte[] buffer = new byte[num];
			long num2 = 0L;
			fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
			int num3;
			while ((num3 = stream.Read(buffer, 0, num)) > 0)
			{
				fileStream.Write(buffer, 0, num3);
				num2 += (long)num3;
				progressCb(num2, contentLength);
			}
			if (contentLength != num2)
			{
				throw new Exception(string.Format(CultureInfo.InvariantCulture, "totalContentRead({0}) != contentLength({1})", new object[]
				{
					num2,
					contentLength
				}));
			}
			flag = true;
		}
		catch (Exception ex)
		{
			Logger.Error(ex.ToString());
			exceptionCb(ex);
		}
		finally
		{
			if (stream != null)
			{
				stream.Close();
			}
			if (httpWebResponse != null)
			{
				httpWebResponse.Close();
			}
			if (fileStream != null)
			{
				fileStream.Flush();
				fileStream.Close();
				Thread.Sleep(1000);
			}
		}
		if (flag)
		{
			completedCb(filePath);
		}
	}

	// Token: 0x04000008 RID: 8
	private string m_ManifestURL;

	// Token: 0x04000009 RID: 9
	private string m_DirPath;

	// Token: 0x0400000A RID: 10
	private string m_UserGUID;

	// Token: 0x0400000B RID: 11
	private string m_UserAgent;

	// Token: 0x0400000C RID: 12
	private SplitDownloader.ProgressCb m_ProgressCb;

	// Token: 0x0400000D RID: 13
	private SplitDownloader.CompletedCb m_CompletedCb;

	// Token: 0x0400000E RID: 14
	private SplitDownloader.ExceptionCb m_ExceptionCb;

	// Token: 0x0400000F RID: 15
	private SplitDownloader.FileSizeCb m_FileSizeCb;

	// Token: 0x04000010 RID: 16
	private int m_NrWorkers;

	// Token: 0x04000011 RID: 17
	private SerialWorkQueue[] m_Workers;

	// Token: 0x04000012 RID: 18
	private bool m_WorkersStarted;

	// Token: 0x04000013 RID: 19
	private Manifest m_Manifest;

	// Token: 0x04000014 RID: 20
	private float m_PercentDownloaded;

	// Token: 0x02000008 RID: 8
	// (Invoke) Token: 0x06000022 RID: 34
	public delegate void ProgressCb(float percent);

	// Token: 0x02000009 RID: 9
	// (Invoke) Token: 0x06000026 RID: 38
	public delegate void CompletedCb(string filePath);

	// Token: 0x0200000A RID: 10
	// (Invoke) Token: 0x0600002A RID: 42
	public delegate void ExceptionCb(Exception e);

	// Token: 0x0200000B RID: 11
	// (Invoke) Token: 0x0600002E RID: 46
	public delegate void FileSizeCb(long fileSize);

	// Token: 0x0200000C RID: 12
	// (Invoke) Token: 0x06000032 RID: 50
	public delegate void DownloadFileProgressCb(long downloaded, long size);

	// Token: 0x0200000D RID: 13
	// (Invoke) Token: 0x06000036 RID: 54
	public delegate void DownloadFileCompletedCb(string filePath);

	// Token: 0x0200000E RID: 14
	// (Invoke) Token: 0x0600003A RID: 58
	public delegate void DownloadFileExceptionCb(Exception e);
}
