using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000070 RID: 112
	public static class InstalledOem
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000356E File Offset: 0x0000176E
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000357A File Offset: 0x0000177A
		public static List<string> AllInstalledOemList
		{
			get
			{
				InstalledOem.SetAllInstalledOems();
				return InstalledOem.mAllInstalledOemList;
			}
			private set
			{
				if (InstalledOem.mAllInstalledOemList != value)
				{
					InstalledOem.mAllInstalledOemList = value;
				}
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000135A8 File Offset: 0x000117A8
		public static void SetAllInstalledOems()
		{
			List<string> list = new List<string>
			{
				"bgp"
			};
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software");
				foreach (string text in registryKey.GetSubKeyNames())
				{
					if (text.StartsWith("BlueStacks", StringComparison.OrdinalIgnoreCase) && !text.StartsWith("BlueStacksGP", StringComparison.OrdinalIgnoreCase) && !text.StartsWith("BlueStacksInstaller", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty((string)Utils.GetRegistryHKLMValue("Software\\" + text, "Version", "")))
					{
						string item = (string)Utils.GetRegistryHKLMValue("Software\\" + text + "\\Config", "Oem", "bgp");
						list.AddIfNotContain(item);
					}
				}
				registryKey.Close();
				RegistryManager.SetRegistryManagers(list);
				InstalledOem.AllInstalledOemList = list;
			}
			catch (Exception ex)
			{
				Logger.Info("Error in finding installed oems " + ex.ToString());
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000290 RID: 656 RVA: 0x0000358A File Offset: 0x0000178A
		// (set) Token: 0x06000291 RID: 657 RVA: 0x00003596 File Offset: 0x00001796
		public static List<string> InstalledCoexistingOemList
		{
			get
			{
				InstalledOem.SetInstalledCoexistingOems();
				return InstalledOem.mInstalledCoexistingOemList;
			}
			private set
			{
				if (InstalledOem.mInstalledCoexistingOemList != value)
				{
					InstalledOem.mInstalledCoexistingOemList = value;
					RegistryManager.SetRegistryManagers(InstalledOem.mInstalledCoexistingOemList);
				}
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000136B8 File Offset: 0x000118B8
		public static void SetInstalledCoexistingOems()
		{
			List<string> list = new List<string>
			{
				"bgp"
			};
			if (Oem.Instance.IsShowMimOtherOEM)
			{
				foreach (AppPlayerModel appPlayerModel in InstalledOem.CoexistingOemList.ToList<AppPlayerModel>())
				{
					string str = appPlayerModel.AppPlayerOem.Equals("bgp", StringComparison.InvariantCultureIgnoreCase) ? "" : ("_" + appPlayerModel.AppPlayerOem);
					RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\BlueStacks" + str + "\\Config");
					if (registryKey != null && !string.IsNullOrEmpty((string)Utils.GetRegistryHKLMValue("Software\\BlueStacks" + str, "Version", "")))
					{
						registryKey.Close();
						list.AddIfNotContain(appPlayerModel.AppPlayerOem);
					}
				}
			}
			RegistryManager.SetRegistryManagers(list);
			InstalledOem.InstalledCoexistingOemList = list;
			Logger.Info("InstalledCoexistingOemList: " + string.Join(",", list.ToArray()));
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000293 RID: 659 RVA: 0x000137E0 File Offset: 0x000119E0
		// (set) Token: 0x06000294 RID: 660 RVA: 0x000035B0 File Offset: 0x000017B0
		public static ObservableCollection<AppPlayerModel> CoexistingOemList
		{
			get
			{
				if (InstalledOem.mCoexistingOemList == null || InstalledOem.mCoexistingOemList.Count < 0)
				{
					InstalledOem.mCoexistingOemList = JsonConvert.DeserializeObject<ObservableCollection<AppPlayerModel>>(RegistryManager.Instance.AppPlayerEngineInfo, Utils.GetSerializerSettings());
					if (!(from x in InstalledOem.mCoexistingOemList
					where string.Equals(x.AppPlayerOem, "bgp", StringComparison.InvariantCultureIgnoreCase)
					select x).Any<AppPlayerModel>())
					{
						foreach (AppPlayerModel appPlayerModel in JsonConvert.DeserializeObject<ObservableCollection<AppPlayerModel>>(Constants.DefaultAppPlayerEngineInfo, Utils.GetSerializerSettings()))
						{
							if (string.Equals(appPlayerModel.AppPlayerOem, "bgp", StringComparison.InvariantCultureIgnoreCase))
							{
								InstalledOem.mCoexistingOemList.Add(appPlayerModel);
							}
						}
					}
				}
				if (!InstalledOem.CloudResponseRecieved)
				{
					InstalledOem.GetCoexistingOemsFromCloud();
				}
				return InstalledOem.mCoexistingOemList;
			}
			private set
			{
				if (InstalledOem.mCoexistingOemList != value)
				{
					InstalledOem.mCoexistingOemList = value;
				}
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000295 RID: 661 RVA: 0x000138C0 File Offset: 0x00011AC0
		private static BackgroundWorker BGGetOem
		{
			get
			{
				if (InstalledOem.mBgwGetOem == null)
				{
					InstalledOem.mBgwGetOem = new BackgroundWorker();
					InstalledOem.mBgwGetOem.DoWork += InstalledOem.BgGetOem_DoWork;
					InstalledOem.mBgwGetOem.RunWorkerCompleted += InstalledOem.BgGetOem_RunWorkerCompleted;
				}
				return InstalledOem.mBgwGetOem;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000296 RID: 662 RVA: 0x000035C0 File Offset: 0x000017C0
		// (set) Token: 0x06000297 RID: 663 RVA: 0x000035C7 File Offset: 0x000017C7
		public static bool CloudResponseRecieved { get; private set; } = false;

		// Token: 0x06000298 RID: 664 RVA: 0x000035CF File Offset: 0x000017CF
		public static void GetCoexistingOemsFromCloud()
		{
			InstalledOem.CloudResponseRecieved = false;
			if (InstalledOem.BGGetOem.IsBusy)
			{
				return;
			}
			InstalledOem.BGGetOem.RunWorkerAsync();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00013910 File Offset: 0x00011B10
		private static void BgGetOem_DoWork(object sender, DoWorkEventArgs e)
		{
			JToken result = null;
			try
			{
				result = JToken.Parse(BstHttpClient.Get(InstalledOem.CreateRequestUrlAndDownloadJsonData(), null, false, "Android", 0, 1, 0, false, "bgp"));
			}
			catch (Exception ex)
			{
				Logger.Error("Failed to get oem err: {0}", new object[]
				{
					ex.Message
				});
			}
			finally
			{
				e.Result = result;
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00013984 File Offset: 0x00011B84
		private static void BgGetOem_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			try
			{
				JToken jtoken = e.Result as JToken;
				if (jtoken != null)
				{
					InstalledOem.ResetCoexistingOems(jtoken);
				}
				InstalledOem.CloudResponseRecieved = true;
				string str = "Oem List data Url: ";
				JToken jtoken2 = jtoken;
				Logger.Debug(str + ((jtoken2 != null) ? jtoken2.ToString() : null));
			}
			catch (Exception ex)
			{
				Logger.Error("Failed to get oem err: {0}", new object[]
				{
					ex.Message
				});
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000139F8 File Offset: 0x00011BF8
		private static string CreateRequestUrlAndDownloadJsonData()
		{
			string text = RegistryManager.Instance.Host + "/bs4/getmultiinstancebuild?";
			try
			{
				string empty = string.Empty;
				string str = "app_player";
				string str2 = "w" + SystemUtils.GetOSArchitecture().ToString();
				string userSelectedLocale = RegistryManager.Instance.UserSelectedLocale;
				string text2;
				string text3;
				SystemUtils.GetOSInfo(out empty, out text2, out text3);
				string text4 = "app_player_win_version=";
				text4 += empty;
				text4 += "&source=";
				text4 += str;
				text4 += "&app_player_os_arch=";
				text4 += str2;
				text4 += "&app_player_language=";
				text4 += userSelectedLocale;
				text = HTTPUtils.MergeQueryParams(text, text4, true);
			}
			catch (Exception ex)
			{
				Logger.Error("Failed to create url err: {0}", new object[]
				{
					ex.Message
				});
			}
			return text;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00013AEC File Offset: 0x00011CEC
		private static void ResetCoexistingOems(JToken jTokenResponse)
		{
			try
			{
				object obj = InstalledOem.listLock;
				lock (obj)
				{
					ObservableCollection<AppPlayerModel> observableCollection = new ObservableCollection<AppPlayerModel>();
					JEnumerable<JToken> jenumerable = jTokenResponse.First<JToken>().Children();
					if (jenumerable.Children<JToken>().Any<JToken>())
					{
						foreach (JToken jtoken in jenumerable.Children<JToken>())
						{
							AppPlayerModel item = JsonConvert.DeserializeObject<AppPlayerModel>(jtoken.ToString(), Utils.GetSerializerSettings());
							observableCollection.Add(item);
						}
					}
					if (!(from x in observableCollection
					where string.Equals(x.AppPlayerOem, "bgp", StringComparison.InvariantCultureIgnoreCase)
					select x).Any<AppPlayerModel>())
					{
						foreach (AppPlayerModel appPlayerModel in JsonConvert.DeserializeObject<ObservableCollection<AppPlayerModel>>(Constants.DefaultAppPlayerEngineInfo, Utils.GetSerializerSettings()))
						{
							if (string.Equals(appPlayerModel.AppPlayerOem, "bgp", StringComparison.InvariantCultureIgnoreCase))
							{
								observableCollection.Add(appPlayerModel);
							}
						}
					}
					string text = JsonConvert.SerializeObject(observableCollection, Utils.GetSerializerSettings());
					if (!string.Equals(RegistryManager.Instance.AppPlayerEngineInfo, text, StringComparison.InvariantCultureIgnoreCase))
					{
						RegistryManager.Instance.AppPlayerEngineInfo = text;
					}
					InstalledOem.CoexistingOemList = observableCollection;
				}
			}
			catch (Exception ex)
			{
				string str = "Exception in parsing cloud response:";
				Exception ex2 = ex;
				Logger.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00013CB4 File Offset: 0x00011EB4
		public static bool CheckIfOemInstancePresent(string oem, string abi)
		{
			if (!string.IsNullOrEmpty(oem) && InstalledOem.InstalledCoexistingOemList.Contains(oem))
			{
				if (!oem.Contains("bgp64"))
				{
					return true;
				}
				int value;
				if (!int.TryParse(abi, out value))
				{
					value = int.Parse(ABISetting.ARM64.GetDescription(), CultureInfo.InvariantCulture);
				}
				abi = (InstalledOem.BGP6432BitABIValues.Contains(value) ? ABISetting.Auto64.GetDescription() : ABISetting.ARM64.GetDescription());
				foreach (string vmName in RegistryManager.RegistryManagers[oem].VmList)
				{
					if (string.Equals(abi, Utils.GetValueInBootParams("abivalue", vmName, string.Empty, oem), StringComparison.InvariantCultureIgnoreCase))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00013D78 File Offset: 0x00011F78
		public static void LaunchOemInstance(string oem, string abi, string vmname = "", string packageName = "", string actionWithRemainingInstances = "")
		{
			if (InstalledOem.CheckIfOemInstancePresent(oem, abi))
			{
				string partnerExePath = RegistryManager.RegistryManagers[oem].PartnerExePath;
				if (string.IsNullOrEmpty(vmname) || !RegistryManager.RegistryManagers[oem].VmList.Contains(vmname))
				{
					vmname = "Android";
					if (oem.Contains("bgp64"))
					{
						int value;
						if (!int.TryParse(abi, out value))
						{
							value = int.Parse(ABISetting.ARM64.GetDescription(), CultureInfo.InvariantCulture);
						}
						abi = (InstalledOem.BGP6432BitABIValues.Contains(value) ? ABISetting.Auto64.GetDescription() : ABISetting.ARM64.GetDescription());
						foreach (string text in RegistryManager.RegistryManagers[oem].VmList)
						{
							if (string.Equals(abi, Utils.GetValueInBootParams("abivalue", text, string.Empty, oem), StringComparison.InvariantCultureIgnoreCase))
							{
								vmname = text;
								break;
							}
						}
					}
				}
				string text2 = "-vmname " + vmname;
				if (!string.IsNullOrEmpty(packageName))
				{
					JObject jobject;
					if (new Version(RegistryManager.RegistryManagers[oem].Version) < new Version("4.210.0.0000"))
					{
						jobject = new JObject
						{
							{
								"app_pkg",
								packageName
							}
						};
					}
					else
					{
						jobject = new JObject
						{
							{
								"fle_pkg",
								packageName
							},
							{
								"source",
								"mim"
							}
						};
					}
					if (jobject != null)
					{
						text2 = text2 + " -json " + Uri.EscapeUriString(jobject.ToString(Formatting.None, new JsonConverter[0]));
					}
				}
				Process.Start(new ProcessStartInfo
				{
					Arguments = text2,
					UseShellExecute = false,
					CreateNoWindow = true,
					FileName = partnerExePath
				});
				if (string.Equals(actionWithRemainingInstances, "close", StringComparison.InvariantCultureIgnoreCase))
				{
					InstalledOem.ActionOnRemainingInstances("stopInstance", oem, vmname);
					return;
				}
				if (string.Equals(actionWithRemainingInstances, "minimize", StringComparison.InvariantCultureIgnoreCase))
				{
					InstalledOem.ActionOnRemainingInstances("minimizeInstance", oem, vmname);
				}
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00013F74 File Offset: 0x00012174
		private static void ActionOnRemainingInstances(string route, string launchedOem, string launchedVmName)
		{
			foreach (string text in InstalledOem.InstalledCoexistingOemList)
			{
				if (ProcessUtils.IsAlreadyRunning("Global\\BlueStacks_BlueStacksUI_Lock" + text))
				{
					foreach (string text2 in RegistryManager.RegistryManagers[text].VmList)
					{
						try
						{
							if (!string.Equals(text, launchedOem, StringComparison.InvariantCultureIgnoreCase) || !string.Equals(text2, launchedVmName, StringComparison.InvariantCultureIgnoreCase))
							{
								if (Utils.PingPartner(text, text2))
								{
									Logger.Info(string.Concat(new string[]
									{
										"Sending ",
										route,
										" call to oem:",
										text,
										" vm:",
										text2
									}));
									HTTPUtils.SendRequestToClientAsync(route, null, text2, 0, null, false, 1, 0, text);
								}
							}
						}
						catch (Exception ex)
						{
							Logger.Info(string.Format("Error Sending {0} call to oem: {1} vm: {2} with exception: {3}", new object[]
							{
								route,
								text,
								text2,
								ex
							}));
						}
					}
				}
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000140A4 File Offset: 0x000122A4
		public static AppPlayerModel GetAppPlayerModel(string oem, string abi)
		{
			if (string.IsNullOrEmpty(oem))
			{
				oem = "bgp";
			}
			if (oem.Contains("bgp64") && !oem.Contains("bgp64_hyperv") && !oem.Contains("msi64_hyperv"))
			{
				int value;
				if (!int.TryParse(abi, out value))
				{
					value = int.Parse(ABISetting.ARM64.GetDescription(), CultureInfo.InvariantCulture);
				}
				abi = (InstalledOem.BGP6432BitABIValues.Contains(value) ? ABISetting.Auto64.GetDescription() : ABISetting.ARM64.GetDescription());
				return InstalledOem.CoexistingOemList.FirstOrDefault((AppPlayerModel x) => x != null && x.AppPlayerOem == oem && string.Equals(x.AbiValue.ToString(CultureInfo.InvariantCulture), abi, StringComparison.InvariantCultureIgnoreCase));
			}
			return InstalledOem.CoexistingOemList.FirstOrDefault((AppPlayerModel x) => x != null && x.AppPlayerOem == oem);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00014194 File Offset: 0x00012394
		public static string GetOemFromVmnameWithSuffix(string vmNameWithSuffix)
		{
			string result = "bgp";
			foreach (AppPlayerModel appPlayerModel in InstalledOem.CoexistingOemList)
			{
				string appPlayerOem = appPlayerModel.AppPlayerOem;
				if (vmNameWithSuffix.EndsWith(appPlayerOem, StringComparison.InvariantCultureIgnoreCase))
				{
					result = appPlayerOem;
					break;
				}
			}
			return result;
		}

		// Token: 0x0400012F RID: 303
		private static readonly object listLock = new object();

		// Token: 0x04000130 RID: 304
		public static readonly int[] BGP6432BitABIValues = new int[]
		{
			1,
			2,
			3,
			4,
			5,
			6,
			7
		};

		// Token: 0x04000131 RID: 305
		private static List<string> mAllInstalledOemList;

		// Token: 0x04000132 RID: 306
		private static List<string> mInstalledCoexistingOemList;

		// Token: 0x04000133 RID: 307
		private static ObservableCollection<AppPlayerModel> mCoexistingOemList;

		// Token: 0x04000134 RID: 308
		private static BackgroundWorker mBgwGetOem = null;
	}
}
