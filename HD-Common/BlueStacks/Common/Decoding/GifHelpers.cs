using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace BlueStacks.Common.Decoding
{
	// Token: 0x02000254 RID: 596
	internal static class GifHelpers
	{
		// Token: 0x0600124C RID: 4684 RVA: 0x00044318 File Offset: 0x00042518
		public static string ReadString(Stream stream, int length)
		{
			byte[] array = new byte[length];
			stream.ReadAll(array, 0, length);
			return Encoding.ASCII.GetString(array);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00044340 File Offset: 0x00042540
		public static byte[] ReadDataBlocks(Stream stream, bool discard)
		{
			MemoryStream memoryStream = discard ? null : new MemoryStream();
			byte[] result;
			using (memoryStream)
			{
				int num;
				while ((num = stream.ReadByte()) > 0)
				{
					byte[] buffer = new byte[num];
					stream.ReadAll(buffer, 0, num);
					if (memoryStream != null)
					{
						memoryStream.Write(buffer, 0, num);
					}
				}
				if (memoryStream != null)
				{
					result = memoryStream.ToArray();
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x000443B4 File Offset: 0x000425B4
		public static GifColor[] ReadColorTable(Stream stream, int size)
		{
			int num = 3 * size;
			byte[] array = new byte[num];
			stream.ReadAll(array, 0, num);
			GifColor[] array2 = new GifColor[size];
			for (int i = 0; i < size; i++)
			{
				byte r = array[3 * i];
				byte g = array[3 * i + 1];
				byte b = array[3 * i + 2];
				array2[i] = new GifColor(r, g, b);
			}
			return array2;
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0000F0E3 File Offset: 0x0000D2E3
		public static bool IsNetscapeExtension(GifApplicationExtension ext)
		{
			return ext.ApplicationIdentifier == "NETSCAPE" && Encoding.ASCII.GetString(ext.AuthenticationCode) == "2.0";
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0000F113 File Offset: 0x0000D313
		public static ushort GetRepeatCount(GifApplicationExtension ext)
		{
			if (ext.Data.Length >= 3)
			{
				return BitConverter.ToUInt16(ext.Data, 1);
			}
			return 1;
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0000F12E File Offset: 0x0000D32E
		public static Exception UnexpectedEndOfStreamException()
		{
			return new GifDecoderException("Unexpected end of stream before trailer was encountered");
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0000F13A File Offset: 0x0000D33A
		public static Exception UnknownBlockTypeException(int blockId)
		{
			return new GifDecoderException("Unknown block type: 0x" + blockId.ToString("x2", CultureInfo.InvariantCulture));
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0000F15C File Offset: 0x0000D35C
		public static Exception UnknownExtensionTypeException(int extensionLabel)
		{
			return new GifDecoderException("Unknown extension type: 0x" + extensionLabel.ToString("x2", CultureInfo.InvariantCulture));
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0000F17E File Offset: 0x0000D37E
		public static Exception InvalidBlockSizeException(string blockName, int expectedBlockSize, int actualBlockSize)
		{
			return new GifDecoderException(string.Format(CultureInfo.InvariantCulture, "Invalid block size for {0}. Expected {1}, but was {2}", new object[]
			{
				blockName,
				expectedBlockSize,
				actualBlockSize
			}));
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x0000F1B0 File Offset: 0x0000D3B0
		public static Exception InvalidSignatureException(string signature)
		{
			return new GifDecoderException("Invalid file signature: " + signature);
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x0000F1C2 File Offset: 0x0000D3C2
		public static Exception UnsupportedVersionException(string version)
		{
			return new GifDecoderException("Unsupported version: " + version);
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x00044414 File Offset: 0x00042614
		public static void ReadAll(this Stream stream, byte[] buffer, int offset, int count)
		{
			for (int i = 0; i < count; i += stream.Read(buffer, offset + i, count - i))
			{
			}
		}
	}
}
