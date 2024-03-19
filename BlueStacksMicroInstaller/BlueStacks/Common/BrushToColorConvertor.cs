using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace BlueStacks.Common
{
	// Token: 0x0200006A RID: 106
	public class BrushToColorConvertor : MarkupExtension, IValueConverter
	{
		// Token: 0x0600018B RID: 395 RVA: 0x00009AA0 File Offset: 0x00007CA0
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return BrushToColorConvertor.Convert(value, targetType);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00009ABC File Offset: 0x00007CBC
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return BrushToColorConvertor.Convert(value, targetType);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00009AD8 File Offset: 0x00007CD8
		public static object Convert(object value, Type targetType)
		{
			bool flag = typeof(SolidColorBrush).IsSubclassOf(targetType);
			object result;
			if (flag)
			{
				result = value;
			}
			else
			{
				bool flag2 = value != null;
				if (flag2)
				{
					SolidColorBrush solidColorBrush = value as SolidColorBrush;
					result = ((solidColorBrush != null) ? new Color?(solidColorBrush.Color) : null);
				}
				else
				{
					result = value;
				}
			}
			return result;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00009B38 File Offset: 0x00007D38
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}
}
