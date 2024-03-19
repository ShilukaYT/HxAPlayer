using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x020000B9 RID: 185
	public class InverseBooleanToVisibilityConverter : IValueConverter
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x00017FD0 File Offset: 0x000161D0
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Visibility visibility;
			if (value != null && (!(value is bool?) || !((bool?)value).GetValueOrDefault()))
			{
				bool flag;
				bool flag2;
				if (value is bool)
				{
					flag = (bool)value;
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
				if (!flag2 || !flag)
				{
					visibility = Visibility.Visible;
					goto IL_36;
				}
			}
			visibility = Visibility.Collapsed;
			IL_36:
			return visibility;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00018018 File Offset: 0x00016218
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool flag;
			if (value is Visibility)
			{
				Visibility visibility = (Visibility)value;
				flag = (visibility > Visibility.Visible);
			}
			else
			{
				flag = false;
			}
			return flag;
		}
	}
}
