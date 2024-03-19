using System;
using System.Runtime.InteropServices;

namespace BlueStacks.Common
{
	// Token: 0x02000091 RID: 145
	public class HyperV
	{
		// Token: 0x0600029B RID: 667
		[DllImport("HD-Common-Native.dll", SetLastError = true)]
		private static extern int IsHyperVEnabled();

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000E508 File Offset: 0x0000C708
		public static HyperV Instance
		{
			get
			{
				bool flag = HyperV.sInstance == null;
				if (flag)
				{
					object obj = HyperV.syncRoot;
					lock (obj)
					{
						bool flag2 = HyperV.sInstance == null;
						if (flag2)
						{
							HyperV hyperV = new HyperV();
							hyperV.SetValues();
							HyperV.sInstance = hyperV;
						}
					}
				}
				return HyperV.sInstance;
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000E57C File Offset: 0x0000C77C
		private void SetValues()
		{
			this.HyperVStatus = HyperV.GetHyperVStatus();
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000E58C File Offset: 0x0000C78C
		private static HyperV.ReturnCodes GetHyperVStatus()
		{
			Logger.Info("Checking Hyper-V in system");
			int num = HyperV.IsHyperVEnabled();
			Logger.Info("IsHyperVEnabled: {0}", new object[]
			{
				num
			});
			return (HyperV.ReturnCodes)num;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000E5CC File Offset: 0x0000C7CC
		public bool IsAnyHyperVPresent
		{
			get
			{
				return this.HyperVStatus > HyperV.ReturnCodes.None;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000E5E8 File Offset: 0x0000C7E8
		public bool IsMicrosoftHyperVPresent
		{
			get
			{
				return this.HyperVStatus == HyperV.ReturnCodes.MicrosoftHyperV;
			}
		}

		// Token: 0x040004E3 RID: 1251
		private static HyperV sInstance;

		// Token: 0x040004E4 RID: 1252
		private static object syncRoot = new object();

		// Token: 0x040004E5 RID: 1253
		public HyperV.ReturnCodes HyperVStatus = HyperV.ReturnCodes.None;

		// Token: 0x020000E2 RID: 226
		public enum ReturnCodes
		{
			// Token: 0x04000800 RID: 2048
			None,
			// Token: 0x04000801 RID: 2049
			MicrosoftHyperV,
			// Token: 0x04000802 RID: 2050
			OtherHyperV
		}
	}
}
