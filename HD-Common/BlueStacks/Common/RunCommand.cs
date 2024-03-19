using System;
using System.Diagnostics;

namespace BlueStacks.Common
{
	// Token: 0x02000133 RID: 307
	public static class RunCommand
	{
		// Token: 0x06000C02 RID: 3074 RVA: 0x0002B9AC File Offset: 0x00029BAC
		public static CmdRes RunCmd(string cmd, string args, bool addOutputToLogs = false, bool addCommandToLogs = true, bool requireAdministrator = false, int timeout = 0)
		{
			if (addCommandToLogs)
			{
				Logger.Info("RunCmd: starting cmd: " + cmd);
				Logger.Info("  args: " + args);
			}
			CmdRes res = new CmdRes();
			using (Process proc = new Process())
			{
				proc.StartInfo.FileName = cmd;
				proc.StartInfo.Arguments = args;
				proc.StartInfo.UseShellExecute = false;
				proc.StartInfo.CreateNoWindow = true;
				if (requireAdministrator)
				{
					proc.StartInfo.Verb = "runas";
				}
				proc.StartInfo.RedirectStandardInput = true;
				proc.StartInfo.RedirectStandardOutput = true;
				proc.StartInfo.RedirectStandardError = true;
				proc.OutputDataReceived += delegate(object sender, DataReceivedEventArgs line)
				{
					string data = line.Data;
					string text = (data != null) ? data.Trim() : null;
					if (!string.IsNullOrEmpty(text))
					{
						if (addOutputToLogs)
						{
							Logger.Info(proc.Id.ToString() + " OUT: " + text);
						}
						CmdRes res = res;
						res.StdOut = res.StdOut + text + "\n";
					}
				};
				proc.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs line)
				{
					if (addOutputToLogs)
					{
						Logger.Info(proc.Id.ToString() + " ERR: " + line.Data);
					}
					CmdRes res = res;
					res.StdErr = res.StdErr + line.Data + "\n";
				};
				proc.Start();
				proc.BeginOutputReadLine();
				proc.BeginErrorReadLine();
				bool flag = true;
				if (timeout == 0)
				{
					proc.WaitForExit();
				}
				else
				{
					flag = proc.WaitForExit(timeout);
				}
				proc.CancelOutputRead();
				proc.CancelErrorRead();
				if (addOutputToLogs && flag)
				{
					Logger.Info("RunCmd for proc.ID = " + proc.Id.ToString() + " exited with exitCode: " + proc.ExitCode.ToString());
					res.ExitCode = proc.ExitCode;
				}
				if (!flag)
				{
					Logger.Fatal("RunCmd for proc.ID = {0} terminated after timeout of {1}", new object[]
					{
						proc.Id,
						timeout
					});
					res.ExitCode = -1;
				}
			}
			return res;
		}
	}
}
