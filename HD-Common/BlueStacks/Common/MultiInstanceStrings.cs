using System;

namespace BlueStacks.Common
{
	// Token: 0x020000D2 RID: 210
	public static class MultiInstanceStrings
	{
		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00004ED6 File Offset: 0x000030D6
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x00019E0C File Offset: 0x0001800C
		public static string VmName
		{
			get
			{
				if (MultiInstanceStrings.sVmName == null)
				{
					return "Android";
				}
				return MultiInstanceStrings.sVmName;
			}
			set
			{
				if (MultiInstanceStrings.sVmName != null)
				{
					throw new Exception("VmName can be set only once");
				}
				MultiInstanceStrings.sVmName = value;
				if (MultiInstanceStrings.sVmName == "Android")
				{
					MultiInstanceStrings.sVmId = 0;
					return;
				}
				string text = MultiInstanceStrings.sVmName;
				if (!int.TryParse((text != null) ? text.Split(new char[]
				{
					'_'
				})[1] : null, out MultiInstanceStrings.sVmId))
				{
					throw new Exception("Invalid VM: " + MultiInstanceStrings.sVmName);
				}
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00004EEA File Offset: 0x000030EA
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x00004F06 File Offset: 0x00003106
		public static ushort BstServerPort
		{
			get
			{
				return (ushort)RegistryManager.Instance.Guest[MultiInstanceStrings.VmName].BstAndroidPort;
			}
			set
			{
				RegistryManager.Instance.Guest[MultiInstanceStrings.VmName].BstAndroidPort = (int)value;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00004F22 File Offset: 0x00003122
		public static int VmId
		{
			get
			{
				return MultiInstanceStrings.sVmId;
			}
		}

		// Token: 0x04000252 RID: 594
		private static string sVmName;

		// Token: 0x04000253 RID: 595
		private static int sVmId;
	}
}
