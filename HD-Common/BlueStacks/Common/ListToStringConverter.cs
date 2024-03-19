using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x020000AD RID: 173
	public class ListToStringConverter : IValueConverter
	{
		// Token: 0x0600044C RID: 1100 RVA: 0x000178E0 File Offset: 0x00015AE0
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			IEnumerable<string> enumerable = value as IEnumerable<string>;
			if (enumerable != null)
			{
				return string.Join(" / ", enumerable.ToArray<string>());
			}
			return string.Empty;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x000030BB File Offset: 0x000012BB
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
