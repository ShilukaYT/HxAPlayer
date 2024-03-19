using System;
using System.Collections.Generic;
using System.IO;

namespace BlueStacks.Common
{
	// Token: 0x020001E9 RID: 489
	public static class IOUtils
	{
		// Token: 0x06000FDB RID: 4059 RVA: 0x0003CC98 File Offset: 0x0003AE98
		public static void DeleteIfExists(IEnumerable<string> filesToDelete)
		{
			if (filesToDelete != null)
			{
				foreach (string text in filesToDelete)
				{
					try
					{
						if (File.Exists(text))
						{
							File.Delete(text);
						}
					}
					catch (Exception ex)
					{
						Logger.Error("Exception while deleting file " + text + ex.ToString());
					}
				}
			}
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0000DD6D File Offset: 0x0000BF6D
		public static long GetAvailableDiskSpaceOfDrive(string path)
		{
			return new DriveInfo(path).AvailableFreeSpace;
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0000DD7A File Offset: 0x0000BF7A
		public static string GetPartitionNameFromPath(string path)
		{
			return new DriveInfo(path).Name;
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x0003CD14 File Offset: 0x0003AF14
		public static long GetDirectorySize(string dirPath)
		{
			if (!Directory.Exists(dirPath))
			{
				return 0L;
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
			long num = 0L;
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				num += fileInfo.Length;
			}
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				num += IOUtils.GetDirectorySize(directoryInfo2.FullName);
			}
			return num;
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x0000DD87 File Offset: 0x0000BF87
		public static bool IfPathExists(string path)
		{
			return new DirectoryInfo(path).Exists || new FileInfo(path).Exists;
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x0003CD88 File Offset: 0x0003AF88
		public static string GetFileOrFolderName(string path)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			if (directoryInfo.Exists)
			{
				return directoryInfo.Name;
			}
			FileInfo fileInfo = new FileInfo(path);
			if (fileInfo.Exists)
			{
				return fileInfo.Name;
			}
			throw new IOException("File or folder name does not exist");
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0003CDCC File Offset: 0x0003AFCC
		public static bool IsDirectoryEmpty(string dir)
		{
			bool result = true;
			if (!Directory.Exists(dir))
			{
				Logger.Info(dir + " does not exist");
				return result;
			}
			if (Directory.GetFiles(dir).Length == 0)
			{
				Logger.Info(dir + " is empty");
			}
			else
			{
				result = false;
			}
			foreach (string text in Directory.GetDirectories(dir))
			{
				Directory.GetFiles(text);
				if (!IOUtils.IsDirectoryEmpty(text))
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x040008DF RID: 2271
		public static readonly char[] DisallowedCharsInDirs = new char[]
		{
			'&',
			'<',
			'>',
			'"',
			'\'',
			'^'
		};
	}
}
