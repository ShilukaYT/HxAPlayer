using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace BlueStacks.Common
{
	// Token: 0x0200009E RID: 158
	[Serializable]
	public class ResolutionModel : INotifyPropertyChanged
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600033B RID: 827 RVA: 0x00014DF4 File Offset: 0x00012FF4
		// (remove) Token: 0x0600033C RID: 828 RVA: 0x00014E2C File Offset: 0x0001302C
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600033D RID: 829 RVA: 0x00003AC2 File Offset: 0x00001CC2
		protected void OnPropertyChanged(string property)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(property));
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600033E RID: 830 RVA: 0x00003ADB File Offset: 0x00001CDB
		// (set) Token: 0x0600033F RID: 831 RVA: 0x00003AE3 File Offset: 0x00001CE3
		public OrientationType OrientationType
		{
			get
			{
				return this.orientationType;
			}
			set
			{
				if (this.orientationType != value)
				{
					this.orientationType = value;
					this.OnPropertyChanged("OrientationType");
				}
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00003B00 File Offset: 0x00001D00
		// (set) Token: 0x06000341 RID: 833 RVA: 0x00003B08 File Offset: 0x00001D08
		public string OrientationName
		{
			get
			{
				return this.orientationName;
			}
			set
			{
				if (this.orientationName != value)
				{
					this.orientationName = value;
					this.OnPropertyChanged("OrientationName");
				}
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000342 RID: 834 RVA: 0x00003B2A File Offset: 0x00001D2A
		// (set) Token: 0x06000343 RID: 835 RVA: 0x00003B32 File Offset: 0x00001D32
		public Dictionary<string, string> AvailableResolutionsDict
		{
			get
			{
				return this.availableResolutionsDict;
			}
			set
			{
				if (this.availableResolutionsDict != value)
				{
					this.availableResolutionsDict = value;
					this.OnPropertyChanged("AvailableResolutionsDict");
				}
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00003B4F File Offset: 0x00001D4F
		// (set) Token: 0x06000345 RID: 837 RVA: 0x00014E64 File Offset: 0x00013064
		public string CombinedResolution
		{
			get
			{
				return this.combinedResolution;
			}
			set
			{
				if (this.combinedResolution != value)
				{
					this.combinedResolution = value;
					this.OnPropertyChanged("CombinedResolution");
					int resolutionWidth;
					int resolutionHeight;
					ResolutionModel.ConvertResolution(this.availableResolutionsDict.ContainsKey(this.combinedResolution) ? this.availableResolutionsDict[this.combinedResolution] : this.combinedResolution, out resolutionWidth, out resolutionHeight);
					this.ResolutionWidth = resolutionWidth;
					this.ResolutionHeight = resolutionHeight;
				}
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000346 RID: 838 RVA: 0x00003B57 File Offset: 0x00001D57
		// (set) Token: 0x06000347 RID: 839 RVA: 0x00003B5F File Offset: 0x00001D5F
		public string SystemDefaultResolution
		{
			get
			{
				return this.systemDefaultResolution;
			}
			set
			{
				if (this.systemDefaultResolution != value)
				{
					this.systemDefaultResolution = value;
					this.OnPropertyChanged("SystemDefaultResolution");
				}
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000348 RID: 840 RVA: 0x00003B81 File Offset: 0x00001D81
		// (set) Token: 0x06000349 RID: 841 RVA: 0x00003B89 File Offset: 0x00001D89
		public int ResolutionWidth
		{
			get
			{
				return this.mResolutionWidth;
			}
			set
			{
				if (this.mResolutionWidth != value)
				{
					this.mResolutionWidth = value;
					this.OnPropertyChanged("ResolutionWidth");
				}
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600034A RID: 842 RVA: 0x00003BA6 File Offset: 0x00001DA6
		// (set) Token: 0x0600034B RID: 843 RVA: 0x00003BAE File Offset: 0x00001DAE
		public int ResolutionHeight
		{
			get
			{
				return this.mResolutionHeight;
			}
			set
			{
				if (this.mResolutionHeight != value)
				{
					this.mResolutionHeight = value;
					this.OnPropertyChanged("ResolutionHeight");
				}
			}
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00014ED4 File Offset: 0x000130D4
		private static void ConvertResolution(string resolution, out int width, out int height)
		{
			string[] array = resolution.Split(new char[]
			{
				'x'
			});
			width = int.Parse(array[0].Trim(), CultureInfo.InvariantCulture);
			height = int.Parse(array[1].Trim(), CultureInfo.InvariantCulture);
		}

		// Token: 0x0400016C RID: 364
		private OrientationType orientationType;

		// Token: 0x0400016D RID: 365
		private Dictionary<string, string> availableResolutionsDict;

		// Token: 0x0400016E RID: 366
		private string combinedResolution;

		// Token: 0x0400016F RID: 367
		private string systemDefaultResolution;

		// Token: 0x04000170 RID: 368
		private int mResolutionWidth;

		// Token: 0x04000171 RID: 369
		private int mResolutionHeight;

		// Token: 0x04000173 RID: 371
		private string orientationName;
	}
}
