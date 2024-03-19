using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;

namespace BlueStacks.Common
{
	// Token: 0x02000197 RID: 407
	public class Downloader
	{
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000F29 RID: 3881 RVA: 0x0003BAF4 File Offset: 0x00039CF4
		// (remove) Token: 0x06000F2A RID: 3882 RVA: 0x0003BB2C File Offset: 0x00039D2C
		public event Downloader.DownloadRetryEventHandler DownloadRetry;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06000F2B RID: 3883 RVA: 0x0003BB64 File Offset: 0x00039D64
		// (remove) Token: 0x06000F2C RID: 3884 RVA: 0x0003BB9C File Offset: 0x00039D9C
		public event Downloader.DownloadExceptionEventHandler DownloadException;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000F2D RID: 3885 RVA: 0x0003BBD4 File Offset: 0x00039DD4
		// (remove) Token: 0x06000F2E RID: 3886 RVA: 0x0003BC0C File Offset: 0x00039E0C
		public event Downloader.UnsupportedResumeEventHandler UnsupportedResume;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000F2F RID: 3887 RVA: 0x0003BC44 File Offset: 0x00039E44
		// (remove) Token: 0x06000F30 RID: 3888 RVA: 0x0003BC7C File Offset: 0x00039E7C
		public event Downloader.FilePayloadInfoReceivedHandler FilePayloadInfoReceived;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000F31 RID: 3889 RVA: 0x0003BCB4 File Offset: 0x00039EB4
		// (remove) Token: 0x06000F32 RID: 3890 RVA: 0x0003BCEC File Offset: 0x00039EEC
		public event Downloader.DownloadFileCompletedEventHandler DownloadFileCompleted;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06000F33 RID: 3891 RVA: 0x0003BD24 File Offset: 0x00039F24
		// (remove) Token: 0x06000F34 RID: 3892 RVA: 0x0003BD5C File Offset: 0x00039F5C
		public event Downloader.DownloadProgressChangedEventHandler DownloadProgressChanged;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000F35 RID: 3893 RVA: 0x0003BD94 File Offset: 0x00039F94
		// (remove) Token: 0x06000F36 RID: 3894 RVA: 0x0003BDCC File Offset: 0x00039FCC
		public event Downloader.DownloadProgressPercentChangedEventHandler DownloadProgressPercentChanged;

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000F37 RID: 3895 RVA: 0x0003BE04 File Offset: 0x0003A004
		// (remove) Token: 0x06000F38 RID: 3896 RVA: 0x0003BE3C File Offset: 0x0003A03C
		public event Downloader.DownloadCancelledEventHandler DownloadCancelled;

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x0000D6FE File Offset: 0x0000B8FE
		// (set) Token: 0x06000F3A RID: 3898 RVA: 0x0000D706 File Offset: 0x0000B906
		public bool IsDownLoadCanceled { get; set; }

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000F3B RID: 3899 RVA: 0x0000D70F File Offset: 0x0000B90F
		// (set) Token: 0x06000F3C RID: 3900 RVA: 0x0000D717 File Offset: 0x0000B917
		public bool IsDownloadInProgress { get; private set; }

