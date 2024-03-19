using System;
using System.Globalization;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x02000059 RID: 89
	public class BooleanToImageNameConverter : IValueConverter
	{
		// Token: 0x0600021C RID: 540 RVA: 0x00012148 File Offset: 0x00010348
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null || parameter == null)
			{
				return Binding.DoNothing;
			}
			string[] array = parameter.ToString().Split(new char[]
			{
				'|'
			});
			if (array.Length != 2)
			{
				return Binding.DoNothing;
			}
			bool flag = false;
			if (value is bool)
			{
				flag = (bool)value;
			}
			else if (value is bool?)
			{
				bool? flag2 = (bool?)value;
				flag = (flag2 != null && flag2.Value);
			}
			if (!flag)
			{
				return array[1];
			}
			return array[0];
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000030BB File Offset: 0x000012BB
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
