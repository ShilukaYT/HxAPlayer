using System;
using System.Collections.Generic;
using System.IO;

namespace BlueStacks.Common
{
	// Token: 0x02000057 RID: 87
	public static class IOUtils
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00004D1C File Offset: 0x00002F1C
		public static void DeleteIfExists(IEnumerable<string> filesToDelete)
		{
			bool flag = filesToDelete != null;
			if (flag)
			{
				foreach (string text in filesToDelete)
				{
					try
					{
						bool flag2 = File.Exists(text);
						if (flag2)
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

		// Token: 0x060000C4 RID: 196 RVA: 0x00004DB0 File Offset: 0x00002FB0
		public static long GetAvailableDiskSpaceOfDrive(string path)
		{
			DriveInfo driveInfo = new DriveInfo(path);
			return driveInfo.AvailableFreeSpace;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004DD0 File Offset: 0x00002FD0
		public static string GetPartitionNameFromPath(string path)
		{
			DriveInfo driveInfo = new DriveInfo(path);
			return driveInfo.Name;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004DF0 File Offset: 0x00002FF0
		public static long GetDirectorySize(string dirPath)
		{
			bool flag = !Directory.Exists(dirPath);
			long result;
			if (flag)
			{
				result = 0L;
			}
			else
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
				long num = 0L;
				FileInfo[] files = directoryInfo.GetFiles();
				foreach (FileInfo fileInfo in files)
				{
					num += fileInfo.Length;
				}
				DirectoryInfo[] directories = directoryInfo.GetDirectories();
				foreach (DirectoryInfo directoryInfo2 in directories)
				{
					num += IOUtils.GetDirectorySize(directoryInfo2.FullName);
				}
				result = num;
			}
			return result;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004E8C File Offset: 0x0000308C
		public static bool IfPathExists(string path)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			bool exists = directoryInfo.Exists;
			bool result;
			if (exists)
			{
				result = true;
			}
			else
			{
				FileInfo fileInfo = new FileInfo(path);
				bool exists2 = fileInfo.Exists;
				result = exists2;
			}
			return result;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004ED0 File Offset: 0x000030D0
		public static string GetFileOrFolderName(string path)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(path);
			bool exists = directoryInfo.Exists;
			string name;
			if (exists)
			{
				name = directoryInfo.Name;
			}
			else
			{
				FileInfo fileInfo = new FileInfo(path);
				bool exists2 = fileInfo.Exists;
				if (!exists2)
				{
					throw new IOException("File or folder name does not exist");
				}
				name = fileInfo.Name;
			}
			return name;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004F24 File Offset: 0x00003124
		public static bool IsDirectoryEmpty(string dir)
		{
			bool flag = true;
			bool flag2 = !Directory.Exists(dir);
			bool result;
			if (flag2)
			{
				Logger.Info(dir + " does not exist");
				result = flag;
			}
			else
			{
				string[] files = Directory.GetFiles(dir);
				bool flag3 = files.Length == 0;
				if (flag3)
				{
					Logger.Info(dir + " is empty");
				}
				else
				{
					flag = false;
				}
				string[] directories = Directory.GetDirectories(dir);
				foreach (string text in directories)
				{
					files = Directory.GetFiles(text);
					bool flag4 = !IOUtils.IsDirectoryEmpty(text);
					if (flag4)
					{
						flag = false;
					}
				}
				result = flag;
			}
			return result;
		}

		// Token: 0x040001B4 RID: 436
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
