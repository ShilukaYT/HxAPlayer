using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace BlueStacks.Common
{
	// Token: 0x0200006B RID: 107
	public class CornerRadiusToThicknessConvertor : MarkupExtension, IValueConverter
	{
		// Token: 0x06000190 RID: 400 RVA: 0x00009B54 File Offset: 0x00007D54
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return CornerRadiusToThicknessConvertor.Convert(value, targetType);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00009B70 File Offset: 0x00007D70
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return CornerRadiusToThicknessConvertor.Convert(value, targetType);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00009B8C File Offset: 0x00007D8C
		public static object Convert(object value, Type targetType)
		{
			bool flag = typeof(Thickness).Equals(targetType);
			object result;
			if (flag)
			{
				CornerRadius cornerRadius = (CornerRadius)value;
				result = new Thickness(cornerRadius.TopLeft, cornerRadius.TopRight, cornerRadius.BottomRight, cornerRadius.BottomLeft);
			}
			else
			{
				result = value;
			}
			return result;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00009BE8 File Offset: 0x00007DE8
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}
}
