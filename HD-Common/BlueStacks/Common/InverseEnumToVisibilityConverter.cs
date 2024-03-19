using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x020000AC RID: 172
	public class InverseEnumToVisibilityConverter : IValueConverter
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x000046AC File Offset: 0x000028AC
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null || parameter == null)
			{
				return Binding.DoNothing;
			}
			return value.ToString().Equals(parameter.ToString(), StringComparison.InvariantCultureIgnoreCase) ? Visibility.Collapsed : Visibility.Visible;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000030BB File Offset: 0x000012BB
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
