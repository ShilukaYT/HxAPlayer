using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;

namespace BlueStacks.Common
{
	// Token: 0x02000118 RID: 280
	public class CustomTextBox : XTextBox, IComponentConnector
	{
		// Token: 0x060007BA RID: 1978 RVA: 0x00006A1E File Offset: 0x00004C1E
		public CustomTextBox()
		{
			this.InitializeComponent();
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00025A60 File Offset: 0x00023C60
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/HD-Common;component/uielements/customtextbox.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00003CF6 File Offset: 0x00001EF6
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00006A2C File Offset: 0x00004C2C
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.mTextBox = (CustomTextBox)target;
				return;
			}
			this._contentLoaded = true;
		}

		// Token: 0x04000412 RID: 1042
		internal CustomTextBox mTextBox;

		// Token: 0x04000413 RID: 1043
		private bool _contentLoaded;
	}
}
