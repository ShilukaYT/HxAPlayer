using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace BlueStacks.Common
{
	// Token: 0x020000E6 RID: 230
	public static class SSDCheck
	{
		// Token: 0x060005FB RID: 1531
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern SafeFileHandle CreateFileW([MarshalAs(UnmanagedType.LPWStr)] string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

		// Token: 0x060005FC RID: 1532
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool DeviceIoControl(SafeFileHandle hDevice, uint dwIoControlCode, ref SSDCheck.STORAGE_PROPERTY_QUERY lpInBuffer, uint nInBufferSize, ref SSDCheck.DEVICE_TRIM_DESCRIPTOR lpOutBuffer, uint nOutBufferSize, out uint lpBytesReturned, IntPtr lpOverlapped);

		// Token: 0x060005FD RID: 1533
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool DeviceIoControl(SafeFileHandle hDevice, uint dwIoControlCode, IntPtr lpInBuffer, uint nInBufferSize, ref SSDCheck.VOLUME_DISK_EXTENTS lpOutBuffer, uint nOutBufferSize, out uint lpBytesReturned, IntPtr lpOverlapped);

		// Token: 0x060005FE RID: 1534
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern uint FormatMessage(uint dwFlags, IntPtr lpSource, uint dwMessageId, uint dwLanguageId, StringBuilder lpBuffer, uint nSize, IntPtr Arguments);

		// Token: 0x060005FF RID: 1535 RVA: 0x000056D1 File Offset: 0x000038D1
		private static uint CTL_CODE(uint DeviceType, uint Function, uint Method, uint Access)
		{
			return DeviceType << 16 | Access << 14 | Function << 2 | Method;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001C2FC File Offset: 0x0001A4FC
		public static bool IsMediaTypeSSD(string path)
		{
			try
			{
				string pathRoot = Path.GetPathRoot(path);
				Logger.Info("Checking if media type ssd for drive: " + pathRoot);
				string text = pathRoot.TrimEnd(new char[]
				{
					'\\'
				}).TrimEnd(new char[]
				{
					':'
				});
				if (text.Length > 1)
				{
					Logger.Info("Invalid drive letter " + text + ". returning!!");
					return false;
				}
				return SSDCheck.HasTrimEnabled("\\\\.\\PhysicalDrive" + SSDCheck.GetDiskExtents(text.ToCharArray()[0]).ToString());
			}
			catch (Exception ex)
			{
				Logger.Error("Failed to find if media is ssd. Ex : " + ex.ToString());
			}
			return false;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001C3B8 File Offset: 0x0001A5B8
		private static bool HasTrimEnabled(string drive)
		{
			Logger.Info("Checking trim enabled for drive: " + drive);
			SafeFileHandle safeFileHandle = SSDCheck.CreateFileW(drive, 0U, 3U, IntPtr.Zero, 3U, 128U, IntPtr.Zero);
			if (safeFileHandle == null || safeFileHandle.IsInvalid)
			{
				string errorMessage = SSDCheck.GetErrorMessage(Marshal.GetLastWin32Error());
				Logger.Error("CreateFile failed with message: " + errorMessage);
				throw new Exception("check the error message above");
			}
			uint dwIoControlCode = SSDCheck.CTL_CODE(45U, 1280U, 0U, 0U);
			SSDCheck.STORAGE_PROPERTY_QUERY storage_PROPERTY_QUERY = new SSDCheck.STORAGE_PROPERTY_QUERY
			{
				PropertyId = 8U,
				QueryType = 0U
			};
			SSDCheck.DEVICE_TRIM_DESCRIPTOR device_TRIM_DESCRIPTOR = default(SSDCheck.DEVICE_TRIM_DESCRIPTOR);
			uint num;
			bool flag = SSDCheck.DeviceIoControl(safeFileHandle, dwIoControlCode, ref storage_PROPERTY_QUERY, (uint)Marshal.SizeOf(storage_PROPERTY_QUERY), ref device_TRIM_DESCRIPTOR, (uint)Marshal.SizeOf(device_TRIM_DESCRIPTOR), out num, IntPtr.Zero);
			if (safeFileHandle != null)
			{
				safeFileHandle.Close();
			}
			if (!flag)
			{
				string errorMessage2 = SSDCheck.GetErrorMessage(Marshal.GetLastWin32Error());
				Logger.Error("DeviceIoControl failed to query trim enabled. " + errorMessage2);
				throw new Exception("check the error message above");
			}
			bool trimEnabled = device_TRIM_DESCRIPTOR.TrimEnabled;
			Logger.Info(string.Format("Is Trim Enabled: {0}", trimEnabled));
			return trimEnabled;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001C4D0 File Offset: 0x0001A6D0
		private static uint GetDiskExtents(char cDrive)
		{
			if (new DriveInfo(cDrive.ToString(CultureInfo.InvariantCulture)).DriveType != DriveType.Fixed)
			{
				Logger.Info(string.Format("The drive {0} is not fixed drive.", cDrive));
			}
			string text = "\\\\.\\" + cDrive.ToString(CultureInfo.InvariantCulture) + ":";
			SafeFileHandle safeFileHandle = SSDCheck.CreateFileW(text, 0U, 3U, IntPtr.Zero, 3U, 128U, IntPtr.Zero);
			if (safeFileHandle == null || safeFileHandle.IsInvalid)
			{
				string errorMessage = SSDCheck.GetErrorMessage(Marshal.GetLastWin32Error());
				Logger.Error("CreateFile failed for " + text + ".  " + errorMessage);
				throw new Exception("check the error message above");
			}
			uint dwIoControlCode = SSDCheck.CTL_CODE(86U, 0U, 0U, 0U);
			SSDCheck.VOLUME_DISK_EXTENTS volume_DISK_EXTENTS = default(SSDCheck.VOLUME_DISK_EXTENTS);
			uint num;
			bool flag = SSDCheck.DeviceIoControl(safeFileHandle, dwIoControlCode, IntPtr.Zero, 0U, ref volume_DISK_EXTENTS, (uint)Marshal.SizeOf(volume_DISK_EXTENTS), out num, IntPtr.Zero);
			if (safeFileHandle != null)
			{
				safeFileHandle.Close();
			}
			if (!flag || volume_DISK_EXTENTS.Extents.Length != 1)
			{
				string errorMessage2 = SSDCheck.GetErrorMessage(Marshal.GetLastWin32Error());
				Logger.Error("DeviceIoControl failed to query disk extension. " + errorMessage2);
				throw new Exception("check the error message above");
			}
			uint diskNumber = volume_DISK_EXTENTS.Extents[0].DiskNumber;
			Logger.Info(string.Format("The physical drive number is: {0}", diskNumber));
			return diskNumber;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001C618 File Offset: 0x0001A818
		private static string GetErrorMessage(int code)
		{
			StringBuilder stringBuilder = new StringBuilder(255);
			SSDCheck.FormatMessage(4096U, IntPtr.Zero, (uint)code, 0U, stringBuilder, (uint)stringBuilder.Capacity, IntPtr.Zero);
			return stringBuilder.ToString();
		}

		// Token: 0x040002E5 RID: 741
		private const uint FILE_SHARE_READ = 1U;

		// Token: 0x040002E6 RID: 742
		private const uint FILE_SHARE_WRITE = 2U;

		// Token: 0x040002E7 RID: 743
		private const uint OPEN_EXISTING = 3U;

		// Token: 0x040002E8 RID: 744
		private const uint FILE_ATTRIBUTE_NORMAL = 128U;

		// Token: 0x040002E9 RID: 745
		private const uint FILE_DEVICE_MASS_STORAGE = 45U;

		// Token: 0x040002EA RID: 746
		private const uint IOCTL_STORAGE_BASE = 45U;

		// Token: 0x040002EB RID: 747
		private const uint METHOD_BUFFERED = 0U;

		// Token: 0x040002EC RID: 748
		private const uint FILE_ANY_ACCESS = 0U;

		// Token: 0x040002ED RID: 749
		private const uint IOCTL_VOLUME_BASE = 86U;

		// Token: 0x040002EE RID: 750
		private const uint StorageDeviceTrimEnabledProperty = 8U;

		// Token: 0x040002EF RID: 751
		private const uint PropertyStandardQuery = 0U;

		// Token: 0x040002F0 RID: 752
		private const uint FORMAT_MESSAGE_FROM_SYSTEM = 4096U;

		// Token: 0x020000E7 RID: 231
		private struct STORAGE_PROPERTY_QUERY
		{
			// Token: 0x040002F1 RID: 753
			public uint PropertyId;

			// Token: 0x040002F2 RID: 754
			public uint QueryType;

			// Token: 0x040002F3 RID: 755
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
			public byte[] AdditionalParameters;
		}

		// Token: 0x020000E8 RID: 232
		private struct DEVICE_TRIM_DESCRIPTOR
		{
			// Token: 0x040002F4 RID: 756
			public uint Version;

			// Token: 0x040002F5 RID: 757
			public uint Size;

			// Token: 0x040002F6 RID: 758
			[MarshalAs(UnmanagedType.U1)]
			public bool TrimEnabled;
		}

		// Token: 0x020000E9 RID: 233
		private struct DISK_EXTENT
		{
			// Token: 0x040002F7 RID: 759
			public uint DiskNumber;

			// Token: 0x040002F8 RID: 760
			public long StartingOffset;

			// Token: 0x040002F9 RID: 761
			public long ExtentLength;
		}

		// Token: 0x020000EA RID: 234
		private struct VOLUME_DISK_EXTENTS
		{
			// Token: 0x040002FA RID: 762
			public uint NumberOfDiskExtents;

			// Token: 0x040002FB RID: 763
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
			public SSDCheck.DISK_EXTENT[] Extents;
		}
	}
}
