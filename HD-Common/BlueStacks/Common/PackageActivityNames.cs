using System;
using System.Collections.Generic;

namespace BlueStacks.Common
{
	// Token: 0x020000ED RID: 237
	public static class PackageActivityNames
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00005743 File Offset: 0x00003943
		public static List<string> SystemApps { get; } = new List<string>
		{
			"com.android.vending",
			"com.android.camera2",
			"com.android.chrome",
			"com.bluestacks.settings",
			"com.bluestacks.filemanager"
		};

		// Token: 0x04000303 RID: 771
		public const string DefaultBundledLauncher = "com.bluestacks.appmart";

		// Token: 0x04000304 RID: 772
		public const string DefaultBundledLauncherMainActivity = "com.bluestacks.appmart.StartTopAppsActivity";

		// Token: 0x020000EE RID: 238
		public static class Google
		{
			// Token: 0x04000306 RID: 774
			public const string GooglePlayStore = "com.android.vending";

			// Token: 0x04000307 RID: 775
			public const string GooglePlayServices = "com.google.android.gms";

			// Token: 0x04000308 RID: 776
			public const string Chrome = "com.android.chrome";
		}

		// Token: 0x020000EF RID: 239
		public static class BlueStacks
		{
			// Token: 0x04000309 RID: 777
			public const string AppMart = "com.bluestacks.appmart";

			// Token: 0x0400030A RID: 778
			public const string AppMartMainActivity = "com.bluestacks.appmart.StartTopAppsActivity";

			// Token: 0x0400030B RID: 779
			public const string GamePopHome = "com.bluestacks.gamepophome";

			// Token: 0x0400030C RID: 780
			public const string FileManager = "com.bluestacks.filemanager";

			// Token: 0x0400030D RID: 781
			public const string Settings = "com.bluestacks.settings";

			// Token: 0x0400030E RID: 782
			public const string ProvisionPackage = "com.android.provision";

			// Token: 0x0400030F RID: 783
			public const string AccountSigninPackage = "com.uncube.account";
		}

		// Token: 0x020000F0 RID: 240
		public static class ThirdParty
		{
			// Token: 0x04000310 RID: 784
			public const string Camera = "com.android.camera2";

			// Token: 0x04000311 RID: 785
			public const string PUBG_International = "com.tencent.ig";

			// Token: 0x04000312 RID: 786
			public const string FreeFire = "com.dts.freefireth";

			// Token: 0x04000313 RID: 787
			public const string BrawlStars = "com.supercell.brawlstars";

			// Token: 0x04000314 RID: 788
			public const string GalaxyStrore = "com.sec.android.app.samsungapps";

			// Token: 0x04000315 RID: 789
			public const string Warface = "com.my.warface.online.fps.pvp.action.shooter";

			// Token: 0x04000316 RID: 790
			public const string RiseOfKingdoms = "com.lilithgame.roc.gp";

			// Token: 0x04000317 RID: 791
			public const string ROKImagePickerActivity = "com.lilithgame.roc.gp/sh.lilith.lilithchat.activities.ImagePickerActivity";

			// Token: 0x04000318 RID: 792
			public const string AndroidGalleryActivity = "com.android.gallery3d /.app.GalleryActivity";

			// Token: 0x04000319 RID: 793
			public const string CODAppName = "Call of Duty: Mobile";

			// Token: 0x0400031A RID: 794
			public const string PUBGAppName = "PUBG Mobile";

			// Token: 0x0400031B RID: 795
			public const string FreeFireAppName = "Garena Free Fire";

			// Token: 0x0400031C RID: 796
			public static readonly List<string> AllPUBGPackageNames = new List<string>
			{
				"com.tencent.ig",
				"com.rekoo.pubgm",
				"com.vng.pubgmobile",
				"com.pubg.krmobile",
				"com.tencent.tmgp.pubgmhd"
			};

			// Token: 0x0400031D RID: 797
			public static readonly List<string> AllCallOfDutyPackageNames = new List<string>
			{
				"com.tencent.tmgp.kr.codm",
				"com.garena.game.codm",
				"com.activision.callofduty.shooter",
				"com.vng.codmvn"
			};

			// Token: 0x0400031E RID: 798
			public static readonly List<string> AllOneStorePackageNames = new List<string>
			{
				"com.skt.skaf.A000Z00040",
				"com.skt.skaf.OA00018282"
			};
		}
	}
}
