using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace BlueStacks.Common
{
	// Token: 0x02000218 RID: 536
	public static class Logger
	{
		// Token: 0x060010A9 RID: 4265 RVA: 0x0000E26C File Offset: 0x0000C46C
		public static void InitUserLog()
		{
			Logger.InitLog(null, null, true);
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00040050 File Offset: 0x0003E250
		private static string GetLogDir()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(string.Format(CultureInfo.InvariantCulture, "Software\\BlueStacks{0}", new object[]
			{
				Strings.GetOemTag()
			})))
			{
				if (registryKey != null)
				{
					Logger.s_logDir = (string)registryKey.GetValue("LogDir", "");
				}
				if (string.IsNullOrEmpty(Logger.s_logDir))
				{
					Logger.s_logDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
					Logger.s_logDir = Path.Combine(Logger.s_logDir, "Bluestacks\\Logs");
				}
			}
			return Logger.s_logDir;
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0000E276 File Offset: 0x0000C476
		private static void HdLogger(int prio, uint tid, string tag, string msg)
		{
			Logger.Print(Logger.GetLogFromLevel(prio), tag, "{0:X8}: {1}", new object[]
			{
				tid,
				msg
			});
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x000400F0 File Offset: 0x0003E2F0
		public static void InitLog(string logFileName, string tag, bool doLogRotation = true)
		{
			if (logFileName == null)
			{
				logFileName = "BlueStacksUsers";
			}
			string text;
			if (logFileName == "-")
			{
				text = "-";
			}
			else
			{
				text = Path.Combine(Logger.GetLogDir(), logFileName);
				if (!Path.HasExtension(text))
				{
					text += ".log";
				}
			}
			Logger.InitLogAtPath(text, tag, doLogRotation);
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00040144 File Offset: 0x0003E344
		public static void InitLogAtPath(string logFilePath, string _, bool doLogRotation)
		{
			Logger.s_loggerInited = true;
			Logger.s_HdLoggerCallback = new Logger.HdLoggerCallback(Logger.HdLogger);
			Logger.s_processId = Process.GetCurrentProcess().Id;
			Logger.s_processName = Process.GetCurrentProcess().ProcessName;
			string directoryName = Path.GetDirectoryName(logFilePath);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			Logger.s_logFilePath = logFilePath;
			Logger.LogLevelsInit();
			Logger.Open();
			if (doLogRotation)
			{
				Thread thread = new Thread(delegate()
				{
					Logger.DoLogRotation();
				});
				if ((int)RegistryUtils.GetRegistryValue(Path.Combine(Strings.RegistryBaseKeyPath, "Client"), "RotateLog", 1, RegistryKeyKind.HKEY_LOCAL_MACHINE) != 0)
				{
					thread.IsBackground = true;
					thread.Start();
				}
			}
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0000E29C File Offset: 0x0000C49C
		public static Logger.HdLoggerCallback GetHdLoggerCallback()
		{
			return Logger.s_HdLoggerCallback;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00040210 File Offset: 0x0003E410
		private static void LogLevelsInit()
		{
			Logger.s_logLevels = (string)RegistryUtils.GetRegistryValue(Strings.RegistryBaseKeyPath, "DebugLogs", null, RegistryKeyKind.HKEY_CURRENT_USER);
			if (string.IsNullOrEmpty(Logger.s_logLevels))
			{
				Logger.s_logLevels = (string)RegistryUtils.GetRegistryValue(Strings.RegistryBaseKeyPath, "DebugLogs", null, RegistryKeyKind.HKEY_LOCAL_MACHINE);
				if (!string.IsNullOrEmpty(Logger.s_logLevels))
				{
					Logger.s_logLevels = Logger.s_logLevels.ToUpper(CultureInfo.InvariantCulture);
				}
			}
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0000E2A3 File Offset: 0x0000C4A3
		public static void EnableDebugLogs()
		{
			Logger.s_logLevels = "ALL";
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x0000E2AF File Offset: 0x0000C4AF
		private static bool IsLogLevelEnabled(string tag, string level)
		{
			return Logger.s_logLevels != null && (Logger.s_logLevels.StartsWith("ALL", StringComparison.OrdinalIgnoreCase) || Logger.s_logLevels.Contains((tag + ":" + level).ToUpper(CultureInfo.InvariantCulture)));
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x00040280 File Offset: 0x0003E480
		private static void DoLogRotation()
		{
			for (;;)
			{
				Thread.Sleep(Logger.s_logRotationTime);
				try
				{
					object obj = Logger.s_sync;
					lock (obj)
					{
						if (new FileInfo(Logger.s_logFilePath).Length >= (long)Logger.s_logFileSize)
						{
							string destFileName = Logger.s_logFilePath + ".1";
							string path = Logger.s_logFilePath + "." + Logger.s_totalLogFileNum.ToString();
							if (File.Exists(path))
							{
								File.Delete(path);
							}
							for (int i = Logger.s_totalLogFileNum - 1; i >= 1; i--)
							{
								string text = Logger.s_logFilePath + "." + i.ToString();
								string destFileName2 = Logger.s_logFilePath + "." + (i + 1).ToString();
								if (File.Exists(text))
								{
									File.Move(text, destFileName2);
								}
							}
							File.Move(Logger.s_logFilePath, destFileName);
						}
					}
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0000E2EE File Offset: 0x0000C4EE
		private static void Open()
		{
			Logger.s_fileStream = new FileStream(Logger.s_logFilePath, FileMode.Append, FileAccess.Write, FileShare.Read | FileShare.Write | FileShare.Delete);
			Logger.sWriter = new StreamWriter(Logger.s_fileStream, Encoding.UTF8);
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00040390 File Offset: 0x0003E590
		private static void Close()
		{
			try
			{
				Logger.sWriter.Close();
				Logger.s_fileStream.Close();
				Logger.s_fileStream.Dispose();
				Logger.sWriter.Dispose();
			}
			catch (Exception)
			{
				Logger.Open();
			}
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0000E316 File Offset: 0x0000C516
		public static TextWriter GetWriter()
		{
			return new Writer(delegate(string msg)
			{
				Logger.Print(msg);
			});
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x000403E0 File Offset: 0x0003E5E0
		private static string GetLogFromLevel(int level)
		{
			string result;
			switch (level)
			{
			case 0:
				result = Logger.s_logStringFatal;
				break;
			case 1:
				result = Logger.s_logStringError;
				break;
			case 2:
				result = Logger.s_logStringWarning;
				break;
			case 3:
				result = Logger.s_logStringInfo;
				break;
			case 4:
				result = Logger.s_logStringDebug;
				break;
			default:
				result = "UNKNOWN";
				break;
			}
			return result;
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00040438 File Offset: 0x0003E638
		private static void Print(string s, string tag, string fmt, params object[] args)
		{
			if (!Logger.s_loggerInited)
			{
				Logger.InitLog("", tag, true);
			}
			if (s == Logger.s_logStringDebug && !Logger.IsLogLevelEnabled(tag, s))
			{
				return;
			}
			object obj = Logger.s_sync;
			lock (obj)
			{
				try
				{
					if (Logger.s_fileStream != null)
					{
						Logger.s_fileStream.Seek(0L, SeekOrigin.End);
					}
				}
				catch (Exception)
				{
					Logger.Close();
					Logger.s_OpenCloseAfterCount = 0;
					Logger.Open();
				}
				Logger.s_OpenCloseAfterCount++;
				if (Logger.s_OpenCloseAfterCount > Logger.s_OpenCloseAfter)
				{
					Logger.Close();
					Logger.s_OpenCloseAfterCount = 0;
					Logger.Open();
				}
				Logger.sWriter.WriteLine(Logger.GetPrefix(tag, s) + Logger.s_vmNameTextToLog + fmt, args);
				Logger.sWriter.Flush();
			}
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x0000E33C File Offset: 0x0000C53C
		private static void Print(string fmt, params object[] args)
		{
			Logger.Print(Logger.s_logStringInfo, Logger.s_processName, fmt, args);
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0000E34F File Offset: 0x0000C54F
		private static void Print(string msg)
		{
			Logger.Print("{0}", new object[]
			{
				msg
			});
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0000E365 File Offset: 0x0000C565
		public static void Fatal(string fmt, params object[] args)
		{
			Logger.Print(Logger.s_logStringFatal, Logger.s_processName, fmt, args);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0000E378 File Offset: 0x0000C578
		public static void Fatal(string msg)
		{
			Logger.Fatal("{0}", new object[]
			{
				msg
			});
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0000E38E File Offset: 0x0000C58E
		public static void Error(string fmt, params object[] args)
		{
			Logger.Print(Logger.s_logStringError, Logger.s_processName, fmt, args);
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0000E3A1 File Offset: 0x0000C5A1
		public static void Error(string msg)
		{
			Logger.Error("{0}", new object[]
			{
				msg
			});
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0000E3B7 File Offset: 0x0000C5B7
		public static void Warning(string fmt, params object[] args)
		{
			Logger.Print(Logger.s_logStringWarning, Logger.s_processName, fmt, args);
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0000E3CA File Offset: 0x0000C5CA
		public static void Warning(string msg)
		{
			Logger.Warning("{0}", new object[]
			{
				msg
			});
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0000E33C File Offset: 0x0000C53C
		public static void Info(string fmt, params object[] args)
		{
			Logger.Print(Logger.s_logStringInfo, Logger.s_processName, fmt, args);
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0000E3E0 File Offset: 0x0000C5E0
		public static void Info(string msg)
		{
			Logger.Info("{0}", new object[]
			{
				msg
			});
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x0000E3F6 File Offset: 0x0000C5F6
		public static void Debug(string fmt, params object[] args)
		{
			Logger.Print(Logger.s_logStringDebug, Logger.s_processName, fmt, args);
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x0000E409 File Offset: 0x0000C609
		public static void Debug(string msg)
		{
			Logger.Debug("{0}", new object[]
			{
				msg
			});
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x0004051C File Offset: 0x0003E71C
		private static string GetPrefix(string tag, string logLevel)
		{
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			DateTime now = DateTime.Now;
			return string.Format(CultureInfo.InvariantCulture, "{0:D4}-{1:D2}-{2:D2} {3:D2}:{4:D2}:{5:D2}.{6:D3} {7}:{8:X8} ({9}) {10}: ", new object[]
			{
				now.Year,
				now.Month,
				now.Day,
				now.Hour,
				now.Minute,
				now.Second,
				now.Millisecond,
				Logger.s_processId,
				managedThreadId,
				tag,
				logLevel
			});
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0000E41F File Offset: 0x0000C61F
		public static void InitVmInstanceName(string vmName)
		{
			if (vmName != null && !vmName.Equals("Android", StringComparison.OrdinalIgnoreCase))
			{
				Logger.s_vmNameTextToLog = vmName + ": ";
			}
		}

		// Token: 0x04000B26 RID: 2854
		private const int HDLOG_PRIORITY_FATAL = 0;

		// Token: 0x04000B27 RID: 2855
		private const int HDLOG_PRIORITY_ERROR = 1;

		// Token: 0x04000B28 RID: 2856
		private const int HDLOG_PRIORITY_WARNING = 2;

		// Token: 0x04000B29 RID: 2857
		private const int HDLOG_PRIORITY_INFO = 3;

		// Token: 0x04000B2A RID: 2858
		private const int HDLOG_PRIORITY_DEBUG = 4;

		// Token: 0x04000B2B RID: 2859
		private static int s_OpenCloseAfter = 300;

		// Token: 0x04000B2C RID: 2860
		private static int s_OpenCloseAfterCount = 0;

		// Token: 0x04000B2D RID: 2861
		private static object s_sync = new object();

		// Token: 0x04000B2E RID: 2862
		private static TextWriter sWriter = Console.Error;

		// Token: 0x04000B2F RID: 2863
		private static int s_logRotationTime = 30000;

		// Token: 0x04000B30 RID: 2864
		public static readonly int s_logFileSize = 10485760;

		// Token: 0x04000B31 RID: 2865
		public static readonly int s_totalLogFileNum = 5;

		// Token: 0x04000B32 RID: 2866
		private static string s_logFilePath = null;

		// Token: 0x04000B33 RID: 2867
		private static bool s_loggerInited = false;

		// Token: 0x04000B34 RID: 2868
		private static int s_processId = -1;

		// Token: 0x04000B35 RID: 2869
		private static string s_processName = "Unknown";

		// Token: 0x04000B36 RID: 2870
		private static string s_logLevels = null;

		// Token: 0x04000B37 RID: 2871
		private static FileStream s_fileStream;

		// Token: 0x04000B38 RID: 2872
		private static string s_logDir = null;

		// Token: 0x04000B39 RID: 2873
		private const string DEFAULT_FILE_NAME = "BlueStacksUsers";

		// Token: 0x04000B3A RID: 2874
		private static string s_logStringFatal = "FATAL";

		// Token: 0x04000B3B RID: 2875
		private static string s_logStringError = "ERROR";

		// Token: 0x04000B3C RID: 2876
		private static string s_logStringWarning = "WARNING";

		// Token: 0x04000B3D RID: 2877
		private static string s_logStringInfo = "INFO";

		// Token: 0x04000B3E RID: 2878
		private static string s_logStringDebug = "DEBUG";

		// Token: 0x04000B3F RID: 2879
		private static string s_vmNameTextToLog = "";

		// Token: 0x04000B40 RID: 2880
		private static Logger.HdLoggerCallback s_HdLoggerCallback;

		// Token: 0x02000219 RID: 537
		// (Invoke) Token: 0x060010C8 RID: 4296
		public delegate void HdLoggerCallback(int prio, uint tid, string tag, string msg);
	}
}
