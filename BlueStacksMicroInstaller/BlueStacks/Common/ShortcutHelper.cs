using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace BlueStacks.Common
{
	// Token: 0x0200008E RID: 142
	public static class ShortcutHelper
	{
		// Token: 0x06000284 RID: 644
		[DllImport("shell32.dll")]
		private static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner, [Out] StringBuilder lpszPath, int nFolder, bool fCreate);

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000E048 File Offset: 0x0000C248
		public static string CommonStartMenuPath
		{
			get
			{
				bool flag = string.IsNullOrEmpty(ShortcutHelper.sCommonStartMenuPath);
				if (flag)
				{
					StringBuilder stringBuilder = new StringBuilder(260);
					ShortcutHelper.SHGetSpecialFolderPath(IntPtr.Zero, stringBuilder, 22, false);
					ShortcutHelper.sCommonStartMenuPath = Path.Combine(stringBuilder.ToString(), "Programs");
				}
				return ShortcutHelper.sCommonStartMenuPath;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000E0A0 File Offset: 0x0000C2A0
		public static string CommonDesktopPath
		{
			get
			{
				bool flag = string.IsNullOrEmpty(ShortcutHelper.sCommonDesktopPath);
				if (flag)
				{
					StringBuilder stringBuilder = new StringBuilder(260);
					ShortcutHelper.SHGetSpecialFolderPath(IntPtr.Zero, stringBuilder, 25, false);
					ShortcutHelper.sCommonDesktopPath = stringBuilder.ToString();
				}
				return ShortcutHelper.sCommonDesktopPath;
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000E0EC File Offset: 0x0000C2EC
		private static void DeleteFileIfExists(string filePath)
		{
			string text = ShortcutHelper.FixFileName(filePath, "");
			bool flag = File.Exists(text);
			if (flag)
			{
				Logger.Info("Deleting: " + text);
				File.Delete(text);
			}
			text = ShortcutHelper.FixFileName(filePath, "ncsoft");
			bool flag2 = File.Exists(text);
			if (flag2)
			{
				Logger.Info("Deleting: " + text);
				File.Delete(text);
			}
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000E15C File Offset: 0x0000C35C
		private static string FixFileName(string fileName, string package = "")
		{
			string text = package.Contains("ncsoft") ? " - BlueStacks.lnk" : ".lnk";
			string result = fileName;
			bool flag = false;
			bool? flag2 = (fileName != null) ? new bool?(fileName.EndsWith(text, StringComparison.InvariantCultureIgnoreCase)) : null;
			bool flag3 = flag == flag2.GetValueOrDefault() & flag2 != null;
			if (flag3)
			{
				result = fileName + text;
			}
			return result;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000E1CA File Offset: 0x0000C3CA
		public static void DeleteDesktopShortcut(string shortcutName)
		{
			ShortcutHelper.DeleteFileIfExists(Path.Combine(ShortcutHelper.sDesktopPath, shortcutName));
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000E1DE File Offset: 0x0000C3DE
		public static void DeleteStartMenuShortcut(string shortcutName)
		{
			ShortcutHelper.DeleteFileIfExists(Path.Combine(ShortcutHelper.CommonStartMenuPath, shortcutName));
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000E1F2 File Offset: 0x0000C3F2
		public static void DeleteCommonDesktopShortcut(string shortcutName)
		{
			ShortcutHelper.DeleteFileIfExists(Path.Combine(ShortcutHelper.CommonDesktopPath, shortcutName));
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000E1DE File Offset: 0x0000C3DE
		public static void DeleteCommonStartMenuShortcut(string shortcutName)
		{
			ShortcutHelper.DeleteFileIfExists(Path.Combine(ShortcutHelper.CommonStartMenuPath, shortcutName));
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000E206 File Offset: 0x0000C406
		public static void CreateCommonDesktopShortcut(string shortcutName, string shortcutIconPath, string targetApplication, string paramsString, string description, string package = "")
		{
			ShortcutHelper.CreateShortcut(Path.Combine(ShortcutHelper.CommonDesktopPath, shortcutName), shortcutIconPath, targetApplication, paramsString, description, package);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000E221 File Offset: 0x0000C421
		public static void CreateCommonStartMenuShortcut(string shortcutName, string shortcutIconPath, string targetApplication, string paramsString, string description, string package = "")
		{
			ShortcutHelper.CreateShortcut(Path.Combine(ShortcutHelper.CommonStartMenuPath, shortcutName), shortcutIconPath, targetApplication, paramsString, description, package);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000E23C File Offset: 0x0000C43C
		public static void CreateDesktopShortcut(string shortcutName, string shortcutIconPath, string targetApplication, string paramsString, string description, string package = "")
		{
			ShortcutHelper.CreateShortcut(Path.Combine(ShortcutHelper.sDesktopPath, shortcutName), shortcutIconPath, targetApplication, paramsString, description, package);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000E257 File Offset: 0x0000C457
		public static void CreateStartMenuShortcut(string shortcutName, string shortcutIconPath, string targetApplication, string paramsString, string description)
		{
			ShortcutHelper.CreateShortcut(Path.Combine(ShortcutHelper.CommonStartMenuPath, shortcutName), shortcutIconPath, targetApplication, paramsString, description, "");
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000E278 File Offset: 0x0000C478
		public static void CreateShortcut(string shortcutPath, string shortcutIconPath, string targetApplication, string paramsString, string description, string package = "")
		{
			try
			{
				package = (package ?? string.Empty);
				shortcutPath = ShortcutHelper.FixFileName(shortcutPath, package);
				ShortcutHelper.DeleteFileIfExists(shortcutPath);
				ShortcutHelper.IShellLink shellLink = (ShortcutHelper.IShellLink)new ShortcutHelper.ShellLink();
				shellLink.SetDescription(description);
				shellLink.SetPath(targetApplication);
				shellLink.SetIconLocation(shortcutIconPath, 0);
				shellLink.SetArguments(paramsString);
				IPersistFile persistFile = (IPersistFile)shellLink;
				persistFile.Save(shortcutPath, false);
			}
			catch (Exception ex)
			{
				Logger.Warning("Could not create shortcut for " + shortcutPath + " . " + ex.ToString());
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000E318 File Offset: 0x0000C518
		public static string GetShortcutArguments(string shortcutPath)
		{
			try
			{
				ShortcutHelper.IShellLink shellLink = (ShortcutHelper.IShellLink)new ShortcutHelper.ShellLink();
				((IPersistFile)shellLink).Load(shortcutPath, 0);
				StringBuilder stringBuilder = new StringBuilder(260);
				ShortcutHelper.WIN32_FIND_DATAW win32_FIND_DATAW = default(ShortcutHelper.WIN32_FIND_DATAW);
				shellLink.GetPath(stringBuilder, stringBuilder.Capacity, out win32_FIND_DATAW, 0);
				return stringBuilder.ToString().Trim();
			}
			catch (Exception ex)
			{
				Logger.Warning("Could not get Shortcut target path. " + ex.ToString());
			}
			return string.Empty;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000E3A8 File Offset: 0x0000C5A8
		public static string GetShortcutIconLocation(string shortcutPath)
		{
			try
			{
				ShortcutHelper.IShellLink shellLink = (ShortcutHelper.IShellLink)new ShortcutHelper.ShellLink();
				((IPersistFile)shellLink).Load(shortcutPath, 0);
				StringBuilder stringBuilder = new StringBuilder(260);
				int num;
				shellLink.GetIconLocation(stringBuilder, stringBuilder.Capacity, out num);
				return stringBuilder.ToString().Trim();
			}
			catch (Exception ex)
			{
				Logger.Warning("Could not get Shortcut target path. " + ex.ToString());
			}
			return string.Empty;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000E430 File Offset: 0x0000C630
		public static void UpdateTargetPathAndIcon(string shortcutPath, string target, string iconPath)
		{
			try
			{
				ShortcutHelper.IShellLink shellLink = (ShortcutHelper.IShellLink)new ShortcutHelper.ShellLink();
				((IPersistFile)shellLink).Load(shortcutPath, 0);
				shellLink.SetPath(target);
				shellLink.SetIconLocation(iconPath, 0);
				IPersistFile persistFile = (IPersistFile)shellLink;
				persistFile.Save(shortcutPath, false);
			}
			catch (Exception ex)
			{
				Logger.Warning("Could not get Shortcut target path. " + ex.ToString());
			}
		}

		// Token: 0x040004D5 RID: 1237
		private const int CSIDL_COMMON_DESKTOPDIRECTORY = 25;

		// Token: 0x040004D6 RID: 1238
		private const int CSIDL_COMMON_STARTMENU = 22;

		// Token: 0x040004D7 RID: 1239
		public static readonly string sDesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

		// Token: 0x040004D8 RID: 1240
		private static string sCommonStartMenuPath = null;

		// Token: 0x040004D9 RID: 1241
		private static string sCommonDesktopPath = null;

		// Token: 0x020000DF RID: 223
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal struct WIN32_FIND_DATAW
		{
			// Token: 0x040007F5 RID: 2037
			public uint dwFileAttributes;

			// Token: 0x040007F6 RID: 2038
			public long ftCreationTime;

			// Token: 0x040007F7 RID: 2039
			public long ftLastAccessTime;

			// Token: 0x040007F8 RID: 2040
			public long ftLastWriteTime;

			// Token: 0x040007F9 RID: 2041
			public uint nFileSizeHigh;

			// Token: 0x040007FA RID: 2042
			public uint nFileSizeLow;

			// Token: 0x040007FB RID: 2043
			public uint dwReserved0;

			// Token: 0x040007FC RID: 2044
			public uint dwReserved1;

			// Token: 0x040007FD RID: 2045
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string cFileName;

			// Token: 0x040007FE RID: 2046
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
			public string cAlternateFileName;
		}

		// Token: 0x020000E0 RID: 224
		[Guid("00021401-0000-0000-C000-000000000046")]
		[ComImport]
		internal class ShellLink
		{
			// Token: 0x06000452 RID: 1106
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern ShellLink();
		}

		// Token: 0x020000E1 RID: 225
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("000214F9-0000-0000-C000-000000000046")]
		[ComImport]
		internal interface IShellLink
		{
			// Token: 0x06000453 RID: 1107
			void GetPath([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszFile, int cchMaxPath, out ShortcutHelper.WIN32_FIND_DATAW pfd, int fFlags);

			// Token: 0x06000454 RID: 1108
			void GetIDList(out IntPtr ppidl);

			// Token: 0x06000455 RID: 1109
			void SetIDList(IntPtr pidl);

			// Token: 0x06000456 RID: 1110
			void GetDescription([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszName, int cchMaxName);

			// Token: 0x06000457 RID: 1111
			void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);

			// Token: 0x06000458 RID: 1112
			void GetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszDir, int cchMaxPath);

			// Token: 0x06000459 RID: 1113
			void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);

			// Token: 0x0600045A RID: 1114
			void GetArguments([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszArgs, int cchMaxPath);

			// Token: 0x0600045B RID: 1115
			void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

			// Token: 0x0600045C RID: 1116
			void GetHotkey(out short pwHotkey);

			// Token: 0x0600045D RID: 1117
			void SetHotkey(short wHotkey);

			// Token: 0x0600045E RID: 1118
			void GetShowCmd(out int piShowCmd);

			// Token: 0x0600045F RID: 1119
			void SetShowCmd(int iShowCmd);

			// Token: 0x06000460 RID: 1120
			void GetIconLocation([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszIconPath, int cchIconPath, out int piIcon);

			// Token: 0x06000461 RID: 1121
			void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);

			// Token: 0x06000462 RID: 1122
			void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);

			// Token: 0x06000463 RID: 1123
			void Resolve(IntPtr hwnd, int fFlags);

			// Token: 0x06000464 RID: 1124
			void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
		}
	}
}
