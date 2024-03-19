using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000120 RID: 288
	public static class HTTPUtils
	{
		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600094E RID: 2382 RVA: 0x000089C6 File Offset: 0x00006BC6
		public static string MultiInstanceServerUrl
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
				{
					"http://127.0.0.1",
					RegistryManager.Instance.MultiInstanceServerPort
				});
			}
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x000089F7 File Offset: 0x00006BF7
		public static string PartnerServerUrl(string oem = "bgp")
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
			{
				"http://127.0.0.1",
				RegistryManager.RegistryManagers[oem].PartnerServerPort
			});
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x00008A2E File Offset: 0x00006C2E
		public static string AgentServerUrl(string oem = "bgp")
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
			{
				"http://127.0.0.1",
				RegistryManager.RegistryManagers[oem].AgentServerPort
			});
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x00027A34 File Offset: 0x00025C34
		public static string FrontendServerUrl(string vmName = "Android", string oem = "bgp")
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
			{
				"http://127.0.0.1",
				RegistryManager.RegistryManagers[oem].Guest[vmName].FrontendServerPort
			});
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00027A84 File Offset: 0x00025C84
		public static string GuestServerUrl(string vmName = "Android", string oem = "bgp")
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", new object[]
			{
				"http://127.0.0.1",
				RegistryManager.RegistryManagers[oem].Guest[vmName].BstAndroidPort
			});
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x00027AD4 File Offset: 0x00025CD4
		public static string UrlForBstCommandProcessor(string url)
		{
			try
			{
				Uri uri = new Uri(url);
				foreach (string text in RegistryManager.Instance.VmList)
				{
					if (uri.Segments.Length > 1 && string.Compare("ping", uri.Segments[1], StringComparison.OrdinalIgnoreCase) != 0 && uri.Port == RegistryManager.Instance.Guest[text].BstAndroidPort)
					{
						return text;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error Occured, Err: {0}", new object[]
				{
					ex.ToString()
				});
			}
			return null;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x00027B7C File Offset: 0x00025D7C
		public static NameValueCollection GetRequestHeaderCollection(string vmName)
		{
			NameValueCollection nameValueCollection = new NameValueCollection
			{
				{
					"x_oem",
					RegistryManager.Instance.Oem
				},
				{
					"x_email",
					RegistryManager.Instance.RegisteredEmail
				},
				{
					"x_machine_id",
					GuidUtils.GetBlueStacksMachineId()
				},
				{
					"x_version_machine_id",
					GuidUtils.GetBlueStacksVersionId()
				}
			};
			if (!string.IsNullOrEmpty(vmName))
			{
				if (vmName.Contains("Android"))
				{
					nameValueCollection.Add("vmname", vmName);
					nameValueCollection.Add("x_google_aid", Utils.GetGoogleAdIdfromRegistry(vmName));
					nameValueCollection.Add("x_android_id", Utils.GetAndroidIdfromRegistry(vmName));
					if (string.Equals(vmName, "Android", StringComparison.InvariantCultureIgnoreCase))
					{
						nameValueCollection.Add("vmid", "0");
					}
					else
					{
						nameValueCollection.Add("vmid", vmName.Split(new char[]
						{
							'_'
						})[1]);
					}
				}
				else
				{
					nameValueCollection.Add("vmid", vmName);
					if (vmName.Equals("0", StringComparison.InvariantCultureIgnoreCase))
					{
						nameValueCollection.Add("vmname", "Android");
						nameValueCollection.Add("x_google_aid", Utils.GetGoogleAdIdfromRegistry("Android"));
						nameValueCollection.Add("x_android_id", Utils.GetAndroidIdfromRegistry("Android"));
					}
					else
					{
						nameValueCollection.Add("vmname", "Android_" + vmName);
						nameValueCollection.Add("x_google_aid", Utils.GetGoogleAdIdfromRegistry("Android_" + vmName));
						nameValueCollection.Add("x_android_id", Utils.GetAndroidIdfromRegistry("Android_" + vmName));
					}
				}
			}
			return nameValueCollection;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x00008A65 File Offset: 0x00006C65
		public static RequestData ParseRequest(HttpListenerRequest req)
		{
			return HTTPUtils.ParseRequest(req, true);
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00027D08 File Offset: 0x00025F08
		public static RequestData ParseRequest(HttpListenerRequest req, bool printData)
		{
			RequestData requestData = new RequestData();
			bool flag = false;
			string text = null;
			requestData.Headers = ((req != null) ? req.Headers : null);
			requestData.RequestVmId = 0;
			requestData.RequestVmName = "Android";
			foreach (string text2 in requestData.Headers.AllKeys)
			{
				if (requestData.Headers[text2].Contains("multipart"))
				{
					text = "--" + requestData.Headers[text2].Substring(requestData.Headers[text2].LastIndexOf("=", StringComparison.OrdinalIgnoreCase) + 1);
					Logger.Debug("boundary: {0}", new object[]
					{
						text
					});
					flag = true;
				}
				if (text2.Contains("oem", StringComparison.InvariantCultureIgnoreCase) && requestData.Headers[text2] != null)
				{
					requestData.Oem = requestData.Headers[text2];
				}
				else if (text2 == "vmid" && requestData.Headers[text2] != null)
				{
					if (!requestData.Headers[text2].Equals("0", StringComparison.OrdinalIgnoreCase))
					{
						requestData.RequestVmId = int.Parse(requestData.Headers["vmid"], CultureInfo.InvariantCulture);
						if (requestData.RequestVmName == "Android")
						{
							RequestData requestData2 = requestData;
							requestData2.RequestVmName = requestData2.RequestVmName + "_" + requestData.Headers[text2].ToString(CultureInfo.InvariantCulture);
						}
					}
				}
				else if (text2 == "vmname" && requestData.Headers[text2] != null)
				{
					requestData.RequestVmName = requestData.Headers[text2].ToString(CultureInfo.InvariantCulture);
				}
			}
			requestData.QueryString = req.QueryString;
			if (!req.HasEntityBody)
			{
				return requestData;
			}
			Stream inputStream = req.InputStream;
			byte[] array = new byte[16384];
			MemoryStream memoryStream = new MemoryStream();
			int count;
			while ((count = inputStream.Read(array, 0, array.Length)) > 0)
			{
				memoryStream.Write(array, 0, count);
			}
			byte[] array2 = memoryStream.ToArray();
			memoryStream.Close();
			inputStream.Close();
			Logger.Debug("byte array size {0}", new object[]
			{
				array2.Length
			});
			string @string = Encoding.UTF8.GetString(array2);
			if (!flag)
			{
				if (!req.ContentType.Contains("application/json", StringComparison.InvariantCultureIgnoreCase))
				{
					requestData.Data = HttpUtility.ParseQueryString(@string);
				}
				else
				{
					JObject jobject = JObject.Parse(@string);
					NameValueCollection nameValueCollection = new NameValueCollection();
					foreach (string text3 in (from p in jobject.Properties()
					select p.Name).ToList<string>())
					{
						nameValueCollection.Add(text3, jobject[text3].ToString());
					}
					requestData.Data = nameValueCollection;
				}
				return requestData;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(text);
			List<int> list = HTTPUtils.IndexOf(array2, bytes);
			int j = 0;
			while (j < list.Count - 1)
			{
				Logger.Info("Creating part");
				int num = list[j];
				int num2 = list[j + 1];
				int num3 = num2 - num;
				byte[] array3 = new byte[num3];
				Logger.Debug("Start: {0}, End: {1}, Length: {2}", new object[]
				{
					num,
					num2,
					num3
				});
				Logger.Debug("byteData length: {0}", new object[]
				{
					array2.Length
				});
				Buffer.BlockCopy(array2, num, array3, 0, num3);
				Logger.Debug("bytePart length: {0}", new object[]
				{
					array3.Length
				});
				string string2 = Encoding.UTF8.GetString(array3);
				Match match = new Regex("(?<=Content\\-Type:)(.*?)(?=\\r\\n)").Match(string2);
				Match match2 = new Regex("(?<=filename\\=\\\")(.*?)(?=\\\")").Match(string2);
				string text4 = new Regex("(?<=name\\=\\\")(.*?)(?=\\\")").Match(string2).Value.Trim();
				Logger.Info("Got name: {0}", new object[]
				{
					text4
				});
				if (match.Success && match2.Success)
				{
					Logger.Debug("Found file");
					string text5 = match.Value.Trim();
					Logger.Debug("Got contenttype: {0}", new object[]
					{
						text5
					});
					string text6 = match2.Value.Trim();
					Logger.Info("Got filename: {0}", new object[]
					{
						text6
					});
					int num4 = string2.IndexOf("\r\n\r\n", StringComparison.OrdinalIgnoreCase) + "\r\n\r\n".Length;
					Encoding.UTF8.GetBytes("\r\n" + text);
					int num5 = num3 - num4;
					byte[] array4 = new byte[num5];
					Logger.Debug("startindex: {0}, contentlength: {1}", new object[]
					{
						num4,
						num5
					});
					Buffer.BlockCopy(array3, num4, array4, 0, num5);
					string path = RegistryStrings.BstUserDataDir;
					if (text6.StartsWith("tombstone", StringComparison.OrdinalIgnoreCase))
					{
						path = RegistryStrings.BstLogsDir;
					}
					try
					{
						string text7 = Path.Combine(path, text6);
						FileStream fileStream = File.OpenWrite(text7);
						fileStream.Write(array4, 0, num5);
						fileStream.Close();
						requestData.Files.Add(text4, text7);
						goto IL_5F5;
					}
					catch (Exception ex)
					{
						Logger.Warning("Exception in generating file: " + ex.ToString());
						goto IL_5F5;
					}
					goto IL_58B;
				}
				goto IL_58B;
				IL_5F5:
				j++;
				continue;
				IL_58B:
				Logger.Info("No file in this part");
				int num6 = string2.LastIndexOf("\r\n\r\n", StringComparison.OrdinalIgnoreCase);
				string text8 = string2.Substring(num6, string2.Length - num6);
				text8 = text8.Trim();
				if (printData)
				{
					Logger.Info("Got value: {0}", new object[]
					{
						text8
					});
				}
				else
				{
					Logger.Info("Value hidden");
				}
				requestData.Data.Add(text4, text8);
				goto IL_5F5;
			}
			return requestData;
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00028340 File Offset: 0x00026540
		private static List<int> IndexOf(byte[] searchWithin, byte[] searchFor)
		{
			List<int> list = new List<int>();
			int startIndex = 0;
			int num = Array.IndexOf<byte>(searchWithin, searchFor[0], startIndex);
			Logger.Debug("boundary size = {0}", new object[]
			{
				searchFor.Length
			});
			do
			{
				int num2 = 0;
				while (num + num2 < searchWithin.Length && searchWithin[num + num2] == searchFor[num2])
				{
					num2++;
					if (num2 == searchFor.Length)
					{
						list.Add(num);
						Logger.Debug("Got boundary postion: {0}", new object[]
						{
							num
						});
						break;
					}
				}
				if (num + num2 > searchWithin.Length)
				{
					break;
				}
				num = Array.IndexOf<byte>(searchWithin, searchFor[0], num + num2);
			}
			while (num != -1);
			return list;
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x000283D8 File Offset: 0x000265D8
		public static string MergeQueryParams(string urlOriginal, string urlOverideParams, bool paramsOnly = false)
		{
			NameValueCollection nameValueCollection;
			if (paramsOnly)
			{
				nameValueCollection = HttpUtility.ParseQueryString(urlOverideParams);
			}
			else
			{
				nameValueCollection = HttpUtility.ParseQueryString(new UriBuilder(urlOverideParams).Query);
			}
			UriBuilder uriBuilder = new UriBuilder(urlOriginal);
			NameValueCollection nameValueCollection2 = HttpUtility.ParseQueryString(uriBuilder.Query);
			foreach (object obj in nameValueCollection.Keys)
			{
				nameValueCollection2.Set(obj.ToString(), nameValueCollection[obj.ToString()]);
			}
			uriBuilder.Query = nameValueCollection2.ToString();
			return uriBuilder.Uri.OriginalString;
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00008A6E File Offset: 0x00006C6E
		public static void Write(StringBuilder sb, HttpListenerResponse res)
		{
			HTTPUtils.Write((sb != null) ? sb.ToString() : null, res);
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x0002848C File Offset: 0x0002668C
		public static void Write(string s, HttpListenerResponse res)
		{
			try
			{
				byte[] bytes = Encoding.UTF8.GetBytes(s);
				if (res != null)
				{
					res.ContentLength64 = (long)bytes.Length;
					res.OutputStream.Write(bytes, 0, bytes.Length);
					res.OutputStream.Flush();
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Exception in writing response to http output stream:{0}", new object[]
				{
					ex
				});
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x000284F8 File Offset: 0x000266F8
		public static HTTPServer SetupServer(int startingPort, int maxPort, Dictionary<string, HTTPServer.RequestHandler> routes, string s_RootDir)
		{
			HTTPServer httpserver = null;
			int i;
			for (i = startingPort; i < maxPort; i++)
			{
				try
				{
					httpserver = new HTTPServer(i, routes, s_RootDir);
					httpserver.Start();
					i = httpserver.Port;
					Logger.Info("Server listening on port " + httpserver.Port.ToString());
					if (!string.IsNullOrEmpty(httpserver.RootDir))
					{
						Logger.Info("Serving static content from " + httpserver.RootDir);
					}
					break;
				}
				catch (Exception ex)
				{
					Logger.Warning("Error occured, port: {0} Err: {1}", new object[]
					{
						i,
						ex
					});
				}
			}
			if (i == maxPort || httpserver == null)
			{
				Logger.Fatal("No free port available or server could not be started, exiting.");
				Environment.Exit(2);
			}
			return httpserver;
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x000285B4 File Offset: 0x000267B4
		public static void SendRequestToMultiInstanceAsync(string route, Dictionary<string, string> data = null, string vmName = "Android", int timeout = 0, Dictionary<string, string> headers = null, bool printResponse = false, int retries = 1, int sleepTimeMSec = 0, string oem = "bgp")
		{
			if (retries == 1)
			{
				ThreadPool.QueueUserWorkItem(delegate(object obj)
				{
					try
					{
						HTTPUtils.SendRequestToMultiInstance(route, data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec, oem);
					}
					catch (Exception ex)
					{
						Logger.Error("An exception in SendRequestToClient. route: {0}, \n{1}", new object[]
						{
							route,
							ex
						});
					}
				});
				return;
			}
			new Thread(delegate()
			{
				try
				{
					HTTPUtils.SendRequestToMultiInstance(route, data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec, oem);
				}
				catch (Exception ex)
				{
					Logger.Error("An exception in SendRequestToClient. route: {0}, \n{1}", new object[]
					{
						route,
						ex
					});
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x00028644 File Offset: 0x00026844
		public static void SendRequestToClientAsync(string route, Dictionary<string, string> data = null, string vmName = "Android", int timeout = 0, Dictionary<string, string> headers = null, bool printResponse = false, int retries = 1, int sleepTimeMSec = 0, string oem = "bgp")
		{
			if (retries == 1)
			{
				ThreadPool.QueueUserWorkItem(delegate(object obj)
				{
					try
					{
						HTTPUtils.SendRequestToClient(route, data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec, oem);
					}
					catch (Exception ex)
					{
						Logger.Error("An exception in SendRequestToClient. route: {0}, \n{1}", new object[]
						{
							route,
							ex
						});
					}
				});
				return;
			}
			new Thread(delegate()
			{
				try
				{
					HTTPUtils.SendRequestToClient(route, data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec, oem);
				}
				catch (Exception ex)
				{
					Logger.Error("An exception in SendRequestToClient. route: {0}, \n{1}", new object[]
					{
						route,
						ex
					});
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x000286D4 File Offset: 0x000268D4
		public static void SendRequestToEngineAsync(string route, Dictionary<string, string> data = null, string vmName = "Android", int timeout = 0, Dictionary<string, string> headers = null, bool printResponse = false, int retries = 1, int sleepTimeMSec = 0, string oem = "bgp")
		{
			if (retries == 1)
			{
				ThreadPool.QueueUserWorkItem(delegate(object obj)
				{
					try
					{
						HTTPUtils.SendRequestToEngine(route, data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec, "", oem);
					}
					catch (Exception ex)
					{
						Logger.Error("An exception in SendRequestToEngine. route: {0}, \n{1}", new object[]
						{
							route,
							ex
						});
					}
				});
				return;
			}
			new Thread(delegate()
			{
				try
				{
					HTTPUtils.SendRequestToEngine(route, data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec, "", oem);
				}
				catch (Exception ex)
				{
					Logger.Error("An exception in SendRequestToEngine. route: {0}, \n{1}", new object[]
					{
						route,
						ex
					});
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00028764 File Offset: 0x00026964
		public static void SendRequestToAgentAsync(string route, Dictionary<string, string> data = null, string vmName = "Android", int timeout = 0, Dictionary<string, string> headers = null, bool printResponse = false, int retries = 1, int sleepTimeMSec = 0, string oem = "bgp")
		{
			if (retries == 1)
			{
				ThreadPool.QueueUserWorkItem(delegate(object obj)
				{
					try
					{
						HTTPUtils.SendRequestToAgent(route, data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec, oem, true);
					}
					catch (Exception ex)
					{
						Logger.Error("An exception in SendRequestToAgent. route: {0}, \n{1}", new object[]
						{
							route,
							ex
						});
					}
				});
				return;
			}
			new Thread(delegate()
			{
				try
				{
					HTTPUtils.SendRequestToAgent(route, data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec, oem, true);
				}
				catch (Exception ex)
				{
					Logger.Error("An exception in SendRequestToAgent. route: {0}, \n{1}", new object[]
					{
						route,
						ex
					});
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x000287F4 File Offset: 0x000269F4
		public static void SendRequestToGuestAsync(string route, Dictionary<string, string> data = null, string vmName = "Android", int timeout = 0, Dictionary<string, string> headers = null, bool printResponse = false, int retries = 1, int sleepTimeMSec = 0, string oem = "bgp")
		{
			if (retries == 1)
			{
				ThreadPool.QueueUserWorkItem(delegate(object obj)
				{
					try
					{
						HTTPUtils.SendRequestToGuest(route, data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec, oem);
					}
					catch (Exception ex)
					{
						Logger.Error("An exception in SendRequestToGuest. route: {0}, \n{1}", new object[]
						{
							route,
							ex
						});
					}
				});
				return;
			}
			new Thread(delegate()
			{
				try
				{
					HTTPUtils.SendRequestToGuest(route, data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec, oem);
				}
				catch (Exception ex)
				{
					Logger.Error("An exception in SendRequestToGuest. route: {0}, \n{1}", new object[]
					{
						route,
						ex
					});
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x00028884 File Offset: 0x00026A84
		public static void SendRequestToCloudAsync(string api, Dictionary<string, string> data = null, string vmName = "Android", int timeout = 0, Dictionary<string, string> headers = null, bool printResponse = false, int retries = 1, int sleepTimeMSec = 0)
		{
			if (retries == 1)
			{
				ThreadPool.QueueUserWorkItem(delegate(object obj)
				{
					try
					{
						HTTPUtils.SendRequestToCloud(api, data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec, false);
					}
					catch (Exception ex)
					{
						Logger.Error("An exception in SendRequestToCloud. route: {0}, \n{1}", new object[]
						{
							api,
							ex
						});
					}
				});
				return;
			}
			new Thread(delegate()
			{
				try
				{
					HTTPUtils.SendRequestToCloud(api, data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec, false);
				}
				catch (Exception ex)
				{
					Logger.Error("An exception in SendRequestToCloud. route: {0}, \n{1}", new object[]
					{
						api,
						ex
					});
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0002890C File Offset: 0x00026B0C
		public static void SendRequestToCloudWithParamsAsync(string api, Dictionary<string, string> data = null, string vmName = "Android", int timeout = 0, Dictionary<string, string> headers = null, bool printResponse = false, int retries = 1, int sleepTimeMSec = 0)
		{
			if (retries == 1)
			{
				ThreadPool.QueueUserWorkItem(delegate(object obj)
				{
					try
					{
						HTTPUtils.SendRequestToCloudWithParams(api, data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec);
					}
					catch (Exception ex)
					{
						Logger.Error("An exception in SendRequestToCloudWithParams. route: {0}, \n{1}", new object[]
						{
							api,
							ex
						});
					}
				});
				return;
			}
			new Thread(delegate()
			{
				try
				{
					HTTPUtils.SendRequestToCloudWithParams(api, data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec);
				}
				catch (Exception ex)
				{
					Logger.Error("An exception in SendRequestToCloudWithParams. route: {0}, \n{1}", new object[]
					{
						api,
						ex
					});
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00028994 File Offset: 0x00026B94
		public static string SendRequestToMultiInstance(string route, Dictionary<string, string> data = null, string vmName = "Android", int timeout = 0, Dictionary<string, string> headers = null, bool printResponse = false, int retries = 1, int sleepTimeMSec = 0, string oem = "bgp")
		{
			return HTTPUtils.SendHTTPRequest(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				HTTPUtils.MultiInstanceServerUrl,
				route
			}), data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec, false, oem);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x000289D8 File Offset: 0x00026BD8
		public static string SendRequestToClient(string route, Dictionary<string, string> data = null, string vmName = "Android", int timeout = 0, Dictionary<string, string> headers = null, bool printResponse = false, int retries = 1, int sleepTimeMSec = 0, string oem = "bgp")
		{
			if (oem == null)
			{
				oem = "bgp";
			}
			return HTTPUtils.SendHTTPRequest(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				HTTPUtils.PartnerServerUrl(oem),
				route
			}), data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec, false, oem);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00028A28 File Offset: 0x00026C28
		public static string SendRequestToEngine(string route, Dictionary<string, string> data = null, string vmName = "Android", int timeout = 0, Dictionary<string, string> headers = null, bool printResponse = false, int retries = 1, int sleepTimeMSec = 0, string destinationVmName = "", string oem = "bgp")
		{
			if (string.IsNullOrEmpty(destinationVmName))
			{
				destinationVmName = vmName;
			}
			if (oem == null)
			{
				oem = "bgp";
			}
			return HTTPUtils.SendHTTPRequest(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				HTTPUtils.FrontendServerUrl(destinationVmName, oem),
				route
			}), data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec, false, oem);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00028A84 File Offset: 0x00026C84
		public static string SendRequestToAgent(string route, Dictionary<string, string> data = null, string vmName = "Android", int timeout = 0, Dictionary<string, string> headers = null, bool printResponse = false, int retries = 1, int sleepTimeMSec = 0, string oem = "bgp", bool isCheckForAgentRunning = true)
		{
			if (oem == null)
			{
				oem = "bgp";
			}
			if (isCheckForAgentRunning && !ProcessUtils.IsLockInUse("Global\\BlueStacks_HDAgent_Lock" + oem))
			{
				Process process = new Process();
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.FileName = Path.Combine(RegistryManager.RegistryManagers[oem].InstallDir, "HD-Agent.exe");
				Logger.Info("Utils: Starting Agent");
				process.Start();
				if (!Utils.WaitForAgentPingResponse(vmName, oem))
				{
					return null;
				}
			}
			return HTTPUtils.SendHTTPRequest(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				HTTPUtils.AgentServerUrl(oem),
				route
			}), data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec, false, oem);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00028B48 File Offset: 0x00026D48
		public static string SendRequestToGuest(string route, Dictionary<string, string> data = null, string vmName = "Android", int timeout = 0, Dictionary<string, string> headers = null, bool printResponse = false, int retries = 1, int sleepTimeMSec = 0, string oem = "bgp")
		{
			if (oem == null)
			{
				oem = "bgp";
			}
			return HTTPUtils.SendHTTPRequest(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				HTTPUtils.GuestServerUrl(vmName, oem),
				route
			}), data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec, false, oem);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00028B98 File Offset: 0x00026D98
		public static string SendRequestToCloud(string api, Dictionary<string, string> data = null, string vmName = "Android", int timeout = 0, Dictionary<string, string> headers = null, bool printResponse = false, int retries = 1, int sleepTimeMSec = 0, bool isOnUIThreadOnPurpose = false)
		{
			return HTTPUtils.SendHTTPRequest(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				RegistryManager.Instance.Host,
				api
			}), data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec, isOnUIThreadOnPurpose, "bgp");
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00028BE4 File Offset: 0x00026DE4
		public static string SendRequestToCloudWithParams(string api, Dictionary<string, string> data = null, string vmName = "Android", int timeout = 0, Dictionary<string, string> headers = null, bool printResponse = false, int retries = 1, int sleepTimeMSec = 0)
		{
			return HTTPUtils.SendHTTPRequest(WebHelper.GetUrlWithParams(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				RegistryManager.Instance.Host,
				api
			}), null, null, null), data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec, false, "bgp");
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00028C38 File Offset: 0x00026E38
		public static string SendRequestToNCSoftAgent(int port, string api, Dictionary<string, string> data = null, string vmName = "Android", int timeout = 0, Dictionary<string, string> headers = null, bool printResponse = false, int retries = 1, int sleepTimeMSec = 0)
		{
			return HTTPUtils.SendHTTPRequest(string.Format(CultureInfo.InvariantCulture, "{0}:{1}/{2}", new object[]
			{
				"http://127.0.0.1",
				port,
				api
			}), data, vmName, timeout, headers, printResponse, retries, sleepTimeMSec, false, "bgp");
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00028C88 File Offset: 0x00026E88
		private static string SendHTTPRequest(string url, Dictionary<string, string> data = null, string vmName = "Android", int timeout = 0, Dictionary<string, string> headers = null, bool printResponse = false, int retries = 1, int sleepTimeMSec = 0, bool isOnUIThreadOnPurpose = false, string oem = "bgp")
		{
			string text;
			if (data == null)
			{
				Logger.Info("Sending GET to {0}", new object[]
				{
					url
				});
				text = BstHttpClient.Get(url, headers, false, vmName, timeout, retries, sleepTimeMSec, isOnUIThreadOnPurpose, oem);
			}
			else
			{
				Logger.Info("Sending POST to {0}", new object[]
				{
					url
				});
				text = BstHttpClient.Post(url, data, headers, false, vmName, timeout, retries, sleepTimeMSec, isOnUIThreadOnPurpose, oem);
			}
			if (printResponse)
			{
				Logger.Info("Loopback resp: {0}", new object[]
				{
					text
				});
			}
			else
			{
				Logger.Debug("Loopback resp: {0}", new object[]
				{
					text
				});
			}
			return text;
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00028D1C File Offset: 0x00026F1C
		public static void WriteSuccessArrayJson(HttpListenerResponse res, string reason = "")
		{
			if (string.IsNullOrEmpty(reason))
			{
				HTTPUtils.Write(JSonTemplates.SuccessArrayJSonTemplate, res);
				return;
			}
			JArray jarray = new JArray();
			JObject item = new JObject
			{
				{
					"success",
					true
				},
				{
					"reason",
					reason
				}
			};
			jarray.Add(item);
			HTTPUtils.Write(jarray.ToString(Formatting.None, new JsonConverter[0]), res);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00028D84 File Offset: 0x00026F84
		public static void WriteErrorArrayJson(HttpListenerResponse res, string reason = "")
		{
			if (string.IsNullOrEmpty(reason))
			{
				HTTPUtils.Write(JSonTemplates.FailedArrayJSonTemplate, res);
				return;
			}
			JArray jarray = new JArray();
			JObject item = new JObject
			{
				{
					"success",
					false
				},
				{
					"reason",
					reason
				}
			};
			jarray.Add(item);
			HTTPUtils.Write(jarray.ToString(Formatting.None, new JsonConverter[0]), res);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00028DEC File Offset: 0x00026FEC
		public static void WriteArrayJson(HttpListenerResponse res, Dictionary<string, string> data)
		{
			JArray jarray = new JArray();
			if (data != null)
			{
				JObject jobject = new JObject();
				foreach (KeyValuePair<string, string> keyValuePair in data)
				{
					jobject.Add(keyValuePair.Key, keyValuePair.Value);
				}
				jarray.Add(jobject);
			}
			HTTPUtils.Write(jarray.ToString(Formatting.None, new JsonConverter[0]), res);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00028E78 File Offset: 0x00027078
		public static void WriteArrayJson(HttpListenerResponse res, Dictionary<string, object> data)
		{
			JArray jarray = new JArray();
			if (data != null)
			{
				JObject jobject = new JObject();
				foreach (KeyValuePair<string, object> keyValuePair in data)
				{
					jobject.Add(keyValuePair.Key, JToken.FromObject(keyValuePair.Value));
				}
				jarray.Add(jobject);
			}
			HTTPUtils.Write(jarray.ToString(Formatting.None, new JsonConverter[0]), res);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00028F04 File Offset: 0x00027104
		public static void WriteSuccessJson(HttpListenerResponse res, string reason = "")
		{
			if (string.IsNullOrEmpty(reason))
			{
				HTTPUtils.Write(JSonTemplates.SuccessJSonTemplate, res);
				return;
			}
			HTTPUtils.Write(new JObject
			{
				{
					"success",
					true
				},
				{
					"reason",
					reason
				}
			}.ToString(Formatting.None, new JsonConverter[0]), res);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00028F60 File Offset: 0x00027160
		public static void WriteErrorJson(HttpListenerResponse res, string reason = "")
		{
			if (string.IsNullOrEmpty(reason))
			{
				HTTPUtils.Write(JSonTemplates.FailedJSonTemplate, res);
				return;
			}
			HTTPUtils.Write(new JObject
			{
				{
					"success",
					false
				},
				{
					"reason",
					reason
				}
			}.ToString(Formatting.None, new JsonConverter[0]), res);
		}

		// Token: 0x040004AC RID: 1196
		private const string sLoopbackUrl = "http://127.0.0.1";
	}
}
