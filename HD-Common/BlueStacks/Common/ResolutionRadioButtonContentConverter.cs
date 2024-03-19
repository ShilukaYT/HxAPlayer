using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x0200006D RID: 109
	public class ResolutionRadioButtonContentConverter : IValueConverter
	{
		// Token: 0x0600026B RID: 619 RVA: 0x000134C4 File Offset: 0x000116C4
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (parameter == null || value == null)
			{
				return Binding.DoNothing;
			}
			Dictionary<string, string> dictionary = (Dictionary<string, string>)value;
			if (dictionary.ContainsKey(parameter.ToString()))
			{
				return dictionary[parameter.ToString()];
			}
			return Binding.DoNothing;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000030BB File Offset: 0x000012BB
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
