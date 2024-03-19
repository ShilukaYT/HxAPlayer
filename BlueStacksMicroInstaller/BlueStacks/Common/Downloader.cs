using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;

namespace BlueStacks.Common
{
	// Token: 0x0200000E RID: 14
	public class Downloader
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000034 RID: 52 RVA: 0x0000330C File Offset: 0x0000150C
		// (remove) Token: 0x06000035 RID: 53 RVA: 0x00003344 File Offset: 0x00001544
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Downloader.DownloadRetryEventHandler DownloadRetry;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000036 RID: 54 RVA: 0x0000337C File Offset: 0x0000157C
		// (remove) Token: 0x06000037 RID: 55 RVA: 0x000033B4 File Offset: 0x000015B4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Downloader.DownloadExceptionEventHandler DownloadException;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000038 RID: 56 RVA: 0x000033EC File Offset: 0x000015EC
		// (remove) Token: 0x06000039 RID: 57 RVA: 0x00003424 File Offset: 0x00001624
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Downloader.UnsupportedResumeEventHandler UnsupportedResume;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600003A RID: 58 RVA: 0x0000345C File Offset: 0x0000165C
		// (remove) Token: 0x0600003B RID: 59 RVA: 0x00003494 File Offset: 0x00001694
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Downloader.FilePayloadInfoReceivedHandler FilePayloadInfoReceived;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600003C RID: 60 RVA: 0x000034CC File Offset: 0x000016CC
		// (remove) Token: 0x0600003D RID: 61 RVA: 0x00003504 File Offset: 0x00001704
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Downloader.DownloadFileCompletedEventHandler DownloadFileCompleted;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600003E RID: 62 RVA: 0x0000353C File Offset: 0x0000173C
		// (remove) Token: 0x0600003F RID: 63 RVA: 0x00003574 File Offset: 0x00001774
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Downloader.DownloadProgressChangedEventHandler DownloadProgressChanged;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000040 RID: 64 RVA: 0x000035AC File Offset: 0x000017AC
		// (remove) Token: 0x06000041 RID: 65 RVA: 0x000035E4 File Offset: 0x000017E4
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Downloader.DownloadProgressPercentChangedEventHandler DownloadProgressPercentChanged;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000042 RID: 66 RVA: 0x0000361C File Offset: 0x0000181C
		// (remove) Token: 0x06000043 RID: 67 RVA: 0x00003654 File Offset: 0x00001854
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Downloader.DownloadCancelledEventHandler DownloadCancelled;

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00003689 File Offset: 0x00001889
		// (set) Token: 0x06000045 RID: 69 RVA: 0x00003691 File Offset: 0x00001891
		public bool IsDownLoadCanceled { get; set; } = false;

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000369A File Offset: 0x0000189A
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000036A2 File Offset: 0x000018A2
		public bool IsDownloadInProgress { get; private set; } = false;

