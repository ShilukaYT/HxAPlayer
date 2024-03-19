using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;

namespace BlueStacks.Common
{
	// Token: 0x02000058 RID: 88
	public static class HTTP
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00004FE8 File Offset: 0x000031E8
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
					bool flag = i == 1;
					if (flag)
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

		// Token: 0x060000CC RID: 204 RVA: 0x0000506C File Offset: 0x0000326C
		private static string GetInternal(string url, Dictionary<string, string> headers, bool gzip, int timeout, NameValueCollection headerCollection = null, string userAgent = "")
		{
			HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
			httpWebRequest.Method = "GET";
			bool flag = timeout != 0;
			if (flag)
			{
				httpWebRequest.Timeout = timeout;
			}
			if (gzip)
			{
				httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip;
				httpWebRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip");
			}
			bool flag2 = headerCollection != null;
			if (flag2)
			{
				httpWebRequest.Headers.Add(headerCollection);
			}
			bool flag3 = !string.IsNullOrEmpty(userAgent);
			if (flag3)
			{
				httpWebRequest.UserAgent = userAgent;
			}
			bool flag4 = headers != null;
			if (flag4)
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

		// Token: 0x060000CD RID: 205 RVA: 0x0000521C File Offset: 0x0000341C
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
					bool flag = i == 1;
					if (flag)
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

		// Token: 0x060000CE RID: 206 RVA: 0x000052A4 File Offset: 0x000034A4
		private static string PostInternal(string url, Dictionary<string, string> data, Dictionary<string, string> headers, bool gzip, int timeout, NameValueCollection headerCollection, string userAgent)
		{
			HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
			httpWebRequest.Method = "POST";
			bool flag = timeout != 0;
			if (flag)
			{
				httpWebRequest.Timeout = timeout;
			}
			if (gzip)
			{
				httpWebRequest.AutomaticDecompression = DecompressionMethods.GZip;
				httpWebRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip");
			}
			bool flag2 = headerCollection != null;
			if (flag2)
			{
				httpWebRequest.Headers.Add(headerCollection);
			}
			bool flag3 = !string.IsNullOrEmpty(userAgent);
			if (flag3)
			{
				httpWebRequest.UserAgent = userAgent;
			}
			bool flag4 = headers != null;
			if (flag4)
			{
				foreach (KeyValuePair<string, string> keyValuePair in headers)
				{
					httpWebRequest.Headers.Set(StringUtils.GetControlCharFreeString(keyValuePair.Key), StringUtils.GetControlCharFreeString(keyValuePair.Value));
				}
			}
			bool flag5 = data == null;
			if (flag5)
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
