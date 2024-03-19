using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Web;

namespace BlueStacks.Common
{
	// Token: 0x020000C6 RID: 198
	public class HTTPServer : IDisposable
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00004CF6 File Offset: 0x00002EF6
		public int Port { get; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x00004CFE File Offset: 0x00002EFE
		// (set) Token: 0x060004D7 RID: 1239 RVA: 0x00004D06 File Offset: 0x00002F06
		public string RootDir { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00004D0F File Offset: 0x00002F0F
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x00004D16 File Offset: 0x00002F16
		public static bool FileWriteComplete { get; set; } = true;

		// Token: 0x060004DA RID: 1242 RVA: 0x00004D1E File Offset: 0x00002F1E
		public HTTPServer(int port, Dictionary<string, HTTPServer.RequestHandler> routes, string rootDir)
		{
			this.Port = port;
			this.Routes = routes;
			this.RootDir = rootDir;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00018AD0 File Offset: 0x00016CD0
		public void Start()
		{
			string uriPrefix = string.Format(CultureInfo.InvariantCulture, "http://{0}:{1}/", new object[]
			{
				"*",
				this.Port
			});
			this.mListener = new HttpListener();
			this.mListener.Prefixes.Add(uriPrefix);
			try
			{
				this.mShutDown = false;
				this.mListener.Start();
			}
			catch (HttpListenerException ex)
			{
				Logger.Error("Failed to start listener. err: " + ex.ToString());
				throw new ENoPortAvailableException("No free port available");
			}
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00018B6C File Offset: 0x00016D6C
		public void Run()
		{
			while (!this.mShutDown)
			{
				HttpListenerContext ctx = null;
				try
				{
					ctx = this.mListener.GetContext();
				}
				catch (Exception ex)
				{
					Logger.Error("Exception while processing HTTP context: " + ex.ToString());
					continue;
				}
				ThreadPool.QueueUserWorkItem(new WaitCallback(new HTTPServer.Worker(ctx, this.Routes, this.RootDir).ProcessRequest));
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00018BE0 File Offset: 0x00016DE0
		public void Stop()
		{
			if (this.mListener != null)
			{
				try
				{
					this.mShutDown = true;
					this.mListener.Close();
				}
				catch (HttpListenerException ex)
				{
					Logger.Error("Failed to stop listener. err: " + ex.ToString());
				}
			}
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00018C34 File Offset: 0x00016E34
		~HTTPServer()
		{
			this.Dispose(false);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00004D3B File Offset: 0x00002F3B
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				HttpListener httpListener = this.mListener;
				if (httpListener != null)
				{
					httpListener.Close();
				}
				this.disposedValue = true;
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00004D5F File Offset: 0x00002F5F
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400022A RID: 554
		private HttpListener mListener;

		// Token: 0x0400022B RID: 555
		private bool mShutDown;

		// Token: 0x0400022D RID: 557
		private readonly Dictionary<string, HTTPServer.RequestHandler> Routes;

		// Token: 0x04000230 RID: 560
		private bool disposedValue;

		// Token: 0x020000C7 RID: 199
		// (Invoke) Token: 0x060004E3 RID: 1251
		public delegate void RequestHandler(HttpListenerRequest req, HttpListenerResponse res);

		// Token: 0x020000C8 RID: 200
		private class Worker
		{
			// Token: 0x060004E6 RID: 1254 RVA: 0x00004D76 File Offset: 0x00002F76
			public Worker(HttpListenerContext ctx, Dictionary<string, HTTPServer.RequestHandler> routes, string rootDir)
			{
				this.mCtx = ctx;
				this.mRoutes = routes;
				this.mRootDir = rootDir;
			}

			// Token: 0x060004E7 RID: 1255 RVA: 0x00018C64 File Offset: 0x00016E64
			[STAThread]
			public void ProcessRequest(object stateInfo)
			{
				try
				{
					if (this.mCtx.Request.Url.AbsolutePath.StartsWith("/static/", StringComparison.OrdinalIgnoreCase))
					{
						this.StaticFileHandler(this.mCtx.Request, this.mCtx.Response);
					}
					else if (this.mCtx.Request.Url.AbsolutePath.StartsWith("/static2/", StringComparison.OrdinalIgnoreCase))
					{
						this.StaticFileChunkHandler(this.mCtx.Request, this.mCtx.Response, "");
					}
					else if (this.mCtx.Request.Url.AbsolutePath.StartsWith("/staticicon/", StringComparison.OrdinalIgnoreCase))
					{
						if (this.mCtx.Request.QueryString != null && this.mCtx.Request.QueryString.Count > 0)
						{
							string text = HttpUtility.ParseQueryString(this.mCtx.Request.Url.Query).Get("oem");
							if (InstalledOem.InstalledCoexistingOemList.Contains(text))
							{
								this.StaticFileChunkHandler(this.mCtx.Request, this.mCtx.Response, Path.Combine(RegistryManager.RegistryManagers[text].EngineDataDir, "UserData\\Gadget"));
							}
						}
						else
						{
							this.StaticFileChunkHandler(this.mCtx.Request, this.mCtx.Response, Path.Combine(RegistryManager.Instance.EngineDataDir, "UserData\\Gadget"));
						}
					}
					else if (this.mRoutes.ContainsKey(this.mCtx.Request.Url.AbsolutePath))
					{
						HTTPServer.RequestHandler requestHandler = this.mRoutes[this.mCtx.Request.Url.AbsolutePath];
						if (requestHandler != null)
						{
							if (this.mCtx.Request.UserAgent != null)
							{
								Logger.Info("Request received {0}", new object[]
								{
									this.mCtx.Request.Url.AbsolutePath
								});
								Logger.Debug("UserAgent = {0}", new object[]
								{
									this.mCtx.Request.UserAgent
								});
							}
							if (HTTPServer.Worker.IsTokenValid(this.mCtx.Request.Headers))
							{
								requestHandler(this.mCtx.Request, this.mCtx.Response);
							}
							else
							{
								Logger.Warning("Token validation check failed, unauthorized access");
								HTTPUtils.WriteErrorJson(this.mCtx.Response, "Unauthorized Access(401)");
								this.mCtx.Response.StatusCode = 401;
							}
						}
					}
					else
					{
						Logger.Warning("Exception: No Handler registered for " + this.mCtx.Request.Url.AbsolutePath);
						HTTPUtils.WriteErrorJson(this.mCtx.Response, "Request NotFound(404)");
						this.mCtx.Response.StatusCode = 404;
					}
				}
				catch (Exception ex)
				{
					Logger.Error("Exception while processing HTTP handler: " + ex.ToString());
					HTTPUtils.WriteErrorJson(this.mCtx.Response, "Internal Server Error(500)");
					this.mCtx.Response.StatusCode = 500;
				}
				finally
				{
					try
					{
						this.mCtx.Response.OutputStream.Close();
					}
					catch (Exception ex2)
					{
						Logger.Warning("Exception during mCtx.Response.OutputStream.Close(): " + ex2.ToString());
					}
				}
			}

			// Token: 0x060004E8 RID: 1256 RVA: 0x00004D93 File Offset: 0x00002F93
			private static bool IsTokenValid(NameValueCollection headers)
			{
				return headers["x_api_token"] != null && headers["x_api_token"].ToString(CultureInfo.InvariantCulture).Equals(RegistryManager.Instance.ApiToken, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x060004E9 RID: 1257 RVA: 0x0001901C File Offset: 0x0001721C
			public void StaticFileHandler(HttpListenerRequest req, HttpListenerResponse res)
			{
				string text = req.Url.AbsolutePath;
				text = text.Substring(text.Substring(1).IndexOf("/", StringComparison.OrdinalIgnoreCase) + 2);
				string text2 = Path.Combine(this.mRootDir, text.Replace("/", "\\"));
				if (File.Exists(text2))
				{
					byte[] array = File.ReadAllBytes(text2);
					if (text2.EndsWith(".css", StringComparison.OrdinalIgnoreCase))
					{
						res.Headers.Add("Content-Type: text/css");
					}
					else if (text2.EndsWith(".js", StringComparison.OrdinalIgnoreCase))
					{
						res.Headers.Add("Content-Type: application/javascript");
					}
					res.OutputStream.Write(array, 0, array.Length);
					return;
				}
				Logger.Error(string.Format(CultureInfo.InvariantCulture, "File {0} doesn't exist", new object[]
				{
					text2
				}));
				res.StatusCode = 404;
				res.StatusDescription = "Not Found.";
			}

			// Token: 0x060004EA RID: 1258 RVA: 0x00019100 File Offset: 0x00017300
			public void StaticFileChunkHandler(HttpListenerRequest req, HttpListenerResponse res, string dir = "")
			{
				string text = req.Url.AbsolutePath;
				text = text.Substring(text.Substring(1).IndexOf("/", StringComparison.OrdinalIgnoreCase) + 2);
				string text2 = string.IsNullOrEmpty(dir) ? Path.Combine(this.mRootDir, text.Replace("/", "\\")) : Path.Combine(dir, text.Replace("/", "\\"));
				int num = 0;
				while (!File.Exists(text2))
				{
					num++;
					Thread.Sleep(100);
					if (num == 20)
					{
						break;
					}
				}
				num = 0;
				if (!File.Exists(text2))
				{
					Logger.Error(string.Format(CultureInfo.InvariantCulture, "File {0} doesn't exist", new object[]
					{
						text2
					}));
					res.StatusCode = 404;
					res.StatusDescription = "Not Found.";
					return;
				}
				FileInfo fileInfo = new FileInfo(text2);
				long length = fileInfo.Length;
				DateTimeOffset dateTimeOffset = fileInfo.LastWriteTimeUtc;
				DateTimeOffset dateTimeOffset2 = new DateTimeOffset(dateTimeOffset.Year, dateTimeOffset.Month, dateTimeOffset.Day, dateTimeOffset.Hour, dateTimeOffset.Minute, dateTimeOffset.Second, dateTimeOffset.Offset).ToUniversalTime();
				long value = dateTimeOffset2.ToFileTime() ^ length;
				if (string.Equals("\"" + Convert.ToString(value, 16) + "\"", req.Headers.Get("If-None-Match"), StringComparison.InvariantCultureIgnoreCase) && string.Equals(Convert.ToString(dateTimeOffset2, CultureInfo.InvariantCulture), req.Headers.Get("If-Modified-Since"), StringComparison.InvariantCultureIgnoreCase))
				{
					res.StatusCode = 304;
					res.StatusDescription = "Not Modified.";
					return;
				}
				if (text2.EndsWith(".flv", StringComparison.OrdinalIgnoreCase))
				{
					res.Headers.Add("Content-Type: video/x-flv");
				}
				if (text2.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
				{
					res.Headers.Add("Content-Type: image/png");
				}
				res.Headers.Add("Cache-Control: public,max-age=120");
				res.Headers.Add("ETag: \"" + Convert.ToString(value, 16) + "\"");
				res.Headers.Add("Last-Modified: " + Convert.ToString(dateTimeOffset2, CultureInfo.InvariantCulture));
				int num2 = 1048576;
				bool flag = false;
				FileStream fileStream = new FileStream(text2, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
				for (;;)
				{
					byte[] buffer = new byte[num2];
					int num3 = fileStream.Read(buffer, 0, num2);
					if (num3 != 0)
					{
						res.OutputStream.Write(buffer, 0, num3);
						flag = true;
					}
					else
					{
						if (num++ == 50 || flag)
						{
							break;
						}
						Thread.Sleep(100);
					}
				}
				fileStream.Close();
			}

			// Token: 0x04000231 RID: 561
			private Dictionary<string, HTTPServer.RequestHandler> mRoutes;

			// Token: 0x04000232 RID: 562
			private HttpListenerContext mCtx;

			// Token: 0x04000233 RID: 563
			private string mRootDir;
		}
	}
}
