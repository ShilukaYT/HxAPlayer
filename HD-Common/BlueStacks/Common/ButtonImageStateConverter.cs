using System;
using System.Globalization;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x020000B8 RID: 184
	public class ButtonImageStateConverter : IValueConverter
	{
		// Token: 0x06000498 RID: 1176 RVA: 0x00004A1B File Offset: 0x00002C1B
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (parameter == null)
			{
				return Binding.DoNothing;
			}
			if (!string.IsNullOrEmpty((string)value))
			{
				return value.ToString() + "_" + parameter.ToString();
			}
			return "";
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x000030BB File Offset: 0x000012BB
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
