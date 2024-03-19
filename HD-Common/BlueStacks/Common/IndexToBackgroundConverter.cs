using System;
using System.Globalization;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x020000AB RID: 171
	public class IndexToBackgroundConverter : IValueConverter
	{
		// Token: 0x06000446 RID: 1094 RVA: 0x0000466E File Offset: 0x0000286E
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return Binding.DoNothing;
			}
			if ((int)value % 2 != 0)
			{
				return BlueStacksUIBinding.Instance.ColorModel["DarkBandingColor"];
			}
			return BlueStacksUIBinding.Instance.ColorModel["LightBandingColor"];
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000030BB File Offset: 0x000012BB
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
