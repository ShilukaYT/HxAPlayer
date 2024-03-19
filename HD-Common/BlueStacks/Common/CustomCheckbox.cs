using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace BlueStacks.Common
{
	// Token: 0x0200010E RID: 270
	public class CustomCheckbox : CheckBox, IComponentConnector, IStyleConnector
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x000062BE File Offset: 0x000044BE
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x000248A0 File Offset: 0x00022AA0
		public string Group
		{
			get
			{
				return this.mGroup;
			}
			set
			{
				this.mGroup = value;
				if (base.IsThreeState)
				{
					CustomCheckbox.dictInterminentTags[this.mGroup] = new Tuple<CustomCheckbox, List<CustomCheckbox>, List<CustomCheckbox>>(this, new List<CustomCheckbox>(), new List<CustomCheckbox>());
					return;
				}
				CustomCheckbox.dictInterminentTags[this.Group].Item3.Add(this);
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x000248F8 File Offset: 0x00022AF8
		public void SetInterminate()
		{
			if (base.IsChecked != null)
			{
				this._mSetInterminate = true;
				base.IsChecked = null;
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x000062C6 File Offset: 0x000044C6
		public Image Image
		{
			get
			{
				if (this.mImage == null)
				{
					this.mImage = (Image)base.Template.FindName("mImage", this);
				}
				return this.mImage;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x000062F2 File Offset: 0x000044F2
		public ColumnDefinition colDefMargin
		{
			get
			{
				return (ColumnDefinition)base.Template.FindName("colDefMargin", this);
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0000630A File Offset: 0x0000450A
		public ColumnDefinition colDefHorizontalLabel
		{
			get
			{
				return (ColumnDefinition)base.Template.FindName("colDefHorizontalLabel", this);
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x00006322 File Offset: 0x00004522
		public TextBlock BottomLabel
		{
			get
			{
				return (TextBlock)base.Template.FindName("VerticalTextBlock", this);
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0000633A File Offset: 0x0000453A
		public TextBlock CheckboxText
		{
			get
			{
				return (TextBlock)base.Template.FindName("HorizontalTextBlock", this);
			}
		}

		// Token: 0x170001E5 RID: 485
		// (set) Token: 0x06000737 RID: 1847 RVA: 0x00006352 File Offset: 0x00004552
		public Orientation Orientation
		{
			set
			{
				this.mOrientaion = value;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x0000635B File Offset: 0x0000455B
		public Visibility LabelVisibility
		{
			set
			{
				this.mLabelVisibility = value;
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x00006364 File Offset: 0x00004564
		public CustomCheckbox()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0002492C File Offset: 0x00022B2C
		private void CheckBox_MouseEnter(object sender, MouseEventArgs e)
		{
			if (base.IsChecked != null && !base.IsChecked.Value)
			{
				CustomPictureBox.SetBitmapImage(this.Image, "check_box_hover", false);
			}
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0002496C File Offset: 0x00022B6C
		private void CheckBox_MouseLeave(object sender, MouseEventArgs e)
		{
			if (base.IsChecked == null)
			{
				CustomPictureBox.SetBitmapImage(this.Image, "check_box_Indeterminate", false);
				return;
			}
			if (base.IsChecked.Value)
			{
				CustomPictureBox.SetBitmapImage(this.Image, "check_box_checked", false);
				return;
			}
			CustomPictureBox.SetBitmapImage(this.Image, "check_box", false);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x000249D0 File Offset: 0x00022BD0
		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(this.mGroup))
			{
				if (base.IsThreeState)
				{
					if (CustomCheckbox.dictInterminentTags[this.mGroup].Item3.Count > 0)
					{
						CustomCheckbox[] array = CustomCheckbox.dictInterminentTags[this.mGroup].Item3.ToArray();
						for (int i = 0; i < array.Length; i++)
						{
							array[i].IsChecked = new bool?(true);
						}
					}
				}
				else
				{
					CustomCheckbox.dictInterminentTags[this.mGroup].Item2.Add(this);
					CustomCheckbox.dictInterminentTags[this.mGroup].Item3.Remove(this);
					if (CustomCheckbox.dictInterminentTags[this.mGroup].Item3.Count == 0)
					{
						CustomCheckbox.dictInterminentTags[this.mGroup].Item1.IsChecked = new bool?(true);
					}
					else
					{
						CustomCheckbox.dictInterminentTags[this.mGroup].Item1.SetInterminate();
					}
				}
			}
			if (this.Image != null)
			{
				CustomPictureBox.SetBitmapImage(this.Image, "check_box_checked", false);
			}
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0000637D File Offset: 0x0000457D
		private void CheckBox_Indeterminate(object sender, RoutedEventArgs e)
		{
			if (!this._mSetInterminate)
			{
				base.IsChecked = new bool?(false);
				return;
			}
			this._mSetInterminate = false;
			if (this.Image != null)
			{
				CustomPictureBox.SetBitmapImage(this.Image, "check_box_Indeterminate", false);
			}
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00024AFC File Offset: 0x00022CFC
		private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(this.mGroup))
			{
				if (base.IsThreeState)
				{
					if (CustomCheckbox.dictInterminentTags[this.mGroup].Item2.Count > 0)
					{
						CustomCheckbox[] array = CustomCheckbox.dictInterminentTags[this.mGroup].Item2.ToArray();
						for (int i = 0; i < array.Length; i++)
						{
							array[i].IsChecked = new bool?(false);
						}
					}
				}
				else
				{
					CustomCheckbox.dictInterminentTags[this.mGroup].Item2.Remove(this);
					CustomCheckbox.dictInterminentTags[this.mGroup].Item3.Add(this);
					if (CustomCheckbox.dictInterminentTags[this.mGroup].Item2.Count == 0)
					{
						CustomCheckbox.dictInterminentTags[this.mGroup].Item1.IsChecked = new bool?(false);
					}
					else
					{
						CustomCheckbox.dictInterminentTags[this.mGroup].Item1.SetInterminate();
					}
				}
			}
			if (base.IsMouseOver)
			{
				if (this.Image != null)
				{
					CustomPictureBox.SetBitmapImage(this.Image, "check_box_hover", false);
					return;
				}
			}
			else if (this.Image != null)
			{
				CustomPictureBox.SetBitmapImage(this.Image, "check_box", false);
			}
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00024C48 File Offset: 0x00022E48
		private void CheckBox_Loaded(object sender, RoutedEventArgs e)
		{
			if (!DesignerProperties.GetIsInDesignMode(this))
			{
				if (this.mOrientaion == Orientation.Vertical)
				{
					Grid.SetRowSpan(this.Image, 1);
					this.colDefHorizontalLabel.Width = new GridLength(0.0);
					this.BottomLabel.Visibility = Visibility.Visible;
				}
				if (this.mLabelVisibility == Visibility.Hidden)
				{
					this.CheckboxText.Visibility = Visibility.Hidden;
					this.BottomLabel.Visibility = Visibility.Hidden;
				}
				if (this.Image != null)
				{
					if (base.IsChecked == null)
					{
						CustomPictureBox.SetBitmapImage(this.Image, "check_box__Indeterminate", false);
						return;
					}
					if (base.IsChecked != null && base.IsChecked.Value)
					{
						CustomPictureBox.SetBitmapImage(this.Image, "check_box_checked", false);
						return;
					}
					CustomPictureBox.SetBitmapImage(this.Image, "check_box", false);
				}
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x000063B4 File Offset: 0x000045B4
		// (set) Token: 0x06000741 RID: 1857 RVA: 0x000063C6 File Offset: 0x000045C6
		public Thickness ImageMargin
		{
			get
			{
				return (Thickness)base.GetValue(CustomCheckbox.ImageMarginProperty);
			}
			set
			{
				base.SetValue(CustomCheckbox.ImageMarginProperty, value);
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x000063D9 File Offset: 0x000045D9
		// (set) Token: 0x06000743 RID: 1859 RVA: 0x000063EB File Offset: 0x000045EB
		public TextWrapping TextWrapping
		{
			get
			{
				return (TextWrapping)base.GetValue(CustomCheckbox.TextWrappingProperty);
			}
			set
			{
				base.SetValue(CustomCheckbox.TextWrappingProperty, value);
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x000063FE File Offset: 0x000045FE
		// (set) Token: 0x06000745 RID: 1861 RVA: 0x00006410 File Offset: 0x00004610
		public TextTrimming TextTrimming
		{
			get
			{
				return (TextTrimming)base.GetValue(CustomCheckbox.TextTrimmingProperty);
			}
			set
			{
				base.SetValue(CustomCheckbox.TextTrimmingProperty, value);
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x00006423 File Offset: 0x00004623
		// (set) Token: 0x06000747 RID: 1863 RVA: 0x00006435 File Offset: 0x00004635
		public double TextFontSize
		{
			get
			{
				return (double)base.GetValue(CustomCheckbox.TextFontSizeProperty);
			}
			set
			{
				base.SetValue(CustomCheckbox.TextFontSizeProperty, value);
			}
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00024D28 File Offset: 0x00022F28
		private void CheckBoxText_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			ContentPresenter contentPresenter = WpfUtils.FindVisualChild<ContentPresenter>(this);
			if (contentPresenter != null)
			{
				TextBlock textBlock = contentPresenter.ContentTemplate.FindName("HorizontalTextBlock", contentPresenter) as TextBlock;
				if (textBlock != null && textBlock.IsTextTrimmed())
				{
					ToolTipService.SetIsEnabled(this, true);
					return;
				}
				ToolTipService.SetIsEnabled(this, false);
			}
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x00024D70 File Offset: 0x00022F70
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Uri resourceLocator = new Uri("/HD-Common;component/uielements/customcheckbox.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00024DA0 File Offset: 0x00022FA0
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IComponentConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 1)
			{
				((CustomCheckbox)target).MouseEnter += this.CheckBox_MouseEnter;
				((CustomCheckbox)target).MouseLeave += this.CheckBox_MouseLeave;
				((CustomCheckbox)target).Checked += this.CheckBox_Checked;
				((CustomCheckbox)target).Unchecked += this.CheckBox_Unchecked;
				((CustomCheckbox)target).Indeterminate += this.CheckBox_Indeterminate;
				((CustomCheckbox)target).Loaded += this.CheckBox_Loaded;
				return;
			}
			this._contentLoaded = true;
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x00006448 File Offset: 0x00004648
		[DebuggerNonUserCode]
		[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		void IStyleConnector.Connect(int connectionId, object target)
		{
			if (connectionId == 2)
			{
				((TextBlock)target).SizeChanged += this.CheckBoxText_SizeChanged;
			}
		}

		// Token: 0x040003D3 RID: 979
		public static readonly Dictionary<string, Tuple<CustomCheckbox, List<CustomCheckbox>, List<CustomCheckbox>>> dictInterminentTags = new Dictionary<string, Tuple<CustomCheckbox, List<CustomCheckbox>, List<CustomCheckbox>>>();

		// Token: 0x040003D4 RID: 980
		private string mGroup = string.Empty;

		// Token: 0x040003D5 RID: 981
		private bool _mSetInterminate;

		// Token: 0x040003D6 RID: 982
		private Image mImage;

		// Token: 0x040003D7 RID: 983
		private Orientation mOrientaion;

		// Token: 0x040003D8 RID: 984
		private Visibility mLabelVisibility;

		// Token: 0x040003D9 RID: 985
		public static readonly DependencyProperty ImageMarginProperty = DependencyProperty.Register("ImageMargin", typeof(Thickness), typeof(CustomCheckbox), new PropertyMetadata(new Thickness(0.0)));

		// Token: 0x040003DA RID: 986
		public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register("TextWrapping", typeof(TextWrapping), typeof(CustomCheckbox), new PropertyMetadata(TextWrapping.NoWrap));

		// Token: 0x040003DB RID: 987
		public static readonly DependencyProperty TextTrimmingProperty = DependencyProperty.Register("TextTrimming", typeof(TextTrimming), typeof(CustomCheckbox), new PropertyMetadata(TextTrimming.CharacterEllipsis));

		// Token: 0x040003DC RID: 988
		public static readonly DependencyProperty TextFontSizeProperty = DependencyProperty.Register("TextFontSize", typeof(double), typeof(CustomCheckbox), new PropertyMetadata(16.0));

		// Token: 0x040003DD RID: 989
		private bool _contentLoaded;
	}
}
