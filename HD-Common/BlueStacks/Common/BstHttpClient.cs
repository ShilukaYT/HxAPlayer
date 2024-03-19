using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x0200011F RID: 287
	public static class BstHttpClient
	{
		// Token: 0x06000945 RID: 2373 RVA: 0x00026A94 File Offset: 0x00024C94
		public static string Get(string url, Dictionary<string, string> headers, bool gzip, string vmName = "Android", int timeout = 0, int retries = 1, int sleepTimeMSec = 0, bool isOnUIThreadOnPurpose = false, string oem = "bgp")
		{
			string @internal;
			try
			{
				if (oem == null)
				{
					oem = "bgp";
				}
				@internal = BstHttpClient.GetInternal(url, headers, gzip, retries, sleepTimeMSec, timeout, vmName, isOnUIThreadOnPurpose, oem);
			}
			catch (Exception ex)
			{
				if (url == null)
				{
					throw new Exception("url cannot be  null");
				}
				if (oem == null)
				{
					oem = "bgp";
				}
				if (url.Contains(RegistryManager.Instance.Host))
				{
					Logger.Error("GET failed: {0}", new object[]
					{
						ex.Message
					});
					url = url.Replace(RegistryManager.Instance.Host, RegistryManager.Instance.Host2);
					@internal = BstHttpClient.GetInternal(url, headers, gzip, retries, sleepTimeMSec, timeout, vmName, isOnUIThreadOnPurpose, oem);
				}
				else
				{
					if (!url.Contains(RegistryManager.Instance.Host2))
					{
						throw;
					}
					Logger.Error("GET failed: {0}", new object[]
					{
						ex.Message
					});
					url = url.Replace(RegistryManager.Instance.Host2, RegistryManager.Instance.Host);
					@internal = BstHttpClient.GetInternal(url, headers, gzip, retries, sleepTimeMSec, timeout, vmName, isOnUIThreadOnPurpose, oem);
				}
			}
			return @internal;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00026BB0 File Offset: 0x00024DB0
		private static string GetInternal(string url, Dictionary<string, string> headers, bool gzip, int retries, int sleepTimeMSec, int timeout, string vmName, bool isOnUIThreadOnPurpose, string oem = "bgp")
		{
			if (Thread.CurrentThread.ManagedThreadId == 1 && !isOnUIThreadOnPurpose)
			{
				StackTrace stackTrace = new StackTrace();
				Logger.Warning("WARNING: This network call is from the UI thread. StackTrace: {0}", new object[]
				{
					stackTrace
				});
			}
			NameValueCollection requestHeaderCollection = HTTPUtils.GetRequestHeaderCollection(vmName);
			Uri uri = new Uri(url);
			if (uri.Host.Contains("localhost") || uri.Host.Contains("127.0.0.1"))
			{
				string str = oem.Equals("bgp", StringComparison.InvariantCultureIgnoreCase) ? "" : ("_" + oem);
				RegistryKey registryKey = RegistryUtils.InitKeyWithSecurityCheck("Software\\BlueStacks" + str);
				requestHeaderCollection.Add("x_api_token", (string)registryKey.GetValue("ApiToken", ""));
			}
			else
			{
				requestHeaderCollection.Remove("x_api_token");
			}
			return HTTP.Get(url, headers, gzip, retries, sleepTimeMSec, timeout, requestHeaderCollection, Utils.GetUserAgent(oem));
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00026C94 File Offset: 0x00024E94
		public static string Post(string url, Dictionary<string, string> data, Dictionary<string, string> headers = null, bool gzip = false, string vmName = "Android", int timeout = 0, int retries = 1, int sleepTimeMSec = 0, bool isOnUIThreadOnPurpose = false, string oem = "bgp")
		{
			string result;
			try
			{
				if (oem == null)
				{
					oem = "bgp";
				}
				if (url != null && Features.IsFeatureEnabled(536870912UL) && url.Contains(RegistryManager.Instance.Host))
				{
					url = url.Replace(RegistryManager.Instance.Host, RegistryManager.Instance.Host2);
				}
				result = BstHttpClient.PostInternal(url, data, headers, gzip, retries, sleepTimeMSec, timeout, vmName, isOnUIThreadOnPurpose, oem);
			}
			catch (Exception ex)
			{
				if (url == null)
				{
					throw new Exception("url cannot be  null");
				}
				if (oem == null)
				{
					oem = "bgp";
				}
				if (url.Contains(RegistryManager.Instance.Host))
				{
					Logger.Error(ex.Message);
					url = url.Replace(RegistryManager.Instance.Host, RegistryManager.Instance.Host2);
					result = BstHttpClient.PostInternal(url, data, headers, gzip, retries, sleepTimeMSec, timeout, vmName, isOnUIThreadOnPurpose, oem);
				}
				else
				{
					if (!url.Contains(RegistryManager.Instance.Host2))
					{
						throw;
					}
					Logger.Error(ex.Message);
					url = url.Replace(RegistryManager.Instance.Host2, RegistryManager.Instance.Host);
					result = BstHttpClient.PostInternal(url, data, headers, gzip, retries, sleepTimeMSec, timeout, vmName, isOnUIThreadOnPurpose, oem);
				}
			}
			return result;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00026DD8 File Offset: 0x00024FD8
		private static string PostInternal(string url, Dictionary<string, string> data, Dictionary<string, string> headers, bool gzip, int retries, int sleepTimeMSecs, int timeout, string vmName, bool isOnUIThreadOnPurpose, string oem = "bgp")
		{
			if (Thread.CurrentThread.ManagedThreadId == 1 && !isOnUIThreadOnPurpose)
			{
				StackTrace stackTrace = new StackTrace();
				Logger.Warning("WARNING: This network call is from UI Thread and its stack trace is {0}", new object[]
				{
					stackTrace.ToString()
				});
			}
			NameValueCollection requestHeaderCollection = HTTPUtils.GetRequestHeaderCollection(vmName);
			if (data == null)
			{
				data = new Dictionary<string, string>();
			}
			Uri uri = new Uri(url);
			if (uri.Host.Contains("localhost") || uri.Host.Contains("127.0.0.1"))
			{
				string str = oem.Equals("bgp", StringComparison.InvariantCultureIgnoreCase) ? "" : ("_" + oem);
				RegistryKey registryKey = RegistryUtils.InitKeyWithSecurityCheck("Software\\BlueStacks" + str);
				requestHeaderCollection.Add("x_api_token", (string)registryKey.GetValue("ApiToken", ""));
			}
			else
			{
				requestHeaderCollection.Remove("x_api_token");
				data = Utils.AddCommonData(data);
			}
			return HTTP.Post(url, data, headers, gzip, retries, sleepTimeMSecs, timeout, requestHeaderCollection, Utils.GetUserAgent(oem));
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00026ED4 File Offset: 0x000250D4
		public static string HTTPGaeFileUploader(string url, Dictionary<string, string> data, Dictionary<string, string> headers, string filepath, string contentType, bool gzip, string vmName)
		{
			if (data == null)
			{
				data = new Dictionary<string, string>();
			}
			string result;
			try
			{
				if (url != null && Features.IsFeatureEnabled(536870912UL) && url.Contains(RegistryManager.Instance.Host))
				{
					url = url.Replace(RegistryManager.Instance.Host, RegistryManager.Instance.Host2);
				}
				result = BstHttpClient.HTTPGaeFileUploaderInternal(url, data, headers, filepath, contentType, gzip, vmName);
			}
			catch (Exception ex)
			{
				if (url == null)
				{
					throw new Exception("url cannot be  null");
				}
				if (url.Contains(RegistryManager.Instance.Host))
				{
					Logger.Error(ex.Message);
					url = url.Replace(RegistryManager.Instance.Host, RegistryManager.Instance.Host2);
					result = BstHttpClient.HTTPGaeFileUploaderInternal(url, data, headers, filepath, contentType, gzip, vmName);
				}
				else
				{
					if (!url.Contains(RegistryManager.Instance.Host2))
					{
						throw;
					}
					Logger.Error(ex.Message);
					url = url.Replace(RegistryManager.Instance.Host2, RegistryManager.Instance.Host);
					result = BstHttpClient.HTTPGaeFileUploaderInternal(url, data, headers, filepath, contentType, gzip, vmName);
				}
			}
			return result;
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00026FFC File Offset: 0x000251FC
		private static string HTTPGaeFileUploaderInternal(string url, Dictionary<string, string> data, Dictionary<string, string> headers, string filepath, string contentType, bool gzip, string vmName)
		{
			if (data == null)
			{
				data = new Dictionary<string, string>();
			}
			if (filepath == null || !File.Exists(filepath))
			{
				return BstHttpClient.Post(url, data, headers, gzip, vmName, 0, 1, 0, false, "bgp");
			}
			JObject jobject = JObject.Parse(BstHttpClient.Get(url, null, false, vmName, 0, 1, 0, false, "bgp"));
			string url2 = null;
			string value = null;
			string value2 = "";
			if (jobject["success"].ToObject<bool>())
			{
				url2 = jobject["url"].ToString();
				try
				{
					value = jobject["country"].ToString();
				}
				catch
				{
					try
					{
						value = new RegionInfo(CultureInfo.CurrentCulture.Name).TwoLetterISORegionName;
					}
					catch
					{
						value = "US";
					}
				}
			}
			data.Add("country", value);
			if (Oem.Instance.IsOEMWithBGPClient)
			{
				value2 = RegistryManager.Instance.ClientVersion;
			}
			data.Add("client_ver", value2);
			return BstHttpClient.HttpUploadFile(url2, filepath, "file", contentType, headers, data);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0002710C File Offset: 0x0002530C
		private static string HttpUploadFile(string url, string file, string paramName, string contentType, Dictionary<string, string> headers, Dictionary<string, string> data)
		{
			string result;
			try
			{
				if (Features.IsFeatureEnabled(536870912UL) && url.Contains(RegistryManager.Instance.Host))
				{
					url = url.Replace(RegistryManager.Instance.Host, RegistryManager.Instance.Host2);
				}
				result = BstHttpClient.HttpUploadFileInternal(url, file, paramName, contentType, headers, data);
			}
			catch (Exception ex)
			{
				if (url.Contains(RegistryManager.Instance.Host))
				{
					Logger.Error(ex.Message);
					url = url.Replace(RegistryManager.Instance.Host, RegistryManager.Instance.Host2);
					result = BstHttpClient.HttpUploadFileInternal(url, file, paramName, contentType, headers, data);
				}
				else
				{
					if (!url.Contains(RegistryManager.Instance.Host2))
					{
						throw;
					}
					Logger.Error(ex.Message);
					url = url.Replace(RegistryManager.Instance.Host2, RegistryManager.Instance.Host);
					result = BstHttpClient.HttpUploadFileInternal(url, file, paramName, contentType, headers, data);
				}
			}
			return result;
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00027210 File Offset: 0x00025410
		private static string HttpUploadFileInternal(string url, string file, string paramName, string contentType, Dictionary<string, string> headers, Dictionary<string, string> data)
		{
			Logger.Info("Uploading {0} to {1}", new object[]
			{
				file,
				url
			});
			string str = "---------------------------" + DateTime.Now.Ticks.ToString("x", CultureInfo.InvariantCulture);
			byte[] bytes = Encoding.ASCII.GetBytes("\r\n--" + str + "\r\n");
			Uri uri = new Uri(url);
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.ContentType = "multipart/form-data; boundary=" + str;
			httpWebRequest.Method = "POST";
			httpWebRequest.KeepAlive = true;
			httpWebRequest.Timeout = 300000;
			httpWebRequest.UserAgent = Utils.GetUserAgent("bgp");
			if (!uri.Host.Contains("localhost") && !uri.Host.Contains("127.0.0.1"))
			{
				string str2 = "URI of proxy = ";
				Uri proxy = httpWebRequest.Proxy.GetProxy(uri);
				Logger.Debug(str2 + ((proxy != null) ? proxy.ToString() : null));
			}
			if (headers != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in headers)
				{
					httpWebRequest.Headers.Set(StringUtils.GetControlCharFreeString(keyValuePair.Key), StringUtils.GetControlCharFreeString(keyValuePair.Value));
				}
			}
			httpWebRequest.Headers.Add(HTTPUtils.GetRequestHeaderCollection(""));
			if (data == null)
			{
				data = new Dictionary<string, string>();
			}
			Stream requestStream = httpWebRequest.GetRequestStream();
			string format = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
			foreach (KeyValuePair<string, string> keyValuePair2 in data)
			{
				requestStream.Write(bytes, 0, bytes.Length);
				string s = string.Format(CultureInfo.InvariantCulture, format, new object[]
				{
					keyValuePair2.Key,
					keyValuePair2.Value
				});
				byte[] bytes2 = Encoding.UTF8.GetBytes(s);
				requestStream.Write(bytes2, 0, bytes2.Length);
			}
			requestStream.Write(bytes, 0, bytes.Length);
			string format2 = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
			string s2 = string.Format(CultureInfo.InvariantCulture, format2, new object[]
			{
				paramName,
				file,
				contentType
			});
			byte[] bytes3 = Encoding.UTF8.GetBytes(s2);
			requestStream.Write(bytes3, 0, bytes3.Length);
			string text = Environment.ExpandEnvironmentVariables("%TEMP%");
			text = Path.Combine(text, Path.GetFileName(file)) + "_bst";
			File.Copy(file, text);
			if (contentType.Equals("text/plain", StringComparison.InvariantCultureIgnoreCase))
			{
				int num = 1048576;
				string s3 = File.ReadAllText(text);
				byte[] array = new byte[num];
				array = Encoding.UTF8.GetBytes(s3);
				requestStream.Write(array, 0, array.Length);
			}
			else
			{
				FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Read);
				byte[] array2 = new byte[4096];
				int count;
				while ((count = fileStream.Read(array2, 0, array2.Length)) != 0)
				{
					requestStream.Write(array2, 0, count);
				}
				fileStream.Close();
			}
			File.Delete(text);
			byte[] bytes4 = Encoding.ASCII.GetBytes("\r\n--" + str + "--\r\n");
			requestStream.Write(bytes4, 0, bytes4.Length);
			requestStream.Close();
			string text2 = null;
			WebResponse webResponse = null;
			try
			{
				webResponse = httpWebRequest.GetResponse();
				using (Stream responseStream = webResponse.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(responseStream))
					{
						text2 = streamReader.ReadToEnd();
						Logger.Info("File uploaded, server response is: {0}", new object[]
						{
							text2
						});
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error uploading file", new object[]
				{
					ex
				});
				if (webResponse != null)
				{
					webResponse.Close();
					webResponse = null;
				}
				throw;
			}
			finally
			{
				httpWebRequest = null;
			}
			return text2;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x00027624 File Offset: 0x00025824
		public static string PostMultipart(string url, Dictionary<string, object> parameters, out byte[] dataArray)
		{
			string str = "---------------------------" + DateTime.Now.Ticks.ToString("x", CultureInfo.InvariantCulture);
			byte[] bytes = Encoding.ASCII.GetBytes("\r\n--" + str + "\r\n");
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.ContentType = "multipart/form-data; boundary=" + str;
			httpWebRequest.Method = "POST";
			httpWebRequest.KeepAlive = true;
			httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
			if (parameters != null && parameters.Count > 0)
			{
				using (Stream requestStream = httpWebRequest.GetRequestStream())
				{
					foreach (KeyValuePair<string, object> keyValuePair in parameters)
					{
						requestStream.Write(bytes, 0, bytes.Length);
						if (keyValuePair.Value is FormFile)
						{
							FormFile formFile = keyValuePair.Value as FormFile;
							string s = string.Concat(new string[]
							{
								"Content-Disposition: form-data; name=\"",
								keyValuePair.Key,
								"\"; filename=\"",
								formFile.Name,
								"\"\r\nContent-Type: ",
								formFile.ContentType,
								"\r\n\r\n"
							});
							byte[] bytes2 = Encoding.UTF8.GetBytes(s);
							requestStream.Write(bytes2, 0, bytes2.Length);
							byte[] array = new byte[32768];
							int count;
							if (formFile.Stream == null)
							{
								using (FileStream fileStream = File.OpenRead(formFile.FilePath))
								{
									while ((count = fileStream.Read(array, 0, array.Length)) != 0)
									{
										requestStream.Write(array, 0, count);
									}
									fileStream.Close();
									continue;
								}
								goto IL_19C;
							}
							IL_1A8:
							if ((count = formFile.Stream.Read(array, 0, array.Length)) == 0)
							{
								continue;
							}
							IL_19C:
							requestStream.Write(array, 0, count);
							goto IL_1A8;
						}
						string str2 = "Content-Disposition: form-data; name=\"";
						string key = keyValuePair.Key;
						string str3 = "\"\r\n\r\n";
						object value = keyValuePair.Value;
						string s2 = str2 + key + str3 + ((value != null) ? value.ToString() : null);
						byte[] bytes3 = Encoding.UTF8.GetBytes(s2);
						requestStream.Write(bytes3, 0, bytes3.Length);
					}
					byte[] bytes4 = Encoding.ASCII.GetBytes("\r\n--" + str + "--\r\n");
					requestStream.Write(bytes4, 0, bytes4.Length);
					requestStream.Close();
				}
			}
			HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest;
			string result = ((HttpWebResponse)httpWebRequest.GetResponse()).ToString();
			int num;
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
			{
				httpStatusCode = httpWebResponse.StatusCode;
				if (int.TryParse(httpWebResponse.Headers.Get("Content-Length"), out num))
				{
					Logger.Info("content lenght.." + num.ToString());
				}
				using (BinaryReader binaryReader = new BinaryReader(httpWebResponse.GetResponseStream()))
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						byte[] array2 = binaryReader.ReadBytes(16384);
						while (array2.Length != 0)
						{
							memoryStream.Write(array2, 0, array2.Length);
							array2 = binaryReader.ReadBytes(16384);
						}
						dataArray = new byte[(int)memoryStream.Length];
						memoryStream.Position = 0L;
						memoryStream.Read(dataArray, 0, dataArray.Length);
					}
				}
			}
			if (httpStatusCode != HttpStatusCode.OK || num < 2000)
			{
				result = "error";
			}
			return result;
		}
	}
}
