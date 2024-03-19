using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Markup;
using BlueStacks.Common;
using CodeTitans.JSon;
using Microsoft.Win32;

namespace BlueStacks.MicroInstaller
{
	// Token: 0x0200009A RID: 154
	public partial class App : System.Windows.Application
	{
		// Token: 0x0600033D RID: 829 RVA: 0x0000F8DB File Offset: 0x0000DADB
		private static void CheckForBgp64Oem()
		{
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00014780 File Offset: 0x00012980
		private static void Init()
		{
			string path = Path.Combine(Path.GetTempPath(), "BlueStacks");
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			string text = Path.Combine(folderPath, "BlueStacks");
			bool flag = !Directory.Exists(path);
			if (flag)
			{
				Directory.CreateDirectory(path);
			}
			bool flag2 = !Directory.Exists(text);
			if (flag2)
			{
				Directory.CreateDirectory(text);
			}
			bool flag3 = !Directory.Exists(Strings.BlueStacksSetupFolder);
			if (flag3)
			{
				Directory.CreateDirectory(Strings.BlueStacksSetupFolder);
			}
			string logFilePath = Path.Combine(text, string.Format("BlueStacksMicroInstaller_{0}.log", "4.250.0.1070"));
			Logger.InitLogAtPath(logFilePath, "", false);
			Globalization.InitLocalization(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Locales"));
			System.Windows.Forms.Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
			System.Windows.Forms.Application.ThreadException += App.Application_ThreadException;
			AppDomain.CurrentDomain.UnhandledException += App.CurrentDomain_UnhandledException;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00014870 File Offset: 0x00012A70
		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			Logger.Fatal("Unhandled Thread Exception:");
			Logger.Fatal(e.Exception.ToString());
			System.Windows.Forms.MessageBox.Show("BlueStacks Installer.\nError: " + e.Exception.ToString());
			App.ExitApplication(0);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000148BC File Offset: 0x00012ABC
		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			Logger.Fatal("Unhandled Application Exception.");
			Logger.Fatal("Err: " + e.ExceptionObject.ToString());
			System.Windows.Forms.MessageBox.Show("BlueStacks Installer.\nError: " + e.ExceptionObject.ToString());
			App.ExitApplication(0);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00014914 File Offset: 0x00012B14
		public static void ShowUi()
		{
			App app = new App();
			app.InitializeComponent();
			app.Exit += App.Application_Exit;
			app.Run();
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00014949 File Offset: 0x00012B49
		private static void Application_Exit(object sender, ExitEventArgs e)
		{
			App.ExitApplication(0);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00014954 File Offset: 0x00012B54
		public static void ExitApplication(int exitCode = 0)
		{
			bool flag = exitCode == 2;
			if (flag)
			{
				App.RunBlueStacks();
			}
			else
			{
				try
				{
					bool flag2 = Directory.Exists(App.sFLEAssetsDir);
					if (flag2)
					{
						Directory.Delete(App.sFLEAssetsDir, true);
					}
				}
				catch
				{
				}
				Environment.Exit(exitCode);
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000149B4 File Offset: 0x00012BB4
		private static void RunBlueStacks()
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\BlueStacks" + App.sRegistrySuffix);
			Logger.Info("Running BlueStacks from regPath: {0}", new object[]
			{
				registryKey.Name
			});
			bool flag = registryKey != null;
			if (flag)
			{
				string text = (string)registryKey.GetValue("InstallDir", null);
				bool flag2 = !string.IsNullOrEmpty(text);
				if (flag2)
				{
					string text2 = Path.Combine(text, "BlueStacks.exe");
					bool flag3 = File.Exists(text2);
					if (flag3)
					{
						string text3 = "-force";
						try
						{
							bool flag4 = SystemUtils.IsAdministrator();
							if (flag4)
							{
								ProcessUtils.ExecuteProcessUnElevated(text2, text3, text);
							}
							else
							{
								Process.Start(text2, text3);
							}
						}
						catch (Exception ex)
						{
							Logger.Warning("Couldn't launch {0} from {1}\n{2}", new object[]
							{
								"BlueStacks",
								text2,
								ex.Message
							});
							new StrippedMessageWindow
							{
								BodyTextBlock = 
								{
									Text = string.Format("Couldn't launch {0} from {1}\n{2}", "BlueStacks", text2, ex.Message)
								},
								ShowInTaskbar = true,
								Topmost = true
							}.ShowDialog();
						}
					}
					else
					{
						Logger.Warning("Couldn't find client bin at {0} ", new object[]
						{
							text2
						});
						new StrippedMessageWindow
						{
							BodyTextBlock = 
							{
								Text = string.Format("Could not find {0} at {1}", "BlueStacks", text2)
							},
							ShowInTaskbar = true,
							Topmost = true
						}.ShowDialog();
					}
				}
				else
				{
					Logger.Warning("InstallDir is null or empty at {0}", new object[]
					{
						registryKey.Name
					});
					new StrippedMessageWindow
					{
						BodyTextBlock = 
						{
							Text = "Could not find installation path"
						},
						ShowInTaskbar = true,
						Topmost = true
					}.ShowDialog();
				}
				registryKey.Close();
			}
			Environment.Exit(0);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00014BB4 File Offset: 0x00012DB4
		public static void SetUpgradeMode()
		{
			string text = "4.250.0.1070";
			bool flag = MicroInstallerProperties.BuildPlatformToDownload == DownloadBuildType.CustomHyperV;
			if (flag)
			{
				text = "4.240.15.4204";
			}
			string registryPath = "Software\\BlueStacks" + App.sRegistrySuffix;
			string text2 = (string)RegistryUtils.GetRegistryValue(registryPath, "Version", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			string sCurrentInstallMode = InstallationModes.Fresh;
			bool flag2 = !string.IsNullOrEmpty(text2);
			if (flag2)
			{
				Logger.Info("Old version: {0}, new version: {1}", new object[]
				{
					text2,
					text
				});
				DownloaderStats.sOldVersion = text2;
				System.Version v = new System.Version(text2.Substring(0, text2.LastIndexOf(".")));
				System.Version v2 = new System.Version(text.Substring(0, text.LastIndexOf(".")));
				bool flag3 = v2 > v;
				if (flag3)
				{
					sCurrentInstallMode = InstallationModes.Upgrade;
				}
				else
				{
					bool flag4 = v2 == v;
					if (flag4)
					{
						sCurrentInstallMode = InstallationModes.Same;
					}
					else
					{
						sCurrentInstallMode = InstallationModes.Lower;
					}
				}
			}
			DownloaderStats.sCurrentInstallMode = sCurrentInstallMode;
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00014CB4 File Offset: 0x00012EB4
		private static void ShowAlreadyInstalledPopupIfPossible(string installationMode)
		{
			bool flag = installationMode != InstallationModes.Upgrade && installationMode != InstallationModes.Fresh;
			if (flag)
			{
				DownloaderStats.SendStats("mi_failed", "already_installed", "", "");
				bool flag2 = !App.GetAndRunFleCampaignJson();
				if (flag2)
				{
					StrippedMessageWindow strippedMessageWindow = new StrippedMessageWindow();
					strippedMessageWindow.TitleTextBlock.Text = Globalization.GetLocalizedString("STRING_VERSION_ALREADY_INSTALLED");
					strippedMessageWindow.BodyTextBlock.Text = Globalization.GetLocalizedString("STRING_LATEST_VERSION_INSTALLED");
					strippedMessageWindow.AddButton(ButtonColors.Blue, Globalization.GetLocalizedString("STRING_LAUNCH"), new EventHandler(App.VersionAlreadyInstalledLaunchBSHandler), null, false, null);
					strippedMessageWindow.AddButton(ButtonColors.Blue, Globalization.GetLocalizedString("STRING_OK"), new EventHandler(App.VersionAlreadyInstalledHandler), null, false, null);
					strippedMessageWindow.CloseButtonHandle(new EventHandler(App.VersionAlreadyInstalledHandler), null);
					strippedMessageWindow.ShowInTaskbar = true;
					strippedMessageWindow.Topmost = true;
					strippedMessageWindow.ShowDialog();
				}
				else
				{
					App.VersionAlreadyInstalledHandler(null, null);
				}
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00014DBC File Offset: 0x00012FBC
		private static void CheckAndAssignDownloadBuildPlatform()
		{
			Logger.Info("Assigning build platform");
			bool flag = !string.IsNullOrEmpty("4.240.15.4204") && SystemUtils.IsOs64Bit() && HyperV.Instance.IsMicrosoftHyperVPresent;
			if (flag)
			{
				MicroInstallerProperties.BuildPlatformToDownload = DownloadBuildType.CustomHyperV;
				App.AssignRegistrySuffix("bgp64_hyperv");
			}
			else
			{
				MicroInstallerProperties.BuildPlatformToDownload = DownloadBuildType.Default;
				App.AssignRegistrySuffix("bgp");
			}
			Logger.Info("Build platform: {0}", new object[]
			{
				MicroInstallerProperties.BuildPlatformToDownload
			});
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00014E44 File Offset: 0x00013044
		private static void AssignRegistrySuffix(string oem)
		{
			bool flag = !string.Equals(oem, "bgp", StringComparison.InvariantCultureIgnoreCase);
			if (flag)
			{
				App.sRegistrySuffix = "_" + oem;
				Logger.Info("Suffix: {0}", new object[]
				{
					App.sRegistrySuffix
				});
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00014E90 File Offset: 0x00013090
		private static bool GetAndRunFleCampaignJson()
		{
			bool flag = !string.IsNullOrEmpty(App.sCampaignHash) && App.sCampaignHash != "null";
			if (flag)
			{
				try
				{
					string text = "bgp";
					string text2 = "4.250.0.1070";
					bool flag2 = MicroInstallerProperties.BuildPlatformToDownload == DownloadBuildType.CustomHyperV;
					if (flag2)
					{
						text = "bgp64_hyperv";
						text2 = "4.240.15.4204";
					}
					string url = string.Format("{0}/bs3/getcampaigninfo?md5_hash={1}&prod_ver={2}&oem={3}", new object[]
					{
						DownloaderUtils.Host,
						App.sCampaignHash,
						text2,
						text
					});
					string webResponse = DownloaderUtils.GetWebResponse(url, null, 5000);
					bool flag3 = !string.IsNullOrEmpty(webResponse.Trim());
					if (flag3)
					{
						IJSonReader ijsonReader = new JSonReader();
						IJSonObject ijsonObject = ijsonReader.ReadAsJSonObject(webResponse.Trim());
						bool flag4 = ijsonObject.Contains("app_pkg");
						if (flag4)
						{
							string value = ijsonObject["app_pkg"].StringValue.Trim();
							bool flag5 = !string.IsNullOrEmpty(value);
							if (flag5)
							{
								string str = "";
								bool flag6 = text != "bgp";
								if (flag6)
								{
									str = "_" + text;
								}
								string defaultValue = "C:\\Program Files\\BlueStacks" + str;
								string path = (string)RegistryUtils.GetRegistryValue(Strings.RegistryBaseKeyPath, "InstallDir", defaultValue, RegistryKeyKind.HKEY_LOCAL_MACHINE);
								string fileName = Path.Combine(path, "HD-RunApp.exe");
								Process process = new Process();
								string arguments = string.Concat(new string[]
								{
									"-force -oem ",
									text,
									" -campaignhash ",
									App.sCampaignHash,
									" -addflepkg -json \"",
									webResponse.Trim().Replace("\"", "\\\""),
									"\""
								});
								process.StartInfo.FileName = fileName;
								process.StartInfo.Arguments = arguments;
								process.Start();
								process.WaitForExit();
								return true;
							}
						}
					}
				}
				catch (Exception ex)
				{
					Logger.Info("Error fetching campaign json or starting BlueStacks post that " + ex.ToString());
				}
			}
			return false;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000150C4 File Offset: 0x000132C4
		private static void VersionAlreadyInstalledHandler(object sender, EventArgs e)
		{
			App.ExitApplication(-3);
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000150CF File Offset: 0x000132CF
		private static void VersionAlreadyInstalledLaunchBSHandler(object sender, EventArgs e)
		{
			Logger.Info("Launching BlueStacks (exiting with exit code: {0})", new object[]
			{
				2
			});
			App.ExitApplication(2);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x000150F4 File Offset: 0x000132F4
		public static void LaunchInAdminModeAndWait()
		{
			string fileName = Process.GetCurrentProcess().MainModule.FileName;
			Logger.Info("File path: {0}", new object[]
			{
				fileName
			});
			Process process = new Process();
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.UseShellExecute = true;
			process.StartInfo.FileName = fileName;
			process.StartInfo.Arguments = string.Format("\"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\" \"{5}\"", new object[]
			{
				App.INSTALL_ARG,
				ProcessDetails.CurrentProcessParentFileName,
				App.sCampaignHash,
				App.sUserPriv,
				App.sBlueStacksMachineId,
				App.sBluestacksVersionId
			});
			bool flag = !string.IsNullOrEmpty(App.sAppNameBase64ByteString);
			if (flag)
			{
				process.StartInfo.Arguments = string.Format("{0} {1}", process.StartInfo.Arguments, App.sAppNameBase64ByteString);
			}
			Logger.Info("args: {0}", new object[]
			{
				process.StartInfo.Arguments
			});
			process.StartInfo.Verb = "runas";
			bool flag2 = !App.sIsAdmin;
			if (flag2)
			{
				DownloaderStats.SendStatsAsync("mi_uac_prompted", "", "", "");
			}
			try
			{
				process.Start();
			}
			catch (Exception ex)
			{
				Exception ex4;
				Exception ex2 = ex4;
				Exception ex = ex2;
				Logger.Error("Could not start admin process. Error: {0}", new object[]
				{
					ex.Message
				});
				StrippedMessageWindow strippedMessageWindow = new StrippedMessageWindow();
				strippedMessageWindow.TitleTextBlock.Text = Globalization.GetLocalizedString("STRING_ADMINISTRATIVE_RIGHTS_REQUIRED");
				strippedMessageWindow.BodyTextBlock.Text = Globalization.GetLocalizedString("STRING_INSTALL_REQUIRES_ADMIN");
				strippedMessageWindow.AddButton(ButtonColors.Blue, Globalization.GetLocalizedString("STRING_CONTINUE"), null, null, false, null);
				strippedMessageWindow.AddButton(ButtonColors.White, Globalization.GetLocalizedString("STRING_CANCEL"), delegate(object s, EventArgs ev)
				{
					DownloaderStats.SendStats("mi_failed", "admin_start_failed", ex.Message, "");
					Environment.Exit(-1);
				}, null, false, null);
				strippedMessageWindow.IsWindowClosable = false;
				strippedMessageWindow.ShowInTaskbar = true;
				strippedMessageWindow.Topmost = true;
				strippedMessageWindow.ShowDialog();
				App.RetryLaunchInAdmin(null, null);
				return;
			}
			process.WaitForExit();
			bool flag3 = process.ExitCode == 0 && !App.sIsAdmin;
			if (flag3)
			{
				string name = "Software\\BlueStacks" + App.sRegistrySuffix;
				RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name);
				bool flag4 = registryKey != null;
				if (flag4)
				{
					try
					{
						string fileName2 = Path.Combine((string)registryKey.GetValue("InstallDir"), "BlueStacks.exe");
						string arguments = "-force";
						Process.Start(fileName2, arguments);
						DownloaderStats.SendStats("mi_client_launched", "", "", "");
					}
					catch (Exception ex3)
					{
						DownloaderStats.SendStats("mi_client_launch_failed", "", ex3.Message, "");
					}
				}
				else
				{
					DownloaderStats.SendStats("mi_registry_not_found", "", "", "");
				}
			}
			else
			{
				bool flag5 = process.ExitCode == 0;
				if (flag5)
				{
				}
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00015420 File Offset: 0x00013620
		private static void RetryLaunchInAdmin(object sender, EventArgs e)
		{
			DownloaderStats.SendStats("mi_uac_prompt_retried", "", "", "");
			App.LaunchInAdminModeAndWait();
		}

		// Token: 0x04000522 RID: 1314
		private static string INSTALL_ARG = "install";

		// Token: 0x04000523 RID: 1315
		public static string sParentFileName = "";

		// Token: 0x04000524 RID: 1316
		public static string sCampaignHash = "null";

		// Token: 0x04000525 RID: 1317
		public static string sUserPriv = "non_admin";

		// Token: 0x04000526 RID: 1318
		public static string sArch = InstallerArchitectures.AMD64;

		// Token: 0x04000527 RID: 1319
		public static string sBlueStacksMachineId = "";

		// Token: 0x04000528 RID: 1320
		public static string sBluestacksVersionId = "";

		// Token: 0x04000529 RID: 1321
		public static string sBlueStacksSetupFolder = "";

		// Token: 0x0400052A RID: 1322
		internal static string sRegistrySuffix = "";

		// Token: 0x0400052B RID: 1323
		private static Mutex sAlreadyRunningMutex;

		// Token: 0x0400052C RID: 1324
		public static bool sIsAdmin = false;

		// Token: 0x0400052D RID: 1325
		public static long sStartingTickCount = InteropUtils.GetTickCount64();

		// Token: 0x0400052E RID: 1326
		public static string sFLEAssetsDir = Path.Combine(Path.GetTempPath(), "Assets");

		// Token: 0x0400052F RID: 1327
		public static string sAppNameBase64ByteString = "";

		// Token: 0x04000530 RID: 1328
		internal const string sCustomOem = "bgp64_hyperv";

		// Token: 0x04000531 RID: 1329
		internal const string sCustomVersion = "4.240.15.4204";
	}
}
