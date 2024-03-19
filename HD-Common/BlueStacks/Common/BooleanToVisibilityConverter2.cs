using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x0200005A RID: 90
	public sealed class BooleanToVisibilityConverter2 : IValueConverter
	{
		// Token: 0x0600021F RID: 543 RVA: 0x000121C4 File Offset: 0x000103C4
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool flag = false;
			if (value is bool)
			{
				flag = (bool)value;
			}
			else if (value is bool?)
			{
				bool? flag2 = (bool?)value;
				flag = (flag2 != null && flag2.Value);
			}
			return (!flag) ? Visibility.Collapsed : Visibility.Visible;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000030C2 File Offset: 0x000012C2
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Visibility)
			{
				return (Visibility)value == Visibility.Visible;
			}
			return false;
		}
	}
}
