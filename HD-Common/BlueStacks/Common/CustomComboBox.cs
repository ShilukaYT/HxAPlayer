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
	// Token: 0x0200010F RID: 271
	public class CustomComboBox : ComboBox, IComponentConnector, IStyleConnector
	{
		// Token: 0x0600074D RID: 1869 RVA: 0x00006465 File Offset: 0x00004665
		public CustomComboBox()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x00006473 File Offset: 0x00004673
		private void OnRequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
		{
			if (Keyboard.IsKeyDown(Key.Down) || Keyboard.IsKeyDown(Key.Up))
			{
				return;
			}
			e.Handled = true;
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x0000648F File Offset: 0x0000468F
		// (set) Token: 0x06000750 RID: 1872 RVA: 0x000064A1 File Offset: 0x000046A1
		public bool Highlight
		{
			get
			{
				return (bool)base.GetValue(CustomComboBox.HighlightProperty);
			}
			set
			{
				base.SetValue(CustomComboBox.HighlightProperty, value);
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000751 RID: 1873 RVA: 0x000064B4 File Offset: 0x000046B4
		// (set) Token: 0x06000752 RID: 1874 RVA: 0x000064C6 File Offset: 0x000046C6
		public string ToolTipText
		{
			get
			{
				return (string)base.GetValue(CustomComboBox.ToolTipTextProperty);
			}
			set
			{
				base.SetValue(CustomComboBox.ToolTipTextProperty, value);
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x000064D4 File Offset: 0x000046D4
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x000064E6 File Offset: 0x000046E6
		public bool SetToolTip
		{
			get
			{
				return (bool)base.GetValue(CustomComboBox.SetToolTipProperty);
			}
			set
			{
				base.SetValue(CustomComboBox.SetToolTipProperty, value);
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x000064F9 File Offset: 0x000046F9
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x0000650B File Offset: 0x0000470B
		public bool ToolTipWhenTrimmed
		{
			get
			{
				return (bool)base.GetValue(CustomComboBox.ToolTipWhenTrimmedProperty);
			}
			set
			{
				base.SetValue(CustomComboBox.ToolTipWhenTrimmedProperty, value);
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00024F2C File Offset: 0x0002312C
		private static void OnSetToolTipChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			CustomComboBox customComboBox = d as CustomComboBox;
			if (customComboBox.ToolTipWhenTrimmed)
			{
				customComboBox.OnSetToolTipChanged(e);
			}
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00024F50 File Offset: 0x00023150
		private void OnSetToolTipChanged(DependencyPropertyChangedEventArgs args)
		{
			bool flag;
			if (bool.TryParse(args.NewValue.ToString(), out flag) && flag && this.IsTextTrimmed(this.ToolTipText))
			{
				ToolTipService.SetIsEnabled(this, true);
				base.ToolTip = this.ToolTipText;
				return;
			}
			ToolTipService.SetIsEnabled(this, false);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00024FA0 File Offset: 0x000231A0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/HD-Common;component/uielements/customcombobox.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0000651E File Offset: 0x0000471E
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			this._contentLoaded = true;
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00024FD0 File Offset: 0x000231D0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				EventSetter eventSetter = new EventSetter();
				eventSetter.Event = FrameworkElement.RequestBringIntoViewEvent;
				eventSetter.Handler = new RequestBringIntoViewEventHandler(this.OnRequestBringIntoView);
				((Style)target).Setters.Add(eventSetter);
			}
		}

		// Token: 0x040003DE RID: 990
		public static readonly DependencyProperty ToolTipTextProperty = DependencyProperty.Register("ToolTipText", typeof(string), typeof(CustomComboBox), new PropertyMetadata(string.Empty));

		// Token: 0x040003DF RID: 991
		public static readonly DependencyProperty HighlightProperty = DependencyProperty.Register("Highlight", typeof(bool), typeof(CustomComboBox), new PropertyMetadata(false));

		// Token: 0x040003E0 RID: 992
		public static readonly DependencyProperty SetToolTipProperty = DependencyProperty.Register("SetToolTip", typeof(bool), typeof(CustomComboBox), new PropertyMetadata(false, new PropertyChangedCallback(CustomComboBox.OnSetToolTipChanged)));

		// Token: 0x040003E1 RID: 993
		public static readonly DependencyProperty ToolTipWhenTrimmedProperty = DependencyProperty.Register("ToolTipWhenTrimmed", typeof(bool), typeof(CustomComboBox), new PropertyMetadata(false));

		// Token: 0x040003E2 RID: 994
		private bool _contentLoaded;
	}
}
