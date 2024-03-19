using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace BlueStacks.Common
{
	// Token: 0x02000243 RID: 579
	public static class ShortcutHelper
	{
		// Token: 0x060011D1 RID: 4561
		[DllImport("shell32.dll")]
		private static extern bool SHGetSpecialFolderPath(IntPtr hwndOwner, [Out] StringBuilder lpszPath, int nFolder, bool fCreate);

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x00043B80 File Offset: 0x00041D80
		public static string CommonStartMenuPath
		{
			get
			{
				if (string.IsNullOrEmpty(ShortcutHelper.sCommonStartMenuPath))
				{
					StringBuilder stringBuilder = new StringBuilder(260);
					ShortcutHelper.SHGetSpecialFolderPath(IntPtr.Zero, stringBuilder, 22, false);
					ShortcutHelper.sCommonStartMenuPath = Path.Combine(stringBuilder.ToString(), "Programs");
				}
				return ShortcutHelper.sCommonStartMenuPath;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x00043BD0 File Offset: 0x00041DD0
		public static string CommonDesktopPath
		{
			get
			{
				if (string.IsNullOrEmpty(ShortcutHelper.sCommonDesktopPath))
				{
					StringBuilder stringBuilder = new StringBuilder(260);
					ShortcutHelper.SHGetSpecialFolderPath(IntPtr.Zero, stringBuilder, 25, false);
					ShortcutHelper.sCommonDesktopPath = stringBuilder.ToString();
				}
				return ShortcutHelper.sCommonDesktopPath;
			}
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00043C14 File Offset: 0x00041E14
		private static void DeleteFileIfExists(string filePath)
		{
			string text = ShortcutHelper.FixFileName(filePath, "");
			if (File.Exists(text))
			{
				Logger.Info("Deleting: " + text);
				File.Delete(text);
			}
			text = ShortcutHelper.FixFileName(filePath, "ncsoft");
			if (File.Exists(text))
			{
				Logger.Info("Deleting: " + text);
				File.Delete(text);
			}
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00043C78 File Offset: 0x00041E78
		private static string FixFileName(string fileName, string package = "")
		{
			string text = package.Contains("ncsoft") ? " - BlueStacks.lnk" : ".lnk";
			string result = fileName;
			bool flag = false;
			bool? flag2 = (fileName != null) ? new bool?(fileName.EndsWith(text, StringComparison.InvariantCultureIgnoreCase)) : null;
			if (flag == flag2.GetValueOrDefault() & flag2 != null)
			{
				result = fileName + text;
			}
			return result;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0000EE0A File Offset: 0x0000D00A
		public static void DeleteDesktopShortcut(string shortcutName)
		{
			ShortcutHelper.DeleteFileIfExists(Path.Combine(ShortcutHelper.sDesktopPath, shortcutName));
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0000EE1C File Offset: 0x0000D01C
		public static void DeleteStartMenuShortcut(string shortcutName)
		{
			ShortcutHelper.DeleteFileIfExists(Path.Combine(ShortcutHelper.CommonStartMenuPath, shortcutName));
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x0000EE2E File Offset: 0x0000D02E
		public static void DeleteCommonDesktopShortcut(string shortcutName)
		{
			ShortcutHelper.DeleteFileIfExists(Path.Combine(ShortcutHelper.CommonDesktopPath, shortcutName));
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0000EE1C File Offset: 0x0000D01C
		public static void DeleteCommonStartMenuShortcut(string shortcutName)
		{
			ShortcutHelper.DeleteFileIfExists(Path.Combine(ShortcutHelper.CommonStartMenuPath, shortcutName));
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0000EE40 File Offset: 0x0000D040
		public static void CreateCommonDesktopShortcut(string shortcutName, string shortcutIconPath, string targetApplication, string paramsString, string description, string package = "")
		{
			ShortcutHelper.CreateShortcut(Path.Combine(ShortcutHelper.CommonDesktopPath, shortcutName), shortcutIconPath, targetApplication, paramsString, description, package);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0000EE59 File Offset: 0x0000D059
		public static void CreateCommonStartMenuShortcut(string shortcutName, string shortcutIconPath, string targetApplication, string paramsString, string description, string package = "")
		{
			ShortcutHelper.CreateShortcut(Path.Combine(ShortcutHelper.CommonStartMenuPath, shortcutName), shortcutIconPath, targetApplication, paramsString, description, package);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0000EE72 File Offset: 0x0000D072
		public static void CreateDesktopShortcut(string shortcutName, string shortcutIconPath, string targetApplication, string paramsString, string description, string package = "")
		{
			ShortcutHelper.CreateShortcut(Path.Combine(ShortcutHelper.sDesktopPath, shortcutName), shortcutIconPath, targetApplication, paramsString, description, package);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x0000EE8B File Offset: 0x0000D08B
		public static void CreateStartMenuShortcut(string shortcutName, string shortcutIconPath, string targetApplication, string paramsString, string description)
		{
			ShortcutHelper.CreateShortcut(Path.Combine(ShortcutHelper.CommonStartMenuPath, shortcutName), shortcutIconPath, targetApplication, paramsString, description, "");
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x00043CDC File Offset: 0x00041EDC
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
				((IPersistFile)shellLink).Save(shortcutPath, false);
			}
			catch (Exception ex)
			{
				Logger.Warning("Could not create shortcut for " + shortcutPath + " . " + ex.ToString());
			}
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00043D6C File Offset: 0x00041F6C
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

		// Token: 0x060011E0 RID: 4576 RVA: 0x00043DF0 File Offset: 0x00041FF0
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

		// Token: 0x060011E1 RID: 4577 RVA: 0x00043E6C File Offset: 0x0004206C
		public static void UpdateTargetPathAndIcon(string shortcutPath, string target, string iconPath)
		{
			try
			{
				ShortcutHelper.IShellLink shellLink = (ShortcutHelper.IShellLink)new ShortcutHelper.ShellLink();
				((IPersistFile)shellLink).Load(shortcutPath, 0);
				shellLink.SetPath(target);
				shellLink.SetIconLocation(iconPath, 0);
				((IPersistFile)shellLink).Save(shortcutPath, false);
			}
			catch (Exception ex)
			{
				Logger.Warning("Could not get Shortcut target path. " + ex.ToString());
			}
		}

		// Token: 0x04000E29 RID: 3625
		private const int CSIDL_COMMON_DESKTOPDIRECTORY = 25;

		// Token: 0x04000E2A RID: 3626
		private const int CSIDL_COMMON_STARTMENU = 22;

		// Token: 0x04000E2B RID: 3627
		public static readonly string sDesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

		// Token: 0x04000E2C RID: 3628
		private static string sCommonStartMenuPath = null;

		// Token: 0x04000E2D RID: 3629
		private static string sCommonDesktopPath = null;

		// Token: 0x02000244 RID: 580
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		internal struct WIN32_FIND_DATAW
		{
			// Token: 0x04000E2E RID: 3630
			public uint dwFileAttributes;

			// Token: 0x04000E2F RID: 3631
			public long ftCreationTime;

			// Token: 0x04000E30 RID: 3632
			public long ftLastAccessTime;

			// Token: 0x04000E31 RID: 3633
			public long ftLastWriteTime;

			// Token: 0x04000E32 RID: 3634
			public uint nFileSizeHigh;

			// Token: 0x04000E33 RID: 3635
			public uint nFileSizeLow;

			// Token: 0x04000E34 RID: 3636
			public uint dwReserved0;

			// Token: 0x04000E35 RID: 3637
			public uint dwReserved1;

			// Token: 0x04000E36 RID: 3638
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string cFileName;

			// Token: 0x04000E37 RID: 3639
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
			public string cAlternateFileName;
		}

		// Token: 0x02000245 RID: 581
		[Guid("00021401-0000-0000-C000-000000000046")]
		[ComImport]
		internal class ShellLink
		{
			// Token: 0x060011E3 RID: 4579
			[MethodImpl(MethodImplOptions.InternalCall)]
			public extern ShellLink();
		}

		// Token: 0x02000246 RID: 582
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("000214F9-0000-0000-C000-000000000046")]
		[ComImport]
		internal interface IShellLink
		{
			// Token: 0x060011E4 RID: 4580
			void GetPath([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszFile, int cchMaxPath, out ShortcutHelper.WIN32_FIND_DATAW pfd, int fFlags);

			// Token: 0x060011E5 RID: 4581
			void GetIDList(out IntPtr ppidl);

			// Token: 0x060011E6 RID: 4582
			void SetIDList(IntPtr pidl);

			// Token: 0x060011E7 RID: 4583
			void GetDescription([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszName, int cchMaxName);

			// Token: 0x060011E8 RID: 4584
			void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);

			// Token: 0x060011E9 RID: 4585
			void GetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszDir, int cchMaxPath);

			// Token: 0x060011EA RID: 4586
			void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);

			// Token: 0x060011EB RID: 4587
			void GetArguments([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszArgs, int cchMaxPath);

			// Token: 0x060011EC RID: 4588
			void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

			// Token: 0x060011ED RID: 4589
			void GetHotkey(out short pwHotkey);

			// Token: 0x060011EE RID: 4590
			void SetHotkey(short wHotkey);

			// Token: 0x060011EF RID: 4591
			void GetShowCmd(out int piShowCmd);

			// Token: 0x060011F0 RID: 4592
			void SetShowCmd(int iShowCmd);

			// Token: 0x060011F1 RID: 4593
			void GetIconLocation([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszIconPath, int cchIconPath, out int piIcon);

			// Token: 0x060011F2 RID: 4594
			void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);

			// Token: 0x060011F3 RID: 4595
			void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);

			// Token: 0x060011F4 RID: 4596
			void Resolve(IntPtr hwnd, int fFlags);

			// Token: 0x060011F5 RID: 4597
			void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
		}
	}
}
