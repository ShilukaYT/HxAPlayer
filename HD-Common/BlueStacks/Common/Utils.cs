using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
using BlueStacks.Common.Interop;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace BlueStacks.Common
{
	// Token: 0x0200016A RID: 362
	public static class Utils
	{
		// Token: 0x06000D47 RID: 3399
		[DllImport("urlmon.dll", CharSet = CharSet.Auto)]
		private static extern uint FindMimeFromData(uint pBC, [MarshalAs(UnmanagedType.LPStr)] string pwzUrl, [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer, uint cbSize, [MarshalAs(UnmanagedType.LPStr)] string pwzMimeProposed, uint dwMimeFlags, out uint ppwzMimeOut, uint dwReserverd);

		// Token: 0x06000D48 RID: 3400
		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		public static extern int SHChangeNotify(int eventId, int flags, IntPtr item1, IntPtr item2);

		// Token: 0x06000D49 RID: 3401
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		// Token: 0x06000D4A RID: 3402
		[DllImport("user32.dll")]
		private static extern uint GetWindowThreadProcessId(IntPtr hwnd, IntPtr proccess);

		// Token: 0x06000D4B RID: 3403
		[DllImport("user32.dll")]
		private static extern IntPtr GetKeyboardLayout(uint thread);

		// Token: 0x06000D4C RID: 3404
		[DllImport("user32.dll")]
		private static extern int GetSystemMetrics(int smIndex);

		// Token: 0x06000D4D RID: 3405
		[DllImport("setupapi.dll", SetLastError = true)]
		public static extern int SetupDiGetClassDevs(ref Guid lpGuid, IntPtr Enumerator, IntPtr hwndParent, ClassDevsFlags Flags);

		// Token: 0x06000D4E RID: 3406
		[DllImport("setupapi.dll", SetLastError = true)]
		public static extern int SetupDiGetClassDevs(IntPtr guid, IntPtr Enumerator, IntPtr hwndParent, ClassDevsFlags Flags);

		// Token: 0x06000D4F RID: 3407
		[DllImport("setupapi.dll", SetLastError = true)]
		public static extern int SetupDiEnumDeviceInfo(int DeviceInfoSet, int Index, ref SP_DEVINFO_DATA DeviceInfoData);

		// Token: 0x06000D50 RID: 3408
		[DllImport("setupapi.dll", SetLastError = true)]
		public static extern int SetupDiEnumDeviceInterfaces(int DeviceInfoSet, int DeviceInfoData, ref Guid lpHidGuid, int MemberIndex, ref SP_DEVICE_INTERFACE_DATA lpDeviceInterfaceData);

		// Token: 0x06000D51 RID: 3409
		[DllImport("setupapi.dll", SetLastError = true)]
		public static extern int SetupDiGetDeviceInterfaceDetail(int DeviceInfoSet, ref SP_DEVICE_INTERFACE_DATA lpDeviceInterfaceData, IntPtr aPtr, int detailSize, ref int requiredSize, IntPtr bPtr);

		// Token: 0x06000D52 RID: 3410
		[DllImport("setupapi.dll", SetLastError = true)]
		public static extern int SetupDiGetDeviceInterfaceDetail(int DeviceInfoSet, ref SP_DEVICE_INTERFACE_DATA lpDeviceInterfaceData, ref PSP_DEVICE_INTERFACE_DETAIL_DATA myPSP_DEVICE_INTERFACE_DETAIL_DATA, int detailSize, ref int requiredSize, IntPtr bPtr);

		// Token: 0x06000D53 RID: 3411
		[DllImport("setupapi.dll", SetLastError = true)]
		public static extern int SetupDiGetDeviceRegistryProperty(int DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, RegPropertyType Property, IntPtr PropertyRegDataType, IntPtr PropertyBuffer, int PropertyBufferSize, ref int RequiredSize);

		// Token: 0x06000D54 RID: 3412
		[DllImport("setupapi.dll", SetLastError = true)]
		public static extern int SetupDiGetDeviceRegistryProperty(int DeviceInfoSet, ref SP_DEVINFO_DATA DeviceInfoData, RegPropertyType Property, IntPtr PropertyRegDataType, ref DATA_BUFFER PropertyBuffer, int PropertyBufferSize, ref int RequiredSize);

		// Token: 0x06000D55 RID: 3413 RVA: 0x0002FD24 File Offset: 0x0002DF24
		public static bool IsTargetForShortcut(string shortcutPath, string targetPath)
		{
			try
			{
				if (File.Exists(shortcutPath))
				{
					string shortcutArguments = ShortcutHelper.GetShortcutArguments(shortcutPath);
					string text = (shortcutArguments != null) ? shortcutArguments.ToLower(CultureInfo.InvariantCulture).Trim() : null;
					targetPath = ((targetPath != null) ? targetPath.ToLower(CultureInfo.InvariantCulture).Trim() : null);
					if (text.Contains(targetPath) && string.Compare(text, Path.Combine(RegistryStrings.InstallDir, targetPath), StringComparison.OrdinalIgnoreCase) == 0)
					{
						Logger.Info("{0} is a shortcut for target {1}", new object[]
						{
							shortcutPath,
							targetPath
						});
						return true;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Warning("Ignoring exception while comparing TargetForShortcut: " + ex.Message);
			}
			return false;
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0002FDD4 File Offset: 0x0002DFD4
		public static bool IsShortcutArgumentContainsPackage(string shortcutPath, string packageName)
		{
			try
			{
				if (File.Exists(shortcutPath))
				{
					ShellLink shellLink = new ShellLink();
					((IPersistFile)shellLink).Load(shortcutPath, 0);
					StringBuilder stringBuilder = new StringBuilder(1000);
					((IShellLink)shellLink).GetArguments(stringBuilder, stringBuilder.Capacity);
					if (stringBuilder.ToString().ToLower(CultureInfo.InvariantCulture).Contains((packageName != null) ? packageName.ToLower(CultureInfo.InvariantCulture) : null))
					{
						return true;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Warning("Ignoring exception " + ex.ToString());
			}
			return false;
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0002FE70 File Offset: 0x0002E070
		public static void NotifyBootFailureToParentWindow(string className, string windowName, int exitCode, string vmName)
		{
			Logger.Info("Sending BOOT_FAILURE message to class = {0}, window = {1}", new object[]
			{
				className,
				windowName
			});
			IntPtr intPtr = InteropWindow.FindWindow(className, windowName);
			try
			{
				if (intPtr == IntPtr.Zero)
				{
					Logger.Info("Unable to find window : {0}", new object[]
					{
						className
					});
				}
				else
				{
					uint num;
					if (vmName == "Android")
					{
						num = 0U;
					}
					else
					{
						num = (uint)int.Parse((vmName != null) ? vmName.Split(new char[]
						{
							'_'
						})[1] : null, CultureInfo.InvariantCulture);
					}
					Logger.Info("Sending wparam : {0} and lparam : {1}", new object[]
					{
						(uint)exitCode,
						num
					});
					InteropWindow.SendMessage(intPtr, 1037U, (IntPtr)((long)((ulong)exitCode)), (IntPtr)((long)((ulong)num)));
					Logger.Info("Sent BOOT_FAILURE message");
				}
			}
			catch (Exception ex)
			{
				Logger.Error(string.Format(CultureInfo.InvariantCulture, "Error Occured, Err: {0}", new object[]
				{
					ex.ToString()
				}));
			}
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0000C948 File Offset: 0x0000AB48
		public static bool IsDesktopPC()
		{
			return Utils.GetSystemMetrics(86) == 0;
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0002FF78 File Offset: 0x0002E178
		public static bool CopyRecursive(string srcPath, string dstPath)
		{
			bool result = true;
			try
			{
				Logger.Info("Copying {0} to {1}", new object[]
				{
					srcPath,
					dstPath
				});
				if (!Directory.Exists(dstPath))
				{
					Directory.CreateDirectory(dstPath);
				}
				DirectoryInfo directoryInfo = new DirectoryInfo(srcPath);
				foreach (FileInfo fileInfo in directoryInfo.GetFiles())
				{
					fileInfo.CopyTo(Path.Combine(dstPath, fileInfo.Name), true);
				}
				foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
				{
					if (!Utils.CopyRecursive(Path.Combine(srcPath, directoryInfo2.Name), Path.Combine(dstPath, directoryInfo2.Name)))
					{
						result = false;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Warning("Ignoring exception in copy recursive src:{0} dst:{1}, exception:{2}", new object[]
				{
					srcPath,
					dstPath,
					ex.Message
				});
				result = false;
			}
			return result;
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0000C954 File Offset: 0x0000AB54
		public static bool IsNumberWithinRange(int number, int lowerLimit, int upperLimit, bool includeLowerLimit = true)
		{
			if (includeLowerLimit)
			{
				return number >= lowerLimit && number < upperLimit;
			}
			return number > lowerLimit && number < upperLimit;
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00030060 File Offset: 0x0002E260
		public static int GetAndroidVMMemory(bool defaultInstance = true)
		{
			Logger.Info("Getting Android VM Memory");
			ulong num = 1048576UL;
			int num2 = 0;
			int num3 = 600;
			int num4 = 3072;
			int num5 = 4096;
			int num6 = 5120;
			int num7 = 6144;
			int upperLimit = 8192;
			try
			{
				int num8 = (int)(SystemUtils.GetSystemTotalPhysicalMemory() / num);
				Logger.Info("Total RAM = {0} MB", new object[]
				{
					num8
				});
				if (SystemUtils.IsOs64Bit())
				{
					if (defaultInstance)
					{
						if (num8 < num4)
						{
							num2 = num3;
						}
						else if (Utils.IsNumberWithinRange(num8, num4, num5, true))
						{
							num2 = 900;
						}
						else if (Utils.IsNumberWithinRange(num8, num5, num6, true))
						{
							num2 = 1200;
						}
						else if (Utils.IsNumberWithinRange(num8, num6, num7, true))
						{
							num2 = 1500;
						}
						else if (Utils.IsNumberWithinRange(num8, num7, upperLimit, true))
						{
							num2 = 1800;
						}
						else
						{
							num2 = 2048;
						}
					}
					else if (num8 > 4000)
					{
						num2 = 1024;
					}
					else
					{
						num2 = 800;
					}
				}
				else if (num8 < num4 || !defaultInstance)
				{
					num2 = num3;
				}
				else
				{
					num2 = 900;
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Failed to check physical memory. Err: " + ex.ToString());
				num2 = 1200;
			}
			Logger.Info("Using RAM: {0}MB", new object[]
			{
				num2
			});
			return num2;
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x000301B8 File Offset: 0x0002E3B8
		public static void KillComServerSafe()
		{
			try
			{
				Logger.Info("KillComServerSafe()");
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(new WqlObjectQuery("SELECT * FROM Win32_Process WHERE Name = 'BstkSVC.exe'")))
				{
					if (managementObjectSearcher.Get().Count != 0)
					{
						Logger.Info("Found BstkSVC. Waiting for it to exit automatically...");
						Thread.Sleep(5000);
					}
					foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						string str = "Considering ";
						object obj = managementObject["ProcessId"];
						string str2 = (obj != null) ? obj.ToString() : null;
						string str3 = " -> ";
						object obj2 = managementObject["ExecutablePath"];
						Logger.Info(str + str2 + str3 + ((obj2 != null) ? obj2.ToString() : null));
						Process processById = Process.GetProcessById((int)((uint)managementObject["ProcessId"]));
						string text = (string)managementObject["ExecutablePath"];
						if (!string.IsNullOrEmpty(text))
						{
							string text2 = Directory.GetParent(text).ToString();
							string installDir = RegistryStrings.InstallDir;
							if (string.Compare(Path.GetFullPath(installDir).TrimEnd(new char[]
							{
								'\\'
							}), Path.GetFullPath(text2).TrimEnd(new char[]
							{
								'\\'
							}), StringComparison.InvariantCultureIgnoreCase) == 0)
							{
								Logger.Info("process BstkSVC not killed since the process Dir:{0} and Ignore Dir:{1} are same", new object[]
								{
									text2,
									installDir
								});
								continue;
							}
						}
						Logger.Info("Trying to kill BstkSvc PID " + processById.Id.ToString());
						processById.Kill();
						if (!processById.WaitForExit(10000))
						{
							Logger.Info("Timeout waiting for process to die");
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Exception in KillComServerSafe :" + ex.ToString());
			}
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x000303C0 File Offset: 0x0002E5C0
		public static bool CheckIfGuestReady(string vmName, int retries)
		{
			if (!Utils.sIsGuestReady.ContainsKey(vmName))
			{
				Utils.sIsGuestReady.Add(vmName, false);
			}
			if (!Utils.sIsGuestReady[vmName] && retries > 0)
			{
				while (retries > 0)
				{
					retries--;
					try
					{
						if (JObject.Parse(HTTPUtils.SendRequestToGuest("checkIfGuestReady", null, vmName, 1000, null, false, 1, 0, "bgp"))["result"].ToString().Equals("ok", StringComparison.OrdinalIgnoreCase))
						{
							Logger.Info("Guest is ready");
							Utils.sIsGuestReady[vmName] = true;
							return Utils.sIsGuestReady[vmName];
						}
						Thread.Sleep(1000);
					}
					catch (Exception)
					{
						Thread.Sleep(1000);
					}
				}
				Logger.Error("Guest is not ready now after all retries");
			}
			return Utils.sIsGuestReady[vmName];
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x000304AC File Offset: 0x0002E6AC
		public static List<string> GetRunningInstancesList()
		{
			List<string> list = new List<string>();
			foreach (string text in RegistryManager.Instance.VmList)
			{
				if (Utils.IsAndroidPlayerRunning(text, "bgp"))
				{
					list.Add(text);
				}
			}
			return list;
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0000C970 File Offset: 0x0000AB70
		private static bool CheckIfAndroidService(string serviceName)
		{
			return Regex.IsMatch(serviceName, "[bB]st[hH]d(Plus{1})?Android(_\\d+)?Svc");
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x000304F4 File Offset: 0x0002E6F4
		public static string GetUserAgent(string oem = "bgp")
		{
			if (string.IsNullOrEmpty(oem))
			{
				oem = "bgp";
			}
			string text = string.Format(CultureInfo.InvariantCulture, "{0}/{1}/{2} gzip", new object[]
			{
				"BlueStacks",
				"4.250.0.1070",
				RegistryManager.RegistryManagers[oem].UserGuid
			});
			Logger.Debug("UserAgent = " + text);
			byte[] bytes = Encoding.Default.GetBytes(text);
			return Encoding.UTF8.GetString(bytes);
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x00030574 File Offset: 0x0002E774
		public static Process StartHiddenFrontend(string vmName, string oem = "bgp")
		{
			if (string.Equals(oem, "dmm", StringComparison.InvariantCultureIgnoreCase))
			{
				string args = vmName + " -h";
				return ProcessUtils.StartExe(RegistryManager.Instance.PartnerExePath, args, false);
			}
			Process process;
			try
			{
				string fileName = Path.Combine(RegistryManager.RegistryManagers[oem].InstallDir, "HD-Player.exe");
				process = new Process();
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.FileName = fileName;
				process.StartInfo.Arguments = vmName + " -h";
				Logger.Info("Sending vmName for vm calling {0}", new object[]
				{
					vmName
				});
				Logger.Info("Utils: Starting hidden Frontend");
				process.Start();
			}
			catch (Exception ex)
			{
				process = null;
				Logger.Error("Error starting process" + ex.ToString());
			}
			return process;
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0003065C File Offset: 0x0002E85C
		public static Process StartFrontend(string vmName)
		{
			string fileName = Path.Combine(RegistryStrings.InstallDir, "HD-RunApp.exe");
			Process process = new Process();
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.FileName = fileName;
			process.StartInfo.Arguments = "-vmname:" + vmName;
			Logger.Info("Utils: Starting Frontend");
			process.Start();
			return process;
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x000306CC File Offset: 0x0002E8CC
		public static string GetMD5HashFromFile(string fileName)
		{
			try
			{
				return new _MD5
				{
					ValueAsFile = fileName
				}.FingerPrint.ToLower(CultureInfo.InvariantCulture);
			}
			catch (Exception ex)
			{
				Logger.Error("Error in creating md5 hash: " + ex.ToString());
			}
			return string.Empty;
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x00030728 File Offset: 0x0002E928
		public static bool IsCheckSumValid(string md5Compare, string filePath)
		{
			Logger.Info("Checking for valid checksum");
			string md5HashFromFile = Utils.GetMD5HashFromFile(filePath);
			Logger.Info("Computed MD5: " + md5HashFromFile);
			return string.Equals(md5Compare, md5HashFromFile, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00030764 File Offset: 0x0002E964
		public static string GetSystemFontName()
		{
			string result;
			try
			{
				using (new Font("Arial", 8f, FontStyle.Regular, GraphicsUnit.Point, 0))
				{
					result = "Arial";
				}
			}
			catch (Exception)
			{
				using (Label label = new Label())
				{
					try
					{
						using (new Font(label.Font.Name, 8f, FontStyle.Regular, GraphicsUnit.Point, 0))
						{
						}
					}
					catch (Exception)
					{
						if (Oem.Instance.IsMessageBoxToBeDisplayed)
						{
							MessageBox.Show("Failed to load Font set.", string.Format(CultureInfo.InvariantCulture, "{0} instance failed.", new object[]
							{
								Strings.ProductDisplayName
							}), MessageBoxButtons.OK, MessageBoxIcon.Hand);
						}
						Environment.Exit(-1);
					}
					result = label.Font.Name;
				}
			}
			return result;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0000C97D File Offset: 0x0000AB7D
		public static bool IsBlueStacksInstalled()
		{
			return !string.IsNullOrEmpty(RegistryManager.Instance.Version);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0000C993 File Offset: 0x0000AB93
		public static string GetLogoFile()
		{
			return Path.Combine(RegistryStrings.InstallDir, "ProductLogo.ico");
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x00030860 File Offset: 0x0002EA60
		public static void AddUploadTextToImage(string inputImage, string outputImage)
		{
			Image image = Image.FromFile(inputImage);
			int width = image.Width;
			int height = image.Height + 100;
			using (Bitmap bitmap = new Bitmap(width, height))
			{
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
				Image image2 = Image.FromFile(Utils.GetLogoFile());
				graphics.DrawImage(image2, new Rectangle(65, image.Height, 40, 40), new Rectangle(80, 0, image2.Width, 40), GraphicsUnit.Pixel);
				using (SolidBrush solidBrush = new SolidBrush(Color.White))
				{
					float width2 = (float)image.Width;
					float height2 = 80f;
					RectangleF layoutRectangle = new RectangleF(120f, (float)(image.Height + 7), width2, height2);
					using (Pen pen = new Pen(Color.Black))
					{
						graphics.DrawRectangle(pen, 120f, (float)image.Height, width2, height2);
						string snapShotShareString = Oem.Instance.SnapShotShareString;
						using (Font font = new Font("Arial", 14f))
						{
							graphics.DrawString(snapShotShareString, font, solidBrush, layoutRectangle);
							graphics.Save();
							image.Dispose();
							bitmap.Save(outputImage, ImageFormat.Jpeg);
						}
					}
				}
			}
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00030A28 File Offset: 0x0002EC28
		public static CmdRes RunCmd(string prog, string args, string outPath)
		{
			try
			{
				return Utils.RunCmdInternal(prog, args, outPath, true, false);
			}
			catch (Exception ex)
			{
				Logger.Error(ex.ToString());
			}
			return new CmdRes();
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00030A68 File Offset: 0x0002EC68
		public static string RunCmdNoLog(string prog, string args, int timeout)
		{
			string output2;
			using (Process process = new Process())
			{
				new CmdRes();
				string output = "";
				process.StartInfo.FileName = prog;
				process.StartInfo.Arguments = args;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.RedirectStandardInput = true;
				process.StartInfo.RedirectStandardOutput = true;
				process.OutputDataReceived += delegate(object obj, DataReceivedEventArgs line)
				{
					string text = line.Data;
					if (text != null && !string.IsNullOrEmpty(text = text.Trim()))
					{
						output = output + text + "\n";
					}
				};
				process.Start();
				process.BeginOutputReadLine();
				process.WaitForExit(timeout);
				output2 = output;
			}
			return output2;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00030B28 File Offset: 0x0002ED28
		private static CmdRes RunCmdInternal(string prog, string args, string outPath, bool enableLog, bool append = false)
		{
			CmdRes res3;
			using (StreamWriter writer = string.IsNullOrEmpty(outPath) ? null : new StreamWriter(outPath, append))
			{
				using (Process proc = new Process())
				{
					Logger.Info("Running Command");
					Logger.Info("    prog: " + prog);
					Logger.Info("    args: " + args);
					Logger.Info("    out:  " + outPath);
					CmdRes res = new CmdRes();
					proc.StartInfo.FileName = prog;
					proc.StartInfo.Arguments = args;
					proc.StartInfo.UseShellExecute = false;
					proc.StartInfo.CreateNoWindow = true;
					proc.StartInfo.RedirectStandardInput = true;
					proc.StartInfo.RedirectStandardOutput = true;
					proc.StartInfo.RedirectStandardError = true;
					proc.OutputDataReceived += delegate(object obj, DataReceivedEventArgs line)
					{
						if (outPath != null)
						{
							writer.WriteLine(line.Data);
						}
						string text = line.Data;
						if (text != null && !string.IsNullOrEmpty(text = text.Trim()))
						{
							if (enableLog)
							{
								Logger.Info(proc.Id.ToString() + " OUT: " + text);
							}
							CmdRes res2 = res;
							res2.StdOut = res2.StdOut + text + "\n";
						}
					};
					proc.ErrorDataReceived += delegate(object obj, DataReceivedEventArgs line)
					{
						if (outPath != null)
						{
							writer.WriteLine(line.Data);
						}
						if (enableLog)
						{
							Logger.Error(proc.Id.ToString() + " ERR: " + line.Data);
						}
						CmdRes res2 = res;
						res2.StdErr = res2.StdErr + line.Data + "\n";
					};
					proc.Start();
					proc.BeginOutputReadLine();
					proc.BeginErrorReadLine();
					proc.WaitForExit();
					res.ExitCode = proc.ExitCode;
					if (enableLog)
					{
						Logger.Info(proc.Id.ToString() + " ExitCode: " + proc.ExitCode.ToString());
					}
					if (outPath != null)
					{
						writer.Close();
					}
					res3 = res;
				}
			}
			return res3;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00030D60 File Offset: 0x0002EF60
		public static void RunCmdAsync(string prog, string args)
		{
			try
			{
				Utils.RunCmdAsyncInternal(prog, args);
			}
			catch (Exception ex)
			{
				Logger.Error(ex.ToString());
			}
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00030D94 File Offset: 0x0002EF94
		private static void RunCmdAsyncInternal(string prog, string args)
		{
			Process process = new Process();
			Logger.Info("Running Command Async");
			Logger.Info("    prog: " + prog);
			Logger.Info("    args: " + args);
			process.StartInfo.FileName = prog;
			process.StartInfo.Arguments = args;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.CreateNoWindow = true;
			process.Start();
		}

		// Token: 0x06000D6E RID: 3438 RVA: 0x00030E08 File Offset: 0x0002F008
		public static string GetPartnerExecutablePath()
		{
			string text = RegistryManager.Instance.PartnerExePath;
			if (string.IsNullOrEmpty(text))
			{
				text = Path.Combine(RegistryStrings.InstallDir, "BlueStacks.exe");
			}
			return text;
		}

		// Token: 0x06000D6F RID: 3439 RVA: 0x00030E3C File Offset: 0x0002F03C
		public static Process StartPartnerExe(string vm = "Android")
		{
			Process process = new Process();
			process.StartInfo.FileName = Utils.GetPartnerExecutablePath();
			process.StartInfo.Arguments = string.Format(CultureInfo.InvariantCulture, "-vmName={0}", new object[]
			{
				vm
			});
			process.StartInfo.UseShellExecute = false;
			process.Start();
			return process;
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0000C9A4 File Offset: 0x0000ABA4
		public static bool RestartBlueStacks()
		{
			MessageBox.Show(string.Format(CultureInfo.InvariantCulture, "Application restart required to use internet on {0}", new object[]
			{
				Strings.ProductDisplayName
			}), Strings.ProductDisplayName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			return true;
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x00030E98 File Offset: 0x0002F098
		public static void GetGuestWidthAndHeight(int sWidth, int sHeight, out int width, out int height)
		{
			if (Oem.Instance.OEM.Equals("yoozoo", StringComparison.OrdinalIgnoreCase))
			{
				width = 1280;
				height = 720;
				return;
			}
			if (sWidth > 1920 && sHeight > 1080)
			{
				width = 1920;
				height = 1080;
				return;
			}
			if (sWidth < 1280 && sHeight < 720)
			{
				width = 1280;
				height = 720;
				return;
			}
			width = sWidth;
			height = sHeight;
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x00030F10 File Offset: 0x0002F110
		public static void GetWindowWidthAndHeight(out int width, out int height)
		{
			int width2 = Screen.PrimaryScreen.Bounds.Width;
			int height2 = Screen.PrimaryScreen.Bounds.Height;
			if (width2 > 1920 && height2 > 1080)
			{
				width = 1920;
				height = 1080;
				return;
			}
			if (width2 > 1600 && height2 > 900)
			{
				width = 1600;
				height = 900;
				return;
			}
			if (width2 > 1280 && height2 > 720)
			{
				width = 1280;
				height = 720;
				return;
			}
			width = 960;
			height = 540;
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x00030FB0 File Offset: 0x0002F1B0
		public static void AddMessagingSupport(out Dictionary<string, string[]> oemWindowMapper)
		{
			oemWindowMapper = new Dictionary<string, string[]>();
			if (!string.IsNullOrEmpty(Oem.Instance.MsgWindowClassName) || !string.IsNullOrEmpty(Oem.Instance.MsgWindowTitle))
			{
				string[] value = new string[]
				{
					Oem.Instance.MsgWindowClassName,
					Oem.Instance.MsgWindowTitle
				};
				oemWindowMapper.Add(Oem.Instance.OEM, value);
			}
		}

		// Token: 0x06000D74 RID: 3444 RVA: 0x0000C9D2 File Offset: 0x0000ABD2
		public static bool IsUIProcessAlive(string vmName, string oem = "bgp")
		{
			return ProcessUtils.IsAlreadyRunning(Strings.GetPlayerLockName(vmName, oem)) || ProcessUtils.IsAlreadyRunning(Strings.GetBlueStacksUILockNameOem(oem));
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x0000C9EF File Offset: 0x0000ABEF
		public static bool IsAllUIProcessAlive(string vmName)
		{
			return ProcessUtils.IsAlreadyRunning(Strings.GetPlayerLockName(vmName, "bgp")) && ProcessUtils.IsAlreadyRunning("Global\\BlueStacks_BlueStacksUI_Lockbgp");
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0000CA0F File Offset: 0x0000AC0F
		public static bool IsAndroidPlayerRunning(string vmName, string oem = "bgp")
		{
			return ProcessUtils.IsAlreadyRunning(Strings.GetPlayerLockName(vmName, oem));
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x0000CA1D File Offset: 0x0000AC1D
		public static bool IsFileNullOrMissing(string file)
		{
			if (!File.Exists(file))
			{
				Logger.Info(file + " does not exist");
				return true;
			}
			if (new FileInfo(file).Length == 0L)
			{
				Logger.Info(file + " is null");
				return true;
			}
			return false;
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x0003101C File Offset: 0x0002F21C
		public static string GetUserGUID()
		{
			string text = null;
			string name = "Software\\\\BlueStacks";
			Logger.Info("Checking if guid present in HKCU");
			RegistryKey registryKey2;
			RegistryKey registryKey = registryKey2 = Registry.CurrentUser.OpenSubKey(name);
			try
			{
				if (registryKey != null)
				{
					text = (string)registryKey.GetValue("USER_GUID", null);
					if (text != null)
					{
						Logger.Info("Detected GUID in HKCU: " + text);
						return text;
					}
				}
			}
			finally
			{
				if (registryKey2 != null)
				{
					((IDisposable)registryKey2).Dispose();
				}
			}
			Logger.Info("Checking if guid present in HKLM");
			registryKey = (registryKey2 = Registry.LocalMachine.OpenSubKey(name));
			try
			{
				if (registryKey != null)
				{
					text = (string)registryKey.GetValue("USER_GUID", null);
					if (text != null)
					{
						Logger.Info("Detected User GUID in HKLM: " + text);
						return text;
					}
				}
			}
			finally
			{
				if (registryKey2 != null)
				{
					((IDisposable)registryKey2).Dispose();
				}
			}
			try
			{
				Logger.Info("Checking if guid present in %temp%");
				string environmentVariable = Environment.GetEnvironmentVariable("TEMP");
				Logger.Info("%TEMP% = " + environmentVariable);
				string path = Path.Combine(environmentVariable, "Bst_Guid_Backup");
				if (File.Exists(path))
				{
					string text2 = File.ReadAllText(path);
					if (!string.IsNullOrEmpty(text2))
					{
						text = text2;
						Logger.Info("Detected User GUID %temp%: " + text);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex.ToString());
			}
			return text;
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00031178 File Offset: 0x0002F378
		private static string GetOldPCode()
		{
			string path = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), "Bst_PCode_Backup");
			string text = "";
			if (File.Exists(path))
			{
				text = File.ReadAllText(path);
				if (!string.IsNullOrEmpty(text))
				{
					Logger.Info(string.Format(CultureInfo.InvariantCulture, "Old PCode = {0}", new object[]
					{
						text
					}));
				}
				try
				{
					File.Delete(path);
				}
				catch (Exception ex)
				{
					Logger.Info(string.Format(CultureInfo.InvariantCulture, "Ignoring Error Occured, Err: {0}", new object[]
					{
						ex.ToString()
					}));
				}
			}
			return text;
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x00031218 File Offset: 0x0002F418
		private static bool IsCACodeValid(string caCode)
		{
			string[] array = new string[]
			{
				"4",
				"20",
				"5",
				"14",
				"8",
				"2",
				"9",
				"36"
			};
			for (int i = 0; i < array.Length; i++)
			{
				if (string.Compare(array[i], caCode, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x0003128C File Offset: 0x0002F48C
		private static string GetOldCaCode()
		{
			string path = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), "Bst_CaCode_Backup");
			string text = "";
			if (File.Exists(path))
			{
				Logger.Info("the ca code temp file exists");
				text = File.ReadAllText(path);
				if (!string.IsNullOrEmpty(text))
				{
					Logger.Info(string.Format(CultureInfo.InvariantCulture, "Old CaCode = {0}", new object[]
					{
						text
					}));
				}
				try
				{
					File.Delete(path);
				}
				catch (Exception ex)
				{
					Logger.Warning(string.Format(CultureInfo.InvariantCulture, "Error Occured, Err: {0}", new object[]
					{
						ex.ToString()
					}));
				}
			}
			if (!Utils.IsCACodeValid(text))
			{
				text = "";
			}
			return text;
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x00031344 File Offset: 0x0002F544
		private static string GetOldCaSelector()
		{
			string path = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), "Bst_CaSelector_Backup");
			string text = "";
			if (File.Exists(path))
			{
				Logger.Info("the ca selector temp file exists");
				text = File.ReadAllText(path);
				if (!string.IsNullOrEmpty(text))
				{
					Logger.Info(string.Format(CultureInfo.InvariantCulture, "Old CaSelector = {0}", new object[]
					{
						text
					}));
				}
				try
				{
					File.Delete(path);
				}
				catch (Exception ex)
				{
					Logger.Warning(string.Format(CultureInfo.InvariantCulture, "Error Occured, Err: {0}", new object[]
					{
						ex.ToString()
					}));
				}
			}
			return text;
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x000313EC File Offset: 0x0002F5EC
		private static string GetRandomPCode()
		{
			string[] array = new string[]
			{
				"madw",
				"mtox",
				"optr",
				"pxln",
				"ofpn",
				"snpe",
				"segn",
				"ptxg"
			};
			int num = new Random().Next(array.Length);
			return array[num];
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x00031454 File Offset: 0x0002F654
		public static JObject JSonResponseFromCloud(string locale, string vmName, string campaignHash, string guid)
		{
			string url = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				RegistryManager.Instance.Host,
				"api/getcacode"
			});
			if (string.IsNullOrEmpty(guid))
			{
				guid = RegistryManager.Instance.UserGuid;
				campaignHash = RegistryManager.Instance.CampaignMD5;
			}
			Dictionary<string, string> data = new Dictionary<string, string>
			{
				{
					"locale",
					locale
				},
				{
					"guid",
					guid
				},
				{
					"campaign_hash",
					campaignHash
				}
			};
			string text = "";
			try
			{
				text = BstHttpClient.Post(url, data, null, false, vmName, 0, 1, 0, false, "bgp");
			}
			catch (Exception ex)
			{
				Logger.Error("An error occured while fetching info from cloud...Err : " + ex.ToString());
			}
			Logger.Info("Got resp: " + text);
			return JObject.Parse(text);
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x00031530 File Offset: 0x0002F730
		public static void GetCodesAndCountryInfo(out string code, out string pcode, out string country, out string caSelector, out string noChangesDroidG, out string pcodeFromCloud, out bool isCacodeValid, out string DNS, out string DNS2, out string abivalue, out string memAllocator, string locale, string upgradeDetected, string vmName, string campaignHash = "", string guid = "")
		{
			code = "";
			pcode = "";
			country = "";
			caSelector = "";
			abivalue = "15";
			memAllocator = string.Empty;
			noChangesDroidG = "";
			pcodeFromCloud = "";
			DNS = "";
			DNS2 = "";
			isCacodeValid = false;
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software");
			string text = (string)registryKey.GetValue("BstTestCA", "");
			string text2 = (string)registryKey.GetValue("BstTestPCode", "");
			string text3 = (string)registryKey.GetValue("BstTestCaSelector", "");
			string text4 = (string)registryKey.GetValue("BstTestNoChangesDroidG", "");
			if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
			{
				Logger.Info("Using test CA/P codes");
				code = text;
				pcode = text2;
				caSelector = text3;
				noChangesDroidG = text4;
			}
			else
			{
				string oldPCode = Utils.GetOldPCode();
				string oldCaCode = Utils.GetOldCaCode();
				string oldCaSelector = Utils.GetOldCaSelector();
				if (!string.IsNullOrEmpty(oldCaCode))
				{
					Logger.Info("The CaCode taken from temp file");
					code = oldCaCode;
					pcode = oldPCode;
					caSelector = oldCaSelector;
					if (!Oem.Instance.IsLoadCACodeFromCloud)
					{
						goto IL_4D4;
					}
					Logger.Info("noChangesDroidG requested from cloud");
					try
					{
						JObject jobject = Utils.JSonResponseFromCloud(locale, vmName, campaignHash, guid);
						if (jobject["success"].ToString().Trim() == "true")
						{
							if (jobject.ToString().Contains("p_code"))
							{
								pcodeFromCloud = jobject["p_code"].ToString().Trim();
							}
							if (jobject.ToString().Contains("no_changes_droidg"))
							{
								noChangesDroidG = jobject["no_changes_droidg"].ToString().Trim();
							}
							if (string.IsNullOrEmpty(caSelector) && string.IsNullOrEmpty(upgradeDetected) && jobject.ToString().Contains("ca_selector"))
							{
								caSelector = jobject["ca_selector"].ToString().Trim();
							}
							if (jobject.ContainsKey("is_valid_code"))
							{
								isCacodeValid = jobject["is_valid_code"].ToObject<bool>();
							}
							if (jobject.ContainsKey("dns"))
							{
								DNS = jobject["dns"].ToObject<string>();
							}
							if (jobject.ContainsKey("dns2"))
							{
								DNS = jobject["dns2"].ToObject<string>();
							}
							if (jobject.ContainsKey("abi_value"))
							{
								abivalue = jobject["abi_value"].ToObject<string>();
							}
							if (jobject.ContainsKey("malloc_value"))
							{
								memAllocator = jobject["malloc_value"].ToObject<string>();
							}
						}
						goto IL_4D4;
					}
					catch (Exception ex)
					{
						Logger.Error(ex.Message);
						goto IL_4D4;
					}
				}
				if (Oem.Instance.IsLoadCACodeFromCloud)
				{
					try
					{
						JObject jobject2 = Utils.JSonResponseFromCloud(locale, vmName, campaignHash, guid);
						if (jobject2["success"].ToString().Trim().Equals("true", StringComparison.InvariantCultureIgnoreCase))
						{
							code = jobject2["code"].ToString().Trim();
							if (jobject2.ToString().Contains("p_code"))
							{
								pcodeFromCloud = jobject2["p_code"].ToString().Trim();
							}
							if (string.IsNullOrEmpty(upgradeDetected))
							{
								pcode = pcodeFromCloud;
							}
							else
							{
								pcode = "";
							}
							if (jobject2.ToString().Contains("ca_selector"))
							{
								caSelector = jobject2["ca_selector"].ToString().Trim();
							}
							if (jobject2.ToString().Contains("no_changes_droidg"))
							{
								noChangesDroidG = jobject2["no_changes_droidg"].ToString().Trim();
							}
							if (jobject2.ContainsKey("is_valid_code"))
							{
								isCacodeValid = jobject2["is_valid_code"].ToObject<bool>();
							}
							if (jobject2.ContainsKey("abi_value"))
							{
								abivalue = jobject2["abi_value"].ToObject<string>();
							}
							if (jobject2.ContainsKey("malloc_value"))
							{
								memAllocator = jobject2["malloc_value"].ToObject<string>();
							}
						}
						else
						{
							pcode = "ofpn";
							code = "840";
							caSelector = "se_310260";
							Logger.Info("Setting default pcode = {0} cacode = {1} caselector = {2} ", new object[]
							{
								pcode,
								code,
								caSelector
							});
						}
						goto IL_4D4;
					}
					catch (Exception ex2)
					{
						Logger.Error("Failed to get cacode, pcode etc from cloud");
						Logger.Error(ex2.Message);
						pcode = "ofpn";
						code = "840";
						caSelector = "se_310260";
						Logger.Info("Setting default pcode = {0} cacode = {1} caselector = {2} ", new object[]
						{
							pcode,
							code,
							caSelector
						});
						goto IL_4D4;
					}
				}
				if (string.IsNullOrEmpty(upgradeDetected))
				{
					pcode = Utils.GetRandomPCode();
				}
				else
				{
					pcode = "";
				}
				code = "156";
				Logger.Info("cacode = {0} and pcode = {1}", new object[]
				{
					code,
					pcode
				});
			}
			IL_4D4:
			if (Oem.Instance.IsCountryChina)
			{
				country = "CN";
				caSelector = "se_46000";
				return;
			}
			country = Utils.GetUserCountry(vmName);
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00031A6C File Offset: 0x0002FC6C
		public static bool IsAndroidFeatureBitEnabled(uint featureBit, string vmName)
		{
			try
			{
				string bootParameters = RegistryManager.Instance.Guest[vmName].BootParameters;
				uint num = 0U;
				string[] array = bootParameters.Split(new char[]
				{
					' '
				});
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(new char[]
					{
						'='
					});
					if (array2[0] == "OEMFEATURES")
					{
						num = Convert.ToUInt32(array2[1], CultureInfo.InvariantCulture);
						break;
					}
				}
				Logger.Info("the android oem feature bits are" + num.ToString(CultureInfo.InvariantCulture));
				if ((num & featureBit) == 0U)
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Got error while checking for android bit, err:{0}", new object[]
				{
					ex.ToString()
				});
				return false;
			}
			return true;
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0000CA59 File Offset: 0x0000AC59
		public static void SetImeSelectedInReg(string imeSelected, string vmName)
		{
			RegistryManager.Instance.Guest[vmName].ImeSelected = imeSelected;
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x00031B40 File Offset: 0x0002FD40
		public static bool IsLatinImeSelected(string vmName)
		{
			string text = RegistryManager.Instance.Guest[vmName].ImeSelected;
			if (text.Equals("com.android.inputmethod.latin/.LatinIME", StringComparison.CurrentCultureIgnoreCase))
			{
				Logger.Info("LatinIme is selected");
				return true;
			}
			if (string.IsNullOrEmpty(text))
			{
				try
				{
					Logger.Info("IME selected in registry is null, query currentImeId");
					string text2 = HTTPUtils.SendRequestToGuest("getCurrentIMEID", null, vmName, 5000, null, false, 1, 0, "bgp");
					Logger.Debug("Response: {0}", new object[]
					{
						text2
					});
					text = JObject.Parse(text2)["currentIme"].ToString();
					Logger.Info("The currentIme: {0}", new object[]
					{
						text
					});
					if (text.Equals("com.android.inputmethod.latin/.LatinIME", StringComparison.CurrentCultureIgnoreCase))
					{
						Utils.SetImeSelectedInReg(text, vmName);
						return true;
					}
				}
				catch (Exception ex)
				{
					Logger.Error("Got exception in checking CurrentImeSelected, ex : {0}", new object[]
					{
						ex.ToString()
					});
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0000CA71 File Offset: 0x0000AC71
		public static bool IsForcePcImeForLang(string locale)
		{
			if (locale != null && locale.Equals("vi-VN", StringComparison.OrdinalIgnoreCase))
			{
				Logger.Info("the system locale is vi-vn, using pcime workflow");
				return true;
			}
			return false;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0000CA91 File Offset: 0x0000AC91
		public static bool IsEastAsianLanguage(string lang)
		{
			return new List<string>
			{
				"zh-CN",
				"ja-JP",
				"ko-KR"
			}.Contains(lang);
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00031C38 File Offset: 0x0002FE38
		public static bool WaitForSyncConfig(string vmName)
		{
			int i = 240;
			while (i > 0)
			{
				i--;
				if (RegistryManager.Instance.Guest[vmName].ConfigSynced != 0)
				{
					Logger.Info("Config is synced now");
					return true;
				}
				Logger.Info("Config not sycned, wait 1 second and try again");
				Thread.Sleep(1000);
			}
			return false;
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00031C90 File Offset: 0x0002FE90
		public static bool WaitForFrontendPingResponse(string vmName)
		{
			Logger.Info("In method WaitForFrontendPingResponse for vmName: " + vmName);
			int i = 50;
			while (i > 0)
			{
				i--;
				try
				{
					string text = HTTPUtils.SendRequestToEngine("pingVm", null, vmName, 1000, null, false, 1, 0, "", "bgp");
					Logger.Debug("Response: " + text);
					if ((JArray.Parse(text)[0] as JObject)["success"].ToObject<bool>())
					{
						Logger.Info("Frontend server running");
						return true;
					}
					Thread.Sleep(1000);
				}
				catch (Exception)
				{
					Thread.Sleep(1000);
				}
			}
			Logger.Error("Frontend server not running after {0} retries", new object[]
			{
				i
			});
			return false;
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x00031D64 File Offset: 0x0002FF64
		public static bool WaitForAgentPingResponse(string vmName, string oem = "bgp")
		{
			Logger.Info("In WaitForAgentPingResponse");
			int i = 50;
			while (i > 0)
			{
				i--;
				try
				{
					if ((JArray.Parse(HTTPUtils.SendRequestToAgent("ping", null, vmName, 1000, null, false, 1, 0, oem, false))[0] as JObject)["success"].ToObject<bool>())
					{
						Logger.Info("Agent server running");
						return true;
					}
					Thread.Sleep(200);
				}
				catch (Exception)
				{
					Thread.Sleep(200);
					if (i <= 40 && !ProcessUtils.IsLockInUse("Global\\BlueStacks_HDAgent_Lockbgp"))
					{
						return false;
					}
				}
			}
			Logger.Info("Agent server not running after {0} retries", new object[]
			{
				i
			});
			return false;
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x0000CAC4 File Offset: 0x0000ACC4
		public static bool WaitForBootComplete(string vmName, string oem = "bgp")
		{
			return Utils.WaitForBootComplete(vmName, 180, oem);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00031E28 File Offset: 0x00030028
		public static bool WaitForBootComplete(string vmName, int retries, string oem = "bgp")
		{
			if (!Utils.OemVmLockNamedata.ContainsKey(vmName + "_" + oem))
			{
				Utils.OemVmLockNamedata.Add(vmName + "_" + oem, new object());
			}
			if (!Utils.sIsGuestBooted.ContainsKey(vmName + "_" + oem))
			{
				Utils.sIsGuestBooted.Add(vmName + "_" + oem, false);
			}
			object obj = Utils.OemVmLockNamedata[vmName + "_" + oem];
			lock (obj)
			{
				Logger.Info("Checking if guest booted or not for {0} retries", new object[]
				{
					retries
				});
				while (retries > 0)
				{
					retries--;
					if (Utils.IsGuestBooted(vmName, oem))
					{
						return true;
					}
					Thread.Sleep(1000);
				}
				Logger.Info("Guest not booted after {0} retries", new object[]
				{
					retries
				});
			}
			return false;
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00031F24 File Offset: 0x00030124
		public static bool IsSharedFolderMounted(string vmName)
		{
			try
			{
				if (!Utils.sIsSharedFolderMounted.ContainsKey(vmName))
				{
					Utils.sIsSharedFolderMounted.Add(vmName, false);
				}
				if (!Utils.sIsSharedFolderMounted[vmName] && JObject.Parse(HTTPUtils.SendRequestToGuest("isSharedFolderMounted", null, vmName, 1000, null, false, 1, 0, "bgp"))["result"].ToString().Equals("ok", StringComparison.InvariantCultureIgnoreCase))
				{
					Utils.sIsSharedFolderMounted[vmName] = true;
					return true;
				}
			}
			catch (Exception ex)
			{
				Logger.Info("shared folder not mounted yet." + ex.Message);
			}
			return Utils.sIsSharedFolderMounted[vmName];
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00031FDC File Offset: 0x000301DC
		public static bool SetCustomAppSize(string vmName, string package, ScreenMode mode)
		{
			string text = "";
			try
			{
				JObject jobject = new JObject
				{
					{
						"package_name",
						package
					},
					{
						"screen_mode",
						mode.ToString()
					}
				};
				Dictionary<string, string> data = new Dictionary<string, string>
				{
					{
						"d",
						jobject.ToString(Formatting.None, new JsonConverter[0])
					}
				};
				text = HTTPUtils.SendRequestToGuest("setcustomappsize", data, vmName, 1000, null, false, 1, 0, "bgp");
				if (JObject.Parse(text)["result"].ToString().Equals("ok", StringComparison.InvariantCultureIgnoreCase))
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				Logger.Error(string.Concat(new string[]
				{
					"Error in sending setCustomAppSize to android response: ",
					text,
					" ",
					Environment.NewLine,
					" message: ",
					ex.Message
				}));
			}
			return false;
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x000320E0 File Offset: 0x000302E0
		public static void SendKeymappingFiledownloadRequest(string packageName, string vmName)
		{
			try
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				string value = "{\"pkgName\":\"" + packageName + "\"}";
				dictionary.Add("action", "com.bluestacks.DOWNLOAD_KEY_MAPPING_SERVICE");
				dictionary.Add("extras", value);
				Logger.Info("Sending request to android for downloading keymapping file for pkg " + packageName);
				HTTPUtils.SendRequestToGuest("customStartService", dictionary, vmName, 0, null, false, 10, 500, "bgp");
			}
			catch (Exception ex)
			{
				Logger.Error("Exception in SendKeymappingFiledownloadRequest: {0}", new object[]
				{
					ex.Message
				});
			}
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x0003217C File Offset: 0x0003037C
		public static bool IsGuestBooted(string vmName, string oem = "bgp")
		{
			try
			{
				if (!Utils.sIsGuestBooted.ContainsKey(vmName + "_" + oem))
				{
					Utils.sIsGuestBooted.Add(vmName + "_" + oem, false);
				}
				if (!Utils.sIsGuestBooted[vmName + "_" + oem] && (bool)JArray.Parse(HTTPUtils.SendRequestToEngine("checkIfGuestBooted", null, vmName, 100, null, false, 1, 0, "", oem))[0]["success"])
				{
					Utils.sIsGuestBooted[vmName + "_" + oem] = true;
					return true;
				}
			}
			catch (Exception ex)
			{
				Logger.Info("Guest not booted yet." + ex.Message);
			}
			return Utils.sIsGuestBooted[vmName + "_" + oem];
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00032264 File Offset: 0x00030464
		public static void ExtractImages(string targetDir, string resourceName)
		{
			try
			{
				Directory.Delete(targetDir, true);
			}
			catch (Exception)
			{
			}
			if (!Directory.Exists(targetDir))
			{
				Directory.CreateDirectory(targetDir);
			}
			ResourceManager resourceManager;
			try
			{
				resourceManager = new ResourceManager(resourceName, Assembly.GetExecutingAssembly());
			}
			catch (Exception ex)
			{
				Logger.Error("Failed to extract resources. err: " + ex.ToString());
				return;
			}
			Image image = (Image)resourceManager.GetObject("bg", CultureInfo.InvariantCulture);
			image.Save(Path.Combine(targetDir, "bg.jpg"), ImageFormat.Jpeg);
			bool flag = true;
			try
			{
				image = (Image)resourceManager.GetObject("HomeScreen", CultureInfo.InvariantCulture);
				image.Save(Path.Combine(targetDir, "HomeScreen.jpg"), ImageFormat.Jpeg);
			}
			catch (Exception)
			{
				flag = false;
			}
			try
			{
				image = (Image)resourceManager.GetObject("ThankYouImage", CultureInfo.InvariantCulture);
				image.Save(Path.Combine(targetDir, "ThankYouImage.jpg"), ImageFormat.Jpeg);
			}
			catch (Exception)
			{
			}
			int num = 0;
			try
			{
				for (;;)
				{
					num++;
					image = (Image)resourceManager.GetObject("SetupImage" + Convert.ToString(num, CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
					image.Save(Path.Combine(targetDir, "SetupImage" + Convert.ToString(num, CultureInfo.InvariantCulture) + ".jpg"), ImageFormat.Jpeg);
					if (!flag && num == 1)
					{
						image.Save(Path.Combine(targetDir, "HomeScreen.jpg"), ImageFormat.Jpeg);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00032404 File Offset: 0x00030604
		public static string DownloadIcon(string package, string directory = "", bool isReDownload = false)
		{
			string format = "https://cloud.bluestacks.com/app/icon?pkg={0}&fallback=false";
			string url = string.Format(CultureInfo.InvariantCulture, format, new object[]
			{
				package
			});
			string fileNameWithExtension = package + ".png";
			return Utils.TinyDownloader(url, fileNameWithExtension, directory, isReDownload);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00032440 File Offset: 0x00030640
		public static string TinyDownloader(string url, string fileNameWithExtension, string directory = "", bool isReDownload = false)
		{
			string text = string.Empty;
			try
			{
				if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(fileNameWithExtension))
				{
					string path = Regex.Replace(fileNameWithExtension, "[\\x22\\\\\\/:*?|<>]", " ");
					if (string.IsNullOrEmpty(directory))
					{
						directory = RegistryStrings.GadgetDir;
					}
					text = Path.Combine(directory, path);
					if (!Directory.Exists(Directory.GetParent(text).FullName))
					{
						Directory.CreateDirectory(Directory.GetParent(text).FullName);
					}
					if (!File.Exists(text) || isReDownload)
					{
						using (WebClient webClient = new WebClient())
						{
							webClient.DownloadFile(url, text);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Cannot download icon file" + ex.ToString());
			}
			return text;
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0003250C File Offset: 0x0003070C
		public static string GetDNS2Value(string oem)
		{
			string result = "8.8.8.8";
			if (string.Compare(oem, "tc_dt", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(oem, "china", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(oem, "china_api", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(oem, "ucweb_dt", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(oem, "4399", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(oem, "anquicafe", StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(oem, "yy_dt", StringComparison.OrdinalIgnoreCase) == 0)
			{
				result = "114.114.114.114";
			}
			return result;
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00032588 File Offset: 0x00030788
		public static bool IsInstallOrUpgradeRequired()
		{
			if (!Utils.IsBlueStacksInstalled())
			{
				return true;
			}
			string version = RegistryManager.Instance.Version;
			if (string.IsNullOrEmpty(version))
			{
				return true;
			}
			string version2 = version.Substring(0, version.LastIndexOf('.')) + ".0";
			string version3 = "4.250.0.1070".Substring(0, "4.250.0.1070".LastIndexOf('.')) + ".0";
			Version v = new Version(version2);
			Version v2 = new Version(version3);
			Logger.Info("Installed Version: {0}, new version: {1}", new object[]
			{
				version,
				"4.250.0.1070"
			});
			if (v2 > v)
			{
				Logger.Info("IMP: lower version: {0} is already installed. Forcing upgrade.", new object[]
				{
					version
				});
				return true;
			}
			return false;
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0000CAD2 File Offset: 0x0000ACD2
		public static void SendBrowserVersionStats(string version, string vmName)
		{
			new Thread(delegate()
			{
				try
				{
					string userGuid = RegistryManager.Instance.UserGuid;
					string url = "https://bluestacks-cloud.appspot.com/stats/ieversionstats";
					Dictionary<string, string> data = new Dictionary<string, string>
					{
						{
							"ie_ver",
							version
						},
						{
							"guid",
							userGuid
						},
						{
							"prod_ver",
							"4.250.0.1070"
						}
					};
					Logger.Info("Sending browser version Stats");
					string text = BstHttpClient.Post(url, data, null, false, vmName, 0, 1, 0, false, "bgp");
					Logger.Info("Got browser version stat response: {0}", new object[]
					{
						text
					});
				}
				catch (Exception ex)
				{
					Logger.Error("Failed to send app stats. error: " + ex.ToString());
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0003263C File Offset: 0x0003083C
		public static bool IsRemoteFilePresent(string url)
		{
			bool result = true;
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.Method = "Head";
			try
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				if (httpWebResponse.StatusCode == HttpStatusCode.NotFound)
				{
					result = false;
				}
				httpWebResponse.Close();
			}
			catch (Exception ex)
			{
				result = false;
				Logger.Error("Could not make http request: " + ex.ToString());
			}
			return result;
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x000326B0 File Offset: 0x000308B0
		public static string ConvertToIco(string imagePath, string iconsDir)
		{
			Logger.Info("Converting {0}", new object[]
			{
				imagePath
			});
			string fileName = Path.GetFileName(imagePath);
			int length = fileName.LastIndexOf(".", StringComparison.OrdinalIgnoreCase);
			string path = fileName.Substring(0, length) + ".ico";
			string text = Path.Combine(iconsDir, path);
			IconHelper.ConvertToIcon(imagePath, text, 256, false);
			return text;
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x00032710 File Offset: 0x00030910
		public static void ResizeImage(string imagePath)
		{
			bool flag = false;
			using (FileStream fileStream = File.OpenRead(imagePath))
			{
				using (Image image = Image.FromStream(fileStream))
				{
					int num = image.Width;
					int num2 = image.Height;
					if (num >= 256)
					{
						int num3 = 256;
						num2 = (int)((float)num2 / ((float)num / (float)num3));
						num = num3;
						flag = true;
					}
					if (num2 >= 256)
					{
						int num4 = 256;
						num = (int)((float)num / ((float)num2 / (float)num4));
						num2 = num4;
						flag = true;
					}
					if (num % 8 != 0)
					{
						num -= num % 8;
						flag = true;
					}
					if (num2 % 8 != 0)
					{
						num2 -= num2 % 8;
						flag = true;
					}
					if (flag)
					{
						using (Image image2 = new Bitmap(num, num2))
						{
							Graphics graphics = Graphics.FromImage(image2);
							graphics.SmoothingMode = SmoothingMode.AntiAlias;
							graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
							graphics.DrawImage(image, 0, 0, image2.Width, image2.Height);
							File.Delete(imagePath);
							image2.Save(imagePath);
						}
					}
				}
			}
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0000CB03 File Offset: 0x0000AD03
		public static int GetSystemHeight()
		{
			return Utils.GetSystemMetrics(1);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0000CB0B File Offset: 0x0000AD0B
		public static int GetSystemWidth()
		{
			return Utils.GetSystemMetrics(0);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0000CB13 File Offset: 0x0000AD13
		public static int GetBstCommandProcessorPort(string vmName)
		{
			return RegistryManager.Instance.Guest[vmName].BstAndroidPort;
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x00032830 File Offset: 0x00030A30
		public static bool IsHomeApp(string appInfo)
		{
			return appInfo == null || appInfo.IndexOf("com.bluestacks.appmart", StringComparison.OrdinalIgnoreCase) != -1 || (appInfo == null || appInfo.IndexOf("com.android.launcher2", StringComparison.OrdinalIgnoreCase) != -1) || (appInfo == null || appInfo.IndexOf("com.uncube.launcher", StringComparison.OrdinalIgnoreCase) != -1) || (appInfo == null || appInfo.IndexOf("com.bluestacks.gamepophome", StringComparison.OrdinalIgnoreCase) != -1);
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x0000CB2A File Offset: 0x0000AD2A
		public static bool IsValidEmail(string email)
		{
			return new Regex("^(([^<>()[\\]\\\\.,;:\\s@\\\"]+(\\.[^<>()[\\]\\\\.,;:\\s@\\\"]+)*)|(\\\".+\\\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$").IsMatch(email);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0000CB3C File Offset: 0x0000AD3C
		public static string GetFileURI(string path)
		{
			return new Uri(path).AbsoluteUri;
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x000328A8 File Offset: 0x00030AA8
		public static string PostToBstCmdProcessorAfterServiceStart(string path, Dictionary<string, string> data, string vmName, bool isLaunchUI = true)
		{
			string result = null;
			if (!Utils.IsAllUIProcessAlive(vmName) && isLaunchUI)
			{
				Logger.Info("Starting Frontend in hidden mode.");
				Utils.StartHiddenFrontend(vmName, "bgp");
				Stats.SendMiscellaneousStatsAsyncForDMM(Stats.DMMEvent.client_launched.ToString(), path, null, null, null, "Android", 0);
			}
			int retries = 300;
			if (!Utils.CheckIfGuestReady(vmName, retries))
			{
				Stats.SendMiscellaneousStatsAsyncForDMM(Stats.DMMEvent.boot_failed.ToString(), "checkIfGuestReady", null, null, null, "Android", 0);
				return new JObject
				{
					{
						"result",
						"error"
					},
					{
						"reason",
						"Guest boot failed"
					}
				}.ToString(Formatting.None, new JsonConverter[0]);
			}
			Stats.SendMiscellaneousStatsAsyncForDMM(Stats.DMMEvent.boot_success.ToString(), "checkIfGuestReady", null, null, null, "Android", 0);
			try
			{
				result = HTTPUtils.SendRequestToGuest(path, data, vmName, 0, null, false, 1, 0, "bgp");
			}
			catch (Exception ex)
			{
				Logger.Error("Exception in PostAfterServiceStart");
				Logger.Error(ex.Message);
			}
			return result;
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x000329C8 File Offset: 0x00030BC8
		public static string GetToBstCmdProcessorAfterServiceStart(string path, string vmName)
		{
			string result = null;
			if (!Utils.IsUIProcessAlive(vmName, "bgp"))
			{
				Logger.Info("Starting Frontend in hidden mode.");
				using (Process process = Utils.StartHiddenFrontend(vmName, "bgp"))
				{
					process.WaitForExit(60000);
				}
			}
			try
			{
				result = HTTPUtils.SendRequestToGuest(path, null, vmName, 0, null, false, 1, 0, "bgp");
			}
			catch (Exception ex)
			{
				Logger.Error("Exception in GetToBstCmdProcessorAfterServiceStart");
				Logger.Error(ex.Message);
			}
			return result;
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x00032A5C File Offset: 0x00030C5C
		public static bool IsAppInstalled(string package, string vmName, out string version)
		{
			version = "";
			string text;
			return Utils.IsAppInstalled(package, vmName, out version, out text, true);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x00032A7C File Offset: 0x00030C7C
		public static bool IsAppInstalled(string package, string vmName, out string version, out string failReason, bool isLaunchUI = true)
		{
			Logger.Info("Utils: IsAppInstalled Called for package {0}", new object[]
			{
				package
			});
			version = "";
			failReason = "App not installed";
			bool flag = false;
			try
			{
				Dictionary<string, string> data = new Dictionary<string, string>
				{
					{
						"package",
						package
					}
				};
				string text = Utils.PostToBstCmdProcessorAfterServiceStart("isPackageInstalled", data, vmName, isLaunchUI);
				Logger.Info("Got response: {0}", new object[]
				{
					text
				});
				if (string.IsNullOrEmpty(text))
				{
					failReason = "The Api failed to get a response";
				}
				else
				{
					JObject jobject = JObject.Parse(text);
					string strA = jobject["result"].ToString().Trim();
					if (string.Compare(strA, "ok", StringComparison.OrdinalIgnoreCase) == 0)
					{
						flag = true;
						version = jobject["version"].ToString().Trim();
						Stats.SendMiscellaneousStatsAsyncForDMM(Stats.DMMEvent.is_app_installed.ToString(), "success", package, version, null, "Android", 0);
					}
					else if (string.Compare(strA, "error", StringComparison.OrdinalIgnoreCase) == 0)
					{
						failReason = jobject["reason"].ToString().Trim();
						Stats.SendMiscellaneousStatsAsyncForDMM(Stats.DMMEvent.is_app_installed.ToString(), "failed", package, failReason, null, "Android", 0);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(string.Format(CultureInfo.InvariantCulture, "Error Occured, Err: {0}", new object[]
				{
					ex.ToString()
				}));
				failReason = ex.Message;
			}
			Logger.Info("Installed = {0}", new object[]
			{
				flag
			});
			return flag;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00032C18 File Offset: 0x00030E18
		private static string FilterSystemApps(JArray packages, out bool isSamsungStorePresent)
		{
			isSamsungStorePresent = false;
			JArray jarray = new JArray();
			foreach (JObject jobject in packages.Children<JObject>())
			{
				if (jobject["package"].ToString().Trim().Equals("com.sec.android.app.samsungapps", StringComparison.Ordinal))
				{
					isSamsungStorePresent = true;
				}
				if (string.Compare(jobject["systemapp"].ToString().Trim(), "0", StringComparison.OrdinalIgnoreCase) == 0)
				{
					bool flag = true;
					for (int i = 0; i < Utils.sListIgnoredApps.Count; i++)
					{
						if (string.Compare(jobject["package"].ToString().Trim(), Utils.sListIgnoredApps[i], StringComparison.OrdinalIgnoreCase) == 0)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						JObject item = new JObject
						{
							{
								"package",
								jobject["package"].ToString().Trim()
							},
							{
								"version",
								jobject["version"].ToString().Trim()
							},
							{
								"appname",
								jobject["appname"].ToString().Trim()
							},
							{
								"gl3required",
								jobject["gl3required"].ToString().Trim()
							}
						};
						jarray.Add(item);
					}
				}
			}
			return jarray.ToString(Formatting.None, new JsonConverter[0]);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00032DC8 File Offset: 0x00030FC8
		public static string GetInstalledPackages(string vmName, out string failReason, out bool isSamsungStorePresent, int count = 0)
		{
			Logger.Info("Utils: GetInstalledPackages Called for VM: {0}", new object[]
			{
				vmName
			});
			failReason = "Unable to get list of installed apps";
			isSamsungStorePresent = false;
			string text = "";
			try
			{
				string toBstCmdProcessorAfterServiceStart = Utils.GetToBstCmdProcessorAfterServiceStart("installedPackages", vmName);
				Logger.Info("Got response: {0}", new object[]
				{
					toBstCmdProcessorAfterServiceStart
				});
				if (string.IsNullOrEmpty(toBstCmdProcessorAfterServiceStart))
				{
					failReason = "The Api failed to get a response";
				}
				else
				{
					JObject jobject = JObject.Parse(toBstCmdProcessorAfterServiceStart);
					string strA = jobject["result"].ToString().Trim();
					if (string.Compare(strA, "ok", StringComparison.OrdinalIgnoreCase) == 0)
					{
						failReason = "";
						text = Utils.FilterSystemApps(jobject["installed_packages"] as JArray, out isSamsungStorePresent);
						Logger.Info("Filtered results: {0}", new object[]
						{
							text
						});
					}
					else if (string.Compare(strA, "error", StringComparison.OrdinalIgnoreCase) == 0)
					{
						failReason = jobject["reason"].ToString().Trim();
						if (string.Compare(failReason, "system not ready", StringComparison.OrdinalIgnoreCase) == 0 && count < 6)
						{
							Thread.Sleep(500);
							return Utils.GetInstalledPackages(vmName, out failReason, out isSamsungStorePresent, count++);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(string.Format(CultureInfo.InvariantCulture, "Error Occurred, Err: {0}", new object[]
				{
					ex.ToString()
				}));
				failReason = ex.Message;
			}
			return text;
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x00032F28 File Offset: 0x00031128
		public static string GetInstalledPackagesFromAppsJSon(string vmName)
		{
			try
			{
				List<string> installedAppsList = JsonParser.GetInstalledAppsList(vmName);
				return string.Join(",", installedAppsList.ToArray());
			}
			catch (Exception ex)
			{
				Logger.Error("Couldn't get installed app list. Ex: {0}", new object[]
				{
					ex
				});
			}
			return string.Empty;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x00032F80 File Offset: 0x00031180
		public static AppInfo GetPackageDetails(string vmName, string package, bool videoPresent, out string failReason)
		{
			AppInfo result = null;
			try
			{
				string text = Utils.PostToBstCmdProcessorAfterServiceStart("getPackageDetails", new Dictionary<string, string>
				{
					{
						"package",
						package
					}
				}, vmName, true);
				if (string.IsNullOrEmpty(text))
				{
					failReason = "The api failed to get a response";
				}
				else
				{
					JObject jobject = JObject.Parse(text);
					if (string.Compare(jobject["result"].ToString().Trim(), "ok", StringComparison.OrdinalIgnoreCase) == 0)
					{
						failReason = "";
						JArray jarray = JArray.Parse(jobject["activities"].ToString());
						result = new AppInfo(jobject["name"].ToString().Trim(), jarray[0]["img"].ToString().Trim(), jobject["package"].ToString().Trim(), (jarray[0] as JObject)["activity"].ToString().Trim(), "0", "no", jobject["version"].ToString().Trim(), jobject["gl3required"].ToObject<bool>(), videoPresent, jobject["versionName"].ToString().Trim(), false);
					}
					else
					{
						failReason = "The api failed to get a response";
					}
				}
			}
			catch (Exception ex)
			{
				failReason = ex.Message;
			}
			return result;
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x000330F0 File Offset: 0x000312F0
		public static void SyncAppJson(string vmName)
		{
			Logger.Info("In SyncAppJson");
			if (Utils.sIsSyncAppJsonComplete)
			{
				return;
			}
			try
			{
				string value;
				bool flag;
				string installedPackages = Utils.GetInstalledPackages(vmName, out value, out flag, 0);
				if (flag && !RegistryManager.Instance.IsSamsungStorePresent)
				{
					RegistryManager.Instance.IsSamsungStorePresent = true;
					HTTPUtils.SendRequestToClient("reloadPromotions", null, vmName, 0, null, false, 1, 0, "bgp");
				}
				if (string.IsNullOrEmpty(value))
				{
					JArray jarray = JArray.Parse(installedPackages);
					JsonParser jsonParser = new JsonParser(vmName);
					AppInfo[] appList = jsonParser.GetAppList();
					List<AppInfo> list = appList.ToList<AppInfo>();
					bool flag2 = false;
					bool videoPresent = false;
					using (IEnumerator<JObject> enumerator = jarray.Children<JObject>().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							JObject installedAppsJsonObj = enumerator.Current;
							string package = installedAppsJsonObj["package"].ToString().Trim();
							if (jsonParser.GetAppInfoFromPackageName(package) != null)
							{
								if (!string.Equals(JsonParser.GetGl3RequirementFromPackage(appList, package).ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture).Trim(), installedAppsJsonObj["gl3required"].ToString().ToLower(CultureInfo.InvariantCulture).Trim(), StringComparison.OrdinalIgnoreCase))
								{
									flag2 = true;
									(from x in list
									where x.Package == package
									select x).FirstOrDefault<AppInfo>().Gl3Required = installedAppsJsonObj["gl3required"].ToObject<bool>();
								}
								videoPresent = JsonParser.GetVideoPresentRequirementFromPackage(appList, package);
								AppInfo appInfo3 = (from x in list
								where x.Package == package
								select x).FirstOrDefault<AppInfo>();
								try
								{
									appInfo3.VideoPresent = installedAppsJsonObj["videopresent"].ToObject<bool>();
								}
								catch
								{
								}
							}
							if (!appList.Any((AppInfo _) => string.Compare(_.Package.Trim(), installedAppsJsonObj["package"].ToString().Trim(), StringComparison.OrdinalIgnoreCase) == 0))
							{
								flag2 = true;
								AppInfo packageDetails = Utils.GetPackageDetails(vmName, installedAppsJsonObj["package"].ToString().Trim(), videoPresent, out value);
								if (packageDetails != null)
								{
									list.Add(packageDetails);
								}
							}
						}
					}
					if (jarray.Count != list.Count || flag2)
					{
						List<string> list2 = new List<string>();
						using (List<AppInfo>.Enumerator enumerator2 = list.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								AppInfo appInfo = enumerator2.Current;
								if (!jarray.Children<JObject>().Any((JObject _) => string.Compare(_["package"].ToString().Trim(), appInfo.Package.Trim(), StringComparison.OrdinalIgnoreCase) == 0))
								{
									list2.Add(appInfo.Package);
									flag2 = true;
								}
							}
						}
						using (List<string>.Enumerator enumerator3 = list2.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								string package = enumerator3.Current;
								list.RemoveAll((AppInfo _) => _.Package == package);
							}
						}
						if (appList.Length != list.Count)
						{
							flag2 = true;
							list = new List<AppInfo>(jarray.Count);
							foreach (JObject jobject in jarray.Children<JObject>())
							{
								AppInfo packageDetails2 = Utils.GetPackageDetails(vmName, jobject["package"].ToString().Trim(), false, out value);
								if (packageDetails2 != null)
								{
									list.Add(packageDetails2);
								}
							}
							Logger.Info("Updating App Json from apps received from android. Details: " + installedPackages);
						}
					}
					foreach (AppInfo appInfo2 in list)
					{
						bool flag3 = Utils.CheckGamepadCompatible(appInfo2.Package);
						if (appInfo2.IsGamepadCompatible != flag3)
						{
							appInfo2.IsGamepadCompatible = flag3;
							flag2 = true;
						}
					}
					if (flag2)
					{
						jsonParser.WriteJson(list.ToArray());
						try
						{
							Dictionary<string, string> data = new Dictionary<string, string>();
							HTTPUtils.SendRequestToClient("appJsonChanged", data, vmName, 0, null, false, 1, 0, "bgp");
						}
						catch (Exception ex)
						{
							Logger.Error("Exception while sending appsync update to client: " + ex.ToString());
						}
					}
					Utils.sIsSyncAppJsonComplete = true;
				}
			}
			catch (Exception ex2)
			{
				Logger.Warning(string.Format(CultureInfo.InvariantCulture, "Unable to sync app.json file for vm:{0}. " + ex2.ToString(), new object[]
				{
					vmName
				}));
			}
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x00033630 File Offset: 0x00031830
		public static bool CheckGamepadCompatible(string packageName)
		{
			try
			{
				string inputmapperFile = Utils.GetInputmapperFile(packageName);
				bool flag = false;
				if (!string.IsNullOrEmpty(inputmapperFile))
				{
					string text = "";
					using (Mutex mutex = new Mutex(false, "BlueStacks_CfgAccess"))
					{
						if (mutex.WaitOne())
						{
							try
							{
								text = File.ReadAllText(inputmapperFile);
								flag = true;
							}
							catch (Exception arg)
							{
								Logger.Error(string.Format("Failed to read cfg file... filepath: {0} Err : {1}", inputmapperFile, arg));
							}
							finally
							{
								mutex.ReleaseMutex();
							}
						}
					}
					if (flag)
					{
						foreach (string value in Constants.ImapGamepadEvents)
						{
							if (text.Contains(value))
							{
								return true;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Exception in CheckGamepadCompatible: " + ex.ToString());
			}
			return false;
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00033728 File Offset: 0x00031928
		public static string GetInputmapperFile(string packageName = "")
		{
			string result = string.Empty;
			try
			{
				if (File.Exists(Utils.GetInputmapperUserFilePath(packageName)))
				{
					result = Utils.GetInputmapperUserFilePath(packageName);
				}
				else if (File.Exists(Utils.GetInputmapperDefaultFilePath(packageName)))
				{
					result = Utils.GetInputmapperDefaultFilePath(packageName);
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Excpetion in GetInputMapper: " + ex.ToString());
			}
			return result;
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0000CB49 File Offset: 0x0000AD49
		public static string GetInputmapperUserFilePath(string packageName)
		{
			return Path.Combine(Path.Combine(RegistryStrings.InputMapperFolder, "UserFiles"), packageName + ".cfg");
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0000CB6A File Offset: 0x0000AD6A
		public static string GetInputmapperDefaultFilePath(string packageName)
		{
			return Path.Combine(RegistryStrings.InputMapperFolder, packageName + ".cfg");
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x00033790 File Offset: 0x00031990
		public static bool UnsupportedProcessor()
		{
			try
			{
				Logger.Info("Checking if Processor Unsupported");
				string[] array = new string[]
				{
					"AMD64 Family 21 Model 16 Stepping 1 AuthenticAMD"
				};
				string text = Path.Combine(Path.GetTempPath(), "SystemInfo.txt");
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				Utils.RunCmd("SystemInfo", null, text);
				string text2 = File.ReadAllText(text);
				foreach (string value in array)
				{
					if (text2.IndexOf(value, StringComparison.OrdinalIgnoreCase) != -1)
					{
						return true;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error in Checking if Processor Unsupported : {0}", new object[]
				{
					ex.ToString()
				});
			}
			return false;
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x00033840 File Offset: 0x00031A40
		public static bool ReserveHTTPPorts()
		{
			bool result = false;
			try
			{
				string str = new SecurityIdentifier("S-1-1-0").Translate(typeof(NTAccount)).ToString();
				string cmd = "netsh.exe";
				int num = 2861;
				int num2 = 2971;
				Logger.Info("Reserving ports {0} - {1}", new object[]
				{
					num,
					num2
				});
				Logger.Info("---------------------------------------------------------------");
				bool flag = false;
				for (int i = num; i < num2; i++)
				{
					try
					{
						RunCommand.RunCmd(cmd, string.Format(CultureInfo.InvariantCulture, "http add urlacl url=http://*:{0}/ User=\\\"" + str + "\"", new object[]
						{
							i
						}), flag, flag, false, 0);
					}
					catch (Exception ex)
					{
						Logger.Error(string.Format(CultureInfo.InvariantCulture, "Error occured, Err: {0}", new object[]
						{
							ex.ToString()
						}));
					}
					flag = (i % 10 == 0);
				}
				result = true;
			}
			catch (Exception ex2)
			{
				Logger.Error("Error in reserving HTTP ports: {0}", new object[]
				{
					ex2.ToString()
				});
				result = false;
			}
			Logger.Info("---------------------------------------------------------------");
			return result;
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00033980 File Offset: 0x00031B80
		public static void RestartService(string serviceName, int timeoutMilliseconds)
		{
			Logger.Info("Restarting {0} service", new object[]
			{
				serviceName
			});
			using (ServiceController serviceController = new ServiceController(serviceName))
			{
				try
				{
					int tickCount = Environment.TickCount;
					TimeSpan timeout = TimeSpan.FromMilliseconds((double)timeoutMilliseconds);
					serviceController.Stop();
					serviceController.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
					int tickCount2 = Environment.TickCount;
					timeout = TimeSpan.FromMilliseconds((double)(timeoutMilliseconds - (tickCount2 - tickCount)));
					serviceController.Start();
					serviceController.WaitForStatus(ServiceControllerStatus.Running, timeout);
				}
				catch (Exception ex)
				{
					Logger.Error("Error in restarting service " + ex.ToString());
				}
			}
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00033A28 File Offset: 0x00031C28
		public static bool CheckOpenGlSupport(out int glRenderMode, out string glVendor, out string glRenderer, out string glVersion, string blueStacksProgramFiles)
		{
			Logger.Info("In CheckSupportedGlRenderMode");
			glRenderMode = 4;
			glVersion = "";
			glRenderer = "";
			glVendor = "";
			Logger.Info("Running glcheck from folder : " + blueStacksProgramFiles);
			Logger.Info("Checking for glRenderMode 1");
			if (Utils.GetGraphicsInfo(Path.Combine(blueStacksProgramFiles, "HD-GLCheck.exe"), "1", out glVendor, out glRenderer, out glVersion) == 0)
			{
				glRenderMode = 1;
				return true;
			}
			Logger.Info("Opengl not supported.");
			return false;
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0000CB81 File Offset: 0x0000AD81
		public static int GetCurrentGraphicsInfo(string args, out string glVendor, out string glRenderer, out string glVersion)
		{
			return Utils.GetGraphicsInfo(Path.Combine(RegistryStrings.InstallDir, "HD-GLCheck.exe"), args, out glVendor, out glRenderer, out glVersion, true);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0000CB9C File Offset: 0x0000AD9C
		public static int GetGraphicsInfo(string prog, string args, out string glVendor, out string glRenderer, out string glVersion)
		{
			return Utils.GetGraphicsInfo(prog, args, out glVendor, out glRenderer, out glVersion, true);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00033AA0 File Offset: 0x00031CA0
		public static int GetGraphicsInfo(string prog, string args, out string glVendor, out string glRenderer, out string glVersion, bool enableLogging)
		{
			Logger.Info("Will run " + prog + " with args " + args);
			string vendor = "";
			string renderer = "";
			string version = "";
			glVendor = vendor;
			glRenderer = renderer;
			glVersion = version;
			int result = -1;
			Environment.GetEnvironmentVariable("TEMP");
			try
			{
				using (Process proc = new Process())
				{
					proc.StartInfo.FileName = prog;
					proc.StartInfo.Arguments = args;
					proc.StartInfo.UseShellExecute = false;
					proc.StartInfo.CreateNoWindow = true;
					proc.StartInfo.RedirectStandardOutput = true;
					proc.OutputDataReceived += delegate(object sender, DataReceivedEventArgs outLine)
					{
						try
						{
							string text = (outLine.Data != null) ? outLine.Data : "";
							if (enableLogging)
							{
								Logger.Info(proc.Id.ToString() + " OUT: " + text);
							}
							if (text.Contains("GL_VENDOR ="))
							{
								int num2 = text.IndexOf('=');
								vendor = text.Substring(num2 + 1).Trim();
								vendor = vendor.Replace(";", ";;");
							}
							if (text.Contains("GL_RENDERER ="))
							{
								int num2 = text.IndexOf('=');
								renderer = text.Substring(num2 + 1).Trim();
								renderer = renderer.Replace(";", ";;");
							}
							if (text.Contains("GL_VERSION ="))
							{
								int num2 = text.IndexOf('=');
								version = text.Substring(num2 + 1).Trim();
								version = version.Replace(";", ";;");
							}
						}
						catch (Exception ex2)
						{
							Logger.Error("A crash occured in the GLCheck delegate");
							Logger.Error(ex2.ToString());
						}
					};
					proc.Start();
					proc.BeginOutputReadLine();
					int num = 10000;
					bool flag = proc.WaitForExit(num);
					glVendor = vendor;
					glRenderer = renderer;
					glVersion = version;
					if (flag)
					{
						Logger.Info(proc.Id.ToString() + " EXIT: " + proc.ExitCode.ToString());
						result = proc.ExitCode;
					}
					else
					{
						Logger.Error("Process killed after timeout: {0}s", new object[]
						{
							num / 1000
						});
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Some error while running graphics check. Ex: {0}", new object[]
				{
					ex
				});
			}
			return result;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x00033C9C File Offset: 0x00031E9C
		public static int CheckSsse3Info(string prog, out string ssse3Supported)
		{
			Logger.Info("Will run " + prog);
			int result = -1;
			string ssse3value = "";
			try
			{
				using (Process process = new Process())
				{
					int num = 10000;
					process.StartInfo.FileName = prog;
					process.StartInfo.UseShellExecute = false;
					process.StartInfo.CreateNoWindow = true;
					process.StartInfo.RedirectStandardOutput = true;
					process.StartInfo.RedirectStandardError = true;
					Countdown countDown = new Countdown(2);
					process.OutputDataReceived += delegate(object sender, DataReceivedEventArgs outLine)
					{
						if (outLine.Data != null)
						{
							try
							{
								string data = outLine.Data;
								if (data.Contains("value ="))
								{
									int num2 = data.IndexOf('=');
									ssse3value = data.Substring(num2 + 1).Trim();
								}
							}
							catch (Exception ex2)
							{
								Logger.Error("A crash occured in check cpu info delegate");
								Logger.Error(ex2.ToString());
							}
							Logger.Info(Path.GetFileName(prog) + ": " + outLine.Data);
							return;
						}
						countDown.Signal();
					};
					process.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs outLine)
					{
						if (outLine.Data != null)
						{
							Logger.Error(Path.GetFileName(prog) + ": " + outLine.Data);
							return;
						}
						countDown.Signal();
					};
					process.Start();
					process.BeginOutputReadLine();
					process.BeginErrorReadLine();
					bool flag = process.WaitForExit(num);
					countDown.Wait();
					if (flag)
					{
						Logger.Info(process.Id.ToString() + " EXIT: " + process.ExitCode.ToString());
						result = process.ExitCode;
					}
					else
					{
						Logger.Error("Process killed after timeout: {0}s", new object[]
						{
							num / 1000
						});
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Some error while running graphics check. Ex: {0}", new object[]
				{
					ex
				});
			}
			if (ssse3value == "1" || string.IsNullOrEmpty(ssse3value))
			{
				ssse3Supported = "1";
			}
			else
			{
				ssse3Supported = "0";
			}
			return result;
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00033E5C File Offset: 0x0003205C
		public static bool CheckTwoCameraPresentOnDevice(ref bool bBothCamera)
		{
			bool result;
			try
			{
				Guid guid = new Guid("{53F56307-B6BF-11D0-94F2-00A0C91EFB8B}");
				int num = Utils.SetupDiGetClassDevs(ref guid, IntPtr.Zero, IntPtr.Zero, ClassDevsFlags.DIGCF_PRESENT | ClassDevsFlags.DIGCF_ALLCLASSES);
				int num2 = -1;
				int num3 = 0;
				int num4 = 0;
				while (num2 != 0)
				{
					SP_DEVINFO_DATA sp_DEVINFO_DATA = default(SP_DEVINFO_DATA);
					sp_DEVINFO_DATA.cbSize = Marshal.SizeOf(sp_DEVINFO_DATA);
					num2 = Utils.SetupDiEnumDeviceInfo(num, num3, ref sp_DEVINFO_DATA);
					if (num2 == 1 && Utils.GetRegistryProperty(num, ref sp_DEVINFO_DATA, RegPropertyType.SPDRP_CLASSGUID).Equals("{6bdd1fc6-810f-11d0-bec7-08002be2092f}", StringComparison.OrdinalIgnoreCase))
					{
						num4++;
						if (num4 == 2)
						{
							bBothCamera = true;
						}
					}
					num3++;
					if (bBothCamera)
					{
						Logger.Info("Both Camera present on Device");
						break;
					}
				}
				result = true;
			}
			catch (Exception ex)
			{
				result = false;
				Logger.Info("Exception when trying to check Camera present on Device");
				Logger.Info(ex.ToString());
			}
			return result;
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00033F28 File Offset: 0x00032128
		private static string GetRegistryProperty(int PnPHandle, ref SP_DEVINFO_DATA DeviceInfoData, RegPropertyType Property)
		{
			int num = 0;
			DATA_BUFFER data_BUFFER = default(DATA_BUFFER);
			Utils.SetupDiGetDeviceRegistryProperty(PnPHandle, ref DeviceInfoData, Property, IntPtr.Zero, ref data_BUFFER, 1024, ref num);
			return data_BUFFER.Buffer;
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0000CBAA File Offset: 0x0000ADAA
		public static int CallApkInstaller(string apkPath, bool isSilentInstall)
		{
			return Utils.CallApkInstaller(apkPath, isSilentInstall, null);
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x00033F5C File Offset: 0x0003215C
		public static int CallApkInstaller(string apkPath, bool isSilentInstall, string vmName)
		{
			Logger.Info("Installing apk :{0} vm name :{1} ", new object[]
			{
				apkPath,
				vmName
			});
			if (vmName == null)
			{
				vmName = "Android";
			}
			int result = -1;
			try
			{
				string installDir = RegistryStrings.InstallDir;
				ProcessStartInfo processStartInfo = new ProcessStartInfo();
				if (string.Equals(Path.GetExtension(apkPath), ".xapk", StringComparison.InvariantCultureIgnoreCase))
				{
					processStartInfo.FileName = Path.Combine(installDir, "HD-XapkHandler.exe");
					if (isSilentInstall)
					{
						processStartInfo.Arguments = string.Format(CultureInfo.InvariantCulture, "-xapk \"{0}\" -s -vmname {1}", new object[]
						{
							apkPath,
							vmName
						});
					}
					else
					{
						processStartInfo.Arguments = string.Format(CultureInfo.InvariantCulture, "-xapk \"{0}\" -vmname {1}", new object[]
						{
							apkPath,
							vmName
						});
					}
				}
				else
				{
					processStartInfo.FileName = Path.Combine(installDir, "HD-ApkHandler.exe");
					if (isSilentInstall)
					{
						processStartInfo.Arguments = string.Format(CultureInfo.InvariantCulture, "-apk \"{0}\" -s -vmname {1}", new object[]
						{
							apkPath,
							vmName
						});
					}
					else
					{
						processStartInfo.Arguments = string.Format(CultureInfo.InvariantCulture, "-apk \"{0}\" -vmname {1}", new object[]
						{
							apkPath,
							vmName
						});
					}
				}
				processStartInfo.UseShellExecute = false;
				processStartInfo.CreateNoWindow = true;
				Logger.Info("Console: installer path {0}", new object[]
				{
					processStartInfo.FileName
				});
				Process process = Process.Start(processStartInfo);
				process.WaitForExit();
				result = process.ExitCode;
				Logger.Info("Console: apk installer exit code: {0}", new object[]
				{
					process.ExitCode
				});
			}
			catch (Exception ex)
			{
				Logger.Info("Error Installing Apk : " + ex.ToString());
			}
			return result;
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0000CBB4 File Offset: 0x0000ADB4
		public static string GetInstallStatsUrl()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				RegistryManager.Instance.Host,
				"stats/bsinstallstats"
			});
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x000340FC File Offset: 0x000322FC
		public static Dictionary<string, string> GetUserData()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string version = RegistryManager.Instance.Version;
			string registeredEmail = RegistryManager.Instance.RegisteredEmail;
			if (!string.IsNullOrEmpty(registeredEmail))
			{
				dictionary.Add("email", registeredEmail);
			}
			long num = DateTime.UtcNow.Ticks - 621355968000000000L;
			string value = (num / 10000000L).ToString(CultureInfo.InvariantCulture);
			dictionary.Add("user_time", value);
			return dictionary;
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x00034178 File Offset: 0x00032378
		public static bool IsForegroundApplication()
		{
			bool result = false;
			IntPtr foregroundWindow = InteropWindow.GetForegroundWindow();
			if (foregroundWindow != IntPtr.Zero)
			{
				uint num = 0U;
				InteropWindow.GetWindowThreadProcessId(foregroundWindow, ref num);
				if ((ulong)num == (ulong)((long)Process.GetCurrentProcess().Id))
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x000341B8 File Offset: 0x000323B8
		public static bool CheckWritePermissionForFolder(string DirectoryPath)
		{
			if (string.IsNullOrEmpty(DirectoryPath))
			{
				return false;
			}
			bool result;
			try
			{
				using (File.Create(Path.Combine(DirectoryPath, Path.GetRandomFileName()), 1, FileOptions.DeleteOnClose))
				{
				}
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0003421C File Offset: 0x0003241C
		public static void UpdateRegistry(string registryKey, string name, object value, RegistryValueKind kind)
		{
			try
			{
				RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey(registryKey, true);
				registryKey2.SetValue(name, value, kind);
				registryKey2.Close();
				registryKey2.Flush();
			}
			catch (Exception ex)
			{
				Logger.Error("Exception occured in UpdateRegistry " + ex.ToString());
				throw;
			}
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00034274 File Offset: 0x00032474
		public static Icon GetApplicationIcon()
		{
			if (RegistryManager.Instance.InstallationType == InstallationTypes.GamingEdition)
			{
				return new Icon(Path.Combine(RegistryStrings.InstallDir, "app_icon.ico"));
			}
			string productIconCompletePath = RegistryStrings.ProductIconCompletePath;
			if (File.Exists(productIconCompletePath))
			{
				return new Icon(productIconCompletePath);
			}
			return Icon.ExtractAssociatedIcon(Application.ExecutablePath);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0000CBE0 File Offset: 0x0000ADE0
		public static bool IsHDPlusDebugMode()
		{
			return RegistryManager.Instance.PlusDebug != 0;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0000CBF1 File Offset: 0x0000ADF1
		public static int GetGMStreamWindowWidth()
		{
			return 320 * SystemUtils.GetDPI() / 96;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000342C4 File Offset: 0x000324C4
		public static void SetCurrentEngineStateAndGlTransportValue(EngineState state, string vmName)
		{
			Logger.Info("Setting CurrentEngineState: " + state.ToString());
			RegistryManager.Instance.CurrentEngine = state.ToString();
			string bootParameters = RegistryManager.Instance.Guest[vmName].BootParameters;
			string[] array = bootParameters.Split(new char[]
			{
				' '
			});
			string text = "";
			string text2 = "GlTransport";
			int num;
			if (state == EngineState.legacy)
			{
				num = RegistryManager.Instance.GlLegacyTransportConfig;
			}
			else
			{
				num = RegistryManager.Instance.GlPlusTransportConfig;
			}
			Logger.Info("setting GlValue to {0}", new object[]
			{
				num
			});
			if (bootParameters.IndexOf(text2, StringComparison.OrdinalIgnoreCase) == -1)
			{
				text = string.Concat(new string[]
				{
					bootParameters,
					" ",
					text2,
					"=",
					num.ToString()
				});
			}
			else
			{
				foreach (string text3 in array)
				{
					if (text3.IndexOf(text2, StringComparison.OrdinalIgnoreCase) != -1)
					{
						if (!string.IsNullOrEmpty(text))
						{
							text += " ";
						}
						text = text + text2 + "=" + num.ToString();
					}
					else
					{
						if (!string.IsNullOrEmpty(text))
						{
							text += " ";
						}
						text += text3;
					}
				}
			}
			RegistryManager.Instance.Guest[vmName].BootParameters = text;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x00034434 File Offset: 0x00032634
		public static bool RegisterComExe(string path, bool register)
		{
			bool result;
			try
			{
				result = (Utils.RunCmd(path, register ? "/RegServer" : "/UnregServer", null).ExitCode == 0);
			}
			catch (Exception ex)
			{
				Logger.Error("Command runner raised an exception: " + ex.ToString());
				result = false;
			}
			return result;
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00034490 File Offset: 0x00032690
		public static string GetCurrentKeyboardLayout()
		{
			string result;
			try
			{
				result = new CultureInfo(Utils.GetKeyboardLayout(Utils.GetWindowThreadProcessId(Utils.GetForegroundWindow(), IntPtr.Zero)).ToInt32() & 65535).Name;
			}
			catch
			{
				result = "en-US";
			}
			return result;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x000344E8 File Offset: 0x000326E8
		public static bool IsEngineRaw()
		{
			bool result = false;
			try
			{
				if (JObject.Parse(RegistryManager.Instance.DeviceCaps)["engine_enabled"].ToString().Trim() == EngineState.raw.ToString())
				{
					result = true;
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error Occured, Err: " + ex.ToString());
			}
			Logger.Info("Engine mode Raw: " + result.ToString());
			return result;
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00034574 File Offset: 0x00032774
		public static string GetCampaignName()
		{
			string result = "";
			try
			{
				string campaignJson = RegistryManager.Instance.CampaignJson;
				if (string.IsNullOrEmpty(campaignJson))
				{
					return result;
				}
				JObject jobject = JObject.Parse(campaignJson);
				if (jobject != null)
				{
					result = jobject["campaign_name"].ToString();
				}
			}
			catch (Exception)
			{
				Logger.Warning("Failed to get campaign name.");
			}
			return result;
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x000345DC File Offset: 0x000327DC
		public static string GetUserCountry(string vmName)
		{
			string result;
			try
			{
				string text = BstHttpClient.Get(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
				{
					RegistryManager.Instance.Host,
					"api/getcountryforip"
				}), null, false, vmName, 0, 1, 0, false, "bgp");
				Logger.Info("Got resp: " + text);
				result = JObject.Parse(text)["country"].ToString().Trim();
			}
			catch (Exception ex)
			{
				Logger.Error(ex.Message);
				result = "";
			}
			return result;
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x00034678 File Offset: 0x00032878
		public static void KillComServer()
		{
			Logger.Info("In KillComServer");
			string fullPath = Path.GetFullPath(RegistryStrings.InstallDir + "\\");
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(new WqlObjectQuery("SELECT * FROM Win32_Process WHERE Name = 'BstkSVC.exe'")))
			{
				foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
				{
					ManagementObject managementObject = (ManagementObject)managementBaseObject;
					string str = "Considering ";
					object obj = managementObject["ProcessId"];
					string str2 = (obj != null) ? obj.ToString() : null;
					string str3 = " -> ";
					object obj2 = managementObject["ExecutablePath"];
					Logger.Info(str + str2 + str3 + ((obj2 != null) ? obj2.ToString() : null));
					if (string.Compare(Path.GetFullPath(Path.GetDirectoryName((string)managementObject["ExecutablePath"]) + "\\"), fullPath, StringComparison.OrdinalIgnoreCase) == 0)
					{
						Process processById = Process.GetProcessById((int)((uint)managementObject["ProcessId"]));
						Logger.Info("Trying to kill PID " + processById.Id.ToString());
						processById.Kill();
						if (!processById.WaitForExit(10000))
						{
							Logger.Info("Timeout waiting for process to die");
						}
					}
				}
			}
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x000347D4 File Offset: 0x000329D4
		public static void StopClientInstanceAsync(string vmName)
		{
			try
			{
				Logger.Info("Will send request stopInstance to " + vmName);
				List<string> list = new List<string>
				{
					vmName
				};
				if (string.IsNullOrEmpty(vmName))
				{
					list = RegistryManager.Instance.VmList.ToList<string>();
				}
				foreach (string text in list)
				{
					try
					{
						Dictionary<string, string> data = new Dictionary<string, string>
						{
							{
								"vmName",
								text
							}
						};
						HTTPUtils.SendRequestToClientAsync("stopInstance", data, text, 0, null, false, 1, 0, "bgp");
					}
					catch (Exception ex)
					{
						Logger.Warning("Exception in closing client for vm: {0} --> {1}", new object[]
						{
							text,
							ex.Message
						});
					}
				}
			}
			catch (Exception ex2)
			{
				Logger.Error("Exception in closing any frontend: " + ex2.ToString());
			}
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x000348D0 File Offset: 0x00032AD0
		public static void StopFrontend(string vmName, bool isWaitForPlayerClosing = true)
		{
			try
			{
				Logger.Info("Will send request shutdown" + vmName);
				List<string> list = new List<string>
				{
					vmName
				};
				if (string.IsNullOrEmpty(vmName))
				{
					list = RegistryManager.Instance.VmList.ToList<string>();
				}
				foreach (string text in list)
				{
					try
					{
						Dictionary<string, string> data = new Dictionary<string, string>
						{
							{
								"vmName",
								text
							}
						};
						bool flag;
						using (Mutex mutex = new Mutex(true, Strings.GetPlayerLockName(text, "bgp"), ref flag))
						{
							if (!flag)
							{
								HTTPUtils.SendRequestToEngineAsync("shutdown", data, text, 0, null, false, 1, 0, "bgp");
								if (isWaitForPlayerClosing)
								{
									try
									{
										if (!mutex.WaitOne(60000))
										{
											HTTPUtils.SendRequestToEngine("forceShutdown", null, "Android", 0, null, false, 1, 0, "", "bgp");
										}
									}
									catch (AbandonedMutexException ex)
									{
										Logger.Info("Player closed: " + ex.Message);
									}
									catch (Exception ex2)
									{
										Logger.Error("Could not check if player is running." + ex2.Message);
									}
								}
							}
						}
					}
					catch (Exception ex3)
					{
						Logger.Warning("Exception in closing any frontend for vm = " + text + " -->" + ex3.ToString());
					}
				}
			}
			catch (Exception ex4)
			{
				Logger.Error("Exception in closing any frontend: " + ex4.ToString());
			}
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00034ACC File Offset: 0x00032CCC
		public static bool CheckIfAndroidBstkExistAndValid(string vmName)
		{
			Logger.Info("Checking if android bstk exist and valid");
			string text = Path.Combine(Path.Combine(RegistryStrings.DataDir, vmName), vmName + ".bstk");
			if (File.Exists(text))
			{
				if (new FileInfo(text).Length == 0L)
				{
					return false;
				}
				try
				{
					new XDocument();
					XDocument.Load(text);
					return true;
				}
				catch (Exception ex)
				{
					Logger.Error("Exception in parsing bstk file" + ex.ToString());
					return false;
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00034B58 File Offset: 0x00032D58
		public static void CreateBstkFileFromPrev(string vmName)
		{
			Logger.Info("Creating Bstk file from Bstk-Prev file");
			string path = Path.Combine(RegistryStrings.DataDir, vmName);
			string destFileName = Path.Combine(path, vmName + ".bstk");
			string text = Path.Combine(path, vmName + ".bstk-prev");
			if (!File.Exists(text))
			{
				Logger.Info("android.bstk-prev file not exist");
				return;
			}
			File.Copy(text, destFileName, true);
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00034BB8 File Offset: 0x00032DB8
		public static bool IsFirstVersionHigher(string firstVersion, string secondVersion)
		{
			string[] array = (firstVersion != null) ? firstVersion.Split(new char[]
			{
				'.'
			}) : null;
			string[] array2 = (secondVersion != null) ? secondVersion.Split(new char[]
			{
				'.'
			}) : null;
			bool flag = false;
			int i = 0;
			int num = Math.Min(array.Length, array2.Length);
			while (i < num)
			{
				long num2 = Convert.ToInt64(array[i], CultureInfo.InvariantCulture);
				long num3 = Convert.ToInt64(array2[i], CultureInfo.InvariantCulture);
				long num4 = num2 - num3;
				if (num4 > 0L)
				{
					flag = true;
					break;
				}
				if (num4 < 0L)
				{
					break;
				}
				i++;
			}
			if (!flag && i < array.Length && i == array2.Length)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00034C54 File Offset: 0x00032E54
		public static bool IsRunningInstanceClashWithAnotherInstance(string procName)
		{
			string installDir = RegistryStrings.InstallDir;
			string clientInstallDir = RegistryManager.Instance.ClientInstallDir;
			if (string.IsNullOrEmpty(installDir) && string.IsNullOrEmpty(clientInstallDir))
			{
				return false;
			}
			procName = ((procName != null) ? procName.Replace(".exe", "") : null);
			List<string> applicationPath = GetProcessExecutionPath.GetApplicationPath(Process.GetProcessesByName(procName));
			Logger.Debug("Number of running instances for the process {0} are {1} ", new object[]
			{
				procName,
				applicationPath.Count
			});
			foreach (string path in applicationPath)
			{
				try
				{
					string directoryName = Path.GetDirectoryName(path);
					if (!directoryName.Equals(installDir.TrimEnd(new char[]
					{
						'\\'
					}), StringComparison.InvariantCultureIgnoreCase) && !directoryName.Equals(clientInstallDir.TrimEnd(new char[]
					{
						'\\'
					}), StringComparison.InvariantCultureIgnoreCase))
					{
						return true;
					}
				}
				catch
				{
				}
			}
			return false;
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00034D60 File Offset: 0x00032F60
		public static int GetVideoControllersNum()
		{
			int num = 0;
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController"))
				{
					ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
					num = managementObjectCollection.Count;
					Logger.Info("Win32_VideoController query count: ", new object[]
					{
						num
					});
					foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
					{
						foreach (PropertyData propertyData in ((ManagementObject)managementBaseObject).Properties)
						{
							string name = propertyData.Name;
							if (name != null)
							{
								if (!(name == "Description"))
								{
									if (!(name == "DriverVersion"))
									{
										if (name == "DriverDate")
										{
											Logger.Info("DriverDate: {0}", new object[]
											{
												ManagementDateTimeConverter.ToDateTime(propertyData.Value.ToString()).ToUniversalTime().ToString("yyyy-MM-dd HH-mm-ss", DateTimeFormatInfo.InvariantInfo)
											});
										}
									}
									else
									{
										Logger.Info("DriverVersion: {0}", new object[]
										{
											propertyData.Value
										});
									}
								}
								else
								{
									Logger.Info("Description (Name): {0}", new object[]
									{
										propertyData.Value
									});
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Exception while runninq query. Ex: ", new object[]
				{
					ex
				});
				Logger.Info("Ignoring and continuing...");
			}
			return num;
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00034F58 File Offset: 0x00033158
		public static void ParseGLVersion(string glVersion, out double version)
		{
			try
			{
				string text;
				if (glVersion != null && glVersion.StartsWith("OpenGL", StringComparison.OrdinalIgnoreCase))
				{
					text = glVersion.Split(new char[]
					{
						'('
					})[0].Trim();
					text = text.Split(new char[]
					{
						'S'
					})[1].Trim();
				}
				else
				{
					text = glVersion.Split(new char[]
					{
						' '
					})[0].Trim();
					string[] array = text.Split(new char[]
					{
						'.'
					});
					text = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
					{
						array[0],
						array[1]
					});
				}
				version = double.Parse(text, CultureInfo.InvariantCulture);
			}
			catch (Exception ex)
			{
				Logger.Error("Couldn't parse for GL3 string: {0}", new object[]
				{
					glVersion
				});
				Logger.Error(ex.ToString());
				version = 0.0;
			}
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0003504C File Offset: 0x0003324C
		public static string GetUpdatedBootParamsString(string var, string val, string oldBootParams)
		{
			Logger.Info("Attempting to update bootparam for {0}={1}", new object[]
			{
				var,
				val
			});
			bool flag = false;
			if (string.IsNullOrEmpty(val))
			{
				flag = true;
			}
			List<string[]> list;
			if (oldBootParams == null)
			{
				list = null;
			}
			else
			{
				list = (from part in oldBootParams.Split(new char[]
				{
					' '
				}, StringSplitOptions.RemoveEmptyEntries)
				select part.Split(new char[]
				{
					'='
				}) into x
				where x.Length == 1
				select x).ToList<string[]>();
			}
			List<string[]> list2 = list;
			Dictionary<string, string> dictionary;
			if (oldBootParams == null)
			{
				dictionary = null;
			}
			else
			{
				dictionary = (from part in oldBootParams.Split(new char[]
				{
					' '
				}, StringSplitOptions.RemoveEmptyEntries)
				select part.Split(new char[]
				{
					'='
				}) into x
				where x.Length == 2
				select x).ToDictionary((string[] split) => split[0], (string[] split) => split[1]);
			}
			Dictionary<string, string> dictionary2 = dictionary;
			if (flag)
			{
				string[] item = new string[]
				{
					var
				};
				bool flag2 = false;
				using (List<string[]>.Enumerator enumerator = list2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Contains(var))
						{
							flag2 = true;
						}
					}
				}
				if (!flag2)
				{
					list2.Add(item);
					Logger.Info("BootParams added for {0}", new object[]
					{
						var,
						val
					});
				}
				else
				{
					Logger.Info("BootParam already present");
				}
			}
			else
			{
				dictionary2[var] = val;
				Logger.Info("BootParam added/updated");
			}
			List<string> list3 = (from x in dictionary2
			select x.Key + "=" + x.Value).ToList<string>();
			list3.AddRange(list2.SelectMany((string[] x) => x));
			return string.Join(" ", list3.ToArray());
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0003528C File Offset: 0x0003348C
		private static string GetServiceImagePath(string svcName)
		{
			string name = "SYSTEM\\CurrentControlSet\\Services\\" + svcName;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name))
			{
				if (registryKey != null)
				{
					return Environment.ExpandEnvironmentVariables(registryKey.GetValue("ImagePath", "").ToString());
				}
			}
			return "";
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x000352F4 File Offset: 0x000334F4
		public static bool IsRunningInstanceClashWithService(string[] servicePrefixes, out ServiceController runningSvc)
		{
			Logger.Info("In IsRunningInstanceClashWithService");
			runningSvc = null;
			ServiceController[] devices = ServiceController.GetDevices();
			List<ServiceController> list = new List<ServiceController>();
			if (servicePrefixes != null)
			{
				foreach (ServiceController serviceController in devices)
				{
					foreach (string value in servicePrefixes)
					{
						if (serviceController.ServiceName.Contains(value))
						{
							list.Add(serviceController);
						}
					}
				}
			}
			string a = RegistryStrings.InstallDir.TrimEnd(new char[]
			{
				'\\'
			});
			foreach (ServiceController serviceController2 in list)
			{
				string text = Path.GetDirectoryName(Utils.GetServiceImagePath(serviceController2.ServiceName));
				text = text.Substring(4, text.Length - 4);
				if (!string.Equals(a, text, StringComparison.InvariantCultureIgnoreCase) && serviceController2.Status == ServiceControllerStatus.Running)
				{
					runningSvc = serviceController2;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00035408 File Offset: 0x00033608
		public static double RoundUp(double input, int places)
		{
			double num = Math.Pow(10.0, Convert.ToDouble(places));
			return Math.Ceiling(input * num) / num;
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00035434 File Offset: 0x00033634
		public static void UpdateBlueStacksSizeToRegistryASync()
		{
			using (BackgroundWorker backgroundWorker = new BackgroundWorker())
			{
				backgroundWorker.DoWork += Utils.UpdateBlueStacksSizeToRegistry;
				backgroundWorker.RunWorkerAsync();
			}
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0003547C File Offset: 0x0003367C
		private static void UpdateBlueStacksSizeToRegistry(object sender, DoWorkEventArgs e)
		{
			try
			{
				string name = string.Empty;
				if (SystemUtils.IsOs64Bit())
				{
					name = string.Format(CultureInfo.InvariantCulture, "{0}\\BlueStacks{1}", new object[]
					{
						"SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall",
						Strings.GetOemTag()
					});
				}
				else
				{
					name = string.Format(CultureInfo.InvariantCulture, "{0}\\BlueStacks{1}", new object[]
					{
						"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall",
						Strings.GetOemTag()
					});
				}
				long num = 0L;
				foreach (string path in RegistryManager.Instance.VmList.ToList<string>())
				{
					string text = Path.Combine(RegistryStrings.DataDir, path);
					if (Directory.Exists(text))
					{
						num += IOUtils.GetDirectorySize(text);
					}
				}
				num /= 1048576L;
				num += 1000L;
				int num2 = Convert.ToInt32(num);
				Logger.Info("Updating {0}MB BlueStacks size to registry", new object[]
				{
					num2
				});
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name, true))
				{
					if (registryKey != null)
					{
						registryKey.SetValue("EstimatedSize", num2 * 1024, RegistryValueKind.DWord);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Warning("Couldn't update size to registry, ignoring error. Ex: {0}", new object[]
				{
					ex.Message
				});
			}
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00035618 File Offset: 0x00033818
		public static object GetRegistryHKLMValue(string regPath, string key, object defaultValue)
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(regPath))
				{
					if (registryKey != null)
					{
						return registryKey.GetValue(key, defaultValue);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error in getting the reistry value " + ex.Message);
			}
			return defaultValue;
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00035684 File Offset: 0x00033884
		public static object GetRegistryHKCUValue(string regPath, string key, object defaultValue)
		{
			try
			{
				using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(regPath))
				{
					if (registryKey != null)
					{
						return registryKey.GetValue(key, defaultValue);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error in getting the HKCU reistry value " + ex.Message);
			}
			return defaultValue;
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x000356F0 File Offset: 0x000338F0
		public static void BackUpGuid(string userGUID)
		{
			try
			{
				StreamWriter streamWriter = new StreamWriter(Path.Combine(Environment.GetEnvironmentVariable("TEMP"), "Bst_Guid_Backup"));
				streamWriter.Write(userGUID);
				streamWriter.Close();
			}
			catch (Exception ex)
			{
				Logger.Error("Failed to backup guid...ignoring...printing exception");
				Logger.Error(ex.ToString());
			}
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0003574C File Offset: 0x0003394C
		public static void SetAttributesNormal(DirectoryInfo dir)
		{
			foreach (DirectoryInfo directoryInfo in (dir != null) ? dir.GetDirectories("*", SearchOption.AllDirectories) : null)
			{
				Utils.SetAttributesNormal(directoryInfo);
				directoryInfo.Attributes = FileAttributes.Normal;
			}
			FileInfo[] files = dir.GetFiles("*", SearchOption.AllDirectories);
			for (int i = 0; i < files.Length; i++)
			{
				files[i].Attributes = FileAttributes.Normal;
			}
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0000CC01 File Offset: 0x0000AE01
		public static string GetString(string currentValue, string defaultValue)
		{
			if (string.IsNullOrEmpty(currentValue))
			{
				return defaultValue;
			}
			return currentValue;
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0000CC0E File Offset: 0x0000AE0E
		public static int GetInt(int currentValue, int defaultValue)
		{
			if (currentValue == 0)
			{
				return defaultValue;
			}
			return currentValue;
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x000357B8 File Offset: 0x000339B8
		public static ulong GeneratePseudoRandomNumber()
		{
			DateTime now = DateTime.Now;
			return (ulong)(((long)now.Month * 1000000000L + (long)now.DayOfWeek * 100000000L + (long)now.Day * 1000000L + (long)now.Hour * 1000L + (long)now.Minute * 100L + (long)now.Second) * (long)now.Millisecond);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0003582C File Offset: 0x00033A2C
		public static string CreateRandomBstSharedFolder(string bstSharedFolder)
		{
			string result;
			try
			{
				ulong value = Utils.GeneratePseudoRandomNumber();
				string text;
				string path;
				for (;;)
				{
					text = string.Format(CultureInfo.InvariantCulture, "Bst_{0}", new object[]
					{
						Convert.ToString(value, CultureInfo.InvariantCulture)
					});
					path = Path.Combine(bstSharedFolder, text);
					if (!Directory.Exists(path))
					{
						break;
					}
					value = Utils.GeneratePseudoRandomNumber();
				}
				Directory.CreateDirectory(path);
				result = text;
			}
			catch (Exception ex)
			{
				Logger.Info("Failed to create random shared folder... Err : " + ex.ToString());
				throw new Exception("Failed to create Bst Shared Folder");
			}
			return result;
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x000358BC File Offset: 0x00033ABC
		public static string GetValueInBootParams(string name, string vmName, string bootparam = "", string oem = "bgp")
		{
			if (oem == null)
			{
				oem = "bgp";
			}
			string result = string.Empty;
			string text = bootparam;
			if (string.IsNullOrEmpty(text))
			{
				text = RegistryManager.RegistryManagers[oem].Guest[vmName].BootParameters;
			}
			Dictionary<string, string> dictionary = (from part in text.Split(new char[]
			{
				' '
			}, StringSplitOptions.RemoveEmptyEntries)
			select part.Split(new char[]
			{
				'='
			}) into x
			where x.Length == 2
			select x).ToDictionary((string[] split) => split[0], (string[] split) => split[1]);
			if (dictionary.ContainsKey(name))
			{
				result = dictionary[name];
			}
			return result;
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x000359B0 File Offset: 0x00033BB0
		public static string RemoveKeyFromBootParam(string key, string bootParam)
		{
			if (bootParam == null)
			{
				return "";
			}
			Dictionary<string, string> dictionary = (from part in bootParam.Split(new char[]
			{
				' '
			}, StringSplitOptions.RemoveEmptyEntries)
			select part.Split(new char[]
			{
				'='
			}) into x
			where x.Length == 2
			select x).ToDictionary((string[] split) => split[0], (string[] split) => split[1]);
			if (dictionary.ContainsKey(key))
			{
				dictionary.Remove(key);
			}
			return string.Join(" ", (from x in dictionary
			select x.Key + "=" + x.Value).ToArray<string>());
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00035AAC File Offset: 0x00033CAC
		public static void UpdateValueInBootParams(string name, string value, string vmName, bool addIfNotPresent = true, string oem = "bgp")
		{
			if (oem == null)
			{
				oem = "bgp";
			}
			string bootParameters;
			if (oem != "bgp")
			{
				bootParameters = RegistryManager.RegistryManagers[oem].Guest[vmName].BootParameters;
			}
			else
			{
				bootParameters = RegistryManager.Instance.Guest[vmName].BootParameters;
			}
			Dictionary<string, string> dictionary = (from part in bootParameters.Split(new char[]
			{
				' '
			}, StringSplitOptions.RemoveEmptyEntries)
			select part.Split(new char[]
			{
				'='
			}) into x
			where x.Length == 2
			select x).ToDictionary((string[] split) => split[0], (string[] split) => split[1]);
			if (dictionary.ContainsKey(name))
			{
				dictionary[name] = value;
			}
			else if (addIfNotPresent)
			{
				dictionary.Add(name, value);
			}
			List<string> list = (from x in dictionary
			select x.Key + "=" + x.Value).ToList<string>();
			string bootParameters2 = string.Join(" ", list.ToArray());
			if (oem != "bgp")
			{
				RegistryManager.RegistryManagers[oem].Guest[vmName].BootParameters = bootParameters2;
				return;
			}
			RegistryManager.Instance.Guest[vmName].BootParameters = bootParameters2;
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00035C44 File Offset: 0x00033E44
		public static string GetDisplayName(string vmName, string oem = "bgp")
		{
			if (oem == "bgp")
			{
				if ("Android".Equals(vmName, StringComparison.OrdinalIgnoreCase))
				{
					return Strings.ProductTopBarDisplayName;
				}
				if (!RegistryManager.Instance.Guest.ContainsKey(vmName))
				{
					if (vmName == null)
					{
						return null;
					}
					return vmName.Replace("Android_", Strings.ProductDisplayName + " ");
				}
				else
				{
					if (!string.IsNullOrEmpty(RegistryManager.Instance.Guest[vmName].DisplayName))
					{
						return RegistryManager.Instance.Guest[vmName].DisplayName;
					}
					if (vmName == null)
					{
						return null;
					}
					return vmName.Replace("Android_", Strings.ProductDisplayName + " ");
				}
			}
			else if (!RegistryManager.RegistryManagers[oem].Guest.ContainsKey(vmName))
			{
				if (vmName == null)
				{
					return null;
				}
				return vmName.Replace("Android_", Strings.ProductDisplayName + " ");
			}
			else
			{
				if (!string.IsNullOrEmpty(RegistryManager.RegistryManagers[oem].Guest[vmName].DisplayName))
				{
					return RegistryManager.RegistryManagers[oem].Guest[vmName].DisplayName;
				}
				if (vmName == null)
				{
					return null;
				}
				return vmName.Replace("Android_", Strings.ProductDisplayName + " ");
			}
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00035D90 File Offset: 0x00033F90
		public static bool IsAnyItemEmptyInStringList(List<string> strList)
		{
			if (strList != null)
			{
				using (List<string>.Enumerator enumerator = strList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (string.IsNullOrEmpty(enumerator.Current))
						{
							return false;
						}
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x00035DE8 File Offset: 0x00033FE8
		private static void SaveFileInUnicode(string filePath)
		{
			string value = File.ReadAllText(filePath);
			using (StreamWriter streamWriter = new StreamWriter(filePath, false, Encoding.Unicode))
			{
				streamWriter.Write(value);
				streamWriter.Flush();
				streamWriter.Close();
			}
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00035E38 File Offset: 0x00034038
		internal static Dictionary<string, string> AddCommonData(Dictionary<string, string> data)
		{
			if (!data.ContainsKey("install_id"))
			{
				data.Add("install_id", RegistryManager.Instance.InstallID);
			}
			if (!data.ContainsKey("launcher_version"))
			{
				data.Add("launcher_version", RegistryManager.Instance.WebAppVersion);
			}
			return data;
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0000CC16 File Offset: 0x0000AE16
		public static string GetVmIdFromVmName(string vmName)
		{
			if (vmName == "Android")
			{
				return "0";
			}
			if (vmName == null)
			{
				return null;
			}
			return vmName.Split(new char[]
			{
				'_'
			})[1];
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00035E8C File Offset: 0x0003408C
		public static string GetAppRunAppJsonArg(string appName, string pkgName)
		{
			JObject jobject = new JObject
			{
				{
					"app_icon_url",
					""
				},
				{
					"app_name",
					appName
				},
				{
					"app_url",
					""
				},
				{
					"app_pkg",
					pkgName
				}
			};
			return string.Format(CultureInfo.InvariantCulture, "-json \"{0}\"", new object[]
			{
				jobject.ToString(Formatting.None, new JsonConverter[0]).Replace("\"", "\\\"")
			});
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00035F20 File Offset: 0x00034120
		public static void DeleteFiles(List<string> listOfFiles)
		{
			if (listOfFiles != null)
			{
				foreach (string text in listOfFiles)
				{
					if (!Utils.DeleteFile(text))
					{
						Logger.Warning("Couldn't delete file: {0}", new object[]
						{
							text
						});
					}
				}
			}
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00035F88 File Offset: 0x00034188
		public static bool DeleteFile(string filePath)
		{
			if (File.Exists(filePath))
			{
				try
				{
					File.Delete(filePath);
				}
				catch
				{
					return false;
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0000CC43 File Offset: 0x0000AE43
		public static void RemoveGamingRelatedFiles()
		{
			Logger.Info("Removing gaming related files");
			Utils.DeleteFiles(new List<string>
			{
				Path.Combine(RegistryManager.Instance.ClientInstallDir, "game_config.json")
			});
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0000CC73 File Offset: 0x0000AE73
		public static void UpgradeGamingRegistriesToFull()
		{
			Logger.Info("Setting registries for full edition");
			RegistryManager.Instance.InstallationType = InstallationTypes.FullEdition;
			RegistryManager.Instance.InstallerPkgName = string.Empty;
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00035FC0 File Offset: 0x000341C0
		public static void UpgradeToFullVersionAndCreateBstShortcut(bool updateUninstallEntryToo = false)
		{
			Logger.Info("Upgrading to full version of BlueStacks");
			Utils.UpgradeGamingRegistriesToFull();
			Utils.RemoveGamingRelatedFiles();
			Utils.RemoveAdminRelatedRegistryAndFiles(updateUninstallEntryToo);
			CommonInstallUtils.CreateDesktopAndStartMenuShortcuts(Strings.ProductDisplayName, RegistryStrings.ProductIconCompletePath, Path.Combine(RegistryManager.Instance.InstallDir, "BlueStacks.exe"), "", "", "");
			Utils.SHChangeNotify(134217728, 4096, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00036034 File Offset: 0x00034234
		private static void RemoveAdminRelatedRegistryAndFiles(bool updateUninstallEntryToo)
		{
			Logger.Info("Removing admin related things");
			List<string> list = new List<string>
			{
				Path.Combine(RegistryStrings.InstallDir, "game_config.json"),
				Path.Combine(RegistryStrings.InstallDir, "app_icon.ico")
			};
			string[] files = Directory.GetFiles(RegistryStrings.InstallDir, "gameinstaller_*.png", SearchOption.TopDirectoryOnly);
			list.AddRange(files);
			string text = Path.Combine(Path.GetTempPath(), "RemoveGamingFiles.bat");
			using (StreamWriter streamWriter = new StreamWriter(text))
			{
				if (updateUninstallEntryToo)
				{
					Logger.Info("Exporting uninstall registry");
					int num = Utils.ExportUninstallEntry("\\BlueStacks" + Strings.GetOemTag(), Strings.UninstallRegistryExportedFilePath);
					Logger.Info("Exporting result: {0}", new object[]
					{
						num
					});
					Utils.UpdateUninstallRegistryFileForFullEdition(Strings.UninstallRegistryExportedFilePath);
					streamWriter.WriteLine(string.Format(CultureInfo.InvariantCulture, "REG IMPORT \"{0}\"", new object[]
					{
						Strings.UninstallRegistryExportedFilePath
					}));
				}
				foreach (string text2 in list)
				{
					streamWriter.WriteLine(string.Format(CultureInfo.InvariantCulture, "DEL /F /Q \"{0}\"", new object[]
					{
						text2
					}));
				}
				streamWriter.Close();
			}
			Logger.Info("Executing: {0}", new object[]
			{
				text
			});
			Process process = new Process();
			process.StartInfo.Verb = "runas";
			process.StartInfo.FileName = text;
			process.StartInfo.WorkingDirectory = Path.GetTempPath();
			process.StartInfo.CreateNoWindow = true;
			try
			{
				process.Start();
				process.WaitForExit();
			}
			catch (Win32Exception ex)
			{
				Logger.Error("User cancelled UAC: " + ex.Message);
			}
			catch (Exception ex2)
			{
				Logger.Error("An error occured while executing the batch script: " + ex2.ToString());
			}
			finally
			{
				process.Dispose();
			}
			Logger.Info("All done!");
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00036264 File Offset: 0x00034464
		public static void UpdateUninstallEntryForFullEdition()
		{
			try
			{
				Logger.Info("Exporting uninstall registry");
				string uninstallRegistryExportedFilePath = Strings.UninstallRegistryExportedFilePath;
				int num = Utils.ExportUninstallEntry("\\BlueStacks" + Strings.GetOemTag(), uninstallRegistryExportedFilePath);
				Logger.Info("Exporting result: {0}", new object[]
				{
					num
				});
				Utils.UpdateUninstallRegistryFileForFullEdition(uninstallRegistryExportedFilePath);
				num = Utils.ImportRegistryFile(uninstallRegistryExportedFilePath, true);
				Logger.Info("Importing result: {0}", new object[]
				{
					num
				});
			}
			catch (Exception ex)
			{
				Logger.Warning("Couldn't update uninstall entry");
				Logger.Warning(ex.ToString());
			}
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00036300 File Offset: 0x00034500
		private static void UpdateUninstallRegistryFileForFullEdition(string regFilePath)
		{
			Logger.Info("Updating exported file for full version");
			List<string> list = new List<string>();
			using (StreamReader streamReader = new StreamReader(regFilePath))
			{
				string text;
				while ((text = streamReader.ReadLine()) != null)
				{
					if (text.Contains("DisplayName"))
					{
						text = string.Format(CultureInfo.InvariantCulture, "\"DisplayName\"=\"{0}\"", new object[]
						{
							Oem.Instance.ControlPanelDisplayName
						});
					}
					else if (text.Contains("DisplayIcon"))
					{
						text = string.Format(CultureInfo.InvariantCulture, "\"DisplayIcon\"=\"{0}\"", new object[]
						{
							RegistryStrings.ProductIconCompletePath
						});
					}
					else if (text.Contains("Publisher"))
					{
						text = string.Format(CultureInfo.InvariantCulture, "\"Publisher\"=\"{0}\"", new object[]
						{
							"BlueStack Systems, Inc."
						});
					}
					list.Add(text);
				}
				streamReader.Close();
			}
			using (StreamWriter streamWriter = new StreamWriter(regFilePath))
			{
				foreach (string value in list)
				{
					streamWriter.WriteLine(value);
				}
				streamWriter.Close();
			}
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00036450 File Offset: 0x00034650
		public static int ExportUninstallEntry(string keyName, string destFilePath)
		{
			string cmd = "Reg.exe";
			string args = string.Format(CultureInfo.InvariantCulture, "EXPORT HKLM\\{0}{1} \"{2}\"", new object[]
			{
				"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall",
				keyName,
				destFilePath
			});
			if (File.Exists(destFilePath))
			{
				File.Delete(destFilePath);
			}
			return RunCommand.RunCmd(cmd, args, true, true, false, 0).ExitCode;
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x000364A8 File Offset: 0x000346A8
		public static int ImportRegistryFile(string regFilePath, bool requireAdminProc)
		{
			string cmd = "Reg.exe";
			string args = string.Format(CultureInfo.InvariantCulture, "IMPORT \"{0}\"", new object[]
			{
				regFilePath
			});
			return RunCommand.RunCmd(cmd, args, true, true, requireAdminProc, 0).ExitCode;
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x000364E8 File Offset: 0x000346E8
		public static string[] FixDuplicate7zArgs(string[] args)
		{
			string[] array;
			if (args == null || args.Length != 0)
			{
				if (args.Length == 1)
				{
					args[0] = args[0].Remove(args[0].Length / 2);
					array = args;
				}
				else
				{
					int num = args.Length / 2 + 1;
					array = new string[num];
					if (args[args.Length / 2].EndsWith(args[0], StringComparison.OrdinalIgnoreCase))
					{
						for (int i = 0; i <= num - 1; i++)
						{
							array[i] = args[i];
						}
						array[num - 1] = array[num - 1].Remove(array[num - 1].LastIndexOf(args[0], StringComparison.OrdinalIgnoreCase));
					}
				}
			}
			else
			{
				array = args;
			}
			return array;
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0000CC99 File Offset: 0x0000AE99
		public static string GetMultiInstanceEventName(string vmName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}-{1}", new object[]
			{
				"BstClient",
				vmName
			});
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00036578 File Offset: 0x00034778
		public static int GetVmIdToCreate(string oem = "bgp")
		{
			int num = RegistryManager.Instance.VmId;
			if (oem == null)
			{
				oem = "bgp";
			}
			if (oem != "bgp")
			{
				num = RegistryManager.RegistryManagers[oem].VmId;
			}
			for (;;)
			{
				string path = string.Format(CultureInfo.InvariantCulture, "Android_{0}", new object[]
				{
					num
				});
				if (oem == "bgp")
				{
					if (!Directory.Exists(Path.Combine(RegistryStrings.DataDir, path)))
					{
						break;
					}
					num++;
					Logger.Info("Incrementing vmId: {0}", new object[]
					{
						num
					});
				}
				else
				{
					if (!Directory.Exists(Path.Combine(RegistryManager.RegistryManagers[oem].EngineDataDir, path)))
					{
						break;
					}
					num++;
					Logger.Info("Incrementing vmId: {0}", new object[]
					{
						num
					});
				}
			}
			if (oem != "bgp")
			{
				RegistryManager.RegistryManagers[oem].VmId = RegistryManager.RegistryManagers[oem].VmId + 1;
			}
			else
			{
				RegistryManager.Instance.VmId = RegistryManager.Instance.VmId + 1;
			}
			return num;
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0003669C File Offset: 0x0003489C
		public static JsonSerializerSettings GetSerializerSettings()
		{
			JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
			jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
			jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
			jsonSerializerSettings.Converters.Add(new StringEnumConverter());
			jsonSerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
			jsonSerializerSettings.Error = (EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs>)Delegate.Combine(jsonSerializerSettings.Error, new EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs>(Utils.JsonSerializer_Error));
			jsonSerializerSettings.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;
			return jsonSerializerSettings;
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0000CCBC File Offset: 0x0000AEBC
		private static void JsonSerializer_Error(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs e)
		{
			e.ErrorContext.Handled = true;
			Logger.Error("Error loading JSON " + e.ErrorContext.Path + Environment.NewLine + e.ErrorContext.Error.ToString());
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x000366FC File Offset: 0x000348FC
		public static void OpenUrl(string url)
		{
			try
			{
				Process.Start(url);
			}
			catch (Win32Exception)
			{
				try
				{
					Process.Start("IExplore.exe", url);
				}
				catch (Exception ex)
				{
					Logger.Warning("Not able to launch the url " + url + "Ignoring Exception: " + ex.ToString());
				}
			}
			catch (Exception ex2)
			{
				Logger.Warning("Not able to launch the url " + url + "Ignoring Exception: " + ex2.ToString());
			}
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x00036788 File Offset: 0x00034988
		public static string GetDpiFromBootParameters(string bootParameterString)
		{
			string[] array = (bootParameterString != null) ? bootParameterString.Split(new char[]
			{
				' '
			}) : null;
			string text = null;
			foreach (string text2 in array)
			{
				if (text2.StartsWith("DPI=", StringComparison.OrdinalIgnoreCase))
				{
					text = text2.Split(new char[]
					{
						'='
					})[1];
					break;
				}
			}
			if (text == null)
			{
				text = "240";
			}
			return text;
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x000367F0 File Offset: 0x000349F0
		public static void ReplaceOldVirtualBoxNamespaceWithNew(string filePath)
		{
			Logger.Info("In ReplaceOldVirtualBoxNamespaceWithNew");
			string text = File.ReadAllText(filePath);
			string text2 = "http://www.innotek.de/VirtualBox-settings";
			string newValue = "http://www.virtualbox.org/";
			if (text.Contains(text2))
			{
				text = text.Replace(text2, newValue);
				File.WriteAllText(filePath, text);
			}
		}

		// Token: 0x06000DF6 RID: 3574 RVA: 0x00036834 File Offset: 0x00034A34
		public static void SetDPIInBootParameters(string bootParameterString, string updatedValue, string vmName, string oem = "bgp")
		{
			string[] array = (bootParameterString != null) ? bootParameterString.Split(new char[]
			{
				' '
			}) : null;
			string text = null;
			foreach (string text2 in array)
			{
				if (text2.StartsWith("DPI=", StringComparison.OrdinalIgnoreCase))
				{
					text = text2.Split(new char[]
					{
						'='
					})[0];
					string text3 = text2.Split(new char[]
					{
						'='
					})[1];
					if (text3 != updatedValue)
					{
						string newValue = string.Format(CultureInfo.InvariantCulture, "DPI={0}", new object[]
						{
							updatedValue
						});
						string oldValue = string.Format(CultureInfo.InvariantCulture, "DPI={0}", new object[]
						{
							text3
						});
						string bootParameters = bootParameterString.Replace(oldValue, newValue);
						if (oem != "bgp")
						{
							RegistryManager.RegistryManagers[oem].Guest[vmName].BootParameters = bootParameters;
						}
						else
						{
							RegistryManager.Instance.Guest[vmName].BootParameters = bootParameters;
						}
					}
				}
			}
			if (text == null)
			{
				string text4 = string.Format(CultureInfo.InvariantCulture, "DPI={0}", new object[]
				{
					updatedValue
				});
				string bootParameters2 = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
				{
					bootParameterString,
					text4
				});
				if (oem != "bgp")
				{
					RegistryManager.RegistryManagers[oem].Guest[vmName].BootParameters = bootParameters2;
					return;
				}
				RegistryManager.Instance.Guest[vmName].BootParameters = bootParameters2;
			}
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x000369C0 File Offset: 0x00034BC0
		public static IntPtr BringToFront(string windowName)
		{
			Logger.Info("Window name is = " + windowName);
			IntPtr result = IntPtr.Zero;
			try
			{
				result = InteropWindow.BringWindowToFront(windowName, false, true);
			}
			catch (Exception ex)
			{
				Logger.Error("Exception in bringing existing window to the foreground Err : " + ex.ToString());
			}
			return result;
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0000CCF9 File Offset: 0x0000AEF9
		public static void SendChangeFPSToInstanceASync(string vmname, int newFps = 2147483647, string oem = "bgp")
		{
			ThreadPool.QueueUserWorkItem(delegate(object obj)
			{
				try
				{
					VmCmdHandler.RunCommand(string.Format(CultureInfo.InvariantCulture, "setfpsvalue?fps={0}", new object[]
					{
						(newFps == int.MaxValue) ? RegistryManager.Instance.Guest[vmname].FPS : newFps
					}), vmname, oem);
				}
				catch (Exception ex)
				{
					string str = "Exception in SendChangeFPSToInstanceASync. Error: ";
					Exception ex2 = ex;
					Logger.Warning(str + ((ex2 != null) ? ex2.ToString() : null));
				}
			});
		}

		// Token: 0x06000DF9 RID: 3577 RVA: 0x00036A18 File Offset: 0x00034C18
		public static void SendEnableVSyncToInstanceASync(bool enable, string vmname, string oem = "bgp")
		{
			Dictionary<string, string> data = new Dictionary<string, string>
			{
				{
					"enableVsync",
					enable.ToString(CultureInfo.InvariantCulture)
				}
			};
			HTTPUtils.SendRequestToEngineAsync("enableVSync", data, vmname, 0, null, true, 1, 0, oem);
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x00036A54 File Offset: 0x00034C54
		public static void SendShowFPSToInstanceASync(string vmname, int isShowFPS)
		{
			Dictionary<string, string> data = new Dictionary<string, string>
			{
				{
					"isshowfps",
					isShowFPS.ToString(CultureInfo.InvariantCulture)
				}
			};
			HTTPUtils.SendRequestToEngineAsync("showFPS", data, vmname, 0, null, true, 1, 0, "bgp");
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x00036A94 File Offset: 0x00034C94
		public static bool CheckMultiInstallBeforeRunQuitMultiInstall()
		{
			try
			{
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software");
				int num = 0;
				foreach (string text in registryKey.GetSubKeyNames())
				{
					if (text.StartsWith("BlueStacks", StringComparison.OrdinalIgnoreCase) && !text.StartsWith("BlueStacksGP", StringComparison.OrdinalIgnoreCase) && !text.StartsWith("BlueStacksInstaller", StringComparison.OrdinalIgnoreCase))
					{
						num++;
					}
				}
				if (num >= 2)
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				Logger.Info("error at CheckMultiInstallBeforeRunQuitMultiInstall" + ex.ToString());
			}
			return false;
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x00036B34 File Offset: 0x00034D34
		public static bool PingPartner(string oem, string vmName)
		{
			try
			{
				if (!string.IsNullOrEmpty(oem) && BstHttpClient.Get(string.Format(CultureInfo.InvariantCulture, "http://127.0.0.1:{0}/ping", new object[]
				{
					RegistryManager.RegistryManagers[oem].PartnerServerPort
				}), null, false, vmName, 0, 1, 0, false, "bgp").Contains("success"))
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Failed to ping partner server. Exc: " + ex.ToString());
			}
			return false;
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0000CD26 File Offset: 0x0000AF26
		public static void WriteAgentPortInFile(int port)
		{
			new Thread(delegate()
			{
				int i = 5;
				while (i > 0)
				{
					try
					{
						Utils.WriteToFile(Path.Combine(RegistryManager.Instance.UserDefinedDir, "bst_params.txt"), string.Format(CultureInfo.InvariantCulture, "agentserverport={0}", new object[]
						{
							port
						}), "agentserverport");
						break;
					}
					catch (Exception ex)
					{
						Logger.Error("Failed to write agent port to bst_params.txt. Ex: " + ex.ToString());
					}
					Logger.Info("retrying..." + i.ToString());
					i--;
					Thread.Sleep(500);
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0002379C File Offset: 0x0002199C
		private static void WriteToFile(string path, string text, string searchText = "")
		{
			bool flag = true;
			List<string> list = new List<string>();
			if (File.Exists(path))
			{
				foreach (string text2 in File.ReadAllLines(path))
				{
					if (text2.Contains("="))
					{
						if (text2.Contains(searchText))
						{
							list.Add(text);
							flag = false;
						}
						else
						{
							list.Add(text2);
						}
					}
				}
			}
			if (flag)
			{
				using (TextWriter textWriter = new StreamWriter(path, true))
				{
					textWriter.WriteLine(text);
					textWriter.Flush();
					return;
				}
			}
			using (TextWriter textWriter2 = new StreamWriter(path, false))
			{
				foreach (string value in list)
				{
					textWriter2.WriteLine(value);
				}
				textWriter2.Flush();
			}
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x00036BC4 File Offset: 0x00034DC4
		public static int RunQuitMultiInstall()
		{
			string installDir = RegistryStrings.InstallDir;
			string text = "HD-QuitMultiInstall.exe";
			int exitCode;
			try
			{
				string text2 = Path.Combine(installDir, text);
				Process process = new Process();
				process.StartInfo.Arguments = "-in";
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.CreateNoWindow = true;
				process.StartInfo.FileName = text2;
				Logger.Info("Complete path to QuitMultiInstall: " + text2);
				if (Environment.OSVersion.Version.Major <= 5)
				{
					process.StartInfo.Verb = "runas";
				}
				Logger.Info("Utils: Starting QuitMultiInstall with -in");
				process.Start();
				process.WaitForExit();
				exitCode = process.ExitCode;
			}
			catch (Exception ex)
			{
				Logger.Error("An error occured: {0}", new object[]
				{
					ex
				});
				Process process2 = new Process();
				process2.StartInfo.Arguments = "-in";
				process2.StartInfo.UseShellExecute = false;
				process2.StartInfo.CreateNoWindow = true;
				process2.StartInfo.FileName = text;
				process2.StartInfo.WorkingDirectory = installDir;
				Logger.Info("Running {0} with WorkingDir {1}", new object[]
				{
					text,
					installDir
				});
				if (Environment.OSVersion.Version.Major <= 5)
				{
					process2.StartInfo.Verb = "runas";
				}
				Logger.Info("Utils: Starting QuitMultiInstall with -in");
				process2.Start();
				process2.WaitForExit();
				exitCode = process2.ExitCode;
			}
			return exitCode;
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00036D4C File Offset: 0x00034F4C
		public static bool WaitForBGPClientPing(int retries = 40)
		{
			while (retries > 0)
			{
				try
				{
					if (JArray.Parse(HTTPUtils.SendRequestToClient("ping", null, "Android", 1000, null, false, 1, 0, "bgp"))[0]["success"].ToString().Trim().Equals("true", StringComparison.InvariantCultureIgnoreCase))
					{
						Logger.Debug("got ping response from client");
						return true;
					}
				}
				catch
				{
				}
				retries--;
				Thread.Sleep(500);
			}
			return false;
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0000CD50 File Offset: 0x0000AF50
		public static IWin32Window GetIWin32Window(IntPtr handle)
		{
			return new OldWindow(handle);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x00036DE0 File Offset: 0x00034FE0
		public static string GetPackageNameFromAPK(string apkFile)
		{
			string result = null;
			try
			{
				using (Process process = new Process())
				{
					process.StartInfo.UseShellExecute = false;
					process.StartInfo.CreateNoWindow = true;
					process.StartInfo.RedirectStandardOutput = true;
					process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
					process.StartInfo.FileName = Path.Combine(RegistryStrings.InstallDir, "hd-aapt.exe");
					process.StartInfo.Arguments = string.Format(CultureInfo.InvariantCulture, "dump badging \"{0}\"", new object[]
					{
						apkFile
					});
					process.Start();
					string input = process.StandardOutput.ReadToEnd();
					process.WaitForExit();
					result = new Regex("package:\\sname='(.+?)'").Match(input).Groups[1].Value.ToString(CultureInfo.InvariantCulture);
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error getting apk name: {0}", new object[]
				{
					ex.Message
				});
			}
			return result;
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x00036EF4 File Offset: 0x000350F4
		public static string GetFileAssemblyVersion(string path)
		{
			string result = string.Empty;
			if (File.Exists(path))
			{
				try
				{
					result = FileVersionInfo.GetVersionInfo(path).FileVersion;
				}
				catch (Exception ex)
				{
					Logger.Error("Error in parsing file version information: {0}", new object[]
					{
						ex.Message
					});
				}
			}
			return result;
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0000CD58 File Offset: 0x0000AF58
		public static string GetHelperInstalledPath()
		{
			return Path.Combine(Path.Combine(RegistryManager.Instance.ClientInstallDir, "Helper"), "BlueStacksHelper.exe");
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x00036F4C File Offset: 0x0003514C
		public static string GetHelperTaskDetailsJSon()
		{
			try
			{
				CmdRes taskQueryCommandOutput = TaskScheduler.GetTaskQueryCommandOutput("BlueStacksHelper");
				JObject jobject = new JObject();
				string[] array = taskQueryCommandOutput.StdOut.Split(new char[]
				{
					'\n'
				});
				string[] array2 = taskQueryCommandOutput.StdErr.Split(new char[]
				{
					'\n'
				});
				JObject jobject2 = new JObject();
				int num = 1;
				foreach (string value in array)
				{
					if (!string.IsNullOrEmpty(value))
					{
						jobject2.Add(string.Format(CultureInfo.InvariantCulture, "line{0}", new object[]
						{
							num
						}), value);
						num++;
					}
				}
				jobject.Add("stdout", jobject2.ToString());
				jobject2 = new JObject();
				num = 1;
				foreach (string value2 in array2)
				{
					if (!string.IsNullOrEmpty(value2))
					{
						jobject2.Add(string.Format(CultureInfo.InvariantCulture, "line{0}", new object[]
						{
							num
						}), value2);
						num++;
					}
				}
				jobject.Add("stderr", jobject2.ToString());
				return jobject.ToString(Formatting.None, new JsonConverter[0]);
			}
			catch (Exception ex)
			{
				Logger.Error("Some error while creating json of the QueryTask: {0}", new object[]
				{
					ex
				});
			}
			return "";
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x000370DC File Offset: 0x000352DC
		public static bool HasOneDayPassed(DateTime srcTime)
		{
			try
			{
				double totalMinutes = (DateTime.Now - srcTime).TotalMinutes;
				double num = 1440.0;
				if (totalMinutes > num)
				{
					return true;
				}
			}
			catch (Exception ex)
			{
				Logger.Warning("Couldn't check if the req time has passed. Ex: {0}", new object[]
				{
					ex.Message
				});
			}
			return false;
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x00037140 File Offset: 0x00035340
		public static string GetAndroidIDFromAndroid(string vmName)
		{
			string result = string.Empty;
			try
			{
				JObject jobject = JObject.Parse(HTTPUtils.SendRequestToGuest("getAndroidID", null, vmName, 0, null, false, 1, 0, "bgp"));
				if (jobject["result"].ToString() == "ok")
				{
					result = jobject["androidID"].ToString();
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error in getting Android ID: {0}", new object[]
				{
					ex.ToString()
				});
			}
			return result;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x000371CC File Offset: 0x000353CC
		public static string GetGoogleAdIDFromAndroid(string vmName)
		{
			string result = string.Empty;
			try
			{
				JObject jobject = JObject.Parse(HTTPUtils.SendRequestToGuest("getGoogleAdID", null, vmName, 0, null, false, 1, 0, "bgp"));
				if (jobject["result"].ToString() == "ok")
				{
					result = jobject["googleadid"].ToString();
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error in getting googleAd ID: {0}", new object[]
				{
					ex.ToString()
				});
			}
			return result;
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0000CD78 File Offset: 0x0000AF78
		public static void SetGoogleAdIdAndAndroidIdFromAndroid(string vmName)
		{
			new Thread(delegate()
			{
				try
				{
					string googleAdIDFromAndroid = Utils.GetGoogleAdIDFromAndroid(vmName);
					if (!string.IsNullOrEmpty(googleAdIDFromAndroid))
					{
						RegistryManager.Instance.Guest[vmName].GoogleAId = UUID.Base64Encode(googleAdIDFromAndroid);
					}
					string androidIDFromAndroid = Utils.GetAndroidIDFromAndroid(vmName);
					if (!string.IsNullOrEmpty(androidIDFromAndroid))
					{
						RegistryManager.Instance.Guest[vmName].AndroidId = UUID.Base64Encode(androidIDFromAndroid);
					}
				}
				catch (Exception ex)
				{
					Logger.Error("Exception while getting ids from android: {0}", new object[]
					{
						ex.ToString()
					});
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00037258 File Offset: 0x00035458
		public static string GetGoogleAdIdfromRegistry(string vmName)
		{
			string s = string.Empty;
			try
			{
				s = UUID.Base64Decode(RegistryManager.Instance.Guest[vmName].GoogleAId);
			}
			catch (Exception ex)
			{
				Logger.Error("Exception in decoding GoogleAid: {0}", new object[]
				{
					ex.ToString()
				});
			}
			return StringUtils.GetControlCharFreeString(s);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x000372BC File Offset: 0x000354BC
		public static string GetAndroidIdfromRegistry(string vmName)
		{
			string s = string.Empty;
			try
			{
				s = UUID.Base64Decode(RegistryManager.Instance.Guest[vmName].AndroidId);
			}
			catch (Exception ex)
			{
				Logger.Error("Exception in decoding AndroidID ID: {0}", new object[]
				{
					ex.ToString()
				});
			}
			return StringUtils.GetControlCharFreeString(s);
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0000CDA2 File Offset: 0x0000AFA2
		public static void CreateMD5HashOfRootVdi()
		{
			new Thread(delegate()
			{
				try
				{
					string blockDevice0Path = RegistryManager.Instance.DefaultGuest.BlockDevice0Path;
					RegistryManager.Instance.RootVdiMd5Hash = Utils.GetMD5HashFromFile(blockDevice0Path);
				}
				catch (Exception ex)
				{
					Logger.Error("Exception while checking md5 hash of root.vdi: {0}", new object[]
					{
						ex.ToString()
					});
				}
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x00037320 File Offset: 0x00035520
		public static int GetMaxVmIdFromVmList(string[] vmList)
		{
			int num = 0;
			try
			{
				if (vmList != null)
				{
					foreach (string text in vmList)
					{
						if (!(text == "Android"))
						{
							int num2;
							int.TryParse(text.Split(new char[]
							{
								'_'
							})[1], out num2);
							if (num < num2)
							{
								num = num2;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error in getting max VmId to create err:", new object[]
				{
					ex.ToString()
				});
			}
			return num + 1;
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x000373AC File Offset: 0x000355AC
		public static bool CheckIfUserIsPartOfGroup(string currentUser, string groupName)
		{
			Logger.Info("Inside CheckIfUserIsPartOfGroup- Args:" + currentUser + ", " + groupName);
			try
			{
				using (DirectoryEntry directoryEntry = new DirectoryEntry("WinNT://" + Environment.MachineName + ",computer"))
				{
					foreach (object obj in directoryEntry.Children)
					{
						DirectoryEntry directoryEntry2 = (DirectoryEntry)obj;
						Logger.Info("\tInside CheckIfUserIsPartOfGroup - SchemeClassName: " + directoryEntry2.SchemaClassName);
						if (directoryEntry2.SchemaClassName == "User" && string.Equals(directoryEntry2.Name, currentUser, StringComparison.InvariantCultureIgnoreCase))
						{
							IEnumerable enumerable = directoryEntry2.Invoke("Groups", new object[0]) as IEnumerable;
							if (enumerable != null)
							{
								using (IEnumerator enumerator2 = enumerable.GetEnumerator())
								{
									while (enumerator2.MoveNext())
									{
										object adsObject = enumerator2.Current;
										try
										{
											DirectoryEntry directoryEntry3 = new DirectoryEntry(adsObject);
											if (string.Equals(directoryEntry3.Name, groupName, StringComparison.InvariantCultureIgnoreCase))
											{
												directoryEntry3.Close();
												return true;
											}
											directoryEntry3.Close();
										}
										catch (Exception ex)
										{
											Logger.Error("Could not create DirectoryEntry", new object[]
											{
												ex
											});
										}
									}
									continue;
								}
							}
							Logger.Info("\tCould not get childEntry.Invoke(\"Groups\") as IEnumerable");
						}
					}
				}
			}
			catch (Exception ex2)
			{
				Logger.Error(string.Concat(new string[]
				{
					"Error while checking if ",
					currentUser,
					" is part of ",
					groupName,
					" group"
				}), new object[]
				{
					ex2
				});
			}
			return false;
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x000375D4 File Offset: 0x000357D4
		public static int GetRecommendedVCPUCount(bool isDefaultVm)
		{
			int result = 2;
			if (!isDefaultVm)
			{
				result = 1;
			}
			return result;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x000375EC File Offset: 0x000357EC
		public static void SetTimeZoneInGuest(string vmName)
		{
			string value = TimeZone.CurrentTimeZone.StandardName;
			TimeSpan utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
			string text = utcOffset.ToString();
			if (text[0] != '-')
			{
				text = string.Format(CultureInfo.InvariantCulture, "GMT+{0}", new object[]
				{
					text
				});
			}
			else
			{
				text = string.Format(CultureInfo.InvariantCulture, "GMT{0}", new object[]
				{
					text
				});
			}
			string text2 = TimeZone.CurrentTimeZone.IsDaylightSavingTime(DateTime.Now).ToString(CultureInfo.InvariantCulture);
			string value2 = SystemUtils.GetSysInfo("Select DaylightBias from Win32_TimeZone");
			string text3;
			if (text2.Equals("True", StringComparison.InvariantCultureIgnoreCase) && !string.IsNullOrEmpty(value2))
			{
				text3 = utcOffset.Add(new TimeSpan(0, Convert.ToInt32(value2, CultureInfo.InvariantCulture), 0)).ToString();
				if (text3[0] != '-')
				{
					text3 = string.Format(CultureInfo.InvariantCulture, "GMT+{0}", new object[]
					{
						text3
					});
				}
				else
				{
					text3 = string.Format(CultureInfo.InvariantCulture, "GMT{0}", new object[]
					{
						text3
					});
				}
			}
			else
			{
				text3 = text;
			}
			if (Features.IsFeatureEnabled(4194304UL))
			{
				text3 = "GMT+08:00:00";
				text2 = "False";
				value2 = "0";
				text = "GMT+08:00:00";
				value = "中国标准时间";
			}
			else if ("bgp".Equals("dmm", StringComparison.OrdinalIgnoreCase))
			{
				text3 = "GMT+09:00:00";
				text2 = "False";
				value2 = "0";
				text = "GMT+09:00:00";
				value = "日本の標準時";
			}
			Dictionary<string, string> data = new Dictionary<string, string>
			{
				{
					"baseUtcOffset",
					text3
				},
				{
					"isDaylightSavingTime",
					text2
				},
				{
					"daylightBias",
					value2
				},
				{
					"utcOffset",
					text
				},
				{
					"standardName",
					value
				}
			};
			JObject jobject;
			VmCmdHandler.SendRequest("settz", data, vmName, out jobject, "bgp");
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x000377D8 File Offset: 0x000359D8
		public static void RunHDQuit(bool isWaitForExit = false, bool isFromClient = false, bool overrideIgnoreAgent = false, string oem = "bgp")
		{
			try
			{
				Logger.Info("Quit bluestacks called with args: {0}, {1}", new object[]
				{
					isWaitForExit,
					isFromClient
				});
				string path = string.Empty;
				try
				{
					path = RegistryManager.RegistryManagers[oem].InstallDir;
				}
				catch (Exception arg)
				{
					Logger.Warning(string.Format("Failed to get RegistryManager.RegistryManagers[oem].InstallDir {0}", arg));
					path = RegistryStrings.InstallDir;
				}
				string fileName = Path.Combine(path, "HD-Quit.exe");
				using (Process process = new Process())
				{
					process.StartInfo.FileName = fileName;
					if (!Oem.IsOEMDmm && !overrideIgnoreAgent)
					{
						process.StartInfo.Arguments = "-ignoreAgent";
					}
					if (isFromClient)
					{
						ProcessStartInfo startInfo = process.StartInfo;
						startInfo.Arguments += " -isFromClient";
					}
					Logger.Debug("Quit Aguments = " + process.StartInfo.Arguments);
					process.Start();
					if (isWaitForExit)
					{
						process.WaitForExit();
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Quit bluestacks failed: " + ex.ToString());
			}
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0003790C File Offset: 0x00035B0C
		public static JToken ExtractInfoFromXapk(string zipFilePath)
		{
			JToken result = null;
			string path = Path.Combine(Path.GetTempPath(), Path.GetFileName(zipFilePath));
			if (File.Exists(Path.Combine(path, "manifest.json")))
			{
				result = JToken.Parse(File.ReadAllText(Path.Combine(path, "manifest.json")));
			}
			return result;
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x00037958 File Offset: 0x00035B58
		public static bool CheckIfDeviceProfileChanged(JObject mCurrentDeviceProfile, JObject mChangedDeviceProfile)
		{
			if (mCurrentDeviceProfile != null && mChangedDeviceProfile != null)
			{
				if (!string.Equals(mCurrentDeviceProfile["pcode"].ToString(), mChangedDeviceProfile["pcode"].ToString(), StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
				if (!string.Equals(mCurrentDeviceProfile["caSelector"].ToString(), mChangedDeviceProfile["caSelector"].ToString(), StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
				if (string.Equals(mCurrentDeviceProfile["pcode"].ToString(), "custom", StringComparison.OrdinalIgnoreCase) && (!string.Equals(mCurrentDeviceProfile["model"].ToString(), mChangedDeviceProfile["model"].ToString(), StringComparison.OrdinalIgnoreCase) || !string.Equals(mCurrentDeviceProfile["brand"].ToString(), mChangedDeviceProfile["brand"].ToString(), StringComparison.OrdinalIgnoreCase) || !string.Equals(mCurrentDeviceProfile["manufacturer"].ToString(), mChangedDeviceProfile["manufacturer"].ToString(), StringComparison.OrdinalIgnoreCase)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x00037A60 File Offset: 0x00035C60
		public static bool CheckForInternetConnection()
		{
			bool result;
			try
			{
				using (WebClient webClient = new WebClient())
				{
					using (webClient.OpenRead("http://connectivitycheck.gstatic.com/generate_204"))
					{
						result = true;
					}
				}
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x00037AC8 File Offset: 0x00035CC8
		public static string[] AddVmNameInArgsIfNotPresent(string[] args)
		{
			if (!Utils.CheckIfVmNamePassedToArgs(args))
			{
				string text = Utils.CheckIfAnyVmRunning();
				List<string> list = args.ToList<string>();
				if (!string.IsNullOrEmpty(text))
				{
					list.Add("-vmname");
					list.Add(text);
				}
				args = list.ToArray();
			}
			return args;
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x00037B10 File Offset: 0x00035D10
		private static string CheckIfAnyVmRunning()
		{
			foreach (object obj in RegistryManager.Instance.VmList)
			{
				string text = obj as string;
				if (Utils.CheckIfGuestReady(text, 1))
				{
					return text;
				}
			}
			return null;
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00037B50 File Offset: 0x00035D50
		private static bool CheckIfVmNamePassedToArgs(string[] args)
		{
			IEnumerable<string> first = args.ToList<string>();
			IList<string> second = new List<string>
			{
				"--vmname",
				"-vmname",
				"vmname"
			};
			return first.Intersect(second).Any<string>();
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x00037B9C File Offset: 0x00035D9C
		public static void SetAstcOption(string vmname, ASTCOption astcOption, string oem = "bgp")
		{
			if (oem == null)
			{
				oem = "bgp";
			}
			if (oem != "bgp")
			{
				RegistryManager.RegistryManagers[oem].Guest[vmname].ASTCOption = astcOption;
			}
			else
			{
				RegistryManager.Instance.Guest[vmname].ASTCOption = astcOption;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string key = "AstcOption";
			int num = (int)astcOption;
			dictionary.Add(key, num.ToString(CultureInfo.InvariantCulture));
			Dictionary<string, string> data = dictionary;
			HTTPUtils.SendRequestToEngineAsync("setAstcOption", data, vmname, 0, null, true, 1, 0, "bgp");
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00037C2C File Offset: 0x00035E2C
		public static void KillCurrentOemProcessByName(string[] nameList, string clientInstallDir = null)
		{
			if (nameList != null)
			{
				if (clientInstallDir == null)
				{
					clientInstallDir = string.Empty;
				}
				for (int i = 0; i < nameList.Length; i++)
				{
					Utils.KillCurrentOemProcessByName(nameList[i], clientInstallDir);
				}
			}
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x00037C60 File Offset: 0x00035E60
		public static void KillCurrentOemProcessByName(string procName, string clientInstallDir = null)
		{
			Process[] processesByName = Process.GetProcessesByName(procName);
			string installDir = RegistryStrings.InstallDir;
			if (string.IsNullOrEmpty(clientInstallDir))
			{
				clientInstallDir = RegistryManager.Instance.ClientInstallDir;
			}
			foreach (Process process in processesByName)
			{
				try
				{
					string directoryName = Path.GetDirectoryName(GetProcessExecutionPath.GetApplicationPathFromProcess(process));
					if (directoryName.Equals(installDir.TrimEnd(new char[]
					{
						'\\'
					}), StringComparison.InvariantCultureIgnoreCase) || directoryName.Equals(clientInstallDir.TrimEnd(new char[]
					{
						'\\'
					}), StringComparison.InvariantCultureIgnoreCase) || directoryName.Equals(RegistryStrings.ObsDir, StringComparison.InvariantCultureIgnoreCase))
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
				}
				catch (Exception ex)
				{
					Logger.Error("Exception in killing process " + ex.Message);
				}
			}
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00037D6C File Offset: 0x00035F6C
		public static void EnableDisableApp(string appPackage, bool enable, string vmName)
		{
			try
			{
				JObject jobject = new JObject
				{
					{
						"packagename",
						appPackage
					},
					{
						"enable",
						enable.ToString(CultureInfo.InvariantCulture)
					}
				};
				Dictionary<string, string> data = new Dictionary<string, string>
				{
					{
						"d",
						jobject.ToString(Formatting.None, new JsonConverter[0])
					}
				};
				HTTPUtils.SendRequestToGuestAsync("setapplicationstate", data, vmName, 0, null, false, 1, 0, "bgp");
			}
			catch (Exception ex)
			{
				Logger.Error("Exception in EnableDisableApp :" + ex.ToString());
			}
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x00037E0C File Offset: 0x0003600C
		public static string GetInstalledAppDataFromAllVms()
		{
			string[] vmList = RegistryManager.Instance.VmList;
			string result = string.Empty;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				using (JsonWriter jsonWriter = new JsonTextWriter(new StringWriter(stringBuilder))
				{
					Formatting = Formatting.Indented
				})
				{
					jsonWriter.WriteStartObject();
					foreach (string vmName in vmList)
					{
						jsonWriter.WritePropertyName("vm" + Utils.GetVmIdFromVmName(vmName));
						jsonWriter.WriteStartObject();
						jsonWriter.WritePropertyName("google_aid");
						jsonWriter.WriteValue(Utils.GetGoogleAdIdfromRegistry(vmName));
						jsonWriter.WritePropertyName("android_id");
						jsonWriter.WriteValue(Utils.GetAndroidIdfromRegistry(vmName));
						jsonWriter.WritePropertyName("installed_apps");
						jsonWriter.WriteStartObject();
						foreach (AppInfo appInfo in new JsonParser(vmName).GetAppList().ToList<AppInfo>())
						{
							string package = appInfo.Package;
							string name = appInfo.Name;
							jsonWriter.WritePropertyName(package);
							jsonWriter.WriteValue(name);
						}
						jsonWriter.WriteEndObject();
						jsonWriter.WriteEndObject();
					}
					jsonWriter.WriteEndObject();
					result = stringBuilder.ToString();
					Logger.Debug("json string of all apps : " + stringBuilder.ToString());
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Exception in getting all installed apps from all Vms: {0}", new object[]
				{
					ex.ToString()
				});
			}
			return result;
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x00037FCC File Offset: 0x000361CC
		public static int GetTimeFromEpoch()
		{
			return (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00037FF8 File Offset: 0x000361F8
		public static Dictionary<string, string> GetBootParamsDictFromBootParamString(string bootParams)
		{
			try
			{
				if (string.IsNullOrEmpty(bootParams))
				{
					return null;
				}
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				foreach (string text in bootParams.Split(new char[]
				{
					' '
				}).ToList<string>())
				{
					dictionary.Add(text.Split(new char[]
					{
						'='
					})[0], text.Split(new char[]
					{
						'='
					})[1]);
				}
				return dictionary;
			}
			catch (Exception ex)
			{
				Logger.Error("Exception in getting bootParamsDict err: " + ex.ToString());
			}
			return null;
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x000380C4 File Offset: 0x000362C4
		public static GlMode GetGlModeForVm(string vmName)
		{
			int glRenderMode = RegistryManager.Instance.Guest[vmName].GlRenderMode;
			int glMode = RegistryManager.Instance.Guest[vmName].GlMode;
			GlMode result;
			if (glRenderMode == 1 && glMode == 1)
			{
				result = GlMode.PGA_GL;
			}
			else if (glRenderMode == 1 && glMode == 2)
			{
				result = GlMode.AGA_GL;
			}
			else if (glMode == 1)
			{
				result = GlMode.PGA_DX;
			}
			else
			{
				if (glMode != 2)
				{
					throw new Exception("Could not determine the engine mode for current build.");
				}
				result = GlMode.AGA_DX;
			}
			return result;
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x00038134 File Offset: 0x00036334
		public static bool CheckIfImagesArrayPresentInCfg(JObject oldConfig)
		{
			try
			{
				if (oldConfig != null)
				{
					foreach (JToken jtoken in ((IEnumerable<JToken>)oldConfig["ControlSchemes"]))
					{
						JObject jobject = (JObject)jtoken;
						if (jobject != null && jobject["Images"] != null && ((JArray)jobject["Images"]).Count > 0)
						{
							return true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Exception in CheckIfImagesArrayPresentInCfg: " + ex.ToString());
			}
			return false;
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x000381DC File Offset: 0x000363DC
		public static void ValidateScreenShotFolder()
		{
			try
			{
				if (!StringExtensions.IsValidPath(RegistryManager.Instance.ScreenShotsPath))
				{
					Logger.Info("Media folder path: " + RegistryManager.Instance.ScreenShotsPath + " is invalid");
					if (!Directory.Exists(RegistryStrings.ScreenshotDefaultPath))
					{
						Logger.Info("ScreenshotDefaultPath: " + RegistryManager.Instance.ScreenShotsPath + " folder does not exist");
						Directory.CreateDirectory(RegistryStrings.ScreenshotDefaultPath);
					}
					RegistryManager.Instance.ScreenShotsPath = RegistryStrings.ScreenshotDefaultPath;
				}
			}
			catch (Exception arg)
			{
				Logger.Error(string.Format("ValidateScreenShotFolder: {0}", arg));
			}
		}

		// Token: 0x04000672 RID: 1650
		private const int SM_TABLETPC = 86;

		// Token: 0x04000673 RID: 1651
		public const int BTV_RIGHT_PANEL_WIDTH = 320;

		// Token: 0x04000674 RID: 1652
		private static bool sIsSyncAppJsonComplete = false;

		// Token: 0x04000675 RID: 1653
		private const int SM_CXSCREEN = 0;

		// Token: 0x04000676 RID: 1654
		private const int SM_CYSCREEN = 1;

		// Token: 0x04000677 RID: 1655
		private static Dictionary<string, object> OemVmLockNamedata = new Dictionary<string, object>();

		// Token: 0x04000678 RID: 1656
		public static readonly Dictionary<string, bool> sIsGuestBooted = new Dictionary<string, bool>();

		// Token: 0x04000679 RID: 1657
		public static readonly Dictionary<string, bool> sIsGuestReady = new Dictionary<string, bool>();

		// Token: 0x0400067A RID: 1658
		public static readonly Dictionary<string, bool> sIsSharedFolderMounted = new Dictionary<string, bool>();

		// Token: 0x0400067B RID: 1659
		private const int PROC_KILL_TIMEOUT = 10000;

		// Token: 0x0400067C RID: 1660
		private const int COMSERVER_EXIT_TIMEOUT = 5000;

		// Token: 0x0400067D RID: 1661
		public static readonly List<string> sListIgnoredApps = new List<string>
		{
			"tv.gamepop.home",
			"com.pop.store",
			"com.pop.store51",
			"com.bluestacks.s2p5105",
			"mpi.v23",
			"com.google.android.gms",
			"com.google.android.gsf.login",
			"com.android.deskclock",
			"me.onemobile.android",
			"me.onemobile.lite.android",
			"android.rk.RockVideoPlayer.RockVideoPlayer",
			"com.bluestacks.chartapp",
			"com.bluestacks.setupapp",
			"com.android.gallery3d",
			"com.bluestacks.keymappingtool",
			"com.baidu.appsearch",
			"com.bluestacks.s2p",
			"com.bluestacks.windowsfilemanager",
			"com.android.quicksearchbox",
			"com.bluestacks.setup",
			"com.bluestacks.appsettings",
			"mpi.v23",
			"com.bluestacks.setup",
			"com.bluestacks.gamepophome",
			"com.bluestacks.appfinder",
			"com.android.providers.downloads.ui"
		};
	}
}
