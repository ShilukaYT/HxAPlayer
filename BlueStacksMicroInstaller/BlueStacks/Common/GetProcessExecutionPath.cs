using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace BlueStacks.Common
{
	// Token: 0x0200000C RID: 12
	public static class GetProcessExecutionPath
	{
		// Token: 0x06000025 RID: 37
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool QueryFullProcessImageName(IntPtr hwnd, int flags, [Out] StringBuilder buffer, out int size);

		// Token: 0x06000026 RID: 38
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr OpenProcess(int flags, bool handle, UIntPtr procId);

		// Token: 0x06000027 RID: 39
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CloseHandle(IntPtr handle);

		// Token: 0x06000028 RID: 40 RVA: 0x000030F8 File Offset: 0x000012F8
		public static List<string> GetApplicationPath(Process[] procList)
		{
			List<string> list = new List<string>();
			bool flag = procList != null;
			if (flag)
			{
				foreach (Process proc in procList)
				{
					try
					{
						string applicationPathFromProcess = GetProcessExecutionPath.GetApplicationPathFromProcess(proc);
						bool flag2 = !string.IsNullOrEmpty(applicationPathFromProcess);
						if (flag2)
						{
							list.Add(applicationPathFromProcess);
						}
					}
					catch (Exception)
					{
					}
				}
			}
			return list;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003174 File Offset: 0x00001374
		public static string GetApplicationPathFromProcess(Process proc)
		{
			try
			{
				bool flag = SystemUtils.IsOSWinXP();
				if (!flag)
				{
					UIntPtr dwProcessId = new UIntPtr((uint)((proc != null) ? new int?(proc.Id) : null).Value);
					return GetProcessExecutionPath.GetExecutablePathAboveVista(dwProcessId);
				}
				bool flag2 = SystemUtils.IsAdministrator();
				if (flag2)
				{
					return (proc != null) ? proc.MainModule.FileName.ToString(CultureInfo.InvariantCulture) : null;
				}
			}
			catch
			{
			}
			return string.Empty;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003210 File Offset: 0x00001410
		public static string GetExecutablePathAboveVista(UIntPtr dwProcessId)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			IntPtr intPtr = GetProcessExecutionPath.OpenProcess(4096, false, dwProcessId);
			bool flag = intPtr != IntPtr.Zero;
			if (flag)
			{
				try
				{
					int capacity = stringBuilder.Capacity;
					bool flag2 = GetProcessExecutionPath.QueryFullProcessImageName(intPtr, 0, stringBuilder, out capacity);
					if (flag2)
					{
						return stringBuilder.ToString();
					}
				}
				finally
				{
					GetProcessExecutionPath.CloseHandle(intPtr);
				}
			}
			return string.Empty;
		}
	}
}
