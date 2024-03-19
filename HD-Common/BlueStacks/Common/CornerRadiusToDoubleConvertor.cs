using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace BlueStacks.Common
{
	// Token: 0x0200021F RID: 543
	public class CornerRadiusToDoubleConvertor : MarkupExtension, IValueConverter
	{
		// Token: 0x060010E2 RID: 4322 RVA: 0x0000E4CE File Offset: 0x0000C6CE
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return CornerRadiusToDoubleConvertor.Convert(value, targetType);
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x0000E4CE File Offset: 0x0000C6CE
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return CornerRadiusToDoubleConvertor.Convert(value, targetType);
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00040728 File Offset: 0x0003E928
		public static object Convert(object value, Type targetType)
		{
			if (typeof(double).Equals(targetType))
			{
				return ((CornerRadius)value).TopLeft;
			}
			return value;
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x000046FB File Offset: 0x000028FB
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}
}
