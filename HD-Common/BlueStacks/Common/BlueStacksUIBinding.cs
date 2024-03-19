using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BlueStacks.Common
{
	// Token: 0x020000FC RID: 252
	public class BlueStacksUIBinding : INotifyPropertyChanged
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x00005A34 File Offset: 0x00003C34
		// (set) Token: 0x0600063B RID: 1595 RVA: 0x00005A4C File Offset: 0x00003C4C
		public static BlueStacksUIBinding Instance
		{
			get
			{
				if (BlueStacksUIBinding._Instance == null)
				{
					BlueStacksUIBinding._Instance = new BlueStacksUIBinding();
				}
				return BlueStacksUIBinding._Instance;
			}
			set
			{
				BlueStacksUIBinding._Instance = value;
			}
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00005A54 File Offset: 0x00003C54
		private BlueStacksUIBinding()
		{
			LocaleStrings.SourceUpdatedEvent += this.Locale_Updated;
			BluestacksUIColor.SourceUpdatedEvent += this.BluestacksUIColor_Updated;
			CustomPictureBox.SourceUpdatedEvent += this.BluestacksImage_Updated;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00005A8F File Offset: 0x00003C8F
		public void Locale_Updated(object sender, EventArgs e)
		{
			this.NotifyPropertyChanged("LocaleModel");
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00005A9C File Offset: 0x00003C9C
		public void BluestacksUIColor_Updated(object sender, EventArgs e)
		{
			this.NotifyPropertyChanged("ColorModel");
			this.NotifyPropertyChanged("GeometryModel");
			this.NotifyPropertyChanged("CornerRadiusModel");
			this.NotifyPropertyChanged("TransformModel");
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00005ACA File Offset: 0x00003CCA
		public void BluestacksImage_Updated(object sender, EventArgs e)
		{
			this.NotifyPropertyChanged("ImageModel");
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000640 RID: 1600 RVA: 0x0001CDF0 File Offset: 0x0001AFF0
		// (remove) Token: 0x06000641 RID: 1601 RVA: 0x0001CE28 File Offset: 0x0001B028
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x00005AD7 File Offset: 0x00003CD7
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x00003C8E File Offset: 0x00001E8E
		public Dictionary<string, Brush> ColorModel
		{
			get
			{
				return BlueStacksUIColorManager.AppliedTheme.DictBrush;
			}
			set
			{
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x00005AE3 File Offset: 0x00003CE3
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x00003C8E File Offset: 0x00001E8E
		public Dictionary<string, Geometry> GeometryModel
		{
			get
			{
				return BlueStacksUIColorManager.AppliedTheme.DictGeometry;
			}
			set
			{
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x00005AEF File Offset: 0x00003CEF
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x00003C8E File Offset: 0x00001E8E
		public Dictionary<string, CornerRadius> CornerRadiusModel
		{
			get
			{
				return BlueStacksUIColorManager.AppliedTheme.DictCornerRadius;
			}
			set
			{
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00005AFB File Offset: 0x00003CFB
		// (set) Token: 0x06000649 RID: 1609 RVA: 0x00003C8E File Offset: 0x00001E8E
		public Dictionary<string, Transform> TransformModel
		{
			get
			{
				return BlueStacksUIColorManager.AppliedTheme.DictTransform;
			}
			set
			{
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600064A RID: 1610 RVA: 0x00005B07 File Offset: 0x00003D07
		// (set) Token: 0x0600064B RID: 1611 RVA: 0x00003C8E File Offset: 0x00001E8E
		public Dictionary<string, string> LocaleModel
		{
			get
			{
				return LocaleStrings.DictLocalizedString;
			}
			set
			{
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600064C RID: 1612 RVA: 0x00005B0E File Offset: 0x00003D0E
		// (set) Token: 0x0600064D RID: 1613 RVA: 0x00003C8E File Offset: 0x00001E8E
		public Dictionary<string, Tuple<BitmapImage, bool>> ImageModel
		{
			get
			{
				return CustomPictureBox.sImageAssetsDict;
			}
			set
			{
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00005B15 File Offset: 0x00003D15
		public void NotifyPropertyChanged(string name)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(name));
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00005B2E File Offset: 0x00003D2E
		public static void Bind(UserControl uc, string path)
		{
			BindingOperations.SetBinding(uc, FrameworkElement.ToolTipProperty, BlueStacksUIBinding.GetLocaleBinding(path));
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00005B42 File Offset: 0x00003D42
		public static void Bind(CustomRadioButton tb, string path)
		{
			BindingOperations.SetBinding(tb, ContentControl.ContentProperty, BlueStacksUIBinding.GetLocaleBinding(path));
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00005B42 File Offset: 0x00003D42
		public static void Bind(ToggleButton tb, string path)
		{
			BindingOperations.SetBinding(tb, ContentControl.ContentProperty, BlueStacksUIBinding.GetLocaleBinding(path));
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00005B56 File Offset: 0x00003D56
		public static void Bind(GroupBox gb, string path)
		{
			BindingOperations.SetBinding(gb, HeaderedContentControl.HeaderProperty, BlueStacksUIBinding.GetLocaleBinding(path));
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00005B42 File Offset: 0x00003D42
		public static void Bind(Label label, string path)
		{
			BindingOperations.SetBinding(label, ContentControl.ContentProperty, BlueStacksUIBinding.GetLocaleBinding(path));
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00005B6A File Offset: 0x00003D6A
		public static void Bind(Run run, string path)
		{
			BindingOperations.SetBinding(run, TextBlock.TextProperty, BlueStacksUIBinding.GetLocaleBinding(path));
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00005B7E File Offset: 0x00003D7E
		public static void Bind(Window wind, string path)
		{
			BindingOperations.SetBinding(wind, Window.TitleProperty, BlueStacksUIBinding.GetLocaleBinding(path));
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x0001CE60 File Offset: 0x0001B060
		public static void Bind(TextBlock tb, string path, string stringFormat = "")
		{
			Binding localeBinding = BlueStacksUIBinding.GetLocaleBinding(path);
			if (!string.IsNullOrEmpty(stringFormat))
			{
				localeBinding.StringFormat = stringFormat;
			}
			BindingOperations.SetBinding(tb, TextBlock.TextProperty, localeBinding);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00005B92 File Offset: 0x00003D92
		public static void Bind(DependencyObject icon, string path, DependencyProperty dp)
		{
			BindingOperations.SetBinding(icon, dp, BlueStacksUIBinding.GetLocaleBinding(path));
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00005BA2 File Offset: 0x00003DA2
		public static void ClearBind(DependencyObject icon, DependencyProperty dp)
		{
			BindingOperations.ClearBinding(icon, dp);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00005B42 File Offset: 0x00003D42
		public static void Bind(ComboBoxItem comboBoxItem, string path)
		{
			BindingOperations.SetBinding(comboBoxItem, ContentControl.ContentProperty, BlueStacksUIBinding.GetLocaleBinding(path));
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00005B42 File Offset: 0x00003D42
		public static void Bind(Button button, string path)
		{
			BindingOperations.SetBinding(button, ContentControl.ContentProperty, BlueStacksUIBinding.GetLocaleBinding(path));
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00005B2E File Offset: 0x00003D2E
		public static void Bind(Image button, string path)
		{
			BindingOperations.SetBinding(button, FrameworkElement.ToolTipProperty, BlueStacksUIBinding.GetLocaleBinding(path));
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00005BAB File Offset: 0x00003DAB
		public static void Bind(TextBox textBox, string path)
		{
			BindingOperations.SetBinding(textBox, TextBox.TextProperty, BlueStacksUIBinding.GetLocaleBinding(path));
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x0001CE90 File Offset: 0x0001B090
		public static Binding GetLocaleBinding(string path)
		{
			Binding binding = new Binding
			{
				Source = BlueStacksUIBinding.Instance
			};
			string text = "";
			if (path != null)
			{
				for (int i = 0; i < path.Length; i++)
				{
					text = text + "^" + path[i].ToString();
				}
			}
			binding.Path = new PropertyPath("Instance.LocaleModel.[" + text.ToUpper(CultureInfo.InvariantCulture) + "]", new object[0]);
			binding.Mode = BindingMode.OneWay;
			binding.FallbackValue = LocaleStrings.RemoveConstants(path);
			binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
			LocaleStrings.AppendLocaleIfDoesntExist(path, LocaleStrings.RemoveConstants(path));
			return binding;
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00005BBF File Offset: 0x00003DBF
		public static void BindColor(DependencyObject dObj, DependencyProperty dp, string path)
		{
			BindingOperations.SetBinding(dObj, dp, BlueStacksUIBinding.GetColorBinding(path));
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x00005BCF File Offset: 0x00003DCF
		public static void BindCornerRadius(DependencyObject dObj, DependencyProperty dp, string path)
		{
			BindingOperations.SetBinding(dObj, dp, BlueStacksUIBinding.GetCornerRadiusBinding(path));
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00005BDF File Offset: 0x00003DDF
		public static void BindCornerRadiusToDouble(DependencyObject dObj, DependencyProperty dp, string path)
		{
			BindingOperations.SetBinding(dObj, dp, BlueStacksUIBinding.GetCornerRadiusDoubleBinding(path));
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0001CF3C File Offset: 0x0001B13C
		public static Binding GetColorBinding(string path)
		{
			return new Binding
			{
				Converter = new BrushToColorConvertor(),
				Source = BlueStacksUIBinding.Instance,
				Path = new PropertyPath("Instance.ColorModel.[" + path + "]", new object[0]),
				Mode = BindingMode.OneWay,
				UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
			};
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x0001CF98 File Offset: 0x0001B198
		public static Binding GetCornerRadiusBinding(string path)
		{
			return new Binding
			{
				Converter = new CornerRadiusToThicknessConvertor(),
				Source = BlueStacksUIBinding.Instance,
				Path = new PropertyPath("Instance.CornerRadiusModel.[" + path + "]", new object[0]),
				Mode = BindingMode.OneWay,
				UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
			};
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x0001CFF4 File Offset: 0x0001B1F4
		public static Binding GetCornerRadiusDoubleBinding(string path)
		{
			return new Binding
			{
				Converter = new CornerRadiusToDoubleConvertor(),
				Source = BlueStacksUIBinding.Instance,
				Path = new PropertyPath("Instance.CornerRadiusModel.[" + path + "]", new object[0]),
				Mode = BindingMode.OneWay,
				UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
			};
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00005BEF File Offset: 0x00003DEF
		public static void Bind(Image button, DependencyProperty dp, string path)
		{
			BindingOperations.SetBinding(button, dp, BlueStacksUIBinding.GetImageBinding(path));
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001D050 File Offset: 0x0001B250
		public static Binding GetImageBinding(string path)
		{
			return new Binding
			{
				Source = BlueStacksUIBinding.Instance,
				Path = new PropertyPath("Instance.ImageModel.[" + path + "].Item1", new object[0]),
				Mode = BindingMode.OneWay,
				UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
			};
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00005BFF File Offset: 0x00003DFF
		public static void BindTransform(DependencyObject button, DependencyProperty dp, string path)
		{
			BindingOperations.SetBinding(button, dp, BlueStacksUIBinding.GetTransformBinding(path));
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x0001D0A0 File Offset: 0x0001B2A0
		public static Binding GetTransformBinding(string path)
		{
			return new Binding
			{
				Source = BlueStacksUIBinding.Instance,
				Path = new PropertyPath("Instance.TransformModel.[" + path + "]", new object[0]),
				Mode = BindingMode.OneWay,
				UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
			};
		}

		// Token: 0x04000356 RID: 854
		private static BlueStacksUIBinding _Instance;
	}
}
