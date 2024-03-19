using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BlueStacks.Common
{
	// Token: 0x020000AF RID: 175
	public class TextInputValidityToVisibilityConverter : IValueConverter
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x00017910 File Offset: 0x00015B10
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Visibility visibility = Visibility.Collapsed;
			if (value != null)
			{
				if (parameter == null)
				{
					TextValidityOptions textValidityOptions = (TextValidityOptions)Enum.Parse(typeof(TextValidityOptions), value.ToString());
					if (textValidityOptions == TextValidityOptions.Warning || textValidityOptions == TextValidityOptions.Info)
					{
						visibility = Visibility.Visible;
					}
				}
				else
				{
					visibility = (object.Equals(value, parameter) ? Visibility.Visible : Visibility.Collapsed);
				}
			}
			return visibility;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000030BB File Offset: 0x000012BB
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Binding.DoNothing;
		}
	}
}
