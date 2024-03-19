using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using Newtonsoft.Json;

namespace BlueStacks.Common
{
	// Token: 0x020000E1 RID: 225
	[Serializable]
	public class MergedMacroConfiguration : INotifyPropertyChanged
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060005CC RID: 1484 RVA: 0x0001C100 File Offset: 0x0001A300
		// (remove) Token: 0x060005CD RID: 1485 RVA: 0x0001C138 File Offset: 0x0001A338
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x060005CE RID: 1486 RVA: 0x000054C2 File Offset: 0x000036C2
		protected void OnPropertyChanged(string property)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(property));
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x000054DB File Offset: 0x000036DB
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x000054E3 File Offset: 0x000036E3
		[JsonIgnore]
		public int Tag { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x000054EC File Offset: 0x000036EC
		[JsonProperty("MacrosToRun")]
		public ObservableCollection<string> MacrosToRun { get; } = new ObservableCollection<string>();

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x000054F4 File Offset: 0x000036F4
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x000054FC File Offset: 0x000036FC
		[JsonProperty("LoopCount")]
		public int LoopCount
		{
			get
			{
				return this.mLoopCount;
			}
			set
			{
				this.mLoopCount = value;
				this.OnPropertyChanged("LoopCount");
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00005510 File Offset: 0x00003710
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x00005518 File Offset: 0x00003718
		[JsonProperty("LoopInterval")]
		public int LoopInterval
		{
			get
			{
				return this.mLoopInterval;
			}
			set
			{
				this.mLoopInterval = value;
				this.OnPropertyChanged("LoopInterval");
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x0000552C File Offset: 0x0000372C
		// (set) Token: 0x060005D7 RID: 1495 RVA: 0x00005534 File Offset: 0x00003734
		[JsonProperty("DelayNextScript")]
		public int DelayNextScript
		{
			get
			{
				return this.mDelayNextScript;
			}
			set
			{
				this.mDelayNextScript = value;
				this.OnPropertyChanged("DelayNextScript");
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x00005548 File Offset: 0x00003748
		// (set) Token: 0x060005D9 RID: 1497 RVA: 0x00005550 File Offset: 0x00003750
		[JsonProperty("Acceleration")]
		public double Acceleration
		{
			get
			{
				return this.mAcceleration;
			}
			set
			{
				this.mAcceleration = value;
				this.OnPropertyChanged("Acceleration");
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00005564 File Offset: 0x00003764
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x0000556C File Offset: 0x0000376C
		[JsonIgnore]
		public bool IsGroupButtonVisible
		{
			get
			{
				return this.mIsGroupButtonVisible;
			}
			set
			{
				this.mIsGroupButtonVisible = value;
				this.OnPropertyChanged("IsGroupButtonVisible");
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060005DC RID: 1500 RVA: 0x00005580 File Offset: 0x00003780
		// (set) Token: 0x060005DD RID: 1501 RVA: 0x00005588 File Offset: 0x00003788
		[JsonIgnore]
		public bool IsUnGroupButtonVisible
		{
			get
			{
				return this.mIsUnGroupButtonVisible;
			}
			set
			{
				this.mIsUnGroupButtonVisible = value;
				this.OnPropertyChanged("IsUnGroupButtonVisible");
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x0000559C File Offset: 0x0000379C
		// (set) Token: 0x060005DF RID: 1503 RVA: 0x000055A4 File Offset: 0x000037A4
		[JsonIgnore]
		public bool IsSettingsVisible
		{
			get
			{
				return this.mIsSettingsVisible;
			}
			set
			{
				this.mIsSettingsVisible = value;
				this.OnPropertyChanged("IsSettingsVisible");
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x000055B8 File Offset: 0x000037B8
		// (set) Token: 0x060005E1 RID: 1505 RVA: 0x000055C0 File Offset: 0x000037C0
		[JsonIgnore]
		public bool IsFirstListBoxItem
		{
			get
			{
				return this.mIsFirstListBoxItem;
			}
			set
			{
				this.mIsFirstListBoxItem = value;
				this.OnPropertyChanged("IsFirstListBoxItem");
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060005E2 RID: 1506 RVA: 0x000055D4 File Offset: 0x000037D4
		// (set) Token: 0x060005E3 RID: 1507 RVA: 0x000055DC File Offset: 0x000037DC
		[JsonIgnore]
		public bool IsLastListBoxItem
		{
			get
			{
				return this.mIsLastListBoxItem;
			}
			set
			{
				this.mIsLastListBoxItem = value;
				this.OnPropertyChanged("IsLastListBoxItem");
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x000055F0 File Offset: 0x000037F0
		[JsonIgnore]
		public IEnumerable<string> AccelerationOptions
		{
			get
			{
				int num;
				for (int i = -1; i <= 8; i = num + 1)
				{
					yield return ((double)(i + 2) * 0.5).ToString(CultureInfo.InvariantCulture) + "x";
					num = i;
				}
				yield break;
			}
		}

		// Token: 0x040002D3 RID: 723
		private int mLoopCount = 1;

		// Token: 0x040002D4 RID: 724
		private int mLoopInterval;

		// Token: 0x040002D5 RID: 725
		private int mDelayNextScript;

		// Token: 0x040002D6 RID: 726
		private double mAcceleration = 1.0;

		// Token: 0x040002D7 RID: 727
		private bool mIsGroupButtonVisible;

		// Token: 0x040002D8 RID: 728
		private bool mIsUnGroupButtonVisible;

		// Token: 0x040002D9 RID: 729
		private bool mIsSettingsVisible;

		// Token: 0x040002DA RID: 730
		private bool mIsFirstListBoxItem;

		// Token: 0x040002DB RID: 731
		private bool mIsLastListBoxItem;
	}
}
