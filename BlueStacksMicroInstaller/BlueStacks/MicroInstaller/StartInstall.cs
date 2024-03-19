using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using BlueStacks.Common;

namespace BlueStacks.MicroInstaller
{
	// Token: 0x02000094 RID: 148
	public class StartInstall : UserControl, IComponentConnector
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x0000F81C File Offset: 0x0000DA1C
		public StartInstall()
		{
			this.InitializeComponent();
			base.Loaded += this.StartInstallUserControl_Loaded;
			this.mInstallNowButton.Content = Globalization.GetLocalizedString("STRING_INSTALL_NOW");
			this.mCustomInstallLabel.Text = Globalization.GetLocalizedString("STRING_CUSTOM");
			this.mSoftwareLicenseTextBlock.Text = Globalization.GetLocalizedString("STRING_SOFTWARE_LICENSE");
			this.mAgreeLabel.Content = Globalization.GetLocalizedString("STRING_AGREE");
			this.mPromotionAgreeLabel.Content = Globalization.GetLocalizedString("STRING_RECEIVE_EMAIL_NOTIFICATION");
			this.mCheckboxImage.Tag = "checked_gray";
			this.mPromotionCheckboxImage.Tag = "unchecked_gray";
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000F8DB File Offset: 0x0000DADB
		private void StartInstallUserControl_Loaded(object sender, RoutedEventArgs e)
		{
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000F8DE File Offset: 0x0000DADE
		private void SoftwareLincenceMouseDown(object sender, MouseButtonEventArgs e)
		{
			Process.Start("http://www.bluestacks.com/terms-and-privacy.html");
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000F8EC File Offset: 0x0000DAEC
		private void MCheckboxImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			bool flag = (string)this.mCheckboxImage.Tag == "checked_gray";
			if (flag)
			{
				this.mCheckboxImage.ImageName = "unchecked_gray";
				this.mCheckboxImage.Tag = "unchecked_gray";
			}
			else
			{
				this.mCheckboxImage.ImageName = "checked_gray";
				this.mCheckboxImage.Tag = "checked_gray";
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000F964 File Offset: 0x0000DB64
		private void MPromotionCheckboxImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			bool flag = (string)this.mPromotionCheckboxImage.Tag == "checked_gray";
			if (flag)
			{
				this.mPromotionCheckboxImage.ImageName = "unchecked_gray";
				this.mPromotionCheckboxImage.Tag = "unchecked_gray";
			}
			else
			{
				this.mPromotionCheckboxImage.ImageName = "checked_gray";
				this.mPromotionCheckboxImage.Tag = "checked_gray";
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000F9DC File Offset: 0x0000DBDC
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/BlueStacksMicroInstaller;component/controls/startinstall.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000FA14 File Offset: 0x0000DC14
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000FA30 File Offset: 0x0000DC30
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.mInstallNowButton = (Button)target;
				break;
			case 2:
				this.mCustomInstallLicensePanel = (StackPanel)target;
				break;
			case 3:
				this.mCustomInstallImage = (CustomPictureBox)target;
				break;
			case 4:
				this.mCustomInstallLabel = (TextBlock)target;
				break;
			case 5:
				this.mCheckboxImage = (CustomPictureBox)target;
				break;
			case 6:
				this.mAgreeLabel = (Label)target;
				this.mAgreeLabel.MouseDown += this.MCheckboxImage_MouseLeftButtonUp;
				break;
			case 7:
				this.mSoftwareLicenseTextBlock = (TextBlock)target;
				this.mSoftwareLicenseTextBlock.MouseDown += this.SoftwareLincenceMouseDown;
				break;
			case 8:
				this.mPromotionCheckboxImage = (CustomPictureBox)target;
				break;
			case 9:
				this.mPromotionAgreeLabel = (Label)target;
				this.mPromotionAgreeLabel.MouseDown += this.MPromotionCheckboxImage_MouseLeftButtonUp;
				break;
			default:
				this._contentLoaded = true;
				break;
			}
		}

		// Token: 0x040004FF RID: 1279
		internal Button mInstallNowButton;

		// Token: 0x04000500 RID: 1280
		internal StackPanel mCustomInstallLicensePanel;

		// Token: 0x04000501 RID: 1281
		internal CustomPictureBox mCustomInstallImage;

		// Token: 0x04000502 RID: 1282
		internal TextBlock mCustomInstallLabel;

		// Token: 0x04000503 RID: 1283
		internal CustomPictureBox mCheckboxImage;

		// Token: 0x04000504 RID: 1284
		internal Label mAgreeLabel;

		// Token: 0x04000505 RID: 1285
		internal TextBlock mSoftwareLicenseTextBlock;

		// Token: 0x04000506 RID: 1286
		internal CustomPictureBox mPromotionCheckboxImage;

		// Token: 0x04000507 RID: 1287
		internal Label mPromotionAgreeLabel;

		// Token: 0x04000508 RID: 1288
		private bool _contentLoaded;
	}
}
