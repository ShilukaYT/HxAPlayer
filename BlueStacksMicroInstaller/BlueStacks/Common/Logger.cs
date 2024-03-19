using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.Win32;

namespace BlueStacks.Common
{
	// Token: 0x02000068 RID: 104
	public static class Logger
	{
		// Token: 0x06000168 RID: 360 RVA: 0x000090A8 File Offset: 0x000072A8
		public static void InitUserLog()
		{
			Logger.InitLog(null, null, true);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000090B4 File Offset: 0x000072B4
		private static string GetLogDir()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(string.Format(CultureInfo.InvariantCulture, "Software\\BlueStacks{0}", new object[]
			{
				Strings.GetOemTag()
			})))
			{
				bool flag = registryKey != null;
				if (flag)
				{
					Logger.s_logDir = (string)registryKey.GetValue("LogDir", "");
				}
				bool flag2 = string.IsNullOrEmpty(Logger.s_logDir);
				if (flag2)
				{
					Logger.s_logDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
					Logger.s_logDir = Path.Combine(Logger.s_logDir, "Bluestacks\\Logs");
				}
			}
			return Logger.s_logDir;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00009168 File Offset: 0x00007368
		private static void HdLogger(int prio, uint tid, string tag, string msg)
		{
			string logFromLevel = Logger.GetLogFromLevel(prio);
			Logger.Print(logFromLevel, tag, "{0:X8}: {1}", new object[]
			{
				tid,
				msg
			});
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000091A0 File Offset: 0x000073A0
		public static void InitLog(string logFileName, string tag, bool doLogRotation = true)
		{
			bool flag = logFileName == null;
			if (flag)
			{
				logFileName = "BlueStacksUsers";
			}
			bool flag2 = logFileName == "-";
			string text;
			if (flag2)
			{
				text = "-";
			}
			else
			{
				text = Path.Combine(Logger.GetLogDir(), logFileName);
				bool flag3 = !Path.HasExtension(text);
				if (flag3)
				{
					text += ".log";
				}
			}
			Logger.InitLogAtPath(text, tag, doLogRotation);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000920C File Offset: 0x0000740C
		public static void InitLogAtPath(string logFilePath, string _, bool doLogRotation)
		{
			Logger.s_loggerInited = true;
			Logger.s_HdLoggerCallback = new Logger.HdLoggerCallback(Logger.HdLogger);
			Logger.s_processId = Process.GetCurrentProcess().Id;
			Logger.s_processName = Process.GetCurrentProcess().ProcessName;
			string directoryName = Path.GetDirectoryName(logFilePath);
			bool flag = !Directory.Exists(directoryName);
			if (flag)
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
				string registryPath = Path.Combine(Strings.RegistryBaseKeyPath, "Client");
				bool flag2 = (int)RegistryUtils.GetRegistryValue(registryPath, "RotateLog", 1, RegistryKeyKind.HKEY_LOCAL_MACHINE) != 0;
				bool flag3 = flag2;
				if (flag3)
				{
					thread.IsBackground = true;
					thread.Start();
				}
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000092F4 File Offset: 0x000074F4
		public static Logger.HdLoggerCallback GetHdLoggerCallback()
		{
			return Logger.s_HdLoggerCallback;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000930C File Offset: 0x0000750C
		private static void LogLevelsInit()
		{
			Logger.s_logLevels = (string)RegistryUtils.GetRegistryValue(Strings.RegistryBaseKeyPath, "DebugLogs", null, RegistryKeyKind.HKEY_CURRENT_USER);
			bool flag = string.IsNullOrEmpty(Logger.s_logLevels);
			if (flag)
			{
				Logger.s_logLevels = (string)RegistryUtils.GetRegistryValue(Strings.RegistryBaseKeyPath, "DebugLogs", null, RegistryKeyKind.HKEY_LOCAL_MACHINE);
				bool flag2 = !string.IsNullOrEmpty(Logger.s_logLevels);
				if (flag2)
				{
					Logger.s_logLevels = Logger.s_logLevels.ToUpper(CultureInfo.InvariantCulture);
				}
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00009387 File Offset: 0x00007587
		public static void EnableDebugLogs()
		{
			Logger.s_logLevels = "ALL";
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00009394 File Offset: 0x00007594
		private static bool IsLogLevelEnabled(string tag, string level)
		{
			bool flag = Logger.s_logLevels == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = Logger.s_logLevels.StartsWith("ALL", StringComparison.OrdinalIgnoreCase);
				result = (flag2 || Logger.s_logLevels.Contains((tag + ":" + level).ToUpper(CultureInfo.InvariantCulture)));
			}
			return result;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000093F0 File Offset: 0x000075F0
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
						FileInfo fileInfo = new FileInfo(Logger.s_logFilePath);
						bool flag = fileInfo.Length >= (long)Logger.s_logFileSize;
						if (flag)
						{
							string destFileName = Logger.s_logFilePath + ".1";
							string path = Logger.s_logFilePath + "." + Logger.s_totalLogFileNum.ToString();
							bool flag2 = File.Exists(path);
							if (flag2)
							{
								File.Delete(path);
							}
							for (int i = Logger.s_totalLogFileNum - 1; i >= 1; i--)
							{
								string text = Logger.s_logFilePath + "." + i.ToString();
								string destFileName2 = Logger.s_logFilePath + "." + (i + 1).ToString();
								bool flag3 = File.Exists(text);
								if (flag3)
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

		// Token: 0x06000172 RID: 370 RVA: 0x00009554 File Offset: 0x00007754
		private static void Open()
		{
			Logger.s_fileStream = new FileStream(Logger.s_logFilePath, FileMode.Append, FileAccess.Write, FileShare.Read | FileShare.Write | FileShare.Delete);
			Logger.sWriter = new StreamWriter(Logger.s_fileStream, Encoding.UTF8);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00009580 File Offset: 0x00007780
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

		// Token: 0x06000174 RID: 372 RVA: 0x000095DC File Offset: 0x000077DC
		public static TextWriter GetWriter()
		{
			return new Writer(delegate(string msg)
			{
				Logger.Print(msg);
			});
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00009614 File Offset: 0x00007814
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

		// Token: 0x06000176 RID: 374 RVA: 0x00009678 File Offset: 0x00007878
		private static void Print(string s, string tag, string fmt, params object[] args)
		{
			bool flag = !Logger.s_loggerInited;
			if (flag)
			{
				Logger.InitLog("", tag, true);
			}
			bool flag2 = s == Logger.s_logStringDebug && !Logger.IsLogLevelEnabled(tag, s);
			if (!flag2)
			{
				object obj = Logger.s_sync;
				lock (obj)
				{
					try
					{
						bool flag3 = Logger.s_fileStream != null;
						if (flag3)
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
					bool flag4 = Logger.s_OpenCloseAfterCount > Logger.s_OpenCloseAfter;
					if (flag4)
					{
						Logger.Close();
						Logger.s_OpenCloseAfterCount = 0;
						Logger.Open();
					}
					Logger.sWriter.WriteLine(Logger.GetPrefix(tag, s) + Logger.s_vmNameTextToLog + fmt, args);
					Logger.sWriter.Flush();
				}
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000978C File Offset: 0x0000798C
		private static void Print(string fmt, params object[] args)
		{
			Logger.Print(Logger.s_logStringInfo, Logger.s_processName, fmt, args);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000097A1 File Offset: 0x000079A1
		private static void Print(string msg)
		{
			Logger.Print("{0}", new object[]
			{
				msg
			});
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000097B9 File Offset: 0x000079B9
		public static void Fatal(string fmt, params object[] args)
		{
			Logger.Print(Logger.s_logStringFatal, Logger.s_processName, fmt, args);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000097CE File Offset: 0x000079CE
		public static void Fatal(string msg)
		{
			Logger.Fatal("{0}", new object[]
			{
				msg
			});
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000097E6 File Offset: 0x000079E6
		public static void Error(string fmt, params object[] args)
		{
			Logger.Print(Logger.s_logStringError, Logger.s_processName, fmt, args);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000097FB File Offset: 0x000079FB
		public static void Error(string msg)
		{
			Logger.Error("{0}", new object[]
			{
				msg
			});
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00009813 File Offset: 0x00007A13
		public static void Warning(string fmt, params object[] args)
		{
			Logger.Print(Logger.s_logStringWarning, Logger.s_processName, fmt, args);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00009828 File Offset: 0x00007A28
		public static void Warning(string msg)
		{
			Logger.Warning("{0}", new object[]
			{
				msg
			});
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000978C File Offset: 0x0000798C
		public static void Info(string fmt, params object[] args)
		{
			Logger.Print(Logger.s_logStringInfo, Logger.s_processName, fmt, args);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00009840 File Offset: 0x00007A40
		public static void Info(string msg)
		{
			Logger.Info("{0}", new object[]
			{
				msg
			});
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00009858 File Offset: 0x00007A58
		public static void Debug(string fmt, params object[] args)
		{
			Logger.Print(Logger.s_logStringDebug, Logger.s_processName, fmt, args);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000986D File Offset: 0x00007A6D
		public static void Debug(string msg)
		{
			Logger.Debug("{0}", new object[]
			{
				msg
			});
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00009888 File Offset: 0x00007A88
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

		// Token: 0x06000184 RID: 388 RVA: 0x0000994C File Offset: 0x00007B4C
		public static void InitVmInstanceName(string vmName)
		{
			bool flag = vmName != null && !vmName.Equals("Android", StringComparison.OrdinalIgnoreCase);
			if (flag)
			{
				Logger.s_vmNameTextToLog = vmName + ": ";
			}
		}

		// Token: 0x040001E3 RID: 483
		private const int HDLOG_PRIORITY_FATAL = 0;

		// Token: 0x040001E4 RID: 484
		private const int HDLOG_PRIORITY_ERROR = 1;

		// Token: 0x040001E5 RID: 485
		private const int HDLOG_PRIORITY_WARNING = 2;

		// Token: 0x040001E6 RID: 486
		private const int HDLOG_PRIORITY_INFO = 3;

		// Token: 0x040001E7 RID: 487
		private const int HDLOG_PRIORITY_DEBUG = 4;

		// Token: 0x040001E8 RID: 488
		private static int s_OpenCloseAfter = 300;

		// Token: 0x040001E9 RID: 489
		private static int s_OpenCloseAfterCount = 0;

		// Token: 0x040001EA RID: 490
		private static object s_sync = new object();

		// Token: 0x040001EB RID: 491
		private static TextWriter sWriter = Console.Error;

		// Token: 0x040001EC RID: 492
		private static int s_logRotationTime = 30000;

		// Token: 0x040001ED RID: 493
		public static readonly int s_logFileSize = 10485760;

		// Token: 0x040001EE RID: 494
		public static readonly int s_totalLogFileNum = 5;

		// Token: 0x040001EF RID: 495
		private static string s_logFilePath = null;

		// Token: 0x040001F0 RID: 496
		private static bool s_loggerInited = false;

		// Token: 0x040001F1 RID: 497
		private static int s_processId = -1;

		// Token: 0x040001F2 RID: 498
		private static string s_processName = "Unknown";

		// Token: 0x040001F3 RID: 499
		private static string s_logLevels = null;

		// Token: 0x040001F4 RID: 500
		private static FileStream s_fileStream;

		// Token: 0x040001F5 RID: 501
		private static string s_logDir = null;

		// Token: 0x040001F6 RID: 502
		private const string DEFAULT_FILE_NAME = "BlueStacksUsers";

		// Token: 0x040001F7 RID: 503
		private static string s_logStringFatal = "FATAL";

		// Token: 0x040001F8 RID: 504
		private static string s_logStringError = "ERROR";

		// Token: 0x040001F9 RID: 505
		private static string s_logStringWarning = "WARNING";

		// Token: 0x040001FA RID: 506
		private static string s_logStringInfo = "INFO";

		// Token: 0x040001FB RID: 507
		private static string s_logStringDebug = "DEBUG";

		// Token: 0x040001FC RID: 508
		private static string s_vmNameTextToLog = "";

		// Token: 0x040001FD RID: 509
		private static Logger.HdLoggerCallback s_HdLoggerCallback;

		// Token: 0x020000DA RID: 218
		// (Invoke) Token: 0x06000447 RID: 1095
		public delegate void HdLoggerCallback(int prio, uint tid, string tag, string msg);
	}
}
