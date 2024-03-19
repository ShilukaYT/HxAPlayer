using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using BlueStacks.Common;

namespace BlueStacks.MicroInstaller
{
	// Token: 0x02000092 RID: 146
	public class ChooseInstallFolder : UserControl, IComponentConnector
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x0000E620 File Offset: 0x0000C820
		public ChooseInstallFolder()
		{
			this.InitializeComponent();
			this.mSpaceRequiredLabel.Content = Globalization.GetLocalizedString("STRING_SPACE_REQUIRED");
			this.mSpaceAvailableLabel.Content = Globalization.GetLocalizedString("STRING_SPACE_AVAILABLE");
			this.mInstallNowLabel.Content = Globalization.GetLocalizedString("STRING_INSTALL_NOW");
			this.mBackButtonLabel.Content = Globalization.GetLocalizedString("STRING_BACK");
			this.mDataPathLabel.Content = Globalization.GetLocalizedString("STRING_DATA_PATH");
			this.mChooseFolderLabel.Content = Globalization.GetLocalizedString("STRING_FOLDER");
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000E6C0 File Offset: 0x0000C8C0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/BlueStacksMicroInstaller;component/controls/chooseinstallfolder.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000E6F8 File Offset: 0x0000C8F8
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000E714 File Offset: 0x0000C914
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.mDataPathLabel = (Label)target;
				break;
			case 2:
				this.mBluestacksDataLocation = (TextBox)target;
				break;
			case 3:
				this.mChooseFolderButton = (CustomPictureBox)target;
				break;
			case 4:
				this.mChooseFolderLabel = (Label)target;
				break;
			case 5:
				this.mSpaceRequiredLabel = (Label)target;
				break;
			case 6:
				this.mSpaceAvailableLabel = (Label)target;
				break;
			case 7:
				this.mSpaceAvailable = (Label)target;
				break;
			case 8:
				this.mInstallNowButton = (CustomPictureBox)target;
				break;
			case 9:
				this.mInstallNowLabel = (Label)target;
				break;
			case 10:
				this.mBackButton = (CustomPictureBox)target;
				break;
			case 11:
				this.mBackButtonLabel = (Label)target;
				break;
			default:
				this._contentLoaded = true;
				break;
			}
		}

		// Token: 0x040004E6 RID: 1254
		internal Label mDataPathLabel;

		// Token: 0x040004E7 RID: 1255
		internal TextBox mBluestacksDataLocation;

		// Token: 0x040004E8 RID: 1256
		internal CustomPictureBox mChooseFolderButton;

		// Token: 0x040004E9 RID: 1257
		internal Label mChooseFolderLabel;

		// Token: 0x040004EA RID: 1258
		internal Label mSpaceRequiredLabel;

		// Token: 0x040004EB RID: 1259
		internal Label mSpaceAvailableLabel;

		// Token: 0x040004EC RID: 1260
		internal Label mSpaceAvailable;

		// Token: 0x040004ED RID: 1261
		internal CustomPictureBox mInstallNowButton;

		// Token: 0x040004EE RID: 1262
		internal Label mInstallNowLabel;

		// Token: 0x040004EF RID: 1263
		internal CustomPictureBox mBackButton;

		// Token: 0x040004F0 RID: 1264
		internal Label mBackButtonLabel;

		// Token: 0x040004F1 RID: 1265
		private bool _contentLoaded;
	}
}
