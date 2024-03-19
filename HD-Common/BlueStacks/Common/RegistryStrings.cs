using System;
using System.IO;

namespace BlueStacks.Common
{
	// Token: 0x020000F1 RID: 241
	public static class RegistryStrings
	{
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0000574A File Offset: 0x0000394A
		public static string DataDir
		{
			get
			{
				if (RegistryStrings.sDataDir == null)
				{
					RegistryStrings.sDataDir = (string)RegistryUtils.GetRegistryValue(Strings.RegistryBaseKeyPath, "DataDir", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
				}
				return RegistryStrings.sDataDir;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00005777 File Offset: 0x00003977
		public static string InstallDir
		{
			get
			{
				if (RegistryStrings.sInstallDir == null)
				{
					RegistryStrings.sInstallDir = (string)RegistryUtils.GetRegistryValue(Strings.RegistryBaseKeyPath, "InstallDir", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
				}
				return RegistryStrings.sInstallDir;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x000057A4 File Offset: 0x000039A4
		public static string UserDefinedDir
		{
			get
			{
				if (RegistryStrings.sUserDefinedDir == null)
				{
					RegistryStrings.sUserDefinedDir = (string)RegistryUtils.GetRegistryValue(Strings.RegistryBaseKeyPath, "UserDefinedDir", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
				}
				return RegistryStrings.sUserDefinedDir;
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000057D1 File Offset: 0x000039D1
		public static string GetBstAndroidDir(string vmName)
		{
			return Path.Combine(RegistryStrings.DataDir, vmName);
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x000057DE File Offset: 0x000039DE
		public static string ProductIconCompletePath
		{
			get
			{
				if (RegistryStrings.sProductIconCompletePath == null)
				{
					RegistryStrings.sProductIconCompletePath = Path.Combine(RegistryStrings.InstallDir, "ProductLogo.ico");
				}
				return RegistryStrings.sProductIconCompletePath;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00005800 File Offset: 0x00003A00
		public static string ProductImageCompletePath
		{
			get
			{
				if (RegistryStrings.sProductImageCompletePath == null)
				{
					RegistryStrings.sProductImageCompletePath = Path.Combine(RegistryStrings.InstallDir, "ProductLogo.png");
				}
				return RegistryStrings.sProductImageCompletePath;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00005822 File Offset: 0x00003A22
		public static string BstUserDataDir
		{
			get
			{
				if (RegistryStrings.sBstUserDataDir == null)
				{
					RegistryStrings.sBstUserDataDir = Path.Combine(RegistryStrings.DataDir, "UserData");
				}
				return RegistryStrings.sBstUserDataDir;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x00005844 File Offset: 0x00003A44
		public static string BstLogsDir
		{
			get
			{
				if (RegistryStrings.sBstLogsDir == null)
				{
					RegistryStrings.sBstLogsDir = Path.Combine(RegistryManager.Instance.UserDefinedDir, "Logs");
				}
				return RegistryStrings.sBstLogsDir;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0000586B File Offset: 0x00003A6B
		public static string GadgetDir
		{
			get
			{
				if (RegistryStrings.sGadgetDir == null)
				{
					RegistryStrings.sGadgetDir = Path.Combine(RegistryStrings.BstUserDataDir, "Gadget");
				}
				return RegistryStrings.sGadgetDir;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0000588D File Offset: 0x00003A8D
		public static string InputMapperFolder
		{
			get
			{
				if (RegistryStrings.sInputMapperFolder == null)
				{
					RegistryStrings.sInputMapperFolder = Path.Combine(RegistryStrings.BstUserDataDir, "InputMapper");
				}
				return RegistryStrings.sInputMapperFolder;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x000058AF File Offset: 0x00003AAF
		public static string SharedFolderDir
		{
			get
			{
				if (RegistryStrings.sSharedFolderDir == null)
				{
					RegistryStrings.sSharedFolderDir = Path.Combine(RegistryStrings.BstUserDataDir, "SharedFolder");
				}
				return RegistryStrings.sSharedFolderDir;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x000058D1 File Offset: 0x00003AD1
		public static string LibraryDir
		{
			get
			{
				if (RegistryStrings.sLibraryDir == null)
				{
					RegistryStrings.sLibraryDir = Path.Combine(RegistryStrings.BstUserDataDir, "Library");
				}
				return RegistryStrings.sLibraryDir;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x000058F3 File Offset: 0x00003AF3
		public static string PromotionDirectory
		{
			get
			{
				if (RegistryStrings.sPromotionDirectory == null)
				{
					RegistryStrings.sPromotionDirectory = Path.Combine(RegistryManager.Instance.ClientInstallDir, "Promo");
				}
				return RegistryStrings.sPromotionDirectory;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x0000591A File Offset: 0x00003B1A
		public static string MacroRecordingsFolderPath
		{
			get
			{
				if (RegistryStrings.sOperationsScriptFolder == null)
				{
					RegistryStrings.sOperationsScriptFolder = Path.Combine(RegistryStrings.InputMapperFolder, "UserScripts");
				}
				return RegistryStrings.sOperationsScriptFolder;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x0000593C File Offset: 0x00003B3C
		public static string BstManagerDir
		{
			get
			{
				if (RegistryStrings.sBstManagerDir == null)
				{
					RegistryStrings.sBstManagerDir = Path.Combine(RegistryStrings.DataDir, "Manager");
				}
				return RegistryStrings.sBstManagerDir;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0000595E File Offset: 0x00003B5E
		public static string RegistryBaseKeyPath
		{
			get
			{
				if (RegistryStrings.sRegistryBaseKeyPath == null)
				{
					RegistryStrings.sRegistryBaseKeyPath = Strings.RegistryBaseKeyPath + RegistryManager.UPGRADE_TAG;
				}
				return RegistryStrings.sRegistryBaseKeyPath;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000624 RID: 1572 RVA: 0x00005980 File Offset: 0x00003B80
		public static string UserAgent
		{
			get
			{
				if (RegistryStrings.sUserAgent == null)
				{
					RegistryStrings.sUserAgent = Utils.GetUserAgent("bgp");
				}
				return RegistryStrings.sUserAgent;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0000599D File Offset: 0x00003B9D
		public static string CursorPath
		{
			get
			{
				return Path.Combine(CustomPictureBox.AssetsDir, "Mouse_cursor.cur");
			}
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001CA0C File Offset: 0x0001AC0C
		static RegistryStrings()
		{
			try
			{
				RegistryStrings.BtvDir = Path.Combine(RegistryManager.Instance.UserDefinedDir, "BTV");
				RegistryStrings.ObsDir = Path.Combine(RegistryStrings.BtvDir, "OBS");
				RegistryStrings.ObsBinaryPath = Path.Combine(RegistryStrings.ObsDir, "HD-OBS.exe");
				RegistryStrings.ScreenshotDefaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), Strings.ProductTopBarDisplayName);
			}
			catch (Exception ex)
			{
				Logger.Error("Error in RegistryStrings " + ex.Message);
			}
		}

		// Token: 0x0400031F RID: 799
		private static string sDataDir;

		// Token: 0x04000320 RID: 800
		private static string sInstallDir;

		// Token: 0x04000321 RID: 801
		private static string sUserDefinedDir;

		// Token: 0x04000322 RID: 802
		private static string sProductIconCompletePath;

		// Token: 0x04000323 RID: 803
		private static string sProductImageCompletePath;

		// Token: 0x04000324 RID: 804
		private static string sBstUserDataDir;

		// Token: 0x04000325 RID: 805
		private static string sBstLogsDir;

		// Token: 0x04000326 RID: 806
		private static string sGadgetDir;

		// Token: 0x04000327 RID: 807
		private static string sInputMapperFolder;

		// Token: 0x04000328 RID: 808
		private static string sSharedFolderDir;

		// Token: 0x04000329 RID: 809
		private static string sLibraryDir;

		// Token: 0x0400032A RID: 810
		private static string sPromotionDirectory;

		// Token: 0x0400032B RID: 811
		private static string sOperationsScriptFolder;

		// Token: 0x0400032C RID: 812
		private static string sBstManagerDir;

		// Token: 0x0400032D RID: 813
		private static string sRegistryBaseKeyPath;

		// Token: 0x0400032E RID: 814
		private static string sUserAgent;

		// Token: 0x0400032F RID: 815
		public static string BtvDir;

		// Token: 0x04000330 RID: 816
		public static string ObsDir;

		// Token: 0x04000331 RID: 817
		public static string ObsBinaryPath;

		// Token: 0x04000332 RID: 818
		public static readonly string ScreenshotDefaultPath;
	}
}
