using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace BlueStacks.MicroInstaller
{
	// Token: 0x0200009B RID: 155
	public class InstallProgress : UserControl, IComponentConnector
	{
		// Token: 0x06000352 RID: 850 RVA: 0x00015535 File Offset: 0x00013735
		public InstallProgress()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00015548 File Offset: 0x00013748
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			bool contentLoaded = this._contentLoaded;
			if (!contentLoaded)
			{
				this._contentLoaded = true;
				Uri resourceLocator = new Uri("/BlueStacksMicroInstaller;component/controls/installprogress.xaml", UriKind.Relative);
				Application.LoadComponent(this, resourceLocator);
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00015580 File Offset: 0x00013780
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			switch (connectionId)
			{
			case 1:
				this.mInstallProgressStatus = (Label)target;
				break;
			case 2:
				this.mInstallProgressPercentage = (Label)target;
				break;
			case 3:
				this.mInstallProgressBar = (ProgressBar)target;
				break;
			case 4:
				this.mInstallTips = (Label)target;
				break;
			default:
				this._contentLoaded = true;
				break;
			}
		}

		// Token: 0x04000533 RID: 1331
		internal Label mInstallProgressStatus;

		// Token: 0x04000534 RID: 1332
		internal Label mInstallProgressPercentage;

		// Token: 0x04000535 RID: 1333
		internal ProgressBar mInstallProgressBar;

		// Token: 0x04000536 RID: 1334
		internal Label mInstallTips;

		// Token: 0x04000537 RID: 1335
		private bool _contentLoaded;
	}
}
