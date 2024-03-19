using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Navigation;

namespace BlueStacks.Common
{
	// Token: 0x020000AA RID: 170
	public class EngineSettingBase : UserControl, IComponentConnector
	{
		// Token: 0x06000436 RID: 1078 RVA: 0x00004535 File Offset: 0x00002735
		public EngineSettingBase()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00017658 File Offset: 0x00015858
		private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
		{
			try
			{
				Logger.Info("Opening url: " + e.Uri.AbsoluteUri);
				Utils.OpenUrl(e.Uri.AbsoluteUri);
				e.Handled = true;
			}
			catch (Exception ex)
			{
				Logger.Error("Exception in opening url" + ex.ToString());
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00004543 File Offset: 0x00002743
		private void ASTCHelpCenterImage_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Utils.OpenUrl(WebHelper.GetUrlWithParams(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				WebHelper.GetServerHost(),
				"help_articles"
			}), null, null, null) + "&article=ASTC_Help");
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00004581 File Offset: 0x00002781
		private void mHelpCenterImage_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Utils.OpenUrl(WebHelper.GetUrlWithParams(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				WebHelper.GetServerHost(),
				"help_articles"
			}), null, null, null) + "&article=ABI_Help");
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000045BF File Offset: 0x000027BF
		private void GPUHelpCenterImage_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Utils.OpenUrl(WebHelper.GetUrlWithParams(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				WebHelper.GetServerHost(),
				"help_articles"
			}), null, null, null) + "&article=GPU_Setting_Help");
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000176C0 File Offset: 0x000158C0
		private void DirectXRadioButton_Click(object sender, RoutedEventArgs e)
		{
			EngineSettingBaseViewModel engineSettingBaseViewModel = base.DataContext as EngineSettingBaseViewModel;
			if (engineSettingBaseViewModel != null)
			{
				engineSettingBaseViewModel.SetGraphicMode(GraphicsMode.DirectX);
			}
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x000176E4 File Offset: 0x000158E4
		internal void SetAdvancedGraphicMode(bool useAdvancedGraphicEngine)
		{
			if (useAdvancedGraphicEngine)
			{
				if (!this.mCompatibilityMode.IsChecked.GetValueOrDefault())
				{
					this.mCompatibilityMode.IsChecked = new bool?(true);
				}
				if (this.mPerformanceMode.IsChecked.GetValueOrDefault())
				{
					this.mPerformanceMode.IsChecked = new bool?(false);
					return;
				}
			}
			else
			{
				if (this.mCompatibilityMode.IsChecked.GetValueOrDefault())
				{
					this.mCompatibilityMode.IsChecked = new bool?(false);
				}
				if (!this.mPerformanceMode.IsChecked.GetValueOrDefault())
				{
					this.mPerformanceMode.IsChecked = new bool?(true);
				}
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00017790 File Offset: 0x00015990
		private void OpenGlRadioButton_Click(object sender, RoutedEventArgs e)
		{
			EngineSettingBaseViewModel engineSettingBaseViewModel = base.DataContext as EngineSettingBaseViewModel;
			if (engineSettingBaseViewModel != null)
			{
				engineSettingBaseViewModel.SetGraphicMode(GraphicsMode.OpenGL);
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x000045FD File Offset: 0x000027FD
		public void SetGraphicMode(GraphicsMode mode)
		{
			this.mDirectX.IsChecked = new bool?(mode == GraphicsMode.DirectX);
			this.mGlMode.IsChecked = new bool?(mode == GraphicsMode.OpenGL);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00004627 File Offset: 0x00002827
		private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			BluestacksUIColor.ScrollBarScrollChanged(sender, e);
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000177B4 File Offset: 0x000159B4
		private void PerformanceMode_Click(object sender, RoutedEventArgs e)
		{
			EngineSettingBaseViewModel engineSettingBaseViewModel = base.DataContext as EngineSettingBaseViewModel;
			if (engineSettingBaseViewModel != null)
			{
				engineSettingBaseViewModel.UseAdvancedGraphicEngine = false;
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000177D8 File Offset: 0x000159D8
		private void CompatibilityMode_Click(object sender, RoutedEventArgs e)
		{
			EngineSettingBaseViewModel engineSettingBaseViewModel = base.DataContext as EngineSettingBaseViewModel;
			if (engineSettingBaseViewModel != null)
			{
				engineSettingBaseViewModel.UseAdvancedGraphicEngine = true;
			}
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00004630 File Offset: 0x00002830
		private void VSyncHelp_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Utils.OpenUrl(WebHelper.GetUrlWithParams(string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				WebHelper.GetServerHost(),
				"help_articles"
			}), null, null, null) + "&article=VSync_Help");
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000177FC File Offset: 0x000159FC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/HD-Common;component/settings/enginesettingbase/enginesettingbase.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00003CF6 File Offset: 0x00001EF6
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001782C File Offset: 0x00015A2C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				((ScrollViewer)target).ScrollChanged += this.ScrollViewer_ScrollChanged;
				return;
			case 2:
				this.mCompatibilityMode = (CustomRadioButton)target;
				return;
			case 3:
				this.mPerformanceMode = (CustomRadioButton)target;
				return;
			case 4:
				this.mDirectX = (CustomRadioButton)target;
				return;
			case 5:
				this.mGlMode = (CustomRadioButton)target;
				return;
			case 6:
				this.softwareDecoding = (CustomRadioButton)target;
				return;
			case 7:
				this.mVSyncHelp = (CustomPictureBox)target;
				return;
			case 8:
				this.mHelpCenterImage = (CustomPictureBox)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x040001FE RID: 510
		internal CustomRadioButton mCompatibilityMode;

		// Token: 0x040001FF RID: 511
		internal CustomRadioButton mPerformanceMode;

		// Token: 0x04000200 RID: 512
		internal CustomRadioButton mDirectX;

		// Token: 0x04000201 RID: 513
		internal CustomRadioButton mGlMode;

		// Token: 0x04000202 RID: 514
		internal CustomRadioButton softwareDecoding;

		// Token: 0x04000203 RID: 515
		internal CustomPictureBox mVSyncHelp;

		// Token: 0x04000204 RID: 516
		internal CustomPictureBox mHelpCenterImage;

		// Token: 0x04000205 RID: 517
		private bool _contentLoaded;
	}
}
