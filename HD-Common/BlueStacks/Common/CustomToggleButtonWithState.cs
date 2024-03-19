using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace BlueStacks.Common
{
	// Token: 0x020000B2 RID: 178
	public class CustomToggleButtonWithState : UserControl, IComponentConnector
	{
		// Token: 0x0600046D RID: 1133 RVA: 0x00004806 File Offset: 0x00002A06
		public CustomToggleButtonWithState()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00004814 File Offset: 0x00002A14
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x00004826 File Offset: 0x00002A26
		public double ImageWidth
		{
			get
			{
				return (double)base.GetValue(CustomToggleButtonWithState.ImageWidthProperty);
			}
			set
			{
				base.SetValue(CustomToggleButtonWithState.ImageWidthProperty, value);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00004839 File Offset: 0x00002A39
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x0000484B File Offset: 0x00002A4B
		public double ImageHeight
		{
			get
			{
				return (double)base.GetValue(CustomToggleButtonWithState.ImageHeightProperty);
			}
			set
			{
				base.SetValue(CustomToggleButtonWithState.ImageHeightProperty, value);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0000485E File Offset: 0x00002A5E
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x00004870 File Offset: 0x00002A70
		public Visibility LabelVisibility
		{
			get
			{
				return (Visibility)base.GetValue(CustomToggleButtonWithState.LabelVisibilityProperty);
			}
			set
			{
				base.SetValue(CustomToggleButtonWithState.LabelVisibilityProperty, value);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00004883 File Offset: 0x00002A83
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x00004895 File Offset: 0x00002A95
		public bool BoolValue
		{
			get
			{
				return (bool)base.GetValue(CustomToggleButtonWithState.BoolValueProperty);
			}
			set
			{
				base.SetValue(CustomToggleButtonWithState.BoolValueProperty, value);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x000048A8 File Offset: 0x00002AA8
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x000048BA File Offset: 0x00002ABA
		public bool IsShowText
		{
			get
			{
				return (bool)base.GetValue(CustomToggleButtonWithState.IsShowTextProperty);
			}
			set
			{
				base.SetValue(CustomToggleButtonWithState.IsShowTextProperty, value);
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000048CD File Offset: 0x00002ACD
		private void mToggleButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			this.BoolValue = !this.BoolValue;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00017BA4 File Offset: 0x00015DA4
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/HD-Common;component/uielements/customtogglebuttonwithstate.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00003CF6 File Offset: 0x00001EF6
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		internal Delegate _CreateDelegate(Type delegateType, string handler)
		{
			return Delegate.CreateDelegate(delegateType, this, handler);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000048DE File Offset: 0x00002ADE
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.mCustomToggleButton = (CustomToggleButtonWithState)target;
				return;
			}
			this._contentLoaded = true;
		}

		// Token: 0x0400020D RID: 525
		public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register("ImageWidth", typeof(double), typeof(CustomPictureBox), new FrameworkPropertyMetadata(32.0));

		// Token: 0x0400020E RID: 526
		public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register("ImageHeight", typeof(double), typeof(CustomPictureBox), new FrameworkPropertyMetadata(16.0));

		// Token: 0x0400020F RID: 527
		public static readonly DependencyProperty LabelVisibilityProperty = DependencyProperty.Register("LabelVisibility", typeof(Visibility), typeof(CustomPictureBox), new FrameworkPropertyMetadata(Visibility.Visible));

		// Token: 0x04000210 RID: 528
		public static readonly DependencyProperty BoolValueProperty = DependencyProperty.Register("BoolValue", typeof(bool), typeof(CustomToggleButtonWithState), new PropertyMetadata(true));

		// Token: 0x04000211 RID: 529
		public static readonly DependencyProperty IsShowTextProperty = DependencyProperty.Register("IsShowTextProperty", typeof(bool), typeof(CustomToggleButtonWithState), new PropertyMetadata(true));

		// Token: 0x04000212 RID: 530
		internal CustomToggleButtonWithState mCustomToggleButton;

		// Token: 0x04000213 RID: 531
		private bool _contentLoaded;
	}
}
