using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;

namespace BlueStacks.Common
{
	// Token: 0x0200009F RID: 159
	public class DisplaySettingsBase : System.Windows.Controls.UserControl, INotifyPropertyChanged, IComponentConnector
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600034E RID: 846 RVA: 0x00014F1C File Offset: 0x0001311C
		// (remove) Token: 0x0600034F RID: 847 RVA: 0x00014F54 File Offset: 0x00013154
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06000350 RID: 848 RVA: 0x00003BCB File Offset: 0x00001DCB
		public void OnPropertyChanged(string name)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(name));
			}
			CommandManager.InvalidateRequerySuggested();
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00003BEA File Offset: 0x00001DEA
		// (set) Token: 0x06000352 RID: 850 RVA: 0x00003BF2 File Offset: 0x00001DF2
		public DisplaySettingsBaseModel InitialDisplaySettingsModel { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00003BFB File Offset: 0x00001DFB
		// (set) Token: 0x06000354 RID: 852 RVA: 0x00003C03 File Offset: 0x00001E03
		public DisplaySettingsBaseModel CurrentDisplaySettingsModel
		{
			get
			{
				return this.mCurrentDisplaySettingsModel;
			}
			set
			{
				this.mCurrentDisplaySettingsModel = value;
				this.OnPropertyChanged("CurrentDisplaySettingsModel");
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00003C17 File Offset: 0x00001E17
		// (set) Token: 0x06000356 RID: 854 RVA: 0x00003C1F File Offset: 0x00001E1F
		public bool IsOpenedFromMultiInstance { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00003C28 File Offset: 0x00001E28
		// (set) Token: 0x06000358 RID: 856 RVA: 0x00003C30 File Offset: 0x00001E30
		public string VmName { get; set; } = "Android";

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00003C39 File Offset: 0x00001E39
		// (set) Token: 0x0600035A RID: 858 RVA: 0x00003C41 File Offset: 0x00001E41
		public ICommand SaveCommand { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00003C4A File Offset: 0x00001E4A
		// (set) Token: 0x0600035C RID: 860 RVA: 0x00003C52 File Offset: 0x00001E52
		public int MinResolutionWidth { get; set; } = 540;

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00003C5B File Offset: 0x00001E5B
		// (set) Token: 0x0600035E RID: 862 RVA: 0x00003C63 File Offset: 0x00001E63
		public int MinResolutionHeight { get; set; } = 540;

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00003C6C File Offset: 0x00001E6C
		// (set) Token: 0x06000360 RID: 864 RVA: 0x00003C74 File Offset: 0x00001E74
		public int MaxResolutionWidth { get; set; } = 2560;

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00003C7D File Offset: 0x00001E7D
		// (set) Token: 0x06000362 RID: 866 RVA: 0x00003C85 File Offset: 0x00001E85
		public int MaxResolutionHeight { get; set; } = 2560;

		// Token: 0x06000363 RID: 867 RVA: 0x00003C8E File Offset: 0x00001E8E
		protected virtual void Save(object param)
		{
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00003C90 File Offset: 0x00001E90
		// (set) Token: 0x06000365 RID: 869 RVA: 0x00003C98 File Offset: 0x00001E98
		public Window Owner { get; private set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00003CA1 File Offset: 0x00001EA1
		// (set) Token: 0x06000367 RID: 871 RVA: 0x00003CA9 File Offset: 0x00001EA9
		public string OEM { get; private set; } = "bgp";

		// Token: 0x06000368 RID: 872 RVA: 0x00014F8C File Offset: 0x0001318C
		public DisplaySettingsBase(Window owner, string vmName, string oem = "")
		{
			this.Owner = owner;
			this.OEM = (string.IsNullOrEmpty(oem) ? "bgp" : oem);
			this.VmName = vmName;
			this.Owner = owner;
			this.Init();
			this.LoadViewFromUri("/HD-Common;component/Settings/DisplaySettingBase/DisplaySettingsBase.xaml");
			base.Visibility = Visibility.Hidden;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00015024 File Offset: 0x00013224
		public void Init()
		{
			this.InitialDisplaySettingsModel = new DisplaySettingsBaseModel(Utils.GetDpiFromBootParameters(RegistryManager.RegistryManagers[this.OEM].Guest[this.VmName].BootParameters), RegistryManager.RegistryManagers[this.OEM].Guest[this.VmName].GuestWidth, RegistryManager.RegistryManagers[this.OEM].Guest[this.VmName].GuestHeight);
			this.CurrentDisplaySettingsModel = this.InitialDisplaySettingsModel.DeepCopy<DisplaySettingsBaseModel>();
			this.SaveCommand = new RelayCommand2(new Func<object, bool>(this.CanSave), new Action<object>(this.Save));
			this.MaxResolutionWidth = Math.Max(this.MaxResolutionWidth, Screen.PrimaryScreen.Bounds.Width);
			this.MaxResolutionHeight = Math.Max(this.MaxResolutionHeight, Screen.PrimaryScreen.Bounds.Height);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00003CB2 File Offset: 0x00001EB2
		private bool CanSave(object _1)
		{
			return this.IsDirty() && this.IsValid();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0001512C File Offset: 0x0001332C
		public bool IsDirty()
		{
			return this.InitialDisplaySettingsModel.ResolutionType.ResolutionWidth != this.CurrentDisplaySettingsModel.ResolutionType.ResolutionWidth || this.InitialDisplaySettingsModel.ResolutionType.ResolutionHeight != this.CurrentDisplaySettingsModel.ResolutionType.ResolutionHeight || this.CurrentDisplaySettingsModel.Dpi != this.InitialDisplaySettingsModel.Dpi;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00003CC4 File Offset: 0x00001EC4
		public bool IsValid()
		{
			return !Validation.GetHasError(this.CustomResolutionHeight) && !Validation.GetHasError(this.CustomResolutionWidth);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001519C File Offset: 0x0001339C
		protected void SaveDisplaySetting()
		{
			Logger.Info("Saving Display setting");
			Utils.SetDPIInBootParameters(RegistryManager.RegistryManagers[this.OEM].Guest[this.VmName].BootParameters, this.CurrentDisplaySettingsModel.Dpi, this.VmName, this.OEM);
			RegistryManager.RegistryManagers[this.OEM].Guest[this.VmName].GuestWidth = this.CurrentDisplaySettingsModel.ResolutionType.ResolutionWidth;
			RegistryManager.RegistryManagers[this.OEM].Guest[this.VmName].GuestHeight = this.CurrentDisplaySettingsModel.ResolutionType.ResolutionHeight;
			Stats.SendMiscellaneousStatsAsync("Setting-save", RegistryManager.RegistryManagers[this.OEM].UserGuid, RegistryManager.RegistryManagers[this.OEM].ClientVersion, "Display-Settings", "", null, this.VmName, null, null, "Android", 0);
			this.InitialDisplaySettingsModel.InitDisplaySettingsBaseModel(Utils.GetDpiFromBootParameters(RegistryManager.RegistryManagers[this.OEM].Guest[this.VmName].BootParameters), RegistryManager.RegistryManagers[this.OEM].Guest[this.VmName].GuestWidth, RegistryManager.RegistryManagers[this.OEM].Guest[this.VmName].GuestHeight);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00003CE3 File Offset: 0x00001EE3
		public void DiscardCurrentChangingModel()
		{
			this.CurrentDisplaySettingsModel = this.InitialDisplaySettingsModel.DeepCopy<DisplaySettingsBaseModel>();
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001532C File Offset: 0x0001352C
		protected void AddToastPopup(string message)
		{
			try
			{
				if (this.mToastPopup == null)
				{
					this.mToastPopup = new CustomToastPopupControl(this.Owner);
				}
				this.mToastPopup.Init(this.Owner, message, null, null, System.Windows.HorizontalAlignment.Center, VerticalAlignment.Bottom, null, 12, null, null, false, false);
				this.mToastPopup.ShowPopup(1.3);
			}
			catch (Exception ex)
			{
				Logger.Error("Exception in showing toast popup: " + ex.ToString());
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x000153C0 File Offset: 0x000135C0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/HD-Common;component/settings/displaysettingbase/displaysettingsbase.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00003CF6 File Offset: 0x00001EF6
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x000153F0 File Offset: 0x000135F0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.mOrientation = (CustomComboBox)target;
				return;
			case 2:
				this.mRadioButtons = (Grid)target;
				return;
			case 3:
				this.mResolution960x540 = (CustomRadioButton)target;
				return;
			case 4:
				this.mResolution1280x720 = (CustomRadioButton)target;
				return;
			case 5:
				this.mResolution1600x900 = (CustomRadioButton)target;
				return;
			case 6:
				this.mResolution1920x1080 = (CustomRadioButton)target;
				return;
			case 7:
				this.mResolution2560x1440 = (CustomRadioButton)target;
				return;
			case 8:
				this.CustomResolutionTextBoxes = (Grid)target;
				return;
			case 9:
				this.CustomResolutionWidth = (CustomTextBox)target;
				return;
			case 10:
				this.CustomResolutionHeight = (CustomTextBox)target;
				return;
			case 11:
				this.mDpi160 = (CustomRadioButton)target;
				return;
			case 12:
				this.mDpi240 = (CustomRadioButton)target;
				return;
			case 13:
				this.mDpi320 = (CustomRadioButton)target;
				return;
			case 14:
				this.mInfoIcon = (CustomPictureBox)target;
				return;
			case 15:
				this.mSaveButton = (CustomButton)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x04000176 RID: 374
		private DisplaySettingsBaseModel mCurrentDisplaySettingsModel;

		// Token: 0x04000180 RID: 384
		private CustomToastPopupControl mToastPopup;

		// Token: 0x04000181 RID: 385
		internal CustomComboBox mOrientation;

		// Token: 0x04000182 RID: 386
		internal Grid mRadioButtons;

		// Token: 0x04000183 RID: 387
		internal CustomRadioButton mResolution960x540;

		// Token: 0x04000184 RID: 388
		internal CustomRadioButton mResolution1280x720;

		// Token: 0x04000185 RID: 389
		internal CustomRadioButton mResolution1600x900;

		// Token: 0x04000186 RID: 390
		internal CustomRadioButton mResolution1920x1080;

		// Token: 0x04000187 RID: 391
		internal CustomRadioButton mResolution2560x1440;

		// Token: 0x04000188 RID: 392
		internal Grid CustomResolutionTextBoxes;

		// Token: 0x04000189 RID: 393
		internal CustomTextBox CustomResolutionWidth;

		// Token: 0x0400018A RID: 394
		internal CustomTextBox CustomResolutionHeight;

		// Token: 0x0400018B RID: 395
		internal CustomRadioButton mDpi160;

		// Token: 0x0400018C RID: 396
		internal CustomRadioButton mDpi240;

		// Token: 0x0400018D RID: 397
		internal CustomRadioButton mDpi320;

		// Token: 0x0400018E RID: 398
		internal CustomPictureBox mInfoIcon;

		// Token: 0x0400018F RID: 399
		internal CustomButton mSaveButton;

		// Token: 0x04000190 RID: 400
		private bool _contentLoaded;
	}
}
