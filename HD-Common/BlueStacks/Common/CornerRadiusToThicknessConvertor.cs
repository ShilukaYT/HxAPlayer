using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace BlueStacks.Common
{
	// Token: 0x0200021E RID: 542
	public class CornerRadiusToThicknessConvertor : MarkupExtension, IValueConverter
	{
		// Token: 0x060010DD RID: 4317 RVA: 0x0000E4C5 File Offset: 0x0000C6C5
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return CornerRadiusToThicknessConvertor.Convert(value, targetType);
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x0000E4C5 File Offset: 0x0000C6C5
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return CornerRadiusToThicknessConvertor.Convert(value, targetType);
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x000406D8 File Offset: 0x0003E8D8
		public static object Convert(object value, Type targetType)
		{
			if (typeof(Thickness).Equals(targetType))
			{
				CornerRadius cornerRadius = (CornerRadius)value;
				return new Thickness(cornerRadius.TopLeft, cornerRadius.TopRight, cornerRadius.BottomRight, cornerRadius.BottomLeft);
			}
			return value;
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x000046FB File Offset: 0x000028FB
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}
}
