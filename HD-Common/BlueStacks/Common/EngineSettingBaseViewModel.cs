using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.VisualBasic.Devices;

namespace BlueStacks.Common
{
	// Token: 0x020000A5 RID: 165
	public abstract class EngineSettingBaseViewModel : ViewModelBase
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00003D55 File Offset: 0x00001F55
		// (set) Token: 0x0600037A RID: 890 RVA: 0x00003D5D File Offset: 0x00001F5D
		public string OEM
		{
			get
			{
				return this._OEM;
			}
			set
			{
				base.SetProperty<string>(ref this._OEM, value, null);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00003D6E File Offset: 0x00001F6E
		// (set) Token: 0x0600037C RID: 892 RVA: 0x00003D76 File Offset: 0x00001F76
		public int MinRam
		{
			get
			{
				return this._MinRam;
			}
			set
			{
				base.SetProperty<int>(ref this._MinRam, value, null);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00003D87 File Offset: 0x00001F87
		// (set) Token: 0x0600037E RID: 894 RVA: 0x00003D8F File Offset: 0x00001F8F
		public int MaxRam
		{
			get
			{
				return this._MaxRam;
			}
			set
			{
				base.SetProperty<int>(ref this._MaxRam, value, null);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00015510 File Offset: 0x00013710
		public static int UserMachineRAM
		{
			get
			{
				if (EngineSettingBaseViewModel._RamInMB == null)
				{
					try
					{
						ulong totalPhysicalMemory = new ComputerInfo().TotalPhysicalMemory;
						EngineSettingBaseViewModel._RamInMB = new int?((int)(totalPhysicalMemory / 1048576UL));
					}
					catch (Exception ex)
					{
						Logger.Error("Exception when finding ram " + ex.ToString());
					}
				}
				return EngineSettingBaseViewModel._RamInMB.GetValueOrDefault();
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00003DA0 File Offset: 0x00001FA0
		// (set) Token: 0x06000381 RID: 897 RVA: 0x00003DA8 File Offset: 0x00001FA8
		public int UserMachineCpuCores
		{
			get
			{
				return this._UserMachineCpuCores;
			}
			set
			{
				base.SetProperty<int>(ref this._UserMachineCpuCores, value, null);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00003DB9 File Offset: 0x00001FB9
		// (set) Token: 0x06000383 RID: 899 RVA: 0x00003DC1 File Offset: 0x00001FC1
		public int MaxFPS
		{
			get
			{
				return this._MaxFPS;
			}
			set
			{
				base.SetProperty<int>(ref this._MaxFPS, value, null);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00003DD2 File Offset: 0x00001FD2
		// (set) Token: 0x06000385 RID: 901 RVA: 0x00003DDA File Offset: 0x00001FDA
		public Status Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				base.SetProperty<Status>(ref this._Status, value, null);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00003DEB File Offset: 0x00001FEB
		// (set) Token: 0x06000387 RID: 903 RVA: 0x00003DF3 File Offset: 0x00001FF3
		public bool IsGraphicModeEnabled
		{
			get
			{
				return this._IsGraphicModeEnabled;
			}
			set
			{
				base.SetProperty<bool>(ref this._IsGraphicModeEnabled, value, null);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00003E04 File Offset: 0x00002004
		// (set) Token: 0x06000389 RID: 905 RVA: 0x00003E0C File Offset: 0x0000200C
		public GraphicsMode GraphicsMode
		{
			get
			{
				return this._GraphicsMode;
			}
			set
			{
				if (this._GraphicsMode != value)
				{
					this.ValidateGraphicMode(this._GraphicsMode, value);
				}
				base.NotifyPropertyChanged("GraphicsMode");
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00003E2F File Offset: 0x0000202F
		// (set) Token: 0x0600038B RID: 907 RVA: 0x00003E37 File Offset: 0x00002037
		public Uri DirectXUri
		{
			get
			{
				return this._DirectXUri;
			}
			set
			{
				base.SetProperty<Uri>(ref this._DirectXUri, value, null);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00003E48 File Offset: 0x00002048
		// (set) Token: 0x0600038D RID: 909 RVA: 0x00003E50 File Offset: 0x00002050
		public string WarningMessage
		{
			get
			{
				return this._WarningMessage;
			}
			set
			{
				base.SetProperty<string>(ref this._WarningMessage, value, null);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00003E61 File Offset: 0x00002061
		// (set) Token: 0x0600038F RID: 911 RVA: 0x00003E69 File Offset: 0x00002069
		public string ProgressMessage
		{
			get
			{
				return this._ProgressMessage;
			}
			set
			{
				base.SetProperty<string>(ref this._ProgressMessage, value, null);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00003E7A File Offset: 0x0000207A
		// (set) Token: 0x06000391 RID: 913 RVA: 0x00003E82 File Offset: 0x00002082
		public string ErrorMessage
		{
			get
			{
				return this._ErrorMessage;
			}
			set
			{
				base.SetProperty<string>(ref this._ErrorMessage, value, null);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00003E93 File Offset: 0x00002093
		// (set) Token: 0x06000393 RID: 915 RVA: 0x00003E9B File Offset: 0x0000209B
		public bool UseAdvancedGraphicEngine
		{
			get
			{
				return this._UseAdvancedGraphicEngine;
			}
			set
			{
				if (this._UseAdvancedGraphicEngine != value)
				{
					this.ValidateGraphicEngine(value);
					return;
				}
				base.NotifyPropertyChanged("UseAdvancedGraphicEngine");
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000394 RID: 916 RVA: 0x00003EB9 File Offset: 0x000020B9
		// (set) Token: 0x06000395 RID: 917 RVA: 0x00003EC1 File Offset: 0x000020C1
		public bool UseDedicatedGPU
		{
			get
			{
				return this._UseDedicatedGPU;
			}
			set
			{
				if (this._UseDedicatedGPU != value)
				{
					this.ValidateGPU(this._UseDedicatedGPU, value);
					return;
				}
				base.NotifyPropertyChanged("UseDedicatedGPU");
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000396 RID: 918 RVA: 0x00003EE5 File Offset: 0x000020E5
		// (set) Token: 0x06000397 RID: 919 RVA: 0x00003EED File Offset: 0x000020ED
		public string PreferDedicatedGPUText
		{
			get
			{
				return this._PreferDedicatedGPUText;
			}
			set
			{
				base.SetProperty<string>(ref this._PreferDedicatedGPUText, value, null);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000398 RID: 920 RVA: 0x00003EFE File Offset: 0x000020FE
		// (set) Token: 0x06000399 RID: 921 RVA: 0x00003F06 File Offset: 0x00002106
		public string UseDedicatedGPUText
		{
			get
			{
				return this._UseDedicatedGPUText;
			}
			set
			{
				base.SetProperty<string>(ref this._UseDedicatedGPUText, value, null);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00003F17 File Offset: 0x00002117
		// (set) Token: 0x0600039B RID: 923 RVA: 0x00003F1F File Offset: 0x0000211F
		public bool IsGPUAvailable
		{
			get
			{
				return this._IsGPUAvailable;
			}
			set
			{
				base.SetProperty<bool>(ref this._IsGPUAvailable, value, null);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00003F30 File Offset: 0x00002130
		// (set) Token: 0x0600039D RID: 925 RVA: 0x00003F38 File Offset: 0x00002138
		public ASTCTexture ASTCTexture
		{
			get
			{
				return this._ASTCTexture;
			}
			set
			{
				if (base.SetProperty<ASTCTexture>(ref this._ASTCTexture, value, null))
				{
					this.SetASTCOption();
				}
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00003F50 File Offset: 0x00002150
		// (set) Token: 0x0600039F RID: 927 RVA: 0x00003F58 File Offset: 0x00002158
		public bool EnableHardwareDecoding
		{
			get
			{
				return this._EnableHardwareDecoding;
			}
			set
			{
				base.SetProperty<bool>(ref this._EnableHardwareDecoding, value, null);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00003F69 File Offset: 0x00002169
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x00003F71 File Offset: 0x00002171
		public bool EnableCaching
		{
			get
			{
				return this._EnableCaching;
			}
			set
			{
				if (base.SetProperty<bool>(ref this._EnableCaching, value, null))
				{
					this.SetASTCOption();
				}
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00003F89 File Offset: 0x00002189
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x00003F91 File Offset: 0x00002191
		public Visibility CpuMemoryVisibility
		{
			get
			{
				return this._CpuMemoryVisibility;
			}
			set
			{
				base.SetProperty<Visibility>(ref this._CpuMemoryVisibility, value, null);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00003FA2 File Offset: 0x000021A2
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x00003FAA File Offset: 0x000021AA
		public bool CustomPerformanceSettingVisibility
		{
			get
			{
				return this._customPerformanceSettingVisibility;
			}
			set
			{
				base.SetProperty<bool>(ref this._customPerformanceSettingVisibility, value, null);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00003FBB File Offset: 0x000021BB
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x00003FC3 File Offset: 0x000021C3
		public ObservableCollection<string> PerformanceSettingList
		{
			get
			{
				return this._PerformanceSettingList;
			}
			set
			{
				base.SetProperty<ObservableCollection<string>>(ref this._PerformanceSettingList, value, null);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00003FD4 File Offset: 0x000021D4
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x00003FDC File Offset: 0x000021DC
		public bool CpuCoreCountChanged { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003AA RID: 938 RVA: 0x00003FE5 File Offset: 0x000021E5
		// (set) Token: 0x060003AB RID: 939 RVA: 0x0001557C File Offset: 0x0001377C
		public int CpuCores
		{
			get
			{
				return this._CpuCores;
			}
			set
			{
				base.SetProperty<int>(ref this._CpuCores, value, null);
				this.CpuCoreCountChanged = (this.EngineData.CpuCores != this.CpuCores);
				if (!this.CpuCoreCustomListVisibility)
				{
					EngineSettingModel engineSettingModel = this.SelectedCPU;
					int? num = (engineSettingModel != null) ? new int?(engineSettingModel.CoreCount) : null;
					int userSupportedVCPU = this._UserSupportedVCPU;
					if (num.GetValueOrDefault() == userSupportedVCPU & num != null)
					{
						goto IL_85;
					}
				}
				if (!this.CpuCoreCustomListVisibility || this.CpuCores != this._UserSupportedVCPU)
				{
					this.MaxCoreWarningTextVisibility = false;
					return;
				}
				IL_85:
				if (this.EngineData.CpuCores != 0)
				{
					this.MaxCoreWarningTextVisibility = true;
					Stats.SendMiscellaneousStatsAsync("core_warning_viewed", RegistryManager.RegistryManagers[this.OEM].UserGuid, RegistryManager.RegistryManagers[this.OEM].ClientVersion, "Engine-Settings", null, RegistryManager.Instance.Oem, null, null, RegistryManager.Instance.UserSelectedLocale, "Android", 0);
					return;
				}
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00003FED File Offset: 0x000021ED
		// (set) Token: 0x060003AD RID: 941 RVA: 0x00003FF5 File Offset: 0x000021F5
		public ObservableCollection<int> CpuCoresList
		{
			get
			{
				return this._CpuCoresList;
			}
			set
			{
				base.SetProperty<ObservableCollection<int>>(ref this._CpuCoresList, value, null);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003AE RID: 942 RVA: 0x00004006 File Offset: 0x00002206
		// (set) Token: 0x060003AF RID: 943 RVA: 0x0000400E File Offset: 0x0000220E
		public int Ram
		{
			get
			{
				return this._Ram;
			}
			set
			{
				base.SetProperty<int>(ref this._Ram, value, null);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000401F File Offset: 0x0000221F
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x00004027 File Offset: 0x00002227
		public bool IsRamSliderEnabled
		{
			get
			{
				return this._IsRamSliderEnabled;
			}
			set
			{
				base.SetProperty<bool>(ref this._IsRamSliderEnabled, value, null);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00004038 File Offset: 0x00002238
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x00004040 File Offset: 0x00002240
		public string RecommendedRamText
		{
			get
			{
				return this._RecommendedRamText;
			}
			set
			{
				base.SetProperty<string>(ref this._RecommendedRamText, value, null);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00004051 File Offset: 0x00002251
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x00004059 File Offset: 0x00002259
		public int FrameRate
		{
			get
			{
				return this._FrameRate;
			}
			set
			{
				base.SetProperty<int>(ref this._FrameRate, value, null);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000406A File Offset: 0x0000226A
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x00004072 File Offset: 0x00002272
		public bool IsFrameRateEnabled
		{
			get
			{
				return this._IsFrameRateEnabled;
			}
			set
			{
				base.SetProperty<bool>(ref this._IsFrameRateEnabled, value, null);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00004083 File Offset: 0x00002283
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x00015680 File Offset: 0x00013880
		public bool EnableHighFrameRates
		{
			get
			{
				return this._EnableHighFrameRates;
			}
			set
			{
				base.SetProperty<bool>(ref this._EnableHighFrameRates, value, null);
				if (this.EnableHighFrameRates)
				{
					this.MaxFPS = 320;
					if (this.EnableVSync)
					{
						this.EnableVSync = false;
						return;
					}
				}
				else
				{
					this.MaxFPS = 90;
					if (this.FrameRate > 60)
					{
						this.FrameRate = 60;
					}
				}
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000408B File Offset: 0x0000228B
		// (set) Token: 0x060003BB RID: 955 RVA: 0x00004093 File Offset: 0x00002293
		public bool EnableVSync
		{
			get
			{
				return this._EnableVSync;
			}
			set
			{
				base.SetProperty<bool>(ref this._EnableVSync, value, null);
				if (this._EnableVSync && this.EnableHighFrameRates)
				{
					this.EnableHighFrameRates = false;
				}
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003BC RID: 956 RVA: 0x000040BB File Offset: 0x000022BB
		// (set) Token: 0x060003BD RID: 957 RVA: 0x000040C3 File Offset: 0x000022C3
		public bool DisplayFPS
		{
			get
			{
				return this._DisplayFPS;
			}
			set
			{
				base.SetProperty<bool>(ref this._DisplayFPS, value, null);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003BE RID: 958 RVA: 0x000040D4 File Offset: 0x000022D4
		// (set) Token: 0x060003BF RID: 959 RVA: 0x000040DC File Offset: 0x000022DC
		public bool IsAndroidBooted
		{
			get
			{
				return this._IsAndroidBooted;
			}
			set
			{
				base.SetProperty<bool>(ref this._IsAndroidBooted, value, null);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x000040ED File Offset: 0x000022ED
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x000040F5 File Offset: 0x000022F5
		public ABISetting ABISetting
		{
			get
			{
				return this._ABISetting;
			}
			set
			{
				base.SetProperty<ABISetting>(ref this._ABISetting, value, null);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00004106 File Offset: 0x00002306
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0000410E File Offset: 0x0000230E
		public bool Is64BitABIValid
		{
			get
			{
				return this._Is64BitABIValid;
			}
			set
			{
				base.SetProperty<bool>(ref this._Is64BitABIValid, value, null);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000411F File Offset: 0x0000231F
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x00004127 File Offset: 0x00002327
		public bool IsCustomABI
		{
			get
			{
				return this._IsCustomABI;
			}
			set
			{
				base.SetProperty<bool>(ref this._IsCustomABI, value, null);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00004138 File Offset: 0x00002338
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00004140 File Offset: 0x00002340
		public bool IsOpenedFromMultiInstane
		{
			get
			{
				return this._IsOpenedFromMultiInstane;
			}
			set
			{
				base.SetProperty<bool>(ref this._IsOpenedFromMultiInstane, value, null);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00004151 File Offset: 0x00002351
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x00004159 File Offset: 0x00002359
		public ICommand SaveCommand { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00004162 File Offset: 0x00002362
		public EngineData EngineData { get; } = new EngineData();

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000416A File Offset: 0x0000236A
		// (set) Token: 0x060003CC RID: 972 RVA: 0x00004172 File Offset: 0x00002372
		public Window Owner { get; private set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000417B File Offset: 0x0000237B
		public EngineSettingBase ParentView { get; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00004183 File Offset: 0x00002383
		// (set) Token: 0x060003CF RID: 975 RVA: 0x0000418B File Offset: 0x0000238B
		public bool CustomRamVisibility
		{
			get
			{
				return this.customRamVisibility;
			}
			set
			{
				base.SetProperty<bool>(ref this.customRamVisibility, value, null);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000419C File Offset: 0x0000239C
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x000041A4 File Offset: 0x000023A4
		public int CoreCount
		{
			get
			{
				return this.coreCount;
			}
			set
			{
				base.SetProperty<int>(ref this.coreCount, value, null);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x000041B5 File Offset: 0x000023B5
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x000041BD File Offset: 0x000023BD
		public bool CpuCoreCustomListVisibility
		{
			get
			{
				return this.cpuCoreCustomListVisibility;
			}
			set
			{
				base.SetProperty<bool>(ref this.cpuCoreCustomListVisibility, value, null);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x000041CE File Offset: 0x000023CE
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x000041D6 File Offset: 0x000023D6
		public bool MaxCoreWarningTextVisibility
		{
			get
			{
				return this.maxCoreWarningTextVisibility;
			}
			set
			{
				base.SetProperty<bool>(ref this.maxCoreWarningTextVisibility, value, null);
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x000041E7 File Offset: 0x000023E7
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x000041EF File Offset: 0x000023EF
		public ObservableCollection<EngineSettingModel> CPUList { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x000041F8 File Offset: 0x000023F8
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x00004200 File Offset: 0x00002400
		public ObservableCollection<EngineSettingModel> RamList { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00004209 File Offset: 0x00002409
		// (set) Token: 0x060003DB RID: 987 RVA: 0x000156DC File Offset: 0x000138DC
		public EngineSettingModel SelectedCPU
		{
			get
			{
				return this.selectedCPU;
			}
			set
			{
				if (value != null)
				{
					base.SetProperty<EngineSettingModel>(ref this.selectedCPU, value, null);
					this.UpdateCustomCPUDetails();
					this.CpuCoreCountChanged = (this.EngineData.CpuCores != ((this.SelectedCPU.PerformanceSettingType == PerformanceSetting.Custom) ? this.CpuCores : this.SelectedCPU.CoreCount));
					if (!this.CpuCoreCustomListVisibility)
					{
						EngineSettingModel engineSettingModel = this.SelectedCPU;
						int? num = (engineSettingModel != null) ? new int?(engineSettingModel.CoreCount) : null;
						int userSupportedVCPU = this._UserSupportedVCPU;
						if (num.GetValueOrDefault() == userSupportedVCPU & num != null)
						{
							goto IL_AC;
						}
					}
					if (!this.CpuCoreCustomListVisibility || this.CpuCores != this._UserSupportedVCPU)
					{
						this.MaxCoreWarningTextVisibility = false;
						return;
					}
					IL_AC:
					if (this.EngineData.CpuCores != 0)
					{
						this.MaxCoreWarningTextVisibility = true;
						Stats.SendMiscellaneousStatsAsync("core_warning_viewed", RegistryManager.RegistryManagers[this.OEM].UserGuid, RegistryManager.RegistryManagers[this.OEM].ClientVersion, "Engine-Settings", null, RegistryManager.Instance.Oem, null, null, RegistryManager.Instance.UserSelectedLocale, "Android", 0);
						return;
					}
				}
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00004211 File Offset: 0x00002411
		// (set) Token: 0x060003DD RID: 989 RVA: 0x00004219 File Offset: 0x00002419
		public EngineSettingModel SelectedRAM
		{
			get
			{
				return this.selectedRAM;
			}
			set
			{
				if (value != null)
				{
					base.SetProperty<EngineSettingModel>(ref this.selectedRAM, value, null);
					this.UpdateCustomRamDetails();
				}
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00004233 File Offset: 0x00002433
		private void UpdateCustomCPUDetails()
		{
			this.CpuCoreCustomListVisibility = (this.SelectedCPU.PerformanceSettingType == PerformanceSetting.Custom);
			this.CustomPerformanceSettingVisibility = (this.CustomRamVisibility || this.CpuCoreCustomListVisibility);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x00004263 File Offset: 0x00002463
		private void UpdateCustomRamDetails()
		{
			this.CustomRamVisibility = (this.SelectedRAM.PerformanceSettingType == PerformanceSetting.Custom);
			this.CustomPerformanceSettingVisibility = (this.CustomRamVisibility || this.CpuCoreCustomListVisibility);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00015808 File Offset: 0x00013A08
		public EngineSettingBaseViewModel(Window owner, string vmName, EngineSettingBase parentView, bool isOpenedFromMultiInstane = false, string oem = "", bool isEcoModeEnabled = false)
		{
			if ((double)EngineSettingBaseViewModel.UserMachineRAM >= 7782.4 && Environment.ProcessorCount >= 8)
			{
				this._HighEndMachine = true;
			}
			this.OEM = (string.IsNullOrEmpty(oem) ? "bgp" : oem);
			this.ParentView = parentView;
			this.Owner = owner;
			this.mIsEcoModeEnabled = isEcoModeEnabled;
			this._VmName = vmName;
			this._IsOpenedFromMultiInstane = isOpenedFromMultiInstane;
			this.SaveCommand = new RelayCommand2(new Func<object, bool>(this.CanSave), new Action<object>(this.Save));
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00004293 File Offset: 0x00002493
		private bool CanSave(object obj)
		{
			return this.IsDirty();
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000429B File Offset: 0x0000249B
		public static bool Is64BitABIValuesValid(string oem = "bgp")
		{
			return Constants.All64BitOems.Contains(oem);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00015994 File Offset: 0x00013B94
		public void Init()
		{
			this.Status = Status.None;
			this.CustomPerformanceSettingVisibility = false;
			this.CpuCoresList.Clear();
			this.PreferDedicatedGPUText = LocaleStrings.GetLocalizedString("STRING_USE_DEDICATED_GPU", "") + " " + LocaleStrings.GetLocalizedString("STRING_NVIDIA_ONLY", "");
			this.Ram = RegistryManager.RegistryManagers[this.OEM].Guest[this._VmName].Memory;
			this._UseDedicatedGPU = RegistryManager.RegistryManagers[this.OEM].ForceDedicatedGPU;
			this._GraphicsMode = (GraphicsMode)RegistryManager.RegistryManagers[this.OEM].Guest[this._VmName].GlRenderMode;
			this._GlMode = RegistryManager.RegistryManagers[this.OEM].Guest[this._VmName].GlMode;
			if (!string.Equals(RegistryManager.RegistryManagers[this.OEM].CurrentEngine, "raw", StringComparison.InvariantCulture))
			{
				this._UserSupportedVCPU = ((Environment.ProcessorCount > 8) ? 8 : Environment.ProcessorCount);
			}
			this.CpuCoresList = new ObservableCollection<int>(Enumerable.Range(1, this._UserSupportedVCPU));
			this.CpuCores = RegistryManager.RegistryManagers[this.OEM].Guest[this._VmName].VCPUs;
			this.CpuCores = Math.Min(this.CpuCores, this._UserSupportedVCPU);
			this.SetRam();
			this.BuildCPUCombinationList();
			this.BuildRAMCombinationList();
			this.SetSelectedRAMAndCPU();
			this.SetUseAdvancedGraphicMode(Utils.GetValueInBootParams("GlMode", this._VmName, string.Empty, this.OEM) == "2");
			this.EnableHighFrameRates = (RegistryManager.RegistryManagers[this.OEM].Guest[this._VmName].EnableHighFPS != 0);
			this.EnableVSync = (RegistryManager.RegistryManagers[this.OEM].Guest[this._VmName].EnableVSync != 0);
			this.FrameRate = RegistryManager.RegistryManagers[this.OEM].Guest[this._VmName].FPS;
			this.IsFrameRateEnabled = !this.mIsEcoModeEnabled;
			this.DisplayFPS = (RegistryManager.RegistryManagers[this.OEM].Guest[this._VmName].ShowFPS == 1);
			this.IsAndroidBooted = Utils.IsGuestBooted(this._VmName, "bgp");
			this.SetASTCTexture();
			if (EngineSettingBaseViewModel.Is64BitABIValuesValid(this.OEM))
			{
				this.Is64BitABIValid = true;
			}
			string valueInBootParams = Utils.GetValueInBootParams("abivalue", this._VmName, string.Empty, this.OEM);
			if (!string.IsNullOrEmpty(valueInBootParams))
			{
				if (EngineSettingBaseViewModel.Is64BitABIValuesValid(this.OEM))
				{
					ABISetting abisetting;
					if (valueInBootParams != null)
					{
						if (valueInBootParams == "7")
						{
							abisetting = ABISetting.Auto64;
							goto IL_305;
						}
						if (valueInBootParams == "15")
						{
							abisetting = ABISetting.ARM64;
							goto IL_305;
						}
					}
					abisetting = ABISetting.Custom;
					IL_305:
					ABISetting abisetting2 = abisetting;
					this.ABISetting = abisetting2;
				}
				else
				{
					ABISetting abisetting2;
					if (valueInBootParams != null)
					{
						if (valueInBootParams == "15")
						{
							abisetting2 = ABISetting.Auto;
							goto IL_33C;
						}
						if (valueInBootParams == "4")
						{
							abisetting2 = ABISetting.ARM;
							goto IL_33C;
						}
					}
					abisetting2 = ABISetting.Custom;
					IL_33C:
					ABISetting abisetting = abisetting2;
					this.ABISetting = abisetting;
				}
			}
			else if (EngineSettingBaseViewModel.Is64BitABIValuesValid(this.OEM))
			{
				Utils.UpdateValueInBootParams("abivalue", ABISetting.Auto64.GetDescription(), this._VmName, true, this.OEM);
			}
			else
			{
				Utils.UpdateValueInBootParams("abivalue", ABISetting.Auto.GetDescription(), this._VmName, true, this.OEM);
			}
			if (this.ABISetting == ABISetting.Custom)
			{
				this.IsCustomABI = true;
			}
			if (!string.IsNullOrEmpty(RegistryManager.RegistryManagers[this.OEM].AvailableGPUDetails))
			{
				this.IsGPUAvailable = true;
				this.UseDedicatedGPUText = LocaleStrings.GetLocalizedString("STRING_GPU_IN_USE", "") + " " + RegistryManager.RegistryManagers[this.OEM].AvailableGPUDetails;
			}
			this._CurrentGraphicsBitPattern = EngineSettingBaseViewModel.GenerateGraphicsBitPattern(this._GlMode, (int)this.GraphicsMode);
			base.NotifyPropertyChanged(string.Empty);
			this.LockForModification();
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00015DCC File Offset: 0x00013FCC
		private void SetSelectedRAMAndCPU()
		{
			if (this.CpuCores == Math.Min(this._UserSupportedVCPU, 4))
			{
				this.SelectedCPU = (from c in this.CPUList
				where c.PerformanceSettingType == PerformanceSetting.High
				select c).FirstOrDefault<EngineSettingModel>();
			}
			else if (this.CpuCores == Math.Min(this._UserSupportedVCPU, 2))
			{
				this.SelectedCPU = (from c in this.CPUList
				where c.PerformanceSettingType == PerformanceSetting.Medium
				select c).FirstOrDefault<EngineSettingModel>();
			}
			else if (this.CpuCores == Math.Min(this._UserSupportedVCPU, 1))
			{
				this.SelectedCPU = (from c in this.CPUList
				where c.PerformanceSettingType == PerformanceSetting.Low
				select c).FirstOrDefault<EngineSettingModel>();
			}
			else
			{
				this.SelectedCPU = (from c in this.CPUList
				where c.PerformanceSettingType == PerformanceSetting.Custom
				select c).FirstOrDefault<EngineSettingModel>();
			}
			if (this.Ram == Math.Min(this.MaxRam, this._HighEndMachine ? 4096 : 3072))
			{
				this.SelectedRAM = (from c in this.RamList
				where c.PerformanceSettingType == PerformanceSetting.High
				select c).FirstOrDefault<EngineSettingModel>();
				return;
			}
			if (this.Ram == Math.Min(this.MaxRam, 2048))
			{
				this.SelectedRAM = (from c in this.RamList
				where c.PerformanceSettingType == PerformanceSetting.Medium
				select c).FirstOrDefault<EngineSettingModel>();
				return;
			}
			if (this.Ram == Math.Min(this.MaxRam, 1024))
			{
				this.SelectedRAM = (from c in this.RamList
				where c.PerformanceSettingType == PerformanceSetting.Low
				select c).FirstOrDefault<EngineSettingModel>();
				return;
			}
			this.SelectedRAM = (from c in this.RamList
			where c.PerformanceSettingType == PerformanceSetting.Custom
			select c).FirstOrDefault<EngineSettingModel>();
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00016024 File Offset: 0x00014224
		private void SetASTCTexture()
		{
			this._ASTCOption = RegistryManager.RegistryManagers[this.OEM].Guest[this._VmName].ASTCOption;
			this.EnableHardwareDecoding = RegistryManager.RegistryManagers[this.OEM].Guest[this._VmName].IsHardwareAstcSupported;
			switch (this._ASTCOption)
			{
			case ASTCOption.Disabled:
				this.ASTCTexture = ASTCTexture.Disabled;
				return;
			case ASTCOption.SoftwareDecoding:
				this.ASTCTexture = ASTCTexture.Software;
				this.EnableCaching = false;
				return;
			case ASTCOption.SoftwareDecodingCache:
				this.ASTCTexture = ASTCTexture.Software;
				this.EnableCaching = true;
				return;
			case ASTCOption.HardwareDecoding:
				this.ASTCTexture = (this.EnableHardwareDecoding ? ASTCTexture.Hardware : ASTCTexture.Disabled);
				return;
			default:
				return;
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000160E0 File Offset: 0x000142E0
		private void SetASTCOption()
		{
			switch (this.ASTCTexture)
			{
			case ASTCTexture.Disabled:
				this._ASTCOption = ASTCOption.Disabled;
				this.EnableCaching = false;
				return;
			case ASTCTexture.Software:
				this._ASTCOption = (this.EnableCaching ? ASTCOption.SoftwareDecodingCache : ASTCOption.SoftwareDecoding);
				return;
			case ASTCTexture.Hardware:
				this._ASTCOption = ASTCOption.HardwareDecoding;
				this.EnableCaching = false;
				return;
			default:
				return;
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00016138 File Offset: 0x00014338
		private void BuildCPUCombinationList()
		{
			this.CPUList = new ObservableCollection<EngineSettingModel>();
			foreach (object obj in Enum.GetValues(typeof(PerformanceSetting)))
			{
				PerformanceSetting performanceSetting = (PerformanceSetting)obj;
				EngineSettingModel engineSettingModel = new EngineSettingModel();
				if (performanceSetting != PerformanceSetting.Custom)
				{
					string text = "";
					switch (performanceSetting)
					{
					case PerformanceSetting.High:
						text = LocaleStrings.GetLocalizedString("STRING_HIGH", "");
						engineSettingModel.CoreCount = Math.Min(this._UserSupportedVCPU, 4);
						break;
					case PerformanceSetting.Medium:
						text = LocaleStrings.GetLocalizedString("STRING_MEDIUM", "");
						engineSettingModel.CoreCount = Math.Min(this._UserSupportedVCPU, 2);
						break;
					case PerformanceSetting.Low:
						text = LocaleStrings.GetLocalizedString("STRING_LOW", "");
						engineSettingModel.CoreCount = Math.Min(this._UserSupportedVCPU, 1);
						break;
					}
					string text2 = (engineSettingModel.CoreCount == 1) ? LocaleStrings.GetLocalizedString("STRING_CORE", "") : LocaleStrings.GetLocalizedString("STRING_CORES", "");
					string text3 = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
					{
						engineSettingModel.CoreCount,
						text2
					});
					engineSettingModel.DisplayText = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
					{
						text,
						string.Format(CultureInfo.InvariantCulture, LocaleStrings.GetLocalizedString("STRING_BRACKETS_0", ""), new object[]
						{
							text3
						})
					});
				}
				else
				{
					engineSettingModel.DisplayText = LocaleStrings.GetLocalizedString("STRING_CUSTOM1", "");
					engineSettingModel.CoreCount = 1;
				}
				engineSettingModel.PerformanceSettingType = performanceSetting;
				this.CPUList.Add(engineSettingModel);
			}
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00016310 File Offset: 0x00014510
		private void BuildRAMCombinationList()
		{
			this.RamList = new ObservableCollection<EngineSettingModel>();
			foreach (object obj in Enum.GetValues(typeof(PerformanceSetting)))
			{
				PerformanceSetting performanceSetting = (PerformanceSetting)obj;
				EngineSettingModel engineSettingModel = new EngineSettingModel();
				if (performanceSetting != PerformanceSetting.Custom)
				{
					string text = "";
					switch (performanceSetting)
					{
					case PerformanceSetting.High:
						text = LocaleStrings.GetLocalizedString("STRING_HIGH", "");
						engineSettingModel.RAM = Math.Min(this.MaxRam, this._HighEndMachine ? 4096 : 3072);
						engineSettingModel.RAMInGB = Math.Min(Convert.ToInt32(this.MaxRam / 1024), this._HighEndMachine ? 4 : 3);
						break;
					case PerformanceSetting.Medium:
						text = LocaleStrings.GetLocalizedString("STRING_MEDIUM", "");
						engineSettingModel.RAM = Math.Min(this.MaxRam, 2048);
						engineSettingModel.RAMInGB = Math.Min(Convert.ToInt32(this.MaxRam / 1024), 2);
						break;
					case PerformanceSetting.Low:
						text = LocaleStrings.GetLocalizedString("STRING_LOW", "");
						engineSettingModel.RAM = Math.Min(this.MaxRam, 1024);
						engineSettingModel.RAMInGB = Math.Min(Convert.ToInt32(this.MaxRam / 1024), 1);
						break;
					}
					string text2 = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
					{
						engineSettingModel.RAMInGB,
						LocaleStrings.GetLocalizedString("STRING_MEMORY_GB", "")
					});
					engineSettingModel.DisplayText = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
					{
						text,
						string.Format(CultureInfo.InvariantCulture, LocaleStrings.GetLocalizedString("STRING_BRACKETS_0", ""), new object[]
						{
							text2
						})
					});
				}
				else
				{
					engineSettingModel.DisplayText = LocaleStrings.GetLocalizedString("STRING_CUSTOM1", "");
					engineSettingModel.RAM = 1024;
					engineSettingModel.RAMInGB = 1;
				}
				engineSettingModel.PerformanceSettingType = performanceSetting;
				this.RamList.Add(engineSettingModel);
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00016558 File Offset: 0x00014758
		private void SetRam()
		{
			this._MaxRam = (int)((double)EngineSettingBaseViewModel.UserMachineRAM * 0.75);
			if (this._MaxRam <= this._MinRam)
			{
				this.IsRamSliderEnabled = false;
			}
			else if (this._MaxRam >= 4096 && !Oem.Instance.IsAndroid64Bit)
			{
				this._MaxRam = 4096;
			}
			if (string.Equals(RegistryManager.RegistryManagers[this.OEM].CurrentEngine, "raw", StringComparison.InvariantCulture) && this._MaxRam >= 3072)
			{
				this._MaxRam = 3072;
			}
			this.Ram = Math.Min(this.Ram, this.MaxRam);
			string str;
			if (string.Equals(this._VmName, Strings.CurrentDefaultVmName, StringComparison.OrdinalIgnoreCase))
			{
				if (EngineSettingBaseViewModel.UserMachineRAM < 3072)
				{
					str = "600";
				}
				else if (SystemUtils.IsOs64Bit())
				{
					if (EngineSettingBaseViewModel.UserMachineRAM <= 4 * this._OneGB)
					{
						str = "900";
					}
					else if (EngineSettingBaseViewModel.UserMachineRAM <= 5 * this._OneGB)
					{
						str = "1200";
					}
					else if (EngineSettingBaseViewModel.UserMachineRAM <= 6 * this._OneGB)
					{
						str = "1500";
					}
					else if (EngineSettingBaseViewModel.UserMachineRAM < 8 * this._OneGB)
					{
						str = "1800";
					}
					else if (RegistryManager.RegistryManagers[this.OEM].CurrentEngine == "raw")
					{
						str = "3072";
					}
					else
					{
						str = "4096";
					}
				}
				else
				{
					str = "900";
				}
			}
			else
			{
				str = (SystemUtils.IsOs64Bit() ? ((EngineSettingBaseViewModel.UserMachineRAM < 4 * this._OneGB) ? "800" : "1100") : "600");
			}
			this.RecommendedRamText = LocaleStrings.GetLocalizedString("STRING_REC_MEM", "") + " " + str;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00016724 File Offset: 0x00014924
		private void CreateGraphicsCompatibilityDictionary()
		{
			object obj = this.lockObject;
			lock (obj)
			{
				if (!this._DictForGraphicsCompatibility.Any<KeyValuePair<int, bool>>())
				{
					string text = "";
					for (int i = 0; i < 4; i++)
					{
						if ((i & 1) == 0)
						{
							text += "4";
						}
						else
						{
							text += "1";
						}
						if ((i & 2) == 2)
						{
							text += " 2";
						}
						int exitCode = RunCommand.RunCmd(Path.Combine(RegistryStrings.InstallDir, "HD-GlCheck"), text, true, true, false, 10000).ExitCode;
						this._DictForGraphicsCompatibility.Add(i, exitCode == 0);
						text = "";
					}
				}
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x000167E8 File Offset: 0x000149E8
		private static int GenerateGraphicsBitPattern(int glMode, int glRenderMode)
		{
			int num = 0;
			if (glMode == 0)
			{
				num |= 0;
			}
			else if (glMode == 2)
			{
				num |= 2;
			}
			if (glRenderMode == 1)
			{
				num |= 1;
			}
			else if (glRenderMode == 4)
			{
				num |= 0;
			}
			return num;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001681C File Offset: 0x00014A1C
		private void ValidateGraphicMode(GraphicsMode oldMode_, GraphicsMode newMode_)
		{
			this._oldMode = oldMode_;
			this._newMode = newMode_;
			if (!this._DictForGraphicsCompatibility.Any<KeyValuePair<int, bool>>())
			{
				using (BackgroundWorker backgroundWorker = new BackgroundWorker())
				{
					this.ProgressMessage = string.Format(CultureInfo.CurrentCulture, LocaleStrings.GetLocalizedString("STRING_CHECKING_GRAPHICS_COMPATIBILITY", ""), new object[]
					{
						newMode_
					});
					backgroundWorker.DoWork += this.BcwWorker_DoWork;
					backgroundWorker.RunWorkerCompleted += this.BcwWorker_RunWorkerCompleted;
					backgroundWorker.RunWorkerAsync();
					return;
				}
			}
			this.HandleChangesForGlRenderModeValueChange();
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x000042A8 File Offset: 0x000024A8
		private void BcwWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			Dispatcher.CurrentDispatcher.BeginInvoke(new Action(delegate()
			{
				this.IsGraphicModeEnabled = true;
				this.HandleChangesForGlRenderModeValueChange();
			}), new object[0]);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x000042C7 File Offset: 0x000024C7
		private void BcwWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			this.IsGraphicModeEnabled = false;
			this.Status = Status.Progress;
			this.CreateGraphicsCompatibilityDictionary();
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000168C4 File Offset: 0x00014AC4
		private void HandleChangesForGlRenderModeValueChange()
		{
			int key = EngineSettingBaseViewModel.GenerateGraphicsBitPattern(this._GlMode, (int)this._GraphicsMode);
			if (this._DictForGraphicsCompatibility.ContainsKey(key) && this._DictForGraphicsCompatibility[key])
			{
				this.SetGraphicMode(this._newMode);
				return;
			}
			this.ErrorMessage = string.Format(CultureInfo.CurrentCulture, LocaleStrings.GetLocalizedString("STRING_GRAPHICS_NOT_SUPPORTED_ON_MACHINE", ""), new object[]
			{
				this._newMode
			});
			this.Status = Status.Error;
			this.SetGraphicMode(this._oldMode);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00016954 File Offset: 0x00014B54
		private void ValidateGraphicEngine(bool newEngine_)
		{
			this._newEngine = newEngine_;
			int num = EngineSettingBaseViewModel.GenerateGraphicsBitPattern(newEngine_ ? 2 : 0, (int)this.GraphicsMode);
			if (this._CurrentGraphicsBitPattern != num)
			{
				if (!this._DictForGraphicsCompatibility.Any<KeyValuePair<int, bool>>())
				{
					using (BackgroundWorker backgroundWorker = new BackgroundWorker())
					{
						this.ProgressMessage = string.Format(CultureInfo.CurrentCulture, LocaleStrings.GetLocalizedString("STRING_CHECKING_ENGINE_COMPATIBILITY", ""), new object[]
						{
							this._newEngine ? LocaleStrings.GetLocalizedString("STRING_ADVANCED_MODE", "") : LocaleStrings.GetLocalizedString("STRING_LEGACY_MODE", "")
						});
						backgroundWorker.DoWork += this.BcwForGlMode_DoWork;
						backgroundWorker.RunWorkerCompleted += this.BcwForGlMode_RunWorkerCompleted;
						backgroundWorker.RunWorkerAsync();
						return;
					}
				}
				this.ChangesForGlMode();
				return;
			}
			this.RevertToOriginalGlMode(this._CurrentGraphicsBitPattern);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x000042DD File Offset: 0x000024DD
		private void RevertToOriginalGlMode(int mGraphicsBitPattern)
		{
			if (mGraphicsBitPattern == 0 || mGraphicsBitPattern == 1)
			{
				this.SetUseAdvancedGraphicMode(false);
				return;
			}
			this.SetUseAdvancedGraphicMode(true);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x000042F5 File Offset: 0x000024F5
		private void BcwForGlMode_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			Dispatcher.CurrentDispatcher.BeginInvoke(new Action(delegate()
			{
				this.IsGraphicModeEnabled = true;
				this.Status = Status.None;
				this.ChangesForGlMode();
			}), new object[0]);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00016A48 File Offset: 0x00014C48
		private void ChangesForGlMode()
		{
			int key = EngineSettingBaseViewModel.GenerateGraphicsBitPattern(this._newEngine ? 2 : 0, (int)this.GraphicsMode);
			this.SetUseAdvancedGraphicMode(this.UseAdvancedGraphicEngine ? (this._DictForGraphicsCompatibility[key] && this._newEngine) : (!this._DictForGraphicsCompatibility[key] || this._newEngine));
			base.NotifyPropertyChanged("UseAdvancedGraphicEngine");
			Logger.Info(string.Format("Setting GlMode to {0}", this.UseAdvancedGraphicEngine ? 2 : 1));
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x000042C7 File Offset: 0x000024C7
		private void BcwForGlMode_DoWork(object sender, DoWorkEventArgs e)
		{
			this.IsGraphicModeEnabled = false;
			this.Status = Status.Progress;
			this.CreateGraphicsCompatibilityDictionary();
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00004314 File Offset: 0x00002514
		public void SetUseAdvancedGraphicMode(bool useAdvancedGraphicMode)
		{
			this._UseAdvancedGraphicEngine = useAdvancedGraphicMode;
			this.ParentView.SetAdvancedGraphicMode(this._UseAdvancedGraphicEngine);
			base.NotifyPropertyChanged("UseAdvancedGraphicEngine");
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00016AD8 File Offset: 0x00014CD8
		private void ValidateGPU(bool oldGPU_, bool newGPU_)
		{
			this._oldGPU = oldGPU_;
			this._newGPU = newGPU_;
			using (BackgroundWorker backgroundWorker = new BackgroundWorker())
			{
				backgroundWorker.DoWork += this.AddDedicatedGPUProfile_DoWork;
				backgroundWorker.RunWorkerCompleted += this.AddDedicatedGPUProfile_RunWorkerCompleted;
				backgroundWorker.RunWorkerAsync(this._newGPU);
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00016B4C File Offset: 0x00014D4C
		private void AddDedicatedGPUProfile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			Dispatcher.CurrentDispatcher.BeginInvoke(new Action(delegate()
			{
				this.Status = Status.None;
				this._UseDedicatedGPU = (this._newGPU && (bool)e.Result);
				this.NotifyPropertyChanged("UseDedicatedGPU");
			}), new object[0]);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00016B8C File Offset: 0x00014D8C
		public void SetGraphicMode(GraphicsMode newMode)
		{
			this._GraphicsMode = newMode;
			base.NotifyPropertyChanged("GraphicsMode");
			this.Status = ((this.EngineData.GraphicsMode == this.GraphicsMode) ? Status.None : Status.Warning);
			this.WarningMessage = string.Format(CultureInfo.CurrentCulture, LocaleStrings.GetLocalizedString(this.IsOpenedFromMultiInstane ? "STRING_LAUNCH_BLUESTACKS_AFTER_GRAPHICS_CHANGE" : "STRING_RESTART_BLUESTACKS_AFTER_GRAPHICS_CHANGE", ""), new object[]
			{
				(this.GraphicsMode == GraphicsMode.DirectX) ? "DirectX" : "OpenGL"
			});
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00016C14 File Offset: 0x00014E14
		private void AddDedicatedGPUProfile_DoWork(object sender, DoWorkEventArgs e)
		{
			this.Status = Status.Progress;
			bool flag = ForceDedicatedGPU.ToggleDedicatedGPU((bool)e.Argument, null);
			e.Result = flag;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00003C8E File Offset: 0x00001E8E
		protected virtual void Save(object param)
		{
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00004339 File Offset: 0x00002539
		public void NotifyPropertyChangedAllProperties()
		{
			base.NotifyPropertyChanged(string.Empty);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00016C48 File Offset: 0x00014E48
		public void LockForModification()
		{
			int ram = (this.SelectedRAM != null && this.SelectedRAM.PerformanceSettingType != PerformanceSetting.Custom) ? this.SelectedRAM.RAM : this.Ram;
			int cpuCores = (this.SelectedCPU != null && this.SelectedCPU.PerformanceSettingType != PerformanceSetting.Custom) ? this.SelectedCPU.CoreCount : this.CpuCores;
			this.EngineData.GraphicsMode = this.GraphicsMode;
			this.EngineData.UseAdvancedGraphicEngine = this.UseAdvancedGraphicEngine;
			this.EngineData.UseDedicatedGPU = this.UseDedicatedGPU;
			this.EngineData.ASTCOption = this._ASTCOption;
			this.EngineData.Ram = ram;
			this.EngineData.CpuCores = cpuCores;
			this.EngineData.FrameRate = this.FrameRate;
			this.EngineData.EnableHighFrameRates = this.EnableHighFrameRates;
			this.EngineData.EnableVSync = this.EnableVSync;
			this.EngineData.DisplayFPS = this.DisplayFPS;
			this.EngineData.ABISetting = this.ABISetting;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00016D5C File Offset: 0x00014F5C
		public bool IsDirty()
		{
			return this.IsRestartRequired() || this.EngineData.ASTCOption != this._ASTCOption || this.EngineData.FrameRate != this.FrameRate || this.EngineData.EnableHighFrameRates != this.EnableHighFrameRates || this.EngineData.EnableVSync != this.EnableVSync || this.EngineData.DisplayFPS != this.DisplayFPS;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00016DD8 File Offset: 0x00014FD8
		public bool IsRestartRequired()
		{
			int num = (this.SelectedRAM != null && this.SelectedRAM.PerformanceSettingType != PerformanceSetting.Custom) ? this.SelectedRAM.RAM : this.Ram;
			int num2 = (this.SelectedCPU != null && this.SelectedCPU.PerformanceSettingType != PerformanceSetting.Custom) ? this.SelectedCPU.CoreCount : this.CpuCores;
			return this.EngineData.GraphicsMode != this.GraphicsMode || this.EngineData.UseAdvancedGraphicEngine != this.UseAdvancedGraphicEngine || this.EngineData.UseDedicatedGPU != this.UseDedicatedGPU || this.EngineData.Ram != num || this.EngineData.CpuCores != num2 || this.EngineData.ABISetting != this.ABISetting;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00016EA8 File Offset: 0x000150A8
		public void SaveEngineSettings(string abiResult = "")
		{
			if (this.EngineData.GraphicsMode != this.GraphicsMode)
			{
				RegistryManager.RegistryManagers[this.OEM].Guest[this._VmName].GlRenderMode = (int)this.GraphicsMode;
			}
			if (this.EngineData.UseAdvancedGraphicEngine != this.UseAdvancedGraphicEngine)
			{
				RegistryManager.RegistryManagers[this.OEM].Guest[this._VmName].GlMode = (this.UseAdvancedGraphicEngine ? 2 : 1);
				Utils.UpdateValueInBootParams("GlMode", RegistryManager.RegistryManagers[this.OEM].Guest[this._VmName].GlMode.ToString(CultureInfo.InvariantCulture), this._VmName, true, this.OEM);
			}
			if (this.EngineData.UseDedicatedGPU != this.UseDedicatedGPU)
			{
				RegistryManager.RegistryManagers[this.OEM].ForceDedicatedGPU = this.UseDedicatedGPU;
			}
			if (this.EngineData.ASTCOption != this._ASTCOption)
			{
				Utils.SetAstcOption(this._VmName, this._ASTCOption, this.OEM);
				Stats.SendMiscellaneousStatsAsync("ASTCOption", RegistryManager.RegistryManagers[this.OEM].UserGuid, RegistryManager.RegistryManagers[this.OEM].ClientVersion, "ASTCOption", this._ASTCOption.ToString(), null, null, null, null, this._VmName, 0);
			}
			if (this.EngineData.CpuCores != ((this.SelectedCPU.PerformanceSettingType == PerformanceSetting.Custom) ? this.CpuCores : this.SelectedCPU.CoreCount))
			{
				RegistryManager.RegistryManagers[this.OEM].Guest[this._VmName].VCPUs = ((this.SelectedCPU.PerformanceSettingType == PerformanceSetting.Custom) ? this.CpuCores : this.SelectedCPU.CoreCount);
			}
			if (this.EngineData.Ram != ((this.SelectedRAM.PerformanceSettingType == PerformanceSetting.Custom) ? this.Ram : this.SelectedRAM.RAM))
			{
				RegistryManager.RegistryManagers[this.OEM].Guest[this._VmName].Memory = ((this.SelectedRAM.PerformanceSettingType == PerformanceSetting.Custom) ? this.Ram : this.SelectedRAM.RAM);
			}
			if (this.EngineData.EnableHighFrameRates != this.EnableHighFrameRates)
			{
				RegistryManager.RegistryManagers[this.OEM].Guest[this._VmName].EnableHighFPS = (this.EnableHighFrameRates ? 1 : 0);
			}
			if (this.EngineData.EnableVSync != this.EnableVSync)
			{
				RegistryManager.RegistryManagers[this.OEM].Guest[this._VmName].EnableVSync = (this.EnableVSync ? 1 : 0);
				Utils.SendEnableVSyncToInstanceASync(this.EnableVSync, this._VmName, "bgp");
			}
			if (this.EngineData.DisplayFPS != this.DisplayFPS)
			{
				RegistryManager.RegistryManagers[this.OEM].Guest[this._VmName].ShowFPS = (this.DisplayFPS ? 1 : 0);
				Utils.SendShowFPSToInstanceASync(this._VmName, RegistryManager.RegistryManagers[this.OEM].Guest[this._VmName].ShowFPS);
				Stats.SendMiscellaneousStatsAsync("DisplayFPSCheckboxClicked", RegistryManager.RegistryManagers[this.OEM].UserGuid, RegistryManager.RegistryManagers[this.OEM].ClientVersion, "enginesettings", this.DisplayFPS ? "checked" : "unchecked", null, this._VmName, null, null, "Android", 0);
			}
			if (this.EngineData.FrameRate != this.FrameRate)
			{
				RegistryManager.RegistryManagers[this.OEM].Guest[this._VmName].FPS = this.FrameRate;
				Utils.UpdateValueInBootParams("fps", RegistryManager.RegistryManagers[this.OEM].Guest[this._VmName].FPS.ToString(CultureInfo.InvariantCulture), this._VmName, true, this.OEM);
				Utils.SendChangeFPSToInstanceASync(this._VmName, int.MaxValue, "bgp");
			}
			if (string.Equals(abiResult, "ok", StringComparison.InvariantCulture))
			{
				Utils.UpdateValueInBootParams("abivalue", this.ABISetting.GetDescription(), this._VmName, true, this.OEM);
				Stats.SendMiscellaneousStatsAsync("ABIChanged", RegistryManager.RegistryManagers[this.OEM].UserGuid, RegistryManager.RegistryManagers[this.OEM].ClientVersion, this.ABISetting.ToString(), "bgp", null, null, null, null, "Android", 0);
			}
			if (this.CpuCoreCountChanged && this.MaxCoreWarningTextVisibility)
			{
				Stats.SendMiscellaneousStatsAsync("core_all_assigned", RegistryManager.RegistryManagers[this.OEM].UserGuid, RegistryManager.RegistryManagers[this.OEM].ClientVersion, "Engine-Settings", null, RegistryManager.Instance.Oem, null, null, RegistryManager.Instance.UserSelectedLocale, "Android", 0);
			}
			this.LockForModification();
			Stats.SendMiscellaneousStatsAsync("Setting-save", RegistryManager.RegistryManagers[this.OEM].UserGuid, RegistryManager.RegistryManagers[this.OEM].ClientVersion, "Engine-Settings", "", null, this._VmName, null, null, "Android", 0);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0001746C File Offset: 0x0001566C
		protected void AddToastPopup(string message)
		{
			try
			{
				if (this.mToastPopup == null)
				{
					this.mToastPopup = new CustomToastPopupControl(this.Owner);
				}
				this.mToastPopup.Init(this.Owner, message, null, null, HorizontalAlignment.Center, VerticalAlignment.Bottom, null, 12, null, null, false, false);
				this.mToastPopup.ShowPopup(1.3);
			}
			catch (Exception ex)
			{
				Logger.Error("Exception in showing toast popup: " + ex.ToString());
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00017500 File Offset: 0x00015700
		protected void AddToastPopupUserControl(string message)
		{
			try
			{
				if (this.mToastPopup == null)
				{
					this.mToastPopup = new CustomToastPopupControl(this.ParentView);
				}
				this.mToastPopup.Init(this.Owner, message, null, null, HorizontalAlignment.Center, VerticalAlignment.Bottom, null, 12, null, null, false, false);
				this.mToastPopup.ShowPopup(1.3);
			}
			catch (Exception ex)
			{
				Logger.Error("Exception in showing toast popup: " + ex.ToString());
			}
		}

		// Token: 0x0400019E RID: 414
		private bool mIsEcoModeEnabled;

		// Token: 0x0400019F RID: 415
		private string _OEM = "bgp";

		// Token: 0x040001A0 RID: 416
		private int _MinRam = 600;

		// Token: 0x040001A1 RID: 417
		private int _MaxRam = 4096;

		// Token: 0x040001A2 RID: 418
		private static int? _RamInMB;

		// Token: 0x040001A3 RID: 419
		private int _UserMachineCpuCores = Environment.ProcessorCount;

		// Token: 0x040001A4 RID: 420
		private int _UserSupportedVCPU = 1;

		// Token: 0x040001A5 RID: 421
		private int _MaxFPS = 60;

		// Token: 0x040001A6 RID: 422
		private Status _Status;

		// Token: 0x040001A7 RID: 423
		private bool _IsGraphicModeEnabled = true;

		// Token: 0x040001A8 RID: 424
		private int _GlMode;

		// Token: 0x040001A9 RID: 425
		private int _CurrentGraphicsBitPattern;

		// Token: 0x040001AA RID: 426
		private GraphicsMode _GraphicsMode;

		// Token: 0x040001AB RID: 427
		private Uri _DirectXUri = new Uri(WebHelper.GetUrlWithParams(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
		{
			WebHelper.GetServerHost(),
			"help_articles"
		}), null, null, null) + "&article=bgp_kk_compat_version");

		// Token: 0x040001AC RID: 428
		private string _WarningMessage;

		// Token: 0x040001AD RID: 429
		private string _ProgressMessage;

		// Token: 0x040001AE RID: 430
		private string _ErrorMessage;

		// Token: 0x040001AF RID: 431
		private bool _UseAdvancedGraphicEngine;

		// Token: 0x040001B0 RID: 432
		private bool _UseDedicatedGPU;

		// Token: 0x040001B1 RID: 433
		private string _PreferDedicatedGPUText;

		// Token: 0x040001B2 RID: 434
		private string _UseDedicatedGPUText;

		// Token: 0x040001B3 RID: 435
		private bool _IsGPUAvailable;

		// Token: 0x040001B4 RID: 436
		private ASTCTexture _ASTCTexture;

		// Token: 0x040001B5 RID: 437
		private ASTCOption _ASTCOption;

		// Token: 0x040001B6 RID: 438
		private bool _EnableHardwareDecoding;

		// Token: 0x040001B7 RID: 439
		private bool _EnableCaching;

		// Token: 0x040001B8 RID: 440
		private Visibility _CpuMemoryVisibility = Visibility.Collapsed;

		// Token: 0x040001B9 RID: 441
		private bool _customPerformanceSettingVisibility;

		// Token: 0x040001BA RID: 442
		private ObservableCollection<string> _PerformanceSettingList = new ObservableCollection<string>();

		// Token: 0x040001BC RID: 444
		private int _CpuCores = 2;

		// Token: 0x040001BD RID: 445
		private ObservableCollection<int> _CpuCoresList = new ObservableCollection<int>();

		// Token: 0x040001BE RID: 446
		private int _Ram = 1100;

		// Token: 0x040001BF RID: 447
		private bool _IsRamSliderEnabled = true;

		// Token: 0x040001C0 RID: 448
		private string _RecommendedRamText;

		// Token: 0x040001C1 RID: 449
		private int _FrameRate = 60;

		// Token: 0x040001C2 RID: 450
		private bool _IsFrameRateEnabled;

		// Token: 0x040001C3 RID: 451
		private bool _EnableHighFrameRates;

		// Token: 0x040001C4 RID: 452
		private bool _EnableVSync;

		// Token: 0x040001C5 RID: 453
		private bool _DisplayFPS;

		// Token: 0x040001C6 RID: 454
		private bool _IsAndroidBooted;

		// Token: 0x040001C7 RID: 455
		private ABISetting _ABISetting = ABISetting.Auto;

		// Token: 0x040001C8 RID: 456
		private bool _Is64BitABIValid;

		// Token: 0x040001C9 RID: 457
		private bool _IsCustomABI;

		// Token: 0x040001CA RID: 458
		private bool _IsOpenedFromMultiInstane;

		// Token: 0x040001CB RID: 459
		private readonly string _VmName;

		// Token: 0x040001CC RID: 460
		private readonly int _OneGB = 1024;

		// Token: 0x040001CD RID: 461
		private Dictionary<int, bool> _DictForGraphicsCompatibility = new Dictionary<int, bool>();

		// Token: 0x040001D1 RID: 465
		private bool _HighEndMachine;

		// Token: 0x040001D2 RID: 466
		private EngineSettingModel selectedCPU;

		// Token: 0x040001D3 RID: 467
		private EngineSettingModel selectedRAM;

		// Token: 0x040001D5 RID: 469
		private bool customRamVisibility;

		// Token: 0x040001D6 RID: 470
		private int coreCount;

		// Token: 0x040001D7 RID: 471
		private bool cpuCoreCustomListVisibility;

		// Token: 0x040001D8 RID: 472
		private bool maxCoreWarningTextVisibility;

		// Token: 0x040001DB RID: 475
		private object lockObject = new object();

		// Token: 0x040001DC RID: 476
		private GraphicsMode _newMode;

		// Token: 0x040001DD RID: 477
		private GraphicsMode _oldMode;

		// Token: 0x040001DE RID: 478
		private bool _newEngine;

		// Token: 0x040001DF RID: 479
		private bool _oldGPU;

		// Token: 0x040001E0 RID: 480
		private bool _newGPU;

		// Token: 0x040001E1 RID: 481
		private CustomToastPopupControl mToastPopup;
	}
}
