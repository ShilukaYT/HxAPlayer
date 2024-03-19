using System;
using Microsoft.Win32;

namespace BlueStacks.Common
{
	// Token: 0x0200011E RID: 286
	public class InstanceRegistry
	{
		// Token: 0x0600084F RID: 2127 RVA: 0x00026690 File Offset: 0x00024890
		public InstanceRegistry(string vmId, string oem = "bgp")
		{
			this.mVmId = vmId;
			if (oem == null)
			{
				oem = "bgp";
			}
			this.Init(oem);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x000267A4 File Offset: 0x000249A4
		private void Init(string oem = "bgp")
		{
			string str = oem.Equals("bgp", StringComparison.InvariantCultureIgnoreCase) ? "" : ("_" + oem);
			this.mBaseKeyPath = "Software\\BlueStacks" + str + RegistryManager.UPGRADE_TAG;
			this.AndroidKeyPath = this.mBaseKeyPath + "\\Guests\\" + this.mVmId;
			this.mBlockDeviceKeyPath = this.AndroidKeyPath + "\\BlockDevice";
			this.mBlockDevice0KeyPath = this.AndroidKeyPath + "\\BlockDevice\\0";
			this.mBlockDevice1KeyPath = this.AndroidKeyPath + "\\BlockDevice\\1";
			this.mBlockDevice2KeyPath = this.AndroidKeyPath + "\\BlockDevice\\2";
			this.mBlockDevice3KeyPath = this.AndroidKeyPath + "\\BlockDevice\\3";
			this.mBlockDevice4KeyPath = this.AndroidKeyPath + "\\BlockDevice\\4";
			this.mVmConfigKeyPath = this.AndroidKeyPath + "\\Config";
			this.mFrameBufferKeyPath = this.AndroidKeyPath + "\\FrameBuffer";
			this.mFrameBuffer0KeyPath = this.AndroidKeyPath + "\\FrameBuffer\\0";
			this.mNetworkKeyPath = this.AndroidKeyPath + "\\Network";
			this.mNetwork0KeyPath = this.AndroidKeyPath + "\\Network\\0";
			this.mNetworkRedirectKeyPath = this.AndroidKeyPath + "\\Network\\Redirect";
			this.mSharedFolderKeyPath = this.AndroidKeyPath + "\\SharedFolder";
			this.mSharedFolder0KeyPath = this.AndroidKeyPath + "\\SharedFolder\\0";
			this.mSharedFolder1KeyPath = this.AndroidKeyPath + "\\SharedFolder\\1";
			this.mSharedFolder2KeyPath = this.AndroidKeyPath + "\\SharedFolder\\2";
			this.mSharedFolder3KeyPath = this.AndroidKeyPath + "\\SharedFolder\\3";
			this.mSharedFolder4KeyPath = this.AndroidKeyPath + "\\SharedFolder\\4";
			this.mSharedFolder5KeyPath = this.AndroidKeyPath + "\\SharedFolder\\5";
			RegistryUtils.InitKey(this.mBlockDevice0KeyPath);
			RegistryUtils.InitKey(this.mBlockDevice1KeyPath);
			RegistryUtils.InitKey(this.mBlockDevice2KeyPath);
			RegistryUtils.InitKey(this.mBlockDevice3KeyPath);
			RegistryUtils.InitKey(this.mBlockDevice4KeyPath);
			RegistryUtils.InitKey(this.mVmConfigKeyPath);
			RegistryUtils.InitKey(this.mFrameBuffer0KeyPath);
			RegistryUtils.InitKey(this.mNetwork0KeyPath);
			RegistryUtils.InitKey(this.mNetworkRedirectKeyPath);
			RegistryUtils.InitKey(this.mSharedFolder0KeyPath);
			RegistryUtils.InitKey(this.mSharedFolder1KeyPath);
			RegistryUtils.InitKey(this.mSharedFolder2KeyPath);
			RegistryUtils.InitKey(this.mSharedFolder3KeyPath);
			RegistryUtils.InitKey(this.mSharedFolder4KeyPath);
			RegistryUtils.InitKey(this.mSharedFolder5KeyPath);
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x00006ED5 File Offset: 0x000050D5
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x00006EDD File Offset: 0x000050DD
		public string AndroidKeyPath { get; private set; } = "";

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x00006EE6 File Offset: 0x000050E6
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x00006F04 File Offset: 0x00005104
		public int EmulatePortraitMode
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mFrameBuffer0KeyPath, "EmulatePortraitMode", -1, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mFrameBuffer0KeyPath, "EmulatePortraitMode", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00006F1F File Offset: 0x0000511F
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x00006F3D File Offset: 0x0000513D
		public int Depth
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mFrameBuffer0KeyPath, "Depth", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mFrameBuffer0KeyPath, "Depth", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x00006F58 File Offset: 0x00005158
		// (set) Token: 0x06000858 RID: 2136 RVA: 0x00006F76 File Offset: 0x00005176
		public int HideBootProgress
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mFrameBuffer0KeyPath, "HideBootProgress", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mFrameBuffer0KeyPath, "HideBootProgress", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x00006F91 File Offset: 0x00005191
		// (set) Token: 0x0600085A RID: 2138 RVA: 0x00006FAF File Offset: 0x000051AF
		public int WindowWidth
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mFrameBuffer0KeyPath, "WindowWidth", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mFrameBuffer0KeyPath, "WindowWidth", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x00006FCA File Offset: 0x000051CA
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x00006FE8 File Offset: 0x000051E8
		public int WindowHeight
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mFrameBuffer0KeyPath, "WindowHeight", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mFrameBuffer0KeyPath, "WindowHeight", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x00007003 File Offset: 0x00005203
		// (set) Token: 0x0600085E RID: 2142 RVA: 0x00007021 File Offset: 0x00005221
		public int GuestWidth
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mFrameBuffer0KeyPath, "GuestWidth", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mFrameBuffer0KeyPath, "GuestWidth", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x0000703C File Offset: 0x0000523C
		// (set) Token: 0x06000860 RID: 2144 RVA: 0x0000705A File Offset: 0x0000525A
		public int GuestHeight
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mFrameBuffer0KeyPath, "GuestHeight", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mFrameBuffer0KeyPath, "GuestHeight", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x00007075 File Offset: 0x00005275
		// (set) Token: 0x06000862 RID: 2146 RVA: 0x00007093 File Offset: 0x00005293
		public int Memory
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.AndroidKeyPath, "Memory", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.AndroidKeyPath, "Memory", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x000070AE File Offset: 0x000052AE
		// (set) Token: 0x06000864 RID: 2148 RVA: 0x000070D1 File Offset: 0x000052D1
		public bool IsSidebarVisible
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.AndroidKeyPath, "IsSidebarVisible", 1, RegistryKeyKind.HKEY_LOCAL_MACHINE) != 0;
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.AndroidKeyPath, "IsSidebarVisible", value ? 1 : 0, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x000070F2 File Offset: 0x000052F2
		// (set) Token: 0x06000866 RID: 2150 RVA: 0x00007115 File Offset: 0x00005315
		public bool IsTopbarVisible
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.AndroidKeyPath, "IsTopbarVisible", 1, RegistryKeyKind.HKEY_LOCAL_MACHINE) != 0;
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.AndroidKeyPath, "IsTopbarVisible", value ? 1 : 0, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000867 RID: 2151 RVA: 0x00007136 File Offset: 0x00005336
		// (set) Token: 0x06000868 RID: 2152 RVA: 0x00007159 File Offset: 0x00005359
		public bool IsSidebarInDefaultState
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.AndroidKeyPath, "IsSidebarInDefaultState", 1, RegistryKeyKind.HKEY_LOCAL_MACHINE) != 0;
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.AndroidKeyPath, "IsSidebarInDefaultState", value ? 1 : 0, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x0000717A File Offset: 0x0000537A
		// (set) Token: 0x0600086A RID: 2154 RVA: 0x00007193 File Offset: 0x00005393
		public string Kernel
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.AndroidKeyPath, "Kernel", null, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.AndroidKeyPath, "Kernel", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x000071A9 File Offset: 0x000053A9
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x000071C2 File Offset: 0x000053C2
		public string Initrd
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.AndroidKeyPath, "Initrd", null, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.AndroidKeyPath, "Initrd", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x000071D8 File Offset: 0x000053D8
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x000071F6 File Offset: 0x000053F6
		public int DisableRobustness
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.AndroidKeyPath, "DisableRobustness", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.AndroidKeyPath, "DisableRobustness", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x00007211 File Offset: 0x00005411
		// (set) Token: 0x06000870 RID: 2160 RVA: 0x0000722E File Offset: 0x0000542E
		public string VirtType
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.AndroidKeyPath, "VirtType", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.AndroidKeyPath, "VirtType", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x00007244 File Offset: 0x00005444
		// (set) Token: 0x06000872 RID: 2162 RVA: 0x00007261 File Offset: 0x00005461
		public string BootParameters
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.AndroidKeyPath, "BootParameters", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.AndroidKeyPath, "BootParameters", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x00007277 File Offset: 0x00005477
		// (set) Token: 0x06000874 RID: 2164 RVA: 0x0000729A File Offset: 0x0000549A
		public bool ShowSidebarInFullScreen
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "ShowSidebarInFullScreen", 1, RegistryKeyKind.HKEY_LOCAL_MACHINE) != 0;
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "ShowSidebarInFullScreen", (!value) ? 0 : 1, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x000072BB File Offset: 0x000054BB
		// (set) Token: 0x06000876 RID: 2166 RVA: 0x000072D8 File Offset: 0x000054D8
		public string BlockDevice0Name
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mBlockDevice0KeyPath, "Name", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mBlockDevice0KeyPath, "Name", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x000072EE File Offset: 0x000054EE
		// (set) Token: 0x06000878 RID: 2168 RVA: 0x0000730B File Offset: 0x0000550B
		public string BlockDevice0Path
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mBlockDevice0KeyPath, "Path", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mBlockDevice0KeyPath, "Path", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x00007321 File Offset: 0x00005521
		// (set) Token: 0x0600087A RID: 2170 RVA: 0x0000733E File Offset: 0x0000553E
		public string BlockDevice1Name
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mBlockDevice1KeyPath, "Name", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mBlockDevice1KeyPath, "Name", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x00007354 File Offset: 0x00005554
		// (set) Token: 0x0600087C RID: 2172 RVA: 0x00007371 File Offset: 0x00005571
		public string BlockDevice1Path
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mBlockDevice1KeyPath, "Path", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mBlockDevice1KeyPath, "Path", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x00007387 File Offset: 0x00005587
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x000073A4 File Offset: 0x000055A4
		public string BlockDevice2Name
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mBlockDevice2KeyPath, "Name", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mBlockDevice2KeyPath, "Name", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x000073BA File Offset: 0x000055BA
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x000073D7 File Offset: 0x000055D7
		public string BlockDevice2Path
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mBlockDevice2KeyPath, "Path", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mBlockDevice2KeyPath, "Path", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x000073ED File Offset: 0x000055ED
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x0000740A File Offset: 0x0000560A
		public string BlockDevice4Name
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mBlockDevice4KeyPath, "Name", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mBlockDevice4KeyPath, "Name", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x00007420 File Offset: 0x00005620
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x0000743D File Offset: 0x0000563D
		public string BlockDevice4Path
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mBlockDevice4KeyPath, "Path", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mBlockDevice4KeyPath, "Path", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x00007453 File Offset: 0x00005653
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x00007470 File Offset: 0x00005670
		public string Locale
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "Locale", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "Locale", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x00007486 File Offset: 0x00005686
		// (set) Token: 0x06000888 RID: 2184 RVA: 0x000074BE File Offset: 0x000056BE
		public int VCPUs
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "VCPUs", Utils.GetRecommendedVCPUCount(this.mVmId == "Android"), RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "VCPUs", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x000074D9 File Offset: 0x000056D9
		// (set) Token: 0x0600088A RID: 2186 RVA: 0x000074F6 File Offset: 0x000056F6
		public string EnableConsoleAccess
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "EnableConsoleAccess", string.Empty, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "EnableConsoleAccess", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x0000750C File Offset: 0x0000570C
		// (set) Token: 0x0600088C RID: 2188 RVA: 0x0000752A File Offset: 0x0000572A
		public int GlRenderMode
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "GlRenderMode", -1, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "GlRenderMode", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00007545 File Offset: 0x00005745
		// (set) Token: 0x0600088E RID: 2190 RVA: 0x00007564 File Offset: 0x00005764
		public int FPS
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "FPS", 60, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "FPS", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x0000757F File Offset: 0x0000577F
		// (set) Token: 0x06000890 RID: 2192 RVA: 0x0000759D File Offset: 0x0000579D
		public int ShowFPS
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "ShowFPS", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "ShowFPS", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x000075B8 File Offset: 0x000057B8
		// (set) Token: 0x06000892 RID: 2194 RVA: 0x000075D6 File Offset: 0x000057D6
		public int EnableHighFPS
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "EnableHighFPS", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "EnableHighFPS", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x000075F1 File Offset: 0x000057F1
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x0000760F File Offset: 0x0000580F
		public int EnableVSync
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "EnableVSync", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "EnableVSync", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x0000762A File Offset: 0x0000582A
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x00007648 File Offset: 0x00005848
		public int GlMode
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "GlMode", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "GlMode", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x00007663 File Offset: 0x00005863
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x00007681 File Offset: 0x00005881
		public int Camera
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "Camera", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "Camera", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x0000769C File Offset: 0x0000589C
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x000076BA File Offset: 0x000058BA
		public int ConfigSynced
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "ConfigSynced", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "ConfigSynced", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x000076D5 File Offset: 0x000058D5
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x000076F3 File Offset: 0x000058F3
		public int HScroll
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "HScroll", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "HScroll", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0000770E File Offset: 0x0000590E
		// (set) Token: 0x0600089E RID: 2206 RVA: 0x0000772C File Offset: 0x0000592C
		public int GpsMode
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "GpsMode", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "GpsMode", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x00007747 File Offset: 0x00005947
		// (set) Token: 0x060008A0 RID: 2208 RVA: 0x00007765 File Offset: 0x00005965
		public int FileSystem
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "FileSystem", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "FileSystem", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00007780 File Offset: 0x00005980
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x0000779E File Offset: 0x0000599E
		public int StopZygoteOnClose
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "StopZygoteOnClose", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "StopZygoteOnClose", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x000077B9 File Offset: 0x000059B9
		// (set) Token: 0x060008A4 RID: 2212 RVA: 0x000077D7 File Offset: 0x000059D7
		public int FenceSyncType
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "FenceSyncType", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "FenceSyncType", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x000077F2 File Offset: 0x000059F2
		// (set) Token: 0x060008A6 RID: 2214 RVA: 0x00007813 File Offset: 0x00005A13
		public bool CanAccessWindowsFolder
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "CanAccessWindowsFolder", 1, RegistryKeyKind.HKEY_LOCAL_MACHINE) == 1;
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "CanAccessWindowsFolder", (!value) ? 0 : 1, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00007834 File Offset: 0x00005A34
		// (set) Token: 0x060008A8 RID: 2216 RVA: 0x00007851 File Offset: 0x00005A51
		public string EcoModeFPS
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "EcoModeFPS", "5", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "EcoModeFPS", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00007867 File Offset: 0x00005A67
		// (set) Token: 0x060008AA RID: 2218 RVA: 0x00007885 File Offset: 0x00005A85
		public int FrontendNoClose
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "FrontendNoClose", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "FrontendNoClose", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x000078A0 File Offset: 0x00005AA0
		// (set) Token: 0x060008AC RID: 2220 RVA: 0x000078BE File Offset: 0x00005ABE
		public int GpsSource
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "GpsSource", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "GpsSource", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x000078D9 File Offset: 0x00005AD9
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x000078F6 File Offset: 0x00005AF6
		public string GpsLatitude
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "GpsLatitude", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "GpsLatitude", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x0000790C File Offset: 0x00005B0C
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x00007929 File Offset: 0x00005B29
		public string GpsLongitude
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "GpsLongitude", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "GpsLongitude", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x0000793F File Offset: 0x00005B3F
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x0000795D File Offset: 0x00005B5D
		public int GlPort
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "GlPort", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "GlPort", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00007978 File Offset: 0x00005B78
		// (set) Token: 0x060008B4 RID: 2228 RVA: 0x00007995 File Offset: 0x00005B95
		public string GamingResolutionPubg
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "GamingResolutionPubg", "1", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "GamingResolutionPubg", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x000079AB File Offset: 0x00005BAB
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x000079C8 File Offset: 0x00005BC8
		public string DisplayQualityPubg
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "DisplayQualityPubg", "-1", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "DisplayQualityPubg", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x000079DE File Offset: 0x00005BDE
		// (set) Token: 0x060008B8 RID: 2232 RVA: 0x000079FB File Offset: 0x00005BFB
		public string GamingResolutionCOD
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "GamingResolutionCOD", "720", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "GamingResolutionCOD", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00007A11 File Offset: 0x00005C11
		// (set) Token: 0x060008BA RID: 2234 RVA: 0x00007A2E File Offset: 0x00005C2E
		public string DisplayQualityCOD
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "DisplayQualityCOD", "-1", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "DisplayQualityCOD", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x00007A44 File Offset: 0x00005C44
		// (set) Token: 0x060008BC RID: 2236 RVA: 0x00007A62 File Offset: 0x00005C62
		public int HostSensorPort
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "HostSensorPort", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "HostSensorPort", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x00007A7D File Offset: 0x00005C7D
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x00007A9B File Offset: 0x00005C9B
		public int SoftControlBarHeightLandscape
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "SoftControlBarHeightLandscape", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "SoftControlBarHeightLandscape", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x00007AB6 File Offset: 0x00005CB6
		// (set) Token: 0x060008C0 RID: 2240 RVA: 0x00007AD4 File Offset: 0x00005CD4
		public int SoftControlBarHeightPortrait
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "SoftControlBarHeightPortrait", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "SoftControlBarHeightPortrait", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00007AEF File Offset: 0x00005CEF
		// (set) Token: 0x060008C2 RID: 2242 RVA: 0x00007B0D File Offset: 0x00005D0D
		public int GrabKeyboard
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "GrabKeyboard", 1, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "GrabKeyboard", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x00007B28 File Offset: 0x00005D28
		// (set) Token: 0x060008C4 RID: 2244 RVA: 0x00007B46 File Offset: 0x00005D46
		public int DisableDWM
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "DisableDWM", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "DisableDWM", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00007B61 File Offset: 0x00005D61
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x00007B7F File Offset: 0x00005D7F
		public int DisablePcIme
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "DisablePcIme", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "DisablePcIme", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00007B9A File Offset: 0x00005D9A
		// (set) Token: 0x060008C8 RID: 2248 RVA: 0x00007BB8 File Offset: 0x00005DB8
		public int EnableBSTVC
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "EnableBSTVC", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "EnableBSTVC", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00007BD3 File Offset: 0x00005DD3
		// (set) Token: 0x060008CA RID: 2250 RVA: 0x00007BF1 File Offset: 0x00005DF1
		public int ForceVMLegacyMode
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "ForceVMLegacyMode", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "ForceVMLegacyMode", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x00007C0C File Offset: 0x00005E0C
		// (set) Token: 0x060008CC RID: 2252 RVA: 0x00007C2E File Offset: 0x00005E2E
		public int FrontendServerPort
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "FrontendServerPort", 2881, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "FrontendServerPort", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00007C49 File Offset: 0x00005E49
		// (set) Token: 0x060008CE RID: 2254 RVA: 0x00007C6B File Offset: 0x00005E6B
		public int BstAndroidPort
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "BstAndroidPort", 9999, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "BstAndroidPort", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00007C86 File Offset: 0x00005E86
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x00007CA8 File Offset: 0x00005EA8
		public int BstAdbPort
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "BstAdbPort", 5555, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "BstAdbPort", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x00007CC3 File Offset: 0x00005EC3
		// (set) Token: 0x060008D2 RID: 2258 RVA: 0x00007CE5 File Offset: 0x00005EE5
		public int TriggerMemoryTrimThreshold
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "TriggerMemoryTrimThreshold", 700, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "TriggerMemoryTrimThreshold", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x00007D00 File Offset: 0x00005F00
		// (set) Token: 0x060008D4 RID: 2260 RVA: 0x00007D22 File Offset: 0x00005F22
		public int TriggerMemoryTrimTimerInterval
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "TriggerMemoryTrimTimerInterval", 60000, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "TriggerMemoryTrimTimerInterval", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x00007D3D File Offset: 0x00005F3D
		// (set) Token: 0x060008D6 RID: 2262 RVA: 0x00007D5B File Offset: 0x00005F5B
		public int UpdatedVersion
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "UpdatedVersion", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "UpdatedVersion", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x00007D76 File Offset: 0x00005F76
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x00007D94 File Offset: 0x00005F94
		public int GPSAvailable
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "GPSAvailable", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "GPSAvailable", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00007DAF File Offset: 0x00005FAF
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x00007DCC File Offset: 0x00005FCC
		public string OpenSensorDeviceId
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "OpenSensorDeviceId", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "OpenSensorDeviceId", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x00007DE2 File Offset: 0x00005FE2
		// (set) Token: 0x060008DC RID: 2268 RVA: 0x00007E00 File Offset: 0x00006000
		public int HostForwardSensorPort
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "HostForwardSensorPort", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "HostForwardSensorPort", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x00007E1B File Offset: 0x0000601B
		// (set) Token: 0x060008DE RID: 2270 RVA: 0x00007E38 File Offset: 0x00006038
		public string ImeSelected
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "ImeSelected", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "ImeSelected", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x00007E4E File Offset: 0x0000604E
		// (set) Token: 0x060008E0 RID: 2272 RVA: 0x00007E6C File Offset: 0x0000606C
		public int RunAppProcessId
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "RunAppProcessId", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "RunAppProcessId", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00007E87 File Offset: 0x00006087
		// (set) Token: 0x060008E2 RID: 2274 RVA: 0x00007EA4 File Offset: 0x000060A4
		public string DisplayName
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "DisplayName", string.Empty, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "DisplayName", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00026A5C File Offset: 0x00024C5C
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x00007EBA File Offset: 0x000060BA
		public string LastBootDate
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "LastBootDate", DateTime.Now.Date.ToShortDateString(), RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "LastBootDate", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00007ED0 File Offset: 0x000060D0
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x00007EF3 File Offset: 0x000060F3
		public bool IsOneTimeSetupDone
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "IsOneTimeSetupDone", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE) != 0;
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "IsOneTimeSetupDone", value ? 1 : 0, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x00007F14 File Offset: 0x00006114
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x00007F37 File Offset: 0x00006137
		public bool IsMuted
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "IsMuted", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE) != 0;
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "IsMuted", value ? 1 : 0, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00007F58 File Offset: 0x00006158
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x00007F76 File Offset: 0x00006176
		public int Volume
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "Volume", 5, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "Volume", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x00007F91 File Offset: 0x00006191
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x00007FB4 File Offset: 0x000061B4
		public bool FixVboxConfig
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "FixVboxConfig", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE) != 0;
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "FixVboxConfig", value ? 1 : 0, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x00007FD5 File Offset: 0x000061D5
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x00007FF2 File Offset: 0x000061F2
		public string WindowPlacement
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "WindowPlacement", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "WindowPlacement", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x00008008 File Offset: 0x00006208
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x0000802B File Offset: 0x0000622B
		public bool IsGoogleSigninDone
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "IsGoogleSigninDone", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE) != 0;
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "IsGoogleSigninDone", value ? 1 : 0, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0000804C File Offset: 0x0000624C
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x0000806F File Offset: 0x0000626F
		public bool IsGoogleSigninPopupShown
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "IsGoogleSigninPopupShown", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE) != 0;
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "IsGoogleSigninPopupShown", value ? 1 : 0, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x00008090 File Offset: 0x00006290
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x000080AE File Offset: 0x000062AE
		public string[] GrmDonotShowRuleList
		{
			get
			{
				return (string[])RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "GrmDonotShowRuleList", new string[0], RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "GrmDonotShowRuleList", value, RegistryValueKind.MultiString, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x000080C4 File Offset: 0x000062C4
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x000080E1 File Offset: 0x000062E1
		public string GoogleAId
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "BstVmAId", string.Empty, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "BstVmAId", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x000080F7 File Offset: 0x000062F7
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x00008114 File Offset: 0x00006314
		public string AndroidId
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "BstVmId", string.Empty, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "BstVmId", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x0000812A File Offset: 0x0000632A
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x0000814D File Offset: 0x0000634D
		public bool ShowMacroDeletePopup
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "ShowMacroDeletePopup", 1, RegistryKeyKind.HKEY_LOCAL_MACHINE) != 0;
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "ShowMacroDeletePopup", value ? 1 : 0, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x0000816E File Offset: 0x0000636E
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x00008191 File Offset: 0x00006391
		public bool ShowSchemeDeletePopup
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "ShowSchemeDeletePopup", 1, RegistryKeyKind.HKEY_LOCAL_MACHINE) != 0;
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "ShowSchemeDeletePopup", value ? 1 : 0, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x000081B2 File Offset: 0x000063B2
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x000081D6 File Offset: 0x000063D6
		public bool TouchSoundEnabled
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "TouchSoundEnabled", 1, RegistryKeyKind.HKEY_LOCAL_MACHINE) == 1;
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "TouchSoundEnabled", value ? 1 : 0, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x000081F7 File Offset: 0x000063F7
		// (set) Token: 0x06000900 RID: 2304 RVA: 0x0000821B File Offset: 0x0000641B
		public bool ShowBlueHighlighter
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "ShowBlueHighlighter", 1, RegistryKeyKind.HKEY_LOCAL_MACHINE) == 1;
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "ShowBlueHighlighter", value ? 1 : 0, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x0000823C File Offset: 0x0000643C
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x0000825F File Offset: 0x0000645F
		public bool IsFreeFireInGameSettingsCustomized
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "IsFreeFireInGameSettingsCustomized", 1, RegistryKeyKind.HKEY_LOCAL_MACHINE) != 0;
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "IsFreeFireInGameSettingsCustomized", value ? 1 : 0, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00008280 File Offset: 0x00006480
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x000082A3 File Offset: 0x000064A3
		public bool IsClientOnTop
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "IsClientOnTop", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE) != 0;
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "IsClientOnTop", value ? 1 : 0, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x000082C4 File Offset: 0x000064C4
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x000082F1 File Offset: 0x000064F1
		public ASTCOption ASTCOption
		{
			get
			{
				return (ASTCOption)RegistryUtils.GetRegistryValue(this.AndroidKeyPath, "ASTCOption", FeatureManager.Instance.IsCustomUIForNCSoft ? ASTCOption.SoftwareDecodingCache : ASTCOption.Disabled, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.AndroidKeyPath, "ASTCOption", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x0000830C File Offset: 0x0000650C
		// (set) Token: 0x06000908 RID: 2312 RVA: 0x0000832F File Offset: 0x0000652F
		public bool IsHardwareAstcSupported
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.AndroidKeyPath, "IsHardwareAstcSupported", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE) != 0;
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.AndroidKeyPath, "IsHardwareAstcSupported", value ? 1 : 0, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00008350 File Offset: 0x00006550
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x0000836E File Offset: 0x0000656E
		public NativeGamepadState NativeGamepadState
		{
			get
			{
				return (NativeGamepadState)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "NativeGamepadState", NativeGamepadState.Auto, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "NativeGamepadState", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00008389 File Offset: 0x00006589
		// (set) Token: 0x0600090C RID: 2316 RVA: 0x000083AC File Offset: 0x000065AC
		public bool IsShowMinimizeBlueStacksPopupOnClose
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "IsShowMinimizeBlueStacksPopupOnClose", 1, RegistryKeyKind.HKEY_LOCAL_MACHINE) != 0;
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "IsShowMinimizeBlueStacksPopupOnClose", value ? 1 : 0, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x000083CD File Offset: 0x000065CD
		// (set) Token: 0x0600090E RID: 2318 RVA: 0x000083F0 File Offset: 0x000065F0
		public bool IsMinimizeSelectedOnReceiveGameNotificationPopup
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "IsMinimizeSelectedOnReceiveGameNotificationPopup", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE) != 0;
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "IsMinimizeSelectedOnReceiveGameNotificationPopup", value ? 1 : 0, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x00008411 File Offset: 0x00006611
		// (set) Token: 0x06000910 RID: 2320 RVA: 0x0000842F File Offset: 0x0000662F
		public int NotificationModePopupShownCount
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "NotificationModePopupShownCount", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "NotificationModePopupShownCount", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x0000844A File Offset: 0x0000664A
		// (set) Token: 0x06000912 RID: 2322 RVA: 0x00008467 File Offset: 0x00006667
		public string LastNotificationEnabledAppLaunched
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mVmConfigKeyPath, "LastNotificationEnabledAppLaunched", string.Empty, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mVmConfigKeyPath, "LastNotificationEnabledAppLaunched", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x0000847D File Offset: 0x0000667D
		// (set) Token: 0x06000914 RID: 2324 RVA: 0x00008496 File Offset: 0x00006696
		public string[] NetworkInboundRules
		{
			get
			{
				return (string[])RegistryUtils.GetRegistryValue(this.mNetwork0KeyPath, "InboundRules", null, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mNetwork0KeyPath, "InboundRules", value, RegistryValueKind.MultiString, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x000084AC File Offset: 0x000066AC
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x000084C5 File Offset: 0x000066C5
		public string AllowRemoteAccess
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mNetwork0KeyPath, "AllowRemoteAccess", null, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mNetwork0KeyPath, "AllowRemoteAccess", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x000084DB File Offset: 0x000066DB
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x000084FD File Offset: 0x000066FD
		public int NetworkRedirectTcp5555
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mNetworkRedirectKeyPath, "tcp/5555", 5555, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mNetworkRedirectKeyPath, "tcp/5555", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000919 RID: 2329 RVA: 0x00008518 File Offset: 0x00006718
		// (set) Token: 0x0600091A RID: 2330 RVA: 0x0000853A File Offset: 0x0000673A
		public int NetworkRedirectTcp6666
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mNetworkRedirectKeyPath, "tcp/6666", 6666, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mNetworkRedirectKeyPath, "tcp/6666", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600091B RID: 2331 RVA: 0x00008555 File Offset: 0x00006755
		// (set) Token: 0x0600091C RID: 2332 RVA: 0x00008577 File Offset: 0x00006777
		public int NetworkRedirectTcp7777
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mNetworkRedirectKeyPath, "tcp/7777", 7777, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mNetworkRedirectKeyPath, "tcp/7777", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600091D RID: 2333 RVA: 0x00008592 File Offset: 0x00006792
		// (set) Token: 0x0600091E RID: 2334 RVA: 0x000085B4 File Offset: 0x000067B4
		public int NetworkRedirectTcp9999
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mNetworkRedirectKeyPath, "tcp/9999", 8888, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mNetworkRedirectKeyPath, "tcp/9999", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x0600091F RID: 2335 RVA: 0x000085CF File Offset: 0x000067CF
		// (set) Token: 0x06000920 RID: 2336 RVA: 0x000085F1 File Offset: 0x000067F1
		public int NetworkRedirectUdp12000
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mNetworkRedirectKeyPath, "udp/12000", 12000, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mNetworkRedirectKeyPath, "udp/12000", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0000860C File Offset: 0x0000680C
		// (set) Token: 0x06000922 RID: 2338 RVA: 0x00008629 File Offset: 0x00006829
		public string SharedFolder0Name
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mSharedFolder0KeyPath, "Name", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mSharedFolder0KeyPath, "Name", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x0000863F File Offset: 0x0000683F
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x0000865C File Offset: 0x0000685C
		public string SharedFolder0Path
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mSharedFolder0KeyPath, "Path", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mSharedFolder0KeyPath, "Path", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x00008672 File Offset: 0x00006872
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x00008690 File Offset: 0x00006890
		public int SharedFolder0Writable
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mSharedFolder0KeyPath, "Writable", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mSharedFolder0KeyPath, "Writable", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x000086AB File Offset: 0x000068AB
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x000086C8 File Offset: 0x000068C8
		public string SharedFolder1Name
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mSharedFolder1KeyPath, "Name", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mSharedFolder1KeyPath, "Name", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x000086DE File Offset: 0x000068DE
		// (set) Token: 0x0600092A RID: 2346 RVA: 0x000086FB File Offset: 0x000068FB
		public string SharedFolder1Path
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mSharedFolder1KeyPath, "Path", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mSharedFolder1KeyPath, "Path", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x00008711 File Offset: 0x00006911
		// (set) Token: 0x0600092C RID: 2348 RVA: 0x0000872F File Offset: 0x0000692F
		public int SharedFolder1Writable
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mSharedFolder1KeyPath, "Writable", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mSharedFolder1KeyPath, "Writable", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x0000874A File Offset: 0x0000694A
		// (set) Token: 0x0600092E RID: 2350 RVA: 0x00008767 File Offset: 0x00006967
		public string SharedFolder2Name
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mSharedFolder2KeyPath, "Name", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mSharedFolder2KeyPath, "Name", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x0000877D File Offset: 0x0000697D
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x0000879A File Offset: 0x0000699A
		public string SharedFolder2Path
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mSharedFolder2KeyPath, "Path", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mSharedFolder2KeyPath, "Path", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x000087B0 File Offset: 0x000069B0
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x000087CE File Offset: 0x000069CE
		public int SharedFolder2Writable
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mSharedFolder2KeyPath, "Writable", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mSharedFolder2KeyPath, "Writable", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x000087E9 File Offset: 0x000069E9
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x00008806 File Offset: 0x00006A06
		public string SharedFolder3Name
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mSharedFolder3KeyPath, "Name", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mSharedFolder3KeyPath, "Name", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x0000881C File Offset: 0x00006A1C
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x00008839 File Offset: 0x00006A39
		public string SharedFolder3Path
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mSharedFolder3KeyPath, "Path", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mSharedFolder3KeyPath, "Path", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x0000884F File Offset: 0x00006A4F
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x0000886D File Offset: 0x00006A6D
		public int SharedFolder3Writable
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mSharedFolder3KeyPath, "Writable", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mSharedFolder3KeyPath, "Writable", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x00008888 File Offset: 0x00006A88
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x000088A5 File Offset: 0x00006AA5
		public string SharedFolder4Name
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mSharedFolder4KeyPath, "Name", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mSharedFolder4KeyPath, "Name", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x000088BB File Offset: 0x00006ABB
		// (set) Token: 0x0600093C RID: 2364 RVA: 0x000088D8 File Offset: 0x00006AD8
		public string SharedFolder4Path
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mSharedFolder4KeyPath, "Path", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mSharedFolder4KeyPath, "Path", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x000088EE File Offset: 0x00006AEE
		// (set) Token: 0x0600093E RID: 2366 RVA: 0x0000890C File Offset: 0x00006B0C
		public int SharedFolder4Writable
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mSharedFolder4KeyPath, "Writable", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mSharedFolder4KeyPath, "Writable", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x00008927 File Offset: 0x00006B27
		// (set) Token: 0x06000940 RID: 2368 RVA: 0x00008944 File Offset: 0x00006B44
		public string SharedFolder5Name
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mSharedFolder5KeyPath, "Name", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mSharedFolder5KeyPath, "Name", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000941 RID: 2369 RVA: 0x0000895A File Offset: 0x00006B5A
		// (set) Token: 0x06000942 RID: 2370 RVA: 0x00008977 File Offset: 0x00006B77
		public string SharedFolder5Path
		{
			get
			{
				return (string)RegistryUtils.GetRegistryValue(this.mSharedFolder5KeyPath, "Path", "", RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mSharedFolder5KeyPath, "Path", value, RegistryValueKind.String, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x0000898D File Offset: 0x00006B8D
		// (set) Token: 0x06000944 RID: 2372 RVA: 0x000089AB File Offset: 0x00006BAB
		public int SharedFolder5Writable
		{
			get
			{
				return (int)RegistryUtils.GetRegistryValue(this.mSharedFolder5KeyPath, "Writable", 0, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
			set
			{
				RegistryUtils.SetRegistryValue(this.mSharedFolder5KeyPath, "Writable", value, RegistryValueKind.DWord, RegistryKeyKind.HKEY_LOCAL_MACHINE);
			}
		}

		// Token: 0x04000496 RID: 1174
		private string mVmId;

		// Token: 0x04000497 RID: 1175
		private string mBaseKeyPath = "";

		// Token: 0x04000498 RID: 1176
		private string mBlockDeviceKeyPath = "";

		// Token: 0x04000499 RID: 1177
		private string mBlockDevice0KeyPath = "";

		// Token: 0x0400049A RID: 1178
		private string mBlockDevice1KeyPath = "";

		// Token: 0x0400049B RID: 1179
		private string mBlockDevice2KeyPath = "";

		// Token: 0x0400049C RID: 1180
		private string mBlockDevice3KeyPath = "";

		// Token: 0x0400049D RID: 1181
		private string mBlockDevice4KeyPath = "";

		// Token: 0x0400049E RID: 1182
		private string mVmConfigKeyPath = "";

		// Token: 0x0400049F RID: 1183
		private string mFrameBufferKeyPath = "";

		// Token: 0x040004A0 RID: 1184
		private string mFrameBuffer0KeyPath = "";

		// Token: 0x040004A1 RID: 1185
		private string mNetworkKeyPath = "";

		// Token: 0x040004A2 RID: 1186
		private string mNetwork0KeyPath = "";

		// Token: 0x040004A3 RID: 1187
		private string mNetworkRedirectKeyPath = "";

		// Token: 0x040004A4 RID: 1188
		private string mSharedFolderKeyPath = "";

		// Token: 0x040004A5 RID: 1189
		private string mSharedFolder0KeyPath = "";

		// Token: 0x040004A6 RID: 1190
		private string mSharedFolder1KeyPath = "";

		// Token: 0x040004A7 RID: 1191
		private string mSharedFolder2KeyPath = "";

		// Token: 0x040004A8 RID: 1192
		private string mSharedFolder3KeyPath = "";

		// Token: 0x040004A9 RID: 1193
		private string mSharedFolder4KeyPath = "";

		// Token: 0x040004AA RID: 1194
		private string mSharedFolder5KeyPath = "";
	}
}
