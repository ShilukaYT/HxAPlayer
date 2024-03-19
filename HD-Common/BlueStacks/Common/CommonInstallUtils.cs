using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlueStacks.Common
{
	// Token: 0x02000108 RID: 264
	public static class CommonInstallUtils
	{
		// Token: 0x060006C3 RID: 1731
		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		private static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, uint dwFlags, [Out] StringBuilder pszPath);

		// Token: 0x060006C4 RID: 1732
		[DllImport("HD-LibraryHandler.dll", CharSet = CharSet.Auto)]
		private static extern int DeleteLibrary(string libraryName);

		// Token: 0x060006C5 RID: 1733
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool CreateHardLink(string lpFileName, string lpExistingFileName, IntPtr lpSecurityAttributes);

		// Token: 0x060006C6 RID: 1734
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsWow64Process([In] IntPtr hProcess, out bool wow64Process);

		// Token: 0x060006C7 RID: 1735
		[DllImport("advapi32", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int RegRenameKey(UIntPtr hKey, [MarshalAs(UnmanagedType.LPWStr)] string oldname, [MarshalAs(UnmanagedType.LPWStr)] string newname);

		// Token: 0x060006C8 RID: 1736
		[DllImport("HD-OpenGl-Native.dll")]
		public static extern int IsVulkanSupported();

		// Token: 0x060006C9 RID: 1737
		[DllImport("HD-OpenGl-Native.dll")]
		public static extern void PgaLoggerInit(Logger.HdLoggerCallback cb);

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00006088 File Offset: 0x00004288
		public static bool Is64BitOperatingSystem
		{
			get
			{
				return CommonInstallUtils.is64BitProcess || CommonInstallUtils.InternalCheckIsWow64();
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00021448 File Offset: 0x0001F648
		private static bool InternalCheckIsWow64()
		{
			if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) || Environment.OSVersion.Version.Major >= 6)
			{
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					bool result;
					if (!CommonInstallUtils.IsWow64Process(currentProcess.Handle, out result))
					{
						return false;
					}
					return result;
				}
				return false;
			}
			return false;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x000214C8 File Offset: 0x0001F6C8
		public static void KillBlueStacksProcesses(string clientInstallDir = null, bool killPlayerProcess = false)
		{
			Logger.Info("Killing all BlueStacks processes");
			Utils.KillCurrentOemProcessByName(new string[]
			{
				"BlueStacks",
				"Keymapui",
				"HD-OBS",
				"HD-Agent",
				"HD-Adb",
				"HD-RunApp",
				"HD-LogCollector",
				"HD-DataManager",
				"HD-QuitMultiInstall",
				"HD-MultiInstanceManager",
				"BlueStacksHelper"
			}, clientInstallDir);
			if (killPlayerProcess)
			{
				Utils.KillCurrentOemProcessByName("HD-Player", clientInstallDir);
				Utils.KillCurrentOemProcessByName("BstkSVC", clientInstallDir);
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x00006098 File Offset: 0x00004298
		public static string EngineInstallDir
		{
			get
			{
				return (string)Utils.GetRegistryHKLMValue(string.Format(CultureInfo.InvariantCulture, "Software\\BlueStacks{0}", new object[]
				{
					Strings.GetOemTag()
				}), "InstallDir", string.Empty);
			}
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00021560 File Offset: 0x0001F760
		public static void RunHdQuit(string hdQuitPath)
		{
			try
			{
				string fileName = Path.Combine(hdQuitPath, "HD-Quit.exe");
				using (Process process = new Process())
				{
					process.StartInfo.FileName = fileName;
					process.Start();
					process.WaitForExit();
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Exception in running hd-quit err: {0}", new object[]
				{
					ex.ToString()
				});
			}
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x000215E0 File Offset: 0x0001F7E0
		public static void ModifyDirectoryPermissionsForEveryone(string dir)
		{
			if (!Directory.Exists(dir))
			{
				Directory.CreateDirectory(dir);
			}
			try
			{
				string identity = new SecurityIdentifier("S-1-1-0").Translate(typeof(NTAccount)).ToString();
				DirectoryInfo directoryInfo = new DirectoryInfo(dir);
				DirectorySecurity accessControl = directoryInfo.GetAccessControl();
				accessControl.AddAccessRule(new FileSystemAccessRule(identity, FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
				directoryInfo.SetAccessControl(accessControl);
			}
			catch (Exception ex)
			{
				Logger.Error("Failed to set permissions. err: " + ex.ToString());
			}
			try
			{
				if (!SystemUtils.IsOSWinXP())
				{
					foreach (string fileName in Directory.GetFiles(dir))
					{
						string identity2 = new SecurityIdentifier("S-1-1-0").Translate(typeof(NTAccount)).ToString();
						FileInfo fileInfo = new FileInfo(fileName);
						FileSecurity accessControl2 = fileInfo.GetAccessControl();
						accessControl2.AddAccessRule(new FileSystemAccessRule(identity2, FileSystemRights.FullControl, AccessControlType.Allow));
						fileInfo.SetAccessControl(accessControl2);
					}
				}
				string[] array = Directory.GetDirectories(dir);
				for (int i = 0; i < array.Length; i++)
				{
					CommonInstallUtils.ModifyDirectoryPermissionsForEveryone(array[i]);
				}
			}
			catch (Exception ex2)
			{
				Logger.Error("Failed to set permissions. err: " + ex2.ToString());
			}
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00021724 File Offset: 0x0001F924
		public static bool MoveDirectory(string srcDir, string dstDir)
		{
			Logger.Info("Moving directory {0} to {1}", new object[]
			{
				srcDir,
				dstDir
			});
			try
			{
				if (Directory.Exists(dstDir))
				{
					Directory.Delete(dstDir, true);
				}
				Directory.Move(srcDir, dstDir);
			}
			catch (Exception ex)
			{
				Logger.Info("------------ FOR DEV TRACKING--------------- Moving failed");
				Logger.Info("Caught exception when moving directory {0} to {1} err :{2}", new object[]
				{
					srcDir,
					dstDir,
					ex.ToString()
				});
				if (!Directory.Exists(dstDir))
				{
					Directory.CreateDirectory(dstDir);
				}
				foreach (string text in Directory.GetFiles(srcDir))
				{
					FileInfo fileInfo = new FileInfo(text);
					string text2 = Path.Combine(dstDir, fileInfo.Name);
					try
					{
						if (File.Exists(text2))
						{
							File.SetAttributes(text, FileAttributes.Normal);
							File.Delete(text2);
						}
						File.Move(text, text2);
					}
					catch (Exception ex2)
					{
						Logger.Info("Exception in file move {0} to {1}. Copying instead.. ex:{2}", new object[]
						{
							text,
							text2,
							ex2.ToString()
						});
						try
						{
							File.Copy(text, text2, true);
						}
						catch (Exception ex3)
						{
							Logger.Error("Exception in file copy: THIS WILL RESULT IN DEPLOYMENT FAILURE" + ex3.ToString());
							return false;
						}
					}
				}
				string[] array = Directory.GetDirectories(srcDir);
				for (int i = 0; i < array.Length; i++)
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(array[i]);
					string text3 = Path.Combine(dstDir, directoryInfo.Name);
					string text4 = Path.Combine(srcDir, directoryInfo.Name);
					if (!CommonInstallUtils.MoveDirectory(text4, text3))
					{
						Logger.Warning("Returing false in directory move for {0} to {1}", new object[]
						{
							text4,
							text3
						});
						return false;
					}
				}
				try
				{
					Directory.Delete(srcDir);
				}
				catch (Exception ex4)
				{
					Logger.Info("Ignoring exception when trying to delete srcDir at the end err:{0}", new object[]
					{
						ex4.ToString()
					});
				}
			}
			return true;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00021944 File Offset: 0x0001FB44
		public static string GetFolderPath(int CSIDL)
		{
			StringBuilder stringBuilder = new StringBuilder(260);
			CommonInstallUtils.SHGetFolderPath(IntPtr.Zero, CSIDL, IntPtr.Zero, 0U, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00021978 File Offset: 0x0001FB78
		public static void DeleteLegacyShortcuts()
		{
			Logger.Info("Deleting legacy shortcuts");
			ShortcutHelper.DeleteDesktopShortcut("Start BlueStacks.lnk");
			ShortcutHelper.DeleteStartMenuShortcut("Start BlueStacks.lnk");
			ShortcutHelper.DeleteStartMenuShortcut("Programs\\BlueStacks\\Start BlueStacks.lnk");
			if (!string.IsNullOrEmpty(Oem.Instance.DesktopShortcutFileName))
			{
				ShortcutHelper.DeleteDesktopShortcut(Oem.Instance.DesktopShortcutFileName);
				ShortcutHelper.DeleteStartMenuShortcut(Oem.Instance.DesktopShortcutFileName);
			}
			if (Oem.Instance.CreateMultiInstanceManagerIcon)
			{
				ShortcutHelper.DeleteDesktopShortcut(Oem.Instance.MultiInstanceManagerShortcutFileName);
				ShortcutHelper.DeleteStartMenuShortcut(Oem.Instance.MultiInstanceManagerShortcutFileName);
			}
			try
			{
				string path = Path.Combine(CommonInstallUtils.GetFolderPath(25), Oem.Instance.DesktopShortcutFileName);
				if (File.Exists(path))
				{
					File.Delete(path);
				}
			}
			catch
			{
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000060CB File Offset: 0x000042CB
		public static bool CheckIfSDCardPresent()
		{
			return File.Exists(Path.Combine(Path.Combine(RegistryStrings.DataDir, "Android"), "SDCard.vdi"));
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00021A44 File Offset: 0x0001FC44
		public static void DeleteOldShortcuts()
		{
			try
			{
				CommonInstallUtils.DeleteLegacyShortcuts();
			}
			catch (Exception ex)
			{
				Logger.Warning("Failed to delete old shortcuts. Err: " + ex.ToString());
			}
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00021A80 File Offset: 0x0001FC80
		public static void CreateDesktopAndStartMenuShortcuts(string shortcutName, string iconPath, string targetBinPath, string args = "", string description = "", string package = "")
		{
			try
			{
				if (Oem.Instance.IsCreateDesktopAndStartMenuShortcut)
				{
					if (!string.IsNullOrEmpty(shortcutName))
					{
						if (string.IsNullOrEmpty(description))
						{
							description = shortcutName;
						}
						ShortcutHelper.CreateCommonDesktopShortcut(shortcutName, iconPath, targetBinPath, args, description, package);
						ShortcutHelper.CreateCommonStartMenuShortcut(shortcutName, iconPath, targetBinPath, args, shortcutName, package);
						if (!File.Exists(Path.Combine(ShortcutHelper.CommonDesktopPath, shortcutName.Replace(".lnk", "") + ".lnk")))
						{
							Logger.Info("Failed to make common desktop shortcut...atempting user desktop");
							ShortcutHelper.CreateDesktopShortcut(shortcutName, iconPath, targetBinPath, args, description, package);
						}
						Utils.SHChangeNotify(134217728, 4096, IntPtr.Zero, IntPtr.Zero);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Warning("Failed to create BlueStacks shortcuts. ex: " + ex.ToString());
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00021B58 File Offset: 0x0001FD58
		public static void UpdateOldAppDesktopShortcutTarget(string oldInstallDir, string newInstallDir)
		{
			try
			{
				foreach (string text in Directory.GetFiles(ShortcutHelper.sDesktopPath, "*.lnk", SearchOption.AllDirectories))
				{
					try
					{
						if (File.Exists(text) && ShortcutHelper.GetShortcutArguments(text).TrimEnd(new char[]
						{
							'\\'
						}).ToLower(CultureInfo.InvariantCulture).Equals(Path.Combine(oldInstallDir, "HD-RunApp.exe").ToLower(CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase))
						{
							string text2 = ShortcutHelper.GetShortcutIconLocation(text);
							if (text2.ToLower(CultureInfo.InvariantCulture).Contains("library\\icons"))
							{
								text2 = text2.Replace("Library\\Icons", "Gadget");
								ShortcutHelper.UpdateTargetPathAndIcon(text, Path.Combine(newInstallDir, "HD-RunApp.exe"), text2);
							}
						}
					}
					catch
					{
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Failed to fix app shortcut target");
				Logger.Error(ex.ToString());
			}
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00021C50 File Offset: 0x0001FE50
		public static void CreatePartnerRegistryEntry(string clientInstallDir)
		{
			string name = string.Format(CultureInfo.InvariantCulture, "Software\\BlueStacks{0}\\Config", new object[]
			{
				Strings.GetOemTag()
			});
			Logger.Info("CreatePartnerRegistryEntry start");
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name, true))
			{
				if (registryKey != null)
				{
					registryKey.SetValue("PartnerExePath", Path.Combine(clientInstallDir, "BlueStacks.exe"));
				}
				else
				{
					Logger.Info("Not writing partner exe path , registry not exists");
				}
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00021CD4 File Offset: 0x0001FED4
		public static bool RecreateAddRemoveRegistry(string pfDir, string iconPath, string displayName, string publisher)
		{
			try
			{
				string subkey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\BlueStacks" + Strings.GetOemTag();
				using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(subkey))
				{
					registryKey.SetValue("DisplayName", displayName);
					registryKey.SetValue("DisplayVersion", "4.250.0.1070");
					registryKey.SetValue("DisplayIcon", iconPath);
					registryKey.SetValue("EstimatedSize", 2096128, RegistryValueKind.DWord);
					registryKey.SetValue("InstallDate", string.Format(CultureInfo.InvariantCulture, "{0:yyyyMMdd}", new object[]
					{
						DateTime.Now
					}));
					registryKey.SetValue("NoModify", "1", RegistryValueKind.DWord);
					registryKey.SetValue("NoRepair", "1", RegistryValueKind.DWord);
					registryKey.SetValue("Publisher", publisher);
					registryKey.SetValue("UninstallString", Path.Combine(pfDir, "BlueStacksUninstaller.exe -tmp"));
				}
				return true;
			}
			catch (Exception ex)
			{
				Logger.Info("Error in creating ControlPanel registry: {0}", new object[]
				{
					ex.ToString()
				});
			}
			return false;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00021DF8 File Offset: 0x0001FFF8
		public static List<string> GetUninstallCurrentVersionSubKey(string keyToSearch)
		{
			List<string> list = new List<string>();
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall", true))
			{
				foreach (string text in registryKey.GetSubKeyNames())
				{
					using (RegistryKey registryKey2 = registryKey.OpenSubKey(text, true))
					{
						string text2 = (string)registryKey2.GetValue("DisplayName");
						if (text2 != null && text2.Equals(keyToSearch, StringComparison.OrdinalIgnoreCase))
						{
							list.Add(text);
						}
					}
				}
			}
			return list;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00021EA4 File Offset: 0x000200A4
		public static CmdRes ExtractZip(string zipFilePath, string extractDirectory)
		{
			string cmd = Path.Combine(Directory.GetCurrentDirectory(), "7zr.exe");
			if (!Directory.Exists(extractDirectory))
			{
				Directory.CreateDirectory(extractDirectory);
			}
			string args = string.Format(CultureInfo.InvariantCulture, "x \"{0}\" -o\"{1}\" -aoa", new object[]
			{
				zipFilePath,
				extractDirectory
			});
			return RunCommand.RunCmd(cmd, args, false, true, false, 0);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00021EFC File Offset: 0x000200FC
		public static void CopyLogFileToLogDir(string logFilePath, string destFileName)
		{
			try
			{
				string destFileName2 = Path.Combine(RegistryManager.Instance.LogDir, destFileName);
				File.Copy(logFilePath, destFileName2, true);
			}
			catch (Exception ex)
			{
				Logger.Error("Got exception when copying isntaller logs in log dir ex :{0}", new object[]
				{
					ex.ToString()
				});
			}
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00021F50 File Offset: 0x00020150
		public static void DeleteDirectory(List<string> directories, bool throwError = false)
		{
			if (directories != null)
			{
				foreach (string text in directories)
				{
					try
					{
						CommonInstallUtils.DeleteDirectory(text);
					}
					catch (Exception ex)
					{
						Logger.Warning("Failed to delete directory {0}, ignoring", new object[]
						{
							text
						});
						Logger.Warning(ex.Message);
						if (throwError)
						{
							throw;
						}
					}
				}
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00021FD8 File Offset: 0x000201D8
		public static void DeleteDirectory(string targetDir)
		{
			if (string.IsNullOrEmpty(targetDir) || CommonInstallUtils.sDisallowedDeletionStrings.Any((string str) => str.Equals(targetDir, StringComparison.CurrentCultureIgnoreCase)))
			{
				Logger.Warning("A hazardous DirectoryDelete for '{0}' was invoked. Ignoring the call", new object[]
				{
					targetDir
				});
				return;
			}
			try
			{
				Logger.Info("Deleting directory : " + targetDir);
				Directory.Delete(targetDir, true);
			}
			catch (DirectoryNotFoundException)
			{
				Logger.Warning("Could not delete {0} as it does not exist", new object[]
				{
					targetDir
				});
			}
			catch (Exception ex)
			{
				Logger.Warning("Got exception when deleting {0}, err:{1}", new object[]
				{
					targetDir,
					ex.ToString()
				});
				Logger.Info("------------- FOR DEV TRACKING --------------");
				foreach (string path in Directory.GetFiles(targetDir))
				{
					File.SetAttributes(path, FileAttributes.Normal);
					File.Delete(path);
				}
				string[] array = Directory.GetDirectories(targetDir);
				for (int i = 0; i < array.Length; i++)
				{
					CommonInstallUtils.DeleteDirectory(array[i]);
				}
				try
				{
					Directory.Delete(targetDir, true);
				}
				catch (IOException)
				{
					Thread.Sleep(100);
					try
					{
						Directory.Delete(targetDir, true);
					}
					catch
					{
					}
				}
				catch (UnauthorizedAccessException)
				{
					Thread.Sleep(100);
					try
					{
						Directory.Delete(targetDir, true);
					}
					catch
					{
					}
				}
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00022188 File Offset: 0x00020388
		public static void GetScreenResolution(out int windowWidth, out int windowHeight, out int guestWidth, out int guestHeight)
		{
			double aspectRatio = 1.7777777777777777;
			double num;
			double num2;
			double num3;
			WpfUtils.GetDefaultSize(out num, out num2, out num3, aspectRatio, true);
			windowWidth = (int)(num - 17.0) / 16 * 16;
			windowHeight = windowWidth / 16 * 9;
			Utils.GetGuestWidthAndHeight(windowWidth, windowHeight, out guestWidth, out guestHeight);
			if (Oem.Instance.IsPixelParityToBeIgnored)
			{
				Utils.GetWindowWidthAndHeight(out guestWidth, out guestHeight);
			}
			Logger.Info("Resolution values: guestWidth: {0} guestHeight: {1} widowWidth: {2} windowHeight: {3}", new object[]
			{
				guestWidth,
				guestHeight,
				windowWidth,
				windowHeight
			});
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00022220 File Offset: 0x00020420
		public static void CreateWebUrlScheme(string clientInstallDir)
		{
			try
			{
				string bgpkeyName = Strings.BGPKeyName;
				using (RegistryKey registryKey = Registry.ClassesRoot.CreateSubKey(bgpkeyName))
				{
					registryKey.SetValue("", "BlueStacks Web Url Scheme");
					registryKey.SetValue("URL Protocol", "");
				}
				using (RegistryKey registryKey2 = Registry.ClassesRoot.CreateSubKey(bgpkeyName + "\\DefaultIcon"))
				{
					registryKey2.SetValue("", Path.Combine(clientInstallDir, "ProductLogo.ico"));
				}
				using (RegistryKey registryKey3 = Registry.ClassesRoot.CreateSubKey(bgpkeyName + "\\Shell"))
				{
					registryKey3.SetValue("", "open");
				}
				using (RegistryKey registryKey4 = Registry.ClassesRoot.CreateSubKey(bgpkeyName + "\\Shell\\open"))
				{
					registryKey4.SetValue("", "Open BlueStacks Game Platform");
				}
				using (RegistryKey registryKey5 = Registry.ClassesRoot.CreateSubKey(bgpkeyName + "\\Shell\\open\\command"))
				{
					string text = Path.Combine(RegistryManager.Instance.InstallDir, "Bluestacks.exe");
					string value = string.Format(CultureInfo.InvariantCulture, "\"{0}\" %1", new object[]
					{
						text
					});
					registryKey5.SetValue("", value);
				}
			}
			catch (Exception ex)
			{
				Logger.Warning("Failed to create web url scheme. Err: " + ex.ToString());
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00022420 File Offset: 0x00020620
		public static void CreateInstallApkScheme(string installDir, string baseKeyName, string targetBinary)
		{
			Logger.Info("CreateInstallApkScheme start");
			try
			{
				using (RegistryKey registryKey = Registry.ClassesRoot.CreateSubKey(baseKeyName))
				{
					registryKey.SetValue("", string.Format(CultureInfo.InvariantCulture, "{0} Android Package File", new object[]
					{
						Strings.ProductDisplayName
					}));
				}
				using (RegistryKey registryKey2 = Registry.ClassesRoot.CreateSubKey(baseKeyName + "\\DefaultIcon"))
				{
					registryKey2.SetValue("", RegistryStrings.ProductIconCompletePath);
				}
				using (RegistryKey registryKey3 = Registry.ClassesRoot.CreateSubKey(baseKeyName + "\\Shell"))
				{
					registryKey3.SetValue("", "open");
				}
				using (RegistryKey registryKey4 = Registry.ClassesRoot.CreateSubKey(baseKeyName + "\\Shell\\open"))
				{
					registryKey4.SetValue("", string.Format(CultureInfo.InvariantCulture, "Open with {0} APK Installer", new object[]
					{
						Strings.ProductDisplayName
					}));
				}
				using (RegistryKey registryKey5 = Registry.ClassesRoot.CreateSubKey(baseKeyName + "\\Shell\\open\\command"))
				{
					string text = Path.Combine(installDir, targetBinary);
					string value = string.Format(CultureInfo.InvariantCulture, "{0} \"%1\"", new object[]
					{
						text
					});
					registryKey5.SetValue("", value);
				}
			}
			catch (Exception ex)
			{
				Logger.Warning("Some error while setting APKHandler extention association, ex: " + ex.Message);
			}
			try
			{
				if (baseKeyName != null && baseKeyName.Equals("BlueStacks.Apk", StringComparison.OrdinalIgnoreCase))
				{
					using (RegistryKey registryKey6 = Registry.ClassesRoot.CreateSubKey(".apk"))
					{
						registryKey6.SetValue("", baseKeyName, RegistryValueKind.String);
						goto IL_1D8;
					}
				}
				using (RegistryKey registryKey7 = Registry.ClassesRoot.CreateSubKey(".xapk"))
				{
					registryKey7.SetValue("", baseKeyName, RegistryValueKind.String);
				}
				IL_1D8:;
			}
			catch (Exception ex2)
			{
				Logger.Warning("Some error while setting main .apk extention association, ex: " + ex2.Message);
			}
			Logger.Info("CreateInstallApkScheme end");
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00022708 File Offset: 0x00020908
		public static void DeleteInstallApkScheme(string installDir, string baseKeyName)
		{
			Logger.Info("DeleteInstallApkScheme start");
			try
			{
				string text = "junkPath";
				using (RegistryKey registryKey = Registry.ClassesRoot.OpenSubKey(baseKeyName + "\\Shell\\open\\command"))
				{
					text = (string)registryKey.GetValue("");
				}
				text = text.Trim(new char[]
				{
					'"'
				});
				installDir = ((installDir != null) ? installDir.Trim(new char[]
				{
					'"'
				}) : null);
				if (text.StartsWith(installDir, StringComparison.OrdinalIgnoreCase))
				{
					Registry.ClassesRoot.DeleteSubKeyTree(baseKeyName);
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Failed to delete apk handler registry. Ex : " + ex.ToString());
			}
			Logger.Info("DeleteInstallApkScheme end");
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000227D8 File Offset: 0x000209D8
		public static void LogAllServiceNames()
		{
			try
			{
				Logger.Info("Installed kernel level services:-");
				foreach (ServiceController serviceController in ServiceController.GetDevices())
				{
					Logger.Info("ServiceName: {0},\tDisplayName: {1}, status: {2}", new object[]
					{
						serviceController.ServiceName,
						serviceController.DisplayName,
						serviceController.Status
					});
				}
				Logger.Info("Installed services:-");
				foreach (ServiceController serviceController2 in ServiceController.GetServices())
				{
					Logger.Info("ServiceName: {0},\tDisplayName: {1}, status: {2}", new object[]
					{
						serviceController2.ServiceName,
						serviceController2.DisplayName,
						serviceController2.Status
					});
				}
			}
			catch (Exception ex)
			{
				Logger.Warning("Some error occured while logging services, ex: " + ex.Message);
			}
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x000228B8 File Offset: 0x00020AB8
		public static void CheckForActiveHandles(string installerExtractedPath)
		{
			Logger.Info("Checking for active Handles");
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Sysinternals\\Handle", true);
				if (registryKey == null)
				{
					Registry.CurrentUser.CreateSubKey("Software\\Sysinternals\\Handle");
				}
				else
				{
					registryKey.Close();
				}
				using (RegistryKey registryKey2 = Registry.CurrentUser.OpenSubKey("Software\\Sysinternals\\Handle", true))
				{
					registryKey2.SetValue("EulaAccepted", 1, RegistryValueKind.DWord);
					Logger.Info("Accepted");
				}
			}
			catch (Exception ex)
			{
				Logger.Warning("Couldn't access registry, ex: {0}", new object[]
				{
					ex.Message
				});
			}
			try
			{
				string cmd = Path.Combine(installerExtractedPath, "HD-Handle.exe");
				string args = "bluestacks";
				RunCommand.RunCmd(cmd, args, true, true, false, 0);
			}
			catch (Exception ex2)
			{
				Logger.Warning("Couldn't check for active handles, ex: {0}", new object[]
				{
					ex2.Message
				});
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x000060EB File Offset: 0x000042EB
		public static void RemoveAndAddFirewallRule(string ruleName, string binPath)
		{
			CommonInstallUtils.RemoveFirewallRule(ruleName);
			CommonInstallUtils.AddFirewallRule(ruleName, binPath);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x000229B8 File Offset: 0x00020BB8
		public static void AddFirewallRule(string ruleName, string binPath)
		{
			Logger.Info("Adding firewall rule fo bin {0}", new object[]
			{
				binPath
			});
			try
			{
				string cmd = "netsh.exe";
				string args = string.Format(CultureInfo.InvariantCulture, "advfirewall firewall add rule name=\"{0}\" dir=in action=allow program=\"{1}\" enable=yes", new object[]
				{
					ruleName,
					binPath
				});
				RunCommand.RunCmd(cmd, args, false, true, false, 0);
			}
			catch (Exception ex)
			{
				Logger.Error("Error in adding firewall rule", new object[]
				{
					ex.ToString()
				});
			}
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00022A38 File Offset: 0x00020C38
		public static void RemoveFirewallRule(string ruleName)
		{
			try
			{
				string cmd = "netsh.exe";
				string args = string.Format(CultureInfo.InvariantCulture, "advfirewall firewall delete rule name=\"{0}\"", new object[]
				{
					ruleName
				});
				RunCommand.RunCmd(cmd, args, false, true, false, 0);
			}
			catch (Exception ex)
			{
				Logger.Error("{0} Firewall rule dont exist {1}", new object[]
				{
					ruleName,
					ex.ToString()
				});
			}
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00022AA4 File Offset: 0x00020CA4
		public static bool CreateHardLinkForFile(string existingFilePath, string newFilePath)
		{
			try
			{
				Logger.Info("Creating link from " + existingFilePath + " to " + newFilePath);
				if (!CommonInstallUtils.CreateHardLink(newFilePath, existingFilePath, IntPtr.Zero))
				{
					Logger.Error("Failed to create hard link: " + Marshal.GetLastWin32Error().ToString());
					if (File.Exists(existingFilePath))
					{
						Logger.Error("Copying the file instead");
						File.Copy(existingFilePath, newFilePath, true);
					}
				}
				return true;
			}
			catch (Exception ex)
			{
				Logger.Error("Failed in creating hard link, Ex: " + ex.ToString());
			}
			return false;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00022B3C File Offset: 0x00020D3C
		public static bool CreateHardLinkForDirectory(string existingDirPath, string newDirPath)
		{
			try
			{
				Logger.Info("Creating link from " + existingDirPath + " to " + newDirPath);
				if (!Directory.Exists(newDirPath))
				{
					Directory.CreateDirectory(newDirPath);
				}
				foreach (FileInfo fileInfo in new DirectoryInfo(existingDirPath).GetFiles())
				{
					string fullName = fileInfo.FullName;
					string newFilePath = Path.Combine(newDirPath, fileInfo.Name);
					if (!CommonInstallUtils.CreateHardLinkForFile(fullName, newFilePath))
					{
						Logger.Error("Failed to create hard link for file : " + fileInfo.FullName);
						return false;
					}
				}
				foreach (DirectoryInfo directoryInfo in new DirectoryInfo(existingDirPath).GetDirectories())
				{
					if (!CommonInstallUtils.CreateHardLinkForDirectory(Path.Combine(existingDirPath, directoryInfo.Name), Path.Combine(newDirPath, directoryInfo.Name)))
					{
						return false;
					}
				}
				return true;
			}
			catch (Exception ex)
			{
				Logger.Error("Failed to create hard link for directory, Err: " + ex.ToString());
			}
			return false;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00022C3C File Offset: 0x00020E3C
		public static bool IsDiskFull(Exception ex)
		{
			int num = Marshal.GetHRForException(ex) & 65535;
			Logger.Info("Win32 error code: " + num.ToString());
			Logger.Info("Disk full error codes are: {0} and {1}", new object[]
			{
				112,
				39
			});
			return num == 39 || num == 112;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00022C9C File Offset: 0x00020E9C
		public static bool StopAndUninstallService(string svcName, int timeoutSeconds = 15, bool isKernelDriver = false)
		{
			Logger.Info("Uninstalling service {0}", new object[]
			{
				svcName
			});
			bool result;
			try
			{
				CommonInstallUtils.StopService(svcName, timeoutSeconds);
				CommonInstallUtils.UninstallService(svcName, isKernelDriver);
				result = true;
			}
			catch (Exception ex)
			{
				Logger.Warning("Ignoring exception when uninstalling service {0} ex : {1}", new object[]
				{
					svcName,
					ex.ToString()
				});
				result = false;
			}
			return result;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00022D04 File Offset: 0x00020F04
		private static void StopService(string serviceName, int timeoutSeconds = 15)
		{
			Logger.Info("Stopping service {0} with timeout {1}s", new object[]
			{
				serviceName,
				timeoutSeconds
			});
			using (ServiceController serviceController = new ServiceController(serviceName))
			{
				TimeSpan timeout = TimeSpan.FromSeconds((double)timeoutSeconds);
				if (serviceController.Status == ServiceControllerStatus.Stopped || serviceController.Status == ServiceControllerStatus.StopPending)
				{
					serviceController.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
					Logger.Info("Service {0} stopped", new object[]
					{
						serviceName
					});
				}
				else
				{
					try
					{
						Logger.Info("Service is running , stopping the service {0}", new object[]
						{
							serviceName
						});
						serviceController.Stop();
						serviceController.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
						Logger.Info("Service stopped successfully");
					}
					catch (System.ServiceProcess.TimeoutException)
					{
						Logger.Error("Timed out while waiting for service to stop");
						throw;
					}
					catch (Exception ex)
					{
						Logger.Error("Got exception stopping service {0}, Err: {1}", new object[]
						{
							serviceName,
							ex.ToString()
						});
						throw;
					}
				}
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x000060FA File Offset: 0x000042FA
		private static void UninstallService(string name, bool isKernelDriverService = false)
		{
			ServiceManager.UninstallService(name, isKernelDriverService);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00022DFC File Offset: 0x00020FFC
		public static bool DoesRegistryHKLMKeyExist(string keyPath)
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(keyPath);
			bool flag = registryKey != null;
			if (flag)
			{
				registryKey.Close();
			}
			return flag;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00006104 File Offset: 0x00004304
		public static string ConvertIntToEnumString(int enumCode)
		{
			return Enum.GetName(typeof(InstallerCodes), (InstallerCodes)enumCode);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00022E24 File Offset: 0x00021024
		public static string GetCurrentLocale()
		{
			string result;
			try
			{
				result = Thread.CurrentThread.CurrentCulture.Name;
			}
			catch
			{
				result = "en-US";
			}
			return result;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00022E60 File Offset: 0x00021060
		public static void StopBlueStacksIfUpgrade(bool isUpgrade, List<string> svcNames, string clientInstallDir, out bool isServiceStopped)
		{
			isServiceStopped = true;
			if (isUpgrade)
			{
				CommonInstallUtils.KillBlueStacksProcesses(clientInstallDir, false);
				CommonInstallUtils.RunHdQuit(CommonInstallUtils.EngineInstallDir);
				Utils.KillCurrentOemProcessByName("HD-Player", null);
				Utils.KillCurrentOemProcessByName("BstkSVC", null);
				if (svcNames != null)
				{
					foreach (string text in svcNames)
					{
						ServiceManager.StopService(text, false);
						isServiceStopped = (isServiceStopped && CommonInstallUtils.CheckIfServiceStopped(text));
					}
				}
			}
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00022EF0 File Offset: 0x000210F0
		private static bool CheckIfServiceStopped(string svcName)
		{
			try
			{
				Logger.Info("Checking if {0} is stopped", new object[]
				{
					svcName
				});
				ServiceController[] devices = ServiceController.GetDevices();
				int i = 0;
				while (i < devices.Length)
				{
					ServiceController serviceController = devices[i];
					if (serviceController.ServiceName == svcName)
					{
						ServiceControllerStatus status = serviceController.Status;
						Logger.Info("Service status of {0} is {1}", new object[]
						{
							svcName,
							status
						});
						if (status != ServiceControllerStatus.Stopped)
						{
							Logger.Warning("Service is not stopped, returning false");
							return false;
						}
						break;
					}
					else
					{
						i++;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("An error occured while checking if svc is stopped, ex: " + ex.ToString());
			}
			return true;
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00022FA0 File Offset: 0x000211A0
		public static bool IsUpgradePossible(string clientKeyPath)
		{
			Logger.Info("Checking if upgrade possible");
			bool flag = false;
			Version v = new Version("3.52.66.1905");
			Version v2 = new Version("2.52.66.8704");
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(clientKeyPath))
			{
				string text = string.Empty;
				string text2 = string.Empty;
				if (registryKey != null)
				{
					text = (string)registryKey.GetValue("ClientVersion", "");
					if (string.IsNullOrEmpty(text))
					{
						text2 = (string)registryKey.GetValue("Version", "");
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					Version v3 = new Version(text);
					Logger.Info("Previous client version is {0}", new object[]
					{
						text
					});
					if (v3 >= v)
					{
						flag = true;
					}
				}
				else if (!string.IsNullOrEmpty(text2))
				{
					Version v4 = new Version(text2);
					Logger.Info("Previous engine version is {0}", new object[]
					{
						text2
					});
					if (v4 >= v2)
					{
						flag = true;
					}
				}
				else
				{
					Logger.Info("ClientVersion as well as Version registry not found so setting isUpgradePossible to false");
					flag = false;
				}
			}
			Logger.Info("IsUpgradePossible: {0}", new object[]
			{
				flag
			});
			return flag;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x000230D8 File Offset: 0x000212D8
		public static string GenerateUniqueInstallId()
		{
			Logger.Info("Generating install ID");
			try
			{
				string text = Guid.NewGuid().ToString();
				Logger.Info("GeneratedID: " + text);
				return text;
			}
			catch (Exception ex)
			{
				Logger.Warning("Failed to generate unique install id");
				Logger.Warning(ex.ToString());
			}
			return string.Empty;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00023144 File Offset: 0x00021344
		public static void LogAllDirs(List<string> listOfDirs)
		{
			Logger.Info("-----------------------------------------------------");
			Logger.Info("Logging all dirs");
			foreach (string dir in listOfDirs.Distinct<string>().ToList<string>())
			{
				CommonInstallUtils.LogDir(dir, false);
			}
			Logger.Info("-----------------------------------------------------");
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x000231B8 File Offset: 0x000213B8
		public static void LogDir(string dir, bool onlyDirs = false)
		{
			try
			{
				CommonInstallUtils.DumpDirListing(dir, onlyDirs);
			}
			catch (Exception ex)
			{
				Logger.Warning("Couldn't log dir {0}, ignoring exception: {1}", new object[]
				{
					dir,
					ex.Message
				});
			}
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00023200 File Offset: 0x00021400
		private static void DumpDirListing(string dir, bool onlyDirs = false)
		{
			string args = string.Format(CultureInfo.InvariantCulture, "/c dir \"{0}\" /s", new object[]
			{
				dir
			});
			if (onlyDirs)
			{
				args = string.Format(CultureInfo.InvariantCulture, "/c dir \"{0}\" /ad /s", new object[]
				{
					dir
				});
			}
			RunCommand.RunCmd("cmd", args, true, true, false, 0);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00023254 File Offset: 0x00021454
		public static bool CheckForOldConfigFiles(string dataDir)
		{
			try
			{
				string path = Path.Combine(dataDir, "UserData\\InputMapper\\UserFiles");
				if (Directory.Exists(path))
				{
					foreach (FileInfo fileInfo in new DirectoryInfo(path).GetFiles("*.cfg"))
					{
						if (fileInfo.Length != 0L)
						{
							return true;
						}
						Logger.Warning("Zero length config file found, ignoring: {0}", new object[]
						{
							fileInfo.Name
						});
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Some error in checking for old Config Files: " + ex.Message);
			}
			return false;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x000232F0 File Offset: 0x000214F0
		public static bool CheckIfValidJsonFile(string jsonFileName)
		{
			try
			{
				Logger.Info("Checking if {0} is a valid json file", new object[]
				{
					jsonFileName
				});
				JArray.Parse(File.ReadAllText(jsonFileName));
				return true;
			}
			catch (Exception ex)
			{
				Logger.Error("{0} may be corrupt. Ex: {1}", new object[]
				{
					jsonFileName,
					ex.Message
				});
			}
			return false;
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00023354 File Offset: 0x00021554
		public static string ZipLogsAndRegistry(string logsDir, string currentInstallerLogPath)
		{
			Logger.Info("In ZipLogsAndRegistry");
			string result;
			try
			{
				string text = CommonInstallUtils.CreateStagingDir();
				string text2 = Path.Combine(text, "Logs");
				if (Directory.Exists(logsDir))
				{
					Directory.CreateDirectory(text2);
					CommonInstallUtils.CopyFiles(logsDir, text2);
					CommonInstallUtils.ExportBluestacksRegistry(text, "RegHKLM.txt");
					string destFileName = Path.Combine(text, Path.GetFileName(currentInstallerLogPath));
					File.Copy(currentInstallerLogPath, destFileName);
					string text3 = Path.Combine(Path.GetTempPath(), "Installer.zip");
					CommonInstallUtils.CreateZipFile(text, text3);
					Directory.Delete(text2, true);
					result = text3;
				}
				else
				{
					result = null;
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Error in creating zip file " + ex.ToString());
				result = null;
			}
			return result;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0002340C File Offset: 0x0002160C
		private static void CreateZipFile(string zipFolderTempPath, string zipFilePath)
		{
			try
			{
				string prog = Path.Combine(Directory.GetCurrentDirectory(), "7zr.exe");
				string args = string.Format(CultureInfo.InvariantCulture, "a {0} -m0=LZMA:a=2 {1}\\*", new object[]
				{
					zipFilePath,
					zipFolderTempPath
				});
				if (File.Exists(zipFilePath))
				{
					File.Delete(zipFilePath);
				}
				Utils.RunCmd(prog, args, null);
			}
			catch (Exception ex)
			{
				Logger.Error("Error in creating Zip " + ex.ToString());
			}
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00023488 File Offset: 0x00021688
		private static void ExportBluestacksRegistry(string destination, string fileName)
		{
			try
			{
				string args = string.Format(CultureInfo.InvariantCulture, "EXPORT HKLM\\{0} \"{1}\"", new object[]
				{
					Strings.RegistryBaseKeyPath,
					Path.Combine(destination, fileName)
				});
				Utils.RunCmd("reg.exe", args, null);
			}
			catch
			{
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x000234E0 File Offset: 0x000216E0
		private static void CopyFiles(string src, string dest)
		{
			foreach (string text in Directory.GetFiles(src))
			{
				string fileName = Path.GetFileName(text);
				string destFileName = Path.Combine(dest, fileName);
				File.Copy(text, destFileName);
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0002351C File Offset: 0x0002171C
		private static string CreateStagingDir()
		{
			string randomFileName = Path.GetRandomFileName();
			string text = Path.Combine(Path.GetTempPath(), randomFileName);
			if (Directory.Exists(text))
			{
				Directory.Delete(text);
			}
			Directory.CreateDirectory(text);
			return text;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00023554 File Offset: 0x00021754
		public static Dictionary<string, string> GetSupportedGLModes(string glCheckDir)
		{
			Logger.Info("Checking for supported GL Modes");
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			List<string> list = new List<string>();
			try
			{
				foreach (string text in new List<string>
				{
					"1 1",
					"4 1",
					"1 2",
					"4 2"
				})
				{
					if (CommonInstallUtils.RunGLCheck(glCheckDir, text) == 0)
					{
						list.Add(text);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("An error occured while checking for supported GLModes");
				Logger.Error(ex.ToString());
			}
			dictionary["supported_glmodes"] = string.Join(",", list.ToArray());
			Logger.Info("Supported GL Modes: " + string.Join(",", list.ToArray()));
			return dictionary;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x00023650 File Offset: 0x00021850
		private static int RunGLCheck(string glCheckDir, string args)
		{
			try
			{
				return RunCommand.RunCmd(Path.Combine(glCheckDir, "HD-GLCheck.exe"), args, true, true, false, 10000).ExitCode;
			}
			catch (Exception ex)
			{
				Logger.Warning("An error occured while running glcheck, ignorning.");
				Logger.Warning(ex.Message);
			}
			return -1;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00003C8E File Offset: 0x00001E8E
		public static void CheckIfVulkanSupported()
		{
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000236A8 File Offset: 0x000218A8
		public static void WriteClientVersionInFile(string version)
		{
			int i = 5;
			while (i > 0)
			{
				try
				{
					string text = Path.Combine(RegistryManager.Instance.UserDefinedDir, "bst_params.txt");
					CommonInstallUtils.WriteToFile(text, string.Format(CultureInfo.InvariantCulture, "version={0}", new object[]
					{
						version
					}), "version");
					Logger.Info("version written to file successfully");
					string identity = new SecurityIdentifier("S-1-1-0").Translate(typeof(NTAccount)).ToString();
					FileInfo fileInfo = new FileInfo(text);
					FileSecurity accessControl = fileInfo.GetAccessControl();
					accessControl.AddAccessRule(new FileSystemAccessRule(identity, FileSystemRights.FullControl, AccessControlType.Allow));
					fileInfo.SetAccessControl(accessControl);
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
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0002379C File Offset: 0x0002199C
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

		// Token: 0x06000703 RID: 1795 RVA: 0x0000611B File Offset: 0x0000431B
		public static int SetupVmConfig(string installDir, string dataDir)
		{
			Logger.Info("In SetupVmConfig");
			if (!CommonInstallUtils.InstallVmConfig(installDir, dataDir))
			{
				Logger.Info("Throwing error cannot create android.bstk from in file");
				return -42;
			}
			return 0;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0000613E File Offset: 0x0000433E
		public static int SetupBstkGlobalConfig(string dataDir)
		{
			Logger.Info("In SetupBstkGlobalConfig");
			if (!CommonInstallUtils.InstallVirtualBoxConfig(dataDir, false))
			{
				Logger.Info("Throwing error cannot create bstkglobal from in file");
				return -41;
			}
			return 0;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000238A4 File Offset: 0x00021AA4
		public static bool InstallVirtualBoxConfig(string dataDir, bool isUpgrade = false)
		{
			Logger.Info("InstallVirtualBoxConfig()");
			string path = Path.Combine(dataDir, "Manager");
			string text = Path.Combine(Path.Combine(dataDir, "Manager"), "BstkGlobal.xml");
			string text2 = Path.Combine(path, "BstkGlobal.xml.in");
			string text3 = null;
			try
			{
				using (StreamReader streamReader = new StreamReader(text2))
				{
					text3 = streamReader.ReadToEnd();
				}
			}
			catch (Exception ex)
			{
				string str = "Cannot read '";
				string str2 = text2;
				string str3 = "': ";
				Exception ex2 = ex;
				Logger.Error(str + str2 + str3 + ((ex2 != null) ? ex2.ToString() : null));
				return false;
			}
			string str4 = dataDir;
			if (isUpgrade)
			{
				str4 = RegistryStrings.DataDir.TrimEnd(new char[]
				{
					'\\'
				});
			}
			string text4 = text3;
			text4 = text4.Replace("@@USER_DEFINED_DIR@@", str4 + "\\");
			try
			{
				using (StreamWriter streamWriter = new StreamWriter(text))
				{
					streamWriter.Write(text4);
				}
			}
			catch (Exception ex3)
			{
				string str5 = "Cannot write '";
				string str6 = text;
				string str7 = "': ";
				Exception ex4 = ex3;
				Logger.Error(str5 + str6 + str7 + ((ex4 != null) ? ex4.ToString() : null));
				return false;
			}
			return true;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x000239F0 File Offset: 0x00021BF0
		public static bool InstallVmConfig(string installDir, string dataDir)
		{
			Logger.Info("InstallVmConfig()");
			string path = Path.Combine(dataDir, "Android");
			string text = Path.Combine(path, "Android.bstk");
			string text2 = Path.Combine(path, "Android.bstk.in");
			string text3 = null;
			if (File.Exists(text))
			{
				Logger.Info("android.bstk already present returning");
				return true;
			}
			try
			{
				using (StreamReader streamReader = new StreamReader(text2))
				{
					text3 = streamReader.ReadToEnd();
				}
			}
			catch (Exception ex)
			{
				string str = "Cannot read '";
				string str2 = text2;
				string str3 = "': ";
				Exception ex2 = ex;
				Logger.Info(str + str2 + str3 + ((ex2 != null) ? ex2.ToString() : null));
				return false;
			}
			string text4 = text3;
			text4 = text4.Replace("@@HD_PLUS_DEVICES_DLL_PATH@@", SecurityElement.Escape(Path.Combine(installDir, "HD-Plus-Devices.dll")));
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			if (!string.IsNullOrEmpty(folderPath))
			{
				string newValue = string.Format(CultureInfo.InvariantCulture, "<SharedFolder name=\"Documents\" hostPath=\"{0}\" writable=\"true\" autoMount=\"false\"/>", new object[]
				{
					SecurityElement.Escape(folderPath)
				});
				text4 = text4.Replace("@@USER_DOCUMENTS_FOLDER@@", newValue);
			}
			else
			{
				text4 = text4.Replace("@@USER_DOCUMENTS_FOLDER@@", "");
			}
			string folderPath2 = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
			if (!string.IsNullOrEmpty(folderPath2))
			{
				string newValue2 = string.Format(CultureInfo.InvariantCulture, "<SharedFolder name=\"Pictures\" hostPath=\"{0}\" writable=\"true\" autoMount=\"false\"/>", new object[]
				{
					SecurityElement.Escape(folderPath2)
				});
				text4 = text4.Replace("@@USER_PICTURES_FOLDER@@", newValue2);
			}
			else
			{
				text4 = text4.Replace("@@USER_PICTURES_FOLDER@@", "");
			}
			text4 = text4.Replace("@@INPUT_MAPPER_FOLDER@@", SecurityElement.Escape(Path.Combine(dataDir, "UserData\\InputMapper")));
			text4 = text4.Replace("@@BST_SHARED_FOLDER@@", SecurityElement.Escape(Path.Combine(dataDir, "UserData\\SharedFolder")));
			text4 = text4.Replace("@@BST_VM_MEMORY_SIZE@@", SecurityElement.Escape(Utils.GetAndroidVMMemory(true).ToString(CultureInfo.InvariantCulture)));
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.LoadXml(text4);
				xmlDocument.Save(text);
			}
			catch (Exception ex3)
			{
				string str4 = "Cannot write '";
				string str5 = text;
				string str6 = "': ";
				Exception ex4 = ex3;
				Logger.Info(str4 + str5 + str6 + ((ex4 != null) ? ex4.ToString() : null));
				return false;
			}
			return true;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00023C1C File Offset: 0x00021E1C
		public static bool CheckSupportedGlRenderMode(out int glRenderMode, out string glVendor, out string glRenderer, out string glVersion, out GLMode glMode, List<GlProperty> glCheckOrder, List<GlProperty> fallbackGlCheckOrder)
		{
			glRenderMode = 4;
			glVersion = "";
			glRenderer = "";
			glVendor = "";
			glMode = GLMode.PGA;
			try
			{
				if (glCheckOrder != null && glCheckOrder.Count > 0)
				{
					foreach (GlProperty glProperty in glCheckOrder)
					{
						Logger.Info("gl check with args.." + glProperty.Gl_renderer.ToString() + " and " + glProperty.Gl_mode.ToString());
						if (BlueStacksGL.GLCheckInstallation(glProperty.Gl_renderer, glProperty.Gl_mode, out glVendor, out glRenderer, out glVersion) == 0)
						{
							glRenderMode = (int)glProperty.Gl_renderer;
							glMode = glProperty.Gl_mode;
							return true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				string str = "exception in getting gl check from list..";
				Exception ex2 = ex;
				Logger.Error(str + ((ex2 != null) ? ex2.ToString() : null));
			}
			if (Oem.Instance.CheckForAGAInInstaller && CommonInstallUtils.CheckSupportedGlRenderMode(GLMode.AGA, out glRenderMode, out glVendor, out glRenderer, out glVersion))
			{
				Logger.Info("AGA supported!");
				glMode = GLMode.AGA;
				return true;
			}
			if (fallbackGlCheckOrder != null && fallbackGlCheckOrder.Count > 0)
			{
				foreach (GlProperty glProperty2 in fallbackGlCheckOrder)
				{
					Logger.Info("gl check with args.." + glProperty2.Gl_renderer.ToString() + " and " + glProperty2.Gl_mode.ToString());
					if (BlueStacksGL.GLCheckInstallation(glProperty2.Gl_renderer, glProperty2.Gl_mode, out glVendor, out glRenderer, out glVersion) == 0)
					{
						glRenderMode = (int)glProperty2.Gl_renderer;
						glMode = glProperty2.Gl_mode;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00023E14 File Offset: 0x00022014
		public static bool CheckSupportedGlRenderMode(GLMode mode, out int glRenderMode, out string glVendor, out string glRenderer, out string glVersion)
		{
			glRenderMode = 4;
			glVersion = "";
			glRenderer = "";
			glVendor = "";
			try
			{
				int num;
				if (BlueStacksGL.GLCheckInstallation(GLRenderer.OpenGL, mode, out glVendor, out glRenderer, out glVersion) == 0)
				{
					num = 0;
					glRenderMode = 1;
				}
				else if (SystemUtils.IsOs64Bit())
				{
					if (CommonInstallUtils.GpuWithDx9SupportOnly())
					{
						Logger.Info("Machine has gpu which runs on DX9 only");
						glRenderMode = 2;
						num = BlueStacksGL.GLCheckInstallation(GLRenderer.DX9, mode, out glVendor, out glRenderer, out glVersion);
					}
					else
					{
						glRenderMode = 4;
						num = BlueStacksGL.GLCheckInstallation(GLRenderer.DX11FallbackDX9, mode, out glVendor, out glRenderer, out glVersion);
						if (num != 0)
						{
							glRenderMode = 2;
							num = BlueStacksGL.GLCheckInstallation(GLRenderer.DX9, mode, out glVendor, out glRenderer, out glVersion);
						}
					}
				}
				else
				{
					glRenderMode = 2;
					num = BlueStacksGL.GLCheckInstallation(GLRenderer.DX9, mode, out glVendor, out glRenderer, out glVersion);
				}
				if (num != 0)
				{
					Logger.Info("DirectX not supported.");
					glRenderMode = -1;
					return false;
				}
				return true;
			}
			catch (Exception ex)
			{
				Logger.Error("Some error occured while checking for GL. Ex: {0}", new object[]
				{
					ex
				});
			}
			return false;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00023EEC File Offset: 0x000220EC
		public static bool GpuWithDx9SupportOnly()
		{
			string[] array = new string[]
			{
				"Mobile Intel(R) 4 Series Express Chipset Family",
				"Mobile Intel(R) 45 Express Chipset Family",
				"Mobile Intel(R) 965 Express Chipset Family",
				"Intel(R) G41 Express Chipset",
				"Intel(R) G45/G43 Express Chipset",
				"Intel(R) Q45/Q43 Express Chipset"
			};
			string text = "";
			try
			{
				using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DisplayConfiguration"))
				{
					foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
					{
						foreach (PropertyData propertyData in ((ManagementObject)managementBaseObject).Properties)
						{
							if (propertyData.Name == "Description")
							{
								text = propertyData.Value.ToString();
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error("Exception while runninq query. Error: ", new object[]
				{
					ex.ToString()
				});
			}
			Logger.Info("Graphics card: {0}", new object[]
			{
				text
			});
			string text2 = text.ToLower(CultureInfo.InvariantCulture);
			if (text2.Contains("intel") && text2.Contains("express chipset"))
			{
				Logger.Info("graphicsCard : {0} part of the list of graphics card to be forced to dx9", new object[]
				{
					text
				});
				return true;
			}
			return false;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00024078 File Offset: 0x00022278
		public static bool IsProcessorIntel()
		{
			bool result;
			try
			{
				Dictionary<string, string> dictionary = Profile.Info();
				if (dictionary != null && dictionary["Processor"].Contains("Intel"))
				{
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch
			{
				Logger.Error("Unable to detect for intel processor");
				result = false;
			}
			return result;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x000240D0 File Offset: 0x000222D0
		public static string GetPrebundledVdiUid(string file)
		{
			string text = string.Empty;
			if (!File.Exists(file))
			{
				return "";
			}
			string s = File.ReadAllText(file);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
			xmlNamespaceManager.AddNamespace("bstk", "http://www.virtualbox.org/");
			string result;
			using (XmlReader xmlReader = XmlReader.Create(new StringReader(s), new XmlReaderSettings
			{
				XmlResolver = null
			}))
			{
				xmlDocument.Load(xmlReader);
				foreach (object obj in xmlDocument.SelectNodes("descendant::bstk:Machine//bstk:MediaRegistry//bstk:HardDisks//bstk:HardDisk", xmlNamespaceManager))
				{
					XmlNode xmlNode = (XmlNode)obj;
					if (xmlNode.Attributes["location"].Value.Equals("Prebundled.vdi", StringComparison.InvariantCultureIgnoreCase))
					{
						text = xmlNode.Attributes["uuid"].Value;
						break;
					}
				}
				result = text;
			}
			return result;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x000241EC File Offset: 0x000223EC
		public static string ReadFile(string path)
		{
			string result = null;
			try
			{
				using (StreamReader streamReader = new StreamReader(path))
				{
					result = streamReader.ReadToEnd();
				}
			}
			catch (Exception ex)
			{
				Logger.Warning("Cannot read '{0}', Ex: {1}", new object[]
				{
					path,
					ex
				});
			}
			return result;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00024250 File Offset: 0x00022450
		public static bool FixVMConfigJson(string oldVmSettingsPath, string newVmFolder, string defaultNewVmFolder)
		{
			bool result;
			try
			{
				Logger.Info("Will update settings from oldVmSettings {0} in new folder {1} using defaults from folder: {2}", new object[]
				{
					oldVmSettingsPath,
					newVmFolder,
					defaultNewVmFolder
				});
				string text = CommonInstallUtils.ReadFile(oldVmSettingsPath);
				if (text == null)
				{
					result = false;
				}
				else
				{
					string text2 = CommonInstallUtils.ReadFile(Path.Combine(defaultNewVmFolder, "Android.json"));
					if (text2 == null)
					{
						result = false;
					}
					else
					{
						JObject jobject = (JObject)JsonConvert.DeserializeObject(text);
						JObject jobject2 = (JObject)JsonConvert.DeserializeObject(text2);
						Logger.Info("Modifying attachments");
						string text3 = jobject["Owner"].ToString();
						JObject jobject3 = (JObject)jobject["VirtualMachine"]["Devices"]["Scsi"]["Boot Disk Controller"]["Attachments"];
						Dictionary<string, JObject> dictionary = null;
						int num = 0;
						foreach (JProperty jproperty in jobject3.Properties())
						{
							if (dictionary == null)
							{
								dictionary = new Dictionary<string, JObject>();
							}
							string text4 = ((JObject)jproperty.Value).ToString().ToLower(CultureInfo.InvariantCulture);
							if (!text4.Contains("fastboot.vhdx") && !text4.Contains("prebundled.vhdx"))
							{
								dictionary[num.ToString(NumberFormatInfo.InvariantInfo)] = (JObject)jproperty.Value;
								num++;
							}
						}
						jobject3 = new JObject();
						foreach (string text5 in dictionary.Keys)
						{
							jobject3[text5] = dictionary[text5];
						}
						jobject2["VirtualMachine"]["Devices"]["Scsi"]["Boot Disk Controller"]["Attachments"] = jobject3;
						if (!text3.Contains("bgp", StringComparison.InvariantCultureIgnoreCase))
						{
							text3 += "_bgp";
						}
						jobject2["Owner"] = text3;
						Logger.Info("Setting owner: {0}", new object[]
						{
							text3
						});
						string text6 = Path.Combine(newVmFolder, "Android.json");
						Logger.Info("Writing setting file at {0}", new object[]
						{
							text6
						});
						File.WriteAllText(text6, JsonConvert.SerializeObject(jobject2, Newtonsoft.Json.Formatting.Indented));
						result = true;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Warning("Some error with removing fastboot entry. Ex: {0}", new object[]
				{
					ex
				});
				result = false;
			}
			return result;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00024514 File Offset: 0x00022714
		public static bool UpdateEndpointWithNewGuid(string vmFolder)
		{
			string path = Path.Combine(vmFolder, "Android.json");
			string path2 = Path.Combine(vmFolder, "Android.Endpoint.json");
			string text = CommonInstallUtils.ReadFile(path);
			string text2 = CommonInstallUtils.ReadFile(path2);
			if (text == null || text2 == null)
			{
				return false;
			}
			Logger.Info("Updating endpoints with new guid in {0}", new object[]
			{
				vmFolder
			});
			JObject jobject = (JObject)JsonConvert.DeserializeObject(text);
			JObject jobject2 = (JObject)JsonConvert.DeserializeObject(text2);
			Guid guid = Guid.NewGuid();
			jobject["VirtualMachine"]["Devices"]["NetworkAdapters"]["default"]["EndpointId"] = guid.ToString();
			jobject2["VirtualNetwork"] = guid.ToString();
			Logger.Info("Writing new guid {0}", new object[]
			{
				guid
			});
			File.WriteAllText(path, JsonConvert.SerializeObject(jobject, Newtonsoft.Json.Formatting.Indented));
			File.WriteAllText(path2, JsonConvert.SerializeObject(jobject2, Newtonsoft.Json.Formatting.Indented));
			return true;
		}

		// Token: 0x040003C0 RID: 960
		private const string OpenGL_Native_DLL = "HD-OpenGl-Native.dll";

		// Token: 0x040003C1 RID: 961
		private const int CSIDL_COMMON_DESKTOPDIRECTORY = 25;

		// Token: 0x040003C2 RID: 962
		internal static List<string> sDisallowedDeletionStrings = new List<string>
		{
			"*",
			"\\",
			Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
			Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles),
			Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
		};

		// Token: 0x040003C3 RID: 963
		private static bool is64BitProcess = IntPtr.Size == 8;
	}
}
