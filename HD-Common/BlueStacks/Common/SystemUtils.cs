using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.VisualBasic.Devices;

namespace BlueStacks.Common
{
	// Token: 0x0200020B RID: 523
	public static class SystemUtils
	{
		// Token: 0x0600106D RID: 4205
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool IsWow64Process(IntPtr proc, ref bool isWow);

		// Token: 0x0600106E RID: 4206
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

		// Token: 0x0600106F RID: 4207 RVA: 0x0000E031 File Offset: 0x0000C231
		public static bool IsOSWinXP()
		{
			return Environment.OSVersion.Version.Major == 5;
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0000E045 File Offset: 0x0000C245
		public static bool IsOSVista()
		{
			return Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 0;
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0000E06D File Offset: 0x0000C26D
		public static bool IsOSWin7()
		{
			return Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 1;
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0000E095 File Offset: 0x0000C295
		public static bool IsOSWin8()
		{
			return Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 2;
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x0000E0BD File Offset: 0x0000C2BD
		public static bool IsOSWin81()
		{
			return Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 3;
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0000E0E5 File Offset: 0x0000C2E5
		private static bool IsOSWin10()
		{
			return ((string)RegistryUtils.GetRegistryValue("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "ProductName", "", RegistryKeyKind.HKEY_LOCAL_MACHINE)).Contains("Windows 10");
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0003EB8C File Offset: 0x0003CD8C
		public static int GetOSArchitecture()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE", EnvironmentVariableTarget.Machine);
			if (!string.IsNullOrEmpty(environmentVariable) && string.Compare(environmentVariable, 0, "x86", 0, 3, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return 64;
			}
			return 32;
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0003EBC4 File Offset: 0x0003CDC4
		public static bool GetOSInfo(out string osName, out string servicePack, out string osArch)
		{
			osName = "";
			servicePack = "";
			osArch = "";
			OperatingSystem osversion = Environment.OSVersion;
			Version version = osversion.Version;
			if (osversion.Platform == PlatformID.Win32Windows)
			{
				int minor = version.Minor;
				if (minor != 0)
				{
					if (minor != 10)
					{
						if (minor == 90)
						{
							osName = "Me";
						}
					}
					else if (version.Revision.ToString(CultureInfo.InvariantCulture) == "2222A")
					{
						osName = "98SE";
					}
					else
					{
						osName = "98";
					}
				}
				else
				{
					osName = "95";
				}
			}
			else if (osversion.Platform == PlatformID.Win32NT)
			{
				switch (version.Major)
				{
				case 3:
					osName = "NT 3.51";
					break;
				case 4:
					osName = "NT 4.0";
					break;
				case 5:
					if (version.Minor == 0)
					{
						osName = "2000";
					}
					else
					{
						osName = "XP";
					}
					break;
				case 6:
					if (version.Minor == 0)
					{
						osName = "Vista";
					}
					else if (version.Minor == 1)
					{
						osName = "7";
					}
					else if (version.Minor == 2)
					{
						osName = "8";
					}
					else if (version.Minor == 3)
					{
						osName = "8.1";
					}
					break;
				case 10:
					osName = "10";
					break;
				}
			}
			string text = osName;
			if (!string.IsNullOrEmpty(text))
			{
				text = "Windows " + text;
				if (!string.IsNullOrEmpty(osversion.ServicePack))
				{
					servicePack = osversion.ServicePack.Substring(osversion.ServicePack.LastIndexOf(' ') + 1);
					text = text + " " + osversion.ServicePack;
				}
				osArch = SystemUtils.GetOSArchitecture().ToString(CultureInfo.InvariantCulture) + "-bit";
				text = text + " " + osArch;
				Logger.Info("Operating system details: " + text);
				return true;
			}
			return false;
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0003EDB0 File Offset: 0x0003CFB0
		public static ulong GetSystemTotalPhysicalMemory()
		{
			ulong result = 0UL;
			try
			{
				result = ulong.Parse(new ComputerInfo().TotalPhysicalMemory.ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
			}
			catch (Exception ex)
			{
				Logger.Error("Couldn't get TotalPhysicalMemory, Ex: {0}", new object[]
				{
					ex.Message
				});
			}
			return result;
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0000E10B File Offset: 0x0000C30B
		public static bool IsOs64Bit()
		{
			return IntPtr.Size == 8 || (IntPtr.Size == 4 && SystemUtils.Is32BitProcessOn64BitProcessor());
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0003EE14 File Offset: 0x0003D014
		private static bool Is32BitProcessOn64BitProcessor()
		{
			bool result = false;
			SystemUtils.IsWow64Process(Process.GetCurrentProcess().Handle, ref result);
			return result;
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0003EE38 File Offset: 0x0003D038
		public static DateTime FromUnixEpochToLocal(long secs)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return dateTime.AddSeconds((double)secs).ToLocalTime();
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x0000E127 File Offset: 0x0000C327
		public static int CurrentDPI
		{
			get
			{
				if (SystemUtils.currentDPI.Equals(-2147483648))
				{
					SystemUtils.currentDPI = SystemUtils.GetDPI();
				}
				return SystemUtils.currentDPI;
			}
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0003EE68 File Offset: 0x0003D068
		public static int GetDPI()
		{
			Logger.Info("Getting DPI");
			IntPtr hdc = Graphics.FromHwnd(IntPtr.Zero).GetHdc();
			int num = SystemUtils.GetDeviceCaps(hdc, 88);
			int deviceCaps = SystemUtils.GetDeviceCaps(hdc, 10);
			int deviceCaps2 = SystemUtils.GetDeviceCaps(hdc, 117);
			float num2 = (float)deviceCaps / (float)deviceCaps2;
			num = (int)((float)num * num2);
			Logger.Info("DPI = {0}", new object[]
			{
				num
			});
			return num;
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0003EED0 File Offset: 0x0003D0D0
		public static bool IsAdministrator()
		{
			bool result = false;
			try
			{
				WindowsIdentity current = WindowsIdentity.GetCurrent();
				if (current == null)
				{
					return false;
				}
				result = new WindowsPrincipal(current).IsInRole(WindowsBuiltInRole.Administrator);
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0003EF18 File Offset: 0x0003D118
		public static string GetSysInfo(string query)
		{
			int num = 0;
			string text = "";
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(query))
				{
					foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
					{
						ManagementBaseObject managementBaseObject2 = (ManagementObject)managementBaseObject;
						num++;
						foreach (PropertyData propertyData in managementBaseObject2.Properties)
						{
							text = text + propertyData.Value.ToString() + "\n";
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Exception in getting sysinfo err:" + ex.ToString());
			}
			return text.Trim();
		}

		// Token: 0x04000ADB RID: 2779
		public const int DEFAULT_DPI = 96;

		// Token: 0x04000ADC RID: 2780
		private static int currentDPI = int.MinValue;

		// Token: 0x0200020C RID: 524
		public enum DeviceCap
		{
			// Token: 0x04000ADE RID: 2782
			LOGPIXELSX = 88,
			// Token: 0x04000ADF RID: 2783
			LOGPIXELSY = 90,
			// Token: 0x04000AE0 RID: 2784
			VERTRES = 10,
			// Token: 0x04000AE1 RID: 2785
			DESKTOPVERTRES = 117
		}
	}
}
