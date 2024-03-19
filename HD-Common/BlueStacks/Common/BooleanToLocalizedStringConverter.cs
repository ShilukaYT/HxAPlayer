using System;
using System.Globalization;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x02000058 RID: 88
	public class BooleanToLocalizedStringConverter : IValueConverter
	{
		// Token: 0x06000219 RID: 537 RVA: 0x000120B8 File Offset: 0x000102B8
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
				return LocaleStrings.GetLocalizedString(array[1], "");
			}
			return LocaleStrings.GetLocalizedString(array[0], "");
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000030BB File Offset: 0x000012BB
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
