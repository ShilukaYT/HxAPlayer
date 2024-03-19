using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace BlueStacks.Common
{
	// Token: 0x020001FD RID: 509
	public static class ProcessUtils
	{
		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001027 RID: 4135 RVA: 0x0000DF61 File Offset: 0x0000C161
		public static Dictionary<string, string> LockToProcessMap
		{
			get
			{
				return new Dictionary<string, string>
				{
					{
						"Global\\BlueStacks_Installer_Lockbgp",
						"Installer"
					},
					{
						"Global\\BlueStacks_MicroInstaller_Lockbgp",
						"MicroInstaller"
					},
					{
						"Global\\BlueStacks_Uninstaller_Lockbgp",
						"Uninstaller"
					}
				};
			}
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0000DF98 File Offset: 0x0000C198
		public static bool FindProcessByName(string name)
		{
			return Process.GetProcessesByName(name).Length != 0;
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0003E068 File Offset: 0x0003C268
		public static void KillProcessByName(string name)
		{
			foreach (Process process in Process.GetProcessesByName(name))
			{
				try
				{
					Logger.Debug("Attempting to kill: {0}", new object[]
					{
						process.ProcessName
					});
					process.Kill();
					if (!process.WaitForExit(5000))
					{
						Logger.Info("Timeout waiting for process {0} to die", new object[]
						{
							process.ProcessName
						});
					}
				}
				catch (Exception ex)
				{
					Logger.Error("Exception in killing process " + ex.Message);
				}
			}
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0003E100 File Offset: 0x0003C300
		public static void KillProcessesByName(string[] nameList)
		{
			if (nameList != null)
			{
				for (int i = 0; i < nameList.Length; i++)
				{
					ProcessUtils.KillProcessByName(nameList[i]);
				}
			}
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0003E128 File Offset: 0x0003C328
		public static Process GetProcessObject(string exePath, string args, bool isAdmin = false)
		{
			Process process = new Process();
			process.StartInfo.Arguments = args;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.FileName = exePath;
			if (isAdmin)
			{
				process.StartInfo.Verb = "runas";
				process.StartInfo.UseShellExecute = true;
			}
			return process;
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0003E18C File Offset: 0x0003C38C
		public static bool IsProcessAlive(int pid)
		{
			bool result = false;
			try
			{
				Process.GetProcessById(pid);
				result = true;
			}
			catch (ArgumentException)
			{
			}
			return result;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0000DFA4 File Offset: 0x0000C1A4
		public static bool IsLockInUse(string lockName)
		{
			return ProcessUtils.IsLockInUse(lockName, true);
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0003E1BC File Offset: 0x0003C3BC
		public static bool IsLockInUse(string lockName, bool printLog)
		{
			Mutex mutex;
			if (ProcessUtils.CheckAlreadyRunningAndTakeLock(lockName, out mutex))
			{
				if (printLog)
				{
					Logger.Info(lockName + " running.");
				}
				return true;
			}
			if (mutex != null)
			{
				mutex.Close();
				mutex = null;
			}
			return false;
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0003E1F4 File Offset: 0x0003C3F4
		public static bool IsAnyInstallerProcesRunning(out string runningProcName)
		{
			runningProcName = null;
			foreach (string text in ProcessUtils.LockToProcessMap.Keys)
			{
				if (ProcessUtils.IsAlreadyRunning(text))
				{
					runningProcName = ProcessUtils.LockToProcessMap[text];
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0003E264 File Offset: 0x0003C464
		public static bool IsAlreadyRunning(string name)
		{
			Mutex mutex;
			if (!ProcessUtils.CheckAlreadyRunningAndTakeLock(name, out mutex))
			{
				if (mutex != null)
				{
					mutex.Close();
				}
				return false;
			}
			return true;
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0003E288 File Offset: 0x0003C488
		public static bool CheckAlreadyRunningAndTakeLock(string name, out Mutex lck)
		{
			bool flag;
			try
			{
				lck = new Mutex(true, name, ref flag);
			}
			catch (AbandonedMutexException ex)
			{
				lck = null;
				Logger.Warning("Abandoned mutex : " + name + ".  " + ex.ToString());
				return false;
			}
			catch (UnauthorizedAccessException ex2)
			{
				lck = null;
				Logger.Warning("UnauthorisedAccess on mutex : " + name + ".  " + ex2.ToString());
				return true;
			}
			if (!flag)
			{
				lck.Close();
				lck = null;
			}
			return !flag;
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0003E318 File Offset: 0x0003C518
		public static void KillProcessByNameIgnoreDirectory(string name, string IgnoreDirectory)
		{
			foreach (Process process in Process.GetProcessesByName(name))
			{
				string path = "";
				try
				{
					path = process.MainModule.FileName;
				}
				catch (Win32Exception ex)
				{
					Logger.Error("Got the excpetion {0}", new object[]
					{
						ex.Message
					});
					Logger.Info("Giving the exit code to start as admin");
					Environment.Exit(2);
				}
				catch (Exception ex2)
				{
					Logger.Error("Got exception: err {0}", new object[]
					{
						ex2.ToString()
					});
				}
				string text = Directory.GetParent(path).ToString();
				Logger.Debug("The Process Dir is {0}", new object[]
				{
					text
				});
				if (text.Equals(IgnoreDirectory, StringComparison.CurrentCultureIgnoreCase))
				{
					Logger.Debug("Process:{0} not killed since the process sir:{1} and ignore dir:{2} are the same", new object[]
					{
						process.ProcessName,
						text,
						IgnoreDirectory
					});
				}
				else
				{
					Logger.Info("Killing PID " + process.Id.ToString() + " -> " + process.ProcessName);
					try
					{
						process.Kill();
					}
					catch (Exception ex3)
					{
						Logger.Error(ex3.ToString());
						goto IL_117;
					}
					if (!process.WaitForExit(5000))
					{
						Logger.Info("Timeout waiting for process to die");
					}
				}
				IL_117:;
			}
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0003E474 File Offset: 0x0003C674
		public static void LogParentProcessDetails()
		{
			try
			{
				Process currentProcessParent = ProcessDetails.CurrentProcessParent;
				if (currentProcessParent == null)
				{
					Logger.Info("Unable to retrieve information about invoking process");
				}
				else
				{
					Logger.Info("Invoking Process Details: (Name: {0}, Pid: {1})", new object[]
					{
						currentProcessParent.ProcessName,
						currentProcessParent.Id
					});
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Unable to get parent process details, Err: {0}", new object[]
				{
					ex.ToString()
				});
			}
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0003E4EC File Offset: 0x0003C6EC
		public static void LogProcessContextDetails()
		{
			Logger.Info("PID {0}, CLR version {0}", new object[]
			{
				Process.GetCurrentProcess().Id,
				Environment.Version
			});
			Logger.Info("IsAdministrator: {0}", new object[]
			{
				SystemUtils.IsAdministrator()
			});
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0003E540 File Offset: 0x0003C740
		public static Process StartExe(string exePath, string args, bool isAdmin = false)
		{
			Process process = new Process();
			process.StartInfo.Arguments = args;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.FileName = exePath;
			if (isAdmin)
			{
				process.StartInfo.Verb = "runas";
				process.StartInfo.UseShellExecute = true;
			}
			try
			{
				Logger.Info("Utils: Starting Process: {0} with args: {1}", new object[]
				{
					exePath,
					args
				});
			}
			catch
			{
			}
			process.Start();
			return process;
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0003E5D8 File Offset: 0x0003C7D8
		public static void ExecuteProcessUnElevated(string process, string args, string currentDirectory = "")
		{
			ProcessUtils.IShellWindows shellWindows = (ProcessUtils.IShellWindows)new ProcessUtils.CShellWindows();
			object obj = 0;
			object obj2 = new object();
			int num;
			ProcessUtils.IServiceProvider serviceProvider = (ProcessUtils.IServiceProvider)shellWindows.FindWindowSW(ref obj, ref obj2, 8, out num, 1);
			Guid sid_STopLevelBrowser = ProcessUtils.SID_STopLevelBrowser;
			Guid guid = typeof(ProcessUtils.IShellBrowser).GUID;
			ProcessUtils.IShellBrowser shellBrowser = (ProcessUtils.IShellBrowser)serviceProvider.QueryService(ref sid_STopLevelBrowser, ref guid);
			Guid guid2 = typeof(ProcessUtils.IDispatch).GUID;
			((ProcessUtils.IShellDispatch2)((ProcessUtils.IShellFolderViewDual)shellBrowser.QueryActiveShellView().GetItemObject(0U, ref guid2)).Application).ShellExecute(process, args, currentDirectory, string.Empty, 1);
		}

		// Token: 0x04000AC2 RID: 2754
		private const int CSIDL_Desktop = 0;

		// Token: 0x04000AC3 RID: 2755
		private const int SWC_DESKTOP = 8;

		// Token: 0x04000AC4 RID: 2756
		private const int SWFO_NEEDDISPATCH = 1;

		// Token: 0x04000AC5 RID: 2757
		private const int SW_SHOWNORMAL = 1;

		// Token: 0x04000AC6 RID: 2758
		private const int SVGIO_BACKGROUND = 0;

		// Token: 0x04000AC7 RID: 2759
		private static readonly Guid SID_STopLevelBrowser = new Guid("4C96BE40-915C-11CF-99D3-00AA004AE837");

		// Token: 0x020001FE RID: 510
		[Guid("9BA05972-F6A8-11CF-A442-00A0C90A8F39")]
		[ClassInterface(ClassInterfaceType.None)]
		[ComImport]
		private class CShellWindows
		{
			// Token: 0x06001038 RID: 4152
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern CShellWindows();
		}

		// Token: 0x020001FF RID: 511
		[Guid("85CB6900-4D95-11CF-960C-0080C7F4EE85")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[ComImport]
		private interface IShellWindows
		{
			// Token: 0x06001039 RID: 4153
			[return: MarshalAs(UnmanagedType.IDispatch)]
			object FindWindowSW([MarshalAs(UnmanagedType.Struct)] ref object pvarloc, [MarshalAs(UnmanagedType.Struct)] ref object pvarlocRoot, int swClass, out int pHWND, int swfwOptions);
		}

		// Token: 0x02000200 RID: 512
		[Guid("6d5140c1-7436-11ce-8034-00aa006009fa")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		private interface IServiceProvider
		{
			// Token: 0x0600103A RID: 4154
			[return: MarshalAs(UnmanagedType.Interface)]
			object QueryService(ref Guid guidService, ref Guid riid);
		}

		// Token: 0x02000201 RID: 513
		[Guid("000214E2-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		private interface IShellBrowser
		{
			// Token: 0x0600103B RID: 4155
			void VTableGap01();

			// Token: 0x0600103C RID: 4156
			void VTableGap02();

			// Token: 0x0600103D RID: 4157
			void VTableGap03();

			// Token: 0x0600103E RID: 4158
			void VTableGap04();

			// Token: 0x0600103F RID: 4159
			void VTableGap05();

			// Token: 0x06001040 RID: 4160
			void VTableGap06();

			// Token: 0x06001041 RID: 4161
			void VTableGap07();

			// Token: 0x06001042 RID: 4162
			void VTableGap08();

			// Token: 0x06001043 RID: 4163
			void VTableGap09();

			// Token: 0x06001044 RID: 4164
			void VTableGap10();

			// Token: 0x06001045 RID: 4165
			void VTableGap11();

			// Token: 0x06001046 RID: 4166
			void VTableGap12();

			// Token: 0x06001047 RID: 4167
			ProcessUtils.IShellView QueryActiveShellView();
		}

		// Token: 0x02000202 RID: 514
		[Guid("000214E3-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		private interface IShellView
		{
			// Token: 0x06001048 RID: 4168
			void VTableGap01();

			// Token: 0x06001049 RID: 4169
			void VTableGap02();

			// Token: 0x0600104A RID: 4170
			void VTableGap03();

			// Token: 0x0600104B RID: 4171
			void VTableGap04();

			// Token: 0x0600104C RID: 4172
			void VTableGap05();

			// Token: 0x0600104D RID: 4173
			void VTableGap06();

			// Token: 0x0600104E RID: 4174
			void VTableGap07();

			// Token: 0x0600104F RID: 4175
			void VTableGap08();

			// Token: 0x06001050 RID: 4176
			void VTableGap09();

			// Token: 0x06001051 RID: 4177
			void VTableGap10();

			// Token: 0x06001052 RID: 4178
			void VTableGap11();

			// Token: 0x06001053 RID: 4179
			void VTableGap12();

			// Token: 0x06001054 RID: 4180
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetItemObject(uint aspectOfView, ref Guid riid);
		}

		// Token: 0x02000203 RID: 515
		[Guid("00020400-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[ComImport]
		private interface IDispatch
		{
		}

		// Token: 0x02000204 RID: 516
		[Guid("E7A1AF80-4D96-11CF-960C-0080C7F4EE85")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[ComImport]
		private interface IShellFolderViewDual
		{
			// Token: 0x1700046D RID: 1133
			// (get) Token: 0x06001055 RID: 4181
			object Application { [return: MarshalAs(UnmanagedType.IDispatch)] get; }
		}

		// Token: 0x02000205 RID: 517
		[Guid("A4C6892C-3BA9-11D2-9DEA-00C04FB16162")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[ComImport]
		public interface IShellDispatch2
		{
			// Token: 0x06001056 RID: 4182
			void ShellExecute([MarshalAs(UnmanagedType.BStr)] string File, [MarshalAs(UnmanagedType.Struct)] object vArgs, [MarshalAs(UnmanagedType.Struct)] object vDir, [MarshalAs(UnmanagedType.Struct)] object vOperation, [MarshalAs(UnmanagedType.Struct)] object vShow);
		}
	}
}
