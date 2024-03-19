using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace BlueStacks.Common
{
	// Token: 0x02000195 RID: 405
	public static class GetProcessExecutionPath
	{
		// Token: 0x06000F1A RID: 3866
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool QueryFullProcessImageName(IntPtr hwnd, int flags, [Out] StringBuilder buffer, out int size);

		// Token: 0x06000F1B RID: 3867
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr OpenProcess(int flags, bool handle, UIntPtr procId);

		// Token: 0x06000F1C RID: 3868
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CloseHandle(IntPtr handle);

		// Token: 0x06000F1D RID: 3869 RVA: 0x0003B9A4 File Offset: 0x00039BA4
		public static List<string> GetApplicationPath(Process[] procList)
		{
			List<string> list = new List<string>();
			if (procList != null)
			{
				foreach (Process proc in procList)
				{
					try
					{
						string applicationPathFromProcess = GetProcessExecutionPath.GetApplicationPathFromProcess(proc);
						if (!string.IsNullOrEmpty(applicationPathFromProcess))
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

		// Token: 0x06000F1E RID: 3870 RVA: 0x0003BA00 File Offset: 0x00039C00
		public static string GetApplicationPathFromProcess(Process proc)
		{
			try
			{
				if (!SystemUtils.IsOSWinXP())
				{
					return GetProcessExecutionPath.GetExecutablePathAboveVista(new UIntPtr((uint)((proc != null) ? new int?(proc.Id) : null).Value));
				}
				if (SystemUtils.IsAdministrator())
				{
					return (proc != null) ? proc.MainModule.FileName.ToString(CultureInfo.InvariantCulture) : null;
				}
			}
			catch
			{
			}
			return string.Empty;
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0003BA84 File Offset: 0x00039C84
		public static string GetExecutablePathAboveVista(UIntPtr dwProcessId)
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			IntPtr intPtr = GetProcessExecutionPath.OpenProcess(4096, false, dwProcessId);
			if (intPtr != IntPtr.Zero)
			{
				try
				{
					int capacity = stringBuilder.Capacity;
					if (GetProcessExecutionPath.QueryFullProcessImageName(intPtr, 0, stringBuilder, out capacity))
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
