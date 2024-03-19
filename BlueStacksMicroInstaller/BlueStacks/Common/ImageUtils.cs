using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace BlueStacks.Common
{
	// Token: 0x02000054 RID: 84
	public static class ImageUtils
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x0000478C File Offset: 0x0000298C
		public static BitmapImage BitmapFromPath(string path)
		{
			BitmapImage bitmapImage = null;
			bool flag = File.Exists(path);
			if (flag)
			{
				bitmapImage = new BitmapImage();
				try
				{
					using (FileStream fileStream = File.OpenRead(path))
					{
						bitmapImage.BeginInit();
						bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
						bitmapImage.StreamSource = fileStream;
						bitmapImage.EndInit();
					}
				}
				catch
				{
				}
			}
			return bitmapImage;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000480C File Offset: 0x00002A0C
		public static BitmapImage BitmapFromUri(string uri)
		{
			BitmapImage bitmapImage = null;
			try
			{
				bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.UriSource = new Uri(uri);
				bitmapImage.EndInit();
			}
			catch
			{
			}
			return bitmapImage;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004864 File Offset: 0x00002A64
		public static Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
		{
			Bitmap result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new BmpBitmapEncoder
				{
					Frames = 
					{
						BitmapFrame.Create(bitmapImage)
					}
				}.Save(memoryStream);
				Bitmap bitmap = new Bitmap(memoryStream);
				result = bitmap;
			}
			return result;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000048C0 File Offset: 0x00002AC0
		public static BitmapImage ByteArrayToImage(byte[] dataArray)
		{
			BitmapImage result;
			using (MemoryStream memoryStream = new MemoryStream(dataArray))
			{
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapImage.StreamSource = memoryStream;
				bitmapImage.EndInit();
				result = bitmapImage;
			}
			return result;
		}
	}
}
