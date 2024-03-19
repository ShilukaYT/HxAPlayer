using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace BlueStacks.Common
{
	// Token: 0x020001EA RID: 490
	public static class HTTP
	{
		// Token: 0x06000FE3 RID: 4067 RVA: 0x0003CE3C File Offset: 0x0003B03C
		public static string Get(string url, Dictionary<string, string> headers, bool gzip, int tries, int sleepTimeMSec, int timeout, NameValueCollection headerCollection = null, string userAgent = "")
		{
			string result = null;
			int i = tries;
			while (i > 0)
			{
				try
				{
					result = HTTP.GetInternal(url, headers, gzip, timeout, headerCollection, userAgent);
					break;
				}
				catch (Exception ex)
				{
					if (i == 1)
					{
						throw;
					}
					Logger.Warning("Exception when GET to url: {0}, Ex: {1}", new object[]
					{
						url,
						ex.Message
					});
				}
				i--;
				Thread.Sleep(sleepTimeMSec);
			}
			return result;
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x0003CEA8 File Offset: 0x0003B0A8
		private static string GetInternal(string url, Dictionary<string, string> headers, bool gzip, int timeout, NameValueCollection headerCollection = null, string userAgent = "")
		{
			HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
			httpWebRequest.Method = "GET";
			if (timeout != 0)
			{
				httpWebRequest.Timeout = timeout;
			}
			if (gzip)
			{
				httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip;
				httpWebRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip");
			}
			if (headerCollection != null)
			{
				httpWebRequest.Headers.Add(headerCollection);
			}
			if (!string.IsNullOrEmpty(userAgent))
			{
				httpWebRequest.UserAgent = userAgent;
			}
			if (headers != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in headers)
				{
					httpWebRequest.Headers.Set(StringUtils.GetControlCharFreeString(keyValuePair.Key), StringUtils.GetControlCharFreeString(keyValuePair.Value));
				}
			}
			string result = null;
			using (HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse)
			{
				Logger.Debug("Response StatusCode:" + httpWebResponse.StatusCode.ToString());
				using (Stream responseStream = httpWebResponse.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8))
					{
						result = streamReader.ReadToEnd();
					}
				}
			}
			return result;
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0003D014 File Offset: 0x0003B214
		public static string Post(string url, Dictionary<string, string> data, Dictionary<string, string> headers, bool gzip, int retries, int sleepTimeMSecs, int timeout, NameValueCollection headerCollection, string userAgent)
		{
			string result = null;
			int i = retries;
			while (i > 0)
			{
				try
				{
					result = HTTP.PostInternal(url, data, headers, gzip, timeout, headerCollection, userAgent);
					break;
				}
				catch (Exception ex)
				{
					if (i == 1)
					{
						throw;
					}
					Logger.Warning("Exception when posting to url: {0}, Ex: {1}", new object[]
					{
						url,
						ex.Message
					});
				}
				i--;
				Thread.Sleep(sleepTimeMSecs);
			}
			return result;
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0003D084 File Offset: 0x0003B284
		private static string PostInternal(string url, Dictionary<string, string> data, Dictionary<string, string> headers, bool gzip, int timeout, NameValueCollection headerCollection, string userAgent)
		{
			HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
			httpWebRequest.Method = "POST";
			if (timeout != 0)
			{
				httpWebRequest.Timeout = timeout;
			}
			if (gzip)
			{
				httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip;
				httpWebRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip");
			}
			if (headerCollection != null)
			{
				httpWebRequest.Headers.Add(headerCollection);
			}
			if (!string.IsNullOrEmpty(userAgent))
			{
				httpWebRequest.UserAgent = userAgent;
			}
			if (headers != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in headers)
				{
					httpWebRequest.Headers.Set(StringUtils.GetControlCharFreeString(keyValuePair.Key), StringUtils.GetControlCharFreeString(keyValuePair.Value));
				}
			}
			if (data == null)
			{
				data = new Dictionary<string, string>();
			}
			byte[] bytes = Encoding.UTF8.GetBytes(StringUtils.Encode(data));
			httpWebRequest.ContentType = "application/x-www-form-urlencoded";
			httpWebRequest.ContentLength = (long)bytes.Length;
			string result = null;
			using (Stream requestStream = httpWebRequest.GetRequestStream())
			{
				requestStream.Write(bytes, 0, bytes.Length);
				using (HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse)
				{
					Logger.Debug("Response StatusCode:" + httpWebResponse.StatusCode.ToString());
					using (Stream responseStream = httpWebResponse.GetResponseStream())
					{
						using (StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8))
						{
							result = streamReader.ReadToEnd();
						}
					}
				}
			}
			return result;
		}
	}
}
