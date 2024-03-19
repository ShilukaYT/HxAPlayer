using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x020000FA RID: 250
	public class MergeMacroFirstItemToMarginConverter : IValueConverter
	{
		// Token: 0x06000634 RID: 1588 RVA: 0x0001CD6C File Offset: 0x0001AF6C
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ((bool)value) ? new Thickness(0.0, 1.0, 0.0, 0.0) : new Thickness(0.0);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00005A11 File Offset: 0x00003C11
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException("ConvertBack() of MergeMacroLastItemToBorderThicknessConverter is not implemented");
		}
	}
}
