using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x02000064 RID: 100
	public class EnumToVisibilityConverter2 : IValueConverter
	{
		// Token: 0x0600023F RID: 575 RVA: 0x000031D3 File Offset: 0x000013D3
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null || parameter == null)
			{
				return Binding.DoNothing;
			}
			return value.ToString().Equals(parameter.ToString(), StringComparison.InvariantCultureIgnoreCase) ? Visibility.Visible : Visibility.Hidden;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000030BB File Offset: 0x000012BB
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
