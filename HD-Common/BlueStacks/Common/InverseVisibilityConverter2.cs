using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x02000065 RID: 101
	public class InverseVisibilityConverter2 : IValueConverter
	{
		// Token: 0x06000242 RID: 578 RVA: 0x000031FE File Offset: 0x000013FE
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return Binding.DoNothing;
			}
			return ((Visibility)value == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000031FE File Offset: 0x000013FE
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return Binding.DoNothing;
			}
			return ((Visibility)value == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
		}
	}
}
