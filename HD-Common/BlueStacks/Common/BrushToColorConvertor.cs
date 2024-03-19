using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace BlueStacks.Common
{
	// Token: 0x0200021D RID: 541
	public class BrushToColorConvertor : MarkupExtension, IValueConverter
	{
		// Token: 0x060010D8 RID: 4312 RVA: 0x0000E4BC File Offset: 0x0000C6BC
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return BrushToColorConvertor.Convert(value, targetType);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x0000E4BC File Offset: 0x0000C6BC
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return BrushToColorConvertor.Convert(value, targetType);
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x0004068C File Offset: 0x0003E88C
		public static object Convert(object value, Type targetType)
		{
			if (typeof(SolidColorBrush).IsSubclassOf(targetType))
			{
				return value;
			}
			if (value != null)
			{
				SolidColorBrush solidColorBrush = value as SolidColorBrush;
				return (solidColorBrush != null) ? new Color?(solidColorBrush.Color) : null;
			}
			return value;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x000046FB File Offset: 0x000028FB
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}
}
