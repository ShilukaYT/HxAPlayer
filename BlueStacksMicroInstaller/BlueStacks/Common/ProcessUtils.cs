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
	// Token: 0x0200005F RID: 95
	public static class ProcessUtils
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00006773 File Offset: 0x00004973
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

		// Token: 0x0600010D RID: 269 RVA: 0x000067B0 File Offset: 0x000049B0
		public static bool FindProcessByName(string name)
		{
			Process[] processesByName = Process.GetProcessesByName(name);
			return processesByName.Length != 0;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000067D0 File Offset: 0x000049D0
		public static void KillProcessByName(string name)
		{
			Process[] processesByName = Process.GetProcessesByName(name);
			Process[] array = processesByName;
			int i = 0;
			while (i < array.Length)
			{
				Process process = array[i];
				try
				{
					Logger.Debug("Attempting to kill: {0}", new object[]
					{
						process.ProcessName
					});
					process.Kill();
					bool flag = !process.WaitForExit(5000);
					if (flag)
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
				IL_86:
				i++;
				continue;
				goto IL_86;
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00006880 File Offset: 0x00004A80
		public static void KillProcessesByName(string[] nameList)
		{
			bool flag = nameList != null;
			if (flag)
			{
				foreach (string name in nameList)
				{
					ProcessUtils.KillProcessByName(name);
				}
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000068B8 File Offset: 0x00004AB8
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

		// Token: 0x06000111 RID: 273 RVA: 0x0000692C File Offset: 0x00004B2C
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

		// Token: 0x06000112 RID: 274 RVA: 0x00006964 File Offset: 0x00004B64
		public static bool IsLockInUse(string lockName)
		{
			return ProcessUtils.IsLockInUse(lockName, true);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006980 File Offset: 0x00004B80
		public static bool IsLockInUse(string lockName, bool printLog)
		{
			Mutex mutex;
			bool flag = ProcessUtils.CheckAlreadyRunningAndTakeLock(lockName, out mutex);
			bool flag2 = flag;
			bool result;
			if (flag2)
			{
				if (printLog)
				{
					Logger.Info(lockName + " running.");
				}
				result = true;
			}
			else
			{
				bool flag3 = mutex != null;
				if (flag3)
				{
					mutex.Close();
					mutex = null;
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000069D8 File Offset: 0x00004BD8
		public static bool IsAnyInstallerProcesRunning(out string runningProcName)
		{
			runningProcName = null;
			foreach (string text in ProcessUtils.LockToProcessMap.Keys)
			{
				bool flag = ProcessUtils.IsAlreadyRunning(text);
				if (flag)
				{
					runningProcName = ProcessUtils.LockToProcessMap[text];
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006A54 File Offset: 0x00004C54
		public static bool IsAlreadyRunning(string name)
		{
			Mutex mutex;
			bool flag = !ProcessUtils.CheckAlreadyRunningAndTakeLock(name, out mutex);
			bool result;
			if (flag)
			{
				bool flag2 = mutex != null;
				if (flag2)
				{
					mutex.Close();
				}
				result = false;
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006A8C File Offset: 0x00004C8C
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
			bool flag2 = !flag;
			if (flag2)
			{
				lck.Close();
				lck = null;
			}
			return !flag;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006B30 File Offset: 0x00004D30
		public static void KillProcessByNameIgnoreDirectory(string name, string IgnoreDirectory)
		{
			Process[] processesByName = Process.GetProcessesByName(name);
			foreach (Process process in processesByName)
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
				bool flag = text.Equals(IgnoreDirectory, StringComparison.CurrentCultureIgnoreCase);
				if (flag)
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
						goto IL_145;
					}
					bool flag2 = !process.WaitForExit(5000);
					if (flag2)
					{
						Logger.Info("Timeout waiting for process to die");
					}
				}
				IL_145:;
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006CB8 File Offset: 0x00004EB8
		public static void LogParentProcessDetails()
		{
			try
			{
				Process currentProcessParent = ProcessDetails.CurrentProcessParent;
				bool flag = currentProcessParent == null;
				if (flag)
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

		// Token: 0x06000119 RID: 281 RVA: 0x00006D40 File Offset: 0x00004F40
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

		// Token: 0x0600011A RID: 282 RVA: 0x00006D98 File Offset: 0x00004F98
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

		// Token: 0x0600011B RID: 283 RVA: 0x00006E44 File Offset: 0x00005044
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
			ProcessUtils.IShellFolderViewDual shellFolderViewDual = (ProcessUtils.IShellFolderViewDual)shellBrowser.QueryActiveShellView().GetItemObject(0U, ref guid2);
			ProcessUtils.IShellDispatch2 shellDispatch = (ProcessUtils.IShellDispatch2)shellFolderViewDual.Application;
			shellDispatch.ShellExecute(process, args, currentDirectory, string.Empty, 1);
		}

		// Token: 0x040001C6 RID: 454
		private const int CSIDL_Desktop = 0;

		// Token: 0x040001C7 RID: 455
		private const int SWC_DESKTOP = 8;

		// Token: 0x040001C8 RID: 456
		private const int SWFO_NEEDDISPATCH = 1;

		// Token: 0x040001C9 RID: 457
		private const int SW_SHOWNORMAL = 1;

		// Token: 0x040001CA RID: 458
		private const int SVGIO_BACKGROUND = 0;

		// Token: 0x040001CB RID: 459
		private static readonly Guid SID_STopLevelBrowser = new Guid("4C96BE40-915C-11CF-99D3-00AA004AE837");

		// Token: 0x020000C7 RID: 199
		[Guid("9BA05972-F6A8-11CF-A442-00A0C90A8F39")]
		[ClassInterface(ClassInterfaceType.None)]
		[ComImport]
		private class CShellWindows
		{
			// Token: 0x0600041F RID: 1055
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern CShellWindows();
		}

		// Token: 0x020000C8 RID: 200
		[Guid("85CB6900-4D95-11CF-960C-0080C7F4EE85")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[ComImport]
		private interface IShellWindows
		{
			// Token: 0x06000420 RID: 1056
			[return: MarshalAs(UnmanagedType.IDispatch)]
			object FindWindowSW([MarshalAs(UnmanagedType.Struct)] ref object pvarloc, [MarshalAs(UnmanagedType.Struct)] ref object pvarlocRoot, int swClass, out int pHWND, int swfwOptions);
		}

		// Token: 0x020000C9 RID: 201
		[Guid("6d5140c1-7436-11ce-8034-00aa006009fa")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		private interface IServiceProvider
		{
			// Token: 0x06000421 RID: 1057
			[return: MarshalAs(UnmanagedType.Interface)]
			object QueryService(ref Guid guidService, ref Guid riid);
		}

		// Token: 0x020000CA RID: 202
		[Guid("000214E2-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		private interface IShellBrowser
		{
			// Token: 0x06000422 RID: 1058
			void VTableGap01();

			// Token: 0x06000423 RID: 1059
			void VTableGap02();

			// Token: 0x06000424 RID: 1060
			void VTableGap03();

			// Token: 0x06000425 RID: 1061
			void VTableGap04();

			// Token: 0x06000426 RID: 1062
			void VTableGap05();

			// Token: 0x06000427 RID: 1063
			void VTableGap06();

			// Token: 0x06000428 RID: 1064
			void VTableGap07();

			// Token: 0x06000429 RID: 1065
			void VTableGap08();

			// Token: 0x0600042A RID: 1066
			void VTableGap09();

			// Token: 0x0600042B RID: 1067
			void VTableGap10();

			// Token: 0x0600042C RID: 1068
			void VTableGap11();

			// Token: 0x0600042D RID: 1069
			void VTableGap12();

			// Token: 0x0600042E RID: 1070
			ProcessUtils.IShellView QueryActiveShellView();
		}

		// Token: 0x020000CB RID: 203
		[Guid("000214E3-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		private interface IShellView
		{
			// Token: 0x0600042F RID: 1071
			void VTableGap01();

			// Token: 0x06000430 RID: 1072
			void VTableGap02();

			// Token: 0x06000431 RID: 1073
			void VTableGap03();

			// Token: 0x06000432 RID: 1074
			void VTableGap04();

			// Token: 0x06000433 RID: 1075
			void VTableGap05();

			// Token: 0x06000434 RID: 1076
			void VTableGap06();

			// Token: 0x06000435 RID: 1077
			void VTableGap07();

			// Token: 0x06000436 RID: 1078
			void VTableGap08();

			// Token: 0x06000437 RID: 1079
			void VTableGap09();

			// Token: 0x06000438 RID: 1080
			void VTableGap10();

			// Token: 0x06000439 RID: 1081
			void VTableGap11();

			// Token: 0x0600043A RID: 1082
			void VTableGap12();

			// Token: 0x0600043B RID: 1083
			[return: MarshalAs(UnmanagedType.Interface)]
			object GetItemObject(uint aspectOfView, ref Guid riid);
		}

		// Token: 0x020000CC RID: 204
		[Guid("00020400-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[ComImport]
		private interface IDispatch
		{
		}

		// Token: 0x020000CD RID: 205
		[Guid("E7A1AF80-4D96-11CF-960C-0080C7F4EE85")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[ComImport]
		private interface IShellFolderViewDual
		{
			// Token: 0x170000C4 RID: 196
			// (get) Token: 0x0600043C RID: 1084
			object Application { [return: MarshalAs(UnmanagedType.IDispatch)] get; }
		}

		// Token: 0x020000CE RID: 206
		[Guid("A4C6892C-3BA9-11D2-9DEA-00C04FB16162")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[ComImport]
		public interface IShellDispatch2
		{
			// Token: 0x0600043D RID: 1085
			void ShellExecute([MarshalAs(UnmanagedType.BStr)] string File, [MarshalAs(UnmanagedType.Struct)] object vArgs, [MarshalAs(UnmanagedType.Struct)] object vDir, [MarshalAs(UnmanagedType.Struct)] object vOperation, [MarshalAs(UnmanagedType.Struct)] object vShow);
		}
	}
}
