using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace BlueStacks.Common
{
	// Token: 0x02000132 RID: 306
	public static class RegistrySymlinkUtils
	{
		// Token: 0x06000BF5 RID: 3061
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool IsWow64Process(IntPtr proc, ref bool isWow);

		// Token: 0x06000BF6 RID: 3062
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern int RegOpenKeyEx(IntPtr hKey, string lpSubKey, uint ulOptions, uint samDesired, ref IntPtr phkResult);

		// Token: 0x06000BF7 RID: 3063
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern int RegCreateKeyEx(IntPtr hKey, string lpSubKey, uint Reserved, string lpClass, uint dwOptions, uint samDesired, IntPtr lpSecurityAttributes, ref IntPtr phkResult, ref uint lpdwDisposition);

		// Token: 0x06000BF8 RID: 3064
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int RegSetValueEx(IntPtr hKey, string lpValueName, uint Reserved, uint dwType, string lpData, int cbData);

		// Token: 0x06000BF9 RID: 3065
		[DllImport("ntdll.dll", SetLastError = true)]
		private static extern int ZwDeleteKey(IntPtr hKey);

		// Token: 0x06000BFA RID: 3066
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern int RegDeleteValue(IntPtr hKey, string lpValueName);

		// Token: 0x06000BFB RID: 3067
		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern int RegCloseKey(IntPtr hKey);

		// Token: 0x06000BFC RID: 3068 RVA: 0x0002B6C0 File Offset: 0x000298C0
		public static bool SymlinkCreator()
		{
			if (RegistrySymlinkUtils.IsOs64Bit())
			{
				try
				{
					RegistrySymlinkUtils.RemoveRegistrySymlink();
				}
				catch (Exception)
				{
				}
				RegistrySymlinkUtils.CreateRegistrySymlink();
			}
			return true;
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0000BF76 File Offset: 0x0000A176
		public static bool IsOs64Bit()
		{
			return IntPtr.Size == 8 || (IntPtr.Size == 4 && RegistrySymlinkUtils.Is32BitProcessOn64BitProcessor());
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0002B6F4 File Offset: 0x000298F4
		private static bool Is32BitProcessOn64BitProcessor()
		{
			bool result = false;
			RegistrySymlinkUtils.IsWow64Process(Process.GetCurrentProcess().Handle, ref result);
			return result;
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0002B718 File Offset: 0x00029918
		public static void CreateRegistrySymlink()
		{
			IntPtr hKey = (IntPtr)(-2147483646);
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			uint num = 0U;
			int num2 = RegistrySymlinkUtils.RegOpenKeyEx(hKey, "Software", 0U, 983359U, ref zero);
			if (num2 != 0)
			{
				throw new ApplicationException("Cannot open 64-bit HKLM\\Software", new Win32Exception(num2));
			}
			try
			{
				num2 = RegistrySymlinkUtils.RegCreateKeyEx(zero, "BlueStacks" + Strings.GetOemTag(), 0U, null, 2U, 983359U, IntPtr.Zero, ref zero2, ref num);
				if (num2 != 0)
				{
					throw new ApplicationException("Cannot create 64-bit registry", new Win32Exception(num2));
				}
				string text = "\\Registry\\Machine\\Software\\Wow6432Node\\BlueStacks" + Strings.GetOemTag();
				num2 = RegistrySymlinkUtils.RegSetValueEx(zero2, "SymbolicLinkValue", 0U, 6U, text, text.Length * 2);
				if (num2 != 0)
				{
					throw new ApplicationException("Cannot set registry symlink value for target" + text, new Win32Exception(num2));
				}
			}
			finally
			{
				RegistrySymlinkUtils.RegCloseKey(zero);
				if (zero2 != IntPtr.Zero)
				{
					RegistrySymlinkUtils.RegCloseKey(zero2);
				}
			}
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0002B814 File Offset: 0x00029A14
		public static void RemoveRegistrySymlink()
		{
			IntPtr hKey = (IntPtr)(-2147483646);
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			int num = RegistrySymlinkUtils.RegOpenKeyEx(hKey, "Software", 0U, 983359U, ref zero);
			if (num != 0)
			{
				throw new ApplicationException("Cannot open 64-bit HKLM\\Software", new Win32Exception(num));
			}
			try
			{
				num = RegistrySymlinkUtils.RegOpenKeyEx(zero, "BlueStacks" + Strings.GetOemTag(), 8U, 983359U, ref zero2);
				if (num != 0)
				{
					throw new ApplicationException("Cannot open 64-bit registry", new Win32Exception(num));
				}
				num = RegistrySymlinkUtils.RegDeleteValue(zero2, "SymbolicLinkValue");
				num = RegistrySymlinkUtils.ZwDeleteKey(zero2);
			}
			finally
			{
				RegistrySymlinkUtils.RegCloseKey(zero);
				if (zero2 != IntPtr.Zero)
				{
					RegistrySymlinkUtils.RegCloseKey(zero2);
				}
			}
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0002B8D4 File Offset: 0x00029AD4
		public static bool IsSymlinkPresent()
		{
			if (RegistrySymlinkUtils.IsOs64Bit())
			{
				try
				{
					IntPtr hKey = (IntPtr)(-2147483646);
					IntPtr zero = IntPtr.Zero;
					int num = RegistrySymlinkUtils.RegOpenKeyEx(hKey, "Software", 0U, 257U, ref zero);
					if (num != 0)
					{
						throw new ApplicationException("Cannot open 64-bit HKLM\\Software: 0x" + num.ToString("x", CultureInfo.InvariantCulture));
					}
					num = RegistrySymlinkUtils.RegOpenKeyEx(zero, "BlueStacks" + Strings.GetOemTag(), 8U, 257U, ref zero);
					if (num != 0)
					{
						throw new ApplicationException("Cannot open 64-bit registry: 0x" + num.ToString("x", CultureInfo.InvariantCulture));
					}
					return true;
				}
				catch (Exception ex)
				{
					Logger.Warning("Some error while detecting symlink. Ex: {0}", new object[]
					{
						ex.Message
					});
					return false;
				}
				return false;
			}
			return false;
		}

		// Token: 0x04000568 RID: 1384
		private const uint REG_LINK = 6U;

		// Token: 0x04000569 RID: 1385
		private const uint HKEY_LOCAL_MACHINE = 2147483650U;

		// Token: 0x0400056A RID: 1386
		private const uint REG_OPTION_CREATE_LINK = 2U;

		// Token: 0x0400056B RID: 1387
		private const uint REG_OPTION_OPEN_LINK = 8U;

		// Token: 0x0400056C RID: 1388
		private const uint KEY_ALL_ACCESS = 983103U;

		// Token: 0x0400056D RID: 1389
		private const uint KEY_WOW64_64KEY = 256U;

		// Token: 0x0400056E RID: 1390
		private const uint KEY_QUERY_VALUE = 1U;
	}
}
