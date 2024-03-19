using System;
using System.Diagnostics;
using System.IO;

namespace BlueStacks.Common
{
	// Token: 0x02000053 RID: 83
	public static class BlueStacksGL
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x0000449C File Offset: 0x0000269C
		public static int GLCheckInstallation(GLRenderer rendererToCheck, GLMode modeToCheck, out string glVendor, out string glRenderer, out string glVersion)
		{
			Logger.Info("Checking for GLRenderer: {0}, GLMode: {1}", new object[]
			{
				rendererToCheck,
				modeToCheck
			});
			string text = "";
			string text2 = "";
			string text3 = "";
			glVendor = text;
			glRenderer = text2;
			glVersion = text3;
			return BlueStacksGL.Run(BlueStacksGL.BinaryPath, BlueStacksGL.GetArgs(rendererToCheck, modeToCheck), true, true, out glVendor, out glRenderer, out glVersion, 10000);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000450C File Offset: 0x0000270C
		private static string GetArgs(GLRenderer rendererToCheck, GLMode modeToCheck)
		{
			return string.Format("{0} {1}", (int)rendererToCheck, (int)modeToCheck);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004534 File Offset: 0x00002734
		private static int Run(string prog, string args, bool logOutput, bool getGLValues, out string glVendor, out string glRenderer, out string glVersion, int timeout = 10000)
		{
			int result = -1;
			string vendor = "";
			string renderer = "";
			string version = "";
			glVendor = vendor;
			glRenderer = renderer;
			glVersion = version;
			try
			{
				using (Process process = new Process())
				{
					process.StartInfo.FileName = prog;
					process.StartInfo.Arguments = args;
					process.StartInfo.UseShellExecute = false;
					process.StartInfo.CreateNoWindow = true;
					bool flag = getGLValues | logOutput;
					if (flag)
					{
						bool flag2 = logOutput && !getGLValues;
						if (flag2)
						{
							process.StartInfo.RedirectStandardOutput = true;
							process.OutputDataReceived += delegate(object sender, DataReceivedEventArgs outLine)
							{
								try
								{
									string str = (outLine.Data != null) ? outLine.Data : "";
									bool logOutput2 = logOutput;
									if (logOutput2)
									{
										Logger.Info("OUT: " + str);
									}
								}
								catch (Exception ex2)
								{
									Logger.Error("A crash occured in the GLCheck delegate");
									Logger.Error(ex2.ToString());
								}
							};
						}
						else
						{
							process.StartInfo.RedirectStandardOutput = true;
							process.OutputDataReceived += delegate(object sender, DataReceivedEventArgs outLine)
							{
								try
								{
									string text = (outLine.Data != null) ? outLine.Data : "";
									bool logOutput2 = logOutput;
									if (logOutput2)
									{
										Logger.Info("OUT: " + text);
									}
									bool flag6 = text.Contains("GL_VENDOR =");
									if (flag6)
									{
										int num = text.IndexOf('=');
										vendor = text.Substring(num + 1).Trim();
										vendor = vendor.Replace(";", ";;");
									}
									bool flag7 = text.Contains("GL_RENDERER =");
									if (flag7)
									{
										int num = text.IndexOf('=');
										renderer = text.Substring(num + 1).Trim();
										renderer = renderer.Replace(";", ";;");
									}
									bool flag8 = text.Contains("GL_VERSION =");
									if (flag8)
									{
										int num = text.IndexOf('=');
										version = text.Substring(num + 1).Trim();
										version = version.Replace(";", ";;");
									}
								}
								catch (Exception ex2)
								{
									Logger.Error("A crash occured in the GLCheck delegate");
									Logger.Error(ex2.ToString());
								}
							};
						}
					}
					Logger.Info("{0}: {1}", new object[]
					{
						prog,
						args
					});
					process.Start();
					bool flag3 = getGLValues | logOutput;
					if (flag3)
					{
						process.BeginOutputReadLine();
					}
					bool flag4 = process.WaitForExit(timeout);
					glVendor = vendor;
					glRenderer = renderer;
					glVersion = version;
					bool flag5 = flag4;
					if (flag5)
					{
						string msg = process.Id.ToString() + " EXIT: " + process.ExitCode.ToString();
						Logger.Info(msg);
						result = process.ExitCode;
					}
					else
					{
						Logger.Fatal("Process {0} killed after timeout: {1}s", new object[]
						{
							process.StartInfo.FileName,
							timeout / 1000
						});
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Some error while running GLCheck. Ex: {0}", new object[]
				{
					ex
				});
			}
			return result;
		}

		// Token: 0x040001B0 RID: 432
		private static string BinaryName = "HD-GLCheck.exe";

		// Token: 0x040001B1 RID: 433
		private static string BinaryPath = Path.Combine(Directory.GetCurrentDirectory(), BlueStacksGL.BinaryName);
	}
}
