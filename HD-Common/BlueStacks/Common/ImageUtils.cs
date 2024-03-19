using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace BlueStacks.Common
{
	// Token: 0x020001E6 RID: 486
	public static class ImageUtils
	{
		// Token: 0x06000FCD RID: 4045 RVA: 0x0003C89C File Offset: 0x0003AA9C
		public static BitmapImage BitmapFromPath(string path)
		{
			BitmapImage bitmapImage = null;
			if (File.Exists(path))
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

		// Token: 0x06000FCE RID: 4046 RVA: 0x0003C908 File Offset: 0x0003AB08
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

		// Token: 0x06000FCF RID: 4047 RVA: 0x0003C954 File Offset: 0x0003AB54
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
				result = new Bitmap(memoryStream);
			}
			return result;
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0003C9A8 File Offset: 0x0003ABA8
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
