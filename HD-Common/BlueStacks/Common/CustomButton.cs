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
	// Token: 0x0200010D RID: 269
	public class CustomButton : Button, IComponentConnector, IStyleConnector
	{
		// Token: 0x06000718 RID: 1816 RVA: 0x0000617A File Offset: 0x0000437A
		public CustomButton()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00006188 File Offset: 0x00004388
		public CustomButton(ButtonColors color) : this()
		{
			this.ButtonColor = color;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x00006197 File Offset: 0x00004397
		// (set) Token: 0x0600071B RID: 1819 RVA: 0x000061A9 File Offset: 0x000043A9
		public ButtonColors ButtonColor
		{
			get
			{
				return (ButtonColors)base.GetValue(CustomButton.ButtonColorProperty);
			}
			set
			{
				base.SetValue(CustomButton.ButtonColorProperty, value);
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x000061BC File Offset: 0x000043BC
		// (set) Token: 0x0600071D RID: 1821 RVA: 0x000061CE File Offset: 0x000043CE
		public bool IsMouseDown
		{
			get
			{
				return (bool)base.GetValue(CustomButton.IsMouseDownProperty);
			}
			set
			{
				base.SetValue(CustomButton.IsMouseDownProperty, value);
			}
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x000061E1 File Offset: 0x000043E1
		private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
		{
			this.IsMouseDown = true;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x000061EA File Offset: 0x000043EA
		private void Button_PreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			this.IsMouseDown = false;
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x000061F3 File Offset: 0x000043F3
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x00006205 File Offset: 0x00004405
		public ButtonImageOrder ImageOrder
		{
			get
			{
				return (ButtonImageOrder)base.GetValue(CustomButton.ImageOrderProperty);
			}
			set
			{
				base.SetValue(CustomButton.ImageOrderProperty, value);
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x00006218 File Offset: 0x00004418
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x0000622A File Offset: 0x0000442A
		public string ImageName
		{
			get
			{
				return (string)base.GetValue(CustomButton.ImageNameProperty);
			}
			set
			{
				base.SetValue(CustomButton.ImageNameProperty, value);
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x00006238 File Offset: 0x00004438
		// (set) Token: 0x06000725 RID: 1829 RVA: 0x0000624A File Offset: 0x0000444A
		public Thickness ImageMargin
		{
			get
			{
				return (Thickness)base.GetValue(CustomButton.ImageMarginProperty);
			}
			set
			{
				base.SetValue(CustomButton.ImageMarginProperty, value);
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000726 RID: 1830 RVA: 0x0000625D File Offset: 0x0000445D
		// (set) Token: 0x06000727 RID: 1831 RVA: 0x0000626F File Offset: 0x0000446F
		public bool IsForceTooltipRequired
		{
			get
			{
				return (bool)base.GetValue(CustomButton.IsForceTooltipRequiredProperty);
			}
			set
			{
				base.SetValue(CustomButton.IsForceTooltipRequiredProperty, value);
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00006282 File Offset: 0x00004482
		private void ButtonTextBlock_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			this.ToolTipIfTextTrimmed();
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00006282 File Offset: 0x00004482
		private void ButtonTextBlock_Loaded(object sender, RoutedEventArgs e)
		{
			this.ToolTipIfTextTrimmed();
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00024684 File Offset: 0x00022884
		private void ToolTipIfTextTrimmed()
		{
			if (!this.IsForceTooltipRequired)
			{
				ContentPresenter contentPresenter = WpfUtils.FindVisualChild<ContentPresenter>(this);
				if (contentPresenter != null)
				{
					TextBlock textBlock = contentPresenter.ContentTemplate.FindName("buttonTextBlock", contentPresenter) as TextBlock;
					if (textBlock != null && textBlock.IsTextTrimmed())
					{
						ToolTipService.SetIsEnabled(this, true);
						return;
					}
					ToolTipService.SetIsEnabled(this, false);
				}
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x000246D4 File Offset: 0x000228D4
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/HD-Common;component/uielements/custombutton.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00024704 File Offset: 0x00022904
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				this.mButton = (CustomButton)target;
				this.mButton.PreviewMouseDown += this.Button_PreviewMouseDown;
				this.mButton.PreviewMouseUp += this.Button_PreviewMouseUp;
				return;
			}
			this._contentLoaded = true;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0000628A File Offset: 0x0000448A
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 2)
			{
				((TextBlock)target).Loaded += this.ButtonTextBlock_Loaded;
				((TextBlock)target).SizeChanged += this.ButtonTextBlock_SizeChanged;
			}
		}

		// Token: 0x040003CB RID: 971
		public static readonly DependencyProperty ButtonColorProperty = DependencyProperty.Register("ButtonColor", typeof(ButtonColors), typeof(CustomButton), new PropertyMetadata(ButtonColors.Blue));

		// Token: 0x040003CC RID: 972
		public static readonly DependencyProperty IsMouseDownProperty = DependencyProperty.Register("IsMouseDown", typeof(bool), typeof(CustomButton), new PropertyMetadata(false));

		// Token: 0x040003CD RID: 973
		public static readonly DependencyProperty ImageOrderProperty = DependencyProperty.Register("ImageOrder", typeof(ButtonImageOrder), typeof(CustomButton), new PropertyMetadata(ButtonImageOrder.BeforeText));

		// Token: 0x040003CE RID: 974
		public static readonly DependencyProperty ImageNameProperty = DependencyProperty.Register("ImageName", typeof(string), typeof(CustomButton), new PropertyMetadata(""));

		// Token: 0x040003CF RID: 975
		public static readonly DependencyProperty ImageMarginProperty = DependencyProperty.Register("ImageMargin", typeof(Thickness), typeof(CustomButton), new PropertyMetadata(new Thickness(0.0, 0.0, 5.0, 0.0)));

		// Token: 0x040003D0 RID: 976
		public static readonly DependencyProperty IsForceTooltipRequiredProperty = DependencyProperty.Register("IsForceTooltipRequired", typeof(bool), typeof(CustomButton), new PropertyMetadata(false));

		// Token: 0x040003D1 RID: 977
		internal CustomButton mButton;

		// Token: 0x040003D2 RID: 978
		private bool _contentLoaded;
	}
}
