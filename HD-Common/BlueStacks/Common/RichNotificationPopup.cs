using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace BlueStacks.Common
{
	// Token: 0x02000180 RID: 384
	public class RichNotificationPopup : CustomWindow, IComponentConnector
	{
		// Token: 0x17000419 RID: 1049
		// (set) Token: 0x06000E92 RID: 3730 RVA: 0x0000D10C File Offset: 0x0000B30C
		public string BackgroundImage
		{
			set
			{
				this.mBackgroundImage.ImageName = value;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (set) Token: 0x06000E93 RID: 3731 RVA: 0x0000D11A File Offset: 0x0000B31A
		public string GameIcon
		{
			set
			{
				this.mGameIcon.ImageName = value;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (set) Token: 0x06000E94 RID: 3732 RVA: 0x0000D128 File Offset: 0x0000B328
		public string GameTitleText
		{
			set
			{
				this.mGameTitle.Text = value;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (set) Token: 0x06000E95 RID: 3733 RVA: 0x0000D136 File Offset: 0x0000B336
		public string GameDeveloperText
		{
			set
			{
				this.mGameDeveloper.Text = value;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x0000D144 File Offset: 0x0000B344
		// (set) Token: 0x06000E97 RID: 3735 RVA: 0x0000D14C File Offset: 0x0000B34C
		public CustomButton Button
		{
			get
			{
				return this.mButton;
			}
			set
			{
				this.mButton = value;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (set) Token: 0x06000E98 RID: 3736 RVA: 0x0000D155 File Offset: 0x0000B355
		public MouseButtonEventHandler CloseButtonHandler
		{
			set
			{
				this.mCloseButton.PreviewMouseLeftButtonUp += value;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (set) Token: 0x06000E99 RID: 3737 RVA: 0x0000D163 File Offset: 0x0000B363
		public MouseButtonEventHandler MuteButtonHandler
		{
			set
			{
				this.mMuteButton.PreviewMouseLeftButtonUp += value;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (set) Token: 0x06000E9A RID: 3738 RVA: 0x0000D171 File Offset: 0x0000B371
		public bool IsCentered
		{
			set
			{
				if (value)
				{
					this.SetWindowStyle(RichPopupStyles.Centered);
					return;
				}
				this.SetWindowStyle(RichPopupStyles.Simple);
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x0000D185 File Offset: 0x0000B385
		// (set) Token: 0x06000E9C RID: 3740 RVA: 0x0000D18D File Offset: 0x0000B38D
		public string AssetFolderPath { get; set; } = Path.Combine(RegistryManager.Instance.ClientInstallDir, RegistryManager.ClientThemeName);

		// Token: 0x06000E9D RID: 3741 RVA: 0x00039950 File Offset: 0x00037B50
		private void SetWindowStyle(RichPopupStyles style)
		{
			if (style == RichPopupStyles.Centered)
			{
				base.Width = 600.0;
				base.Height = 380.0;
				base.WindowStartupLocation = WindowStartupLocation.CenterScreen;
				return;
			}
			if (style != RichPopupStyles.Simple)
			{
				return;
			}
			base.Width = 320.0;
			base.Height = 210.0;
			base.Left = SystemParameters.FullPrimaryScreenWidth - base.Width - 16.0;
			base.Top = SystemParameters.FullPrimaryScreenHeight - base.Height;
			this.mMuteButton.Height = (this.mMuteButton.Width = 16.0);
			this.mMuteButton.Margin = new Thickness(0.0, 0.0, 5.0, 0.0);
			this.mCloseButton.Height = (this.mCloseButton.Width = 16.0);
			this.mBottomGrid.Margin = new Thickness(10.0);
			this.mBottomGrid.Height = 26.0;
			this.mGameTitle.FontSize = 11.0;
			Grid.SetRowSpan(this.mGameTitle, 2);
			this.mGameTitle.VerticalAlignment = VerticalAlignment.Center;
			this.mGameDeveloper.Visibility = Visibility.Collapsed;
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x0000D196 File Offset: 0x0000B396
		public RichNotificationPopup()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00003C8E File Offset: 0x00001E8E
		private void mCloseButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x00039AB4 File Offset: 0x00037CB4
		public void ShowWindow()
		{
			string path = Path.Combine(RegistryManager.Instance.UserDefinedDir, "Client\\Helper");
			this.mMuteButton.ImageName = Path.Combine(path, "mute2.png");
			this.mCloseButton.ImageName = Path.Combine(path, "close.png");
			this.mMuteButton.ToolTip = LocaleStrings.GetLocalizedString("STRING_MUTE_NOTIFICATION_TOOLTIP", "");
			this.mCloseButton.ToolTip = LocaleStrings.GetLocalizedString("STRING_CLOSE", "");
			base.Show();
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00039B3C File Offset: 0x00037D3C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/HD-Common;component/wpf/richnotificationpopup.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x00003CF6 File Offset: 0x00001EF6
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00039B6C File Offset: 0x00037D6C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.mBackgroundImage = (CustomPictureBox)target;
				return;
			case 2:
				this.mCloseButtonGrid = (Grid)target;
				return;
			case 3:
				this.mCloseButtonStackPanel = (StackPanel)target;
				return;
			case 4:
				this.mMuteButton = (CustomPictureBox)target;
				return;
			case 5:
				this.mCloseButton = (CustomPictureBox)target;
				return;
			case 6:
				this.mBottomGrid = (Grid)target;
				return;
			case 7:
				this.mGameIcon = (CustomPictureBox)target;
				return;
			case 8:
				this.mGameDescriptionGrid = (Grid)target;
				return;
			case 9:
				this.mGameTitle = (TextBlock)target;
				return;
			case 10:
				this.mGameDeveloper = (TextBlock)target;
				return;
			case 11:
				this.mButton = (CustomButton)target;
				return;
			default:
				this._contentLoaded = true;
				return;
			}
		}

		// Token: 0x040006C9 RID: 1737
		internal CustomPictureBox mBackgroundImage;

		// Token: 0x040006CA RID: 1738
		internal Grid mCloseButtonGrid;

		// Token: 0x040006CB RID: 1739
		internal StackPanel mCloseButtonStackPanel;

		// Token: 0x040006CC RID: 1740
		internal CustomPictureBox mMuteButton;

		// Token: 0x040006CD RID: 1741
		internal CustomPictureBox mCloseButton;

		// Token: 0x040006CE RID: 1742
		internal Grid mBottomGrid;

		// Token: 0x040006CF RID: 1743
		internal CustomPictureBox mGameIcon;

		// Token: 0x040006D0 RID: 1744
		internal Grid mGameDescriptionGrid;

		// Token: 0x040006D1 RID: 1745
		internal TextBlock mGameTitle;

		// Token: 0x040006D2 RID: 1746
		internal TextBlock mGameDeveloper;

		// Token: 0x040006D3 RID: 1747
		internal CustomButton mButton;

		// Token: 0x040006D4 RID: 1748
		private bool _contentLoaded;
	}
}
