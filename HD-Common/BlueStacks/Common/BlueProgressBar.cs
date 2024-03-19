using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace BlueStacks.Common
{
	// Token: 0x020000FB RID: 251
	public class BlueProgressBar : ProgressBar, IComponentConnector
	{
		// Token: 0x06000637 RID: 1591 RVA: 0x00005A1D File Offset: 0x00003C1D
		public BlueProgressBar()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001CDC0 File Offset: 0x0001AFC0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/HD-Common;component/uielements/blueprogressbar.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00005A2B File Offset: 0x00003C2B
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x04000355 RID: 853
		private bool _contentLoaded;
	}
}
