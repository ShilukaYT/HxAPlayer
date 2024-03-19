using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000050 RID: 80
	public class AdbCommandRunner : IDisposable
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00002CC1 File Offset: 0x00000EC1
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x00002CC9 File Offset: 0x00000EC9
		public int Port { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00002CD2 File Offset: 0x00000ED2
		// (set) Token: 0x060001B6 RID: 438 RVA: 0x00002CDA File Offset: 0x00000EDA
		public string Path { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00002CE3 File Offset: 0x00000EE3
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00002CEB File Offset: 0x00000EEB
		public bool IsHostConnected { get; set; }

		// Token: 0x060001B9 RID: 441 RVA: 0x000116FC File Offset: 0x0000F8FC
		public AdbCommandRunner(string vmName = "Android")
		{
			if (!string.IsNullOrEmpty(vmName))
			{
				this.mVmName = vmName;
				AdbCommandRunner.GUEST_URL = string.Format(CultureInfo.InvariantCulture, "http://127.0.0.1:{0}", new object[]
				{
					RegistryManager.Instance.Guest[this.mVmName].BstAndroidPort
				});
			}
			this.IsHostConnected = this.EnableADB();
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00011774 File Offset: 0x0000F974
		private static bool CheckIfGuestCommandSuccess(string res)
		{
			string text = JObject.Parse(res)["result"].ToString().Trim();
			if (string.Equals(text, "ok", StringComparison.InvariantCultureIgnoreCase))
			{
				return true;
			}
			Logger.Error("result: {0}", new object[]
			{
				text
			});
			return false;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000117C4 File Offset: 0x0000F9C4
		private bool EnableADB()
		{
			string api = "connectHost";
			return this.HitGuestAPI(api);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000117E0 File Offset: 0x0000F9E0
		private bool DisableADB()
		{
			string api = "disconnectHost";
			return this.HitGuestAPI(api);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000117FC File Offset: 0x0000F9FC
		private bool HitGuestAPI(string api)
		{
			try
			{
				return AdbCommandRunner.CheckIfGuestCommandSuccess(HTTPUtils.SendRequestToGuest(api, null, this.mVmName, 0, null, false, 1, 0, "bgp"));
			}
			catch (Exception ex)
			{
				Logger.Error("Error in Sending request {0} to guest {1}", new object[]
				{
					api,
					ex.ToString()
				});
			}
			return false;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00002CF4 File Offset: 0x00000EF4
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00002CFC File Offset: 0x00000EFC
		public AdbCommandRunner.OutputLineHandlerDelegate OutputLineHandler { get; set; }

		// Token: 0x060001C0 RID: 448 RVA: 0x0001185C File Offset: 0x0000FA5C
		public bool Connect(string vmName)
		{
			this.Port = RegistryManager.Instance.Guest[vmName].BstAdbPort;
			this.Path = System.IO.Path.Combine(RegistryStrings.InstallDir, "HD-Adb.exe");
			if (!this.RunInternal(string.Format(CultureInfo.InvariantCulture, "connect 127.0.0.1:{0}", new object[]
			{
				this.Port
			}), true))
			{
				return false;
			}
			this.RunInternal("devices", true);
			return true;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000118D8 File Offset: 0x0000FAD8
		private bool RunInternal(string cmd, bool retry = true)
		{
			Logger.Info("ADB CMD: " + cmd);
			bool result;
			using (Process process = new Process())
			{
				process.StartInfo.FileName = this.Path;
				process.StartInfo.Arguments = cmd;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.RedirectStandardError = true;
				process.StartInfo.CreateNoWindow = true;
				process.OutputDataReceived += delegate(object sender, DataReceivedEventArgs evt)
				{
					Logger.Info("ADB OUT: " + evt.Data);
				};
				process.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs evt)
				{
					if (!string.IsNullOrEmpty(evt.Data))
					{
						Logger.Info("ERR: " + evt.Data);
					}
				};
				process.Start();
				process.BeginOutputReadLine();
				process.BeginErrorReadLine();
				process.WaitForExit();
				int num = process.ExitCode;
				Logger.Info("ADB EXIT: " + num.ToString());
				if (num != 0 && retry)
				{
					Logger.Info("ADB retring");
					Thread.Sleep(4000);
					num = (this.RunInternal(cmd, false) ? 0 : 1);
				}
				result = (num == 0);
			}
			return result;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00011A14 File Offset: 0x0000FC14
		public bool Push(string localPath, string remotePath)
		{
			Logger.Info("Pushing {0} to {1}", new object[]
			{
				localPath,
				remotePath
			});
			return this.RunInternal(string.Format(CultureInfo.InvariantCulture, "-s 127.0.0.1:{0} push \"{1}\" \"{2}\"", new object[]
			{
				this.Port,
				localPath,
				remotePath
			}), true);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00011A6C File Offset: 0x0000FC6C
		public bool Pull(string filePath, string destPath)
		{
			Logger.Info("Pull file {0} in {1}", new object[]
			{
				filePath,
				destPath
			});
			return this.RunInternal(string.Format(CultureInfo.InvariantCulture, "-s 127.0.0.1:{0} pull \"{1}\" \"{2}\"", new object[]
			{
				this.Port,
				filePath,
				destPath
			}), true);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00002D05 File Offset: 0x00000F05
		public bool RunShell(string fmt, params object[] args)
		{
			return this.RunShell(string.Format(CultureInfo.InvariantCulture, fmt, args));
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00002D19 File Offset: 0x00000F19
		public bool RunShell(string cmd)
		{
			Logger.Info("RunShell: " + cmd);
			return this.RunInternal(string.Format(CultureInfo.InvariantCulture, "-s 127.0.0.1:{0} shell {1}", new object[]
			{
				this.Port,
				cmd
			}), true);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		public bool RunShellScript(string[] cmdList)
		{
			if (cmdList != null)
			{
				foreach (string cmd in cmdList)
				{
					if (!this.RunShell(cmd))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00002D59 File Offset: 0x00000F59
		public bool RunShellPrivileged(string fmt, params object[] cmd)
		{
			return this.RunShellPrivileged(string.Format(CultureInfo.InvariantCulture, fmt, cmd));
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00011AF4 File Offset: 0x0000FCF4
		public bool RunShellPrivileged(string cmd)
		{
			Logger.Info("RunShellPrivileged: " + cmd);
			return this.RunInternal(string.Format(CultureInfo.InvariantCulture, "-s 127.0.0.1:{0} shell {1} -c {2}", new object[]
			{
				this.Port,
				"/system/xbin/bstk/su",
				cmd
			}), true);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00011B48 File Offset: 0x0000FD48
		public bool RunShellScriptPrivileged(string[] cmdList)
		{
			if (cmdList != null)
			{
				foreach (string cmd in cmdList)
				{
					if (!this.RunShellPrivileged(cmd))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00002D6D File Offset: 0x00000F6D
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposedValue)
			{
				this.DisableADB();
				this.disposedValue = true;
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00011B78 File Offset: 0x0000FD78
		~AdbCommandRunner()
		{
			this.Dispose(false);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00002D87 File Offset: 0x00000F87
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x040000AE RID: 174
		private const string SU_PATH = "/system/xbin/bstk/su";

		// Token: 0x040000AF RID: 175
		private static string GUEST_URL = string.Format(CultureInfo.InvariantCulture, "http://127.0.0.1:{0}", new object[]
		{
			RegistryManager.Instance.Guest["Android"].BstAndroidPort
		});

		// Token: 0x040000B2 RID: 178
		private string mVmName = "Android";

		// Token: 0x040000B5 RID: 181
		private bool disposedValue;

		// Token: 0x02000051 RID: 81
		// (Invoke) Token: 0x060001CF RID: 463
		public delegate void OutputLineHandlerDelegate(string line);
	}
}
