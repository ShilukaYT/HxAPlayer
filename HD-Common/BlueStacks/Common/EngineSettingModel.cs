using System;
using System.ComponentModel;

namespace BlueStacks.Common
{
	// Token: 0x020000A9 RID: 169
	public class EngineSettingModel : INotifyPropertyChanged
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000445E File Offset: 0x0000265E
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x00004466 File Offset: 0x00002666
		public PerformanceSetting PerformanceSettingType
		{
			get
			{
				return this.performanceSettingType;
			}
			set
			{
				if (this.performanceSettingType != value)
				{
					this.performanceSettingType = value;
					this.OnPropertyChanged("PerformanceSettingType");
				}
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x00004483 File Offset: 0x00002683
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0000448B File Offset: 0x0000268B
		public string DisplayText
		{
			get
			{
				return this.displayText;
			}
			set
			{
				if (this.displayText != value)
				{
					this.displayText = value;
					this.OnPropertyChanged("DisplayText");
				}
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x000044AD File Offset: 0x000026AD
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x000044B5 File Offset: 0x000026B5
		public int CoreCount
		{
			get
			{
				return this.coreCount;
			}
			set
			{
				if (this.coreCount != value)
				{
					this.coreCount = value;
					this.OnPropertyChanged("CoreCount");
				}
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x000044D2 File Offset: 0x000026D2
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x000044DA File Offset: 0x000026DA
		public int RAM
		{
			get
			{
				return this.ram;
			}
			set
			{
				if (this.ram != value)
				{
					this.ram = value;
					this.OnPropertyChanged("RAM");
				}
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x000044F7 File Offset: 0x000026F7
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x000044FF File Offset: 0x000026FF
		public int RAMInGB
		{
			get
			{
				return this.ramInGB;
			}
			set
			{
				if (this.ramInGB != value)
				{
					this.ramInGB = value;
					this.OnPropertyChanged("RAMInGB");
				}
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000432 RID: 1074 RVA: 0x000175E8 File Offset: 0x000157E8
		// (remove) Token: 0x06000433 RID: 1075 RVA: 0x00017620 File Offset: 0x00015820
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000434 RID: 1076 RVA: 0x0000451C File Offset: 0x0000271C
		protected void OnPropertyChanged(string property)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(property));
		}

		// Token: 0x040001F8 RID: 504
		private PerformanceSetting performanceSettingType;

		// Token: 0x040001F9 RID: 505
		private string displayText;

		// Token: 0x040001FA RID: 506
		private int coreCount;

		// Token: 0x040001FB RID: 507
		private int ram;

		// Token: 0x040001FC RID: 508
		private int ramInGB;
	}
}
