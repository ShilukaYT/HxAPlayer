using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using BlueStacks.Common;

namespace BlueStacks.MicroInstaller
{
	// Token: 0x0200009C RID: 156
	internal class DownloaderUtils
	{
		// Token: 0x06000355 RID: 853 RVA: 0x000155EC File Offset: 0x000137EC
		public static bool IsDiskSpaceAvailable(string dataDir, out bool isSystemDrive)
		{
			isSystemDrive = false;
			try
			{
				long num = IOUtils.GetAvailableDiskSpaceOfDrive(dataDir) / 1073741824L;
				Logger.Info("HDD space available datadir: {0} GB", new object[]
				{
					num
				});
				bool flag = num < 5L;
				if (flag)
				{
					Logger.Error("Failing disk space check");
					return false;
				}
				bool flag2 = IOUtils.GetPartitionNameFromPath(dataDir) != IOUtils.GetPartitionNameFromPath(Path.GetTempPath());
				if (flag2)
				{
					long num2 = IOUtils.GetAvailableDiskSpaceOfDrive(Path.GetTempPath()) / 1048576L;
					Logger.Info("HDD space available in system drive: {0} MB", new object[]
					{
						num2
					});
					bool flag3 = num2 < 500L;
					if (flag3)
					{
						isSystemDrive = true;
						Logger.Error("Failing disk space check for installDir");
						return false;
					}
				}
			}
			catch (Exception)
			{
			}
			return true;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000156C8 File Offset: 0x000138C8
		internal static bool IsPhysicalMemoryAvailable()
		{
			try
			{
				Logger.Info("Checking for physical memory");
				string sysInfo = SystemUtils.GetSysInfo("Select TotalPhysicalMemory from Win32_ComputerSystem");
				long num = long.Parse(sysInfo);
				long num2 = 1073741824L;
				Logger.Info("Available: {0} MB, Required: {1} MB", new object[]
				{
					num / 1048576L,
					num2 / 1048576L
				});
				bool flag = num < num2;
				if (flag)
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				Logger.Warning("Failed to check physical memory, ignoring. Err: " + ex.Message);
			}
			return true;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00015774 File Offset: 0x00013974
		public static string GetCampaignHash(string installerFileName)
		{
			string text = "null";
			try
			{
				Logger.Info("FileName: {0}", new object[]
				{
					installerFileName
				});
				string text2 = "native_";
				bool flag = installerFileName.Contains(text2);
				if (flag)
				{
					int startIndex = installerFileName.LastIndexOf(text2);
					string text3 = installerFileName.Substring(startIndex);
					text3 = text3.Replace(".exe", "").Replace(text2, "");
					bool flag2 = text3.Contains(" ");
					if (flag2)
					{
						text3 = text3.Split(new char[]
						{
							' '
						})[0];
						Logger.Info("Using part filename: {0} for using campaign hash", new object[]
						{
							text3
						});
					}
					string[] array = text3.Split(new char[]
					{
						'_'
					});
					bool flag3 = array.Length > 1;
					if (flag3)
					{
						text = array[0];
						App.sAppNameBase64ByteString = array[1];
					}
					else
					{
						text = text3;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Warning("Failed to get campaign hash: {0}", new object[]
				{
					ex.Message
				});
			}
			Logger.Info("Campaign: {0}", new object[]
			{
				text
			});
			return text;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000158AC File Offset: 0x00013AAC
		public static string GetPDDir()
		{
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
			string path = "BlueStacks" + App.sRegistrySuffix;
			string text = Path.Combine(folderPath, path);
			Logger.Info("Returning PDDir: {0}", new object[]
			{
				text
			});
			return text;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x000158F4 File Offset: 0x00013AF4
		public static string GetPFDir()
		{
			string path = "BlueStacks" + App.sRegistrySuffix;
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
			string text = Path.Combine(folderPath, path);
			Logger.Info("Returning PFDir: {0}", new object[]
			{
				text
			});
			return text;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0001593C File Offset: 0x00013B3C
		internal static void DownloadImage(string url, string fileNameWithExtension, string directory)
		{
			string text = string.Empty;
			try
			{
				bool flag = !string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(fileNameWithExtension);
				if (flag)
				{
					string path = Regex.Replace(fileNameWithExtension, "[\\x22\\\\\\/:*?|<>]", " ");
					text = Path.Combine(directory, path);
					bool flag2 = !Directory.Exists(Directory.GetParent(text).FullName);
					if (flag2)
					{
						Directory.CreateDirectory(Directory.GetParent(text).FullName);
					}
					bool flag3 = !File.Exists(text);
					if (flag3)
					{
						using (WebClient webClient = new WebClient())
						{
							webClient.DownloadFile(url, text);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Cannot download icon file" + ex.ToString());
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00015A20 File Offset: 0x00013C20
		internal static string GetWebResponse(string url, Dictionary<string, string> headers = null, int timeout = 0)
		{
			string result = "";
			try
			{
				HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
				httpWebRequest.Method = "GET";
				bool flag = timeout != 0;
				if (flag)
				{
					httpWebRequest.Timeout = timeout;
				}
				bool flag2 = headers != null;
				if (flag2)
				{
					foreach (KeyValuePair<string, string> keyValuePair in headers)
					{
						httpWebRequest.Headers.Set(StringUtils.GetControlCharFreeString(keyValuePair.Key), StringUtils.GetControlCharFreeString(keyValuePair.Value));
					}
				}
				httpWebRequest.Headers.Set("x_oem", "bgp");
				httpWebRequest.Headers.Set("x_machine_id", App.sBlueStacksMachineId);
				httpWebRequest.Headers.Set("x_version_machine_id", App.sBluestacksVersionId);
				string text = string.Format("{0}/{1}/{2}", "BlueStacks", "4.250.0.1070", App.sBlueStacksMachineId);
				text += " gzip";
				byte[] bytes = Encoding.Default.GetBytes(text);
				httpWebRequest.UserAgent = Encoding.UTF8.GetString(bytes);
				using (HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse)
				{
					using (Stream responseStream = httpWebResponse.GetResponseStream())
					{
						using (StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8))
						{
							result = streamReader.ReadToEnd();
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Failed to get fle response. Err : " + ex.ToString());
			}
			return result;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00015C48 File Offset: 0x00013E48
		internal static string GetHost()
		{
			string text = "https://cloud.bluestacks.com";
			try
			{
				text = (string)RegistryUtils.GetRegistryValue(Strings.RegistryBaseKeyPath + App.sRegistrySuffix, "Host", "https://cloud.bluestacks.com", RegistryKeyKind.HKEY_CURRENT_USER);
			}
			catch (Exception ex)
			{
				Logger.Error("Couldn't fetch host. Ex: {0}", new object[]
				{
					ex
				});
			}
			Logger.Info("Using cloud host: {0}", new object[]
			{
				text
			});
			return text;
		}

		// Token: 0x04000538 RID: 1336
		public static string Host = DownloaderUtils.GetHost();

		// Token: 0x04000539 RID: 1337
		private const long SPACE_REQUIRED_IN_GB = 5L;

		// Token: 0x0400053A RID: 1338
		private const long GB_MULTIPLIER = 1073741824L;

		// Token: 0x0400053B RID: 1339
		private const long MB_MULTIPLIER = 1048576L;
	}
}
