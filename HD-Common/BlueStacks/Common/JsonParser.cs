using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x0200012C RID: 300
	public class JsonParser
	{
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00008BF6 File Offset: 0x00006DF6
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x00008BFE File Offset: 0x00006DFE
		public string VmName { get; set; }

		// Token: 0x060009A8 RID: 2472 RVA: 0x00029718 File Offset: 0x00027918
		public JsonParser(string vmName)
		{
			if (string.IsNullOrEmpty(vmName))
			{
				this.VmName = "";
				this.mAppsDotJsonFile = Path.Combine(RegistryStrings.GadgetDir, "systemApps.json");
			}
			else
			{
				this.VmName = vmName;
				this.mAppsDotJsonFile = Path.Combine(RegistryStrings.GadgetDir, "apps_" + vmName + ".json");
			}
			using (Mutex mutex = new Mutex(false, "BlueStacks_AppJsonUpdate"))
			{
				if (mutex.WaitOne())
				{
					try
					{
						JsonParser.DeleteIfInvalidJsonFile(this.mAppsDotJsonFile);
					}
					catch (Exception ex)
					{
						Logger.Error("Failed to delete invalid json file... Err : " + ex.ToString());
					}
					finally
					{
						mutex.ReleaseMutex();
					}
				}
			}
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x000297F0 File Offset: 0x000279F0
		public static List<string> GetInstalledAppsList(string vmName)
		{
			List<string> list = new List<string>();
			AppInfo[] appList = new JsonParser(vmName).GetAppList();
			for (int i = 0; i < appList.Length; i++)
			{
				if (appList[i] != null && appList[i].Package != null)
				{
					list.Add(appList[i].Package);
				}
			}
			return list;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0002983C File Offset: 0x00027A3C
		public AppInfo[] GetAppList()
		{
			string json = "[]";
			using (Mutex mutex = new Mutex(false, "BlueStacks_AppJsonUpdate"))
			{
				if (mutex.WaitOne())
				{
					try
					{
						if (!File.Exists(this.mAppsDotJsonFile))
						{
							using (StreamWriter streamWriter = new StreamWriter(this.mAppsDotJsonFile, true))
							{
								streamWriter.Write("[");
								streamWriter.WriteLine();
								streamWriter.Write("]");
							}
						}
						StreamReader streamReader = new StreamReader(this.mAppsDotJsonFile);
						json = streamReader.ReadToEnd();
						streamReader.Close();
					}
					catch (Exception ex)
					{
						Logger.Error("Failed to create empty app json... Err : " + ex.ToString());
					}
					finally
					{
						mutex.ReleaseMutex();
					}
				}
			}
			this.GetOriginalJson(JArray.Parse(json));
			return this.mOriginalJson;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00029930 File Offset: 0x00027B30
		private void GetOriginalJson(JArray input)
		{
			this.mOriginalJson = new AppInfo[input.Count];
			for (int i = 0; i < input.Count; i++)
			{
				this.mOriginalJson[i] = JsonConvert.DeserializeObject<AppInfo>(input[i].ToString());
			}
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00029978 File Offset: 0x00027B78
		public int GetInstalledAppCount()
		{
			this.GetAppList();
			int num = 0;
			for (int i = 0; i < this.mOriginalJson.Length; i++)
			{
				if (string.Compare(this.mOriginalJson[i].Activity, ".Main", StringComparison.OrdinalIgnoreCase) != 0 && string.Compare(this.mOriginalJson[i].Appstore, "yes", StringComparison.OrdinalIgnoreCase) != 0)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x000299DC File Offset: 0x00027BDC
		public bool GetAppInfoFromAppName(string appName, out string packageName, out string imageName, out string activityName)
		{
			packageName = null;
			imageName = null;
			activityName = null;
			this.GetAppList();
			for (int i = 0; i < this.mOriginalJson.Length; i++)
			{
				if (this.mOriginalJson[i].Name == appName)
				{
					packageName = this.mOriginalJson[i].Package;
					imageName = this.mOriginalJson[i].Img;
					activityName = this.mOriginalJson[i].Activity;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x00029A54 File Offset: 0x00027C54
		public bool GetAppInfoFromPackageName(string packageName, out string appName, out string imageName, out string activityName, out string appstore)
		{
			appName = "";
			imageName = "";
			activityName = "";
			appstore = "";
			this.GetAppList();
			for (int i = 0; i < this.mOriginalJson.Length; i++)
			{
				if (this.mOriginalJson[i].Package == packageName)
				{
					appName = this.mOriginalJson[i].Name;
					imageName = this.mOriginalJson[i].Img;
					activityName = this.mOriginalJson[i].Activity;
					appstore = this.mOriginalJson[i].Appstore;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x00029AF0 File Offset: 0x00027CF0
		public AppInfo GetAppInfoFromPackageName(string packageName)
		{
			AppInfo result = null;
			this.GetAppList();
			for (int i = 0; i < this.mOriginalJson.Length; i++)
			{
				if (this.mOriginalJson[i].Package == packageName)
				{
					result = this.mOriginalJson[i];
				}
			}
			return result;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x00029B38 File Offset: 0x00027D38
		public string GetPackageNameFromAppName(string appName)
		{
			AppInfo appInfo = null;
			this.GetAppList();
			for (int i = 0; i < this.mOriginalJson.Length; i++)
			{
				if (this.mOriginalJson[i].Name == appName)
				{
					appInfo = this.mOriginalJson[i];
				}
			}
			if (appInfo == null)
			{
				return string.Empty;
			}
			return appInfo.Package;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00029B90 File Offset: 0x00027D90
		public string GetAppNameFromPackageActivity(string packageName, string activityName)
		{
			this.GetAppList();
			for (int i = 0; i < this.mOriginalJson.Length; i++)
			{
				if (this.mOriginalJson[i].Package == packageName && this.mOriginalJson[i].Activity == activityName)
				{
					return this.mOriginalJson[i].Name;
				}
			}
			return string.Empty;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00029BF4 File Offset: 0x00027DF4
		public string GetAppNameFromPackage(string packageName)
		{
			this.GetAppList();
			for (int i = 0; i < this.mOriginalJson.Length; i++)
			{
				if (this.mOriginalJson[i].Package == packageName)
				{
					return this.mOriginalJson[i].Name;
				}
			}
			return string.Empty;
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00029C44 File Offset: 0x00027E44
		public static bool GetGl3RequirementFromPackage(AppInfo[] appJson, string packageName)
		{
			if (appJson != null)
			{
				for (int i = 0; i < appJson.Length; i++)
				{
					if (appJson[i].Package == packageName)
					{
						return appJson[i].Gl3Required;
					}
				}
			}
			return false;
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x00029C7C File Offset: 0x00027E7C
		public static bool GetVideoPresentRequirementFromPackage(AppInfo[] appJson, string packageName)
		{
			if (appJson != null)
			{
				for (int i = 0; i < appJson.Length; i++)
				{
					if (appJson[i].Package == packageName)
					{
						return appJson[i].VideoPresent;
					}
				}
			}
			return false;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00029CB4 File Offset: 0x00027EB4
		public string GetPackageNameFromActivityName(string activityName)
		{
			this.GetAppList();
			for (int i = 0; i < this.mOriginalJson.Length; i++)
			{
				if (this.mOriginalJson[i].Activity == activityName)
				{
					return this.mOriginalJson[i].Package;
				}
			}
			return string.Empty;
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x00029D04 File Offset: 0x00027F04
		public string GetActivityNameFromPackageName(string packageName)
		{
			this.GetAppList();
			for (int i = 0; i < this.mOriginalJson.Length; i++)
			{
				if (this.mOriginalJson[i].Package == packageName)
				{
					return this.mOriginalJson[i].Activity;
				}
			}
			return string.Empty;
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x00029D54 File Offset: 0x00027F54
		public bool IsPackageNameSystemApp(string packageName)
		{
			this.GetAppList();
			for (int i = 0; i < this.mOriginalJson.Length; i++)
			{
				if (this.mOriginalJson[i].Package == packageName)
				{
					return this.mOriginalJson[i].System == "1";
				}
			}
			return false;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x00029DB0 File Offset: 0x00027FB0
		public bool IsAppNameSystemApp(string appName)
		{
			this.GetAppList();
			for (int i = 0; i < this.mOriginalJson.Length; i++)
			{
				if (this.mOriginalJson[i].Name == appName)
				{
					return this.mOriginalJson[i].System == "1";
				}
			}
			return false;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x00029E0C File Offset: 0x0002800C
		public bool IsAppInstalled(string packageName)
		{
			string text;
			return this.IsAppInstalled(packageName, out text);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00029E24 File Offset: 0x00028024
		public bool IsAppInstalled(string packageName, out string version)
		{
			this.GetAppList();
			for (int i = 0; i < this.mOriginalJson.Length; i++)
			{
				if (this.mOriginalJson[i].Package == packageName)
				{
					version = this.mOriginalJson[i].Version;
					return true;
				}
			}
			version = "NA";
			return false;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00029E7C File Offset: 0x0002807C
		public bool GetAppData(string package, string activity, out string name, out string img)
		{
			this.GetAppList();
			name = "";
			img = "";
			for (int i = 0; i < this.mOriginalJson.Length; i++)
			{
				if (this.mOriginalJson[i].Package == package && this.mOriginalJson[i].Activity == activity)
				{
					name = this.mOriginalJson[i].Name;
					img = this.mOriginalJson[i].Img;
					Logger.Info("Got AppName: {0} and AppIcon: {1}", new object[]
					{
						name,
						img
					});
					return true;
				}
			}
			return false;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00029F1C File Offset: 0x0002811C
		public void WriteJson(AppInfo[] json)
		{
			JArray jarray = new JArray();
			Logger.Info("JsonParser: Writing json object array to json writer");
			if (json != null)
			{
				for (int i = 0; i < json.Length; i++)
				{
					JObject jobject = new JObject
					{
						{
							"img",
							json[i].Img
						},
						{
							"name",
							json[i].Name
						},
						{
							"system",
							json[i].System
						},
						{
							"package",
							json[i].Package
						},
						{
							"appstore",
							json[i].Appstore
						},
						{
							"activity",
							json[i].Activity
						},
						{
							"version",
							json[i].Version
						},
						{
							"versionName",
							json[i].VersionName
						},
						{
							"gl3required",
							json[i].Gl3Required
						},
						{
							"videopresent",
							json[i].VideoPresent
						},
						{
							"isgamepadcompatible",
							json[i].IsGamepadCompatible
						}
					};
					if (json[i].Url != null)
					{
						jobject.Add("url", json[i].Url);
					}
					jarray.Add(jobject);
				}
			}
			using (Mutex mutex = new Mutex(false, "BlueStacks_AppJsonUpdate"))
			{
				if (mutex.WaitOne())
				{
					try
					{
						StreamWriter streamWriter = new StreamWriter(this.mAppsDotJsonFile + ".tmp");
						streamWriter.Write(jarray.ToString(Formatting.None, new JsonConverter[0]));
						streamWriter.Close();
						File.Copy(this.mAppsDotJsonFile + ".tmp", this.mAppsDotJsonFile + ".bak", true);
						File.Delete(this.mAppsDotJsonFile);
						int num = 10;
						while (File.Exists(this.mAppsDotJsonFile) && num > 0)
						{
							num--;
							Thread.Sleep(100);
						}
						File.Move(this.mAppsDotJsonFile + ".tmp", this.mAppsDotJsonFile);
					}
					catch (Exception ex)
					{
						Logger.Error("Failed to write in apps json file... Err : " + ex.ToString());
					}
					finally
					{
						mutex.ReleaseMutex();
					}
				}
			}
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0002A19C File Offset: 0x0002839C
		public int AddToJson(AppInfo json)
		{
			this.GetAppList();
			Logger.Info("Adding to Json");
			AppInfo[] array = new AppInfo[this.mOriginalJson.Length + 1];
			int i;
			for (i = 0; i < this.mOriginalJson.Length; i++)
			{
				array[i] = this.mOriginalJson[i];
			}
			array[i] = json;
			this.WriteJson(array);
			return this.mOriginalJson.Length;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0002A1FC File Offset: 0x000283FC
		public static void DeleteIfInvalidJsonFile(string fileName)
		{
			try
			{
				if (!JsonParser.IsValidJsonFile(fileName))
				{
					File.Delete(fileName);
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Some error in deleting file, ex: " + ex.Message);
			}
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0002A244 File Offset: 0x00028444
		private static bool IsValidJsonFile(string fileName)
		{
			bool result;
			try
			{
				JArray.Parse(File.ReadAllText(fileName));
				result = true;
			}
			catch (Exception ex)
			{
				Logger.Error("Invalid JSon file: " + fileName);
				Logger.Error(ex.Message);
				result = false;
			}
			return result;
		}

		// Token: 0x040004F3 RID: 1267
		private string mAppsDotJsonFile;

		// Token: 0x040004F4 RID: 1268
		private AppInfo[] mOriginalJson;
	}
}
