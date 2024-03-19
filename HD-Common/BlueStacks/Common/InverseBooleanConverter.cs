using System;
using System.Globalization;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x020000F8 RID: 248
	public class InverseBooleanConverter : IValueConverter
	{
		// Token: 0x0600062E RID: 1582 RVA: 0x000059E6 File Offset: 0x00003BE6
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool)
			{
				return !(bool)value;
			}
			return false;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00005A05 File Offset: 0x00003C05
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException("ConvertBack() of BoolToInvertedBoolConverter is not implemented");
		}
	}
}
