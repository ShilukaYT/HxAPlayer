using System;
using System.Runtime.InteropServices;

namespace BlueStacks.Common
{
	// Token: 0x020000D1 RID: 209
	public static class MemoryMappedFile
	{
		// Token: 0x0600051D RID: 1309
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr OpenFileMapping(uint dwDesiredAccess, bool bInheritHandle, string lpName);

		// Token: 0x0600051E RID: 1310
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr MapViewOfFile(IntPtr hFileMappingObject, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, UIntPtr dwNumberOfBytesToMap);

		// Token: 0x0600051F RID: 1311
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool CloseHandle(IntPtr hObject);

		// Token: 0x06000520 RID: 1312 RVA: 0x00019D34 File Offset: 0x00017F34
		public static int GetNCSoftAgentPort(string SharedMemoryName, uint NumBytes)
		{
			IntPtr intPtr = MemoryMappedFile.OpenFileMapping(983071U, false, SharedMemoryName);
			if (IntPtr.Zero == intPtr)
			{
				Logger.Error("Shared Memory Handle not found. Last Error : " + Marshal.GetLastWin32Error().ToString());
				return -1;
			}
			IntPtr intPtr2 = MemoryMappedFile.MapViewOfFile(intPtr, 983071U, 0U, 0U, new UIntPtr(NumBytes));
			if (intPtr2 == IntPtr.Zero)
			{
				Logger.Error("Cannot map view of file. Last Error : " + Marshal.GetLastWin32Error().ToString());
				return -1;
			}
			int result = -1;
			try
			{
				result = Marshal.ReadInt32(intPtr2);
			}
			catch (Exception ex)
			{
				Logger.Error("Failed to read memory as int32");
				Logger.Error(ex.ToString());
			}
			if (IntPtr.Zero != intPtr)
			{
				MemoryMappedFile.CloseHandle(intPtr);
				intPtr = IntPtr.Zero;
			}
			intPtr2 = IntPtr.Zero;
			return result;
		}

		// Token: 0x0400024A RID: 586
		private const uint STANDARD_RIGHTS_REQUIRED = 983040U;

		// Token: 0x0400024B RID: 587
		private const uint SECTION_QUERY = 1U;

		// Token: 0x0400024C RID: 588
		private const uint SECTION_MAP_WRITE = 2U;

		// Token: 0x0400024D RID: 589
		private const uint SECTION_MAP_READ = 4U;

		// Token: 0x0400024E RID: 590
		private const uint SECTION_MAP_EXECUTE = 8U;

		// Token: 0x0400024F RID: 591
		private const uint SECTION_EXTEND_SIZE = 16U;

		// Token: 0x04000250 RID: 592
		private const uint SECTION_ALL_ACCESS = 983071U;

		// Token: 0x04000251 RID: 593
		private const uint FILE_MAP_ALL_ACCESS = 983071U;
	}
}
