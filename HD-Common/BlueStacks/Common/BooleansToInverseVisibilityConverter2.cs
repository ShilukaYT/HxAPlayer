using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x02000057 RID: 87
	public class BooleansToInverseVisibilityConverter2 : IValueConverter
	{
		// Token: 0x06000216 RID: 534 RVA: 0x00012068 File Offset: 0x00010268
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
			return (!flag) ? Visibility.Visible : Visibility.Collapsed;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000309C File Offset: 0x0000129C
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is Visibility)
			{
				return (Visibility)value > Visibility.Visible;
			}
			return true;
		}
	}
}
