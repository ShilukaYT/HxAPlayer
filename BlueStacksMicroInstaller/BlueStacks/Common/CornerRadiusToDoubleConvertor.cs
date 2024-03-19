using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace BlueStacks.Common
{
	// Token: 0x0200006C RID: 108
	public class CornerRadiusToDoubleConvertor : MarkupExtension, IValueConverter
	{
		// Token: 0x06000195 RID: 405 RVA: 0x00009BFC File Offset: 0x00007DFC
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return CornerRadiusToDoubleConvertor.Convert(value, targetType);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00009C18 File Offset: 0x00007E18
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return CornerRadiusToDoubleConvertor.Convert(value, targetType);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00009C34 File Offset: 0x00007E34
		public static object Convert(object value, Type targetType)
		{
			bool flag = typeof(double).Equals(targetType);
			object result;
			if (flag)
			{
				result = ((CornerRadius)value).TopLeft;
			}
			else
			{
				result = value;
			}
			return result;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00009C74 File Offset: 0x00007E74
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}
}
