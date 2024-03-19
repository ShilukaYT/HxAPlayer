using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x020000CD RID: 205
	public static class MemoryManager
	{
		// Token: 0x060004FC RID: 1276
		[DllImport("KERNEL32.DLL", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetProcessWorkingSetSize", SetLastError = true)]
		private static extern bool SetProcessWorkingSetSize32(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

		// Token: 0x060004FD RID: 1277
		[DllImport("KERNEL32.DLL", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetProcessWorkingSetSize", SetLastError = true)]
		private static extern bool SetProcessWorkingSetSize64(IntPtr pProcess, long dwMinimumWorkingSetSize, long dwMaximumWorkingSetSize);

		// Token: 0x060004FE RID: 1278
		[DllImport("psapi.dll")]
		private static extern int EmptyWorkingSet(IntPtr hwProc);

		// Token: 0x060004FF RID: 1279 RVA: 0x00019788 File Offset: 0x00017988
		public static void TrimMemory(bool isForceMemoryTrim = false)
		{
			if (!isForceMemoryTrim)
			{
				Thread thread = MemoryManager.trimMemoryThread;
				if (((thread != null) ? new bool?(thread.IsAlive) : null).GetValueOrDefault() || !RegistryManager.Instance.EnableMemoryTrim)
				{
					return;
				}
			}
			MemoryManager.trimMemoryThread = new Thread(delegate()
			{
				int millisecondsTimeout = RegistryManager.Instance.TrimMemoryDuration * 1000;
				Logger.Info("Setting trim memory duration to: " + millisecondsTimeout.ToString());
				for (;;)
				{
					Thread.Sleep(millisecondsTimeout);
					try
					{
						if (isForceMemoryTrim || RegistryManager.Instance.EnableMemoryTrim)
						{
							GC.Collect(2, GCCollectionMode.Forced);
							GC.WaitForPendingFinalizers();
							if (Environment.OSVersion.Platform == PlatformID.Win32NT)
							{
								if (IntPtr.Size == 8)
								{
									MemoryManager.SetProcessWorkingSetSize64(Process.GetCurrentProcess().Handle, -1L, -1L);
								}
								else
								{
									MemoryManager.SetProcessWorkingSetSize32(Process.GetCurrentProcess().Handle, -1, -1);
								}
							}
							using (Process currentProcess = Process.GetCurrentProcess())
							{
								Logger.Debug("Trimming memory");
								MemoryManager.EmptyWorkingSet(currentProcess.Handle);
							}
							continue;
						}
					}
					catch (Exception ex)
					{
						Logger.Error("Exception while trimming memory ex: " + ex.ToString());
						continue;
					}
					break;
				}
			})
			{
				IsBackground = true
			};
			MemoryManager.trimMemoryThread.Start();
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00019808 File Offset: 0x00017A08
		public static void CheckAndTrimAndroidMemory()
		{
			Thread thread = MemoryManager.androidtrimMemoryThread;
			if (!((thread != null) ? new bool?(thread.IsAlive) : null).GetValueOrDefault() && RegistryManager.Instance.EnableMemoryTrim)
			{
				int timerInterval = 60000;
				int triggerThreshold = 700;
				timerInterval = RegistryManager.Instance.DefaultGuest.TriggerMemoryTrimTimerInterval;
				triggerThreshold = RegistryManager.Instance.DefaultGuest.TriggerMemoryTrimThreshold;
				MemoryManager.androidtrimMemoryThread = new Thread(delegate()
				{
					for (;;)
					{
						Thread.Sleep(timerInterval);
						if (!RegistryManager.Instance.EnableMemoryTrim)
						{
							break;
						}
						long workingSet = Process.GetCurrentProcess().WorkingSet64;
						int num = triggerThreshold * 1024 * 1024;
						if (workingSet > (long)num)
						{
							Logger.Info("Current Process Working set exceeds {0} MB, its {1} now.", new object[]
							{
								triggerThreshold,
								workingSet / 1048576L
							});
							MemoryManager.TriggerMemoryTrimInAndroid();
						}
					}
				})
				{
					IsBackground = true
				};
				MemoryManager.androidtrimMemoryThread.Start();
			}
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x000198B4 File Offset: 0x00017AB4
		private static void TriggerMemoryTrimInAndroid()
		{
			try
			{
				int bstCommandProcessorPort = Utils.GetBstCommandProcessorPort(MultiInstanceStrings.VmName);
				string text = "triggerMemoryTrim";
				string text2 = string.Format(CultureInfo.InvariantCulture, "http://127.0.0.1:{0}/{1}", new object[]
				{
					bstCommandProcessorPort,
					text
				});
				Logger.Info("Sending request to: " + text2);
				string text3 = JObject.Parse(BstHttpClient.Get(text2, null, false, MultiInstanceStrings.VmName, 0, 1, 0, false, "bgp"))["result"].ToString();
				Logger.Info("the result we get from {0} is {1}", new object[]
				{
					text,
					text3
				});
			}
			catch (Exception ex)
			{
				Logger.Error("Exception occured when calling triggerMemoryTrim API of BstCommandProcessor. Err : {0}", new object[]
				{
					ex.ToString()
				});
			}
		}

		// Token: 0x04000239 RID: 569
		private static Thread trimMemoryThread;

		// Token: 0x0400023A RID: 570
		private static Thread androidtrimMemoryThread;
	}
}
