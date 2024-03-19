using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

namespace BlueStacks.Common
{
	// Token: 0x0200005E RID: 94
	public static class ProcessDetails
	{
		// Token: 0x06000100 RID: 256
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CreateToolhelp32Snapshot([In] uint dwFlags, [In] uint th32ProcessID);

		// Token: 0x06000101 RID: 257
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool Process32First([In] IntPtr hSnapshot, ref ProcessDetails.PROCESSENTRY32 lppe);

		// Token: 0x06000102 RID: 258
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool Process32Next([In] IntPtr hSnapshot, ref ProcessDetails.PROCESSENTRY32 lppe);

		// Token: 0x06000103 RID: 259
		[DllImport("kernel32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseHandle([In] IntPtr hObject);

		// Token: 0x06000104 RID: 260 RVA: 0x0000659C File Offset: 0x0000479C
		public static int? GetParentProcessId(int pid)
		{
			Process parentProcess = ProcessDetails.GetParentProcess(pid);
			bool flag = parentProcess == null;
			int? result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = new int?(parentProcess.Id);
			}
			return result;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000065D8 File Offset: 0x000047D8
		public static Process GetParentProcess(int pid)
		{
			Process result = null;
			int id = Process.GetCurrentProcess().Id;
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				ProcessDetails.PROCESSENTRY32 processentry = new ProcessDetails.PROCESSENTRY32
				{
					dwSize = (uint)Marshal.SizeOf(typeof(ProcessDetails.PROCESSENTRY32))
				};
				intPtr = ProcessDetails.CreateToolhelp32Snapshot(2U, 0U);
				bool flag = ProcessDetails.Process32First(intPtr, ref processentry);
				if (!flag)
				{
					throw new ApplicationException(string.Format(CultureInfo.InvariantCulture, "Failed with win32 error code {0}", new object[]
					{
						Marshal.GetLastWin32Error()
					}));
				}
				for (;;)
				{
					bool flag2 = (long)pid == (long)((ulong)processentry.th32ProcessID);
					if (flag2)
					{
						break;
					}
					if (!ProcessDetails.Process32Next(intPtr, ref processentry))
					{
						goto IL_7D;
					}
				}
				result = Process.GetProcessById((int)processentry.th32ParentProcessID);
				IL_7D:;
			}
			catch (Exception ex)
			{
				Logger.Error("Can't get the process. Ex: {0}", new object[]
				{
					ex.ToString()
				});
			}
			finally
			{
				ProcessDetails.CloseHandle(intPtr);
			}
			return result;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000066E4 File Offset: 0x000048E4
		public static string CurrentProcessParentFileName
		{
			get
			{
				return Path.GetFileName(ProcessDetails.CurrentProcessParentFullPath);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000066F0 File Offset: 0x000048F0
		public static string CurrentProcessParentFullPath
		{
			get
			{
				return ProcessDetails.CurrentProcessParent.MainModule.FileName;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00006701 File Offset: 0x00004901
		public static Process CurrentProcessParent
		{
			get
			{
				return ProcessDetails.GetParentProcess(Process.GetCurrentProcess().Id);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006712 File Offset: 0x00004912
		public static int? CurrentProcessParentId
		{
			get
			{
				return ProcessDetails.GetParentProcessId(Process.GetCurrentProcess().Id);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00006723 File Offset: 0x00004923
		public static int CurrentProcessId
		{
			get
			{
				return Process.GetCurrentProcess().Id;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00006730 File Offset: 0x00004930
		public static int? GetNthParentPid(int pid, int order)
		{
			int? parentProcessId = new int?(pid);
			while (order > 0 && parentProcessId != null)
			{
				parentProcessId = ProcessDetails.GetParentProcessId(parentProcessId.Value);
				order--;
			}
			return parentProcessId;
		}

		// Token: 0x020000C5 RID: 197
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private struct PROCESSENTRY32
		{
			// Token: 0x04000788 RID: 1928
			private const int MAX_PATH = 260;

			// Token: 0x04000789 RID: 1929
			internal uint dwSize;

			// Token: 0x0400078A RID: 1930
			internal uint cntUsage;

			// Token: 0x0400078B RID: 1931
			internal uint th32ProcessID;

			// Token: 0x0400078C RID: 1932
			internal IntPtr th32DefaultHeapID;

			// Token: 0x0400078D RID: 1933
			internal uint th32ModuleID;

			// Token: 0x0400078E RID: 1934
			internal uint cntThreads;

			// Token: 0x0400078F RID: 1935
			internal uint th32ParentProcessID;

			// Token: 0x04000790 RID: 1936
			internal int pcPriClassBase;

			// Token: 0x04000791 RID: 1937
			internal uint dwFlags;

			// Token: 0x04000792 RID: 1938
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string szExeFile;
		}

		// Token: 0x020000C6 RID: 198
		[Flags]
		private enum SnapshotFlags : uint
		{
			// Token: 0x04000794 RID: 1940
			HeapList = 1U,
			// Token: 0x04000795 RID: 1941
			Process = 2U,
			// Token: 0x04000796 RID: 1942
			Thread = 4U,
			// Token: 0x04000797 RID: 1943
			Module = 8U,
			// Token: 0x04000798 RID: 1944
			Module32 = 16U,
			// Token: 0x04000799 RID: 1945
			Inherit = 2147483648U,
			// Token: 0x0400079A RID: 1946
			All = 31U,
			// Token: 0x0400079B RID: 1947
			NoHeaps = 1073741824U
		}
	}
}
