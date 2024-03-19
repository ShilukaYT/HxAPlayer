using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BlueStacks.Common;

namespace BlueStacks.MicroInstaller
{
	// Token: 0x02000097 RID: 151
	public class BlueStacksUIBinding : INotifyPropertyChanged
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000FE08 File Offset: 0x0000E008
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x0000FE37 File Offset: 0x0000E037
		public static BlueStacksUIBinding Instance
		{
			get
			{
				bool flag = BlueStacksUIBinding._Instance == null;
				if (flag)
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

		// Token: 0x060002E9 RID: 745 RVA: 0x0000FE3F File Offset: 0x0000E03F
		private BlueStacksUIBinding()
		{
			BluestacksUIColor.SourceUpdatedEvent += this.BluestacksUIColor_Updated;
			CustomPictureBox.SourceUpdatedEvent += this.BluestacksImage_Updated;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000FE6D File Offset: 0x0000E06D
		public void BluestacksUIColor_Updated(object sender, EventArgs e)
		{
			this.NotifyPropertyChanged("ColorModel");
			this.NotifyPropertyChanged("GeometryModel");
			this.NotifyPropertyChanged("CornerRadiusModel");
			this.NotifyPropertyChanged("TransformModel");
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000FEA0 File Offset: 0x0000E0A0
		public void BluestacksImage_Updated(object sender, EventArgs e)
		{
			this.NotifyPropertyChanged("ImageModel");
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060002EC RID: 748 RVA: 0x0000FEB0 File Offset: 0x0000E0B0
		// (remove) Token: 0x060002ED RID: 749 RVA: 0x0000FEE8 File Offset: 0x0000E0E8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000FF1D File Offset: 0x0000E11D
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0000F8DB File Offset: 0x0000DADB
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

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000FF29 File Offset: 0x0000E129
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000F8DB File Offset: 0x0000DADB
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

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000FF35 File Offset: 0x0000E135
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000F8DB File Offset: 0x0000DADB
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

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000FF41 File Offset: 0x0000E141
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000F8DB File Offset: 0x0000DADB
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

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000FF4D File Offset: 0x0000E14D
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000F8DB File Offset: 0x0000DADB
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

		// Token: 0x060002F8 RID: 760 RVA: 0x0000FF54 File Offset: 0x0000E154
		public void NotifyPropertyChanged(string name)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged != null)
			{
				propertyChanged(this, new PropertyChangedEventArgs(name));
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000FF70 File Offset: 0x0000E170
		public static void BindColor(DependencyObject dObj, DependencyProperty dp, string path)
		{
			BindingOperations.SetBinding(dObj, dp, BlueStacksUIBinding.GetColorBinding(path));
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000FF84 File Offset: 0x0000E184
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

		// Token: 0x060002FB RID: 763 RVA: 0x0000FFE9 File Offset: 0x0000E1E9
		public static void Bind(Image button, DependencyProperty dp, string path)
		{
			BindingOperations.SetBinding(button, dp, BlueStacksUIBinding.GetImageBinding(path));
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000FFFC File Offset: 0x0000E1FC
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

		// Token: 0x04000511 RID: 1297
		private static BlueStacksUIBinding _Instance;
	}
}
