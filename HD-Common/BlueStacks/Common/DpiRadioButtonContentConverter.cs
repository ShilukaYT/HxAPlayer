using System;
using System.Globalization;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x02000062 RID: 98
	public class DpiRadioButtonContentConverter : IValueConverter
	{
		// Token: 0x06000239 RID: 569 RVA: 0x00003162 File Offset: 0x00001362
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (parameter == null || value == null)
			{
				return Binding.DoNothing;
			}
			return parameter.ToString() + " " + value.ToString();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000030BB File Offset: 0x000012BB
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
