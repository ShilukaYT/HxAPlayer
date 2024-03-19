using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace BlueStacks.Common
{
	// Token: 0x0200009B RID: 155
	[Serializable]
	public class DisplaySettingsBaseModel : INotifyPropertyChanged
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600032A RID: 810 RVA: 0x00014AA4 File Offset: 0x00012CA4
		// (remove) Token: 0x0600032B RID: 811 RVA: 0x00014ADC File Offset: 0x00012CDC
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x0600032C RID: 812 RVA: 0x000039E8 File Offset: 0x00001BE8
		public void OnPropertyChanged(string name)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(name));
			}
			CommandManager.InvalidateRequerySuggested();
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00003A07 File Offset: 0x00001C07
		// (set) Token: 0x0600032E RID: 814 RVA: 0x00003A0F File Offset: 0x00001C0F
		public ObservableCollection<ResolutionModel> ResolutionsList { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00003A18 File Offset: 0x00001C18
		// (set) Token: 0x06000330 RID: 816 RVA: 0x00003A20 File Offset: 0x00001C20
		public ResolutionModel ResolutionType
		{
			get
			{
				return this.mResolutionType;
			}
			set
			{
				if (value != null && this.mResolutionType != value)
				{
					this.mResolutionType = value;
					this.OnPropertyChanged("ResolutionType");
				}
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00003A40 File Offset: 0x00001C40
		// (set) Token: 0x06000332 RID: 818 RVA: 0x00003A48 File Offset: 0x00001C48
		public string Dpi
		{
			get
			{
				return this.mDpi;
			}
			set
			{
				this.mDpi = value;
				this.OnPropertyChanged("Dpi");
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00003A5C File Offset: 0x00001C5C
		public DisplaySettingsBaseModel(string dpi, int resolutionWidth, int resolutionHeight)
		{
			this.BuildResolutionsList();
			this.InitDisplaySettingsBaseModel(dpi, resolutionWidth, resolutionHeight);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00014B14 File Offset: 0x00012D14
		public void InitDisplaySettingsBaseModel(string dpi, int resolutionWidth, int resolutionHeight)
		{
			this.Dpi = (string.IsNullOrEmpty(dpi) ? "240" : dpi);
			ResolutionModel resolutionModel = this.ResolutionsList.FirstOrDefault((ResolutionModel x) => x.AvailableResolutionsDict.ContainsValue(string.Format("{0} x {1}", resolutionWidth, resolutionHeight)));
			if (resolutionModel == null)
			{
				resolutionModel = this.ResolutionsList.First((ResolutionModel x) => x.OrientationType == OrientationType.Custom);
			}
			resolutionModel.CombinedResolution = ((resolutionModel.OrientationType == OrientationType.Landscape || resolutionModel.OrientationType == OrientationType.Custom) ? string.Format("{0} x {1}", resolutionWidth, resolutionHeight) : string.Format("{0} x {1}", resolutionHeight, resolutionWidth));
			this.ResolutionType = resolutionModel;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00014BF4 File Offset: 0x00012DF4
		private void BuildResolutionsList()
		{
			int num;
			int num2;
			Utils.GetWindowWidthAndHeight(out num, out num2);
			this.ResolutionsList = new ObservableCollection<ResolutionModel>
			{
				new ResolutionModel
				{
					OrientationType = OrientationType.Landscape,
					OrientationName = LocaleStrings.GetLocalizedString("STRING_ORIENTATION_LANDSCAPE", ""),
					AvailableResolutionsDict = new Dictionary<string, string>
					{
						{
							"960 x 540",
							"960 x 540"
						},
						{
							"1280 x 720",
							"1280 x 720"
						},
						{
							"1600 x 900",
							"1600 x 900"
						},
						{
							"1920 x 1080",
							"1920 x 1080"
						},
						{
							"2560 x 1440",
							"2560 x 1440"
						}
					},
					CombinedResolution = string.Format("{0} x {1}", num, num2),
					SystemDefaultResolution = string.Format("{0} x {1}", num, num2)
				},
				new ResolutionModel
				{
					OrientationType = OrientationType.Portrait,
					OrientationName = LocaleStrings.GetLocalizedString("STRING_ORIENTATION_PORTRAIT", ""),
					AvailableResolutionsDict = new Dictionary<string, string>
					{
						{
							"960 x 540",
							"540 x 960"
						},
						{
							"1280 x 720",
							"720 x 1280"
						},
						{
							"1600 x 900",
							"900 x 1600"
						},
						{
							"1920 x 1080",
							"1080 x 1920"
						},
						{
							"2560 x 1440",
							"1440 x 2560"
						}
					},
					CombinedResolution = string.Format("{0} x {1}", num, num2),
					SystemDefaultResolution = string.Format("{0} x {1}", num2, num)
				},
				new ResolutionModel
				{
					OrientationType = OrientationType.Custom,
					OrientationName = LocaleStrings.GetLocalizedString("STRING_CUSTOM1", ""),
					AvailableResolutionsDict = new Dictionary<string, string>(),
					CombinedResolution = string.Format("{0} x {1}", num, num2),
					SystemDefaultResolution = string.Format("{0} x {1}", num, num2)
				}
			};
		}

		// Token: 0x04000166 RID: 358
		private ResolutionModel mResolutionType;

		// Token: 0x04000167 RID: 359
		private string mDpi = "240";
	}
}
