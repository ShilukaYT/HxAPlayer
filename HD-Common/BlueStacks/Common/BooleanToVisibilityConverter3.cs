using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x0200005B RID: 91
	public class BooleanToVisibilityConverter3 : IValueConverter
	{
		// Token: 0x06000222 RID: 546 RVA: 0x00012214 File Offset: 0x00010414
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
			return (!flag) ? Visibility.Hidden : Visibility.Visible;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000030C2 File Offset: 0x000012C2
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
