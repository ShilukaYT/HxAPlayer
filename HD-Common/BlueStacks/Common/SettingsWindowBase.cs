using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace BlueStacks.Common
{
	// Token: 0x02000156 RID: 342
	public abstract class SettingsWindowBase : UserControl, IComponentConnector
	{
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x0000C548 File Offset: 0x0000A748
		// (set) Token: 0x06000CCD RID: 3277 RVA: 0x0000C550 File Offset: 0x0000A750
		private protected UserControl visibleControl { protected get; private set; }

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x0000C559 File Offset: 0x0000A759
		// (set) Token: 0x06000CCF RID: 3279 RVA: 0x0000C561 File Offset: 0x0000A761
		public string StartUpTab { get; set; } = "STRING_ENGINE_SETTING";

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0000C56A File Offset: 0x0000A76A
		// (set) Token: 0x06000CD1 RID: 3281 RVA: 0x0000C572 File Offset: 0x0000A772
		public List<string> SettingsControlNameList { get; set; } = new List<string>();

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0000C57B File Offset: 0x0000A77B
		// (set) Token: 0x06000CD3 RID: 3283 RVA: 0x0000C583 File Offset: 0x0000A783
		public Dictionary<string, UserControl> SettingsWindowControlsDict { get; set; } = new Dictionary<string, UserControl>();

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0000C58C File Offset: 0x0000A78C
		// (set) Token: 0x06000CD5 RID: 3285 RVA: 0x0000C594 File Offset: 0x0000A794
		public bool IsVtxLearned { get; set; }

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0000C59D File Offset: 0x0000A79D
		public CustomPopUp EnableVTPopup
		{
			get
			{
				return this.mEnableVTPopup;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0000C5A5 File Offset: 0x0000A7A5
		public Grid SettingsWindowGrid
		{
			get
			{
				return this.settingsWindowGrid;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0000C5AD File Offset: 0x0000A7AD
		public StackPanel SettingsWindowStackPanel
		{
			get
			{
				return this.settingsStackPanel;
			}
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x00003C8E File Offset: 0x00001E8E
		protected virtual void SetPopupOffset()
		{
		}

		// Token: 0x06000CDA RID: 3290
		public abstract void CloseButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e);

		// Token: 0x06000CDB RID: 3291 RVA: 0x0000C5B5 File Offset: 0x0000A7B5
		public SettingsWindowBase()
		{
			this.LoadViewFromUri("/HD-Common;component/Settings/SettingsWindowBase.xaml");
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0000C5E9 File Offset: 0x0000A7E9
		public void AddControlInGridAndDict(string btnName, UserControl control)
		{
			this.SettingsWindowControlsDict[btnName] = control;
			if (!this.settingsWindowGrid.Children.Contains(control))
			{
				this.settingsWindowGrid.Children.Add(control);
			}
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0002EEF8 File Offset: 0x0002D0F8
		public void BringToFront(UserControl control)
		{
			if (control == null)
			{
				return;
			}
			if (this.visibleControl != null && this.visibleControl != control)
			{
				this.visibleControl.Visibility = Visibility.Collapsed;
			}
			control.Visibility = Visibility.Visible;
			this.visibleControl = control;
			EngineSettingBase engineSettingBase = control as EngineSettingBase;
			if (engineSettingBase != null)
			{
				EngineSettingBaseViewModel engineSettingBaseViewModel = engineSettingBase.DataContext as EngineSettingBaseViewModel;
				if (engineSettingBaseViewModel != null)
				{
					engineSettingBaseViewModel.Init();
					engineSettingBase.SetGraphicMode(engineSettingBaseViewModel.GraphicsMode);
					engineSettingBase.SetAdvancedGraphicMode(engineSettingBaseViewModel.UseAdvancedGraphicEngine);
					engineSettingBaseViewModel.NotifyPropertyChangedAllProperties();
					goto IL_7E;
				}
			}
			DisplaySettingsBase displaySettingsBase = control as DisplaySettingsBase;
			if (displaySettingsBase != null)
			{
				displaySettingsBase.Init();
			}
			IL_7E:
			this.SetPopupOffset();
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0000C61D File Offset: 0x0000A81D
		public bool CheckWidth()
		{
			return this.settingsStackPanel.ActualWidth == this.settingsStackPanel.ActualWidth && this.settingsWindowGrid.ActualWidth == this.settingsWindowGrid.ActualWidth;
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0002EF8C File Offset: 0x0002D18C
		public void SettingsBtn_Click(object sender, RoutedEventArgs e)
		{
			CustomSettingsButton customSettingsButton = (CustomSettingsButton)sender;
			if (customSettingsButton != null)
			{
				customSettingsButton.IsSelected = true;
				UserControl control = this.SettingsWindowControlsDict[customSettingsButton.Name];
				Logger.Info("Clicked {0} button", new object[]
				{
					customSettingsButton.Name
				});
				this.BringToFront(control);
				if (customSettingsButton.Name.Equals("STRING_SHORTCUT_KEY_SETTINGS", StringComparison.OrdinalIgnoreCase))
				{
					Stats.SendMiscellaneousStatsAsync("KeyboardShortcuts", RegistryManager.Instance.UserGuid, RegistryManager.Instance.ClientVersion, "shortcut_open", null, null, null, null, null, "Android", 0);
					return;
				}
				Stats.SendMiscellaneousStatsAsync("settings", RegistryManager.Instance.UserGuid, LocaleStrings.GetLocalizedString(customSettingsButton.Name, ""), "MouseClick", RegistryManager.Instance.ClientVersion, Oem.Instance.OEM, null, null, null, "Android", 0);
			}
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0000C652 File Offset: 0x0000A852
		private void mCrossButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0002F068 File Offset: 0x0002D268
		private void EnableVtInfo_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			Logger.Info("Clicked Enable Vt popup Settings window");
			this.IsVtxLearned = true;
			string text = WebHelper.GetUrlWithParams(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				WebHelper.GetServerHost(),
				"help_articles"
			}), null, null, null);
			text = string.Format(CultureInfo.InvariantCulture, "{0}&article={1}", new object[]
			{
				text,
				"enable_virtualization"
			});
			if (Oem.IsOEMDmm)
			{
				text = "http://help.dmm.com/-/detail/=/qid=45997/";
			}
			Utils.OpenUrl(text);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0000C65B File Offset: 0x0000A85B
		private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			this.mEnableVTPopup.IsOpen = false;
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0000C652 File Offset: 0x0000A852
		private void mEnableVTPopup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			e.Handled = true;
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0000C65B File Offset: 0x0000A85B
		private void UserControl_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
		{
			this.mEnableVTPopup.IsOpen = false;
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0000C669 File Offset: 0x0000A869
		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			Window.GetWindow(this).LostKeyboardFocus += this.UserControl_LostKeyboardFocus;
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0000C65B File Offset: 0x0000A85B
		private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			this.mEnableVTPopup.IsOpen = false;
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0002F0EC File Offset: 0x0002D2EC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/HD-Common;component/settings/settingswindowbase.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00003CF6 File Offset: 0x00001EF6
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x0002F11C File Offset: 0x0002D31C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((SettingsWindowBase)target).Loaded += this.UserControl_Loaded;
				((SettingsWindowBase)target).MouseLeftButtonUp += this.UserControl_MouseLeftButtonUp;
				return;
			case 2:
				((Grid)target).MouseLeftButtonUp += this.Grid_MouseLeftButtonUp;
				return;
			case 3:
				this.mGrid = (Grid)target;
				return;
			case 4:
				this.mSettingsWindowIcon = (CustomPictureBox)target;
				return;
			case 5:
				this.mLblBlueStacksSettings = (Label)target;
				return;
			case 6:
				this.mCrossButton = (CustomPictureBox)target;
				return;
			case 7:
				this.mEnableVTPopup = (CustomPopUp)target;
				return;
			case 8:
				this.EnableVtInfo = (TextBlock)target;
				this.EnableVtInfo.PreviewMouseLeftButtonUp += this.EnableVtInfo_PreviewMouseLeftButtonUp;
				return;
			case 9:
				this.mBottomGrid = (Grid)target;
				return;
			case 10:
				this.settingsStackPanel = (StackPanel)target;
				return;
			case 11:
				this.mSelectedLine = (Line)target;
				return;
			case 12:
				this.settingsWindowGrid = (Grid)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x0400061F RID: 1567
		internal Grid mGrid;

		// Token: 0x04000620 RID: 1568
		internal CustomPictureBox mSettingsWindowIcon;

		// Token: 0x04000621 RID: 1569
		internal Label mLblBlueStacksSettings;

		// Token: 0x04000622 RID: 1570
		internal CustomPictureBox mCrossButton;

		// Token: 0x04000623 RID: 1571
		internal CustomPopUp mEnableVTPopup;

		// Token: 0x04000624 RID: 1572
		internal TextBlock EnableVtInfo;

		// Token: 0x04000625 RID: 1573
		internal Grid mBottomGrid;

		// Token: 0x04000626 RID: 1574
		internal StackPanel settingsStackPanel;

		// Token: 0x04000627 RID: 1575
		internal Line mSelectedLine;

		// Token: 0x04000628 RID: 1576
		internal Grid settingsWindowGrid;

		// Token: 0x04000629 RID: 1577
		private bool _contentLoaded;
	}
}
