using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x020000B6 RID: 182
	public class ButtonCornerRadiusMultiConverter : IMultiValueConverter
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x00017EEC File Offset: 0x000160EC
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values == null)
			{
				return Binding.DoNothing;
			}
			CornerRadius cornerRadius = (CornerRadius)values[0];
			double topLeft = (cornerRadius.TopLeft == 0.0) ? 0.0 : ((double)values[1] / cornerRadius.TopLeft);
			double topRight = (cornerRadius.TopRight == 0.0) ? 0.0 : ((double)values[1] / cornerRadius.TopRight);
			double bottomRight = (cornerRadius.BottomRight == 0.0) ? 0.0 : ((double)values[1] / cornerRadius.BottomRight);
			double bottomLeft = (cornerRadius.BottomLeft == 0.0) ? 0.0 : ((double)values[1] / cornerRadius.BottomLeft);
			return new CornerRadius(topLeft, topRight, bottomRight, bottomLeft);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00004A00 File Offset: 0x00002C00
		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
