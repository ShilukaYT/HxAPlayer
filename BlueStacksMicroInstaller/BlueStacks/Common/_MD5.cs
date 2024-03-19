using System;
using System.IO;
using System.Text;

namespace BlueStacks.Common
{
	// Token: 0x02000074 RID: 116
	public class _MD5
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000AA8D File Offset: 0x00008C8D
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000AA95 File Offset: 0x00008C95
		protected uint[] X { get; set; } = new uint[16];

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000AA9E File Offset: 0x00008C9E
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x0000AAA6 File Offset: 0x00008CA6
		protected byte[] m_byteInput { get; set; }

		// Token: 0x17000067 RID: 103
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x0000AAAF File Offset: 0x00008CAF
		public string Value
		{
			set
			{
				this.ValueAsByte = Encoding.ASCII.GetBytes(value);
			}
		}

		// Token: 0x17000068 RID: 104
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x0000AAC4 File Offset: 0x00008CC4
		public byte[] ValueAsByte
		{
			set
			{
				bool flag = value != null;
				if (flag)
				{
					this.m_byteInput = new byte[value.Length];
					for (int i = 0; i < value.Length; i++)
					{
						this.m_byteInput[i] = value[i];
					}
					this.CalculateMD5Value();
				}
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000AB11 File Offset: 0x00008D11
		public string FingerPrint
		{
			get
			{
				return this.dg.GetString();
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0000AB20 File Offset: 0x00008D20
		public byte[] FingerPrintBytes
		{
			get
			{
				byte[] array = new byte[16];
				BitConverter.GetBytes(this.dg.A).CopyTo(array, 0);
				BitConverter.GetBytes(this.dg.B).CopyTo(array, 4);
				BitConverter.GetBytes(this.dg.C).CopyTo(array, 8);
				BitConverter.GetBytes(this.dg.D).CopyTo(array, 12);
				return array;
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000AB9C File Offset: 0x00008D9C
		public _MD5()
		{
			this.dg = new Digest();
		}

		// Token: 0x1700006B RID: 107
		// (set) Token: 0x060001FA RID: 506 RVA: 0x0000ABC0 File Offset: 0x00008DC0
		public string ValueAsFile
		{
			set
			{
				int num = 65536;
				byte[] array = new byte[num];
				uint num2 = 0U;
				byte[] array2 = new byte[0];
				using (Stream stream = File.OpenRead(value))
				{
					int num3;
					while ((num3 = stream.Read(array, 0, num)) == num)
					{
						num2 += 1U;
						this.TransformBlock(array);
					}
					byte[] array3 = new byte[0];
					bool flag = num3 != 0;
					if (flag)
					{
						array3 = new byte[num3];
						for (int i = 0; i < num3; i++)
						{
							array3[i] = array[i];
						}
					}
					ulong totalSize = (ulong)num2 * (ulong)((long)num) + (ulong)((long)num3);
					this.TransformBlock(_MD5.GetLastPaddedBuffer(totalSize, array3));
				}
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000AC94 File Offset: 0x00008E94
		public void TransformBlock(byte[] block)
		{
			uint num = 0U;
			bool flag = block != null;
			if (flag)
			{
				num = (uint)(block.Length * 8 / 32);
			}
			for (uint num2 = 0U; num2 < num / 16U; num2 += 1U)
			{
				this.CopyBlock(block, num2);
				uint a = this.dg.A;
				uint b = this.dg.B;
				uint c = this.dg.C;
				uint d = this.dg.D;
				this.PerformTransformation(ref a, ref b, ref c, ref d);
				this.dg.A = a;
				this.dg.B = b;
				this.dg.C = c;
				this.dg.D = d;
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000AD58 File Offset: 0x00008F58
		protected static byte[] GetLastPaddedBuffer(ulong totalSize, byte[] buffer)
		{
			bool flag = buffer != null;
			int num;
			if (flag)
			{
				num = 448 - buffer.Length * 8 % 512;
			}
			else
			{
				num = 448;
			}
			uint num2 = (uint)((num + 512) % 512);
			bool flag2 = num2 == 0U;
			if (flag2)
			{
				num2 = 512U;
			}
			bool flag3 = buffer != null;
			uint num3;
			if (flag3)
			{
				num3 = (uint)((long)buffer.Length + (long)((ulong)(num2 / 8U)) + 8L);
			}
			else
			{
				num3 = num2 / 8U + 8U;
			}
			ulong num4 = totalSize * 8UL;
			byte[] array = new byte[num3];
			bool flag4 = buffer != null;
			if (flag4)
			{
				for (int i = 0; i < buffer.Length; i++)
				{
					array[i] = buffer[i];
				}
				byte[] array2 = array;
				int num5 = buffer.Length;
				array2[num5] |= 128;
			}
			for (int j = 8; j > 0; j--)
			{
				array[(int)(checked((IntPtr)(unchecked((ulong)num3 - (ulong)((long)j)))))] = (byte)(num4 >> (8 - j) * 8 & 255UL);
			}
			return array;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000AE60 File Offset: 0x00009060
		protected void CalculateMD5Value()
		{
			byte[] array = this.CreatePaddedBuffer();
			uint num = (uint)(array.Length * 8 / 32);
			for (uint num2 = 0U; num2 < num / 16U; num2 += 1U)
			{
				this.CopyBlock(array, num2);
				uint a = this.dg.A;
				uint b = this.dg.B;
				uint c = this.dg.C;
				uint d = this.dg.D;
				this.PerformTransformation(ref a, ref b, ref c, ref d);
				this.dg.A = a;
				this.dg.B = b;
				this.dg.C = c;
				this.dg.D = d;
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000AF1D File Offset: 0x0000911D
		protected void TransF(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
		{
			a = b + _MD5.RotateLeft(a + ((b & c) | (~b & d)) + this.X[(int)k] + _MD5.T[(int)(i - 1U)], s);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000AF4C File Offset: 0x0000914C
		protected void TransG(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
		{
			a = b + _MD5.RotateLeft(a + ((b & d) | (c & ~d)) + this.X[(int)k] + _MD5.T[(int)(i - 1U)], s);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000AF7C File Offset: 0x0000917C
		protected void TransH(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
		{
			a = b + _MD5.RotateLeft(a + (b ^ c ^ d) + this.X[(int)k] + _MD5.T[(int)(i - 1U)], s);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000AFA8 File Offset: 0x000091A8
		protected void TransI(ref uint a, uint b, uint c, uint d, uint k, ushort s, uint i)
		{
			a = b + _MD5.RotateLeft(a + (c ^ (b | ~d)) + this.X[(int)k] + _MD5.T[(int)(i - 1U)], s);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000AFD8 File Offset: 0x000091D8
		protected void PerformTransformation(ref uint A, ref uint B, ref uint C, ref uint D)
		{
			uint num = A;
			uint num2 = B;
			uint num3 = C;
			uint num4 = D;
			this.TransF(ref A, B, C, D, 0U, 7, 1U);
			this.TransF(ref D, A, B, C, 1U, 12, 2U);
			this.TransF(ref C, D, A, B, 2U, 17, 3U);
			this.TransF(ref B, C, D, A, 3U, 22, 4U);
			this.TransF(ref A, B, C, D, 4U, 7, 5U);
			this.TransF(ref D, A, B, C, 5U, 12, 6U);
			this.TransF(ref C, D, A, B, 6U, 17, 7U);
			this.TransF(ref B, C, D, A, 7U, 22, 8U);
			this.TransF(ref A, B, C, D, 8U, 7, 9U);
			this.TransF(ref D, A, B, C, 9U, 12, 10U);
			this.TransF(ref C, D, A, B, 10U, 17, 11U);
			this.TransF(ref B, C, D, A, 11U, 22, 12U);
			this.TransF(ref A, B, C, D, 12U, 7, 13U);
			this.TransF(ref D, A, B, C, 13U, 12, 14U);
			this.TransF(ref C, D, A, B, 14U, 17, 15U);
			this.TransF(ref B, C, D, A, 15U, 22, 16U);
			this.TransG(ref A, B, C, D, 1U, 5, 17U);
			this.TransG(ref D, A, B, C, 6U, 9, 18U);
			this.TransG(ref C, D, A, B, 11U, 14, 19U);
			this.TransG(ref B, C, D, A, 0U, 20, 20U);
			this.TransG(ref A, B, C, D, 5U, 5, 21U);
			this.TransG(ref D, A, B, C, 10U, 9, 22U);
			this.TransG(ref C, D, A, B, 15U, 14, 23U);
			this.TransG(ref B, C, D, A, 4U, 20, 24U);
			this.TransG(ref A, B, C, D, 9U, 5, 25U);
			this.TransG(ref D, A, B, C, 14U, 9, 26U);
			this.TransG(ref C, D, A, B, 3U, 14, 27U);
			this.TransG(ref B, C, D, A, 8U, 20, 28U);
			this.TransG(ref A, B, C, D, 13U, 5, 29U);
			this.TransG(ref D, A, B, C, 2U, 9, 30U);
			this.TransG(ref C, D, A, B, 7U, 14, 31U);
			this.TransG(ref B, C, D, A, 12U, 20, 32U);
			this.TransH(ref A, B, C, D, 5U, 4, 33U);
			this.TransH(ref D, A, B, C, 8U, 11, 34U);
			this.TransH(ref C, D, A, B, 11U, 16, 35U);
			this.TransH(ref B, C, D, A, 14U, 23, 36U);
			this.TransH(ref A, B, C, D, 1U, 4, 37U);
			this.TransH(ref D, A, B, C, 4U, 11, 38U);
			this.TransH(ref C, D, A, B, 7U, 16, 39U);
			this.TransH(ref B, C, D, A, 10U, 23, 40U);
			this.TransH(ref A, B, C, D, 13U, 4, 41U);
			this.TransH(ref D, A, B, C, 0U, 11, 42U);
			this.TransH(ref C, D, A, B, 3U, 16, 43U);
			this.TransH(ref B, C, D, A, 6U, 23, 44U);
			this.TransH(ref A, B, C, D, 9U, 4, 45U);
			this.TransH(ref D, A, B, C, 12U, 11, 46U);
			this.TransH(ref C, D, A, B, 15U, 16, 47U);
			this.TransH(ref B, C, D, A, 2U, 23, 48U);
			this.TransI(ref A, B, C, D, 0U, 6, 49U);
			this.TransI(ref D, A, B, C, 7U, 10, 50U);
			this.TransI(ref C, D, A, B, 14U, 15, 51U);
			this.TransI(ref B, C, D, A, 5U, 21, 52U);
			this.TransI(ref A, B, C, D, 12U, 6, 53U);
			this.TransI(ref D, A, B, C, 3U, 10, 54U);
			this.TransI(ref C, D, A, B, 10U, 15, 55U);
			this.TransI(ref B, C, D, A, 1U, 21, 56U);
			this.TransI(ref A, B, C, D, 8U, 6, 57U);
			this.TransI(ref D, A, B, C, 15U, 10, 58U);
			this.TransI(ref C, D, A, B, 6U, 15, 59U);
			this.TransI(ref B, C, D, A, 13U, 21, 60U);
			this.TransI(ref A, B, C, D, 4U, 6, 61U);
			this.TransI(ref D, A, B, C, 11U, 10, 62U);
			this.TransI(ref C, D, A, B, 2U, 15, 63U);
			this.TransI(ref B, C, D, A, 9U, 21, 64U);
			A += num;
			B += num2;
			C += num3;
			D += num4;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000B514 File Offset: 0x00009714
		protected byte[] CreatePaddedBuffer()
		{
			int num = 448 - this.m_byteInput.Length * 8 % 512;
			uint num2 = (uint)((num + 512) % 512);
			bool flag = num2 == 0U;
			if (flag)
			{
				num2 = 512U;
			}
			uint num3 = (uint)((long)this.m_byteInput.Length + (long)((ulong)(num2 / 8U)) + 8L);
			ulong num4 = (ulong)((long)this.m_byteInput.Length * 8L);
			byte[] array = new byte[num3];
			for (int i = 0; i < this.m_byteInput.Length; i++)
			{
				array[i] = this.m_byteInput[i];
			}
			byte[] array2 = array;
			int num5 = this.m_byteInput.Length;
			array2[num5] |= 128;
			for (int j = 8; j > 0; j--)
			{
				array[(int)(checked((IntPtr)(unchecked((ulong)num3 - (ulong)((long)j)))))] = (byte)(num4 >> (8 - j) * 8 & 255UL);
			}
			return array;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000B604 File Offset: 0x00009804
		protected void CopyBlock(byte[] bMsg, uint block)
		{
			block <<= 6;
			bool flag = bMsg != null;
			if (flag)
			{
				for (uint num = 0U; num < 61U; num += 4U)
				{
					this.X[(int)(num >> 2)] = (uint)((int)bMsg[(int)(block + (num + 3U))] << 24 | (int)bMsg[(int)(block + (num + 2U))] << 16 | (int)bMsg[(int)(block + (num + 1U))] << 8 | (int)bMsg[(int)(block + num)]);
				}
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000B664 File Offset: 0x00009864
		public static uint RotateLeft(uint uiNumber, ushort shift)
		{
			return uiNumber >> (int)(32 - shift) | uiNumber << (int)shift;
		}

		// Token: 0x0400033B RID: 827
		private static readonly uint[] T = new uint[]
		{
			3614090360U,
			3905402710U,
			606105819U,
			3250441966U,
			4118548399U,
			1200080426U,
			2821735955U,
			4249261313U,
			1770035416U,
			2336552879U,
			4294925233U,
			2304563134U,
			1804603682U,
			4254626195U,
			2792965006U,
			1236535329U,
			4129170786U,
			3225465664U,
			643717713U,
			3921069994U,
			3593408605U,
			38016083U,
			3634488961U,
			3889429448U,
			568446438U,
			3275163606U,
			4107603335U,
			1163531501U,
			2850285829U,
			4243563512U,
			1735328473U,
			2368359562U,
			4294588738U,
			2272392833U,
			1839030562U,
			4259657740U,
			2763975236U,
			1272893353U,
			4139469664U,
			3200236656U,
			681279174U,
			3936430074U,
			3572445317U,
			76029189U,
			3654602809U,
			3873151461U,
			530742520U,
			3299628645U,
			4096336452U,
			1126891415U,
			2878612391U,
			4237533241U,
			1700485571U,
			2399980690U,
			4293915773U,
			2240044497U,
			1873313359U,
			4264355552U,
			2734768916U,
			1309151649U,
			4149444226U,
			3174756917U,
			718787259U,
			3951481745U
		};

		// Token: 0x0400033E RID: 830
		private Digest dg;
	}
}
