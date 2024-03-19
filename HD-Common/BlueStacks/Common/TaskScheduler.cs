using System;
using System.Globalization;
using System.Reflection;

namespace BlueStacks.Common
{
	// Token: 0x020000F7 RID: 247
	public static class TaskScheduler
	{
		// Token: 0x06000627 RID: 1575 RVA: 0x0001CA9C File Offset: 0x0001AC9C
		public static int CreateTask(string taskName, string binaryToRun, Tasks.Frequency frequency, int modifierOrIdleTime, DateTime timeToStart)
		{
			if (string.IsNullOrEmpty(binaryToRun))
			{
				binaryToRun = Assembly.GetEntryAssembly().Location;
			}
			DateTimeFormatInfo dateTimeFormat = new CultureInfo("en-US", false).DateTimeFormat;
			string text = string.Format(CultureInfo.InvariantCulture, string.Concat(new string[]
			{
				"/",
				Tasks.Parameter.Create.ToString(),
				" /SC ",
				frequency.ToString(),
				" /TN ",
				taskName,
				string.Format(CultureInfo.InvariantCulture, " /TR \"{0}\"", new object[]
				{
					binaryToRun
				}),
				" /F"
			}), new object[0]);
			if (frequency != Tasks.Frequency.ONIDLE)
			{
				text = string.Format(CultureInfo.InvariantCulture, "{0} /ST {1}", new object[]
				{
					text,
					timeToStart.ToString("HH:mm", dateTimeFormat)
				});
				text = string.Format(CultureInfo.InvariantCulture, "{0} /MO {1}", new object[]
				{
					text,
					modifierOrIdleTime.ToString(CultureInfo.InvariantCulture)
				});
			}
			else
			{
				text = string.Format(CultureInfo.InvariantCulture, "{0} /I " + modifierOrIdleTime.ToString(CultureInfo.InvariantCulture), new object[]
				{
					text
				});
			}
			int num = TaskScheduler.RunSchedulerCommand(text);
			if (num != 0)
			{
				Logger.Error("An error occured while creating the task, exit code: {0}", new object[]
				{
					num
				});
			}
			return num;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001CBF4 File Offset: 0x0001ADF4
		public static int DeleteTask(string taskName)
		{
			int num = TaskScheduler.RunSchedulerCommand(string.Format(CultureInfo.InvariantCulture, string.Concat(new string[]
			{
				"/",
				Tasks.Parameter.Delete.ToString(),
				" /TN ",
				taskName,
				" /F"
			}), new object[0]));
			if (num != 0)
			{
				Logger.Error("An error occured while deleting the task, exit code: {0}", new object[]
				{
					num
				});
			}
			return num;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001CC70 File Offset: 0x0001AE70
		private static string QueryTaskArguments(string taskName)
		{
			return string.Format(CultureInfo.InvariantCulture, "/" + Tasks.Parameter.Query.ToString() + " /FO LIST /V  /TN " + taskName, new object[0]);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001CCAC File Offset: 0x0001AEAC
		public static int QueryTask(string taskName)
		{
			int num = TaskScheduler.RunSchedulerCommand(TaskScheduler.QueryTaskArguments(taskName));
			if (num != 0)
			{
				Logger.Error("An error occured while querying the task, exit code: {0}", new object[]
				{
					num
				});
			}
			return num;
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x000059AE File Offset: 0x00003BAE
		private static int RunSchedulerCommand(string args)
		{
			return RunCommand.RunCmd(TaskScheduler.BinaryName, args, true, true, false, 0).ExitCode;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x000059C4 File Offset: 0x00003BC4
		public static CmdRes GetTaskQueryCommandOutput(string taskName)
		{
			return RunCommand.RunCmd(TaskScheduler.BinaryName, TaskScheduler.QueryTaskArguments(taskName), false, true, false, 0);
		}

		// Token: 0x04000354 RID: 852
		private static string BinaryName = "schtasks.exe";
	}
}
