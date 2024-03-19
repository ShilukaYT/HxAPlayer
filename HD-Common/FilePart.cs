using System;
using System.IO;
using BlueStacks.Common;

// Token: 0x02000013 RID: 19
public class FilePart
{
	// Token: 0x06000049 RID: 73 RVA: 0x00002320 File Offset: 0x00000520
	public FilePart(string name, long size, string sha1, string path)
	{
		this.Name = name;
		this.Size = size;
		this.SHA1 = sha1;
		this.Path = path;
		this.DownloadedSize = 0L;
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x0600004A RID: 74 RVA: 0x0000234D File Offset: 0x0000054D
	public string Name { get; }

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x0600004B RID: 75 RVA: 0x00002355 File Offset: 0x00000555
	public long Size { get; }

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x0600004C RID: 76 RVA: 0x0000235D File Offset: 0x0000055D
	public string SHA1 { get; }

	// Token: 0x0600004D RID: 77 RVA: 0x00002365 File Offset: 0x00000565
	public string URL(string manifestURL)
	{
		return ((manifestURL != null) ? manifestURL.Substring(0, manifestURL.LastIndexOf('/') + 1) : null) + this.Name;
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x0600004E RID: 78 RVA: 0x00002389 File Offset: 0x00000589
	public string Path { get; }

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x0600004F RID: 79 RVA: 0x00002391 File Offset: 0x00000591
	// (set) Token: 0x06000050 RID: 80 RVA: 0x00002399 File Offset: 0x00000599
	public long DownloadedSize { get; set; }

	// Token: 0x06000051 RID: 81 RVA: 0x000102D4 File Offset: 0x0000E4D4
	public bool Check()
	{
		Logger.Info("Will check " + this.Path);
		bool result = false;
		if (!File.Exists(this.Path))
		{
			Logger.Error("File missing");
			return false;
		}
		using (Stream stream = File.OpenRead(this.Path))
		{
			if (stream.Length != this.Size)
			{
				Logger.Error("File size incorrect: " + stream.Length.ToString());
				return false;
			}
			if (SplitFile.CheckSum(stream) == this.SHA1)
			{
				this.DownloadedSize = this.Size;
				Logger.Info("File size correct");
				result = true;
			}
		}
		return result;
	}
}
