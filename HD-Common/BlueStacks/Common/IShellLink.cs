using System;
using System.Runtime.InteropServices;
using System.Text;

namespace BlueStacks.Common
{
	// Token: 0x0200013F RID: 319
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("000214F9-0000-0000-C000-000000000046")]
	[ComImport]
	public interface IShellLink
	{
		// Token: 0x06000C40 RID: 3136
		void GetPath([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszFile, int cchMaxPath, out WIN32_FIND_DATAW pfd, int fFlags);

		// Token: 0x06000C41 RID: 3137
		void GetIDList(out IntPtr ppidl);

		// Token: 0x06000C42 RID: 3138
		void SetIDList(IntPtr pidl);

		// Token: 0x06000C43 RID: 3139
		void GetDescription([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszName, int cchMaxName);

		// Token: 0x06000C44 RID: 3140
		void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);

		// Token: 0x06000C45 RID: 3141
		void GetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszDir, int cchMaxPath);

		// Token: 0x06000C46 RID: 3142
		void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);

		// Token: 0x06000C47 RID: 3143
		void GetArguments([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszArgs, int cchMaxPath);

		// Token: 0x06000C48 RID: 3144
		void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

		// Token: 0x06000C49 RID: 3145
		void GetHotkey(out short pwHotkey);

		// Token: 0x06000C4A RID: 3146
		void SetHotkey(short wHotkey);

		// Token: 0x06000C4B RID: 3147
		void GetShowCmd(out int piShowCmd);

		// Token: 0x06000C4C RID: 3148
		void SetShowCmd(int iShowCmd);

		// Token: 0x06000C4D RID: 3149
		void GetIconLocation([MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pszIconPath, int cchIconPath, out int piIcon);

		// Token: 0x06000C4E RID: 3150
		void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);

		// Token: 0x06000C4F RID: 3151
		void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);

		// Token: 0x06000C50 RID: 3152
		void Resolve(IntPtr hwnd, int fFlags);

		// Token: 0x06000C51 RID: 3153
		void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
	}
}
