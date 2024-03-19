using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x020000B7 RID: 183
	public class ButtonImageNameToVisibilityConverter : IValueConverter
	{
		// Token: 0x06000495 RID: 1173 RVA: 0x00004A03 File Offset: 0x00002C03
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return string.IsNullOrEmpty((string)value) ? Visibility.Collapsed : Visibility.Visible;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000030BB File Offset: 0x000012BB
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
