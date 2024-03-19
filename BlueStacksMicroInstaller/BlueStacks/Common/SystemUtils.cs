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
	// Token: 0x02000065 RID: 101
	public static class SystemUtils
	{
		// Token: 0x06000133 RID: 307
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool IsWow64Process(IntPtr proc, ref bool isWow);

		// Token: 0x06000134 RID: 308
		[DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
		public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

		// Token: 0x06000135 RID: 309 RVA: 0x000075B4 File Offset: 0x000057B4
		public static bool IsOSWinXP()
		{
			return Environment.OSVersion.Version.Major == 5;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000075D8 File Offset: 0x000057D8
		public static bool IsOSVista()
		{
			return Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 0;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007614 File Offset: 0x00005814
		public static bool IsOSWin7()
		{
			return Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 1;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007650 File Offset: 0x00005850
		public static bool IsOSWin8()
		{
			return Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 2;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000768C File Offset: 0x0000588C
		public static bool IsOSWin81()
		{
			return Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 3;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000076C8 File Offset: 0x000058C8
		private static bool IsOSWin10()
		{
			string text = (string)RegistryUtils.GetRegistryValue("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "ProductName", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			return text.Contains("Windows 10");
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00007700 File Offset: 0x00005900
		public static int GetOSArchitecture()
		{
			string environmentVariable = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE", EnvironmentVariableTarget.Machine);
			return (string.IsNullOrEmpty(environmentVariable) || string.Compare(environmentVariable, 0, "x86", 0, 3, StringComparison.OrdinalIgnoreCase) == 0) ? 32 : 64;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00007740 File Offset: 0x00005940
		public static bool GetOSInfo(out string osName, out string servicePack, out string osArch)
		{
			osName = "";
			servicePack = "";
			osArch = "";
			OperatingSystem osversion = Environment.OSVersion;
			Version version = osversion.Version;
			bool flag = osversion.Platform == PlatformID.Win32Windows;
			if (flag)
			{
				int minor = version.Minor;
				int num = minor;
				if (num != 0)
				{
					if (num != 10)
					{
						if (num == 90)
						{
							osName = "Me";
						}
					}
					else
					{
						bool flag2 = version.Revision.ToString(CultureInfo.InvariantCulture) == "2222A";
						if (flag2)
						{
							osName = "98SE";
						}
						else
						{
							osName = "98";
						}
					}
				}
				else
				{
					osName = "95";
				}
			}
			else
			{
				bool flag3 = osversion.Platform == PlatformID.Win32NT;
				if (flag3)
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
					{
						bool flag4 = version.Minor == 0;
						if (flag4)
						{
							osName = "2000";
						}
						else
						{
							osName = "XP";
						}
						break;
					}
					case 6:
					{
						bool flag5 = version.Minor == 0;
						if (flag5)
						{
							osName = "Vista";
						}
						else
						{
							bool flag6 = version.Minor == 1;
							if (flag6)
							{
								osName = "7";
							}
							else
							{
								bool flag7 = version.Minor == 2;
								if (flag7)
								{
									osName = "8";
								}
								else
								{
									bool flag8 = version.Minor == 3;
									if (flag8)
									{
										osName = "8.1";
									}
								}
							}
						}
						break;
					}
					case 10:
						osName = "10";
						break;
					}
				}
			}
			string text = osName;
			bool flag9 = !string.IsNullOrEmpty(text);
			bool result;
			if (flag9)
			{
				text = "Windows " + text;
				bool flag10 = !string.IsNullOrEmpty(osversion.ServicePack);
				if (flag10)
				{
					servicePack = osversion.ServicePack.Substring(osversion.ServicePack.LastIndexOf(' ') + 1);
					text = text + " " + osversion.ServicePack;
				}
				osArch = SystemUtils.GetOSArchitecture().ToString(CultureInfo.InvariantCulture) + "-bit";
				text = text + " " + osArch;
				Logger.Info("Operating system details: " + text);
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000079A8 File Offset: 0x00005BA8
		public static ulong GetSystemTotalPhysicalMemory()
		{
			ulong result = 0UL;
			try
			{
				ComputerInfo computerInfo = new ComputerInfo();
				result = ulong.Parse(computerInfo.TotalPhysicalMemory.ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
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

		// Token: 0x0600013E RID: 318 RVA: 0x00007A18 File Offset: 0x00005C18
		public static bool IsOs64Bit()
		{
			return IntPtr.Size == 8 || (IntPtr.Size == 4 && SystemUtils.Is32BitProcessOn64BitProcessor());
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007A50 File Offset: 0x00005C50
		private static bool Is32BitProcessOn64BitProcessor()
		{
			bool result = false;
			SystemUtils.IsWow64Process(Process.GetCurrentProcess().Handle, ref result);
			return result;
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00007A78 File Offset: 0x00005C78
		public static DateTime FromUnixEpochToLocal(long secs)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return dateTime.AddSeconds((double)secs).ToLocalTime();
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00007AB0 File Offset: 0x00005CB0
		public static int CurrentDPI
		{
			get
			{
				bool flag = SystemUtils.currentDPI.Equals(int.MinValue);
				if (flag)
				{
					SystemUtils.currentDPI = SystemUtils.GetDPI();
				}
				return SystemUtils.currentDPI;
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00007AE8 File Offset: 0x00005CE8
		public static int GetDPI()
		{
			Logger.Info("Getting DPI");
			Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
			IntPtr hdc = graphics.GetHdc();
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

		// Token: 0x06000143 RID: 323 RVA: 0x00007B60 File Offset: 0x00005D60
		public static bool IsAdministrator()
		{
			bool result = false;
			try
			{
				WindowsIdentity current = WindowsIdentity.GetCurrent();
				bool flag = current == null;
				if (flag)
				{
					return false;
				}
				WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
				result = windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
			}
			catch
			{
			}
			return result;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00007BB8 File Offset: 0x00005DB8
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
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						num++;
						PropertyDataCollection properties = managementObject.Properties;
						foreach (PropertyData propertyData in properties)
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

		// Token: 0x040001DF RID: 479
		public const int DEFAULT_DPI = 96;

		// Token: 0x040001E0 RID: 480
		private static int currentDPI = int.MinValue;

		// Token: 0x020000CF RID: 207
		public enum DeviceCap
		{
			// Token: 0x0400079D RID: 1949
			LOGPIXELSX = 88,
			// Token: 0x0400079E RID: 1950
			LOGPIXELSY = 90,
			// Token: 0x0400079F RID: 1951
			VERTRES = 10,
			// Token: 0x040007A0 RID: 1952
			DESKTOPVERTRES = 117
		}
	}
}