		// Token: 0x06000048 RID: 72 RVA: 0x000036AB File Offset: 0x000018AB
		protected virtual void OnDownloadProgressChanged(long bytes)
		{
			Downloader.DownloadProgressChangedEventHandler downloadProgressChanged = this.DownloadProgressChanged;
			if (downloadProgressChanged != null)
			{
				downloadProgressChanged(bytes);
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000036C1 File Offset: 0x000018C1
		protected virtual void OnDownloadPercentProgressChanged(double percent)
		{
			Downloader.DownloadProgressPercentChangedEventHandler downloadProgressPercentChanged = this.DownloadProgressPercentChanged;
			if (downloadProgressPercentChanged != null)
			{
				downloadProgressPercentChanged(percent);
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000036D7 File Offset: 0x000018D7
		protected virtual void OnDownloadException(Exception e)
		{
			Downloader.DownloadExceptionEventHandler downloadException = this.DownloadException;
			if (downloadException != null)
			{
				downloadException(e);
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000036ED File Offset: 0x000018ED
		protected virtual void OnDownloadFileCompleted()
		{
			Downloader.DownloadFileCompletedEventHandler downloadFileCompleted = this.DownloadFileCompleted;
			if (downloadFileCompleted != null)
			{
				downloadFileCompleted(this, new EventArgs());
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003708 File Offset: 0x00001908
		protected virtual void OnFilePayloadInfoReceived(long size)
		{
			Downloader.FilePayloadInfoReceivedHandler filePayloadInfoReceived = this.FilePayloadInfoReceived;
			if (filePayloadInfoReceived != null)
			{
				filePayloadInfoReceived(size);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000371E File Offset: 0x0000191E
		protected virtual void OnUnsupportedResume(HttpStatusCode code)
		{
			Downloader.UnsupportedResumeEventHandler unsupportedResume = this.UnsupportedResume;
			if (unsupportedResume != null)
			{
				unsupportedResume(code);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003734 File Offset: 0x00001934
		protected virtual void OnDownloadCancelled()
		{
			Downloader.DownloadCancelledEventHandler downloadCancelled = this.DownloadCancelled;
			if (downloadCancelled != null)
			{
				downloadCancelled();
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003749 File Offset: 0x00001949
		protected virtual void OnDownloadRetryEvent()
		{
			Downloader.DownloadRetryEventHandler downloadRetry = this.DownloadRetry;
			if (downloadRetry != null)
			{
				downloadRetry(this, new EventArgs());
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003764 File Offset: 0x00001964
		public void DownloadFile(string url, string fileDestination)
		{
			this.IsDownloadInProgress = true;
			FileStream fileStream = null;
			HttpWebRequest httpWebRequest = null;
			HttpWebResponse httpWebResponse = null;
			try
			{
				bool flag = File.Exists(fileDestination);
				if (flag)
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
							bool flag2 = statusCode == HttpStatusCode.RequestedRangeNotSatisfiable;
							if (flag2)
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
							goto IL_3DD;
						}
						catch (Exception ex2)
						{
							Logger.Warning("An error occured while creating a request. Ex: {0}", new object[]
							{
								ex2.Message
							});
							goto IL_3DD;
						}
						goto IL_16B;
						IL_3DD:
						this.OnDownloadRetryEvent();
						bool flag3 = num3 == num;
						if (flag3)
						{
							num2++;
						}
						bool flag4 = num2 == 20;
						if (flag4)
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
						bool flag5 = num2 > 10;
						int num4;
						if (flag5)
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
						IL_16B:
						bool flag6 = httpWebResponse.StatusCode != HttpStatusCode.PartialContent && httpWebResponse.StatusCode != HttpStatusCode.OK;
						if (flag6)
						{
							Logger.Warning("Got an unexpected status code: {0}", new object[]
							{
								httpWebResponse.StatusCode
							});
							goto IL_3DD;
						}
						bool flag7 = num != 0L && httpWebResponse.StatusCode != HttpStatusCode.PartialContent;
						if (flag7)
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
							goto IL_3DD;
						}
						byte[] buffer = new byte[10485760];
						for (;;)
						{
							int num6;
							try
							{
								bool isDownLoadCanceled = this.IsDownLoadCanceled;
								if (isDownLoadCanceled)
								{
									fileStream.Close();
									fileStream = null;
									bool flag8 = File.Exists(fileDestination);
									if (flag8)
									{
										File.Delete(fileDestination);
									}
									bool flag9 = File.Exists(text);
									if (flag9)
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
								goto IL_3DD;
							}
							bool flag10 = num6 == 0;
							if (flag10)
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
						bool flag11 = num != num5;
						if (flag11)
						{
							Logger.Error("Stream does not have more bytes to read. {0} != {1}", new object[]
							{
								num,
								num5
							});
							goto IL_3DD;
						}
						goto IL_308;
					}
					this.OnUnsupportedResume(httpWebResponse.StatusCode);
					return;
					IL_308:
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

		// Token: 0x06000051 RID: 81 RVA: 0x00003D44 File Offset: 0x00001F44
		private long GetSizeFromResponseHeaders(WebHeaderCollection headers)
		{
			string text = headers["Content-Range"];
			return Convert.ToInt64(text.Split(new char[]
			{
				'/'
			})[1], CultureInfo.InvariantCulture);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003D84 File Offset: 0x00001F84
		private static void ThrowOnFatalException(Exception e)
		{
			bool flag = e is ThreadAbortException || e is StackOverflowException || e is OutOfMemoryException;
			if (flag)
			{
				throw e;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003DB8 File Offset: 0x00001FB8
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

		// Token: 0x06000054 RID: 84 RVA: 0x00003E1C File Offset: 0x0000201C
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

		// Token: 0x04000031 RID: 49
		private const int DEFAULT_BUFFER_LENGTH = 10485760;

		// Token: 0x020000B2 RID: 178
		// (Invoke) Token: 0x060003FA RID: 1018
		public delegate void DownloadRetryEventHandler(object sender, EventArgs args);

		// Token: 0x020000B3 RID: 179
		// (Invoke) Token: 0x060003FE RID: 1022
		public delegate void DownloadFileCompletedEventHandler(object sender, EventArgs args);

		// Token: 0x020000B4 RID: 180
		// (Invoke) Token: 0x06000402 RID: 1026
		public delegate void FilePayloadInfoReceivedHandler(long size);

		// Token: 0x020000B5 RID: 181
		// (Invoke) Token: 0x06000406 RID: 1030
		public delegate void DownloadExceptionEventHandler(Exception e);

		// Token: 0x020000B6 RID: 182
		// (Invoke) Token: 0x0600040A RID: 1034
		public delegate void DownloadProgressChangedEventHandler(long bytes);

		// Token: 0x020000B7 RID: 183
		// (Invoke) Token: 0x0600040E RID: 1038
		public delegate void UnsupportedResumeEventHandler(HttpStatusCode sc);

		// Token: 0x020000B8 RID: 184
		// (Invoke) Token: 0x06000412 RID: 1042
		public delegate void DownloadCancelledEventHandler();

		// Token: 0x020000B9 RID: 185
		// (Invoke) Token: 0x06000416 RID: 1046
		public delegate void DownloadProgressPercentChangedEventHandler(double percent);
	}
}