		// Token: 0x06000F3D RID: 3901 RVA: 0x0000D720 File Offset: 0x0000B920
		protected virtual void OnDownloadProgressChanged(long bytes)
		{
			Downloader.DownloadProgressChangedEventHandler downloadProgressChanged = this.DownloadProgressChanged;
			if (downloadProgressChanged == null)
			{
				return;
			}
			downloadProgressChanged(bytes);
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x0000D733 File Offset: 0x0000B933
		protected virtual void OnDownloadPercentProgressChanged(double percent)
		{
			Downloader.DownloadProgressPercentChangedEventHandler downloadProgressPercentChanged = this.DownloadProgressPercentChanged;
			if (downloadProgressPercentChanged == null)
			{
				return;
			}
			downloadProgressPercentChanged(percent);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0000D746 File Offset: 0x0000B946
		protected virtual void OnDownloadException(Exception e)
		{
			Downloader.DownloadExceptionEventHandler downloadException = this.DownloadException;
			if (downloadException == null)
			{
				return;
			}
			downloadException(e);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0000D759 File Offset: 0x0000B959
		protected virtual void OnDownloadFileCompleted()
		{
			Downloader.DownloadFileCompletedEventHandler downloadFileCompleted = this.DownloadFileCompleted;
			if (downloadFileCompleted == null)
			{
				return;
			}
			downloadFileCompleted(this, new EventArgs());
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0000D771 File Offset: 0x0000B971
		protected virtual void OnFilePayloadInfoReceived(long size)
		{
			Downloader.FilePayloadInfoReceivedHandler filePayloadInfoReceived = this.FilePayloadInfoReceived;
			if (filePayloadInfoReceived == null)
			{
				return;
			}
			filePayloadInfoReceived(size);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0000D784 File Offset: 0x0000B984
		protected virtual void OnUnsupportedResume(HttpStatusCode code)
		{
			Downloader.UnsupportedResumeEventHandler unsupportedResume = this.UnsupportedResume;
			if (unsupportedResume == null)
			{
				return;
			}
			unsupportedResume(code);
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0000D797 File Offset: 0x0000B997
		protected virtual void OnDownloadCancelled()
		{
			Downloader.DownloadCancelledEventHandler downloadCancelled = this.DownloadCancelled;
			if (downloadCancelled == null)
			{
				return;
			}
			downloadCancelled();
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x0000D7A9 File Offset: 0x0000B9A9
		protected virtual void OnDownloadRetryEvent()
		{
			Downloader.DownloadRetryEventHandler downloadRetry = this.DownloadRetry;
			if (downloadRetry == null)
			{
				return;
			}
			downloadRetry(this, new EventArgs());
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x0003BE74 File Offset: 0x0003A074
		public void DownloadFile(string url, string fileDestination)
		{
			this.IsDownloadInProgress = true;
			FileStream fileStream = null;
			HttpWebRequest httpWebRequest = null;
			HttpWebResponse httpWebResponse = null;
			try
			{
				if (File.Exists(fileDestination))
				{
					Logger.Info("{0} already downloaded to {1}", new object[]
					{
						url,
						fileDestination
					});
					this.OnDownloadFileCompleted();
				}
				else
				{
					string text = fileDestination + ".tmp";
					try
					{
						fileStream = new FileStream(text, FileMode.Append, FileAccess.Write, FileShare.None);
					}
					catch (Exception e)
					{
						this.OnDownloadException(e);
						return;
					}
					long num = fileStream.Length;
					int num2 = 0;
					for (;;)
					{
						long num3 = num;
						try
						{
							httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
							Downloader.AddRangeToRequest(httpWebRequest, string.Format(CultureInfo.InvariantCulture, "{0}-", new object[]
							{
								num
							}), "bytes");
							httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
						}
						catch (WebException ex)
						{
							HttpStatusCode statusCode = ((HttpWebResponse)ex.Response).StatusCode;
							if (statusCode == HttpStatusCode.RequestedRangeNotSatisfiable)
							{
								Logger.Warning("Unsupported resume! {0}", new object[]
								{
									statusCode
								});
								if (fileStream != null)
								{
									fileStream.Close();
								}
								this.OnUnsupportedResume(statusCode);
								return;
							}
							Logger.Warning("An error occured while creating a request. WebEx: {0}", new object[]
							{
								ex.Message
							});
							goto IL_34B;
						}
						catch (Exception ex2)
						{
							Logger.Warning("An error occured while creating a request. Ex: {0}", new object[]
							{
								ex2.Message
							});
							goto IL_34B;
						}
						goto IL_143;
						IL_34B:
						this.OnDownloadRetryEvent();
						if (num3 == num)
						{
							num2++;
						}
						if (num2 == 20)
						{
							this.OnDownloadException(new UnknownErrorException());
						}
						if (httpWebRequest != null)
						{
							httpWebRequest.Abort();
						}
						httpWebRequest = null;
						if (httpWebResponse != null)
						{
							httpWebResponse.Close();
						}
						httpWebResponse = null;
						int num4;
						if (num2 > 10)
						{
							num4 = 1800;
						}
						else
						{
							num4 = Convert.ToInt32(Math.Pow(2.0, (double)num2));
						}
						Logger.Info("Will retry after {0}s", new object[]
						{
							num4
						});
						Thread.Sleep(num4 * 1000);
						continue;
						IL_143:
						if (httpWebResponse.StatusCode != HttpStatusCode.PartialContent && httpWebResponse.StatusCode != HttpStatusCode.OK)
						{
							Logger.Warning("Got an unexpected status code: {0}", new object[]
							{
								httpWebResponse.StatusCode
							});
							goto IL_34B;
						}
						if (num != 0L && httpWebResponse.StatusCode != HttpStatusCode.PartialContent)
						{
							break;
						}
						long num5 = httpWebResponse.ContentLength + num;
						this.OnFilePayloadInfoReceived(num5);
						Stream responseStream;
						try
						{
							responseStream = httpWebResponse.GetResponseStream();
						}
						catch (Exception ex3)
						{
							Logger.Warning("An error occured while getting a response stream: {0}", new object[]
							{
								ex3.Message
							});
							goto IL_34B;
						}
						byte[] buffer = new byte[10485760];
						for (;;)
						{
							int num6;
							try
							{
								if (this.IsDownLoadCanceled)
								{
									fileStream.Close();
									fileStream = null;
									if (File.Exists(fileDestination))
									{
										File.Delete(fileDestination);
									}
									if (File.Exists(text))
									{
										File.Delete(text);
									}
									this.OnDownloadCancelled();
									return;
								}
								num6 = responseStream.Read(buffer, 0, 10485760);
							}
							catch (Exception ex4)
							{
								Logger.Warning("Some error while reading from the stream. Ex: {0}", new object[]
								{
									ex4.Message
								});
								goto IL_34B;
							}
							if (num6 == 0)
							{
								break;
							}
							try
							{
								fileStream.Write(buffer, 0, num6);
							}
							catch (Exception ex5)
							{
								Logger.Warning("Some error while writing the stream to file. Ex: {0}", new object[]
								{
									ex5.Message
								});
								this.OnDownloadException(ex5);
								return;
							}
							num += (long)num6;
							this.OnDownloadProgressChanged(num);
							this.OnDownloadPercentProgressChanged((double)Math.Round(decimal.Divide(num, num5) * 100m, 2));
						}
						if (num != num5)
						{
							Logger.Error("Stream does not have more bytes to read. {0} != {1}", new object[]
							{
								num,
								num5
							});
							goto IL_34B;
						}
						goto IL_28B;
					}
					this.OnUnsupportedResume(httpWebResponse.StatusCode);
					return;
					IL_28B:
					try
					{
						fileStream.Close();
						fileStream = null;
						File.Move(text, fileDestination);
						this.OnDownloadFileCompleted();
					}
					catch (Exception ex6)
					{
						Logger.Warning("Could not move file to destination. Ex: {0}", new object[]
						{
							ex6.Message
						});
						this.OnDownloadException(ex6);
					}
				}
			}
			catch (Exception ex7)
			{
				Logger.Error("Unable to download the file: {0}", new object[]
				{
					ex7.Message
				});
				Downloader.ThrowOnFatalException(ex7);
				this.OnDownloadException(ex7);
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
				if (httpWebRequest != null)
				{
					httpWebRequest.Abort();
				}
				if (httpWebResponse != null)
				{
					httpWebResponse.Close();
				}
				this.IsDownloadInProgress = false;
			}
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0000D7C1 File Offset: 0x0000B9C1
		private long GetSizeFromResponseHeaders(WebHeaderCollection headers)
		{
			return Convert.ToInt64(headers["Content-Range"].Split(new char[]
			{
				'/'
			})[1], CultureInfo.InvariantCulture);
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0000D7EA File Offset: 0x0000B9EA
		private static void ThrowOnFatalException(Exception e)
		{
			if (e is ThreadAbortException || e is StackOverflowException || e is OutOfMemoryException)
			{
				throw e;
			}
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0003C384 File Offset: 0x0003A584
		private static void AddRangeToRequest(WebRequest req, string range, string rangeSpecifier = "bytes")
		{
			MethodInfo method = typeof(WebHeaderCollection).GetMethod("AddWithoutValidate", BindingFlags.Instance | BindingFlags.NonPublic);
			string text = "Range";
			string text2 = string.Format(CultureInfo.InvariantCulture, "{0}={1}", new object[]
			{
				rangeSpecifier,
				range
			});
			method.Invoke(req.Headers, new object[]
			{
				text,
				text2
			});
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0003C3E8 File Offset: 0x0003A5E8
		private long GetSizeFromContentRange(HttpWebResponse webResponse)
		{
			string text = webResponse.Headers["Content-Range"];
			char[] separator = new char[]
			{
				'/'
			};
			string[] array = text.Split(separator);
			return Convert.ToInt64(array[array.Length - 1], CultureInfo.InvariantCulture);
		}

		// Token: 0x04000758 RID: 1880
		private const int DEFAULT_BUFFER_LENGTH = 10485760;

		// Token: 0x02000198 RID: 408
		// (Invoke) Token: 0x06000F4C RID: 3916
		public delegate void DownloadRetryEventHandler(object sender, EventArgs args);

		// Token: 0x02000199 RID: 409
		// (Invoke) Token: 0x06000F50 RID: 3920
		public delegate void DownloadFileCompletedEventHandler(object sender, EventArgs args);

		// Token: 0x0200019A RID: 410
		// (Invoke) Token: 0x06000F54 RID: 3924
		public delegate void FilePayloadInfoReceivedHandler(long size);

		// Token: 0x0200019B RID: 411
		// (Invoke) Token: 0x06000F58 RID: 3928
		public delegate void DownloadExceptionEventHandler(Exception e);

		// Token: 0x0200019C RID: 412
		// (Invoke) Token: 0x06000F5C RID: 3932
		public delegate void DownloadProgressChangedEventHandler(long bytes);

		// Token: 0x0200019D RID: 413
		// (Invoke) Token: 0x06000F60 RID: 3936
		public delegate void UnsupportedResumeEventHandler(HttpStatusCode sc);

		// Token: 0x0200019E RID: 414
		// (Invoke) Token: 0x06000F64 RID: 3940
		public delegate void DownloadCancelledEventHandler();

		// Token: 0x0200019F RID: 415
		// (Invoke) Token: 0x06000F68 RID: 3944
		public delegate void DownloadProgressPercentChangedEventHandler(double percent);
	}
}
