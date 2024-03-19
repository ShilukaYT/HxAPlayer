using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace BlueStacks.Common
{
	// Token: 0x020000FF RID: 255
	public class CenterToolTipConverter : MarkupExtension, IMultiValueConverter
	{
		// Token: 0x060006AB RID: 1707 RVA: 0x00021100 File Offset: 0x0001F300
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values.FirstOrDefault((object v) => v == DependencyProperty.UnsetValue) != null)
			{
				return double.NaN;
			}
			double num = 0.0;
			double num2 = 0.0;
			if (values != null)
			{
				num = (double)values[0];
				num2 = (double)values[1];
			}
			return num / 2.0 - num2 / 2.0;
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000046F4 File Offset: 0x000028F4
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x000046FB File Offset: 0x000028FB
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}
}
