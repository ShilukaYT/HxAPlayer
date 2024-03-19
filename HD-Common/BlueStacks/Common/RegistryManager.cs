using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace BlueStacks.Common
{
	// Token: 0x02000131 RID: 305
	public sealed class RegistryManager
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x00009324 File Offset: 0x00007524
		// (set) Token: 0x06000A7B RID: 2683 RVA: 0x0000932B File Offset: 0x0000752B
		public static string UPGRADE_TAG
		{
			get
			{
				return RegistryManager.mUPGRADE_TAG;
			}
			set
			{
				RegistryManager.ClearRegistryMangerInstance();
				RegistryManager.mUPGRADE_TAG = value;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0002ADA0 File Offset: 0x00028FA0
		// (set) Token: 0x06000A7D RID: 2685 RVA: 0x00009338 File Offset: 0x00007538
		public static RegistryManager Instance
		{
			get
			{
				if (RegistryManager.sInstance == null)
				{
					object obj = RegistryManager.sLock;
					lock (obj)
					{
						if (RegistryManager.sInstance == null)
						{
							RegistryManager registryManager = new RegistryManager();
							registryManager.mIsAdmin = SystemUtils.IsAdministrator();
							registryManager.Init("bgp");
							RegistryManager.sInstance = registryManager;
							if (RegistryManager.RegistryManagers.ContainsKey("bgp"))
							{
								RegistryManager.RegistryManagers["bgp"] = RegistryManager.sInstance;
							}
						}
					}
				}
				return RegistryManager.sInstance;
			}
			set
			{
				RegistryManager.sInstance = value;
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0002AE2C File Offset: 0x0002902C
		public static void SetRegistryManagers(List<string> oems)
		{
			if (oems == null || oems.Count == 0)
			{
				oems = new List<string>
				{
					"bgp"
				};
			}
			object obj = RegistryManager.sLock;
			lock (obj)
			{
				Dictionary<string, RegistryManager> dictionary = new Dictionary<string, RegistryManager>();
				foreach (string text in oems)
				{
					RegistryManager registryManager = new RegistryManager
					{
						mIsAdmin = SystemUtils.IsAdministrator()
					};
					registryManager.Init(text);
					dictionary.Add(text, registryManager);
				}
				RegistryManager.RegistryManagers = dictionary;
			}
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002AEE0 File Offset: 0x000290E0
		public static bool CheckOemInRegistry(string oemToCheck, string vmId)
		{
			bool flag = false;
			if (!string.IsNullOrEmpty(oemToCheck))
			{
				string str = oemToCheck.Equals("bgp", StringComparison.InvariantCultureIgnoreCase) ? "" : ("_" + oemToCheck);
				string text = "Software\\BlueStacks" + str + RegistryManager.UPGRADE_TAG;
				RegistryKey registryKey;
				if (!string.IsNullOrEmpty(vmId))
				{
					string name = text + "\\Guests\\" + vmId;
					registryKey = Registry.LocalMachine.OpenSubKey(name);
				}
				else
				{
					registryKey = Registry.LocalMachine.OpenSubKey(text);
				}
				flag = (registryKey != null);
				if (flag)
				{
					registryKey.Close();
				}
			}
			return flag;
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x00009340 File Offset: 0x00007540
		// (set) Token: 0x06000A81 RID: 2689 RVA: 0x00009368 File Offset: 0x00007568
		public static Dictionary<string, RegistryManager> RegistryManagers
		{
			get
			{
				if (RegistryManager._RegistryManagers == null)
				{
					RegistryManager._RegistryManagers = new Dictionary<string, RegistryManager>
					{
						{
							"bgp",
							RegistryManager.Instance
						}
					};
				}
				return RegistryManager._RegistryManagers;
			}
			set
			{
				RegistryManager._RegistryManagers = value;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00009370 File Offset: 0x00007570
		public Dictionary<string, InstanceRegistry> Guest { get; } = new Dictionary<string, InstanceRegistry>();

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00009378 File Offset: 0x00007578
		public InstanceRegistry DefaultGuest
		{
			get
			{
				return this.Guest[Strings.CurrentDefaultVmName];
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0000938A File Offset: 0x0000758A
		// (set) Token: 0x06000A85 RID: 2693 RVA: 0x00009392 File Offset: 0x00007592
		public string BaseKeyPath { get; private set; } = "";

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0000939B File Offset: 0x0000759B
		// (set) Token: 0x06000A87 RID: 2695 RVA: 0x000093A3 File Offset: 0x000075A3
		public string ClientBaseKeyPath { get; private set; } = "";

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x000093AC File Offset: 0x000075AC
		// (set) Token: 0x06000A89 RID: 2697 RVA: 0x000093B4 File Offset: 0x000075B4
		public string BTVKeyPath { get; private set; } = "";

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x000093BD File Offset: 0x000075BD
		// (set) Token: 0x06000A8B RID: 2699 RVA: 0x000093C5 File Offset: 0x000075C5
		public string HostConfigKeyPath { get; private set; } = "";

		// Token: 0x06000A8C RID: 2700 RVA: 0x0002AF6C File Offset: 0x0002916C
		private RegistryManager()
		{
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x000093CE File Offset: 0x000075CE
		private RegistryKey InitKeyWithSecurityCheck(string keyName)
		{
			if (!this.mIsAdmin)
			{
				return Registry.LocalMachine.OpenSubKey(keyName);
			}
			return Registry.LocalMachine.CreateSubKey(keyName);
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x000093EF File Offset: 0x000075EF
		public static void ClearRegistryMangerInstance()
		{
			RegistryManager.sInstance = null;
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x0002AFCC File Offset: 0x000291CC
		private void Init(string oem = "bgp")
		{
			string str = oem.Equals("bgp", StringComparison.InvariantCultureIgnoreCase) ? "" : ("_" + oem);
			this.BaseKeyPath = "Software\\BlueStacks" + str + RegistryManager.UPGRADE_TAG;
			this.HostConfigKeyPath = this.BaseKeyPath + "\\Config";
			this.ClientBaseKeyPath = this.BaseKeyPath + "\\Client";
			this.BTVKeyPath = this.BaseKeyPath + "\\BTV";
			this.mBaseKey = this.InitKeyWithSecurityCheck(this.BaseKeyPath);
			this.mBTVKey = RegistryUtils.InitKey(this.BaseKeyPath + "\\BTV");
			this.mBTVFilterKey = RegistryUtils.InitKey(this.BaseKeyPath + "\\BTV\\Filters");
			this.mClientKey = RegistryUtils.InitKey(this.BaseKeyPath + "\\Client");
			this.mUserKey = RegistryUtils.InitKey(this.BaseKeyPath + "\\User");
			this.mHostConfigKey = RegistryUtils.InitKey(this.BaseKeyPath + "\\Config");
			this.mGuestsKey = RegistryUtils.InitKey(this.BaseKeyPath + "\\Guests");
			this.mMonitorsKey = RegistryUtils.InitKey(this.BaseKeyPath + "\\Monitors");
			if (this.mClientKey == null)
			{
				if (SystemUtils.IsOs64Bit())
				{
					this.mClientKey = RegistryUtils.InitKey("Software\\Wow6432Node\\BlueStacksGP");
				}
				else
				{
					this.mClientKey = RegistryUtils.InitKey("Software\\BlueStacksGP");
				}
			}
			foreach (string text in this.VmList)
			{
				this.Guest[text] = new InstanceRegistry(text, oem);
			}
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0002B180 File Offset: 0x00029380
		public static void InitVmKeysForInstaller(List<string> vmList)
		{
			if (RegistryManager.sInstance == null)
			{
				RegistryManager.sInstance = new RegistryManager
				{
					mIsAdmin = SystemUtils.IsAdministrator()
				};
				RegistryManager.sInstance.Init("bgp");
				if (RegistryManager.RegistryManagers.ContainsKey("bgp"))
				{
					RegistryManager.RegistryManagers["bgp"] = RegistryManager.sInstance;
				}
			}
			if (vmList != null)
			{
				foreach (string text in vmList)
				{
					if (!RegistryManager.sInstance.Guest.ContainsKey(text))
					{
						RegistryManager.sInstance.Guest[text] = new InstanceRegistry(text, "bgp");
					}
				}
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0002B248 File Offset: 0x00029448
		public void SetAccessPermissions()
		{
			RegistryUtils.GrantAllAccessPermission(this.mHostConfigKey);
			RegistryUtils.GrantAllAccessPermission(this.mUserKey);
			RegistryUtils.GrantAllAccessPermission(this.mBTVKey);
			RegistryUtils.GrantAllAccessPermission(this.mClientKey);
			RegistryUtils.GrantAllAccessPermission(this.mGuestsKey);
			RegistryUtils.GrantAllAccessPermission(this.mMonitorsKey);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0002B298 File Offset: 0x00029498
		public bool DeleteAndroidSubKey(string vmName)
		{
			try
			{
				string subkey = string.Format(CultureInfo.InvariantCulture, "{0}\\Guests\\{1}", new object[]
				{
					this.BaseKeyPath,
					vmName
				});
				Registry.LocalMachine.DeleteSubKeyTree(subkey);
				this.Guest.Remove(vmName);
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000A93 RID: 2707 RVA: 0x000093F7 File Offset: 0x000075F7
		// (set) Token: 0x06000A94 RID: 2708 RVA: 0x0000941C File Offset: 0x0000761C
		public string[] VmList
		{
			get
			{
				return (string[])this.mHostConfigKey.GetValue("VmList", new string[]
				{
					"Android"
				});
			}
			set
			{
				this.mHostConfigKey.SetValue("VmList", value, RegistryValueKind.MultiString);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000A95 RID: 2709 RVA: 0x0000943B File Offset: 0x0000763B
		// (set) Token: 0x06000A96 RID: 2710 RVA: 0x00009458 File Offset: 0x00007658
		public string[] UpgradeVersionList
		{
			get
			{
				return (string[])this.mHostConfigKey.GetValue("UpgradeVersionList", new string[0]);
			}
			set
			{
				this.mHostConfigKey.SetValue("UpgradeVersionList", value, RegistryValueKind.MultiString);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x00009477 File Offset: 0x00007677
		// (set) Token: 0x06000A98 RID: 2712 RVA: 0x00009499 File Offset: 0x00007699
		public bool IsShootingModeTooltipVisible
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("IsShootingModeTooltipVisible", 1) != 0;
			}
			set
			{
				this.mHostConfigKey.SetValue("IsShootingModeTooltipVisible", (!value) ? 0 : 1);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x000094C2 File Offset: 0x000076C2
		// (set) Token: 0x06000A9A RID: 2714 RVA: 0x000094E4 File Offset: 0x000076E4
		public bool KeyMappingAvailablePromptEnabled
		{
			get
			{
				return (int)this.mClientKey.GetValue("KeyMappingAvailablePromptEnabled", 1) != 0;
			}
			set
			{
				this.mClientKey.SetValue("KeyMappingAvailablePromptEnabled", (!value) ? 0 : 1);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0000950D File Offset: 0x0000770D
		// (set) Token: 0x06000A9C RID: 2716 RVA: 0x00009533 File Offset: 0x00007733
		public bool ForceDedicatedGPU
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("ForceDedicatedGPU", Strings.ForceDedicatedGPUDefaultValue) != 0;
			}
			set
			{
				this.mHostConfigKey.SetValue("ForceDedicatedGPU", (!value) ? 0 : 1);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0000955C File Offset: 0x0000775C
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x00009578 File Offset: 0x00007778
		public string AvailableGPUDetails
		{
			get
			{
				return (string)this.mHostConfigKey.GetValue("AvailableGPUDetails", "");
			}
			set
			{
				this.mHostConfigKey.SetValue("AvailableGPUDetails", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x00009596 File Offset: 0x00007796
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x000095B8 File Offset: 0x000077B8
		public bool OverlayAvailablePromptEnabled
		{
			get
			{
				return (int)this.mClientKey.GetValue("OverlayAvailablePromptEnabled", 0) != 0;
			}
			set
			{
				this.mClientKey.SetValue("OverlayAvailablePromptEnabled", (!value) ? 0 : 1);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x000095E1 File Offset: 0x000077E1
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x00009603 File Offset: 0x00007803
		public bool DisableImageDetection
		{
			get
			{
				return (int)this.mClientKey.GetValue("DisableImageDetection", 0) != 0;
			}
			set
			{
				this.mClientKey.SetValue("DisableImageDetection", (!value) ? 0 : 1);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0000962C File Offset: 0x0000782C
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x0000964E File Offset: 0x0000784E
		public bool ShowKeyControlsOverlay
		{
			get
			{
				return (int)this.mClientKey.GetValue("ShowKeyControlsOverlay", 0) != 0;
			}
			set
			{
				this.mClientKey.SetValue("ShowKeyControlsOverlay", (!value) ? 0 : 1);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x00009677 File Offset: 0x00007877
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x00009699 File Offset: 0x00007899
		public bool TranslucentControlsEnabled
		{
			get
			{
				return (int)this.mClientKey.GetValue("TranslucentControlsEnabled", 0) != 0;
			}
			set
			{
				this.mClientKey.SetValue("TranslucentControlsEnabled", (!value) ? 0 : 1);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x0002B2FC File Offset: 0x000294FC
		// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x000096C2 File Offset: 0x000078C2
		public double TranslucentControlsTransparency
		{
			get
			{
				return double.Parse((string)this.mClientKey.GetValue("TranslucentControlsTransparency", 0.8.ToString(CultureInfo.InvariantCulture)), CultureInfo.InvariantCulture);
			}
			set
			{
				this.mClientKey.SetValue("TranslucentControlsTransparency", value.ToString(CultureInfo.InvariantCulture));
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x000096EB File Offset: 0x000078EB
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x0000970E File Offset: 0x0000790E
		public bool ShowGamingSummary
		{
			get
			{
				return (int)this.mClientKey.GetValue("ShowGamingSummary", 1) == 1;
			}
			set
			{
				this.mClientKey.SetValue("ShowGamingSummary", value ? 1 : 0);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x00009737 File Offset: 0x00007937
		// (set) Token: 0x06000AAC RID: 2732 RVA: 0x0000975A File Offset: 0x0000795A
		public bool DiscordEnabled
		{
			get
			{
				return (int)this.mClientKey.GetValue("DiscordEnabled", 1) == 1;
			}
			set
			{
				this.mClientKey.SetValue("DiscordEnabled", value ? 1 : 0);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x00009783 File Offset: 0x00007983
		// (set) Token: 0x06000AAE RID: 2734 RVA: 0x000097A6 File Offset: 0x000079A6
		public bool CustomCursorEnabled
		{
			get
			{
				return (int)this.mClientKey.GetValue("CustomCursorEnabled", 1) == 1;
			}
			set
			{
				this.mClientKey.SetValue("CustomCursorEnabled", value ? 1 : 0);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x000097CF File Offset: 0x000079CF
		// (set) Token: 0x06000AB0 RID: 2736 RVA: 0x000097F2 File Offset: 0x000079F2
		public bool GamepadDetectionEnabled
		{
			get
			{
				return (int)this.mClientKey.GetValue("GamepadDetectionEnabled", 1) == 1;
			}
			set
			{
				this.mClientKey.SetValue("GamepadDetectionEnabled", value ? 1 : 0);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x0000981B File Offset: 0x00007A1B
		// (set) Token: 0x06000AB2 RID: 2738 RVA: 0x0000983D File Offset: 0x00007A3D
		public List<string> IgnoreAutoPlayPackageList
		{
			get
			{
				return ((string[])this.mClientKey.GetValue("ShownVideoOnFirstLaunchPackageList", new string[0])).ToList<string>();
			}
			set
			{
				this.mClientKey.SetValue("ShownVideoOnFirstLaunchPackageList", (value != null) ? value.ToArray() : null, RegistryValueKind.MultiString);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x00009867 File Offset: 0x00007A67
		// (set) Token: 0x06000AB4 RID: 2740 RVA: 0x00009889 File Offset: 0x00007A89
		public bool UpdateBstConfig
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("UpdateBstConfig", 1) != 0;
			}
			set
			{
				this.mHostConfigKey.SetValue("UpdateBstConfig", (!value) ? 0 : 1);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x000098B2 File Offset: 0x00007AB2
		// (set) Token: 0x06000AB6 RID: 2742 RVA: 0x000098D4 File Offset: 0x00007AD4
		public bool IsEcoModeBlurbShown
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("IsEcoModeBlurbShown", 0) != 0;
			}
			set
			{
				this.mHostConfigKey.SetValue("IsEcoModeBlurbShown", (!value) ? 0 : 1);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x000098FD File Offset: 0x00007AFD
		// (set) Token: 0x06000AB8 RID: 2744 RVA: 0x0000991F File Offset: 0x00007B1F
		public bool IsGameTvEnabled
		{
			get
			{
				return (int)this.mClientKey.GetValue("IsGameTvEnabled", 0) != 0;
			}
			set
			{
				this.mClientKey.SetValue("IsGameTvEnabled", (!value) ? 0 : 1);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x00009948 File Offset: 0x00007B48
		// (set) Token: 0x06000ABA RID: 2746 RVA: 0x00009965 File Offset: 0x00007B65
		public int OnboardingBlurbShownCount
		{
			get
			{
				return (int)this.mClientKey.GetValue("OnboardingBlurbShownCount", 0);
			}
			set
			{
				this.mClientKey.SetValue("OnboardingBlurbShownCount", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x00009988 File Offset: 0x00007B88
		// (set) Token: 0x06000ABC RID: 2748 RVA: 0x000099A6 File Offset: 0x00007BA6
		public int CommonFPS
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("CommonFPS", 60);
			}
			set
			{
				this.mHostConfigKey.SetValue("CommonFPS", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000ABD RID: 2749 RVA: 0x000099C9 File Offset: 0x00007BC9
		// (set) Token: 0x06000ABE RID: 2750 RVA: 0x000099E7 File Offset: 0x00007BE7
		public int TrimMemoryDuration
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("TrimMemoryDuration", 15);
			}
			set
			{
				this.mHostConfigKey.SetValue("TrimMemoryDuration", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x00009A0A File Offset: 0x00007C0A
		public int DevEnv
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("DevEnv", 0);
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00009A27 File Offset: 0x00007C27
		// (set) Token: 0x06000AC1 RID: 2753 RVA: 0x00009A44 File Offset: 0x00007C44
		public int ArrangeWindowMode
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("ArrangeWindowModeConfig", 0);
			}
			set
			{
				this.mHostConfigKey.SetValue("ArrangeWindowModeConfig", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00009A67 File Offset: 0x00007C67
		// (set) Token: 0x06000AC3 RID: 2755 RVA: 0x00009A8E File Offset: 0x00007C8E
		public long TileWindowColumnCount
		{
			get
			{
				return long.Parse(this.mHostConfigKey.GetValue("TileWindowColumnCount", 2).ToString(), CultureInfo.InvariantCulture);
			}
			set
			{
				this.mHostConfigKey.SetValue("TileWindowColumnCount", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x00009AB1 File Offset: 0x00007CB1
		// (set) Token: 0x06000AC5 RID: 2757 RVA: 0x00009AD3 File Offset: 0x00007CD3
		public bool ManageGooglePlayPromptEnabled
		{
			get
			{
				return (int)this.mClientKey.GetValue("ManageGooglePlayPromptEnabled", 1) != 0;
			}
			set
			{
				this.mClientKey.SetValue("ManageGooglePlayPromptEnabled", (!value) ? 0 : 1);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x00009AFC File Offset: 0x00007CFC
		// (set) Token: 0x06000AC7 RID: 2759 RVA: 0x00009B1F File Offset: 0x00007D1F
		public bool UseEscapeToExitFullScreen
		{
			get
			{
				return (int)this.mClientKey.GetValue("UseEscapeToExitFullScreen", 0) == 1;
			}
			set
			{
				this.mClientKey.SetValue("UseEscapeToExitFullScreen", value ? 1 : 0);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x00009B48 File Offset: 0x00007D48
		// (set) Token: 0x06000AC9 RID: 2761 RVA: 0x00009B6B File Offset: 0x00007D6B
		public bool IsVTXPopupEnable
		{
			get
			{
				return (int)this.mClientKey.GetValue("IsVTXPopupEnable", 1) == 1;
			}
			set
			{
				this.mClientKey.SetValue("IsVTXPopupEnable", value ? 1 : 0);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x00009B94 File Offset: 0x00007D94
		// (set) Token: 0x06000ACB RID: 2763 RVA: 0x00009BB1 File Offset: 0x00007DB1
		public int FrontendHeight
		{
			get
			{
				return (int)this.mClientKey.GetValue("FrontendHeight", 0);
			}
			set
			{
				this.mClientKey.SetValue("FrontendHeight", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x00009BD4 File Offset: 0x00007DD4
		// (set) Token: 0x06000ACD RID: 2765 RVA: 0x00009BF1 File Offset: 0x00007DF1
		public int FrontendWidth
		{
			get
			{
				return (int)this.mClientKey.GetValue("FrontendWidth", 0);
			}
			set
			{
				this.mClientKey.SetValue("FrontendWidth", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x00009C14 File Offset: 0x00007E14
		// (set) Token: 0x06000ACF RID: 2767 RVA: 0x00009C30 File Offset: 0x00007E30
		public string BossKey
		{
			get
			{
				return (string)this.mClientKey.GetValue("BossKey", "");
			}
			set
			{
				this.mClientKey.SetValue("BossKey", value);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000AD0 RID: 2768 RVA: 0x00009C43 File Offset: 0x00007E43
		// (set) Token: 0x06000AD1 RID: 2769 RVA: 0x00009C5F File Offset: 0x00007E5F
		public string CampaignMD5
		{
			get
			{
				return (string)this.mClientKey.GetValue("CampaignMD5", "");
			}
			set
			{
				this.mClientKey.SetValue("CampaignMD5", value);
				this.mClientKey.SetValue("FLECampaignMD5", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00009C8E File Offset: 0x00007E8E
		public string FLECampaignMD5
		{
			get
			{
				return (string)this.mClientKey.GetValue("FLECampaignMD5", string.Empty);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x00009CAA File Offset: 0x00007EAA
		// (set) Token: 0x06000AD4 RID: 2772 RVA: 0x00009CC6 File Offset: 0x00007EC6
		public string CampaignJson
		{
			get
			{
				return (string)this.mClientKey.GetValue("CampaignJson", "");
			}
			set
			{
				this.mClientKey.SetValue("CampaignJson", value);
				if (!string.IsNullOrEmpty(value))
				{
					this.DeleteFLECampaignMD5();
				}
				this.mClientKey.Flush();
			}
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x00009CF2 File Offset: 0x00007EF2
		public void DeleteFLECampaignMD5()
		{
			if (this.mClientKey.GetValue("FLECampaignMD5", null) != null)
			{
				this.mClientKey.DeleteValue("FLECampaignMD5");
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x00009D17 File Offset: 0x00007F17
		// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x00009D33 File Offset: 0x00007F33
		public string CDNAppsTimeStamp
		{
			get
			{
				return (string)this.mClientKey.GetValue("CDNAppsTimeStamp", "");
			}
			set
			{
				this.mClientKey.SetValue("CDNAppsTimeStamp", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x00009D51 File Offset: 0x00007F51
		// (set) Token: 0x06000AD9 RID: 2777 RVA: 0x00009D6D File Offset: 0x00007F6D
		public string SetupFolder
		{
			get
			{
				return (string)this.mClientKey.GetValue("SetupFolder", Strings.BlueStacksSetupFolder);
			}
			set
			{
				this.mClientKey.SetValue("SetupFolder", value);
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00009D80 File Offset: 0x00007F80
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x00009D9C File Offset: 0x00007F9C
		public string EngineDataDir
		{
			get
			{
				return (string)this.mClientKey.GetValue("EngineDataDir", "");
			}
			set
			{
				this.mClientKey.SetValue("EngineDataDir", value);
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00009DAF File Offset: 0x00007FAF
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x00009DCB File Offset: 0x00007FCB
		public string ClientInstallDir
		{
			get
			{
				return (string)this.mClientKey.GetValue("ClientInstallDir", "");
			}
			set
			{
				this.mClientKey.SetValue("ClientInstallDir", value);
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00009DDE File Offset: 0x00007FDE
		// (set) Token: 0x06000ADF RID: 2783 RVA: 0x00009DE5 File Offset: 0x00007FE5
		public static string ClientThemeName { get; set; } = "Assets";

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00009DED File Offset: 0x00007FED
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x00009E10 File Offset: 0x00008010
		public bool OpenThemeEditor
		{
			get
			{
				return (int)this.mClientKey.GetValue("OpenThemeEditor", 0) == 1;
			}
			set
			{
				this.mClientKey.SetValue("OpenThemeEditor", value ? 1 : 0);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x00009E39 File Offset: 0x00008039
		// (set) Token: 0x06000AE3 RID: 2787 RVA: 0x00009E55 File Offset: 0x00008055
		public string CefDataPath
		{
			get
			{
				return (string)this.mClientKey.GetValue("CefDataPath", string.Empty);
			}
			set
			{
				this.mClientKey.SetValue("CefDataPath", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x0002B340 File Offset: 0x00029540
		// (set) Token: 0x06000AE5 RID: 2789 RVA: 0x00009E73 File Offset: 0x00008073
		public string OfflineHtmlHomeUrl
		{
			get
			{
				string result = string.Empty;
				if (File.Exists(Path.Combine(this.ClientInstallDir, "OfflineHtmlHome\\offline.html")))
				{
					result = Path.Combine(this.ClientInstallDir, "OfflineHtmlHome\\offline.html");
				}
				if (string.IsNullOrEmpty((string)this.mClientKey.GetValue("OfflineHtmlHomeUrl", string.Empty)))
				{
					return result;
				}
				return (string)this.mClientKey.GetValue("OfflineHtmlHomeUrl", string.Empty);
			}
			set
			{
				this.mClientKey.SetValue("OfflineHtmlHomeUrl", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x00009E91 File Offset: 0x00008091
		// (set) Token: 0x06000AE7 RID: 2791 RVA: 0x00009EB4 File Offset: 0x000080B4
		public bool HomeHtmlErrorHandling
		{
			get
			{
				return (int)this.mClientKey.GetValue("HomeHtmlErrorHandling", 1) == 1;
			}
			set
			{
				this.mClientKey.SetValue("HomeHtmlErrorHandling", (!value) ? 0 : 1);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000AE8 RID: 2792 RVA: 0x00009EDD File Offset: 0x000080DD
		public int CefDevEnv
		{
			get
			{
				return (int)this.mClientKey.GetValue("CefDevEnv", 0);
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00009EFA File Offset: 0x000080FA
		public int CefDebugPort
		{
			get
			{
				return (int)this.mClientKey.GetValue("CefDebugPort", 0);
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000AEA RID: 2794 RVA: 0x00009F17 File Offset: 0x00008117
		// (set) Token: 0x06000AEB RID: 2795 RVA: 0x00009F38 File Offset: 0x00008138
		public int LastBootTime
		{
			get
			{
				return (int)this.mClientKey.GetValue("LastBootTime", 120000);
			}
			set
			{
				this.mClientKey.SetValue("LastBootTime", value);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00009F50 File Offset: 0x00008150
		// (set) Token: 0x06000AED RID: 2797 RVA: 0x00009F71 File Offset: 0x00008171
		public int AvgBootTime
		{
			get
			{
				return (int)this.mClientKey.GetValue("AvgBootTime", 20000);
			}
			set
			{
				this.mClientKey.SetValue("AvgBootTime", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x00009F94 File Offset: 0x00008194
		// (set) Token: 0x06000AEF RID: 2799 RVA: 0x00009FB1 File Offset: 0x000081B1
		public int NoOfBootCompleted
		{
			get
			{
				return (int)this.mClientKey.GetValue("NoOfBootCompleted", 0);
			}
			set
			{
				this.mClientKey.SetValue("NoOfBootCompleted", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00009FD4 File Offset: 0x000081D4
		// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x00009FF5 File Offset: 0x000081F5
		public int AvgHomeHtmlLoadTime
		{
			get
			{
				return (int)this.mClientKey.GetValue("AvgHomeHtmlLoadTime", 10000);
			}
			set
			{
				this.mClientKey.SetValue("AvgHomeHtmlLoadTime", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x0000A018 File Offset: 0x00008218
		// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x0000A03B File Offset: 0x0000823B
		public bool IsScreenshotsLocationPopupEnabled
		{
			get
			{
				return (int)this.mClientKey.GetValue("ScreenshotsLocationPopupEnabled", 1) == 1;
			}
			set
			{
				this.mClientKey.SetValue("ScreenshotsLocationPopupEnabled", (!value) ? 0 : 1);
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x0000A059 File Offset: 0x00008259
		// (set) Token: 0x06000AF5 RID: 2805 RVA: 0x0000A075 File Offset: 0x00008275
		public string ScreenShotsPath
		{
			get
			{
				return (string)this.mClientKey.GetValue("ScreenShotsPath", RegistryStrings.ScreenshotDefaultPath);
			}
			set
			{
				this.mClientKey.SetValue("ScreenShotsPath", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x0000A093 File Offset: 0x00008293
		// (set) Token: 0x06000AF7 RID: 2807 RVA: 0x0000A0B6 File Offset: 0x000082B6
		public bool RequirementConfigUpdateRequired
		{
			get
			{
				return (int)this.mClientKey.GetValue("RequirementConfigUpdateRequired", 0) == 1;
			}
			set
			{
				this.mClientKey.SetValue("RequirementConfigUpdateRequired", (!value) ? 0 : 1);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x0000A0DF File Offset: 0x000082DF
		public bool IsShowIconBorder
		{
			get
			{
				return (int)this.mClientKey.GetValue("ShowIconBorder", 0) == 1;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0000A102 File Offset: 0x00008302
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x0000A11E File Offset: 0x0000831E
		public string UserSelectedLocale
		{
			get
			{
				return (string)this.mClientKey.GetValue("UserSelectedLocale", "");
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					this.mClientKey.SetValue("UserSelectedLocale", value);
					this.mClientKey.Flush();
				}
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x0000A144 File Offset: 0x00008344
		public string TargetLocale
		{
			get
			{
				return (string)this.mClientKey.GetValue("TargetLocale", "");
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x0000A160 File Offset: 0x00008360
		public string TargetLocaleUrl
		{
			get
			{
				return (string)this.mClientKey.GetValue("TargetLocaleUrl", "");
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0000A17C File Offset: 0x0000837C
		// (set) Token: 0x06000AFE RID: 2814 RVA: 0x0000A198 File Offset: 0x00008398
		public string FailedUpgradeVersion
		{
			get
			{
				return (string)this.mClientKey.GetValue("FailedUpgradeVersion", "");
			}
			set
			{
				this.mClientKey.SetValue("FailedUpgradeVersion", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0000A1B6 File Offset: 0x000083B6
		// (set) Token: 0x06000B00 RID: 2816 RVA: 0x0000A1D2 File Offset: 0x000083D2
		public string LastUpdateSkippedVersion
		{
			get
			{
				return (string)this.mClientKey.GetValue("LastUpdateSkippedVersion", "");
			}
			set
			{
				this.mClientKey.SetValue("LastUpdateSkippedVersion", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x0000A1F0 File Offset: 0x000083F0
		// (set) Token: 0x06000B02 RID: 2818 RVA: 0x0000A20C File Offset: 0x0000840C
		public string Partner
		{
			get
			{
				return (string)this.mClientKey.GetValue("Partner", "");
			}
			set
			{
				this.mClientKey.SetValue("Partner", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x0000A22A File Offset: 0x0000842A
		// (set) Token: 0x06000B04 RID: 2820 RVA: 0x0000A246 File Offset: 0x00008446
		public string DownloadedUpdateFile
		{
			get
			{
				return (string)this.mClientKey.GetValue("DownloadedUpdateFile", "");
			}
			set
			{
				this.mClientKey.SetValue("DownloadedUpdateFile", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x0002B3B8 File Offset: 0x000295B8
		// (set) Token: 0x06000B06 RID: 2822 RVA: 0x0000A264 File Offset: 0x00008464
		public string ClientVersion
		{
			get
			{
				string text = (string)this.mBaseKey.GetValue("ClientVersion", "");
				if (string.IsNullOrEmpty(text))
				{
					text = (string)this.mClientKey.GetValue("ClientVersion", "");
				}
				return text;
			}
			set
			{
				this.mBaseKey.SetValue("ClientVersion", value);
				this.mBaseKey.Flush();
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x0000A282 File Offset: 0x00008482
		// (set) Token: 0x06000B08 RID: 2824 RVA: 0x0000A29F File Offset: 0x0000849F
		public int IsClientFirstLaunch
		{
			get
			{
				return (int)this.mClientKey.GetValue("IsClientFirstLaunch", 1);
			}
			set
			{
				this.mClientKey.SetValue("IsClientFirstLaunch", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x0000A2C2 File Offset: 0x000084C2
		// (set) Token: 0x06000B0A RID: 2826 RVA: 0x0000A2DF File Offset: 0x000084DF
		public int IsEngineUpgraded
		{
			get
			{
				return (int)this.mClientKey.GetValue("IsEngineUpgraded", 0);
			}
			set
			{
				this.mClientKey.SetValue("IsEngineUpgraded", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000B0B RID: 2827 RVA: 0x0000A302 File Offset: 0x00008502
		// (set) Token: 0x06000B0C RID: 2828 RVA: 0x0000A325 File Offset: 0x00008525
		public bool IsShowRibbonNotification
		{
			get
			{
				return (int)this.mClientKey.GetValue("IsShowRibbonNotification", 1) == 1;
			}
			set
			{
				this.mClientKey.SetValue("IsShowRibbonNotification", value ? 1 : 0);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x0000A34E File Offset: 0x0000854E
		// (set) Token: 0x06000B0E RID: 2830 RVA: 0x0000A371 File Offset: 0x00008571
		public bool IsShowToastNotification
		{
			get
			{
				return (int)this.mClientKey.GetValue("IsShowToastNotification", 1) == 1;
			}
			set
			{
				this.mClientKey.SetValue("IsShowToastNotification", value ? 1 : 0);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0000A39A File Offset: 0x0000859A
		// (set) Token: 0x06000B10 RID: 2832 RVA: 0x0000A3BD File Offset: 0x000085BD
		public bool IsShowGamepadDesktopNotification
		{
			get
			{
				return (int)this.mClientKey.GetValue("IsShowGamepadDesktopNotification", 1) == 1;
			}
			set
			{
				this.mClientKey.SetValue("IsShowGamepadDesktopNotification", value ? 1 : 0);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x0000A3E6 File Offset: 0x000085E6
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x0000A409 File Offset: 0x00008609
		public bool IsShowPromotionalTeaser
		{
			get
			{
				return (int)this.mClientKey.GetValue("IsShowPromotionalTeaser", 1) == 1;
			}
			set
			{
				this.mClientKey.SetValue("IsShowPromotionalTeaser", value ? 1 : 0);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x0000A432 File Offset: 0x00008632
		// (set) Token: 0x06000B14 RID: 2836 RVA: 0x0000A455 File Offset: 0x00008655
		public bool IsClientUpgraded
		{
			get
			{
				return (int)this.mClientKey.GetValue("IsClientUpgraded", 0) == 1;
			}
			set
			{
				this.mClientKey.SetValue("IsClientUpgraded", value ? 1 : 0);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x0000A47E File Offset: 0x0000867E
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x0000A49A File Offset: 0x0000869A
		public string AInfo
		{
			get
			{
				return (string)this.mClientKey.GetValue("AInfo", "");
			}
			set
			{
				this.mClientKey.SetValue("AInfo", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0000A4B8 File Offset: 0x000086B8
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x0000A4D4 File Offset: 0x000086D4
		public string BGPDevUrl
		{
			get
			{
				return (string)this.mClientKey.GetValue("BGPDevUrl", "");
			}
			set
			{
				this.mClientKey.SetValue("BGPDevUrl", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x0000A4F2 File Offset: 0x000086F2
		public string FriendsDevServer
		{
			get
			{
				return (string)this.mClientKey.GetValue("FriendsDevServer", "");
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0000A50E File Offset: 0x0000870E
		// (set) Token: 0x06000B1B RID: 2843 RVA: 0x0000A52A File Offset: 0x0000872A
		public string PromotionId
		{
			get
			{
				return (string)this.mClientKey.GetValue("PromotionId", "");
			}
			set
			{
				this.mClientKey.SetValue("PromotionId", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0000A548 File Offset: 0x00008748
		// (set) Token: 0x06000B1D RID: 2845 RVA: 0x0000A564 File Offset: 0x00008764
		public string DMMRecommendedWindowUrl
		{
			get
			{
				return (string)this.mClientKey.GetValue("RecommendedWindowUrl", "http://site-gameplayer.dmm.com/emulator-recommend");
			}
			set
			{
				this.mClientKey.SetValue("RecommendedWindowUrl", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000B1E RID: 2846 RVA: 0x0000A582 File Offset: 0x00008782
		// (set) Token: 0x06000B1F RID: 2847 RVA: 0x0000A59E File Offset: 0x0000879E
		public string DeviceProfileFromCloud
		{
			get
			{
				return (string)this.mClientKey.GetValue("DeviceProfileFromCloud", string.Empty);
			}
			set
			{
				this.mClientKey.SetValue("DeviceProfileFromCloud", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0000A5BC File Offset: 0x000087BC
		// (set) Token: 0x06000B21 RID: 2849 RVA: 0x0000A5D9 File Offset: 0x000087D9
		public int GlPlusTransportConfig
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("GlPlusTransportConfig", 3);
			}
			set
			{
				this.mHostConfigKey.SetValue("GlPlusTransportConfig", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0000A5FC File Offset: 0x000087FC
		// (set) Token: 0x06000B23 RID: 2851 RVA: 0x0000A619 File Offset: 0x00008819
		public int GlLegacyTransportConfig
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("GlLegacyTransportConfig", 0);
			}
			set
			{
				this.mHostConfigKey.SetValue("GlLegacyTransportConfig", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0000A63C File Offset: 0x0000883C
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x0000A671 File Offset: 0x00008871
		public string CurrentEngine
		{
			get
			{
				if (string.IsNullOrEmpty(this.sCurrentEngine))
				{
					this.sCurrentEngine = (string)this.mHostConfigKey.GetValue("CurrentEngine", "plus");
				}
				return this.sCurrentEngine;
			}
			set
			{
				this.sCurrentEngine = value;
				this.mHostConfigKey.SetValue("CurrentEngine", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0000A696 File Offset: 0x00008896
		// (set) Token: 0x06000B27 RID: 2855 RVA: 0x0000A6B2 File Offset: 0x000088B2
		public string EnginePreference
		{
			get
			{
				return (string)this.mHostConfigKey.GetValue("EnginePreference", "plus");
			}
			set
			{
				this.mHostConfigKey.SetValue("EnginePreference", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0000A6D0 File Offset: 0x000088D0
		// (set) Token: 0x06000B29 RID: 2857 RVA: 0x0000A6EC File Offset: 0x000088EC
		public string InstallDir
		{
			get
			{
				return (string)this.mBaseKey.GetValue("InstallDir", "");
			}
			set
			{
				this.mBaseKey.SetValue("InstallDir", value);
				this.mBaseKey.Flush();
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0000A70A File Offset: 0x0000890A
		// (set) Token: 0x06000B2B RID: 2859 RVA: 0x0000A72D File Offset: 0x0000892D
		public bool IsUpgrade
		{
			get
			{
				return (int)this.mBaseKey.GetValue("IsUpgrade", 0) == 1;
			}
			set
			{
				this.mBaseKey.SetValue("IsUpgrade", value ? 1 : 0);
				this.mBaseKey.Flush();
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0000A756 File Offset: 0x00008956
		// (set) Token: 0x06000B2D RID: 2861 RVA: 0x0000A772 File Offset: 0x00008972
		public string DataDir
		{
			get
			{
				return (string)this.mBaseKey.GetValue("DataDir", "");
			}
			set
			{
				this.mBaseKey.SetValue("DataDir", value);
				this.mBaseKey.Flush();
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0002B404 File Offset: 0x00029604
		// (set) Token: 0x06000B2F RID: 2863 RVA: 0x0000A790 File Offset: 0x00008990
		public string UserDefinedDir
		{
			get
			{
				if (this.sUserDefinedDir == null)
				{
					string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
					this.sUserDefinedDir = (string)this.mBaseKey.GetValue("UserDefinedDir", folderPath);
				}
				return this.sUserDefinedDir;
			}
			set
			{
				this.sUserDefinedDir = value;
				this.mBaseKey.SetValue("UserDefinedDir", value);
				this.mBaseKey.Flush();
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0002B444 File Offset: 0x00029644
		// (set) Token: 0x06000B31 RID: 2865 RVA: 0x0000A7B5 File Offset: 0x000089B5
		public string LogDir
		{
			get
			{
				string text = (string)this.mBaseKey.GetValue("LogDir", null);
				if (text == null)
				{
					text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Bluestacks\\Logs");
				}
				return text;
			}
			set
			{
				this.mBaseKey.SetValue("LogDir", value);
				this.mBaseKey.Flush();
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x0000A7D3 File Offset: 0x000089D3
		// (set) Token: 0x06000B33 RID: 2867 RVA: 0x0000A7F0 File Offset: 0x000089F0
		public int PlusDebug
		{
			get
			{
				return (int)this.mBaseKey.GetValue("PlusDebug", 0);
			}
			set
			{
				this.mBaseKey.SetValue("PlusDebug", value);
				this.mBaseKey.Flush();
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0000A813 File Offset: 0x00008A13
		// (set) Token: 0x06000B35 RID: 2869 RVA: 0x0000A84A File Offset: 0x00008A4A
		public string Version
		{
			get
			{
				if (this.sVersion != null)
				{
					return this.sVersion;
				}
				this.sVersion = (string)this.mBaseKey.GetValue("Version", "");
				return this.sVersion;
			}
			set
			{
				this.mBaseKey.SetValue("Version", value);
				this.mBaseKey.Flush();
				this.sVersion = value;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x0000A86F File Offset: 0x00008A6F
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x0000A88B File Offset: 0x00008A8B
		public string UserGuid
		{
			get
			{
				return (string)this.mBaseKey.GetValue("USER_GUID", "");
			}
			set
			{
				this.mBaseKey.SetValue("USER_GUID", value);
				this.mBaseKey.Flush();
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x0000A8A9 File Offset: 0x00008AA9
		// (set) Token: 0x06000B39 RID: 2873 RVA: 0x0000A8C5 File Offset: 0x00008AC5
		public string WebAppVersion
		{
			get
			{
				return (string)this.mClientKey.GetValue("WebAppVersion", string.Empty);
			}
			set
			{
				this.mClientKey.SetValue("WebAppVersion", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x0000A8E3 File Offset: 0x00008AE3
		// (set) Token: 0x06000B3B RID: 2875 RVA: 0x0000A8FF File Offset: 0x00008AFF
		public string InstanceSortOption
		{
			get
			{
				return (string)this.mClientKey.GetValue("InstanceSortOption", string.Empty);
			}
			set
			{
				this.mClientKey.SetValue("InstanceSortOption", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0000A91D File Offset: 0x00008B1D
		// (set) Token: 0x06000B3D RID: 2877 RVA: 0x0000A939 File Offset: 0x00008B39
		public string OpenExternalLink
		{
			get
			{
				return (string)this.mClientKey.GetValue("OpenExternalLink", string.Empty);
			}
			set
			{
				this.mClientKey.SetValue("OpenExternalLink", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x0000A957 File Offset: 0x00008B57
		// (set) Token: 0x06000B3F RID: 2879 RVA: 0x0000A973 File Offset: 0x00008B73
		public string ClientLaunchParams
		{
			get
			{
				return (string)this.mClientKey.GetValue("ClientLaunchParams", "");
			}
			set
			{
				this.mClientKey.SetValue("ClientLaunchParams", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x0000A991 File Offset: 0x00008B91
		// (set) Token: 0x06000B41 RID: 2881 RVA: 0x0000A9AD File Offset: 0x00008BAD
		public string ApiToken
		{
			get
			{
				return (string)this.mBaseKey.GetValue("ApiToken", "");
			}
			set
			{
				this.mBaseKey.SetValue("ApiToken", value);
				this.mBaseKey.Flush();
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000B42 RID: 2882 RVA: 0x0000A9CB File Offset: 0x00008BCB
		// (set) Token: 0x06000B43 RID: 2883 RVA: 0x0000A9EE File Offset: 0x00008BEE
		public bool IsBTVCheckedAfterUpdate
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("IsBTVCheckedAfterUpdate", 0) == 1;
			}
			set
			{
				this.mHostConfigKey.SetValue("IsBTVCheckedAfterUpdate", value ? 1 : 0);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x0000AA17 File Offset: 0x00008C17
		// (set) Token: 0x06000B45 RID: 2885 RVA: 0x0000AA33 File Offset: 0x00008C33
		public string CurrentBtvVersionInstalled
		{
			get
			{
				return (string)this.mHostConfigKey.GetValue("CurrentBtvVersionInstalled", "");
			}
			set
			{
				this.mHostConfigKey.SetValue("CurrentBtvVersionInstalled", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0002B480 File Offset: 0x00029680
		public bool IsFirstTimeCheck
		{
			get
			{
				bool result = (int)this.mHostConfigKey.GetValue("IsFirstTimeCheck", 1) == 1;
				this.mHostConfigKey.SetValue("IsFirstTimeCheck", 0);
				this.mHostConfigKey.Flush();
				return result;
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x0000AA51 File Offset: 0x00008C51
		// (set) Token: 0x06000B48 RID: 2888 RVA: 0x0000AA6E File Offset: 0x00008C6E
		public int SystemInfoStats2
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("SystemInfoStats2", 0);
			}
			set
			{
				this.mHostConfigKey.SetValue("SystemInfoStats2", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x0000AA91 File Offset: 0x00008C91
		// (set) Token: 0x06000B4A RID: 2890 RVA: 0x0000AAAE File Offset: 0x00008CAE
		public int Features
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("Features", 0);
			}
			set
			{
				this.mHostConfigKey.SetValue("Features", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000B4B RID: 2891 RVA: 0x0000AAD1 File Offset: 0x00008CD1
		// (set) Token: 0x06000B4C RID: 2892 RVA: 0x0000AAEE File Offset: 0x00008CEE
		public int FeaturesHigh
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("FeaturesHigh", 0);
			}
			set
			{
				this.mHostConfigKey.SetValue("FeaturesHigh", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x0000AB11 File Offset: 0x00008D11
		// (set) Token: 0x06000B4E RID: 2894 RVA: 0x0000AB2E File Offset: 0x00008D2E
		public int SystemStats
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("SystemStats", 0);
			}
			set
			{
				this.mHostConfigKey.SetValue("SystemStats", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x0000AB51 File Offset: 0x00008D51
		// (set) Token: 0x06000B50 RID: 2896 RVA: 0x0000AB6E File Offset: 0x00008D6E
		public int SendBotsCheckStats
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("SendBotsCheckStats", 0);
			}
			set
			{
				this.mHostConfigKey.SetValue("SendBotsCheckStats", 1);
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0000AB86 File Offset: 0x00008D86
		// (set) Token: 0x06000B52 RID: 2898 RVA: 0x0000ABA2 File Offset: 0x00008DA2
		public string BotsCheckStatsTime
		{
			get
			{
				return (string)this.mHostConfigKey.GetValue("BotsCheckStatsTime", "");
			}
			set
			{
				this.mHostConfigKey.SetValue("BotsCheckStatsTime", value);
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x0002B4D0 File Offset: 0x000296D0
		// (set) Token: 0x06000B54 RID: 2900 RVA: 0x0000ABB5 File Offset: 0x00008DB5
		public string Host
		{
			get
			{
				if (string.IsNullOrEmpty(this.mHost))
				{
					string value = (string)this.mHostConfigKey.GetValue("Host", "https://cloud.bluestacks.com");
					if (string.IsNullOrEmpty(value))
					{
						value = "https://cloud.bluestacks.com";
					}
					this.mHost = value;
				}
				return this.mHost;
			}
			set
			{
				this.mHostConfigKey.SetValue("Host", value);
				this.mHostConfigKey.Flush();
				this.mHost = value;
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0000ABDA File Offset: 0x00008DDA
		// (set) Token: 0x06000B56 RID: 2902 RVA: 0x0000ABF6 File Offset: 0x00008DF6
		public string Host2
		{
			get
			{
				return (string)this.mHostConfigKey.GetValue("Host2", "https://23.23.194.123");
			}
			set
			{
				this.mHostConfigKey.SetValue("Host2", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0000AC14 File Offset: 0x00008E14
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x0000AC30 File Offset: 0x00008E30
		public string RedDotShownOnIcon
		{
			get
			{
				return (string)this.mHostConfigKey.GetValue("RedDotShownOnIcon", "");
			}
			set
			{
				this.mHostConfigKey.SetValue("RedDotShownOnIcon", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000B59 RID: 2905 RVA: 0x0000AC4E File Offset: 0x00008E4E
		// (set) Token: 0x06000B5A RID: 2906 RVA: 0x0000AC6A File Offset: 0x00008E6A
		public string TwitchServerPath
		{
			get
			{
				return (string)this.mBTVKey.GetValue("TwitchServerPath", "");
			}
			set
			{
				this.mBTVKey.SetValue("TwitchServerPath", value);
				this.mBTVKey.Flush();
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000B5B RID: 2907 RVA: 0x0000AC88 File Offset: 0x00008E88
		// (set) Token: 0x06000B5C RID: 2908 RVA: 0x0000ACA9 File Offset: 0x00008EA9
		public int CLRBrowserServerPort
		{
			get
			{
				return (int)this.mBTVKey.GetValue("CLRBrowserServerPort", 2911);
			}
			set
			{
				this.mBTVKey.SetValue("CLRBrowserServerPort", value);
				this.mBTVKey.Flush();
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000B5D RID: 2909 RVA: 0x0000ACCC File Offset: 0x00008ECC
		// (set) Token: 0x06000B5E RID: 2910 RVA: 0x0000ACE8 File Offset: 0x00008EE8
		public string BtvDevServer
		{
			get
			{
				return (string)this.mBTVKey.GetValue("BtvDevServer", "");
			}
			set
			{
				this.mBTVKey.SetValue("BtvDevServer", value);
				this.mBTVKey.Flush();
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x0000AD06 File Offset: 0x00008F06
		// (set) Token: 0x06000B60 RID: 2912 RVA: 0x0000AD22 File Offset: 0x00008F22
		public string BtvNetwork
		{
			get
			{
				return (string)this.mBTVKey.GetValue("Network", "");
			}
			set
			{
				this.mBTVKey.SetValue("Network", value);
				this.mBTVKey.Flush();
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x0000AD40 File Offset: 0x00008F40
		// (set) Token: 0x06000B62 RID: 2914 RVA: 0x0000AD5D File Offset: 0x00008F5D
		public int StreamingResolution
		{
			get
			{
				return (int)this.mBTVKey.GetValue("StreamingResolution", 0);
			}
			set
			{
				this.mBTVKey.SetValue("StreamingResolution", value);
				this.mBTVKey.Flush();
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x0000AD80 File Offset: 0x00008F80
		// (set) Token: 0x06000B64 RID: 2916 RVA: 0x0000AD9C File Offset: 0x00008F9C
		public string SelectedCam
		{
			get
			{
				return (string)this.mBTVKey.GetValue("SelectedCam", string.Empty);
			}
			set
			{
				this.mBTVKey.SetValue("SelectedCam", value);
				this.mBTVKey.Flush();
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x0000ADBA File Offset: 0x00008FBA
		// (set) Token: 0x06000B66 RID: 2918 RVA: 0x0000ADD7 File Offset: 0x00008FD7
		public int ReplayBufferEnabled
		{
			get
			{
				return (int)this.mBTVKey.GetValue("ReplayBufferEnabled", 0);
			}
			set
			{
				this.mBTVKey.SetValue("ReplayBufferEnabled", value);
				this.mBTVKey.Flush();
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x0000ADFA File Offset: 0x00008FFA
		// (set) Token: 0x06000B68 RID: 2920 RVA: 0x0000AE1B File Offset: 0x0000901B
		public int BTVServerPort
		{
			get
			{
				return (int)this.mBTVKey.GetValue("BTVServerPort", 2885);
			}
			set
			{
				this.mBTVKey.SetValue("BTVServerPort", value);
				this.mBTVKey.Flush();
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0000AE3E File Offset: 0x0000903E
		// (set) Token: 0x06000B6A RID: 2922 RVA: 0x0000AE5B File Offset: 0x0000905B
		public int AppViewLayout
		{
			get
			{
				return (int)this.mBTVKey.GetValue("AppViewLayout", 0);
			}
			set
			{
				this.mBTVKey.SetValue("AppViewLayout", value);
				this.mBTVKey.Flush();
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x0000AE7E File Offset: 0x0000907E
		public string FilterUrl
		{
			get
			{
				return (string)this.mBTVKey.GetValue("FilterUrl", "");
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x0000AE9A File Offset: 0x0000909A
		public string LayoutUrl
		{
			get
			{
				return (string)this.mBTVKey.GetValue("LayoutUrl", "");
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0000AEB6 File Offset: 0x000090B6
		// (set) Token: 0x06000B6E RID: 2926 RVA: 0x0000AED2 File Offset: 0x000090D2
		public string LayoutTheme
		{
			get
			{
				return (string)this.mBTVKey.GetValue("LayoutTheme", "");
			}
			set
			{
				this.mBTVKey.SetValue("LayoutTheme", value);
				this.mBTVKey.Flush();
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x0000AEF0 File Offset: 0x000090F0
		// (set) Token: 0x06000B70 RID: 2928 RVA: 0x0000AF0C File Offset: 0x0000910C
		public string LastCameraLayoutTheme
		{
			get
			{
				return (string)this.mBTVKey.GetValue("LastCameraLayoutTheme", "");
			}
			set
			{
				this.mBTVKey.SetValue("LastCameraLayoutTheme", value);
				this.mBTVKey.Flush();
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x0000AF2A File Offset: 0x0000912A
		// (set) Token: 0x06000B72 RID: 2930 RVA: 0x0000AF47 File Offset: 0x00009147
		public int ScreenWidth
		{
			get
			{
				return (int)this.mBTVKey.GetValue("ScreenWidth", 0);
			}
			set
			{
				this.mBTVKey.SetValue("ScreenWidth", value);
				this.mBTVKey.Flush();
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x0000AF6A File Offset: 0x0000916A
		// (set) Token: 0x06000B74 RID: 2932 RVA: 0x0000AF87 File Offset: 0x00009187
		public int ScreenHeight
		{
			get
			{
				return (int)this.mBTVKey.GetValue("ScreenHeight", 0);
			}
			set
			{
				this.mBTVKey.SetValue("ScreenHeight", value);
				this.mBTVKey.Flush();
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0000AFAA File Offset: 0x000091AA
		public bool IsImeDebuggingEnabled
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("IsImeDebuggingEnabled", 0) == 1;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0000AFCD File Offset: 0x000091CD
		// (set) Token: 0x06000B77 RID: 2935 RVA: 0x0000AFEE File Offset: 0x000091EE
		public int OBSServerPort
		{
			get
			{
				return (int)this.mBTVKey.GetValue("OBSServerPort", 2851);
			}
			set
			{
				this.mBTVKey.SetValue("OBSServerPort", value);
				this.mBTVKey.Flush();
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000B78 RID: 2936 RVA: 0x0000B011 File Offset: 0x00009211
		// (set) Token: 0x06000B79 RID: 2937 RVA: 0x0000B033 File Offset: 0x00009233
		public bool IsGameCaptureSupportedInMachine
		{
			get
			{
				return (int)this.mBTVKey.GetValue("IsGameCaptureSupportedInMachine", 1) != 0;
			}
			set
			{
				this.mBTVKey.SetValue("IsGameCaptureSupportedInMachine", (!value) ? 0 : 1);
				this.mBTVKey.Flush();
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000B7A RID: 2938 RVA: 0x0000B05C File Offset: 0x0000925C
		// (set) Token: 0x06000B7B RID: 2939 RVA: 0x0000B078 File Offset: 0x00009278
		public string StreamName
		{
			get
			{
				return (string)this.mBTVKey.GetValue("StreamName", "");
			}
			set
			{
				this.mBTVKey.SetValue("StreamName", value);
				this.mBTVKey.Flush();
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0000B096 File Offset: 0x00009296
		// (set) Token: 0x06000B7D RID: 2941 RVA: 0x0000B0B2 File Offset: 0x000092B2
		public string ServerLocation
		{
			get
			{
				return (string)this.mBTVKey.GetValue("ServerLocation", "");
			}
			set
			{
				this.mBTVKey.SetValue("ServerLocation", value);
				this.mBTVKey.Flush();
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000B7E RID: 2942 RVA: 0x0000B0D0 File Offset: 0x000092D0
		// (set) Token: 0x06000B7F RID: 2943 RVA: 0x0000B0EC File Offset: 0x000092EC
		public string ChannelName
		{
			get
			{
				return (string)this.mBTVFilterKey.GetValue("ChannelName", "");
			}
			set
			{
				this.mBTVFilterKey.SetValue("ChannelName", value);
				this.mBTVFilterKey.Flush();
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x0000B10A File Offset: 0x0000930A
		// (set) Token: 0x06000B81 RID: 2945 RVA: 0x0000B126 File Offset: 0x00009326
		public string NotificationData
		{
			get
			{
				return (string)this.mHostConfigKey.GetValue("NotificationData", string.Empty);
			}
			set
			{
				this.mHostConfigKey.SetValue("NotificationData", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0000B144 File Offset: 0x00009344
		// (set) Token: 0x06000B83 RID: 2947 RVA: 0x0000B160 File Offset: 0x00009360
		public string DeviceCaps
		{
			get
			{
				return (string)this.mHostConfigKey.GetValue("DeviceCaps", "");
			}
			set
			{
				this.mHostConfigKey.SetValue("DeviceCaps", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000B84 RID: 2948 RVA: 0x0000B17E File Offset: 0x0000937E
		// (set) Token: 0x06000B85 RID: 2949 RVA: 0x0000B19F File Offset: 0x0000939F
		public int AgentServerPort
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("AgentServerPort", 2861);
			}
			set
			{
				this.mHostConfigKey.SetValue("AgentServerPort", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x0000B1C2 File Offset: 0x000093C2
		// (set) Token: 0x06000B87 RID: 2951 RVA: 0x0000B1E3 File Offset: 0x000093E3
		public int MultiInstanceServerPort
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("MultiInstanceServerPort", 2961);
			}
			set
			{
				this.mHostConfigKey.SetValue("MultiInstanceServerPort", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0000B206 File Offset: 0x00009406
		// (set) Token: 0x06000B89 RID: 2953 RVA: 0x0000B222 File Offset: 0x00009422
		public string Oem
		{
			get
			{
				return (string)this.mHostConfigKey.GetValue("Oem", "gamemanager");
			}
			set
			{
				this.mHostConfigKey.SetValue("Oem", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0000B240 File Offset: 0x00009440
		// (set) Token: 0x06000B8B RID: 2955 RVA: 0x0000B25D File Offset: 0x0000945D
		public int BatchInstanceStartInterval
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("BatchInstanceStartInterval", 2);
			}
			set
			{
				this.mHostConfigKey.SetValue("BatchInstanceStartInterval", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0000B280 File Offset: 0x00009480
		// (set) Token: 0x06000B8D RID: 2957 RVA: 0x0000B29C File Offset: 0x0000949C
		public string CampaignName
		{
			get
			{
				return (string)this.mHostConfigKey.GetValue("CampaignName", "");
			}
			set
			{
				this.mHostConfigKey.SetValue("CampaignName", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x0000B2BA File Offset: 0x000094BA
		// (set) Token: 0x06000B8F RID: 2959 RVA: 0x0000B2D6 File Offset: 0x000094D6
		public string PartnerExePath
		{
			get
			{
				return (string)this.mHostConfigKey.GetValue("PartnerExePath", "");
			}
			set
			{
				this.mHostConfigKey.SetValue("PartnerExePath", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0000B2F4 File Offset: 0x000094F4
		// (set) Token: 0x06000B91 RID: 2961 RVA: 0x0000B311 File Offset: 0x00009511
		public int CamStatus
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("CamStatus", 0);
			}
			set
			{
				this.mHostConfigKey.SetValue("CamStatus", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0000B334 File Offset: 0x00009534
		// (set) Token: 0x06000B93 RID: 2963 RVA: 0x0000B355 File Offset: 0x00009555
		public int PartnerServerPort
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("PartnerServerPort", 2871);
			}
			set
			{
				this.mHostConfigKey.SetValue("PartnerServerPort", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x0000B378 File Offset: 0x00009578
		// (set) Token: 0x06000B95 RID: 2965 RVA: 0x0000B394 File Offset: 0x00009594
		public string RegisteredEmail
		{
			get
			{
				return (string)this.mUserKey.GetValue("RegisteredEmail", "");
			}
			set
			{
				this.mUserKey.SetValue("RegisteredEmail", value);
				this.mUserKey.Flush();
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x0000B3B2 File Offset: 0x000095B2
		// (set) Token: 0x06000B97 RID: 2967 RVA: 0x0000B3D4 File Offset: 0x000095D4
		public bool IsTimelineStats4Enabled
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("IsTimelineStats4Enabled", 0) != 0;
			}
			set
			{
				this.mHostConfigKey.SetValue("IsTimelineStats4Enabled", (!value) ? 0 : 1);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0000B3FD File Offset: 0x000095FD
		// (set) Token: 0x06000B99 RID: 2969 RVA: 0x0000B41F File Offset: 0x0000961F
		public bool EnableAutomation
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("EnableAutomation", 0) != 0;
			}
			set
			{
				this.mHostConfigKey.SetValue("EnableAutomation", (!value) ? 0 : 1);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0000B448 File Offset: 0x00009648
		// (set) Token: 0x06000B9B RID: 2971 RVA: 0x0000B464 File Offset: 0x00009664
		public string PikaWorldId
		{
			get
			{
				return (string)this.mUserKey.GetValue("PikaWorldId", "");
			}
			set
			{
				this.mUserKey.SetValue("PikaWorldId", value);
				this.mUserKey.Flush();
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x0000B482 File Offset: 0x00009682
		// (set) Token: 0x06000B9D RID: 2973 RVA: 0x0000B49E File Offset: 0x0000969E
		public string Token
		{
			get
			{
				return (string)this.mUserKey.GetValue("Token", "");
			}
			set
			{
				this.mUserKey.SetValue("Token", value);
				this.mUserKey.Flush();
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0000B4BC File Offset: 0x000096BC
		// (set) Token: 0x06000B9F RID: 2975 RVA: 0x0000B4DF File Offset: 0x000096DF
		public bool IsPremium
		{
			get
			{
				return (int)this.mUserKey.GetValue("IsPremium", 0) == 1;
			}
			set
			{
				this.mUserKey.SetValue("IsPremium", value ? 1 : 0);
				this.mUserKey.Flush();
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x0000B508 File Offset: 0x00009708
		// (set) Token: 0x06000BA1 RID: 2977 RVA: 0x0000B52A File Offset: 0x0000972A
		public bool AddDesktopShortcuts
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("AddDesktopShortcuts", 1) != 0;
			}
			set
			{
				this.mHostConfigKey.SetValue("AddDesktopShortcuts", (!value) ? 0 : 1);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0000B553 File Offset: 0x00009753
		// (set) Token: 0x06000BA3 RID: 2979 RVA: 0x0000B575 File Offset: 0x00009775
		public bool SwitchToAndroidHome
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("SwitchToAndroidHome", 0) != 0;
			}
			set
			{
				this.mHostConfigKey.SetValue("SwitchToAndroidHome", (!value) ? 0 : 1);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x0000B59E File Offset: 0x0000979E
		// (set) Token: 0x06000BA5 RID: 2981 RVA: 0x0000B5C0 File Offset: 0x000097C0
		public bool SwitchKillWebTab
		{
			get
			{
				return (int)this.mClientKey.GetValue("SwitchKillWebTab", 1) != 0;
			}
			set
			{
				this.mClientKey.SetValue("SwitchKillWebTab", (!value) ? 0 : 1);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0000B5E9 File Offset: 0x000097E9
		// (set) Token: 0x06000BA7 RID: 2983 RVA: 0x0000B60B File Offset: 0x0000980B
		public bool EnableMemoryTrim
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("EnableMemoryTrim", 0) != 0;
			}
			set
			{
				this.mHostConfigKey.SetValue("EnableMemoryTrim", (!value) ? 0 : 1);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0000B634 File Offset: 0x00009834
		// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x0000B656 File Offset: 0x00009856
		public bool GLES3
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("GLES3", 0) != 0;
			}
			set
			{
				this.mHostConfigKey.SetValue("GLES3", (!value) ? 0 : 1);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0000B67F File Offset: 0x0000987F
		// (set) Token: 0x06000BAB RID: 2987 RVA: 0x0000B6A1 File Offset: 0x000098A1
		public bool IsAutoShowGuidance
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("IsAutoShowGuidance", 1) != 0;
			}
			set
			{
				this.mHostConfigKey.SetValue("IsAutoShowGuidance", (!value) ? 0 : 1);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x0000B6CA File Offset: 0x000098CA
		// (set) Token: 0x06000BAD RID: 2989 RVA: 0x0000B6E7 File Offset: 0x000098E7
		public string[] DisabledGuidancePackages
		{
			get
			{
				return (string[])this.mHostConfigKey.GetValue("DisabledGuidancePackages", new string[0]);
			}
			set
			{
				this.mHostConfigKey.SetValue("DisabledGuidancePackages", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0000B705 File Offset: 0x00009905
		// (set) Token: 0x06000BAF RID: 2991 RVA: 0x0000B727 File Offset: 0x00009927
		public bool IsRememberWindowPositionEnabled
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("IsRememberWindowPositionEnabled", 1) != 0;
			}
			set
			{
				this.mHostConfigKey.SetValue("IsRememberWindowPositionEnabled", (!value) ? 0 : 1);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0000B750 File Offset: 0x00009950
		// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x0000B76C File Offset: 0x0000996C
		public string InstallID
		{
			get
			{
				return (string)this.mHostConfigKey.GetValue("InstallID", string.Empty);
			}
			set
			{
				this.mHostConfigKey.SetValue("InstallID", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000BB2 RID: 2994 RVA: 0x0000B78A File Offset: 0x0000998A
		// (set) Token: 0x06000BB3 RID: 2995 RVA: 0x0000B7A6 File Offset: 0x000099A6
		public string OldInstallID
		{
			get
			{
				return (string)this.mHostConfigKey.GetValue("OldInstallID", string.Empty);
			}
			set
			{
				this.mHostConfigKey.SetValue("OldInstallID", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0000B7C4 File Offset: 0x000099C4
		// (set) Token: 0x06000BB5 RID: 2997 RVA: 0x0000B7E0 File Offset: 0x000099E0
		public string HelperVersion
		{
			get
			{
				return (string)this.mHostConfigKey.GetValue("HelperVersion", string.Empty);
			}
			set
			{
				this.mHostConfigKey.SetValue("HelperVersion", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000BB6 RID: 2998 RVA: 0x0000B7FE File Offset: 0x000099FE
		// (set) Token: 0x06000BB7 RID: 2999 RVA: 0x0000B81A File Offset: 0x00009A1A
		public string InstallerPkgName
		{
			get
			{
				return (string)this.mHostConfigKey.GetValue("InstallerPkgName", string.Empty);
			}
			set
			{
				this.mHostConfigKey.SetValue("InstallerPkgName", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x0002B520 File Offset: 0x00029720
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x0000B838 File Offset: 0x00009A38
		public InstallationTypes InstallationType
		{
			get
			{
				if (this.mInstallationType == InstallationTypes.None)
				{
					this.mInstallationType = (InstallationTypes)Enum.Parse(typeof(InstallationTypes), (string)this.mHostConfigKey.GetValue("InstallationType", InstallationTypes.FullEdition.ToString()), true);
				}
				return this.mInstallationType;
			}
			set
			{
				this.mHostConfigKey.SetValue("InstallationType", value);
				this.mHostConfigKey.Flush();
				this.mInstallationType = value;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x0000B862 File Offset: 0x00009A62
		// (set) Token: 0x06000BBB RID: 3003 RVA: 0x0000B87E File Offset: 0x00009A7E
		public string CurrentFirebaseHost
		{
			get
			{
				return (string)this.mClientKey.GetValue("CurrentFirebaseHost", string.Empty);
			}
			set
			{
				this.mClientKey.SetValue("CurrentFirebaseHost", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000BBC RID: 3004 RVA: 0x0002B57C File Offset: 0x0002977C
		// (set) Token: 0x06000BBD RID: 3005 RVA: 0x0000B89C File Offset: 0x00009A9C
		public string PendingLaunchAction
		{
			get
			{
				return (string)this.mClientKey.GetValue("PendingLaunchAction", string.Format(CultureInfo.InvariantCulture, "{0},{1}", new object[]
				{
					GenericAction.None,
					string.Empty
				}));
			}
			set
			{
				this.mClientKey.SetValue("PendingLaunchAction", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0002B5C8 File Offset: 0x000297C8
		// (set) Token: 0x06000BBF RID: 3007 RVA: 0x0002B628 File Offset: 0x00029828
		public DateTime AnnouncementTime
		{
			get
			{
				string text = (string)this.mHostConfigKey.GetValue("AnnouncementTime", string.Empty);
				DateTime result = DateTime.Now;
				try
				{
					if (!string.IsNullOrEmpty(text))
					{
						result = DateTime.ParseExact(text, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
					}
				}
				catch
				{
				}
				return result;
			}
			set
			{
				string value2 = value.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
				this.mHostConfigKey.SetValue("AnnouncementTime", value2);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0000B8BA File Offset: 0x00009ABA
		// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x0000B8D6 File Offset: 0x00009AD6
		public string RootVdiMd5Hash
		{
			get
			{
				return (string)this.mHostConfigKey.GetValue("RootVdiMd5Hash", string.Empty);
			}
			set
			{
				this.mHostConfigKey.SetValue("RootVdiMd5Hash", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x0000B8F4 File Offset: 0x00009AF4
		// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x0000B910 File Offset: 0x00009B10
		public string Geo
		{
			get
			{
				return (string)this.mClientKey.GetValue("Geo", "");
			}
			set
			{
				this.mClientKey.SetValue("Geo", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x0000B92E File Offset: 0x00009B2E
		// (set) Token: 0x06000BC5 RID: 3013 RVA: 0x0000B94A File Offset: 0x00009B4A
		public string QuitDefaultOption
		{
			get
			{
				return (string)this.mClientKey.GetValue("QuitDefaultOption", "STRING_CLOSE_CURRENT_INSTANCE");
			}
			set
			{
				this.mClientKey.SetValue("QuitDefaultOption", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0000B968 File Offset: 0x00009B68
		// (set) Token: 0x06000BC7 RID: 3015 RVA: 0x0000B98A File Offset: 0x00009B8A
		public bool IsQuitOptionSaved
		{
			get
			{
				return (int)this.mClientKey.GetValue("QuitOptionSaved", 0) != 0;
			}
			set
			{
				this.mClientKey.SetValue("QuitOptionSaved", (!value) ? 0 : 1);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x0000B9B3 File Offset: 0x00009BB3
		// (set) Token: 0x06000BC9 RID: 3017 RVA: 0x0000B9D0 File Offset: 0x00009BD0
		public int VmId
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("VmId", 1);
			}
			set
			{
				this.mHostConfigKey.SetValue("VmId", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x0000B9F3 File Offset: 0x00009BF3
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x0000BA10 File Offset: 0x00009C10
		public string[] BookmarkedScriptList
		{
			get
			{
				return (string[])this.mHostConfigKey.GetValue("BookmarkedScriptList", new string[0]);
			}
			set
			{
				this.mHostConfigKey.SetValue("BookmarkedScriptList", value, RegistryValueKind.MultiString);
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0000BA24 File Offset: 0x00009C24
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x0000BA40 File Offset: 0x00009C40
		public string DefaultShortcuts
		{
			get
			{
				return (string)this.mHostConfigKey.GetValue("DefaultShortcuts", "");
			}
			set
			{
				this.mHostConfigKey.SetValue("DefaultShortcuts", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0000BA5E File Offset: 0x00009C5E
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x0000BA7A File Offset: 0x00009C7A
		public string UserDefinedShortcuts
		{
			get
			{
				return (string)this.mHostConfigKey.GetValue("UserDefinedShortcuts", "");
			}
			set
			{
				this.mHostConfigKey.SetValue("UserDefinedShortcuts", Regex.Replace(value, "\\n|\\r", ""));
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0000BAA7 File Offset: 0x00009CA7
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x0000BAC4 File Offset: 0x00009CC4
		public string[] DefaultSidebarElements
		{
			get
			{
				return (string[])this.mHostConfigKey.GetValue("DefaultSidebarElements", new string[0]);
			}
			set
			{
				this.mHostConfigKey.SetValue("DefaultSidebarElements", value, RegistryValueKind.MultiString);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0000BAE3 File Offset: 0x00009CE3
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x0000BB00 File Offset: 0x00009D00
		public string[] UserDefinedSidebarElements
		{
			get
			{
				return (string[])this.mHostConfigKey.GetValue("UserDefinedSidebarElements", new string[0]);
			}
			set
			{
				this.mHostConfigKey.SetValue("UserDefinedSidebarElements", value, RegistryValueKind.MultiString);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0000BB1F File Offset: 0x00009D1F
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x0000BB41 File Offset: 0x00009D41
		public bool AreAllInstancesMuted
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("AreAllInstancesMuted", 0) != 0;
			}
			set
			{
				this.mHostConfigKey.SetValue("AreAllInstancesMuted", (!value) ? 0 : 1);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x0000BB6A File Offset: 0x00009D6A
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x0000BB8C File Offset: 0x00009D8C
		public bool IsSamsungStorePresent
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("IsSamsungStorePresent", 0) != 0;
			}
			set
			{
				this.mHostConfigKey.SetValue("IsSamsungStorePresent", (!value) ? 0 : 1);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0000BBB5 File Offset: 0x00009DB5
		// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x0000BBD7 File Offset: 0x00009DD7
		public bool IsCacodeValid
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("IsCacodeValid", 0) != 0;
			}
			set
			{
				this.mHostConfigKey.SetValue("IsCacodeValid", value ? 1 : 0);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x0000BC00 File Offset: 0x00009E00
		public void SetClientThemeNameInRegistry(string themeName)
		{
			this.mClientKey.SetValue("ClientThemeName", themeName);
			RegistryManager.ClientThemeName = themeName;
			this.mClientKey.Flush();
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x0000BC24 File Offset: 0x00009E24
		public string GetClientThemeNameFromRegistry()
		{
			return this.mClientKey.GetValue("ClientThemeName", "Assets").ToString();
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x0000BC40 File Offset: 0x00009E40
		// (set) Token: 0x06000BDD RID: 3037 RVA: 0x0000BC5E File Offset: 0x00009E5E
		public int AdvancedControlTransparencyLevel
		{
			get
			{
				return (int)this.mClientKey.GetValue("AdvancedControlTransparencyLevel", 50);
			}
			set
			{
				this.mClientKey.SetValue("AdvancedControlTransparencyLevel", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0000BC81 File Offset: 0x00009E81
		// (set) Token: 0x06000BDF RID: 3039 RVA: 0x0000BCA3 File Offset: 0x00009EA3
		public bool IsUtcConverterBlurbOnboardingCompleted
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("IsUtcConverterBlurbOnboardingCompleted", 0) != 0;
			}
			set
			{
				this.mHostConfigKey.SetValue("IsUtcConverterBlurbOnboardingCompleted", value ? 1 : 0);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0000BCCC File Offset: 0x00009ECC
		// (set) Token: 0x06000BE1 RID: 3041 RVA: 0x0000BCEE File Offset: 0x00009EEE
		public bool IsUtcConverterRedDotOnboardingCompleted
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("IsUtcConverterRedDotOnboardingCompleted", 0) != 0;
			}
			set
			{
				this.mHostConfigKey.SetValue("IsUtcConverterRedDotOnboardingCompleted", value ? 1 : 0);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0002B664 File Offset: 0x00029864
		// (set) Token: 0x06000BE3 RID: 3043 RVA: 0x0000BD17 File Offset: 0x00009F17
		public AppLaunchState FirstAppLaunchState
		{
			get
			{
				if (this.mFirstAppLaunchState == AppLaunchState.Unknown)
				{
					this.mFirstAppLaunchState = (AppLaunchState)Enum.Parse(typeof(AppLaunchState), (string)this.mClientKey.GetValue("FirstAppLaunchedState", AppLaunchState.Launched.ToString()), true);
				}
				return this.mFirstAppLaunchState;
			}
			set
			{
				this.mClientKey.SetValue("FirstAppLaunchedState", value);
				this.mClientKey.Flush();
				this.mFirstAppLaunchState = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0000BD41 File Offset: 0x00009F41
		// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x0000BD5D File Offset: 0x00009F5D
		public string AppConfiguration
		{
			get
			{
				return (string)this.mHostConfigKey.GetValue("AppConfiguration", "{ }");
			}
			set
			{
				this.mHostConfigKey.SetValue("AppConfiguration", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0000BD7B File Offset: 0x00009F7B
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x0000BD97 File Offset: 0x00009F97
		public string AppPlayerEngineInfo
		{
			get
			{
				return (string)this.mHostConfigKey.GetValue("AppPlayerEngineInfo", Constants.DefaultAppPlayerEngineInfo);
			}
			set
			{
				this.mHostConfigKey.SetValue("AppPlayerEngineInfo", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0000BDB5 File Offset: 0x00009FB5
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x0000BDD2 File Offset: 0x00009FD2
		public int CloudABIValue
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("CloudABIValue", 0);
			}
			set
			{
				this.mHostConfigKey.SetValue("CloudABIValue", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0000BDF5 File Offset: 0x00009FF5
		// (set) Token: 0x06000BEB RID: 3051 RVA: 0x0000BE12 File Offset: 0x0000A012
		public int NotificationModeCounter
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("NotificationModeCounter", 3);
			}
			set
			{
				this.mHostConfigKey.SetValue("NotificationModeCounter", value);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0000BE35 File Offset: 0x0000A035
		// (set) Token: 0x06000BED RID: 3053 RVA: 0x0000BE57 File Offset: 0x0000A057
		public bool IsNotificationModeAlwaysOn
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("IsNotificationModeAlwaysOn", 0) != 0;
			}
			set
			{
				this.mHostConfigKey.SetValue("IsNotificationModeAlwaysOn", (!value) ? 0 : 1);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0000BE80 File Offset: 0x0000A080
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x0000BEA2 File Offset: 0x0000A0A2
		public bool IsNotificationSoundsActive
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("IsNotificationSoundsActive", 1) != 0;
			}
			set
			{
				this.mHostConfigKey.SetValue("IsNotificationSoundsActive", (!value) ? 0 : 1);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0000BECB File Offset: 0x0000A0CB
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x0000BEED File Offset: 0x0000A0ED
		public bool FixNotificationDataAfterFirstPostBootCloudInfo
		{
			get
			{
				return (int)this.mHostConfigKey.GetValue("FixNotificationDataAfterFirstPostBootCloudInfo", 0) != 0;
			}
			set
			{
				this.mHostConfigKey.SetValue("FixNotificationDataAfterFirstPostBootCloudInfo", (!value) ? 0 : 1);
				this.mHostConfigKey.Flush();
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0000BF16 File Offset: 0x0000A116
		// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x0000BF32 File Offset: 0x0000A132
		public string UpdaterFileDeletePath
		{
			get
			{
				return (string)this.mClientKey.GetValue("UpdaterFileDeletePath", "");
			}
			set
			{
				this.mClientKey.SetValue("UpdaterFileDeletePath", value);
				this.mClientKey.Flush();
			}
		}

		// Token: 0x0400054E RID: 1358
		private static string mUPGRADE_TAG = string.Empty;

		// Token: 0x0400054F RID: 1359
		public const string UPGRADE_TAG_NEW = ".new";

		// Token: 0x04000550 RID: 1360
		private RegistryKey mBaseKey;

		// Token: 0x04000551 RID: 1361
		private RegistryKey mClientKey;

		// Token: 0x04000552 RID: 1362
		private RegistryKey mBTVKey;

		// Token: 0x04000553 RID: 1363
		private RegistryKey mBTVFilterKey;

		// Token: 0x04000554 RID: 1364
		private RegistryKey mUserKey;

		// Token: 0x04000555 RID: 1365
		private RegistryKey mHostConfigKey;

		// Token: 0x04000556 RID: 1366
		private RegistryKey mGuestsKey;

		// Token: 0x04000557 RID: 1367
		private RegistryKey mMonitorsKey;

		// Token: 0x04000558 RID: 1368
		private bool mIsAdmin;

		// Token: 0x04000559 RID: 1369
		private static RegistryManager sInstance = null;

		// Token: 0x0400055A RID: 1370
		private static object sLock = new object();

		// Token: 0x0400055B RID: 1371
		private static Dictionary<string, RegistryManager> _RegistryManagers;

		// Token: 0x04000562 RID: 1378
		private string sCurrentEngine = "";

		// Token: 0x04000563 RID: 1379
		private string sUserDefinedDir;

		// Token: 0x04000564 RID: 1380
		private string sVersion;

		// Token: 0x04000565 RID: 1381
		private string mHost = string.Empty;

		// Token: 0x04000566 RID: 1382
		private InstallationTypes mInstallationType;

		// Token: 0x04000567 RID: 1383
		private AppLaunchState mFirstAppLaunchState;
	}
}
