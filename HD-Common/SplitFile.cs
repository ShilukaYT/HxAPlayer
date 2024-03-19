using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

// Token: 0x02000016 RID: 22
internal class SplitFile
{
	// Token: 0x06000064 RID: 100 RVA: 0x0001071C File Offset: 0x0000E91C
	public static void Split(string path, int size, SplitFile.ProgressCb progressCb)
	{
		byte[] buffer = new byte[16384];
		using (Stream stream = File.OpenRead(path))
		{
			int num = 0;
			string.Format(CultureInfo.InvariantCulture, "{0}.manifest", new object[]
			{
				path
			});
			while (stream.Position < stream.Length)
			{
				string path2 = string.Format(CultureInfo.InvariantCulture, "{0}_part_{1}", new object[]
				{
					path,
					num
				});
				using (Stream stream2 = File.Create(path2))
				{
					int num2;
					for (int i = size; i > 0; i -= num2)
					{
						num2 = stream.Read(buffer, 0, Math.Min(i, 16384));
						if (num2 == 0)
						{
							break;
						}
						stream2.Write(buffer, 0, num2);
					}
				}
				string manifest = null;
				using (Stream stream3 = File.OpenRead(path2))
				{
					string text = SplitFile.CheckSum(stream3);
					long length = stream3.Length;
					manifest = string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", new object[]
					{
						Path.GetFileName(path2),
						length,
						text
					});
				}
				progressCb(manifest);
				num++;
			}
		}
	}

	// Token: 0x06000065 RID: 101 RVA: 0x000108A0 File Offset: 0x0000EAA0
	public static string CheckSum(Stream stream)
	{
		string result;
		using (SHA1CryptoServiceProvider sha1CryptoServiceProvider = new SHA1CryptoServiceProvider())
		{
			byte[] array = sha1CryptoServiceProvider.ComputeHash(stream);
			StringBuilder stringBuilder = new StringBuilder(array.Length * 2);
			foreach (byte b in array)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			result = stringBuilder.ToString();
		}
		return result;
	}

	// Token: 0x02000017 RID: 23
	// (Invoke) Token: 0x06000068 RID: 104
	public delegate void ProgressCb(string manifest);
}
