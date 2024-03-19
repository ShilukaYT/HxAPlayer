using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x020000A1 RID: 161
	public class EnumToVisibilityConverter : IValueConverter
	{
		// Token: 0x06000376 RID: 886 RVA: 0x00003D2A File Offset: 0x00001F2A
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null || parameter == null)
			{
				return Binding.DoNothing;
			}
			return value.ToString().Equals(parameter.ToString(), StringComparison.InvariantCultureIgnoreCase) ? Visibility.Visible : Visibility.Collapsed;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000030BB File Offset: 0x000012BB
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
