using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using BlueStacks.Common;

// Token: 0x02000014 RID: 20
public class Manifest
{
	// Token: 0x06000052 RID: 82 RVA: 0x000023A2 File Offset: 0x000005A2
	public Manifest(string filePath)
	{
		this.m_FileParts = new List<FilePart>();
		this.m_FilePath = filePath;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00010398 File Offset: 0x0000E598
	public bool Check()
	{
		int num = 0;
		using (List<FilePart>.Enumerator enumerator = this.m_FileParts.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.Check())
				{
					Logger.Error("Check failed for part " + num.ToString());
					return false;
				}
				num++;
			}
		}
		return true;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00010410 File Offset: 0x0000E610
	public void Build()
	{
		using (StreamReader streamReader = new StreamReader(File.OpenRead(this.m_FilePath)))
		{
			string text;
			while ((text = streamReader.ReadLine()) != null)
			{
				string[] array = text.Split(new char[]
				{
					' '
				});
				string text2 = array[0];
				long num = Convert.ToInt64(array[1], CultureInfo.InvariantCulture);
				string sha = array[2];
				string path = Path.Combine(Path.GetDirectoryName(this.m_FilePath), text2);
				FilePart filePart = new FilePart(text2, num, sha, path);
				if (filePart.Check())
				{
					filePart.DownloadedSize = filePart.Size;
				}
				this.m_FileParts.Add(filePart);
				this.FileSize += num;
			}
		}
	}

	// Token: 0x06000055 RID: 85 RVA: 0x000104D4 File Offset: 0x0000E6D4
	public void Dump()
	{
		foreach (FilePart filePart in this.m_FileParts)
		{
			Logger.Info("{0} {1} {2}", new object[]
			{
				filePart.Name,
				filePart.Size,
				filePart.SHA1
			});
		}
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000056 RID: 86 RVA: 0x000023BC File Offset: 0x000005BC
	public long Count
	{
		get
		{
			return (long)this.m_FileParts.Count;
		}
	}

	// Token: 0x1700000D RID: 13
	public FilePart this[int i]
	{
		get
		{
			return this.m_FileParts[i];
		}
	}

	// Token: 0x06000058 RID: 88
	[DllImport("kernel32", SetLastError = true)]
	private static extern bool FlushFileBuffers(IntPtr handle);

	// Token: 0x06000059 RID: 89 RVA: 0x00010550 File Offset: 0x0000E750
	public string MakeFile()
	{
		int num = 16384;
		byte[] buffer = new byte[num];
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.m_FilePath);
		string text = Path.Combine(Path.GetDirectoryName(this.m_FilePath), fileNameWithoutExtension);
		using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write, FileShare.None))
		{
			foreach (FilePart filePart in this.m_FileParts)
			{
				using (Stream stream = new FileStream(filePart.Path, FileMode.Open, FileAccess.Read))
				{
					int count;
					while ((count = stream.Read(buffer, 0, num)) > 0)
					{
						fileStream.Write(buffer, 0, count);
					}
				}
			}
			fileStream.Flush();
			if (!Manifest.FlushFileBuffers(fileStream.Handle))
			{
				throw new SystemException("Win32 FlushFileBuffers failed for " + text, new Win32Exception(Marshal.GetLastWin32Error()));
			}
		}
		return text;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00010668 File Offset: 0x0000E868
	public void DeleteFileParts()
	{
		foreach (FilePart filePart in this.m_FileParts)
		{
			File.Delete(filePart.Path);
		}
	}

	// Token: 0x0600005B RID: 91 RVA: 0x000023D8 File Offset: 0x000005D8
	public void DeleteManifest()
	{
		File.Delete(this.m_FilePath);
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x0600005C RID: 92 RVA: 0x000106C0 File Offset: 0x0000E8C0
	public long DownloadedSize
	{
		get
		{
			long num = 0L;
			foreach (FilePart filePart in this.m_FileParts)
			{
				num += filePart.DownloadedSize;
			}
			return num;
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x0600005D RID: 93 RVA: 0x000023E5 File Offset: 0x000005E5
	// (set) Token: 0x0600005E RID: 94 RVA: 0x000023ED File Offset: 0x000005ED
	public long FileSize { get; private set; }

	// Token: 0x0600005F RID: 95 RVA: 0x000023F6 File Offset: 0x000005F6
	public float PercentDownloaded()
	{
		return (float)Math.Round((double)this.DownloadedSize * 100.0 / (double)this.FileSize, 1);
	}

	// Token: 0x04000026 RID: 38
	private List<FilePart> m_FileParts;

	// Token: 0x04000027 RID: 39
	private string m_FilePath;
}
