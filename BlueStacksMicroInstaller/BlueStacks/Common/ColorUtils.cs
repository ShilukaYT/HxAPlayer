using System;
using System.Drawing;
using System.Windows.Media;

namespace BlueStacks.Common
{
	// Token: 0x0200006D RID: 109
	[Serializable]
	public class ColorUtils
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00009C87 File Offset: 0x00007E87
		// (set) Token: 0x0600019B RID: 411 RVA: 0x00009C8F File Offset: 0x00007E8F
		public byte R { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00009C98 File Offset: 0x00007E98
		// (set) Token: 0x0600019D RID: 413 RVA: 0x00009CA0 File Offset: 0x00007EA0
		public byte G { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600019E RID: 414 RVA: 0x00009CA9 File Offset: 0x00007EA9
		// (set) Token: 0x0600019F RID: 415 RVA: 0x00009CB1 File Offset: 0x00007EB1
		public byte B { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00009CBA File Offset: 0x00007EBA
		// (set) Token: 0x060001A1 RID: 417 RVA: 0x00009CC2 File Offset: 0x00007EC2
		public byte A { get; set; }

		// Token: 0x060001A2 RID: 418 RVA: 0x00009CCB File Offset: 0x00007ECB
		public ColorUtils()
		{
			this.R = byte.MaxValue;
			this.G = byte.MaxValue;
			this.B = byte.MaxValue;
			this.A = byte.MaxValue;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00009D08 File Offset: 0x00007F08
		public ColorUtils(System.Windows.Media.Color value)
		{
			this.R = value.R;
			this.G = value.G;
			this.B = value.B;
			this.A = value.A;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00009D58 File Offset: 0x00007F58
		public ColorUtils(System.Drawing.Color value)
		{
			this.R = value.R;
			this.G = value.G;
			this.B = value.B;
			this.A = value.A;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00009DA8 File Offset: 0x00007FA8
		public static implicit operator System.Drawing.Color(ColorUtils rgb)
		{
			bool flag = rgb != null;
			System.Drawing.Color result;
			if (flag)
			{
				System.Drawing.Color color = System.Drawing.Color.FromArgb((int)rgb.A, (int)rgb.R, (int)rgb.G, (int)rgb.B);
				result = color;
			}
			else
			{
				result = default(System.Drawing.Color);
			}
			return result;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00009DF0 File Offset: 0x00007FF0
		public static explicit operator ColorUtils(System.Drawing.Color c)
		{
			return new ColorUtils(c);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00009E08 File Offset: 0x00008008
		public static ColorUtils FromHSL(double H, double S, double L)
		{
			return ColorUtils.FromHSLA(H, S, L, 1.0);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00009E2C File Offset: 0x0000802C
		public static ColorUtils FromHSLA(double H, double S, double L, double A)
		{
			bool flag = H > 1.0;
			if (flag)
			{
				H = 1.0;
			}
			bool flag2 = S > 1.0;
			if (flag2)
			{
				S = 1.0;
			}
			bool flag3 = L > 1.0;
			if (flag3)
			{
				L = 1.0;
			}
			bool flag4 = H < 0.0;
			if (flag4)
			{
				H = 0.0;
			}
			bool flag5 = S < 0.0;
			if (flag5)
			{
				S = 0.0;
			}
			bool flag6 = L < 0.0;
			if (flag6)
			{
				L = 0.0;
			}
			bool flag7 = A > 1.0;
			if (flag7)
			{
				A = 1.0;
			}
			double num = L;
			double num2 = L;
			double num3 = L;
			double num4 = (L <= 0.5) ? (L * (1.0 + S)) : (L + S - L * S);
			bool flag8 = num4 > 0.0;
			if (flag8)
			{
				double num5 = L + L - num4;
				double num6 = (num4 - num5) / num4;
				H *= 6.0;
				int num7 = (int)H;
				double num8 = H - (double)num7;
				double num9 = num4 * num6 * num8;
				double num10 = num5 + num9;
				double num11 = num4 - num9;
				switch (num7)
				{
				case 0:
					num = num4;
					num2 = num10;
					num3 = num5;
					break;
				case 1:
					num = num11;
					num2 = num4;
					num3 = num5;
					break;
				case 2:
					num = num5;
					num2 = num4;
					num3 = num10;
					break;
				case 3:
					num = num5;
					num2 = num11;
					num3 = num4;
					break;
				case 4:
					num = num10;
					num2 = num5;
					num3 = num4;
					break;
				case 5:
					num = num4;
					num2 = num5;
					num3 = num11;
					break;
				}
			}
			return new ColorUtils
			{
				R = Convert.ToByte(num * 255.0),
				G = Convert.ToByte(num2 * 255.0),
				B = Convert.ToByte(num3 * 255.0),
				A = Convert.ToByte(A * 255.0)
			};
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000A060 File Offset: 0x00008260
		public float H
		{
			get
			{
				return this.GetHue() / 360f;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0000A084 File Offset: 0x00008284
		public float S
		{
			get
			{
				return this.GetSaturation();
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000A0A0 File Offset: 0x000082A0
		public float L
		{
			get
			{
				return this.GetBrightness();
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000A0BB File Offset: 0x000082BB
		public System.Windows.Media.Color WPFColor
		{
			get
			{
				return System.Windows.Media.Color.FromArgb(this.A, this.R, this.G, this.B);
			}
		}
	}
}
