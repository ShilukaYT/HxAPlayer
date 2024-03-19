using System;
using System.IO;

namespace BlueStacks.MicroInstaller
{
	// Token: 0x02000099 RID: 153
	public sealed class BlueStacksUIColorManager
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000336 RID: 822 RVA: 0x00010055 File Offset: 0x0000E255
		public static string ThemeFilePath
		{
			get
			{
				return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ThemeFile");
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0001435B File Offset: 0x0001255B
		private BlueStacksUIColorManager()
		{
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00014368 File Offset: 0x00012568
		public static BlueStacksUIColorManager Instance
		{
			get
			{
				bool flag = BlueStacksUIColorManager.mInstance == null;
				if (flag)
				{
					object obj = BlueStacksUIColorManager.syncRoot;
					lock (obj)
					{
						bool flag2 = BlueStacksUIColorManager.mInstance == null;
						if (flag2)
						{
							BlueStacksUIColorManager blueStacksUIColorManager = new BlueStacksUIColorManager();
							BlueStacksUIColorManager.mInstance = blueStacksUIColorManager;
						}
					}
				}
				return BlueStacksUIColorManager.mInstance;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000339 RID: 825 RVA: 0x000143DC File Offset: 0x000125DC
		// (set) Token: 0x0600033A RID: 826 RVA: 0x00014484 File Offset: 0x00012684
		public static BluestacksUIColor AppliedTheme
		{
			get
			{
				bool flag = BlueStacksUIColorManager.mAppliedTheme == null;
				if (flag)
				{
					object obj = BlueStacksUIColorManager.syncRoot;
					lock (obj)
					{
						bool flag2 = BlueStacksUIColorManager.mAppliedTheme == null;
						if (flag2)
						{
							BluestacksUIColor bluestacksUIColor = BluestacksUIColor.Load(BlueStacksUIColorManager.ThemeFilePath);
							bool flag3 = bluestacksUIColor != null && bluestacksUIColor.DictBrush.Count > 0;
							if (flag3)
							{
								BlueStacksUIColorManager.mAppliedTheme = bluestacksUIColor;
							}
							bool flag4 = BlueStacksUIColorManager.mAppliedTheme != null;
							if (flag4)
							{
								BlueStacksUIColorManager.mAppliedTheme.NotifyUIElements();
							}
						}
					}
				}
				return BlueStacksUIColorManager.mAppliedTheme;
			}
			private set
			{
				bool flag = value != null;
				if (flag)
				{
					BlueStacksUIColorManager.mAppliedTheme = value;
					BlueStacksUIColorManager.mAppliedTheme.NotifyUIElements();
				}
			}
		}

		// Token: 0x0400051E RID: 1310
		public const string ThemeFileName = "ThemeFile";

		// Token: 0x0400051F RID: 1311
		private static volatile BlueStacksUIColorManager mInstance = null;

		// Token: 0x04000520 RID: 1312
		private static object syncRoot = new object();

		// Token: 0x04000521 RID: 1313
		private static BluestacksUIColor mAppliedTheme = null;
	}
}
