using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

namespace BlueStacks.Common
{
	// Token: 0x020001FA RID: 506
	public static class ProcessDetails
	{
		// Token: 0x0600101B RID: 4123
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CreateToolhelp32Snapshot([In] uint dwFlags, [In] uint th32ProcessID);

		// Token: 0x0600101C RID: 4124
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool Process32First([In] IntPtr hSnapshot, ref ProcessDetails.PROCESSENTRY32 lppe);

		// Token: 0x0600101D RID: 4125
		[DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool Process32Next([In] IntPtr hSnapshot, ref ProcessDetails.PROCESSENTRY32 lppe);

		// Token: 0x0600101E RID: 4126
		[DllImport("kernel32", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseHandle([In] IntPtr hObject);

		// Token: 0x0600101F RID: 4127 RVA: 0x0003DF20 File Offset: 0x0003C120
		public static int? GetParentProcessId(int pid)
		{
			Process parentProcess = ProcessDetails.GetParentProcess(pid);
			if (parentProcess == null)
			{
				return null;
			}
			return new int?(parentProcess.Id);
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0003DF4C File Offset: 0x0003C14C
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
				if (!ProcessDetails.Process32First(intPtr, ref processentry))
				{
					throw new ApplicationException(string.Format(CultureInfo.InvariantCulture, "Failed with win32 error code {0}", new object[]
					{
						Marshal.GetLastWin32Error()
					}));
				}
				while ((long)pid != (long)((ulong)processentry.th32ProcessID))
				{
					if (!ProcessDetails.Process32Next(intPtr, ref processentry))
					{
						return result;
					}
				}
				result = Process.GetProcessById((int)processentry.th32ParentProcessID);
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

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x0000DF16 File Offset: 0x0000C116
		public static string CurrentProcessParentFileName
		{
			get
			{
				return Path.GetFileName(ProcessDetails.CurrentProcessParentFullPath);
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x0000DF22 File Offset: 0x0000C122
		public static string CurrentProcessParentFullPath
		{
			get
			{
				return ProcessDetails.CurrentProcessParent.MainModule.FileName;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x0000DF33 File Offset: 0x0000C133
		public static Process CurrentProcessParent
		{
			get
			{
				return ProcessDetails.GetParentProcess(Process.GetCurrentProcess().Id);
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0000DF44 File Offset: 0x0000C144
		public static int? CurrentProcessParentId
		{
			get
			{
				return ProcessDetails.GetParentProcessId(Process.GetCurrentProcess().Id);
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x0000DF55 File Offset: 0x0000C155
		public static int CurrentProcessId
		{
			get
			{
				return Process.GetCurrentProcess().Id;
			}
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0003E030 File Offset: 0x0003C230
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

		// Token: 0x020001FB RID: 507
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private struct PROCESSENTRY32
		{
			// Token: 0x04000AAE RID: 2734
			private const int MAX_PATH = 260;

			// Token: 0x04000AAF RID: 2735
			internal uint dwSize;

			// Token: 0x04000AB0 RID: 2736
			internal uint cntUsage;

			// Token: 0x04000AB1 RID: 2737
			internal uint th32ProcessID;

			// Token: 0x04000AB2 RID: 2738
			internal IntPtr th32DefaultHeapID;

			// Token: 0x04000AB3 RID: 2739
			internal uint th32ModuleID;

			// Token: 0x04000AB4 RID: 2740
			internal uint cntThreads;

			// Token: 0x04000AB5 RID: 2741
			internal uint th32ParentProcessID;

			// Token: 0x04000AB6 RID: 2742
			internal int pcPriClassBase;

			// Token: 0x04000AB7 RID: 2743
			internal uint dwFlags;

			// Token: 0x04000AB8 RID: 2744
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			internal string szExeFile;
		}

		// Token: 0x020001FC RID: 508
		[Flags]
		private enum SnapshotFlags : uint
		{
			// Token: 0x04000ABA RID: 2746
			HeapList = 1U,
			// Token: 0x04000ABB RID: 2747
			Process = 2U,
			// Token: 0x04000ABC RID: 2748
			Thread = 4U,
			// Token: 0x04000ABD RID: 2749
			Module = 8U,
			// Token: 0x04000ABE RID: 2750
			Module32 = 16U,
			// Token: 0x04000ABF RID: 2751
			Inherit = 2147483648U,
			// Token: 0x04000AC0 RID: 2752
			All = 31U,
			// Token: 0x04000AC1 RID: 2753
			NoHeaps = 1073741824U
		}
	}
}
