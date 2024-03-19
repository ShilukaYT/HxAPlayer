using System;
using System.Drawing;
using System.Windows.Media;

namespace BlueStacks.Common
{
	// Token: 0x02000220 RID: 544
	[Serializable]
	public class ColorUtils
	{
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x0000E4D7 File Offset: 0x0000C6D7
		// (set) Token: 0x060010E8 RID: 4328 RVA: 0x0000E4DF File Offset: 0x0000C6DF
		public byte R { get; set; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x0000E4E8 File Offset: 0x0000C6E8
		// (set) Token: 0x060010EA RID: 4330 RVA: 0x0000E4F0 File Offset: 0x0000C6F0
		public byte G { get; set; }

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x0000E4F9 File Offset: 0x0000C6F9
		// (set) Token: 0x060010EC RID: 4332 RVA: 0x0000E501 File Offset: 0x0000C701
		public byte B { get; set; }

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x0000E50A File Offset: 0x0000C70A
		// (set) Token: 0x060010EE RID: 4334 RVA: 0x0000E512 File Offset: 0x0000C712
		public byte A { get; set; }

		// Token: 0x060010EF RID: 4335 RVA: 0x0000E51B File Offset: 0x0000C71B
		public ColorUtils()
		{
			this.R = byte.MaxValue;
			this.G = byte.MaxValue;
			this.B = byte.MaxValue;
			this.A = byte.MaxValue;
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0000E54F File Offset: 0x0000C74F
		public ColorUtils(System.Windows.Media.Color value)
		{
			this.R = value.R;
			this.G = value.G;
			this.B = value.B;
			this.A = value.A;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0000E58B File Offset: 0x0000C78B
		public ColorUtils(System.Drawing.Color value)
		{
			this.R = value.R;
			this.G = value.G;
			this.B = value.B;
			this.A = value.A;
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0004075C File Offset: 0x0003E95C
		public static implicit operator System.Drawing.Color(ColorUtils rgb)
		{
			if (rgb != null)
			{
				return System.Drawing.Color.FromArgb((int)rgb.A, (int)rgb.R, (int)rgb.G, (int)rgb.B);
			}
			return default(System.Drawing.Color);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0000E5C7 File Offset: 0x0000C7C7
		public static explicit operator ColorUtils(System.Drawing.Color c)
		{
			return new ColorUtils(c);
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0000E5CF File Offset: 0x0000C7CF
		public static ColorUtils FromHSL(double H, double S, double L)
		{
			return ColorUtils.FromHSLA(H, S, L, 1.0);
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00040794 File Offset: 0x0003E994
		public static ColorUtils FromHSLA(double H, double S, double L, double A)
		{
			if (H > 1.0)
			{
				H = 1.0;
			}
			if (S > 1.0)
			{
				S = 1.0;
			}
			if (L > 1.0)
			{
				L = 1.0;
			}
			if (H < 0.0)
			{
				H = 0.0;
			}
			if (S < 0.0)
			{
				S = 0.0;
			}
			if (L < 0.0)
			{
				L = 0.0;
			}
			if (A > 1.0)
			{
				A = 1.0;
			}
			double num = L;
			double num2 = L;
			double num3 = L;
			double num4 = (L <= 0.5) ? (L * (1.0 + S)) : (L + S - L * S);
			if (num4 > 0.0)
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

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x00040970 File Offset: 0x0003EB70
		public float H
		{
			get
			{
				return this.GetHue() / 360f;
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x00040994 File Offset: 0x0003EB94
		public float S
		{
			get
			{
				return this.GetSaturation();
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x000409B0 File Offset: 0x0003EBB0
		public float L
		{
			get
			{
				return this.GetBrightness();
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x0000E5E2 File Offset: 0x0000C7E2
		public System.Windows.Media.Color WPFColor
		{
			get
			{
				return System.Windows.Media.Color.FromArgb(this.A, this.R, this.G, this.B);
			}
		}
	}
}
