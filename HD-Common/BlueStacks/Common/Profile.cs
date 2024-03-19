using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;
using Microsoft.Win32;

namespace BlueStacks.Common
{
	// Token: 0x0200018C RID: 396
	public static class Profile
	{
		// Token: 0x06000F06 RID: 3846
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool IsWow64Process(IntPtr proc, ref bool isWow);

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0000D5CD File Offset: 0x0000B7CD
		// (set) Token: 0x06000F08 RID: 3848 RVA: 0x0000D5D4 File Offset: 0x0000B7D4
		public static string GlVendor { get; set; } = "";

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x0000D5DC File Offset: 0x0000B7DC
		// (set) Token: 0x06000F0A RID: 3850 RVA: 0x0000D5E3 File Offset: 0x0000B7E3
		public static string GlRenderer { get; set; } = "";

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x0000D5EB File Offset: 0x0000B7EB
		// (set) Token: 0x06000F0C RID: 3852 RVA: 0x0000D5F2 File Offset: 0x0000B7F2
		public static string GlVersion { get; set; } = "";

		// Token: 0x06000F0D RID: 3853 RVA: 0x0000D5FA File Offset: 0x0000B7FA
		private static bool IsOs64Bit()
		{
			return IntPtr.Size == 8 || (IntPtr.Size == 4 && Profile.Is32BitProcessOn64BitProcessor());
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0003AF94 File Offset: 0x00039194
		private static bool Is32BitProcessOn64BitProcessor()
		{
			bool result = false;
			Profile.IsWow64Process(Process.GetCurrentProcess().Handle, ref result);
			return result;
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0003AFB8 File Offset: 0x000391B8
		public static Dictionary<string, string> Info()
		{
			if (Profile.s_Info != null)
			{
				return Profile.s_Info;
			}
			string key = "Android";
			Dictionary<string, string> dictionary = new Dictionary<string, string>
			{
				{
					"ProcessorId",
					SystemUtils.GetSysInfo("Select processorID from Win32_Processor")
				},
				{
					"Processor",
					Profile.CPU
				}
			};
			string sysInfo = SystemUtils.GetSysInfo("Select NumberOfLogicalProcessors from Win32_Processor");
			Logger.Info("the length of numOfProcessor string is {0}", new object[]
			{
				sysInfo.Length.ToString(CultureInfo.InvariantCulture)
			});
			dictionary.Add("NumberOfProcessors", sysInfo);
			dictionary.Add("GPU", Profile.GPU);
			dictionary.Add("GPUDriver", SystemUtils.GetSysInfo("Select DriverVersion from Win32_VideoController"));
			dictionary.Add("OS", Profile.OS);
			string value = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				Environment.OSVersion.Version.Major,
				Environment.OSVersion.Version.Minor
			});
			dictionary.Add("OSVersion", value);
			dictionary.Add("RAM", Profile.RAM);
			try
			{
				string version = RegistryManager.Instance.Version;
				dictionary.Add("BlueStacksVersion", version);
			}
			catch
			{
			}
			int num;
			try
			{
				num = RegistryManager.Instance.Guest[key].GlMode;
			}
			catch
			{
				num = -1;
			}
			dictionary.Add("GlMode", num.ToString(CultureInfo.InvariantCulture));
			int num2;
			try
			{
				num2 = RegistryManager.Instance.Guest[key].GlRenderMode;
			}
			catch
			{
				num2 = -1;
			}
			dictionary.Add("GlRenderMode", num2.ToString(CultureInfo.InvariantCulture));
			string value2 = "";
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\OEMInformation");
				string text = (string)registryKey.GetValue("Manufacturer", "");
				string text2 = (string)registryKey.GetValue("Model", "");
				value2 = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
				{
					text,
					text2
				});
			}
			catch
			{
			}
			dictionary.Add("OEMInfo", value2);
			int num3 = Screen.PrimaryScreen.Bounds.Width;
			int num4 = Screen.PrimaryScreen.Bounds.Height;
			dictionary.Add("ScreenResolution", num3.ToString(CultureInfo.InvariantCulture) + "x" + num4.ToString(CultureInfo.InvariantCulture));
			try
			{
				num3 = RegistryManager.Instance.Guest[key].WindowWidth;
				num4 = RegistryManager.Instance.Guest[key].WindowHeight;
				dictionary.Add("BlueStacksResolution", num3.ToString(CultureInfo.InvariantCulture) + "x" + num4.ToString(CultureInfo.InvariantCulture));
			}
			catch
			{
			}
			string value3 = "";
			try
			{
				RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP");
				foreach (string text3 in registryKey2.GetSubKeyNames())
				{
					if (text3.StartsWith("v", StringComparison.OrdinalIgnoreCase))
					{
						RegistryKey registryKey3 = registryKey2.OpenSubKey(text3);
						if (registryKey3.GetValue("Install") != null && (int)registryKey3.GetValue("Install") == 1)
						{
							value3 = (string)registryKey3.GetValue("Version");
						}
						if (text3 == "v4")
						{
							RegistryKey registryKey4 = registryKey3.OpenSubKey("Client");
							if (registryKey4 != null && (int)registryKey4.GetValue("Install") == 1)
							{
								value3 = (string)registryKey4.GetValue("Version") + " Client";
							}
							registryKey4 = registryKey3.OpenSubKey("Full");
							if (registryKey4 != null && (int)registryKey4.GetValue("Install") == 1)
							{
								value3 = (string)registryKey4.GetValue("Version") + " Full";
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Got exception when checking dot net version,err: {0}", new object[]
				{
					ex.ToString()
				});
			}
			dictionary.Add("DotNetVersion", value3);
			if (Profile.IsOs64Bit())
			{
				dictionary.Add("OSVERSIONTYPE", "64 bit");
			}
			else
			{
				dictionary.Add("OSVERSIONTYPE", "32 bit");
			}
			Profile.s_Info = dictionary;
			return Profile.s_Info;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0003B4BC File Offset: 0x000396BC
		public static Dictionary<string, string> InfoForGraphicsDriverCheck()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>
			{
				{
					"os_version",
					SystemUtils.GetSysInfo("Select Caption from Win32_OperatingSystem")
				},
				{
					"os_arch",
					SystemUtils.GetSysInfo("Select OSArchitecture from Win32_OperatingSystem")
				},
				{
					"processor_vendor",
					SystemUtils.GetSysInfo("Select Manufacturer from Win32_Processor")
				},
				{
					"processor",
					SystemUtils.GetSysInfo("Select Name from Win32_Processor")
				}
			};
			string text = SystemUtils.GetSysInfo("Select Caption from Win32_VideoController");
			string text2 = "";
			string[] array = text.Split(new string[]
			{
				Environment.NewLine,
				"\r\n",
				"\n"
			}, StringSplitOptions.RemoveEmptyEntries);
			if (!string.IsNullOrEmpty(text))
			{
				foreach (string text3 in array)
				{
					text2 = text2 + text3.Substring(0, text3.IndexOf(" ", StringComparison.OrdinalIgnoreCase)) + "\r\n";
				}
				text2 = text2.Trim();
			}
			string text4 = SystemUtils.GetSysInfo("Select DriverVersion from Win32_VideoController");
			string text5 = SystemUtils.GetSysInfo("Select DriverDate from Win32_VideoController");
			string[] array3 = text2.Split(new string[]
			{
				Environment.NewLine,
				"\r\n",
				"\n"
			}, StringSplitOptions.RemoveEmptyEntries);
			string[] array4 = text4.Split(new string[]
			{
				Environment.NewLine,
				"\r\n",
				"\n"
			}, StringSplitOptions.RemoveEmptyEntries);
			string[] array5 = text5.Split(new string[]
			{
				Environment.NewLine,
				"\r\n",
				"\n"
			}, StringSplitOptions.RemoveEmptyEntries);
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j] == Profile.GlRenderer || Profile.GlVendor.Contains(array3[j]))
				{
					text = array[j];
					text2 = array3[j];
					text4 = array4[j];
					text5 = array5[j];
					break;
				}
			}
			dictionary.Add("gpu", text);
			dictionary.Add("gpu_vendor", text2);
			dictionary.Add("driver_version", text4);
			dictionary.Add("driver_date", text5);
			string value = "";
			RegistryKey registryKey2;
			RegistryKey registryKey = registryKey2 = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\OEMInformation");
			try
			{
				if (registryKey != null)
				{
					value = (string)registryKey.GetValue("Manufacturer", "");
				}
			}
			finally
			{
				if (registryKey2 != null)
				{
					((IDisposable)registryKey2).Dispose();
				}
			}
			dictionary.Add("oem_manufacturer", value);
			string value2 = "";
			registryKey = (registryKey2 = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\OEMInformation"));
			try
			{
				if (registryKey != null)
				{
					value2 = (string)registryKey.GetValue("Model", "");
				}
			}
			finally
			{
				if (registryKey2 != null)
				{
					((IDisposable)registryKey2).Dispose();
				}
			}
			dictionary.Add("oem_model", value2);
			dictionary.Add("bst_oem", RegistryManager.Instance.Oem);
			return dictionary;
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0000D616 File Offset: 0x0000B816
		private static string ToUpper(string id)
		{
			return id.ToUpperInvariant();
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x0003B78C File Offset: 0x0003998C
		public static ulong TotalPhysicalMemory
		{
			get
			{
				if (Profile.mTotalPhysicalMemory == 0UL)
				{
					try
					{
						Profile.mTotalPhysicalMemory = ulong.Parse(new ComputerInfo().TotalPhysicalMemory.ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
					}
					catch (Exception ex)
					{
						Logger.Error("Couldn't get TotalPhysicalMemory, Ex: {0}", new object[]
						{
							ex.Message
						});
					}
				}
				return Profile.mTotalPhysicalMemory;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x0003B7FC File Offset: 0x000399FC
		public static int NumberOfLogicalProcessors
		{
			get
			{
				if (Profile.mNumberOfLogicalProcessors == 0)
				{
					try
					{
						int.TryParse(SystemUtils.GetSysInfo("Select NumberOfLogicalProcessors from Win32_Processor"), out Profile.mNumberOfLogicalProcessors);
					}
					catch (Exception ex)
					{
						Logger.Error("Couldn't get NumberOfLogicalProcessors, Ex: {0}", new object[]
						{
							ex.Message
						});
					}
				}
				return Profile.mNumberOfLogicalProcessors;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x0003B858 File Offset: 0x00039A58
		public static string RAM
		{
			get
			{
				int num = 0;
				try
				{
					num = (int)(Convert.ToUInt64(SystemUtils.GetSysInfo("Select TotalPhysicalMemory from Win32_ComputerSystem"), CultureInfo.InvariantCulture) / 1048576UL);
				}
				catch (Exception ex)
				{
					Logger.Error("Exception when finding ram");
					Logger.Error(ex.ToString());
				}
				return num.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x0000D61E File Offset: 0x0000B81E
		public static string CPU
		{
			get
			{
				return SystemUtils.GetSysInfo("Select Name from Win32_Processor");
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x0000D62A File Offset: 0x0000B82A
		public static string GPU
		{
			get
			{
				return SystemUtils.GetSysInfo("Select Caption from Win32_VideoController");
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x0000D636 File Offset: 0x0000B836
		public static string OS
		{
			get
			{
				if (string.IsNullOrEmpty(Profile.sOS))
				{
					Profile.sOS = SystemUtils.GetSysInfo("Select Caption from Win32_OperatingSystem");
				}
				return Profile.sOS;
			}
		}

		// Token: 0x040006FD RID: 1789
		private static Dictionary<string, string> s_Info;

		// Token: 0x04000701 RID: 1793
		private static ulong mTotalPhysicalMemory = 0UL;

		// Token: 0x04000702 RID: 1794
		private static int mNumberOfLogicalProcessors = 0;

		// Token: 0x04000703 RID: 1795
		private static string sOS = "";
	}
}
