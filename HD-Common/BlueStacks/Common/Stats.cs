using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000140 RID: 320
	public static class Stats
	{
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x0000C17D File Offset: 0x0000A37D
		// (set) Token: 0x06000C53 RID: 3155 RVA: 0x0000C191 File Offset: 0x0000A391
		private static string SessionId
		{
			get
			{
				if (Stats.sSessionId == null)
				{
					Stats.ResetSessionId();
				}
				return Stats.sSessionId;
			}
			set
			{
				Stats.sSessionId = value;
			}
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0000C199 File Offset: 0x0000A399
		public static string GetSessionId()
		{
			return Stats.SessionId;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0000C1A0 File Offset: 0x0000A3A0
		public static string ResetSessionId()
		{
			Stats.SessionId = Stats.Timestamp;
			return Stats.SessionId;
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0000C1B1 File Offset: 0x0000A3B1
		public static void SendAppStats(string appName, string packageName, string appVersion, string homeVersion, Stats.AppType appType, string vmName, string appVersionName = "")
		{
			Stats.SendAppStats(appName, packageName, appVersion, homeVersion, appType, null, vmName, appVersionName);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0002BFA0 File Offset: 0x0002A1A0
		public static void SendAppStats(string appName, string packageName, string appVersion, string homeVersion, Stats.AppType appType, string source, string vmName, string appVersionName)
		{
			new Thread(delegate()
			{
				try
				{
					string url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
					{
						RegistryManager.Instance.Host,
						"stats/appclickstats"
					});
					Dictionary<string, string> dictionary = new Dictionary<string, string>
					{
						{
							"email",
							Stats.GetURLSafeBase64String(RegistryManager.Instance.RegisteredEmail)
						},
						{
							"app_name",
							Stats.GetURLSafeBase64String(appName)
						},
						{
							"app_pkg",
							Stats.GetURLSafeBase64String(packageName)
						},
						{
							"app_ver",
							Stats.GetURLSafeBase64String(appVersion)
						},
						{
							"home_app_ver",
							Stats.GetURLSafeBase64String(homeVersion)
						},
						{
							"user_time",
							Stats.GetURLSafeBase64String(Stats.Timestamp)
						},
						{
							"app_type",
							Stats.GetURLSafeBase64String(appType.ToString())
						},
						{
							"app_ver_name",
							Stats.GetURLSafeBase64String(appVersionName)
						}
					};
					if (source != null)
					{
						dictionary.Add("source", Stats.GetURLSafeBase64String(source));
					}
					if (!string.IsNullOrEmpty(RegistryManager.Instance.ClientLaunchParams))
					{
						JObject jobject = JObject.Parse(RegistryManager.Instance.ClientLaunchParams);
						if (jobject["campaign_id"] != null)
						{
							dictionary.Add("externalsource_campaignid", Stats.GetURLSafeBase64String(jobject["campaign_id"].ToString()));
						}
						if (jobject["source_version"] != null)
						{
							dictionary.Add("externalsource_version", Stats.GetURLSafeBase64String(jobject["source_version"].ToString()));
						}
					}
					Logger.Info("Sending App Stats for: {0}", new object[]
					{
						appName
					});
					BstHttpClient.Post(url, dictionary, null, false, vmName, 0, 1, 0, false, "bgp");
				}
				catch (Exception ex)
				{
					Logger.Error("Failed to send app stats. error: " + ex.ToString());
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0002C00C File Offset: 0x0002A20C
		public static void SendWebAppChannelStats(string appName, string packageName, string homeVersion, string source, string vmName)
		{
			new Thread(delegate()
			{
				string url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
				{
					RegistryManager.Instance.Host,
					"stats/webappchannelclickstats"
				});
				Dictionary<string, string> data = new Dictionary<string, string>
				{
					{
						"app_name",
						Stats.GetURLSafeBase64String(appName)
					},
					{
						"app_pkg",
						Stats.GetURLSafeBase64String(packageName)
					},
					{
						"home_app_ver",
						Stats.GetURLSafeBase64String(homeVersion)
					},
					{
						"user_time",
						Stats.GetURLSafeBase64String(Stats.Timestamp)
					},
					{
						"email",
						Stats.GetURLSafeBase64String(RegistryManager.Instance.RegisteredEmail)
					},
					{
						"source",
						Stats.GetURLSafeBase64String(source)
					}
				};
				try
				{
					Logger.Info("Sending Channel App Stats for: {0}", new object[]
					{
						appName
					});
					string text = BstHttpClient.Post(url, data, null, false, vmName, 0, 1, 0, false, "bgp");
					Logger.Info("Got Channel App Stat response: {0}", new object[]
					{
						text
					});
				}
				catch (Exception ex)
				{
					Logger.Error(ex.ToString());
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0000C1C3 File Offset: 0x0000A3C3
		public static void SendSearchAppStats(string keyword, string vmName)
		{
			Stats.SendSearchAppStats(keyword, null, vmName);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0000C1CD File Offset: 0x0000A3CD
		public static void SendSearchAppStats(string keyword, string source, string vmName)
		{
			new Thread(delegate()
			{
				string url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
				{
					RegistryManager.Instance.Host,
					"stats/searchappstats"
				});
				Dictionary<string, string> dictionary = new Dictionary<string, string>
				{
					{
						"keyword",
						keyword
					}
				};
				if (source != null)
				{
					dictionary.Add("source", source);
				}
				try
				{
					Logger.Info("Sending Search App Stats for: {0}", new object[]
					{
						keyword
					});
					string text = BstHttpClient.Post(url, dictionary, null, false, vmName, 0, 1, 0, false, "bgp");
					Logger.Info("Got Search App Stat response: {0}", new object[]
					{
						text
					});
				}
				catch (Exception ex)
				{
					Logger.Error(ex.ToString());
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0002C060 File Offset: 0x0002A260
		public static void SendAppInstallStats(string appName, string packageName, string appVersion, string appVersionName, string appInstall, string isUpdate, string source, string vmName, string campaignName, string clientVersion, string apkType = "")
		{
			new Thread(delegate()
			{
				string url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
				{
					RegistryManager.Instance.Host,
					"stats/appinstallstats"
				});
				Dictionary<string, string> dictionary = new Dictionary<string, string>
				{
					{
						"email",
						Stats.GetURLSafeBase64String(RegistryManager.Instance.RegisteredEmail)
					},
					{
						"app_name",
						Stats.GetURLSafeBase64String(appName)
					},
					{
						"app_pkg",
						Stats.GetURLSafeBase64String(packageName)
					},
					{
						"app_ver",
						Stats.GetURLSafeBase64String(appVersion)
					},
					{
						"is_install",
						Stats.GetURLSafeBase64String(appInstall)
					},
					{
						"is_update",
						Stats.GetURLSafeBase64String(isUpdate)
					},
					{
						"user_time",
						Stats.GetURLSafeBase64String(Stats.Timestamp)
					},
					{
						"install_source",
						Stats.GetURLSafeBase64String(source)
					},
					{
						"utm_campaign",
						Stats.GetURLSafeBase64String(campaignName)
					},
					{
						"client_ver",
						Stats.GetURLSafeBase64String(clientVersion)
					},
					{
						"apk_type",
						Stats.GetURLSafeBase64String(apkType)
					},
					{
						"app_ver_name",
						Stats.GetURLSafeBase64String(appVersionName)
					}
				};
				if (!string.IsNullOrEmpty(RegistryManager.Instance.ClientLaunchParams))
				{
					JObject jobject = JObject.Parse(RegistryManager.Instance.ClientLaunchParams);
					if (jobject["campaign_id"] != null)
					{
						if (jobject["isFarmingInstance"] != null)
						{
							dictionary.Add("feature_campaign_id", Stats.GetURLSafeBase64String(jobject["campaign_id"].ToString()));
						}
						else
						{
							dictionary.Add("externalsource_campaignid", Stats.GetURLSafeBase64String(jobject["campaign_id"].ToString()));
						}
					}
					if (jobject["source_version"] != null)
					{
						dictionary.Add("externalsource_version", Stats.GetURLSafeBase64String(jobject["source_version"].ToString()));
					}
				}
				try
				{
					Logger.Info("Sending App Install Stats for: {0}", new object[]
					{
						appName
					});
					string text = BstHttpClient.Post(url, dictionary, null, false, vmName, 0, 1, 0, false, "bgp");
					Logger.Debug("Got App Install Stat response: {0}", new object[]
					{
						text
					});
				}
				catch (Exception ex)
				{
					Logger.Error("Error in Sending AppInstallStats : " + ex.ToString());
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0000C205 File Offset: 0x0000A405
		public static void SendSystemInfoStats(string vmName)
		{
			Stats.SendSystemInfoStatsAsync(null, true, null, null, null, null, vmName);
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0002C0E4 File Offset: 0x0002A2E4
		public static void SendSystemInfoStatsAsync(string host, bool createRegKey, Dictionary<string, string> dataInfo, string guid, string pfDir, string pdDir, string vmName)
		{
			new Thread(delegate()
			{
				Stats.SendSystemInfoStatsSync(host, createRegKey, dataInfo, guid, pfDir, pdDir, vmName);
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0002C148 File Offset: 0x0002A348
		public static string SendSystemInfoStatsSync(string host, bool createRegKey, Dictionary<string, string> dataInfo, string guid, string programFilesDir, string programDataDir, string vmName)
		{
			string text = "not sent";
			try
			{
				Dictionary<string, string> dictionary = Profile.Info();
				Logger.Info("Got Device Profile Info:");
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					Logger.Info(keyValuePair.Key + " " + keyValuePair.Value);
				}
				if (host == null)
				{
					host = RegistryManager.Instance.Host;
				}
				string url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
				{
					host,
					"stats/systeminfostats"
				});
				Dictionary<string, string> dictionary2 = new Dictionary<string, string>
				{
					{
						"p",
						Stats.GetURLSafeBase64String(dictionary["Processor"])
					},
					{
						"nop",
						Stats.GetURLSafeBase64String(dictionary["NumberOfProcessors"])
					},
					{
						"g",
						Stats.GetURLSafeBase64String(dictionary["GPU"])
					},
					{
						"gd",
						Stats.GetURLSafeBase64String(dictionary["GPUDriver"])
					},
					{
						"o",
						Stats.GetURLSafeBase64String(dictionary["OS"])
					},
					{
						"osv",
						Stats.GetURLSafeBase64String(dictionary["OSVersion"])
					},
					{
						"sr",
						Stats.GetURLSafeBase64String(dictionary["ScreenResolution"])
					},
					{
						"dnv",
						Stats.GetURLSafeBase64String(dictionary["DotNetVersion"])
					},
					{
						"osl",
						Stats.GetURLSafeBase64String(CultureInfo.CurrentCulture.Name.ToLower(CultureInfo.InvariantCulture))
					},
					{
						"oem_info",
						Stats.GetURLSafeBase64String(dictionary["OEMInfo"])
					},
					{
						"ram",
						Stats.GetURLSafeBase64String(dictionary["RAM"])
					},
					{
						"machine_type",
						Stats.GetURLSafeBase64String(dictionary["OSVERSIONTYPE"])
					}
				};
				if (dataInfo != null)
				{
					dictionary2.Add("glmode", Stats.GetURLSafeBase64String(dataInfo["GlMode"]));
					dictionary2.Add("glrendermode", Stats.GetURLSafeBase64String(dataInfo["GlRenderMode"]));
					dictionary2.Add("gl_vendor", Stats.GetURLSafeBase64String(dataInfo["GlVendor"]));
					dictionary2.Add("gl_renderer", Stats.GetURLSafeBase64String(dataInfo["GlRenderer"]));
					dictionary2.Add("gl_version", Stats.GetURLSafeBase64String(dataInfo["GlVersion"]));
					dictionary2.Add("bstr", Stats.GetURLSafeBase64String(dataInfo["BlueStacksResolution"]));
					if (dataInfo.ContainsKey("gl_check"))
					{
						dictionary2.Add("gl_check", Stats.GetURLSafeBase64String(dataInfo["gl_check"]));
					}
					if (dataInfo.ContainsKey("supported_glmodes"))
					{
						dictionary2.Add("supported_glmodes", Stats.GetURLSafeBase64String(dataInfo["supported_glmodes"]));
					}
					if (dataInfo.ContainsKey("IsVulkanSupported"))
					{
						dictionary2.Add("is_vulkan_supported", dataInfo["IsVulkanSupported"]);
					}
				}
				else
				{
					dictionary2.Add("bstr", Stats.GetURLSafeBase64String(dictionary["BlueStacksResolution"]));
					dictionary2.Add("glmode", Stats.GetURLSafeBase64String(dictionary["GlMode"]));
					dictionary2.Add("glrendermode", Stats.GetURLSafeBase64String(dictionary["GlRenderMode"]));
				}
				try
				{
					string text2;
					string text3;
					string text4;
					bool graphicsInfo = Utils.GetGraphicsInfo(programFilesDir + "\\HD-GLCheck.exe", "2", out text2, out text3, out text4, false) != 0;
					int graphicsInfo2 = Utils.GetGraphicsInfo(programFilesDir + "\\HD-GLCheck.exe", "3", out text2, out text3, out text4, false);
					int graphicsInfo3 = Utils.GetGraphicsInfo(programFilesDir + "\\HD-GLCheck.exe", "1", out text2, out text3, out text4, false);
					string originalString;
					if (!graphicsInfo)
					{
						originalString = "1";
					}
					else
					{
						originalString = "0";
					}
					string originalString2;
					if (graphicsInfo2 == 0)
					{
						originalString2 = "1";
					}
					else
					{
						originalString2 = "0";
					}
					string originalString3;
					if (graphicsInfo3 == 0)
					{
						originalString3 = "1";
					}
					else
					{
						originalString3 = "0";
					}
					dictionary2.Add("dx9check", Stats.GetURLSafeBase64String(originalString));
					dictionary2.Add("dx11check", Stats.GetURLSafeBase64String(originalString2));
					dictionary2.Add("gl_check", Stats.GetURLSafeBase64String(originalString3));
				}
				catch (Exception ex)
				{
					Logger.Error("got exception when checking dxcheck and glcheck for sending to systeminfostats ex:{0}", new object[]
					{
						ex.ToString()
					});
				}
				bool flag = false;
				if (!Utils.CheckTwoCameraPresentOnDevice(ref flag))
				{
					Logger.Error("Check for Two Camera Present on Device Failed");
				}
				Logger.Info("Two Camera present on Device: " + flag.ToString());
				dictionary2.Add("two_camera", flag ? "1" : "0");
				Logger.Info("TwoCamera Value: " + dictionary2["two_camera"]);
				if (guid != null)
				{
					dictionary2.Add("guid", Stats.GetURLSafeBase64String(guid));
				}
				dictionary2.Add("install_id", RegistryManager.Instance.InstallID);
				if (string.IsNullOrEmpty(programDataDir))
				{
					programDataDir = RegistryStrings.UserDefinedDir;
				}
				dictionary2.Add("program_data_drive_type", SSDCheck.IsMediaTypeSSD(programDataDir) ? "ssd" : "hdd");
				Logger.Info("Sending System Info Stats");
				vmName = "";
				text = BstHttpClient.Post(url, dictionary2, null, false, vmName, 10000, 1, 0, false, "bgp");
				Logger.Info("Got System Info  response: {0}", new object[]
				{
					text
				});
				if (createRegKey)
				{
					RegistryManager.Instance.SystemStats = 1;
				}
			}
			catch (Exception ex2)
			{
				Logger.Error(ex2.ToString());
			}
			return text;
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0002C708 File Offset: 0x0002A908
		public static void SendFrontendStatusUpdate(string evt, string vmName)
		{
			Logger.Info("SendFrontendStatusUpdate: evt {0}", new object[]
			{
				evt
			});
			Thread thread = new Thread(delegate()
			{
				try
				{
					string text = string.Format(CultureInfo.InvariantCulture, "http://127.0.0.1:{0}", new object[]
					{
						RegistryManager.Instance.AgentServerPort
					});
					string text2 = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
					{
						text,
						"FrontendStatusUpdate"
					});
					Dictionary<string, string> data = new Dictionary<string, string>
					{
						{
							"event",
							evt
						}
					};
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					if (!vmName.Equals("Android", StringComparison.OrdinalIgnoreCase))
					{
						dictionary.Add("vmid", vmName.Split(new char[]
						{
							'_'
						})[1]);
					}
					Logger.Info("Sending FrontendStatusUpdate to {0}", new object[]
					{
						text2
					});
					string text3 = BstHttpClient.Post(text2, data, dictionary, false, vmName, 0, 10, 1000, false, "bgp");
					Logger.Info("Got FrontendStatusUpdate response: {0}", new object[]
					{
						text3
					});
				}
				catch (Exception ex)
				{
					Logger.Error(string.Format(CultureInfo.InvariantCulture, "Error Occured, Err : {0}", new object[]
					{
						ex.ToString()
					}));
				}
			})
			{
				IsBackground = true
			};
			thread.Start();
			if (string.Compare(evt, "frontend-closed", StringComparison.OrdinalIgnoreCase) == 0)
			{
				thread.Join(200);
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0002C780 File Offset: 0x0002A980
		public static void SendTimelineStats(long agent_timestamp, long sequence, string evt, long duration, string s1, string s2, string s3, string s4, string s5, string s6, string s7, string s8, string timezone, string locale, long from_timestamp, long to_timestamp, long from_ticks, long to_ticks, string vmName)
		{
			try
			{
				string url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
				{
					RegistryManager.Instance.Host,
					"stats/timelinestats4"
				});
				Dictionary<string, string> data = new Dictionary<string, string>
				{
					{
						"agent_timestamp",
						Stats.GetURLSafeBase64String(agent_timestamp.ToString(CultureInfo.InvariantCulture))
					},
					{
						"sequence",
						Stats.GetURLSafeBase64String(sequence.ToString(CultureInfo.InvariantCulture))
					},
					{
						"event",
						Stats.GetURLSafeBase64String(evt)
					},
					{
						"duration",
						Stats.GetURLSafeBase64String(duration.ToString(CultureInfo.InvariantCulture))
					},
					{
						"s1",
						Stats.GetURLSafeBase64String(s1)
					},
					{
						"s2",
						Stats.GetURLSafeBase64String(s2)
					},
					{
						"s3",
						Stats.GetURLSafeBase64String(s3)
					},
					{
						"s4",
						Stats.GetURLSafeBase64String(s4)
					},
					{
						"s5",
						Stats.GetURLSafeBase64String(s5)
					},
					{
						"s6",
						Stats.GetURLSafeBase64String(s6)
					},
					{
						"s7",
						Stats.GetURLSafeBase64String(s7)
					},
					{
						"s8",
						Stats.GetURLSafeBase64String(s8)
					},
					{
						"timezone",
						Stats.GetURLSafeBase64String(timezone)
					},
					{
						"locale",
						Stats.GetURLSafeBase64String(locale)
					},
					{
						"from_timestamp",
						Stats.GetURLSafeBase64String(from_timestamp.ToString(CultureInfo.InvariantCulture))
					},
					{
						"to_timestamp",
						Stats.GetURLSafeBase64String(to_timestamp.ToString(CultureInfo.InvariantCulture))
					},
					{
						"from_ticks",
						Stats.GetURLSafeBase64String(from_ticks.ToString(CultureInfo.InvariantCulture))
					},
					{
						"to_ticks",
						Stats.GetURLSafeBase64String(to_ticks.ToString(CultureInfo.InvariantCulture))
					}
				};
				BstHttpClient.Post(url, data, null, false, vmName, 0, 1, 0, false, "bgp");
			}
			catch (Exception ex)
			{
				Logger.Error("Failed to send timeline stats for : " + evt);
				Logger.Error(ex.ToString());
			}
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0002C994 File Offset: 0x0002AB94
		public static void SendBootStats(string type, bool booted, bool wait, string vmName)
		{
			Thread thread = new Thread(delegate()
			{
				string text = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
				{
					RegistryManager.Instance.Host,
					"stats/bootstats"
				});
				string value = string.Empty;
				if (!string.IsNullOrEmpty(RegistryManager.Instance.ClientLaunchParams))
				{
					JObject jobject = JObject.Parse(RegistryManager.Instance.ClientLaunchParams);
					if (jobject["campaign_id"] != null)
					{
						value = jobject["campaign_id"].ToString();
					}
				}
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				if (!string.IsNullOrEmpty(value))
				{
					dictionary.Add("x_campaign_id", value);
				}
				Dictionary<string, string> data = new Dictionary<string, string>
				{
					{
						"type",
						Stats.GetURLSafeBase64String(type)
					},
					{
						"booted",
						Stats.GetURLSafeBase64String(booted.ToString(CultureInfo.InvariantCulture))
					}
				};
				try
				{
					Logger.Info("Sending Boot Stats to {0}", new object[]
					{
						text
					});
					string text2 = BstHttpClient.Post(text, data, dictionary, false, vmName, 0, 1, 0, false, "bgp");
					Logger.Info("Got Boot Stats response: {0}", new object[]
					{
						text2
					});
				}
				catch (Exception ex)
				{
					Logger.Error(ex.ToString());
				}
			})
			{
				IsBackground = true
			};
			thread.Start();
			if (wait && !thread.Join(5000))
			{
				thread.Abort();
			}
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0000C213 File Offset: 0x0000A413
		public static void SendHomeScreenDisplayedStats(string vmName)
		{
			new Thread(delegate()
			{
				string text = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
				{
					RegistryManager.Instance.Host,
					"stats/homescreenstats"
				});
				try
				{
					Logger.Info("Sending Home Screen Displayed Stats to {0}", new object[]
					{
						text
					});
					string text2 = BstHttpClient.Get(text, null, false, vmName, 0, 1, 0, false, "bgp");
					Logger.Info("Got Home Screen Displayed Stats response: {0}", new object[]
					{
						text2
					});
				}
				catch (Exception ex)
				{
					Logger.Error(ex.ToString());
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0002C9F0 File Offset: 0x0002ABF0
		public static void SendBtvFunnelStatsSync(string network, string statEvent, string statDataKey, string statDataValue, string vmName)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				RegistryManager.Instance.Host,
				"stats/btvfunnelstats"
			});
			Dictionary<string, string> dictionary = new Dictionary<string, string>
			{
				{
					"session_id",
					Stats.SessionId
				},
				{
					"streaming_platform",
					network
				},
				{
					"event_type",
					statEvent
				}
			};
			if (statDataKey != null)
			{
				dictionary.Add(statDataKey, statDataValue);
			}
			try
			{
				Logger.Info("Sending Btv Funnel Stats to {0}", new object[]
				{
					text
				});
				vmName = "";
				BstHttpClient.Post(text, dictionary, null, false, vmName, 0, 1, 0, false, "bgp");
				Logger.Info("Sent Btv Funnel Stats");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.ToString());
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0000C23D File Offset: 0x0000A43D
		public static void SendStyleAndThemeInfoStats(string actionName, string styleName, string themeName, string optionalParam, string vmName)
		{
			Stats.SendStyleAndThemeInfoStatsAsync(actionName, styleName, themeName, optionalParam, vmName);
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002CABC File Offset: 0x0002ACBC
		public static void SendStyleAndThemeInfoStatsAsync(string actionName, string styleName, string themeName, string optionalParam, string vmName)
		{
			new Thread(delegate()
			{
				Stats.SendStyleAndThemeInfoStatsSync(actionName, styleName, themeName, optionalParam, vmName);
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0002CB10 File Offset: 0x0002AD10
		public static void SendStyleAndThemeInfoStatsSync(string actionName, string styleName, string themeName, string optionalParam, string vmName)
		{
			try
			{
				Logger.Info("Sending Style and Theme Stats");
				Dictionary<string, string> dictionary = Stats.CollectStyleAndThemeData(actionName, styleName, themeName, optionalParam);
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					Logger.Info(keyValuePair.Key + " " + keyValuePair.Value);
				}
				Stats.SendData(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
				{
					RegistryManager.Instance.Host,
					"/stats/miscellaneousstats"
				}), dictionary, vmName, 0);
			}
			catch (Exception ex)
			{
				Logger.Error(ex.ToString());
			}
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0002CBD8 File Offset: 0x0002ADD8
		public static void SendMiscellaneousStatsSync(string tag, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6 = null, string arg7 = null, string arg8 = null, string vmName = "Android", int timeOut = 0)
		{
			try
			{
				Logger.Info("Sending miscellaneous stats for tag: {0}", new object[]
				{
					tag
				});
				Dictionary<string, string> dictionary = new Dictionary<string, string>
				{
					{
						"tag",
						tag
					},
					{
						"arg1",
						arg1
					},
					{
						"arg2",
						arg2
					},
					{
						"arg3",
						arg3
					},
					{
						"arg4",
						arg4
					},
					{
						"arg5",
						arg5
					},
					{
						"arg6",
						arg6
					},
					{
						"arg7",
						arg7
					},
					{
						"arg8",
						arg8
					}
				};
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					Logger.Debug(keyValuePair.Key + " " + keyValuePair.Value);
				}
				Stats.SendData(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
				{
					RegistryManager.Instance.Host,
					"/stats/miscellaneousstats"
				}), dictionary, vmName, timeOut);
			}
			catch (Exception ex)
			{
				Logger.Error(ex.ToString());
			}
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0002CD30 File Offset: 0x0002AF30
		public static void SendMiscellaneousStatsAsync(string tag, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6 = null, string arg7 = null, string arg8 = null, string vmName = "Android", int timeOut = 0)
		{
			new Thread(delegate()
			{
				Stats.SendMiscellaneousStatsSync(tag, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, vmName, timeOut);
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0002CDB4 File Offset: 0x0002AFB4
		public static void SendMiscellaneousStatsAsyncForDMM(string arg1, string arg2, string arg3 = null, string arg4 = null, string arg5 = null, string vmName = "Android", int timeOut = 0)
		{
			if ("bgp".Equals("dmm", StringComparison.InvariantCultureIgnoreCase))
			{
				new Thread(delegate()
				{
					Stats.SendMiscellaneousStatsSync("dmm_event", RegistryManager.Instance.UserGuid, arg1, arg2, arg3, arg4, arg5, RegistryManager.Instance.ClientVersion, RegistryManager.Instance.InstallID, vmName, timeOut);
				})
				{
					IsBackground = true
				}.Start();
			}
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002CE2C File Offset: 0x0002B02C
		public static void SendGamingMouseStats(string jsonData, string vmName)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>
			{
				{
					"dataset_id",
					"SystemInfoStatsDataset"
				},
				{
					"table_id",
					"GamingMouseStats"
				},
				{
					"body",
					jsonData
				}
			};
			foreach (KeyValuePair<string, string> keyValuePair in dictionary)
			{
				Logger.Info(keyValuePair.Key + " " + keyValuePair.Value);
			}
			string url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				RegistryManager.Instance.Host,
				"bigquery/uploadtobigquery"
			});
			vmName = "";
			Stats.SendData(url, dictionary, vmName, 0);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0002CEFC File Offset: 0x0002B0FC
		public static void SendData(string url, Dictionary<string, string> data, string vmName, int timeOut = 0)
		{
			Logger.Info("Sending stats to " + url);
			try
			{
				BstHttpClient.Post(url, data, null, false, vmName, timeOut, 1, 0, false, "bgp");
			}
			catch (Exception ex)
			{
				Logger.Error(ex.ToString());
			}
			Logger.Info("Sent stats");
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0002CF58 File Offset: 0x0002B158
		public static void SendCommonClientStatsAsync(string featureType, string eventType, string vmName, string packageName = "", string extraInfo = "", string arg2 = "", string arg4 = "")
		{
			ThreadPool.QueueUserWorkItem(delegate(object obj)
			{
				try
				{
					Logger.Info("Sending Client Stats");
					Dictionary<string, string> dictionary = new Dictionary<string, string>
					{
						{
							"guid",
							RegistryManager.Instance.UserGuid
						},
						{
							"prod_ver",
							RegistryManager.Instance.ClientVersion
						},
						{
							"utm_campaign",
							string.Empty
						},
						{
							"feature_type",
							featureType
						},
						{
							"event_type",
							eventType
						},
						{
							"app_pkg",
							packageName
						},
						{
							"arg1",
							extraInfo
						},
						{
							"arg2",
							arg2
						},
						{
							"arg3",
							RegistryManager.Instance.UserSelectedLocale
						}
					};
					if (!string.IsNullOrEmpty(arg4))
					{
						dictionary.Add("game_popup_id", arg4);
					}
					Stats.SendData(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
					{
						RegistryManager.Instance.Host,
						"bs4/stats/clientstats"
					}), dictionary, vmName, 0);
				}
				catch (Exception ex)
				{
					Logger.Error("Exception in sending client stats async err : " + ex.ToString());
				}
			});
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0002CFB0 File Offset: 0x0002B1B0
		private static Dictionary<string, string> CollectStyleAndThemeData(string actionName, string styleName, string themeName, string optionalParam)
		{
			return new Dictionary<string, string>
			{
				{
					"tag",
					"StyleAndThemeData"
				},
				{
					"arg1",
					RegistryManager.Instance.UserGuid
				},
				{
					"arg2",
					actionName
				},
				{
					"arg3",
					styleName
				},
				{
					"arg4",
					themeName
				},
				{
					"arg5",
					optionalParam
				}
			};
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0002D018 File Offset: 0x0002B218
		private static string Timestamp
		{
			get
			{
				long num = DateTime.Now.Ticks - DateTime.Parse("01/01/1970 00:00:00", CultureInfo.InvariantCulture).Ticks;
				return (num / 10000000L).ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0000C24A File Offset: 0x0000A44A
		private static string GetURLSafeBase64String(string originalString)
		{
			if (string.IsNullOrEmpty(originalString))
			{
				return "";
			}
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(originalString));
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0002D060 File Offset: 0x0002B260
		public static void SendMultiInstanceStatsAsync(string vmId, string oem, string cloneType, string eventType, string timeCompletion, string exitCode, bool wait)
		{
			Thread thread = new Thread(delegate()
			{
				string text = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
				{
					RegistryManager.Instance.Host,
					"stats/multiinstancestats"
				});
				Dictionary<string, string> data = new Dictionary<string, string>
				{
					{
						"vm_id",
						Stats.GetURLSafeBase64String(vmId)
					},
					{
						"oem",
						Stats.GetURLSafeBase64String(oem)
					},
					{
						"return_code",
						Stats.GetURLSafeBase64String(exitCode)
					},
					{
						"clone_type",
						Stats.GetURLSafeBase64String(cloneType)
					},
					{
						"event_type",
						Stats.GetURLSafeBase64String(eventType)
					},
					{
						"time_completed",
						Stats.GetURLSafeBase64String(timeCompletion)
					}
				};
				try
				{
					Logger.Info("Sending MultiInstance Stats to {0}", new object[]
					{
						text
					});
					string text2 = BstHttpClient.Post(text, data, null, false, vmId, 0, 1, 0, false, "bgp");
					Logger.Info("Got MultiInstance Stats response: {0}", new object[]
					{
						text2
					});
				}
				catch (Exception ex)
				{
					Logger.Error(ex.ToString());
				}
			})
			{
				IsBackground = true
			};
			thread.Start();
			if (wait && !thread.Join(5000))
			{
				thread.Abort();
			}
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0002D0D4 File Offset: 0x0002B2D4
		public static void SendMultiInstanceStatsAsync(string eventName, string displayName, string performance, string resolution, int abiValue, string dpi, int instanceCount, string oemOption, string prodVerOption, string arg1, string arg2, string vmId, string utmCampaign, bool isMim, string arg3 = "")
		{
			new Thread(delegate()
			{
				string text = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
				{
					RegistryManager.Instance.Host,
					"stats/multiinstancestats"
				});
				Dictionary<string, string> dictionary = new Dictionary<string, string>
				{
					{
						"event_name",
						eventName
					},
					{
						"display_name",
						displayName
					},
					{
						"performance",
						performance
					},
					{
						"resolution",
						resolution
					},
					{
						"abi_value",
						abiValue.ToString(CultureInfo.InvariantCulture)
					},
					{
						"dpi",
						dpi
					},
					{
						"instance_count",
						instanceCount.ToString(CultureInfo.InvariantCulture)
					},
					{
						"oem_option",
						oemOption
					},
					{
						"prod_ver_option",
						prodVerOption
					},
					{
						"arg1",
						arg1
					},
					{
						"arg2",
						arg2
					},
					{
						"vm_id",
						vmId
					},
					{
						"utm_campaign",
						utmCampaign
					},
					{
						"is_mim",
						isMim.ToString(CultureInfo.InvariantCulture)
					}
				};
				if (!string.IsNullOrEmpty(arg3))
				{
					dictionary.Add("arg3", arg3);
				}
				try
				{
					Logger.Info("Sending MultiInstance Stats to {0}", new object[]
					{
						text
					});
					string text2 = BstHttpClient.Post(text, dictionary, null, false, vmId, 0, 1, 0, false, "bgp");
					Logger.Info("Got MultiInstance Stats response: {0}", new object[]
					{
						text2
					});
				}
				catch (Exception ex)
				{
					Logger.Error(ex.ToString());
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0000C26A File Offset: 0x0000A46A
		public static void SendTroubleshooterStatsASync(string eventType, string issueName, string ver, string vm)
		{
			new Thread(delegate()
			{
				Logger.Info("Sending Troubleshooter stats");
				string url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
				{
					RegistryManager.Instance.Host,
					"stats/troubleshooterlogs"
				});
				Dictionary<string, string> data = new Dictionary<string, string>
				{
					{
						"guid",
						Utils.GetUserGUID()
					},
					{
						"prod_ver",
						RegistryManager.Instance.Version
					},
					{
						"issue_name",
						issueName
					},
					{
						"event_type",
						eventType
					},
					{
						"country",
						Utils.GetUserCountry(vm)
					},
					{
						"oem",
						RegistryManager.Instance.Oem
					},
					{
						"locale",
						CultureInfo.CurrentCulture.ToString()
					},
					{
						"troubleshooter_ver",
						ver
					}
				};
				try
				{
					BstHttpClient.Post(url, data, null, false, vm, 5000, 1, 0, false, "bgp");
				}
				catch (Exception ex)
				{
					Logger.Error(ex.ToString());
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x0002D178 File Offset: 0x0002B378
		public static Dictionary<string, string> GetUnifiedInstallStatsCommonData()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>
			{
				{
					"os",
					Profile.OS
				},
				{
					"oem",
					"bgp"
				},
				{
					"guid",
					RegistryManager.Instance.UserGuid
				},
				{
					"email",
					RegistryManager.Instance.RegisteredEmail
				},
				{
					"locale",
					RegistryManager.Instance.UserSelectedLocale
				},
				{
					"install_id",
					RegistryManager.Instance.InstallID
				},
				{
					"campaign_hash",
					RegistryManager.Instance.CampaignMD5
				},
				{
					"campaign_name",
					RegistryManager.Instance.CampaignName
				},
				{
					"client_version",
					"4.250.0.1070"
				},
				{
					"engine_version",
					"4.250.0.1070"
				},
				{
					"product_version",
					"4.250.0.1070"
				}
			};
			if (RegistryManager.Instance.InstallationType != InstallationTypes.FullEdition)
			{
				dictionary.Add("installation_type", RegistryManager.Instance.InstallationType.ToString());
				dictionary.Add("gaming_pkg_name", RegistryManager.Instance.InstallerPkgName);
			}
			return dictionary;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0000C2A9 File Offset: 0x0000A4A9
		public static void SendUnifiedInstallStatsAsync(string eventName, string email = "", string vmname = "Android", string campaignID = "")
		{
			ThreadPool.QueueUserWorkItem(delegate(object obj)
			{
				try
				{
					Stats.SendUnifiedInstallStats(eventName, email, vmname, campaignID);
				}
				catch (Exception ex)
				{
					Logger.Error("An exception in sending unified install stats. Ex: {0}", new object[]
					{
						ex
					});
				}
			});
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0002D2A0 File Offset: 0x0002B4A0
		public static string SendUnifiedInstallStats(string eventName, string email = "", string vmname = "Android", string campaignID = "")
		{
			string text = "";
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (!string.IsNullOrEmpty(campaignID))
			{
				dictionary.Add("x_campaign_id", campaignID);
			}
			Dictionary<string, string> unifiedInstallStatsCommonData = Stats.GetUnifiedInstallStatsCommonData();
			unifiedInstallStatsCommonData.Add("event", eventName);
			if (!string.IsNullOrEmpty(email))
			{
				unifiedInstallStatsCommonData["email"] = email;
			}
			try
			{
				HTTPUtils.SendRequestToCloud("/bs3/stats/unified_install_stats", unifiedInstallStatsCommonData, vmname, 0, dictionary, false, 1, 0, false);
				Logger.Debug(string.Format(CultureInfo.InvariantCulture, "Response for event {0}: {1}", new object[]
				{
					eventName,
					text
				}));
			}
			catch (Exception ex)
			{
				Logger.Warning("Failed to send stats for event: {0}, Ex: {1}", new object[]
				{
					eventName,
					ex.Message
				});
			}
			return text;
		}

		// Token: 0x0400058C RID: 1420
		public const string AppInstall = "true";

		// Token: 0x0400058D RID: 1421
		public const string AppUninstall = "false";

		// Token: 0x0400058E RID: 1422
		private static string sSessionId;

		// Token: 0x02000141 RID: 321
		public enum AppType
		{
			// Token: 0x04000590 RID: 1424
			app,
			// Token: 0x04000591 RID: 1425
			market,
			// Token: 0x04000592 RID: 1426
			suggestedapps,
			// Token: 0x04000593 RID: 1427
			web
		}

		// Token: 0x02000142 RID: 322
		public enum DMMEvent
		{
			// Token: 0x04000595 RID: 1429
			download_start,
			// Token: 0x04000596 RID: 1430
			download_failed,
			// Token: 0x04000597 RID: 1431
			download_complete,
			// Token: 0x04000598 RID: 1432
			app_install_started,
			// Token: 0x04000599 RID: 1433
			app_install_success,
			// Token: 0x0400059A RID: 1434
			app_install_failed,
			// Token: 0x0400059B RID: 1435
			agent_launched,
			// Token: 0x0400059C RID: 1436
			get_progress_success,
			// Token: 0x0400059D RID: 1437
			install_app,
			// Token: 0x0400059E RID: 1438
			runapp_started,
			// Token: 0x0400059F RID: 1439
			runapp_completed,
			// Token: 0x040005A0 RID: 1440
			client_launched,
			// Token: 0x040005A1 RID: 1441
			boot_failed,
			// Token: 0x040005A2 RID: 1442
			boot_success,
			// Token: 0x040005A3 RID: 1443
			is_app_installed
		}
	}
}
