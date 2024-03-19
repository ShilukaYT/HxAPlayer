using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlueStacks.Common
{
	// Token: 0x02000054 RID: 84
	[Serializable]
	public class AppSettings
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00002E67 File Offset: 0x00001067
		// (set) Token: 0x060001DE RID: 478 RVA: 0x00002E6F File Offset: 0x0000106F
		[JsonProperty("IsKeymappingTooltipShown")]
		public bool IsKeymappingTooltipShown
		{
			get
			{
				return this.mIsKeymappingTooltipShown;
			}
			set
			{
				this.mIsKeymappingTooltipShown = value;
				AppConfigurationManager.Save();
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001DF RID: 479 RVA: 0x00002E7D File Offset: 0x0000107D
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x00002E85 File Offset: 0x00001085
		[JsonProperty("IsDefaultSchemeRecorded")]
		public bool IsDefaultSchemeRecorded
		{
			get
			{
				return this.mIsDefaultSchemeRecorded;
			}
			set
			{
				this.mIsDefaultSchemeRecorded = value;
				AppConfigurationManager.Save();
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x00002E93 File Offset: 0x00001093
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x00002E9B File Offset: 0x0000109B
		[JsonProperty("IsAppOnboardingCompleted")]
		public bool IsAppOnboardingCompleted
		{
			get
			{
				return this.mIsAppOnboardingCompleted;
			}
			set
			{
				this.mIsAppOnboardingCompleted = value;
				AppConfigurationManager.Save();
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x00002EA9 File Offset: 0x000010A9
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x00002EB1 File Offset: 0x000010B1
		[JsonProperty("IsGeneralAppOnBoardingCompleted")]
		public bool IsGeneralAppOnBoardingCompleted
		{
			get
			{
				return this.mIsGeneralAppOnBoardingCompleted;
			}
			set
			{
				this.mIsGeneralAppOnBoardingCompleted = value;
				AppConfigurationManager.Save();
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00002EBF File Offset: 0x000010BF
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x00002EC7 File Offset: 0x000010C7
		[JsonProperty("IsCloseGuidanceOnboardingCompleted")]
		public bool IsCloseGuidanceOnboardingCompleted
		{
			get
			{
				return this.mIsCloseGuidanceOnboardingCompleted;
			}
			set
			{
				this.mIsCloseGuidanceOnboardingCompleted = value;
				AppConfigurationManager.Save();
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00002ED5 File Offset: 0x000010D5
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x00002EDD File Offset: 0x000010DD
		[JsonProperty("AppInstallTime")]
		public string AppInstallTime
		{
			get
			{
				return this.mAppInstallTime;
			}
			set
			{
				this.mAppInstallTime = value;
				AppConfigurationManager.Save();
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00002EEB File Offset: 0x000010EB
		// (set) Token: 0x060001EA RID: 490 RVA: 0x00002EF3 File Offset: 0x000010F3
		[JsonProperty("IsForcedLandscapeEnabled")]
		public bool IsForcedLandscapeEnabled
		{
			get
			{
				return this.mIsForcedLandscapeEnabled;
			}
			set
			{
				this.mIsForcedLandscapeEnabled = value;
				AppConfigurationManager.Save();
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00002F01 File Offset: 0x00001101
		// (set) Token: 0x060001EC RID: 492 RVA: 0x00002F09 File Offset: 0x00001109
		[JsonProperty("IsForcedPortraitEnabled")]
		public bool IsForcedPortraitEnabled
		{
			get
			{
				return this.mIsForcedPortraitEnabled;
			}
			set
			{
				this.mIsForcedPortraitEnabled = value;
				AppConfigurationManager.Save();
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001ED RID: 493 RVA: 0x00002F17 File Offset: 0x00001117
		// (set) Token: 0x060001EE RID: 494 RVA: 0x00002F1F File Offset: 0x0000111F
		[JsonProperty("CfgStored")]
		public string CfgStored
		{
			get
			{
				return this.mCfgStored;
			}
			set
			{
				this.mCfgStored = value;
				AppConfigurationManager.Save();
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001EF RID: 495 RVA: 0x00002F2D File Offset: 0x0000112D
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x00002F35 File Offset: 0x00001135
		[JsonExtensionData]
		public IDictionary<string, object> ExtraData { get; set; }

		// Token: 0x040000BC RID: 188
		private bool mIsKeymappingTooltipShown;

		// Token: 0x040000BD RID: 189
		private bool mIsDefaultSchemeRecorded;

		// Token: 0x040000BE RID: 190
		private bool mIsAppOnboardingCompleted = true;

		// Token: 0x040000BF RID: 191
		private bool mIsGeneralAppOnBoardingCompleted = true;

		// Token: 0x040000C0 RID: 192
		private bool mIsCloseGuidanceOnboardingCompleted = true;

		// Token: 0x040000C1 RID: 193
		private string mAppInstallTime = string.Empty;

		// Token: 0x040000C2 RID: 194
		private bool mIsForcedLandscapeEnabled;

		// Token: 0x040000C3 RID: 195
		private bool mIsForcedPortraitEnabled;

		// Token: 0x040000C4 RID: 196
		private string mCfgStored = "";
	}
}
