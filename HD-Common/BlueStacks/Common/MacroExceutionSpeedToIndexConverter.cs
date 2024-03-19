using System;
using System.Globalization;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x020000F9 RID: 249
	public class MacroExceutionSpeedToIndexConverter : IValueConverter
	{
		// Token: 0x06000631 RID: 1585 RVA: 0x0001CCE4 File Offset: 0x0001AEE4
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double num = System.Convert.ToDouble(value, CultureInfo.InvariantCulture);
			if (num == 0.0)
			{
				return 1;
			}
			return (int)(num / 0.5 - 1.0);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001CD2C File Offset: 0x0001AF2C
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			int num = System.Convert.ToInt32(value, CultureInfo.InvariantCulture);
			if (num < 0)
			{
				return 1.0;
			}
			return (double)(num + 1) * 0.5;
		}
	}
}
